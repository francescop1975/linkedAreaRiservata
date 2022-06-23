using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.WsUrlAccessoConsoleService;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneVbgConsole
{
    public class UrlConsoleRepository : IUrlConsoleRepository
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(UrlConsoleRepository));
        private readonly ITokenApplicazioneService _tokenApplicazioneService;
        private readonly IConfigurazione<ParametriSigeproSecurity> _config;
        private readonly ISoftwareResolver _softwareResolver;

        public UrlConsoleRepository(ITokenApplicazioneService tokenApplicazioneService, IConfigurazione<ParametriSigeproSecurity> config, ISoftwareResolver softwareResolver)
        {
            _tokenApplicazioneService = tokenApplicazioneService ?? throw new ArgumentNullException(nameof(tokenApplicazioneService));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _softwareResolver = softwareResolver ?? throw new ArgumentNullException(nameof(softwareResolver));
        }

        public ConfigurazioneUrlConsole GetUrlAccesso()
        {
            return CallService(ws =>
            {
                return ws.Service.GetUrlAccessoConsole(ws.Token, this._softwareResolver.Software);
            });
        }

        #region metodi per l'nvocazione del ws
        private T CallService<T>(Func<ServiceInstance<WsUrlAccessoConsoleService.WsUrlAccessoConsoleServiceClient>, T> callback)
        {
            using (var ws = CreateClient())
            {
                try
                {
                    return callback(ws);
                }
                catch (Exception ex)
                {
                    this._log.Error($"{GetType()}, Errore durante la chiamata al web service: {ex} ");
                    ws.Service.Abort();
                    throw;
                }
            }
        }

        private ServiceInstance<WsUrlAccessoConsoleService.WsUrlAccessoConsoleServiceClient> CreateClient()
        {
            var serviceUrl = this._config.Parametri.UrlWsUrlAccessoConsoleService;
            var bindingName = "defaultServiceBinding";
            var token = this._tokenApplicazioneService.GetToken();

            this._log.Debug($"Inizializzazione del web service di url accesso console all'endpoint {serviceUrl} utilizzando il binding {bindingName}");

            var endPoint = new EndpointAddress(serviceUrl);
            var binding = new BasicHttpBinding(bindingName);
            var ws = new WsUrlAccessoConsoleService.WsUrlAccessoConsoleServiceClient(binding, endPoint);

            return new ServiceInstance<WsUrlAccessoConsoleService.WsUrlAccessoConsoleServiceClient>(ws, token);
        }
        #endregion
    }
}
