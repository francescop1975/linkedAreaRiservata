using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneRisorseTestuali
{
    public class CachedRisorseTestualiService: RisorseTestualiService
    {
        private readonly IAliasResolver _aliasResolver;
        private readonly ISoftwareResolver _softwareResolver;

        internal CachedRisorseTestualiService(AreaRiservataServiceCreator serviceCreator, IAliasResolver aliasResolver, ISoftwareResolver softwareResolver, IAuthenticationDataResolver authDataResolver)
            :base(serviceCreator, softwareResolver, authDataResolver)
        {
            this._aliasResolver = aliasResolver;
            this._softwareResolver = softwareResolver;
        }

        public override Dictionary<string, string> GetListaRisorse()
        {
            var cacheKey = GetCacheKey();

            return CacheHelper.GetOrAdd(cacheKey, () => base.GetListaRisorse());
        }

        public override void AggiornaRisorsa(string id, string valore)
        {
            var key = RisorseTestualiService.Constants.Prefix + id;

            base.AggiornaRisorsa(id, valore);

            if (String.IsNullOrEmpty(valore))
            {
                GetListaRisorse().Remove(key);
                return;
            }

            GetListaRisorse()[key] = valore;
        }

        private string GetCacheKey()
        {
            return $"risorse-testuali.{this._aliasResolver.AliasComune}.{this._softwareResolver.Software}";
        }
    }
}
