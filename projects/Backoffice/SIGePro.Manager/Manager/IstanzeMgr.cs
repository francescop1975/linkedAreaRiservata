using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Init.SIGePro.Data;
using Init.SIGePro.Exceptions.Istanze;
using Init.SIGePro.Manager.Logic.DatiDinamici;
using Init.SIGePro.Manager.Utils;
using Init.SIGePro.Validator;
using Init.SIGePro.Verticalizzazioni;
using Init.Utils;
using PersonalLib2.Data;
using PersonalLib2.Sql;
using PersonalLib2.Sql.Collections;
using Init.SIGePro.Manager.Manager;
using Init.SIGePro.Manager.Configuration;
using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneMercati;
using Init.SIGePro.Manager.Stc;
using Init.SIGePro.Manager.Logic.SubvisuraPratica;

namespace Init.SIGePro.Manager
{
	public class DettaglioPermessiIstanza
	{
		public bool AccessoConsentito{get;set;}
		public bool SolaLettura{get;set;}
		public bool GestMovimenti{get;set;}
		public bool DisabilitaGestMovimentiAmminNonInterna{get;set;}

		public DettaglioPermessiIstanza ()
		{
			this.AccessoConsentito = false;
			this.SolaLettura = false;
			this.GestMovimenti = false;
			this.DisabilitaGestMovimentiAmminNonInterna = false;
		}
	}


	public class IstanzeMgr : BaseManager//, IIstanzeManager
    {
        public class RiferimentiIstanzaDaUuid
        {
            public readonly int CodiceIstanza;
            public readonly string IdComune;

            public RiferimentiIstanzaDaUuid(string idComune, int codiceIstanza)
            {
                this.IdComune = idComune;
                this.CodiceIstanza = codiceIstanza;
            }
        }

        #region parametri per la configurazione di alcune procedure automatiche all'interno del manager
        //per default teniamo conto della configurazione di sigepro dal web.
        bool _insertanagrafe = false;
        bool _updateanagrafe = false;
        bool _autogeneratedocumentiistanza = true;
        bool _autogenerateistanzeoneri = true;
        bool _autogeneratepermistanze = true;
        bool m_escludiControlliSuAnagraficheDisabilitate = false;
        bool m_ForzaInserimentoAnagrafe = false;
        bool _ricercasolocfpiva = false;
        bool _escludiverificaincongruenze = false;

        #region propriet? per la protocollazione

        /// <summary>
        /// Se impostato a true effettua la chiamata al manager di protocollazione
        /// </summary>
        bool _protocollaistanza = true;
        public bool Protocollaistanza
        {
            get { return _protocollaistanza; }
            set { _protocollaistanza = value; }
        }

        /// <summary>
        /// Definisce da dove arriva la richiesta di protocollazione
        /// </summary>
        int _protocollaistanzaSource = 2; //corrisponde a Source.ON_LINE di ProtocolloMgr
        public int ProtocollaistanzaSource
        {
            get { return _protocollaistanzaSource; }
            set { _protocollaistanzaSource = value; }
        }
        #endregion 

        #region propriet? per la fascicolazione

        bool _fascicolaistanza = true;
        int _fascicolaistanzaSource = 2; //Corrisponde a SourceFascicolazione.ON_LINE della classe ProtocolloMgr dell'assembly Sigepro.Protocollo

        /// <summary>
        /// Se impostato a true effettua la chiamata al manager di protocollazione
        /// </summary>
        public bool Fascicolaistanza
        {
            get { return _fascicolaistanza; }
            set { _fascicolaistanza = value; }
        }

        /// <summary>
        /// Definisce da dove arriva la richiesta di protocollazione
        /// </summary>
        public int FascicolazioneistanzaSource
        {
            get { return _fascicolaistanzaSource; }
            set { _fascicolaistanzaSource = value; }
        }
        #endregion 

        private bool m_autoGenerateModelliDinamici;

        public bool AutoGenerateModelliDinamici
        {
            get { return m_autoGenerateModelliDinamici; }
            set { m_autoGenerateModelliDinamici = value; }
        }

        /// <summary>
        /// Se impostato a true non effettua verifiche dei dati sulle anagrafiche disabilitate nei metodi Insert e Extract
        /// </summary>
        public bool EscludiControlliSuAnagraficheDisabilitate
        {
            get { return m_escludiControlliSuAnagraficheDisabilitate; }
            set { m_escludiControlliSuAnagraficheDisabilitate = value; }
        }


