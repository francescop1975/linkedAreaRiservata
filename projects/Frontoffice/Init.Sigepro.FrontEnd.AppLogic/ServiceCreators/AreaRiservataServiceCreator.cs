﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using System.ServiceModel;
// using Init.Sigepro.FrontEnd.AppLogic.Configuration;

using Init.Sigepro.FrontEnd.AppLogic.Configurazione;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using log4net;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using CuttingEdge.Conditions;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;

namespace Init.Sigepro.FrontEnd.AppLogic.ServiceCreators
{
	internal class AreaRiservataServiceCreator
	{
		IConfigurazione<ParametriSigeproSecurity> _config;
		ITokenApplicazioneService _tokenApplicazioneService;
        IAliasResolver _aliasResolver;

		public AreaRiservataServiceCreator(IAliasResolver aliasResolver, IConfigurazione<ParametriSigeproSecurity> config, ITokenApplicazioneService tokenApplicazioneService)
		{
			Condition.Requires(config, "config").IsNotNull();
			Condition.Requires(tokenApplicazioneService, "tokenApplicazioneService").IsNotNull();
			
			this._config = config;
			this._tokenApplicazioneService = tokenApplicazioneService;
            this._aliasResolver = aliasResolver;
		}

        public ServiceInstance<AreaRiservataServiceSoapClient> CreateClient()
        {
            return CreateClient(this._aliasResolver.AliasComune);
        }

		public ServiceInstance<AreaRiservataServiceSoapClient> CreateClient(string aliasComune)
		{
			ILog log = LogManager.GetLogger(typeof(AreaRiservataServiceCreator));

			log.DebugFormat("Inizializzazione del web service di gestione dati dell'area riservata all'endpoint {0} utilizzando il binding OggettiServiceBinding", this._config.Parametri.UrlAreaRiservataService);


			var endPoint = new EndpointAddress(this._config.Parametri.UrlAreaRiservataService);

			var binding = new BasicHttpBinding("areaRiservataServiceBinding");

			if (new Uri(this._config.Parametri.UrlAreaRiservataService).Scheme.ToUpper() == "HTTPS")
			{
				binding.Security.Mode = BasicHttpSecurityMode.Transport;
			}


			var ws = new AreaRiservataServiceSoapClient(binding, endPoint);

			var token = this._tokenApplicazioneService.GetToken(aliasComune);

			return new ServiceInstance<AreaRiservataServiceSoapClient>(ws, token);
		}
	}
}
