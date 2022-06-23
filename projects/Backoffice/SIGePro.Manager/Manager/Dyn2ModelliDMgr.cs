
using Init.SIGePro.Authentication;
using Init.SIGePro.Data;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.Validator;
using PersonalLib2.Data;
using PersonalLib2.Sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Init.SIGePro.Manager
{
    [DataObject(true)]
    public partial class Dyn2ModelliDMgr : IDyn2DettagliModelloManager
    {
        private bool m_generaErroriModificaRiga = true;

        public bool VerificaPresenzaInStorico(string idComune, int id)
        {
            Dyn2ModelliD riga = this.GetById(idComune, id);

            // La riga è una intestazione e non va a modificare lo storico
            if (!riga.FkD2cId.HasValue)
                return false;

            // Verifico se lo storico schede istanze contiene il modello
            int count = this.recordCount("istanzedyn2dati_storico", "*", String.Format("where idcomune='{0}' and fk_d2mt_id = {1} AND fk_d2c_id = {2}",
                                                                                    riga.Idcomune, riga.FkD2mtId, riga.FkD2cId));
            if (count > 0)
                return true;

            // Verifico se lo storico schede anagrafe contiene il modello
            count = this.recordCount("anagrafedyn2dati_storico", "*", String.Format("where idcomune='{0}' and fk_d2mt_id = {1} AND fk_d2c_id = {2}",
                                                                                    riga.Idcomune, riga.FkD2mtId, riga.FkD2cId));
            if (count > 0)
                return true;

            // Verifico se lo storico schede attivita contiene il modello






            return false;
        }



        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Dyn2ModelliD> Find(string token, int idModello)
        {
            AuthenticationInfo authInfo = AuthenticationManager.CheckToken(token);

            Dyn2ModelliDMgr mgr = new Dyn2ModelliDMgr(authInfo.CreateDatabase());

            Dyn2ModelliD filtro = new Dyn2ModelliD();

            filtro.Idcomune = authInfo.IdComune;
            filtro.FkD2mtId = idModello;
            filtro.OrderBy = "posverticale asc, posorizzontale asc";
            filtro.UseForeign = useForeignEnum.Recoursive;

            List<Dyn2ModelliD> list = mgr.GetList(filtro);

            return list;
        }

        /// <summary>
        /// Ritorna la lista dei soli campi dinamici del modello identificato dall'id passato.
        /// NOTA: Le foreign key s sono abilitate
        /// </summary>
        /// <param name="idComune">id comune</param>
        /// <param name="idModello">id modello</param>
        /// <returns>Lista di campi dinamici</returns>
        public List<Dyn2ModelliD> GetCampiDinamiciModello(string idComune, int idModello)
        {
            Dyn2ModelliD filtro = new Dyn2ModelliD();

            filtro.Idcomune = idComune;
            filtro.FkD2mtId = idModello;
            filtro.OthersWhereClause.Add("FK_D2C_ID is not null");
            filtro.UseForeign = useForeignEnum.Recoursive;

            return this.GetList(filtro);
        }

        /// <summary>
        /// Ritorna la lista dei soli campi statici del modello identificato dall'id passato.
        /// NOTA: Le foreign key s sono abilitate
        /// </summary>
        /// <param name="idComune">id comune</param>
        /// <param name="idModello">id modello</param>
        /// <returns>Lista di campi dinamici</returns>
        public List<Dyn2ModelliD> GetCampiStaticiModello(string idComune, int idModello)
        {
            Dyn2ModelliD filtro = new Dyn2ModelliD();

            filtro.Idcomune = idComune;
            filtro.FkD2mtId = idModello;
            filtro.OthersWhereClause.Add("FK_D2C_ID is null");
            filtro.UseForeign = useForeignEnum.Recoursive;

            return this.GetList(filtro);
        }

        public int GetProssimaPosizioneVerticale(string idComune, int idModello)
        {
            var where = new List<KeyValuePair<string, object>>{
                                                                    new KeyValuePair<string,object>( "fk_d2mt_id", idModello )
                                                                 };

            return this.FindMax("posverticale", "dyn2_modellid", idComune, 10, where);
        }

        public int GetProssimaPosizioneOrizzontale(string idComune, int idModello, int posVerticale)
        {
            var where = new List<KeyValuePair<string, object>>{
                                                                    new KeyValuePair<string,object>( "fk_d2mt_id", idModello ),
                                                                    new KeyValuePair<string,object>( "posverticale", posVerticale )
                                                                 };

            return this.FindMax("posorizzontale", "dyn2_modellid", idComune, where);
        }

        public void Delete(Dyn2ModelliD cls)
        {
            this.VerificaRecordCollegati(cls);

            var sql = $"delete from dyn2_modellid where idcomune={this.db.Specifics.QueryParameterName("idComune")} and id={this.db.Specifics.QueryParameterName("id")}";

            this.db.ExecuteNonQuery(sql, mp =>
            {
                mp.AddParameter("idComune", cls.Idcomune);
                mp.AddParameter("id", cls.Id.Value);
            });


            if (cls.FkD2mdtId.HasValue && !this.IsTestoCondivisoTraSchede(cls.Idcomune, cls.FkD2mdtId.Value))
            {
                new Dyn2ModelliDTestiMgr(this.db).Delete(cls.Idcomune, cls.FkD2mdtId.Value);
            }
        }

        private bool IsTestoCondivisoTraSchede(string idcomune, int idTesto)
        {
            var sql = $"select count(*) from dyn2_modellid where idcomune={this.db.Specifics.QueryParameterName("idComune")} and fk_d2mdt_id={this.db.Specifics.QueryParameterName("idTesto")}";

            var count = this.db.ExecuteScalar(sql, 0, mp =>
            {
                mp.AddParameter("idComune", idcomune);
                mp.AddParameter("idTesto", idTesto);
            });

            // La riga corrente è già stata eliminata
            return count > 0;
        }

        private void EffettuaCancellazioneACascata(Dyn2ModelliD cls)
        {


            /* Elimino tutti i dati storici (se esistono) relativi ad istanze, anagrafiche o attivita */
            // EliminaDatiStorico(cls.Idcomune, cls.FkD2mtId.Value);
        }
        /*
        private void EliminaDatiStorico(string idComune, int idModello)
        {
            bool inTransaction = db.Transaction != null;

            try
            {
                if (!inTransaction)
                    db.BeginTransaction();

                // Dati delle istanze
                IstanzeDyn2ModelliTStorico filtroIstanzeStorico = new IstanzeDyn2ModelliTStorico();
                filtroIstanzeStorico.Idcomune = idComune;
                filtroIstanzeStorico.FkD2mtId = idModello;

                IstanzeDyn2ModelliTStoricoMgr istStoricoMgr = new IstanzeDyn2ModelliTStoricoMgr(db);

                List<IstanzeDyn2ModelliTStorico> valoriIstanzeStorico = istStoricoMgr.GetList(filtroIstanzeStorico);

                for (int i = 0; i < valoriIstanzeStorico.Count; i++)
                    istStoricoMgr.Delete(valoriIstanzeStorico[i]);


                // dati delle anagrafiche
                AnagrafeDyn2ModelliTStorico filtroAnagrafeStorico = new AnagrafeDyn2ModelliTStorico();
                filtroAnagrafeStorico.Idcomune = idComune;
                filtroAnagrafeStorico.FkD2mtId = idModello;

                AnagrafeDyn2ModelliTStoricoMgr anagStoricoMgr = new AnagrafeDyn2ModelliTStoricoMgr(db);

                List<AnagrafeDyn2ModelliTStorico> valoriAnagrafeStorico = anagStoricoMgr.GetList(filtroAnagrafeStorico);

                for (int i = 0; i < valoriAnagrafeStorico.Count; i++)
                    anagStoricoMgr.Delete(valoriAnagrafeStorico[i]);


                // dati delle attivita


                if (!inTransaction)
                    db.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (!inTransaction)
                    db.RollbackTransaction();

                throw;
            }
        }
        */

        private void VerificaRecordCollegati(Dyn2ModelliD cls)
        {
            if (cls.FkD2mdtId.HasValue)  // il modello è un testo
                return;

            // Cerco di capire in che ambito è utilizzato il modello
            bool ambitoIstanza = false;
            bool ambitoAnagrafica = false;
            bool ambitoAttivita = false;

            Dyn2ModelliT modello = new Dyn2ModelliTMgr(this.db).GetById(cls.Idcomune, cls.FkD2mtId.GetValueOrDefault(-1));

            if (modello == null)
                throw new Exception("Impossibile trovare il modello associato alla riga " + cls.Id);

            switch (modello.FkD2bcId)
            {
                case ("IS"):
                    ambitoIstanza = true;
                    break;
                case ("AT"):
                    ambitoAttivita = true;
                    break;
                case ("AN"):
                    ambitoAnagrafica = true;
                    break;
                default:
                    ambitoAnagrafica = true;
                    ambitoAttivita = true;
                    ambitoIstanza = true;
                    break;
            }

            // verifico se il campo è utilizzato in un istanza
            if (ambitoIstanza)
            {
                // Il modello è utilizzato da qualche istanza?
                var sqlIstanza = $"select count(*) from istanzedyn2modellit where idcomune={this.db.Specifics.QueryParameterName("idcomune")} and fk_d2mt_id={this.db.Specifics.QueryParameterName("idModello")}";

                int recCountModello = this.db.ExecuteScalar(sqlIstanza, 0, mp =>
                {
                    mp.AddParameter("idcomune", cls.Idcomune);
                    mp.AddParameter("idModello", modello.Id);
                });

                if (recCountModello > 0)
                {
                    sqlIstanza = $@"select distinct numeroistanza from 
                                        istanze 
                                            inner join istanzedyn2dati on
                                                istanzedyn2dati.idcomune = istanze.idcomune and
                                                istanzedyn2dati.codiceistanza = istanze.codiceistanza
                                            inner join istanzedyn2modellit on 
                                                istanzedyn2dati.idcomune = istanzedyn2modellit.idcomune AND
                                                istanzedyn2dati.codiceistanza = istanzedyn2modellit.codiceistanza 
                                        where 
                                            istanzedyn2modellit.idcomune = {this.db.Specifics.QueryParameterName("idComune")} AND
                                            istanzedyn2modellit.fk_d2mt_id = {this.db.Specifics.QueryParameterName("idModello")} and
                                            istanzedyn2dati.fk_d2c_id = {this.db.Specifics.QueryParameterName("idCampo")}";

                    var numeriIstanza = this.db.ExecuteReader(sqlIstanza, mp =>
                    {
                        mp.AddParameter("idComune", cls.Idcomune);
                        mp.AddParameter("idModello", cls.FkD2mtId);
                        mp.AddParameter("idCampo", cls.FkD2cId);
                    }, dr => dr.GetString("numeroistanza"));

                    if (numeriIstanza.Any())
                    {
                        throw new Exception("Impossibile rimuovere il campo dal modello corrente in quanto è utilizzato nelle seguenti istanze: " + String.Join(", ", numeriIstanza.ToArray()));
                    }
                }
            }

            if (ambitoAnagrafica)
            {
                // Il modello è utilizzato da qualche istanza?
                var sqlAnagrafe = $"select count(*) from anagrafedyn2modellit where idcomune={this.db.Specifics.QueryParameterName("idcomune")} and fk_d2mt_id={this.db.Specifics.QueryParameterName("idModello")}";

                int recCountModello = this.db.ExecuteScalar(sqlAnagrafe, 0, mp =>
                {
                    mp.AddParameter("idcomune", cls.Idcomune);
                    mp.AddParameter("idModello", modello.Id);
                });

                if (recCountModello > 0)
                {
                    // il modello è usato da almeno un'anagrafica, verifico che il campo sia valorizzato
                    sqlAnagrafe = $@"select distinct anagrafe.codiceanagrafe from 
                                        anagrafe 
                                            inner join anagrafedyn2dati on
                                                anagrafedyn2dati.idcomune = anagrafe.idcomune and
                                                anagrafedyn2dati.codiceanagrafe = anagrafe.codiceanagrafe
                                            inner join anagrafedyn2modellit on 
                                                anagrafedyn2dati.idcomune = anagrafedyn2modellit.idcomune AND
                                                anagrafedyn2dati.codiceanagrafe = anagrafedyn2modellit.codiceanagrafe 
                                        where 
                                            anagrafedyn2modellit.idcomune = {this.db.Specifics.QueryParameterName("idComune")} AND
                                            anagrafedyn2modellit.fk_d2mt_id = {this.db.Specifics.QueryParameterName("idModello")} and
                                            anagrafedyn2dati.fk_d2c_id = {this.db.Specifics.QueryParameterName("idCampo")}";

                    var codiciAnagrafe = this.db.ExecuteReader(sqlAnagrafe, mp =>
                    {
                        mp.AddParameter("idComune", cls.Idcomune);
                        mp.AddParameter("idModello", cls.FkD2mtId);
                        mp.AddParameter("idCampo", cls.FkD2cId);
                    }, dr => dr.GetString("codiceanagrafe"));

                    if (codiciAnagrafe.Any())
                    {
                        throw new Exception("Impossibile rimuovere il campo dal modello corrente in quanto è utilizzato dai seguenti codici anagrafe: " + String.Join(", ", codiciAnagrafe.ToArray()));
                    }
                }
            }

            if (ambitoAttivita)
            {
                // Il modello è utilizzato da qualche istanza?
                var sqlAttivita = $"select count(*) from i_attivitadyn2modellit where idcomune={this.db.Specifics.QueryParameterName("idcomune")} and fk_d2mt_id={this.db.Specifics.QueryParameterName("idModello")}";

                int recCountModello = this.db.ExecuteScalar(sqlAttivita, 0, mp =>
                {
                    mp.AddParameter("idcomune", cls.Idcomune);
                    mp.AddParameter("idModello", modello.Id);
                });

                if (recCountModello > 0)
                {
                    // il modello è usato da almeno un'attivita, verifico che il campo sia valorizzato
                    sqlAttivita = $@"select 
                                                distinct I_attivita.id 
                                            from 
                                                I_Attivita 
                                                    inner join i_attivitadyn2modellit on 
                                                        i_attivitadyn2modellit.idcomune = i_attivita.idcomune and
                                                        i_attivitadyn2modellit.fk_ia_id = I_Attivita.id
                                                    inner join i_attivitadyn2dati on
                                                        i_attivitadyn2dati.idcomune = i_attivitadyn2modellit.idcomune AND
		                                                i_attivitadyn2dati.fk_ia_id = i_attivitadyn2modellit.fk_ia_id
                                            where 
                                                i_attivitadyn2modellit.idcomune = {this.db.Specifics.QueryParameterName("idcomune")} AND
	                                            i_attivitadyn2modellit.fk_d2mt_id = {this.db.Specifics.QueryParameterName("idmodello")} and
	                                            i_attivitadyn2dati.fk_d2c_id = {this.db.Specifics.QueryParameterName("idCampo")}";

                    var codiciAttivita = this.db.ExecuteReader(sqlAttivita, mp =>
                    {
                        mp.AddParameter("idComune", cls.Idcomune);
                        mp.AddParameter("idModello", cls.FkD2mtId);
                        mp.AddParameter("idCampo", cls.FkD2cId);
                    }, dr => dr.GetString("id"));

                    if (codiciAttivita.Any())
                    {
                        throw new Exception("Impossibile rimuovere il campo dal modello corrente in quanto è utilizzato dai seguenti codici attività: " + String.Join(", ", codiciAttivita.ToArray()));
                    }
                }
            }
        }

        /// <summary>
        /// Ricalcola la numerazione delle righe di un modello
        /// </summary>
        /// <param name="IdComune"></param>
        /// <param name="p"></param>
        public void RicalcolaNumerazioneRighe(string idComune, int idModello)
        {
            Dyn2ModelliD filtro = new Dyn2ModelliD();
            filtro.Idcomune = idComune;
            filtro.FkD2mtId = idModello;
            filtro.OrderBy = "posverticale asc";

            List<Dyn2ModelliD> campi = this.GetList(filtro);

            Dictionary<int, List<Dyn2ModelliD>> righe = new Dictionary<int, List<Dyn2ModelliD>>();
            List<int> keys = new List<int>();

            foreach (Dyn2ModelliD campo in campi)
            {
                int key = campo.Posverticale.GetValueOrDefault(int.MinValue);
                if (!righe.ContainsKey(key))
                {
                    righe[key] = new List<Dyn2ModelliD>();
                    keys.Add(key);
                }

                righe[key].Add(campo);
            }

            this.m_generaErroriModificaRiga = false;

            for (int i = 0; i < keys.Count; i++)
            {
                for (int j = 0; j < righe[keys[i]].Count; j++)
                {
                    righe[keys[i]][j].Posverticale = (i + 1) * 10;
                    //righe[keys[i]][j].Posorizzontale = (j + 1);
                    this.Update(righe[keys[i]][j]);
                }
            }
        }

        private void Validate(Dyn2ModelliD cls, AmbitoValidazione ambitoValidazione)
        {
            this.RequiredFieldValidate(cls, ambitoValidazione);



            // Se il campo è un campo dinamico verifico se il campo è già presente all'interno della scheda
            if (cls.FkD2cId.HasValue)
            {
                var whereCond = String.Format("idcomune='{0}' and fk_d2mt_id={1} and fk_d2c_id={2} and id <> {3}", cls.Idcomune, cls.FkD2mtId, cls.FkD2cId, cls.Id);
                int count = this.recordCount(cls.GetDBTableName(), "FK_D2C_ID", whereCond);

                if (count > 0)
                    throw new InvalidOperationException("Il modello contiene già il campo con id " + cls.FkD2cId + ", un campo dinamico può comparire una sola volta all'interno di un modello");
            }

            // Verifico che non si stia cercando di inserire un campo in una posizione già occupata
            if (this.m_generaErroriModificaRiga)
            {
                var whereCond = String.Format("idcomune='{0}' and fk_d2mt_id={1} and posverticale={2} and posorizzontale={3} and id <> {4}", cls.Idcomune, cls.FkD2mtId, cls.Posverticale, cls.Posorizzontale, cls.Id);
                int count = this.recordCount(cls.GetDBTableName(), "FK_D2C_ID", whereCond);

                if (count > 0)
                    throw new InvalidOperationException("Si sta cercando di inserire il campo in una posizione già occupata: riga=" + cls.Posverticale + " e colonna=" + cls.Posorizzontale);
            }


            Dyn2ModelliD clsconfronto = null;

            if (ambitoValidazione == AmbitoValidazione.Update)
                clsconfronto = this.GetById(cls.Idcomune, cls.Id.Value);

            // Se il campo fa parte di una riga multipla non deve essere possibile modificare 
            // - il numero riga
            // - l'id del campo dinamico
            // - il tipo di campo
            if (clsconfronto != null && cls.FlgMultiplo.GetValueOrDefault(0) == 1)
            {
                if (cls.Posverticale != clsconfronto.Posverticale && this.m_generaErroriModificaRiga)
                    throw new InvalidOperationException("Impossibile modificare la riga del campo in quanto fa parte di una riga multipla");

                if (cls.FkD2cId != clsconfronto.FkD2cId)
                    throw new InvalidOperationException("Impossibile modificare il campo dinamico in questa posizione in quanto fa parte di una riga multipla");
            }

            // Se è stato modificato l'id del campo dinamico collegato elimino tutti i dati di storico
            // if (clsconfronto != null && clsconfronto.FkD2cId != cls.FkD2cId)
            //    EliminaDatiStorico(cls.Idcomune, cls.FkD2mtId.Value);


            // Se sto aggiungendo il campo ad una riga che contiene campi con il flag flgmultiplo == 1
            // imposto anche in questo campo la molteplicità
            Dyn2ModelliD filtro = new Dyn2ModelliD();
            filtro.Idcomune = cls.Idcomune;
            filtro.FkD2mtId = cls.FkD2mtId.Value;
            filtro.Posverticale = cls.Posverticale.Value;

            List<Dyn2ModelliD> righe = this.GetList(filtro);

            if (righe.Count > 0 && righe[0].FlgMultiplo.GetValueOrDefault(0) == 1)
                cls.FlgMultiplo = 1;
        }

        #region Gestione delle righe multiple

        public void ImpostaRigaMultipla(string idComune, int idModello, int indiceRiga, int tipoRiga)
        {
            // Vincoli alla creazione di una riga multipla:
            //	1.	Nelle righe multiple non posso essere presenti controlli con una formula associata
            //	2.	Una volta impostato il flag di molteplicità in un controllo tutti i controlli della stessa riga diventeranno automaticamente multipli (se non ci sono condizioni che lo impediscono)
            //	3.	Una volta impostato il flag di molteplicità in un controllo non sarà più possibile modificare il campo POSVERTICALE di tutti i controlli contenuti nella stessa riga (rendere il campo di input readonly) finche il flag di molteplicità non sarà rimosso.
            //	4.	Se un modello contiene dei campi multipli non può essere impostato come modello multiplo (effettuare anche il controllo inverso in fase di impostazione del flag )

            bool closecnn = false;

            if (this.db.Connection.State == ConnectionState.Closed)
            {
                this.db.Connection.Open();
                closecnn = true;
            }

            try
            {
                Dyn2ModelliT modello = new Dyn2ModelliTMgr(this.db).GetById(idComune, idModello);

                Dyn2ModelliD filtro = new Dyn2ModelliD();
                filtro.Idcomune = idComune;
                filtro.FkD2mtId = idModello;
                filtro.Posverticale = indiceRiga;

                Dyn2ModelliDMgr mgr = new Dyn2ModelliDMgr(this.db);

                List<Dyn2ModelliD> righe = mgr.GetList(filtro);

                string sql = "";

                if (tipoRiga == 0)
                {
                    // Se la riga non è  multipla tolgo semplicemente il flag a tutti i campi che stanno nella stessa riga
                    try
                    {
                        this.db.BeginTransaction();

                        sql = "update dyn2_modellid set flg_multiplo = 0 where idcomune={0} and fk_d2mt_id = {1} and posverticale = {2}";

                        sql = string.Format(sql, this.db.Specifics.QueryParameterName("idComune"),
                                                    this.db.Specifics.QueryParameterName("idModello"),
                                                    this.db.Specifics.QueryParameterName("posVerticale"));

                        using (IDbCommand cmd = this.db.CreateCommand(sql))
                        {
                            cmd.Parameters.Add(this.db.CreateParameter("idComune", idComune));
                            cmd.Parameters.Add(this.db.CreateParameter("idModello", idModello));
                            cmd.Parameters.Add(this.db.CreateParameter("posVerticale", indiceRiga));

                            cmd.ExecuteNonQuery();
                        }

                        this.db.CommitTransaction();

                        return;
                    }
                    catch (Exception ex)
                    {
                        this.db.RollbackTransaction();

                        throw;
                    }
                }
                /*
				if (modello.Modellomultiplo.GetValueOrDefault(0) == 1)
					throw new InvalidOperationException("Impossibile assegnare una riga multipla al modello in quanto è già impostato come modello multiplo");
				*/
                // Inizio controlli sui vincoli
                //	1.	Nelle righe multiple non posso essere presenti controlli con una formula associata
                sql = @"SELECT 
							  count(*)
							FROM 
							  dyn2_campi_script,
							  dyn2_modellid
							WHERE
							  dyn2_campi_script.idcomune = dyn2_modellid.idcomune AND
							  dyn2_campi_script.fk_d2c_id = dyn2_modellid.fk_d2c_id and
							  dyn2_modellid.idcomune = {0} AND
							  dyn2_modellid.fk_d2mt_id = {1} AND
							  dyn2_modellid.posverticale = {2} AND
							  dyn2_campi_script.script IS NOT null";

                sql = string.Format(sql, this.db.Specifics.QueryParameterName("idComune"),
                                            this.db.Specifics.QueryParameterName("idModello"),
                                            this.db.Specifics.QueryParameterName("posVerticale"));

                using (IDbCommand cmd = this.db.CreateCommand(sql))
                {
                    cmd.Parameters.Add(this.db.CreateParameter("idComune", idComune));
                    cmd.Parameters.Add(this.db.CreateParameter("idModello", idModello));
                    cmd.Parameters.Add(this.db.CreateParameter("posVerticale", indiceRiga));

                    int cnt = Convert.ToInt32(cmd.ExecuteScalar());

                    if (cnt > 0)
                        throw new InvalidOperationException("Impossibile impostare la riga come multipla in quanto esiste almeno un campo contenente formule. Prima di rendere la riga multipla è necessario spostare le formule nel modello");
                }

                this.AggiornaFlagMultiplo(idComune, idModello, indiceRiga, tipoRiga);

                var righeSuccessive = this.GetRigheMultipleSuccessive(idComune, idModello, indiceRiga, tipoRiga);
                var righePrecedenti = this.GetRigheMultiplePrecedenti(idComune, idModello, indiceRiga, tipoRiga);

                foreach (var riga in righeSuccessive.Union(righePrecedenti))
                {
                    this.AggiornaFlagMultiplo(idComune, idModello, riga, tipoRiga);
                }
            }
            finally
            {
                if (closecnn)
                    this.db.Connection.Close();
            }
        }

        public void ImpostaSpezzaTabella(string idComune, int idModello, int id, bool spezzaTabella)
        {
            try
            {
                this.db.Connection.Open();

                Dyn2ModelliD filtro = new Dyn2ModelliD();
                filtro.Idcomune = idComune;
                filtro.FkD2mtId = idModello;

                var righe = this.GetList(filtro);
                var elementoCorrente = righe.Where(x => x.Id.GetValueOrDefault(-1) == id).FirstOrDefault();

                elementoCorrente.FlgSpezzaTabella = spezzaTabella ? 1 : 0;
                this.db.Update(elementoCorrente);

                if (elementoCorrente.FlgMultiplo.GetValueOrDefault(0) != 0)
                {
                    var righeSuccessive = righe.OrderBy(x => x.Posverticale.GetValueOrDefault(0))
                                                 .SkipWhile(x => x.Posverticale.GetValueOrDefault(0) < elementoCorrente.Posverticale.GetValueOrDefault(0))
                                                 .TakeWhile(x => x.FlgMultiplo.GetValueOrDefault(0) == elementoCorrente.FlgMultiplo.GetValueOrDefault(0));

                    var righePrecedenti = righe.OrderByDescending(x => x.Posverticale.GetValueOrDefault(0))
                                                 .SkipWhile(x => x.Posverticale.GetValueOrDefault(0) > elementoCorrente.Posverticale.GetValueOrDefault(0))
                                                 .TakeWhile(x => x.FlgMultiplo.GetValueOrDefault(0) == elementoCorrente.FlgMultiplo.GetValueOrDefault(0));

                    var righeDaModificare = righeSuccessive.Union(righePrecedenti)
                                                            .ToList();

                    righeDaModificare.ForEach(x =>
                    {
                        x.FlgSpezzaTabella = spezzaTabella ? 1 : 0;
                        this.db.Update(x);
                    });
                }
                else
                {
                    righe.Where(x => x.Posverticale == elementoCorrente.Posverticale)
                         .ToList()
                         .ForEach(x =>
                         {
                             x.FlgSpezzaTabella = spezzaTabella ? 1 : 0;
                             this.db.Update(x);
                         });
                }



                /*
                for (int i = 0; i < righe.Count(); i++)
                {
                    var riga = righe.ElementAt(i);

                    if (riga.Posverticale.GetValueOrDefault(0) < indiceRiga)
                    {
                        continue;
                    }

                    if (riga.Posverticale == indiceRiga)
                    {
                        multiplo = riga.FlgMultiplo.GetValueOrDefault(0);
                    }

                    if (riga.FlgMultiplo.GetValueOrDefault(0) != multiplo)
                    {
                        break;
                    }

                    riga.FlgSpezzaTabella = spezzaTabella ? 1 : 0;

                    Update(riga);
                }*/
                /*
                var sql = PreparaQueryParametrica("update dyn2_modellid set flg_spezza_tabella = {0} where idcomune={1} and fk_d2mt_id = {2} and posverticale = {3}", 
                                                    "flg_spezza_tabella", 
                                                    "idcomune",
                                                    "idModello",
                                                    "posVerticale");

                using (var cmd = db.CreateCommand(sql))
                {
                    
                    cmd.Parameters.Add(db.CreateParameter("flg_spezza_tabella", spezzaTabella ? 1 : 0));
                    cmd.Parameters.Add(db.CreateParameter("idComune", idComune));
                    cmd.Parameters.Add(db.CreateParameter("idModello", idModello));
                    cmd.Parameters.Add(db.CreateParameter("posVerticale", indiceRiga));

                    cmd.ExecuteNonQuery();
                }
                */
            }
            finally
            {
                this.db.Connection.Close();
            }
        }



        private void AggiornaFlagMultiplo(string idComune, int idModello, int posVerticale, int tipoRiga)
        {
            try
            {
                this.db.BeginTransaction();

                var sql = "update dyn2_modellid set flg_multiplo = {3} where idcomune={0} and fk_d2mt_id = {1} and posverticale = {2}";

                sql = string.Format(sql, this.db.Specifics.QueryParameterName("idComune"),
                                            this.db.Specifics.QueryParameterName("idModello"),
                                            this.db.Specifics.QueryParameterName("posVerticale"),
                                            this.db.Specifics.QueryParameterName("tipoRiga"));

                using (IDbCommand cmd = this.db.CreateCommand(sql))
                {
                    cmd.Parameters.Add(this.db.CreateParameter("idComune", idComune));
                    cmd.Parameters.Add(this.db.CreateParameter("idModello", idModello));
                    cmd.Parameters.Add(this.db.CreateParameter("posVerticale", posVerticale));
                    cmd.Parameters.Add(this.db.CreateParameter("tipoRiga", tipoRiga));

                    cmd.ExecuteNonQuery();
                }

                this.db.CommitTransaction();

                return;
            }
            catch (Exception ex)
            {
                this.db.RollbackTransaction();

                throw;
            }
        }

        private IEnumerable<int> GetRigheMultipleSuccessive(string idComune, int idModello, int posVerticale, int tipoRigaCorrente)
        {
            if (tipoRigaCorrente == 0)
            {
                return Enumerable.Empty<int>();
            }

            var sql = @"SELECT posVerticale, flg_multiplo FROM dyn2_modellid 
                        WHERE idcomune={0} AND fk_d2mt_id={1} AND posVerticale > {2}
                        ORDER BY posverticale asc";

            sql = string.Format(sql, this.db.Specifics.QueryParameterName("idComune"),
                                            this.db.Specifics.QueryParameterName("idModello"),
                                            this.db.Specifics.QueryParameterName("posVerticale"));

            using (IDbCommand cmd = this.db.CreateCommand(sql))
            {
                cmd.Parameters.Add(this.db.CreateParameter("idComune", idComune));
                cmd.Parameters.Add(this.db.CreateParameter("idModello", idModello));
                cmd.Parameters.Add(this.db.CreateParameter("posVerticale", posVerticale));

                using (var dr = cmd.ExecuteReader())
                {
                    var rVal = new List<int>();

                    while (dr.Read())
                    {
                        if (dr.GetInt("flg_multiplo").GetValueOrDefault(0) == 0)
                        {
                            break;
                        }

                        rVal.Add(dr.GetInt("posVerticale").GetValueOrDefault(-1));
                    }

                    return rVal;
                }
            }
        }

        private IEnumerable<int> GetRigheMultiplePrecedenti(string idComune, int idModello, int posVerticale, int tipoRigaCorrente)
        {
            if (tipoRigaCorrente == 0)
            {
                return Enumerable.Empty<int>();
            }

            var sql = @"SELECT posVerticale, flg_multiplo FROM dyn2_modellid 
                        WHERE idcomune={0} AND fk_d2mt_id={1} AND posVerticale < {2}
                        ORDER BY posverticale desc";

            sql = string.Format(sql, this.db.Specifics.QueryParameterName("idComune"),
                                            this.db.Specifics.QueryParameterName("idModello"),
                                            this.db.Specifics.QueryParameterName("posVerticale"));

            using (IDbCommand cmd = this.db.CreateCommand(sql))
            {
                cmd.Parameters.Add(this.db.CreateParameter("idComune", idComune));
                cmd.Parameters.Add(this.db.CreateParameter("idModello", idModello));
                cmd.Parameters.Add(this.db.CreateParameter("posVerticale", posVerticale));

                using (var dr = cmd.ExecuteReader())
                {
                    var rVal = new List<int>();

                    while (dr.Read())
                    {
                        var tipoRiga = dr["flg_multiplo"];

                        if (dr.GetInt("flg_multiplo").GetValueOrDefault(0) == 0)
                        {
                            break;
                        }

                        rVal.Add(dr.GetInt("posVerticale").GetValueOrDefault(-1));
                    }

                    return rVal;
                }
            }
        }

        #endregion

        #region IDyn2DettagliModelloManager Members
        public List<Dyn2ModelliD> GetListImpl(string idComune, int idModello)
        {
            var filtro = new Dyn2ModelliD
            {
                Idcomune = idComune,
                FkD2mtId = idModello,
                OrderBy = "posverticale asc, posorizzontale asc"
            };

            //filtro.OthersWhereClause.Add("FK_D2C_ID is not null");

            return this.GetList(filtro);
        }

        public List<IDyn2DettagliModello> GetList(string idComune, int idModello)
        {
            var filtro = new Dyn2ModelliD
            {
                Idcomune = idComune,
                FkD2mtId = idModello,
                OrderBy = "posverticale asc, posorizzontale asc"
            };

            //filtro.OthersWhereClause.Add("FK_D2C_ID is not null");

            var list = this.GetList(filtro);

            var rVal = new List<IDyn2DettagliModello>(list.Count);

            list.ForEach(x => rVal.Add(x));

            return rVal;
        }

        #endregion
    }
}
