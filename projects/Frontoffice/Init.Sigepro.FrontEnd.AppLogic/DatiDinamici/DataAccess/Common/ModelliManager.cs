using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.SIGePro.DatiDinamici.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common
{
    public class ModelliManager : IDyn2ModelliManager
    {
        private readonly ModelloDinamicoCache _cache;

        public ModelliManager(ModelloDinamicoCache cache)
        {
            _cache = cache;
        }

        public IDyn2Modello GetById(string idComune, int idModello)
        {
            return _cache.Modello;
        }
    }
}
