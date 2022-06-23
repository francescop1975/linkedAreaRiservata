
using System;
using System.Collections.Generic;
using System.Text;
using Init.SIGePro.Data;
using Init.SIGePro.Exceptions;
using System.Data;
using System.ComponentModel;
using Init.SIGePro.Authentication;
using PersonalLib2.Sql;
using Init.Utils.Sorting;
using Init.Utils;
using Init.SIGePro.Validator;
//using Init.SIGePro.DatiDinamici.Interfaces.Anagrafe;
using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using PersonalLib2.Data;

namespace Init.SIGePro.Manager
{
    [DataObject(true)]
	public partial class AnagrafeDyn2DatiMgr
    {
		///// <summary>
		///// Ottiene il valore di un campo collegato ad un'istanza
		///// (ho preferito implementare questo metodo inveec di utilizzare il GetById
		///// in quanto verr� richiamato numerose volte e ha bisogno di tutta l'ottimizzazione possibile
		///// </summary>
		///// <param name="idComune"></param>
		///// <param name="idAnagrafe"></param>
		///// <param name="idCampo"></param>
		///// <returns></returns>
		//public List<IAnagrafeDyn2Dati> GetValoriCampo(string idComune, int idAnagrafe, int idCampo, int indice)
		//{
		//	var filtro = new AnagrafeDyn2Dati
		//	{
		//		Idcomune = idComune,
		//		Codiceanagrafe = idAnagrafe,
		//		FkD2cId = idCampo,
		//		Indice = indice
		//	};

		//	var lista = GetList(filtro);
		//	var rVal = new List<IAnagrafeDyn2Dati>();

		//	lista.ForEach(x => rVal.Add(x));

		//	return rVal;

		//}

        public IEnumerable<AnagrafeDyn2Dati> GetValoriCampiDaIdModello(string idComune, int idAnagrafe, int idModello, int indiceModello)
        {
            string sql = $@"select 
							anagrafedyn2dati.* 
						from 
							dyn2_modellid 
                                INNER JOIN  anagrafedyn2dati ON
                                    anagrafedyn2dati.idcomune = dyn2_modellid.idcomune AND
									anagrafedyn2dati.fk_d2c_id = dyn2_modellid.fk_d2c_id
						WHERE
							dyn2_modellid.idcomune = {db.Specifics.QueryParameterName("idComune")} and									
							dyn2_modellid.fk_d2mt_id = {db.Specifics.QueryParameterName("idModello")} and
							anagrafedyn2dati.codiceanagrafe = {db.Specifics.QueryParameterName("idAnagrafe")} and
							anagrafedyn2dati.indice = {db.Specifics.QueryParameterName("indiceModello")}
						order by 
							anagrafedyn2dati.indice_molteplicita";

            return this.db.ExecuteReader(sql,
                mp =>
                {
                    mp.AddParameter("idComune", idComune);
                    mp.AddParameter("idModello", idModello);
                    mp.AddParameter("idAnagrafe", idAnagrafe);
                    mp.AddParameter("indiceModello", indiceModello);
                },
                dr => new AnagrafeDyn2Dati
                {
                    Codiceanagrafe = idAnagrafe,
                    FkD2cId = dr.GetInt("fk_d2c_id"),
                    Idcomune = idComune,
                    Indice = indiceModello,
                    IndiceMolteplicita = dr.GetInt("indice_molteplicita"),
                    Valore = dr.GetString("valore"),
                    Valoredecodificato = dr.GetString("valoredecodificato")
                });
        }


        public List<AnagrafeDyn2Dati> GetByIdNoIndice(string idcomune, int codiceAnagrafe, int fk_d2c_id)
		{
			AnagrafeDyn2Dati c = new AnagrafeDyn2Dati();

			c.Idcomune = idcomune;
			c.Codiceanagrafe = codiceAnagrafe;
			c.FkD2cId = fk_d2c_id;

			return db.GetClassList(c).ToList<AnagrafeDyn2Dati>();
		}

