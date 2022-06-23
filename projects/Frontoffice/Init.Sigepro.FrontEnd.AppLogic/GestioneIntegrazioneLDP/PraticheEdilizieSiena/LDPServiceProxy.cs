using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GestioneLocalizzazioni;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.Utils.SerializationExtensions;
using System.Net;
using System.Configuration;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneBandiUmbria;
using Init.Sigepro.FrontEnd.Infrastructure.Web;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneIntegrazioneLDP.PresentazionePraticheEdilizieSiena
{
    internal class LDPServiceProxy
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(LDPServiceProxy));
        private readonly Uri _serviceUrl;
        private readonly BasicSoapAuthenticationCredentials _credentials;
        private readonly ILocalizzazioniService _localizzazioniService;
        private readonly Tls12Utils _tls12Utils;

        internal LDPServiceProxy(IConfigurazione<ParametriIntegrazioneLDP> cfg, ILocalizzazioniService localizzazioniService, Tls12Utils tls12Utils)
        {
            this._serviceUrl = new Uri(cfg.Parametri.UrlServizioDomanda);
            this._credentials = new BasicSoapAuthenticationCredentials(cfg.Parametri.ServiceUsername, cfg.Parametri.ServicePassword);
            this._localizzazioniService = localizzazioniService;
            this._tls12Utils = tls12Utils;
        }

        public LocalizzazioneInterventoLDP GetDatiPratica(string identificativoPratica)
        {
            var dati = CallServiceMethod(ws =>
            {
                return ws.getDatiTerritorialiByIdentificativoTemporaneo(new ComplexTypeStringa { testo = identificativoPratica });
            });

            this._log.DebugFormat("Dati della pratica {0}: {1}", identificativoPratica, dati.ToXmlString());

            return new LocalizzazioneInterventoLDP(dati, this._localizzazioniService);
        }

        private R CallServiceMethod<R>(Func<PresentazionePraticheEdilizieSoapClient, R> operation)
        {
            using (var ws = CreateClient())
            {
                try
                {
                    using (var scope = new OperationContextScope(ws.InnerChannel))
                    {
                        this._tls12Utils.ApplicaImpostazioniTls12(ws.Endpoint.Address.Uri.ToString());

                        this._credentials.AggiungiCredenzialiAContextScope();

                        return operation(ws);
                    }
                }
                catch (Exception)
                {
                    ws.Abort();

                    throw;
                }
            }
        }

        

        private PresentazionePraticheEdilizieSoapClient CreateClient()
        {
            var endpoint = new EndpointAddress(this._serviceUrl);

            var binding = new BasicHttpBinding();

            binding.MaxBufferSize = 1024000;
            binding.MaxReceivedMessageSize = 1024000;

            if (this._serviceUrl.Scheme.ToUpper() == "HTTPS")
            {
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
            }

            return new PresentazionePraticheEdilizieSoapClient(binding, endpoint);
        }
    }
}
