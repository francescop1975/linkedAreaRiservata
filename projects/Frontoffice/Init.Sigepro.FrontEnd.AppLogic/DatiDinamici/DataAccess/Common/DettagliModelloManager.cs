using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.SIGePro.DatiDinamici.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common
{
    public class DettagliModelloManager : IDyn2DettagliModelloManager
    {
        private readonly ModelloDinamicoCache _cache;

        public DettagliModelloManager(ModelloDinamicoCache cache)
        {
            _cache = cache;
        }

        public List<IDyn2DettagliModello> GetList(string idComune, int idModello)
        {
            return this._cache.Struttura;
        }
    }
}
