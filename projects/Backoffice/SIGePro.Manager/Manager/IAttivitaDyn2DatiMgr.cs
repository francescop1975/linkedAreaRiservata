
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Init.SIGePro.Data;
//using Init.SIGePro.Exceptions;
//using System.Data;
//using System.ComponentModel;
//using Init.SIGePro.Authentication;

//using PersonalLib2.Sql;
//using Init.Utils.Sorting;
//using Init.SIGePro.Validator;
//using Init.SIGePro.DatiDinamici;
//using Init.SIGePro.Manager.Logic.GestioneSchedeAttivita;
//using Init.SIGePro.Manager.Logic.GestioneSchedeAttivita.Eventi;
//using Init.SIGePro.DatiDinamici.Interfaces;

//namespace Init.SIGePro.Manager
//{
//    [DataObject(true)]
//	public partial class IAttivitaDyn2DatiMgr //: IIAttivitaDyn2DatiManager
//    {



//		/// <summary>
//		/// Utilizzato per mantenere la compatibilità.
//		/// va in errore se trova un campo con più di un indice
//		/// </summary>
//		/// <param name="idcomune"></param>
//		/// <param name="fk_ia_id"></param>
//		/// <param name="fk_d2c_id"></param>
//		/// <param name="indice"></param>
//		/// <returns></returns>
//		public IAttivitaDyn2Dati GetById(string idcomune, int fk_ia_id, int fk_d2c_id, int indice)
//		{
//			IAttivitaDyn2Dati c = new IAttivitaDyn2Dati();

//			c.Idcomune = idcomune;
//			c.FkIaId = fk_ia_id;
//			c.FkD2cId = fk_d2c_id;
//			c.Indice = indice;

//			return (IAttivitaDyn2Dati)db.GetClass(c);
//		}

//		internal List<IAttivitaDyn2Dati> GetValoriCampoNoIndice(string idComune, int idAttivita, int idCampo)
//		{
//			var filtro = new IAttivitaDyn2Dati
//			{
//				Idcomune = idComune,
//				FkIaId = idAttivita,
//				FkD2cId = idCampo,
//				OrderBy = "indice_molteplicita asc"
//			};

//			var list = GetList(filtro);
//			var rVal = new List<IAttivitaDyn2Dati>(list.Count);

//			list.ForEach(x => rVal.Add(x));

//			return rVal;
//		}

//		private void Validate(IAttivitaDyn2Dati cls, AmbitoValidazione ambitoValidazione)
//		{
//			if (ambitoValidazione == AmbitoValidazione.Insert && !cls.IndiceMolteplicita.HasValue)
//				cls.IndiceMolteplicita = 0;

//			RequiredFieldValidate(cls, ambitoValidazione);
//		}


//		public IEnumerable<int> GetCampiModelloPresentiAncheNelleIstanze(string idComune, int idModello, int idAttivita)
//		{

//			bool closeCnn = false;

//			try
//			{
//				if (db.Connection.State == ConnectionState.Closed)
//				{
//					db.Connection.Open();
//					closeCnn = true;
//				}

//				var sql = PreparaQueryParametrica(@"SELECT
//													  istanzedyn2dati.fk_d2c_id AS idCampo,
//													  Count(istanzedyn2dati.fk_d2c_id) AS datiPresenti
//													FROM
//													  dyn2_modellid,
//													  istanze,
//													  istanzedyn2dati
//													WHERE
//													  istanzedyn2dati.idcomune = dyn2_modellid.idcomune AND
//													  istanzedyn2dati.fk_d2c_id= dyn2_modellid.fk_d2c_id and
//													  istanze.idcomune         = istanzedyn2dati.idcomune and
//													  istanze.codiceistanza    = istanzedyn2dati.codiceistanza AND
//													  dyn2_modellid.idcomune   = {0} AND
//													  dyn2_modellid.fk_d2mt_id = {1} AND
//													  istanze.fk_idi_attivita = {2}
//													GROUP BY
//													  istanze.fk_idi_attivita,
//													  istanzedyn2dati.fk_d2c_id", "idComune", "idModello", "codiceAttivita");

//				using (IDbCommand cmd = db.CreateCommand(sql))
//				{
//					cmd.Parameters.Add(db.CreateParameter("idComune", idComune));
//					cmd.Parameters.Add(db.CreateParameter("idModello", idModello));
//					cmd.Parameters.Add(db.CreateParameter("codiceAttivita", idAttivita));

//					using (var dr = cmd.ExecuteReader())
//					{
//						var rVal = new List<int>();

//						while (dr.Read())
//						{
//							var idCampo = Convert.ToInt32(dr["idCampo"]);
//							var datiPresenti = Convert.ToInt32(dr["datiPresenti"]);

//							if (datiPresenti > 0)
//								rVal.Add(idCampo);
//						}

//						return rVal;
//					}
//				}
//			}
//			finally
//			{
//				if (closeCnn)
//					db.Connection.Close();
//			}

//		}

//        #region IIAttivitaDyn2DatiManager Members
//        public IEnumerable<IAttivitaDyn2Dati> GetValoriCampiDaIdModello(string idComune, int idAttivita, int idModello, int indiceModello)
//        {

