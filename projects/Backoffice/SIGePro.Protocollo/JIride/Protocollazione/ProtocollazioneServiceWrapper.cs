using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocollazioneJIrideService;
using Init.SIGePro.Protocollo.Serialize;
using Init.SIGePro.Protocollo.Validation;

namespace Init.SIGePro.Protocollo.JIride.Protocollazione
{
    public class ProtocollazioneServiceWrapper : IProtocollazione
    {
        ProtocolloLogs _logs;
        ProtocolloSerializer _serializer;
        string _codiceAmministrazione;
        string _url;
        string _codiceAoo;

        public ProtocollazioneServiceWrapper(string url, ProtocolloLogs logs, ProtocolloSerializer serializer, string codiceAmministrazione, string codiceAoo)
        {
            this._logs = logs;
            this._serializer = serializer;
            this._url = url;
            this._codiceAmministrazione = codiceAmministrazione;
            this._codiceAoo = codiceAoo;
        }

        private ProtocolloSoapClient CreaWebService()
        {
            try
            {
                _logs.Debug("Creazione del webservice di protocollazione J-IRIDE");

                var endPointAddress = new EndpointAddress(_url);
                var binding = new BasicHttpBinding("defaultHttpBinding");

                if (String.IsNullOrEmpty(_url))
                    throw new Exception("IL PARAMETRO URL DELLA VERTICALIZZAZIONE PROTOCOLLO_JIRIDE NON È STATO VALORIZZATO.");

                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                    binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                var ws = new ProtocolloSoapClient(binding, endPointAddress);

                _logs.Debug("Fine creazione del web service di protocollazione J-IRIDE");

                return ws;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("ERRORE DURANTE LA CREAZIONE DEL WEB SERVICE, {0}", ex.Message), ex);
            }
        }

        public ProtocolloOutXml InserisciDocumento(ProtocolloInXml protocolloIn)
        {
            try
            {
                var requestXml = _serializer.Serialize(ProtocolloLogsConstants.InserisciDocumentoRequestFileName, protocolloIn, ProtocolloValidation.TipiValidazione.NO_NAMESPACE);
                _logs.Info("CHIAMATA A INSERISCI DOCUMENTO STRING DI J-IRIDE");

                using (var ws = CreaWebService())
                {
                    var responseXml = ws.InserisciDocumentoEAnagraficheString(requestXml, this._codiceAmministrazione, this._codiceAoo);
                    _logs.InfoFormat("RISPOSTA A INSERISCI DOCUMENTO STRING DI J-IRIDE, {0}", responseXml);
                    _logs.Info("DESERIALIZZAZIONE DELLA RISPOSTA");
                    var response = _serializer.Deserialize<ProtocolloOutXml>(responseXml);
                    _logs.Info("DESERIALIZZAZIONE DELLA RISPOSTA AVVENUTA CON SUCCESSO");

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE GENERATO DURANTE L'INSERIMENTO DEL DOCUMENTO {ex.Message}", ex);
            }
        }

        public ProtocolloOutXml InserisciProtocollo(ProtocolloInXml protocolloIn)
        {
            try
            {
                var requestXml = _serializer.Serialize(ProtocolloLogsConstants.ProtocollazioneRequestFileName, protocolloIn, ProtocolloValidation.TipiValidazione.NO_NAMESPACE);
                _logs.Info("CHIAMATA A INSERISCI PROTOCOLLO STRING DI J-IRIDE");

                using (var ws = CreaWebService())
                {
                    var responseXml = ws.InserisciProtocolloEAnagraficheString(requestXml, this._codiceAmministrazione, this._codiceAoo);
                    _logs.InfoFormat("RISPOSTA A INSERISCI PROTOCOLLO STRING DI J-IRIDE, {0}", responseXml);
                    _logs.Info("DESERIALIZZAZIONE DELLA RISPOSTA");
                    var response = _serializer.Deserialize<ProtocolloOutXml>(responseXml);
                    _logs.Info("DESERIALIZZAZIONE DELLA RISPOSTA AVVENUTA CON SUCCESSO");

                    if (!String.IsNullOrEmpty(response.Errore))
                    {
                        throw new Exception(response.Errore);
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE GENERATO DURANTE L'INSERIMENTO DEL PROTOCOLLO {ex.Message}", ex);
            }
        }

        public string LeggiAnagraficaPerCodiceFiscale(string codiceFiscale, string operatore, string ruolo)
        {
            throw new NotImplementedException();
        }

       
        public bool IsCopia(string idProtocollo)
        {
            if (!String.IsNullOrEmpty(idProtocollo))
            {
                var arrIdProtocollo = idProtocollo.Split('-');
                if (arrIdProtocollo.Length > 1 && arrIdProtocollo[1] == "COPIA")
                {
                    _logs.Info("E' UNA COPIA");
                    return true;
                }
            }

            return false;
        }
    }
}