        /// <summary>
        /// Se True, inserisce le anagrafiche (tabella ANAGRAFE) non presenti in SIGePro
        /// </summary>
        public bool InsertAnagrafe
        {
            get { return _insertanagrafe; }
            set { _insertanagrafe = value; }
        }

        /// <summary>
        /// Se True, aggiorna le anagrafiche (tabella ANAGRAFE) quando gi? presenti in SIGePro.
        /// </summary>
        public bool UpdateAnagrafe
        {
            get { return _updateanagrafe; }
            set { _updateanagrafe = value; }
        }

        public bool AutoGenerateDocumentiIstanza
        {
            get { return _autogeneratedocumentiistanza; }
            set { _autogeneratedocumentiistanza = value; }
        }

        public bool AutoGenerateIstanzeOneri
        {
            get { return _autogenerateistanzeoneri; }
            set { _autogenerateistanzeoneri = value; }
        }

        public bool AutoGeneratePermIstanze
        {
            get { return _autogeneratepermistanze; }
            set { _autogeneratepermistanze = value; }
        }

        public bool ForzaInserimentoAnagrafe
        {
            get { return m_ForzaInserimentoAnagrafe; }
            set { m_ForzaInserimentoAnagrafe = value; }
        }

        /// <summary>
        /// Se True, effettua la ricerca delle anagrafiche solamente per codice fiscale/partita iva. Per default ? False
        /// </summary>
        public bool RicercaSoloCF_PIVA
        {
            get { return _ricercasolocfpiva; }
            set { _ricercasolocfpiva = value; }
        }

        /// <summary>
        /// Se True, esclude la verifica dei dati una volta trova un'anagrafica. Per default ? false.
        /// </summary>
        public bool EscludiVerificaIncongruenze
        {
            get { return _escludiverificaincongruenze; }
            set { _escludiverificaincongruenze = value; }
        }

        #endregion

        public IstanzeMgr(DataBase dataBase) : base(dataBase) { }

		public RiferimentiIstanzaDaUuid GetCodiceIstanzaDaUuid(string uuid)
        {
            var sql = $"select idcomune, codiceistanza from istanze where uuid = {db.Specifics.QueryParameterName("uuid")}";

            return this.db.ExecuteReader(sql, mp =>
            {
                mp.AddParameter("uuid", uuid);
            },
            dr => new RiferimentiIstanzaDaUuid(dr.GetString(0), dr.GetInt32(1)))
            .FirstOrDefault();
        }

        public string GetUUIDdaCodiceIstanza(string idComune, int codiceIstanza)
        {
            var sql = $"select uuid from istanze where idcomune={db.QueryParameter("idComune")} and codiceistanza={db.QueryParameter("codiceIstanza")}";

            return db.ExecuteScalar(sql, String.Empty, 
                mp => mp.Add("idComune", idComune)
                        .Add("codiceIstanza", codiceIstanza));
        }

