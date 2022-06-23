using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Manager.ParixGateService;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Net;
using log4net;
using System.Security.Cryptography.X509Certificates;

namespace Init.SIGePro.Manager.Logic.RicercheAnagrafiche.Parix
{
	internal class ParixProxy
	{
		private readonly ConfigurazioneParix _configurazione;
		private readonly ILog _log = LogManager.GetLogger(typeof(ParixProxy));

		public ParixProxy(ConfigurazioneParix configurazione)
		{
			this._configurazione = configurazione;
		}

		public string DettaglioRidottoImpresa(string CCIAA, string NREA)
		{
			using (var ws = CreaWebService())
			{
				ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, error) => true;

				using (OperationContextScope scope = new OperationContextScope(ws.InnerChannel))
				{
					AggiungiCredenzialiCartAContextScope(this._configurazione.BasicAuthUser, this._configurazione.BasicAuthPassword);

					_log.Debug($"DettagliRidottoImpresa, prarametri: CCIAA={CCIAA}, NREA={NREA}, config.Switchcontrol={this._configurazione.Switchcontrol}, config.User={this._configurazione.User}, config.Password={this._configurazione.Password}");

					string result = ws.DettaglioRidottoImpresa(CCIAA, NREA, this._configurazione.Switchcontrol, this._configurazione.User, this._configurazione.Password);

					_log.Debug("result: " + result);

					return result;
				}
			}
		}

		public string RicercaImpreseNonCessatePerCodiceFiscale(string partitaIva)
		{
			try
			{
				using (var ws = CreaWebService())
				{
					ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, error) => true;

					using (OperationContextScope scope = new OperationContextScope(ws.InnerChannel))
					{
						AggiungiCredenzialiCartAContextScope(this._configurazione.BasicAuthUser, this._configurazione.BasicAuthPassword);

						_log.Debug($"RicercaImpreseNonCessatePerCodiceFiscale: partitaIva={partitaIva}, config.Switchcontrol={this._configurazione.Switchcontrol}, config.User={this._configurazione.User}, config.Password={this._configurazione.Password}");

						string result = ws.RicercaImpreseNonCessatePerCodiceFiscale(partitaIva, this._configurazione.Switchcontrol, this._configurazione.User, this._configurazione.Password);

						_log.Debug("result: " + result);

						return result;
					}
				}
			}catch(Exception ex)
            {
				this._log.Error($"Errore durante la chiamata al ws Parix: {ex}");

				throw;
            }
		}

		private void AggiungiCredenzialiCartAContextScope(string username, string password)
		{
			if (!String.IsNullOrEmpty(username))
			{
				_log.Debug($"Il parametro BasicAuthUser non è vuoto, verrà utilizzata l'autenticazione basic per effettuare la chiamata a parix, username={username}");

				var credentials = GetCartCredentials(username, password);
				var request = new HttpRequestMessageProperty();

				request.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " + credentials;

				OperationContext.Current.OutgoingMessageProperties.Add(HttpRequestMessageProperty.Name, request);
			}
		}


		private CRSimpleWSImplClient CreaWebService()
		{
			var url = this._configurazione.Url;
			var proxy = this._configurazione.ProxyAddress;
			var usaProxy = this._configurazione.UsaProxy;
			var usaCertificatoClient = this._configurazione.UsaCertificatoClient;
			var pathCertificatoClient = this._configurazione.PathCertificatoClient;
			var passwordCertificatoClient = this._configurazione.PasswordCertificatoClient;

			var endPointAddress = new EndpointAddress(url);
			var binding = new BasicHttpBinding("parixHttpBinding");

			binding.MaxReceivedMessageSize = 2147483647;
			binding.ReaderQuotas.MaxStringContentLength = 2048000;

			if (usaCertificatoClient)
			{
				binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
			}

			var uri = new Uri(url);

			binding.Security.Mode = (uri.Scheme.ToUpperInvariant() == "HTTPS") ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None;

			if (usaProxy)
			{
				this._log.Debug($"Ws parix inizializzato con proxy {proxy}");

				binding.UseDefaultWebProxy = false;
				binding.ProxyAddress = new Uri(proxy);
			}

			var svc = new CRSimpleWSImplClient(binding, endPointAddress);

			if (usaCertificatoClient)
			{
				this._log.Debug($"Ws parix inizializzato con il certificato trovato al path {pathCertificatoClient} {(String.IsNullOrEmpty(passwordCertificatoClient) ? "senza password" : "con password")}");

				var certificate = new X509Certificate2(pathCertificatoClient, passwordCertificatoClient, 
					X509KeyStorageFlags.MachineKeySet
					| X509KeyStorageFlags.PersistKeySet
					| X509KeyStorageFlags.Exportable);
				svc.ClientCredentials.ClientCertificate.Certificate = certificate;
			}

			return svc;
		}

		private string GetCartCredentials(string username, string password)
		{
			var credentials = username + ":" + password;

			return Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
		}


	}
}
