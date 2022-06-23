using Init.SIGePro.Data;
using Init.SIGePro.Exceptions;
using Init.SIGePro.Validator;
using PersonalLib2.Data;
using PersonalLib2.Sql.Collections;
using System;
using System.Collections.Generic;
using System.Data;

namespace Init.SIGePro.Manager
{

    public class Mercati_DMgr : BaseManager
    {
        public Mercati_DMgr(DataBase dataBase) : base(dataBase) { }

        public Mercati_D GetByClass(Mercati_D cls)
        {
            var mydc = db.GetClassList(cls, true, false);
            if (mydc.Count != 0)
                return (mydc[0]) as Mercati_D;

            return null;
        }

        public Mercati_D GetById(String idComune, int idPosteggio)
        {
            Mercati_D retVal = new Mercati_D();
            retVal.IdPosteggio = idPosteggio;
            retVal.IdComune = idComune;

            var mydc = db.GetClassList(retVal, true, false);
            if (mydc.Count != 0)
                return (mydc[0]) as Mercati_D;

            return null;
        }

        public List<Mercati_D> GetList(Mercati_D p_class)
        {
            return this.GetList(p_class, null);
        }

        public List<Mercati_D> GetList(Mercati_D p_class, Mercati_D p_cmpClass)
        {
            return db.GetClassList(p_class, p_cmpClass, false, true).ToList<Mercati_D>();
        }

        public void Delete(Mercati_D p_class)
        {
            db.Delete(p_class);
        }

        public Mercati_D Insert(Mercati_D p_class)
        {
            p_class = DataIntegrations(p_class);

            Validate(p_class, AmbitoValidazione.Insert);

            db.Insert(p_class);

            p_class = ChildDataIntegrations(p_class);

            ChildInsert(p_class);

            return p_class;
        }

        private Mercati_D DataIntegrations(Mercati_D p_class)
        {
            Mercati_D retVal = (Mercati_D)p_class.Clone();

            if (String.IsNullOrEmpty(retVal.IdComune))
                throw new RequiredFieldException("MERCATI.IDCOMUNE obbligatorio");

            if (retVal.FkCodiceMercato.GetValueOrDefault(int.MinValue) == int.MinValue)
                throw new RequiredFieldException("MERCATI_D.FKCODICEMERCATO obbligatorio");

            if (String.IsNullOrEmpty(retVal.CodicePosteggio))
                throw new RequiredFieldException("MERCATI_D.CODICEPOSTEGGIO obbligatorio");

            if (String.IsNullOrEmpty(retVal.Disabilitato))
                retVal.Disabilitato = "0";

            return retVal;
        }

        private Mercati_D ChildDataIntegrations(Mercati_D p_class)
        {
            Mercati_D retVal = (Mercati_D)p_class.Clone();

            #region ii. Integrazione delle classi figlio con i dati della classe padre

            #region 1.	Mercati_DAttivitaIstat

            foreach (Mercati_DAttivitaIstat p_att in retVal.AttivitaIstat)
            {
                if (String.IsNullOrEmpty(p_att.IDCOMUNE))
                    p_att.IDCOMUNE = retVal.IdComune;
                else if (p_att.IDCOMUNE.ToUpper() != retVal.IdComune.ToUpper())
                    throw new Exceptions.IncongruentDataException("MERCATI_DATTIVITAISTAT.IDCOMUNE diverso da MERCATI_D.IDCOMUNE");


                if (p_att.FKCODICEMERCATO.GetValueOrDefault(int.MinValue) > int.MinValue)
                    p_att.FKCODICEMERCATO = retVal.FkCodiceMercato;

                if (p_att.FKIDPOSTEGGIO.GetValueOrDefault(int.MinValue) == int.MinValue)
                    p_att.FKIDPOSTEGGIO = retVal.IdPosteggio;

            }

            #endregion

            #region 2.	Mercati_DConti

            foreach (Mercati_DConti conto in retVal.Conti)
            {
                if (string.IsNullOrEmpty(retVal.IdComune))
                    conto.Idcomune = retVal.IdComune;
                else if (conto.Idcomune.ToUpper() != retVal.IdComune.ToUpper())
                    throw new Exceptions.IncongruentDataException("MERCATI_D_CONTI.IDCOMUNE diverso da MERCATI_D.IDCOMUNE");

                if (conto.FkMdId.GetValueOrDefault(int.MinValue) == int.MinValue)
                    conto.FkMdId = retVal.IdPosteggio;
                else if (conto.FkMdId != retVal.IdPosteggio)
                    throw new Exceptions.IncongruentDataException("MERCATI_D_CONTI.FK_MDID diverso da MERCATI_D.IDPOSTEGGIO");

            }

            #endregion


            #endregion

            return retVal;
        }

        private void ChildInsert(Mercati_D p_class)
        {
            for (int i = 0; i < p_class.AttivitaIstat.Count; i++)
            {
                Mercati_DAttivitaIstatMgr pManager = new Mercati_DAttivitaIstatMgr(this.db);
                pManager.Insert(p_class.AttivitaIstat[i]);
            }

            for (int i = 0; i < p_class.Conti.Count; i++)
            {
                Mercati_DContiMgr pManager = new Mercati_DContiMgr(this.db);
                pManager.Insert(p_class.Conti[i]);
            }
        }