		public Istanze GetById(String codiceIstanza, String idComune, useForeignEnum useForeign)
		{
			Istanze retVal = new Istanze();
			retVal.CODICEISTANZA = codiceIstanza;
			retVal.IDCOMUNE = idComune;
			retVal.UseForeign = useForeign;

            var closeCnn = false;

            if (db.Connection.State != ConnectionState.Open)
            {
                db.Connection.Open();
                closeCnn = true;
            }

            try
            {
                var mydc = db.GetClassList(retVal, true, false);

                if (mydc.Count == 0)
                {
                    return null;
                }

                var istanza = (mydc[0]) as Istanze;

                if (String.IsNullOrEmpty(istanza.CODICEPRATICATEL))
                {
                    istanza.CODICEPRATICATEL = EstraiCodicePraticaTelematica(db, idComune, codiceIstanza);
                }

                return istanza;
            }
            finally
            {
                if (closeCnn)
                {
                    db.Connection.Close();
                    closeCnn = false;
                }
            }
		}

//        private string GetIdSubvisura(VerticalizzazioneStc verticalizzazione, string idComune, int idMovimento)
//        {
//            var sql = PreparaQueryParametrica(@"SELECT 
//  istanze.codiceistanza AS idpratica,
//  amministrazioni.stc_idnodo AS idnodo,
//  amministrazioni.stc_idente AS idente,
//  amministrazioni.stc_idsportello AS idsportello,
//  movimenti.codicemovimento AS idprocedimento
//FROM     
//  movimenti 
                           
//    INNER JOIN amministrazioni ON
//      movimenti.idcomune = amministrazioni.idcomune AND
//      movimenti.codiceamministrazione = amministrazioni.codiceamministrazione

//    INNER JOIN istanze ON 
//      istanze.idcomune = movimenti.idcomune AND
//      istanze.codiceistanza = movimenti.codiceistanza

//    INNER JOIN softwareattivi ON
//      softwareattivi.idcomune = amministrazioni.idcomune AND 
//      softwareattivi.fk_software = amministrazioni.stc_idsportello

//WHERE
//    movimenti.idcomune = {0} AND
//    movimenti.codicemovimento = {1} AND
//    movimenti.inviato_con_stc <> 0 and
//    amministrazioni.stc_idnodo = {2} AND
//    softwareattivi.flag_subvisura = 1", "idcomune", "codicemovimento", "idNodoInterno");


//            var riferimentiPratica = this.db.ExecuteReader(sql, mp =>
//            {
//                mp.Add("idcomune", idComune);
//                mp.Add("codicemovimento", idMovimento);
//                mp.Add("idNodoInterno", verticalizzazione.NlaIdnodo);
//            },
//            dr =>
//            {
//                return new
//                {
//                    IdPratica = dr.GetInt("idpratica"),
//                    IdNodo = dr.GetInt("IdNodo"),
//                    IdEnte = dr.GetString("IdEnte"),
//                    IdSportello = dr.GetString("IdSportello")                
//                };
//            }).FirstOrDefault(); 

//            if (riferimentiPratica == null)
//            {
//                return string.Empty;
//            }

//            var stcClient = new StcProxy(verticalizzazione, )
//        }

        private string EstraiCodicePraticaTelematica(DataBase db, string idComune, string codiceIstanza)
        {
            var sql = PreparaQueryParametrica("SELECT id_domandamitt FROM domandestc WHERE idcomune={0} AND codiceistanza = {1}", "idcomune", "codiceistanza");

            using (var cmd = db.CreateCommand(sql))
            {
                cmd.Parameters.Add(db.CreateParameter("idcomune", idComune));
                cmd.Parameters.Add(db.CreateParameter("codiceistanza", codiceIstanza));

                var result = cmd.ExecuteScalar();

                return result == null ? String.Empty : result.ToString();
            }
            
        }

        public Istanze GetById(string idComune, int codiceIstanza)
        {
            return GetById(codiceIstanza.ToString(), idComune, useForeignEnum.No);
        }

        public Istanze GetById(string idComune, int codiceIstanza, useForeignEnum useForeign)
        {
            return GetById(codiceIstanza.ToString(), idComune, useForeign);
        }
        
        public Istanze GetByClass(Istanze pClass)
        {
            var mydc = db.GetClassList(pClass, true, false);

            if (mydc.Count != 0)
                return (mydc[0]) as Istanze;

            return null;
        }

        public List<Istanze> GetList(Istanze p_class)
        {
            return this.GetList(p_class, null);
        }

        public List<Istanze> GetList(Istanze p_class, Istanze p_cmpClass)
        {
            return db.GetClassList(p_class, p_cmpClass, false, false).ToList<Istanze>();
        }

		public int GetPresenze(string token, int codiceistanza, string autoriznumero, string autorizdata, string autorizcomune, int fkidregistro, string catMerc, bool inserisciAutSeNonTrovata)
		{
			if (codiceistanza == int.MinValue)
				throw new Exception("Impossibile utilizzare il metodo GetPresenze senza aver impostato il codiceistanza");

			return new MercatiService(token).GetPresenze(codiceistanza, autoriznumero, autorizdata, autorizcomune, fkidregistro, catMerc, inserisciAutSeNonTrovata);

		}

		public int GetPresenzeTitolare(string token, Int32 codiceistanza, string autoriznumero, string autorizdata, string autorizcomune, int fkidregistro, string catMerc, bool inserisciAutSeNonTrovata)
		{
			if (codiceistanza == int.MinValue)
				throw new Exception("Impossibile utilizzare il metodo GetPresenzeTitolare senza aver impostato il codiceistanza");

			return new MercatiService(token).GetPresenzeTitolare(codiceistanza, autoriznumero, autorizdata, autorizcomune, fkidregistro, catMerc, inserisciAutSeNonTrovata);

		}