//                string sql = $@"select 
//							i_attivitadyn2dati.* 
//						from 
//							dyn2_modellid 
//                                INNER JOIN  i_attivitadyn2dati ON
//                                    i_attivitadyn2dati.idcomune = dyn2_modellid.idcomune AND
//									i_attivitadyn2dati.fk_d2c_id = dyn2_modellid.fk_d2c_id
//						WHERE
//							dyn2_modellid.idcomune = {db.Specifics.QueryParameterName("idComune")} and									
//							dyn2_modellid.fk_d2mt_id = {db.Specifics.QueryParameterName("idModello")} and
//							i_attivitadyn2dati.fk_ia_id = {db.Specifics.QueryParameterName("idAttivita")} and
//							i_attivitadyn2dati.indice = {db.Specifics.QueryParameterName("indiceModello")}
//						order by 
//							i_attivitadyn2dati.indice_molteplicita";

//                return this.db.ExecuteReader(sql,
//                    mp =>
//                    {
//                        mp.AddParameter("idComune", idComune);
//                        mp.AddParameter("idModello", idModello);
//                        mp.AddParameter("idAttivita", idAttivita);
//                        mp.AddParameter("indiceModello", indiceModello);
//                    },
//                    dr => new IAttivitaDyn2Dati
//                    {
//                        FkIaId = idAttivita,
//                        FkD2cId = dr.GetInt("fk_d2c_id"),
//                        Idcomune = idComune,
//                        Indice = indiceModello,
//                        IndiceMolteplicita = dr.GetInt("indice_molteplicita"),
//                        Valore = dr.GetString("valore"),
//                        Valoredecodificato = dr.GetString("valoredecodificato")
//                    });


//        }

//		public void SalvaValoriCampi(string idComune, int idAttivita, DatiIdentificativiModello modello, IEnumerable<CampoDaSalvare> campiDaSalvare)
//		{
//			bool commitTrans = !db.IsInTransaction;

//			if (commitTrans)
//				db.BeginTransaction();
//			try
//			{

//				//if (salvaStorico)
//				//	SalvaStoricoModello(modello);

//				var indiceModello = modello.IndiceModello;

//				// Elimino tutti i valori del campo per la scheda corrente
//				foreach (var campo in campiDaSalvare)
//					EliminaValoriMultipliCampo(idComune, idAttivita, campo.Id, indiceModello);

//				// Ricreo tutti i valori dei campi che non siano == ""
//				foreach (var campo in campiDaSalvare)
//				{
//					for (int indiceMolteplicita = 0; indiceMolteplicita < campo.ListaValori.Count; indiceMolteplicita++)
//					{
//						var valore = campo.ListaValori[indiceMolteplicita].Valore;
//						var valoreDecodificato = campo.ListaValori[indiceMolteplicita].ValoreDecodificato;

//						if (String.IsNullOrEmpty(valore.Trim()))
//							continue;

//						if (String.IsNullOrEmpty(valoreDecodificato))
//							valoreDecodificato = valore;

//						var cls = new IAttivitaDyn2Dati
//						{
//							Idcomune = idComune,
//							FkD2cId = campo.Id,
//							FkIaId = idAttivita,
//							Valore = valore,
//							Valoredecodificato = valoreDecodificato,
//							Indice = indiceModello,
//							IndiceMolteplicita = indiceMolteplicita
//						};

//						Insert(cls);
//					}
//				}

//				if (commitTrans)
//				{
//					db.CommitTransaction();
//					commitTrans = false;
//				}

//				// Notifico al servizio di snapshotting che la scheda è stata modificata
//				this.SchedaAttivitaSalvata(idComune, idAttivita);
//			}
//			catch (Exception)
//			{
//				if (commitTrans)
//					db.RollbackTransaction();

//				throw;
//			}
//		}

//		private void SchedaAttivitaSalvata(string idComune, int idAttivita)
//		{
//			var svc = new EventiSchedeDinamicheAttivitaService();

//			svc.Handle(new SchedaDinamicaAttivitaSalvata( idAttivita ));
//		}

//		private void EliminaValoriMultipliCampo(string idComune, int idAttivita, int idCampo, int indiceMolteplicitaModello)
//		{
//			var filtro = new IAttivitaDyn2Dati
//			{
//				Idcomune = idComune,
//				FkIaId = idAttivita,
//				FkD2cId = idCampo,
//				Indice = indiceMolteplicitaModello
//			};

//			var lista = GetList(filtro);

//			for (int i = 0; i < lista.Count; i++)
//				Delete(lista[i]);
//		}

//		public void EliminaValoriCampi(string idComune, int idAttivita, DatiIdentificativiModello modello, IEnumerable<DatiIdentificativiCampo> campiDaEliminare)
//		{
//			bool commitTrans = !db.IsInTransaction;

//            if (commitTrans)
//            {
//                db.BeginTransaction();
//            }

//			try
//			{
//                foreach (var campo in campiDaEliminare)
//                {
//                    var sql = $@"delete from i_attivitadyn2dati where 
//                                    idcomune = {this.db.Specifics.QueryParameterName("idComune")} and 
//                                    fk_ia_id = {this.db.Specifics.QueryParameterName("idAttivita")} and 
//                                    fk_d2c_id = {this.db.Specifics.QueryParameterName("idCampo")} and 
//                                    indice = {this.db.Specifics.QueryParameterName("indice")}";

//                    this.db.ExecuteNonQuery(sql, mp =>
//                    {
//                        mp.AddParameter("idComune", idComune);
//                        mp.AddParameter("idAttivita", idAttivita);
//                        mp.AddParameter("idCampo", campo.Id);
//                        mp.AddParameter("indice", modello.IndiceModello);
//                    });
//                }

//                if (commitTrans)
//                    db.CommitTransaction();
//            }
//			catch (Exception)
//			{
//				if (commitTrans)
//					db.RollbackTransaction();

//				throw;
//			}
//		}

//		#endregion
//	}
//}
				