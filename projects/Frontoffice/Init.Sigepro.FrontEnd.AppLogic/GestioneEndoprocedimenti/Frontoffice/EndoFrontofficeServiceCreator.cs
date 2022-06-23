using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.Frontoffice
{
    internal class EndoFrontofficeServiceCreator
    {
        private readonly IConfigurazione<ParametriSigeproSecurity> _config;
        private readonly ITokenApplicazioneService _tokenApplicazioneService;
        private readonly ILog _log = LogManager.GetLogger(typeof(EndoFrontofficeServiceCreator));

        public EndoFrontofficeServiceCreator(IConfigurazione<ParametriSigeproSecurity> config, ITokenApplicazioneService tokenApplicazioneService, IAliasResolver aliasResolver)
        {
            this._config = config ?? throw new ArgumentNullException(nameof(config));
            this._tokenApplicazioneService = tokenApplicazioneService ?? throw new ArgumentNullException(nameof(tokenApplicazioneService));
        }

        public ServiceInstance<WsEndoFrontofficeServiceClient> CreateClient(string alias)
        {
            _log.DebugFormat("Inizializzazione del web service di gestione dati endo console all'endpoint {0} utilizzando il binding areaRiservataServiceBinding", this._config.Parametri.UrlWsEndoFrontoffice);

            var endPoint = new EndpointAddress(this._config.Parametri.UrlWsEndoFrontoffice);
            var binding = new BasicHttpBinding("areaRiservataServiceBinding");
            var ws = new WsEndoFrontofficeServiceClient(binding, endPoint);

            var token = this._tokenApplicazioneService.GetToken(alias);

            return new ServiceInstance<WsEndoFrontofficeServiceClient>(ws, token);
        }
    }
}