		/// <summary>
		/// Verifica i permessi di un operatore rispetto ad una deeterminata istanza
		/// </summary>
		/// <param name="idComune"></param>
		/// <param name="codiceResponsabile"></param>
		/// <param name="codiceIstanza"></param>
		/// <returns></returns>
		public DettaglioPermessiIstanza PermessiIstanza(string idComune, int codiceResponsabile, int codiceIstanza)
		{
			DettaglioPermessiIstanza dpi = PermessiIstanzaInternal(idComune, codiceResponsabile, codiceIstanza);

			if (!dpi.AccessoConsentito || dpi.SolaLettura)
				return dpi;

			bool solaLettura = new ResponsabiliMgr(db).GetById(idComune, codiceResponsabile).READONLY == "1";

			if (solaLettura)
				dpi.SolaLettura = true;

			return dpi;
		}

		private DettaglioPermessiIstanza PermessiIstanzaInternal(string idComune, int codiceResponsabile, int codiceIstanza)
		{
			Istanze istanza = GetById(idComune, codiceIstanza);
			DettaglioPermessiIstanza rVal = new DettaglioPermessiIstanza();

			bool amministratoreSoftware = new ResponsabiliMgr(db).VerificaSeAmministratoreSoftware(idComune, codiceResponsabile, istanza.SOFTWARE);

			if (amministratoreSoftware)
			{
				rVal.AccessoConsentito = true;
				rVal.SolaLettura = false;
				rVal.GestMovimenti = true;
				rVal.DisabilitaGestMovimentiAmminNonInterna = false;

				return rVal;
			}

			// Verifico sel'operatore ha un permesso impostato nell'istanza
			if (new PermIstanzeMgr(db).VerificaPermessiUtente(idComune, codiceResponsabile, codiceIstanza, 1))
			{
				rVal.AccessoConsentito = true;
				rVal.SolaLettura = false;
				rVal.GestMovimenti = true;
				rVal.DisabilitaGestMovimentiAmminNonInterna = false;

				return rVal;
			}

			//	Se l'operatore (responsabile) non ha diritto di vedere l'istanza
			//	perch? non ? amministratore, non ? amministratore software e non
			//	? specificato nella tabella PERMISTANZE per l'istanza specificata
			//	allora potrebbe avere accesso all'istanza perch? appartiene ad un ruolo
			//	che ha diritto di vedere l'istanza
			bool closeCnn = false;

			try
			{
				if (db.Connection.State == ConnectionState.Closed)
				{
					db.Connection.Open();
					closeCnn = true;
				}

				string sql = @"Select  
									" + db.Specifics.NvlFunction("RUOLI.READONLY", 0) + @" as READONLY, 
									" + db.Specifics.NvlFunction("FLAG_GESTMOVIMENTI", 0) + @" as FLAG_GESTMOVIMENTI,
									" + db.Specifics.NvlFunction("FLAG_DISGESTMOVAMM", 0) + @" as FLAG_DISGESTMOVAMM 
								From  
									ISTANZERUOLI , RESPONSABILIRUOLI, RUOLI  
								where  
									ISTANZERUOLI.IDCOMUNE=RESPONSABILIRUOLI.IDCOMUNE and  
									ISTANZERUOLI.IDRUOLO=RESPONSABILIRUOLI.IDRUOLO and  
									RUOLI.IDCOMUNE=ISTANZERUOLI.IDCOMUNE and  
									RUOLI.ID=ISTANZERUOLI.IDRUOLO and  
									ISTANZERUOLI.IDCOMUNE = {0} and  
									ISTANZERUOLI.CODICEISTANZA= {1} and  
									RESPONSABILIRUOLI.CODICERESPONSABILE = {2}
								Order By  
									READONLY,FLAG_GESTMOVIMENTI DESC, FLAG_DISGESTMOVAMM";

				sql = String.Format(sql, db.Specifics.QueryParameterName("idComune"),
											db.Specifics.QueryParameterName("codiceIstanza"),
											db.Specifics.QueryParameterName("codiceResponsabile"));

				using (IDbCommand cmd = db.CreateCommand(sql))
				{
					cmd.Parameters.Add(db.CreateParameter("idComune", idComune));
					cmd.Parameters.Add(db.CreateParameter("codiceIstanza", codiceIstanza));
					cmd.Parameters.Add(db.CreateParameter("codiceResponsabile", codiceResponsabile));

					using (IDataReader dr = cmd.ExecuteReader())
					{
						if (!dr.Read())
							return rVal;

						rVal.AccessoConsentito = true;
						rVal.GestMovimenti = true;
						rVal.DisabilitaGestMovimentiAmminNonInterna = false;

						if (dr["READONLY"].ToString() == "1")
						{
							rVal.SolaLettura = true;

							if (dr["FLAG_GESTMOVIMENTI"].ToString() == "0")
							{
								rVal.GestMovimenti = false;
							}
							else
							{
								if (dr["FLAG_DISGESTMOVAMM"].ToString() == "1")
									rVal.DisabilitaGestMovimentiAmminNonInterna = true;
							}
						}

						return rVal;
					}
				}
			}
			finally
			{
				if (closeCnn)
					db.Connection.Close();
			}
		}

