using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocollazioneJIrideService;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Protocollazione.CreaCopie
{
    public class CreaCopieWSClient
    {
        private ProtocolloLogs _protocolloLogs;
        private ProtocolloSerializer _protocolloSerializer;
        private string _url;

        public CreaCopieWSClient(ProtocolloLogs protocolloLogs, ProtocolloSerializer protocolloSerializer, string url)
        {
            this._protocolloLogs = protocolloLogs;
            this._protocolloSerializer = protocolloSerializer;

            if (String.IsNullOrEmpty(url))
            {
                throw new Exception("IL PARAMETRO URL DELLA VERTICALIZZAZIONE PROTOCOLLO_JIRIDE NON È STATO VALORIZZATO.");
            }

            this._url = url;
        }

        public CreaCopieOutXml CreaCopieString(string creaCopieRequestXml, string codiceAmministrazione, string codiceAOO)
        {

            using (var ws = CreaWebService())
            {
                var creaCopieOutXml = ws.CreaCopieString(creaCopieRequestXml, codiceAmministrazione, codiceAOO);
                this._protocolloLogs.InfoFormat("RISPOSTA DA CREA COPIE, RESPONSE XML: {0}", creaCopieOutXml);
                var creaCopieOut = this._protocolloSerializer.Deserialize<CreaCopieOutXml>(creaCopieOutXml);
                return creaCopieOut;
            }
        }

        private ProtocolloSoapClient CreaWebService()
        {
            try
            {
                this._protocolloLogs.Debug("Creazione del webservice di protocollazione J-IRIDE");

                var endPointAddress = new EndpointAddress(_url);
                var binding = new BasicHttpBinding("defaultHttpBinding");

                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                    binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                var ws = new ProtocolloSoapClient(binding, endPointAddress);

                this._protocolloLogs.Debug("Fine creazione del web service di protocollazione J-IRIDE");

                return ws;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("ERRORE DURANTE LA CREAZIONE DEL WEB SERVICE, {0}", ex.Message), ex);
            }
        }
    }
}