		internal List<AnagrafeDyn2Dati> GetValoriCampoNoIndice(string idComune, int idAnagrafe, int idCampo)
		{
			AnagrafeDyn2Dati filtro = new AnagrafeDyn2Dati();
			filtro.Idcomune = idComune;
			filtro.Codiceanagrafe = idAnagrafe;
			filtro.FkD2cId = idCampo;
			filtro.OrderBy = "indice_molteplicita asc";

			return GetList(filtro);
		}



		/// <summary>
		/// Utilizzato per mantenere la compatibilit�.
		/// Va in errore se trova pi� di un record
		/// </summary>
		/// <param name="idcomune"></param>
		/// <param name="codiceanagrafe"></param>
		/// <param name="fk_d2c_id"></param>
		/// <param name="indice"></param>
		/// <returns></returns>
		public AnagrafeDyn2Dati GetById(string idcomune, int? codiceanagrafe, int? fk_d2c_id, int? indice)
		{
			AnagrafeDyn2Dati c = new AnagrafeDyn2Dati();

			c.Idcomune = idcomune;
			c.Codiceanagrafe = codiceanagrafe;
			c.FkD2cId = fk_d2c_id;
			c.Indice = indice;

			return (AnagrafeDyn2Dati)db.GetClass(c);
		}


		private void Validate(AnagrafeDyn2Dati cls, AmbitoValidazione ambitoValidazione)
		{
			if (ambitoValidazione == AmbitoValidazione.Insert && !cls.IndiceMolteplicita.HasValue)
				cls.IndiceMolteplicita = 0;

			RequiredFieldValidate(cls, ambitoValidazione);
		}

		#region IAnagrafeDyn2DatiManager Members

		public void SalvaValoriCampi(string idComune, int codiceAnagrafe, DatiIdentificativiModello modello, IEnumerable<CampoDaSalvare> campiDaSalvare)
		{
			bool commitTrans = !db.IsInTransaction;

			if (commitTrans)
				db.BeginTransaction();
			try
			{

				//if (salvaStorico)
				//	SalvaStoricoModello(modello);

				var indiceModello = modello.IndiceModello;

				// Elimino tutti i valori del campo per la scheda corrente
				foreach (var campo in campiDaSalvare)
					EliminaValoriMultipliCampo(idComune, codiceAnagrafe, campo.Id, indiceModello);

				// Ricreo tutti i valori dei campi che non siano == ""
				foreach (var campo in campiDaSalvare)
				{
					for(int indiceMolteplicita = 0; indiceMolteplicita < campo.ListaValori.Count; indiceMolteplicita++)
					{
						var valore = campo.ListaValori[indiceMolteplicita].Valore;
						var valoreDecodificato = campo.ListaValori[indiceMolteplicita].ValoreDecodificato;

						if (String.IsNullOrEmpty(valore.Trim()))
							continue;

						if (String.IsNullOrEmpty(valoreDecodificato))
							valoreDecodificato = valore;

						var cls = new AnagrafeDyn2Dati
						{
							Idcomune = idComune,
							FkD2cId = campo.Id,
							Codiceanagrafe = codiceAnagrafe,
							Valore = valore,
							Valoredecodificato = valoreDecodificato,
							Indice = indiceModello,
							IndiceMolteplicita = indiceMolteplicita
						};

						Insert(cls);
					}
				}
			}
			catch (Exception)
			{
				if (commitTrans)
					db.RollbackTransaction();

				throw;
			}
			finally
			{
				if (commitTrans)
					db.CommitTransaction();
			}
		}


		private void EliminaValoriMultipliCampo(string idComune, int idAnagrafe, int idCampo, int indiceMolteplicitaModello)
		{
			var filtro = new AnagrafeDyn2Dati
			{
				Idcomune = idComune,
				Codiceanagrafe = idAnagrafe,
				FkD2cId = idCampo,
				Indice = indiceMolteplicitaModello
			};

			var lista = GetList(filtro);

			for (int i = 0; i < lista.Count; i++)
				Delete(lista[i]);
		}



		//private void SalvaStoricoModello(ModelloDinamicoAnagrafica modello)
		//{
		//	// Carico le righe modificate
		//	var righeStorico = new List<AnagrafeDyn2DatiStorico>();

