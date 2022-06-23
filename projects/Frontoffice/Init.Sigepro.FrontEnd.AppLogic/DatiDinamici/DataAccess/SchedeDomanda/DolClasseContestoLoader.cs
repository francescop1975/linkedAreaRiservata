using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Init.SIGePro.DatiDinamici.Interfaces;
using System;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.SchedeDomanda
{
    public class DolClasseContestoLoader : IClasseContestoLoader
    {
        private readonly Lazy<Istanze> _cacheIstanza;

        public DolClasseContestoLoader(Lazy<Istanze> cacheIstanza)
        {
            _cacheIstanza = cacheIstanza;
        }

        internal QueryLocalizzazioniDaClasseIstanze GetQueryLocalizzazioni()
        {
            return new QueryLocalizzazioniDaClasseIstanze(_cacheIstanza.Value);
        }

        public IClasseContestoModelloDinamico LoadClass()
        {
            return _cacheIstanza.Value;
        }
    }
}