        public void UpdateDatiProtocollo(string idProtocollo, string numeroProtocollo, DateTime dataProtocollo, string idComune, int codiceIstanza)
        {
            string sql = "update istanze set fkidprotocollo = {0}, numeroprotocollo = {1}, dataprotocollo = {2} where idcomune = {3} and codiceistanza = {4}";

            bool closeCnn = false;

            if (db.Connection.State == ConnectionState.Closed)
            {
                closeCnn = true;
                db.Connection.Open();
            }

            try
            {
                sql = PreparaQueryParametrica(sql, "fkidProtocollo", "numeroProtocollo", "dataProtocollo", "idComune", "codiceIstanza");

                using (IDbCommand cmd = db.CreateCommand(sql))
                {
                    cmd.Parameters.Add(db.CreateParameter("fkidProtocollo", String.IsNullOrEmpty(idProtocollo) ? DBNull.Value : (object)idProtocollo));
                    cmd.Parameters.Add(db.CreateParameter("numeroProtocollo", numeroProtocollo));
                    cmd.Parameters.Add(db.CreateParameter("dataProtocollo", dataProtocollo));
                    cmd.Parameters.Add(db.CreateParameter("idComune", idComune));
                    cmd.Parameters.Add(db.CreateParameter("codiceIstanza", codiceIstanza));

                    cmd.ExecuteNonQuery();
                }

            }
            finally
            {
                if (closeCnn)
                    db.Connection.Close();
            }
        }