		//	foreach (var rigaModello in modello.Righe)
		//	{
		//		for (int j = 0; j < rigaModello.NumeroColonne; j++)
		//		{
		//			if (rigaModello[j] == null) continue;

		//			var valoriDb = GetValoriCampoNoIndice(modello.IdComune, modello.IdAnagrafe, rigaModello[j].Id);

		//			int indiceMin = int.MaxValue;

		//			if (valoriDb.Count == 0)
		//				indiceMin = 0;

		//			for (int idxCampo = 0; idxCampo < valoriDb.Count; idxCampo++)
		//			{
		//				var fkD2cId = valoriDb[idxCampo].FkD2cId.Value;
		//				var indice = valoriDb[idxCampo].Indice.GetValueOrDefault(0);
		//				var indiceMolteplicita = valoriDb[idxCampo].IndiceMolteplicita.GetValueOrDefault(0);
		//				var valore = valoriDb[idxCampo].Valore;
		//				var valoreDecodificato = valoriDb[idxCampo].Valoredecodificato;

		//				if (indiceMin > indiceMolteplicita)
		//					indiceMin = indiceMolteplicita;

		//				var riga = new AnagrafeDyn2DatiStorico
		//				{
		//					Idcomune = modello.IdComune,
		//					Codiceanagrafe = modello.IdAnagrafe,
		//					FkD2mtId = modello.IdModello,
		//					FkD2cId = fkD2cId,
		//					Indice = indice,
		//					IndiceMolteplicita = indiceMolteplicita,
		//					Valore = String.IsNullOrEmpty(valoreDecodificato) ? valore : valoreDecodificato

		//				};
		//				righeStorico.Add(riga);
		//			}
		//		}
		//	}

		//	// Se non � stata caricata nessuna riga allora non salvo la versione perch� sarebbe un modello vuoto
		//	if (righeStorico.Count == 0)
		//		return;

		//	// Preparo il salvataggio della testata
		//	var testataStoricoMgr = new AnagrafeDyn2ModelliTStoricoMgr(db);
		//	var righeStoricoMgr = new AnagrafeDyn2DatiStoricoMgr(db);

		//	// Salvo una nuova riga in AnagrafeDyn2Modellit_storico
		//	var testataStorico = new AnagrafeDyn2ModelliTStorico
		//	{
		//		Idcomune = modello.IdComune,
		//		Codiceanagrafe = modello.IdAnagrafe,
		//		FkD2mtId = modello.IdModello
		//	};

		//	testataStorico = testataStoricoMgr.Insert(testataStorico);

		//	for (int i = 0; i < righeStorico.Count; i++)
		//	{
		//		righeStorico[i].Idversione = testataStorico.Idversione;

		//		righeStoricoMgr.Insert(righeStorico[i]);
		//	}
		//}

		public void EliminaValoriCampi(string idComune, int codiceAnagrafe, DatiIdentificativiModello modello, IEnumerable<DatiIdentificativiCampo> campiDaEliminare)
		{
			bool commitTrans = !db.IsInTransaction;

            if (commitTrans)
            {
                db.BeginTransaction();
            }

			try
			{
				foreach (var campo in campiDaEliminare)
				{
                    var sql = $@"delete from anagrafedyn2dati where 
                                    idcomune = {this.db.Specifics.QueryParameterName("idComune")} and 
                                    codiceanagrafe = {this.db.Specifics.QueryParameterName("codiceAnagrafe")} and 
                                    fk_d2c_id = {this.db.Specifics.QueryParameterName("idCampo")} and 
                                    indice = {this.db.Specifics.QueryParameterName("indice")}";

                    this.db.ExecuteNonQuery(sql, mp =>
                    {
                        mp.AddParameter("idComune", idComune);
                        mp.AddParameter("codiceAnagrafe", codiceAnagrafe);
                        mp.AddParameter("idCampo", campo.Id);
                        mp.AddParameter("indice", modello.IndiceModello);
                    });
				}

                if (commitTrans)
                    db.CommitTransaction();
            }
			catch (Exception)
			{
				if (commitTrans)
					db.RollbackTransaction();

				throw;
			}
		}


        #endregion
    }
}
				