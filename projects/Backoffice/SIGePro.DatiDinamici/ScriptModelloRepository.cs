using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici
{
    public class ScriptModelloRepository : IScriptModelloRepository
    {
        private readonly string _idComune;
        private readonly IDyn2ScriptModelloManager _scriptModelloManager;

        public ScriptModelloRepository(IDyn2ScriptModelloManager scriptModelloManager, string idComune)
        {
            _idComune = idComune;
            _scriptModelloManager = scriptModelloManager;
        }

        public IDyn2ScriptModello GetById(int idModello, TipoScriptEnum contesto)
        {
            return this._scriptModelloManager.GetById(this._idComune, idModello, contesto);
        }
    }
}
