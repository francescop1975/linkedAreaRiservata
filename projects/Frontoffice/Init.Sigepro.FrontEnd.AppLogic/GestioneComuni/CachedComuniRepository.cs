using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.WsComuniService;
using Init.Sigepro.FrontEnd.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneComuni
{
    internal class WsCachedComuniRepository : IComuniRepository
    {
        private static class Constants
        {
            public const string LISTA_COMUNI_PROVINCIA = "SESSION_KEY_LISTA_COMUNI_PROVINCIA_";
            public const string LISTA_PROVINCIE = "SESSION_KEY_LISTA_PROVINCIE";
            public const string LISTA_CITTADINANZE = "CACHE_KEY_LISTA_CITTADINANZE";
            public const string DATI_COMUNE = "CACHE_KEY_DATI_COMUNE_";
            public const string DATI_PROVINCIA = "CACHE_KEY_DATI_PROVINCIA_";
            public const string ListaComuniAssociati = "WsCachedComuniRepository.ListaComuniAssociati.";
            public const string ListaCompletaComuni = "WsCachedComuniRepository.ListaCompletaComuni";
            public const string ListaComuniDaProvincia = "WsCachedComuniRepository.ListaComuniDaProvincia";
            public const string ListaProvincie = "WsCachedComuniRepository.ListaProvincie";
        }

        private readonly WsComuniRepository _repository;
        private readonly IAliasResolver _aliasResolver;
        private readonly ApplicationLevelCacheHelper _cache;

        public WsCachedComuniRepository(IAliasResolver aliasResolver,ComuniServiceCreator serviceCreator, ApplicationLevelCacheHelper cache)
        {
            this._repository = new WsComuniRepository(serviceCreator);
            _aliasResolver = aliasResolver;
            _cache = cache;
        }

        public IEnumerable<DatiComuneCompatto> FindComuneDaMatchParziale(string matchComune)
        {
            return this._repository.FindComuneDaMatchParziale(matchComune);
        }

        public IEnumerable<DatiProvinciaCompatto> FindProvinciaDaMatchParziale(string matchProvincia)
        {
            return this._repository.FindProvinciaDaMatchParziale(matchProvincia);
        }

        public CittadinanzaCompatto GetCittadinanzaDaId(int idCittadinanza)
        {
            return this._repository.GetCittadinanzaDaId(idCittadinanza);
        }

        public IEnumerable<CittadinanzaCompatto> GetCittadinanze()
        {
            return this._cache.GetOrAdd(
                Constants.LISTA_CITTADINANZE,
                () => this._repository.GetCittadinanze()
            );
        }

        public DatiComuneCompatto GetComuneDaCodiceIstat(string codiceIstat)
        {
            return this._repository.GetComuneDaCodiceIstat(codiceIstat);
        }

        public IEnumerable<DatiComuneCompatto> GetComuniAssociati(string software)
        {
            return this._cache.GetOrAdd(
                $"{Constants.ListaComuniAssociati}.{_aliasResolver.AliasComune}.{software}",
                () => this._repository.GetComuniAssociati(software)
            );
        }

        public DatiComuneCompatto GetDatiComune(string codiceComune)
        {
            return this._repository.GetDatiComune(codiceComune);
        }

        public DatiProvinciaCompatto GetDatiProvincia(string siglaProvincia)
        {
            return this._repository.GetDatiProvincia(siglaProvincia);
        }

        public IEnumerable<DatiComuneCompatto> GetListaComuni()
        {
            return this._cache.GetOrAdd(
                Constants.ListaCompletaComuni,
                () => this._repository.GetListaComuni()
            );
        }

        public IEnumerable<DatiComuneCompatto> GetListaComuni(string siglaProvincia)
        {
            return this._cache.GetOrAdd(
                Constants.ListaComuniDaProvincia + siglaProvincia,
                () => this._repository.GetListaComuni(siglaProvincia)
            );
        }

        public IEnumerable<DatiProvinciaCompatto> GetListaProvincie()
        {
            return this._cache.GetOrAdd(
                Constants.ListaProvincie,
                () => this._repository.GetListaProvincie()
            );
        }

        public string GetPecComuneAssociato(string codiceComune, string software)
        {
            return this._repository.GetPecComuneAssociato(codiceComune, software);
        }

        public DatiProvinciaCompatto GetProvinciaDaCodiceComune(string codiceComune)
        {
            return this._repository.GetProvinciaDaCodiceComune(codiceComune);
        }

        public bool IsCittadinanzaExtracomunitaria(int idCittadinanza)
        {
            return this._repository.IsCittadinanzaExtracomunitaria(idCittadinanza);
        }
    }
}