        public string GetTestoTipoFascicoloFromIstanza(string idComune, int codiceIstanza)
        {
            try
            {
                var sql = $@"select 
								codiceinterventoproc,
                                codicecomune,
                                software
							from 
								istanze 
							where 
								idcomune = {this.db.Specifics.QueryParameterName("idcomune")} and 
								codiceistanza = {this.db.Specifics.QueryParameterName("codiceistanza")}";

                var istanza = this.db.ExecuteReader(sql, mapParameters =>
                {
                    mapParameters.AddParameter("idcomune", idComune);
                    mapParameters.AddParameter("codiceistanza", codiceIstanza);
                }, dr => new Istanze
                {
                    CODICEINTERVENTOPROC = dr.GetString("codiceinterventoproc"),
                    CODICECOMUNE = dr.GetString("codicecomune"),
                    SOFTWARE = dr.GetString("software")

                }).FirstOrDefault();

                return new AlberoProcMgr(this.db).GetTestoTipoFascicoloFromAlberoProcProtocollo(Convert.ToInt32(istanza.CODICEINTERVENTOPROC), idComune, istanza.SOFTWARE, istanza.CODICECOMUNE);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetNumeroFascicoloFromIstanza(string idComune, int codiceIstanza)
        {
            try
            {
                var sql = $@"select 
								codiceinterventoproc,
                                codicecomune,
                                software
							from 
								istanze 
							where 
								idcomune = {this.db.Specifics.QueryParameterName("idcomune")} and 
								codiceistanza = {this.db.Specifics.QueryParameterName("codiceistanza")}";

                var istanza = this.db.ExecuteReader(sql, mapParameters =>
                {
                    mapParameters.AddParameter("idcomune", idComune);
                    mapParameters.AddParameter("codiceistanza", codiceIstanza);
                }, dr => new Istanze
                {
                    CODICEINTERVENTOPROC = dr.GetString("codiceinterventoproc"),
                    CODICECOMUNE = dr.GetString("codicecomune"),
                    SOFTWARE = dr.GetString("software")

                }).FirstOrDefault();

                return new AlberoProcMgr(this.db).GetNumeroFascicoloFromAlberoProcProtocollo(Convert.ToInt32(istanza.CODICEINTERVENTOPROC), idComune, istanza.SOFTWARE, istanza.CODICECOMUNE);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Funzionalit? recuperate dalle dll VisualBasic 6.0

        public Init.SIGePro.Data.RetStatoPratica StatoPratica(Istanze cls, bool retStatoIter)
        {

			bool closeCnn = false;

			try
			{
				if (db.Connection.State == ConnectionState.Closed)
				{
					db.Connection.Open();
					closeCnn = true;
				}

				var rVal = new Init.SIGePro.Data.RetStatoPratica();

				var sql = @"SELECT 
							  statiistanza.codicestato,
							  statiistanza.stato,
							  statiistanza.modificaistanza
							FROM 
							  istanze left join statiistanza ON 
								istanze.idcomune=statiistanza.idcomune AND 
								istanze.chiusura=statiistanza.codicestato AND 
								istanze.software=statiistanza.software 
							WHERE 
							  istanze.idcomune={0} AND 
							  istanze.codiceistanza={1}";

				sql = PreparaQueryParametrica(sql, "idComune", "codiceIstanza");

				using (IDbCommand cmd = db.CreateCommand(sql))
				{
					cmd.Parameters.Add(db.CreateParameter("idComune", cls.IDCOMUNE));
					cmd.Parameters.Add(db.CreateParameter("codiceIstanza", Convert.ToInt32(cls.CODICEISTANZA)));

					using (var dr = cmd.ExecuteReader())
					{
						if (dr.Read())
						{
							rVal.CodStatoPratica = dr["codicestato"].ToString();
							rVal.StatoPratica = dr["stato"].ToString();
							rVal.ModificaIstanza = Convert.ToInt32( dr["modificaistanza"] );
						}
					}

					if (retStatoIter)
					{
						cmd.CommandText = PreparaQueryParametrica("SELECT stato FROM istanze_tempistica WHERE idcomune={0} AND codiceistanza={1}", "idComune", "codiceIstanza");

						using (var dr = cmd.ExecuteReader())
						{
							if (dr.Read())
								rVal.StatoIter = dr["stato"].ToString();
						}
					}

					return rVal;
				}
			}
			finally
			{
				if (closeCnn)
					db.Connection.Close();
			}
        }

		internal DateTime? GetDataValidita(string idcomune, int codiceistanza)
		{

			bool closeCnn = false;

			try
			{
				if (db.Connection.State == ConnectionState.Closed)
				{
					db.Connection.Open();
					closeCnn = true;
				}

				var sql = PreparaQueryParametrica("SELECT datavalidita FROM istanze WHERE idcomune={0} and codiceistanza={1}","idComune","codiceIstanza");

				using (IDbCommand cmd = db.CreateCommand(sql))
				{
					cmd.Parameters.Add(db.CreateParameter("idComune", idcomune));
					cmd.Parameters.Add(db.CreateParameter("codiceIstanza", codiceistanza));

					using (var dr = cmd.ExecuteReader())
					{
						if (!dr.Read())
							return null;

						if (dr["datavalidita"] == DBNull.Value)
							return null;

						return Convert.ToDateTime(dr["datavalidita"]);
					}
				}
			}
			finally
			{
				if (closeCnn)
					db.Connection.Close();
			}



		//    /*
		//     * FIXVB6 000012: corpo del metodo commentato
		//     * 
		//     * implementazione del metodo commentata. Ritorna il valore fisso 01/01/2001
		//     * 
		//    sigeproClass_01.IstanzaClass istanza = new sigeproClass_01.IstanzaClass();
		//    Db myDb = Vb6db.CreaDb(db);
		//    istanza.Db = myDb;
		//    istanza.IdComune = idcomune;
		//    istanza.CodiceIstanza = codiceistanza;
		//    sigeproClass_01.retDataValidita retDataValidita = istanza.CalcolaDataValidita();

		//    return retDataValidita.Data.ToStringIT();
		//    */

		    throw new NotImplementedException("Metodo non implementato. Vedi 000012");
		}

        #endregion

		#region IIstanzeManager Members

		public IClasseContestoModelloDinamico LeggiIstanza(string idComune, int codiceIstanza)
		{
			return GetById(idComune, codiceIstanza , useForeignEnum.Recoursive);
		}



		#endregion
	}
}