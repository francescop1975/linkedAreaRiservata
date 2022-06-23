using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common
{
    public class ScriptModelloManager : IDyn2ScriptModelloManager
    {
        private readonly ModelloDinamicoCache _cache;

        public ScriptModelloManager(ModelloDinamicoCache cache)
        {
            _cache = cache;
        }

        public IDyn2ScriptModello GetById(string idComune, int idModello, TipoScriptEnum contesto)
        {
            if (!_cache.ScriptsModello.ContainsKey(contesto))
                return null;

            return _cache.ScriptsModello[contesto];
        }
    }
}
