using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common
{
    public class TestoModelloManager : IDyn2TestoModelloManager
    {
        private readonly ModelloDinamicoCache _cache;

        public TestoModelloManager(ModelloDinamicoCache cache)
        {
            _cache = cache;
        }

        public SerializableDictionary<int, IDyn2TestoModello> GetListaTestiDaIdModello(string idComune, int idModello)
        {
            return this._cache.ListaTesti;
        }
    }
}
