using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.DatiDinamici;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Visura;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Visura
{
    public class VisuraDatiDinamiciService : IVisuraDatiDinamiciService
    {
        private readonly IAliasResolver _aliasResolver;
        private readonly IDatiDinamiciRepository _repository;
        private readonly IVisuraService _visuraService;
        private readonly ITokenApplicazioneService _tokenService;

        internal VisuraDatiDinamiciService(IAliasResolver aliasResolver, IDatiDinamiciRepository repository, IVisuraService visuraService, ITokenApplicazioneService tokenService)
        {
            this._aliasResolver = aliasResolver;
            this._repository = repository;
            this._visuraService = visuraService;
            _tokenService = tokenService;
        }

        public ModelloDinamicoIstanza GetModello(Istanze istanza, int idModello)
        {
            var cache = this._repository.GetCacheModelloDinamico(idModello);
            var factory = new VisuraFoDataAccessFactory(cache, istanza, this._tokenService);
            var loader = new ModelloDinamicoLoader(factory, _aliasResolver.AliasComune, ModelloDinamicoLoader.TipoModelloDinamicoEnum.Frontoffice);
            return new ModelloDinamicoIstanza(loader, idModello, 0, false);
        }

        
        public IEnumerable<VisuraTitoloModelloDinamicoIstanza> GetTitoliModelli(Istanze istanza)
        {
            var intervento = Convert.ToInt32(istanza.CODICEINTERVENTOPROC);
            var endo = istanza.EndoProcedimenti.Select(x => Convert.ToInt32(x.CODICEINVENTARIO));

            var schede = this._repository.GetSchedeDaInterventoEEndo(intervento, endo, Enumerable.Empty<string>(), UsaTipiLocalizzazioniPerSelezionareSchedeDinamiche.No);

            var schedeIntervento = schede.SchedeIntervento.Select(x => new VisuraTitoloModelloDinamicoIstanza
            {
                Id = x.Id,
                Descrizione = x.Descrizione
            });

            var schedeEndo = schede.SchedeEndoprocedimenti.Select(x => new VisuraTitoloModelloDinamicoIstanza
            {
                Id = x.Id,
                Descrizione = x.Descrizione
            });

            return schedeIntervento.Union(schedeEndo);
        }
        
    }
}
