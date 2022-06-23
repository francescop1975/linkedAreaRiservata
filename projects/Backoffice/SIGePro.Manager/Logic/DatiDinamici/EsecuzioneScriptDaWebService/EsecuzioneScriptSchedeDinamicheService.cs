using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonalLib2.Data;
using Init.SIGePro.DatiDinamici;
using Init.SIGePro.Manager.Manager;
using Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Istanze;

namespace Init.SIGePro.Manager.Logic.DatiDinamici.EsecuzioneScriptDaWebService
{
	public class EsecuzioneScriptSchedeDinamicheService
	{
		DataBase _database;
        private readonly string _idComune;

        public EsecuzioneScriptSchedeDinamicheService (DataBase database, string idComune)
		{
			this._database = database;
            _idComune = idComune;
        }

		public List<string> EseguiScriptSchedaSingolaIstanza(int codiceIstanza, int idScheda, bool eseguiScriptCaricamento, bool eseguiScriptSalvataggio)
		{
            var daf = new IstanzeDyn2DataAccessFactory(this._database, this._idComune, codiceIstanza);
            var loader = new ModelloDinamicoLoader(daf, this._idComune, ModelloDinamicoLoader.TipoModelloDinamicoEnum.Backoffice);
            var modello = new ModelloDinamicoIstanza(loader, idScheda, 0, false);

			var esecutoreScript = new EsecutoreScript(modello);

			esecutoreScript.EseguiScript(eseguiScriptCaricamento, eseguiScriptSalvataggio);

			return esecutoreScript.GetErrori();
		}

		public List<string> EseguiScriptSchedeIstanza(int codiceIstanza, bool eseguiScriptCaricamento, bool eseguiScriptSalvataggio)
		{
			var mgr = new IstanzeDyn2ModelliTMgr(this._database);

			var listaModelli = mgr.GetList(new Data.IstanzeDyn2ModelliT
			{
				Idcomune = this._idComune,
				Codiceistanza = codiceIstanza
			}).Select( x => x.FkD2mtId.Value );

			var listaErrori = new List<string>();

			foreach (var idModello in listaModelli)
			{
				var errori = EseguiScriptSchedaSingolaIstanza(codiceIstanza, idModello, eseguiScriptCaricamento, eseguiScriptSalvataggio);

				listaErrori.AddRange(errori);
			}

			return listaErrori;
		}
	}
}
