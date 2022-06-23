
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
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Scripts;

namespace Init.SIGePro.Manager
{
    [DataObject(true)]
	public partial class Dyn2ModelliScriptMgr : IDyn2ScriptModelloManager
    {

		#region IDyn2ScriptModelloManager Members

		public List<Dyn2ModelliScript> GetList(string idComune, int idModello)
		{
			var filtro = new Dyn2ModelliScript
			{
				Idcomune = idComune,
				FkD2mtId = idModello
			};

			return GetList(filtro);
		}

		public IDyn2ScriptModello GetById(string idComune, int idModello, TipoScriptEnum contesto)
		{
			return GetById(idComune, idModello, contesto.ToString());
		}

		#endregion

		public string GetTestoScript(string idComune, int idModello, TipoScriptEnum contesto)
        {
			var script = GetById(idComune, idModello, contesto.ToString());

			return script == null ? String.Empty : script.GetTestoScript();
		}

		public void SalvaScript(string idComune, int idModello, TipoScriptEnum tipoScript, string corpoScript, string corpoUsing)
        {
			this.db.BeginTransaction();
			try
            {
				this.AggiornaScript(idComune, idModello, tipoScript, corpoScript);
				this.AggiornaScript(idComune, idModello, TipoScriptEnum.Using, corpoUsing);

				this.db.CommitTransaction();
			}
			catch(Exception)
            {
				this.db.RollbackTransaction();
				
				throw;
            }
		}

		private void AggiornaScript(string idComune, int idModello, TipoScriptEnum tipoScript, string corpoScript)
		{
			Dyn2ModelliScript script = this.GetById(idComune, idModello, tipoScript.ToString());
			var insert = false;

			if (script != null && String.IsNullOrEmpty(corpoScript))
            {
				this.Delete(script);
				return;
            }

			if (script == null)
			{
				script = new Dyn2ModelliScript();
				script.Idcomune = idComune;
				script.FkD2mtId = idModello;
				script.Evento = tipoScript.ToString();

				insert = true;
			}

			script.SetTestoScript(corpoScript);

			if (insert)
				this.Insert(script);
			else
				this.Update(script);
		}
	}
}
				