        private void Validate(Mercati_D p_class, AmbitoValidazione ambitoValidazione)
        {
            RequiredFieldValidate(p_class, ambitoValidazione);

            ForeignValidate(p_class);
        }

        private void ForeignValidate(Mercati_D p_class)
        {
            #region MERCATI_D.FKCODICEMERCATO
            if (p_class.FkCodiceMercato.GetValueOrDefault(int.MinValue) > int.MinValue)
            {
                if (this.recordCount("MERCATI", "CODICEMERCATO", "WHERE CODICEMERCATO = " + p_class.FkCodiceMercato.ToString() + " AND IDCOMUNE = '" + p_class.IdComune + "'") == 0)
                {
                    throw (new RecordNotfoundException("MERCATI_D.FKCODICEMERCATO (" + p_class.FkCodiceMercato.ToString() + ") non trovato nella tabella MERCATI"));
                }
            }
            #endregion

            #region MERCATI_D.FKCODICETIPOSPAZIO
            if (!String.IsNullOrEmpty(p_class.FkCodiceTipoSpazio))
            {
                if (this.recordCount("POSTEGGITIPOSPAZIO", "CODICE", "WHERE CODICE = " + p_class.FkCodiceTipoSpazio + " AND IDCOMUNE = '" + p_class.IdComune + "'") == 0)
                {
                    throw (new RecordNotfoundException("MERCATI_D.FKCODICETIPOSPAZIO (" + p_class.FkCodiceTipoSpazio + ") non trovato nella tabella POSTEGGITIPOSPAZIO"));
                }
            }
            #endregion
        }

        public List<Occupanti> GetOccupanti(string idComune, int idMercato, int idPosteggio, int idUso)
        {
            List<Occupanti> retVal = new List<Occupanti>();

            string cmdText = "select " +
                                 "istanze.codicerichiedente, richiedente.nominativo || ' ' || richiedente.nome as richiedente, " +
                                 "istanze.codicetitolarelegale, azienda.nominativo || ' ' || azienda.nome as azienda, " +
                                 "mercati_uso.id,  mercati_uso.descrizione " +
                             "from " +
                                "istanze, istanzeconcessioni, concessioni, mercati_uso, anagrafe richiedente, anagrafe azienda " +
                             "where " +
                                "richiedente.idcomune = istanze.idcomune and " +
                                "richiedente.codiceanagrafe = istanze.codicerichiedente and " +
                                "azienda.idcomune(+) = istanze.idcomune and " +
                                "azienda.codiceanagrafe(+) = istanze.codicetitolarelegale and " +
                                "mercati_uso.idcomune = concessioni.idcomune and " +
                                "mercati_uso.id = concessioni.fk_idmercatiuso and " +
                                "istanzeconcessioni.idcomune = istanze.idcomune and " +
                                "istanzeconcessioni.fkcodiceistanza = istanze.codiceistanza and " +
                                "istanzeconcessioni.fkcodicecausalestorico is null and " +
                                "concessioni.idcomune = istanzeconcessioni.idcomune and " +
                                "concessioni.id = istanzeconcessioni.fkidconcessione and " +
                                "concessioni.idcomune = '" + idComune + "' and " +
                                "concessioni.fk_codicemercato = " + idMercato.ToString() + " and " +
                                "concessioni.fk_idposteggio = " + idPosteggio.ToString() + " and " +
                                "concessioni.attiva = 1";
            if (idUso > int.MinValue)
            {
                cmdText += " and concessioni.fk_idmercatiuso= " + idUso.ToString();
            }

            db.Connection.Open();

            using (IDataReader rd = (db.CreateCommand(cmdText).ExecuteReader()))
            {
                while (rd.Read())
                {
                    Occupanti o = new Occupanti();
                    if (rd["codicetitolarelegale"] == DBNull.Value)
                    {
                        o.CodiceAnagrafe = Convert.ToInt32(rd["codicerichiedente"].ToString());
                        o.Nominativo = rd["richiedente"].ToString();
                    }
                    else
                    {
                        o.CodiceAnagrafe = Convert.ToInt32(rd["codicetitolarelegale"].ToString());
                        o.Nominativo = rd["azienda"].ToString();
                    }

                    o.CodiceUso = Convert.ToInt32(rd["id"].ToString());
                    o.Uso = rd["descrizione"].ToString();

                    retVal.Add(o);
                }
            }

            db.Connection.Close();

            return retVal;
        }
    }

    public class Occupanti
    {
        int? m_codiceanagrafe = null;
        public int? CodiceAnagrafe
        {
            get { return m_codiceanagrafe; }
            set { m_codiceanagrafe = value; }
        }

        string m_nominativo = null;
        public string Nominativo
        {
            get { return m_nominativo; }
            set { m_nominativo = value; }
        }

        int? m_codiceuso = null;
        public int? CodiceUso
        {
            get { return m_codiceuso; }
            set { m_codiceuso = value; }
        }

        string m_uso = null;
        public string Uso
        {
            get { return m_uso; }
            set { m_uso = value; }
        }

    }
}