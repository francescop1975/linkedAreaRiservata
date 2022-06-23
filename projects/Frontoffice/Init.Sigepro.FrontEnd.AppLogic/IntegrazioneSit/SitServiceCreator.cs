// -----------------------------------------------------------------------
// <copyright file="SitServiceCreator.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.IntegrazioneSit
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
	using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
	using log4net;
	using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
	using Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService;
	using System.ServiceModel;

	public class SitServiceCreator : ISitServiceCreator
	{
		ILog log = LogManager.GetLogger(typeof(SitServiceCreator));

		IConfigurazione<ParametriSIT> _configurazione;
		ITokenApplicazioneService _tokenApplicazioneService;

		public SitServiceCreator(IConfigurazione<ParametriSIT> configurazione, ITokenApplicazioneService tokenApplicazioneService)
		{
			this._configurazione = configurazione;
			this._tokenApplicazioneService = tokenApplicazioneService;
		}

		public ServiceInstance<WsSitSoapClient> CreateClient(string aliasComune)
		{
			if (!this._configurazione.Parametri.Attivo)
            {
				throw new Exception("Impossibile stabilire una connessione con il WS SIT, verificare che l'integrazione con il SIT sia attiva per il modulo corrente");
            }
			log.DebugFormat("Inizializzazione del web service di integrazione sit all'endpoint {0} utilizzando il binding areaRiservataServiceBinding", this._configurazione.Parametri.UrlWsSit);

			var endPoint = new EndpointAddress(this._configurazione.Parametri.UrlWsSit);

			var binding = new BasicHttpBinding("areaRiservataServiceBinding");

			if (endPoint.Uri.Scheme.ToUpper() == "HTTPS")
			{
				binding.Security.Mode = BasicHttpSecurityMode.Transport;
			}

			var ws = new WsSitSoapClient(binding, endPoint);

			var token = this._tokenApplicazioneService.GetToken(aliasComune);

			return new ServiceInstance<WsSitSoapClient>(ws, token);
		}

	}
}
