using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common
{
    public class IstanzaDyn2CampiManager : IDyn2CampiManager
    {
        private readonly ModelloDinamicoCache _cache;

        public IstanzaDyn2CampiManager(ModelloDinamicoCache cache)
        {
            _cache = cache;
        }

        public SerializableDictionary<int, IDyn2Campo> GetListaCampiDaIdModello(string idComune, int idModello)
        {
            return this._cache.ListaCampiDinamici;
        }

        public IDyn2Campo GetById(string idComune, int idCampo)
        {
            throw new NotImplementedException();
        }
    }
}
