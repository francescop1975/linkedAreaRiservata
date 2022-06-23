using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.SIGePro.DatiDinamici.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common
{
    public class ProprietaCampiManager : IDyn2ProprietaCampiManager
    {
        private readonly ModelloDinamicoCache _cache;

        public ProprietaCampiManager(ModelloDinamicoCache cache)
        {
            _cache = cache;
        }

        public List<IDyn2ProprietaCampo> GetProprietaCampo(string idComune, int idCampo)
        {
            if (_cache.ProprietaCampiDinamici.ContainsKey(idCampo))
                return _cache.ProprietaCampiDinamici[idCampo];

            return new List<IDyn2ProprietaCampo>();
        }
    }
}
