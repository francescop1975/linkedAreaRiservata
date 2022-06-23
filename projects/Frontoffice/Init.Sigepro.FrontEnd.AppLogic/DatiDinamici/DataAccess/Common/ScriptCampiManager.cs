using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Scripts;
using System.Collections.Generic;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common
{
    public class ScriptCampiManager : IDyn2ScriptCampiManager
    {
        private readonly ModelloDinamicoCache _cache;

        public ScriptCampiManager(ModelloDinamicoCache cache)
        {
            _cache = cache;
        }


        public Dictionary<TipoScriptEnum, IDyn2ScriptCampo> GetScriptsCampo(string idComune, int idCampo)
        {
            if (_cache.ScriptsCampiDinamici.ContainsKey(idCampo))
                return _cache.ScriptsCampiDinamici[idCampo];

            return new Dictionary<TipoScriptEnum, IDyn2ScriptCampo>();
        }
    }
}
