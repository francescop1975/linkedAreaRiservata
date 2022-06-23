using Init.SIGePro.Manager.Configuration;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.MailTipoService;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.ProtocolloServices.MailTipo
{
    public abstract class MailTipoService : IMailTipoService
    {
        private static class Constants 
        {
            public const string UriMailTipoWs = "services/mailtipo?wsdl";
            public const string Binding = "MailTipoServiceBinding";
        }


        public ProtocolloLogs Logger { get; }
        public abstract MailTipoType GetMailTipo();

        public MailTipoService(ProtocolloLogs logger)
        {
            this.Logger = logger;
        }

        /*
        public MailTipoService(ProtocolloLogs logs, int codiceMailTipo, string token)
        {
            try
            {
                using (var ws = this.CreaWebService())
                {
                    var request = new MailtipoRequest
                    {
                        token = token,
                        codicemailtipo = codiceMailTipo
                    };

                    var response = ws.Mailtipo(request);

                    if (response == null)
                    {
                        throw new Exception("IL SERVIZIO HA RESTITUITO DEI VALORI NULL");
                    }

                    this.Oggetto = response.oggetto;
                    this.Corpo = response.corpo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE GENERATO DURANTE LA RICHIESTA DELLA MAIL TIPO, {ex.Message}", ex);
            }
        }
        */
        protected MailtipoClient CreaWebService()
        {
            try
            {
                var uri = string.Concat(ParametriConfigurazione.Get.WsHostUrlJava, Constants.UriMailTipoWs);
                var binding = new BasicHttpBinding(Constants.Binding);
                var remoteAddress = new EndpointAddress(uri);

                return new MailtipoClient(binding, remoteAddress);
            }
            catch (Exception ex)
            {
                throw Logger.LogErrorException("ERRORE VERIFICATO DURANTE LA CREAZIONE DEL CLIENT DI CONNESSIONE AL WEB SERVICE PER IL RECUPERO DEI DATI DELL'OGGETTO DA MAIL E TESTI TIPO", ex);
            }
        }
    }
}
