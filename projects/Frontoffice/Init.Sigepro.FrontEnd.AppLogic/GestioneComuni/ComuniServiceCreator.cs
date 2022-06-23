using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneComuni
{
    internal class ComuniServiceCreator
	{
		private readonly IConfigurazione<ParametriSigeproSecurity> _config;
		private readonly ITokenApplicazioneService _tokenApplicazioneService;
		private readonly ILog log = LogManager.GetLogger(typeof(AreaRiservataServiceCreator));

		public ComuniServiceCreator(IConfigurazione<ParametriSigeproSecurity> config, ITokenApplicazioneService tokenApplicazioneService)
		{
			this._config = config;
			this._tokenApplicazioneService = tokenApplicazioneService;
		}

		public ServiceInstance<WsComuniService.WsComuniServiceClient> CreateClient()
		{
			var serviceUrl = this._config.Parametri.UrlWsComuniService;
			var bindingName = "defaultServiceBinding";
			var token = this._tokenApplicazioneService.GetToken();

			log.Debug($"Inizializzazione del web service di gestione comuni all'endpoint {serviceUrl} utilizzando il binding {bindingName}");

			var endPoint = new EndpointAddress(serviceUrl);
			var binding = new BasicHttpBinding(bindingName);

			if (new Uri(serviceUrl).Scheme.ToUpper() == "HTTPS")
			{
				binding.Security.Mode = BasicHttpSecurityMode.Transport;
			}

			var ws = new WsComuniService.WsComuniServiceClient(binding, endPoint);

			return new ServiceInstance<WsComuniService.WsComuniServiceClient>(ws, token);
		}
	}
}
