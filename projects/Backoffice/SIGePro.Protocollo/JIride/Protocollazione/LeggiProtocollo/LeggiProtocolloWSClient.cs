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

namespace Init.SIGePro.Protocollo.JIride.Protocollazione.LeggiProtocollo
{
    public class LeggiProtocolloWSClient
    {
        private ProtocolloLogs _protocolloLogs;
        private ProtocolloSerializer _protocolloSerializer;
        private string _url;

        public LeggiProtocolloWSClient(ProtocolloLogs protocolloLogs, ProtocolloSerializer protocolloSerializer, string url)
        {
            this._protocolloLogs = protocolloLogs;
            this._protocolloSerializer = protocolloSerializer;

            if (String.IsNullOrEmpty(url))
            {
                throw new Exception("IL PARAMETRO URL DELLA VERTICALIZZAZIONE PROTOCOLLO_JIRIDE NON È STATO VALORIZZATO.");
            }

            this._url = url;
        }

        public DocumentoOutXml LeggiProtocollo(short annoProtocollo, int numeroProtocollo, string operatore, string ruolo, string codiceAmministrazione, string codiceAoo)
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    this._protocolloLogs.InfoFormat($"CHIAMATA A LEGGI PROTOCOLLO STRING DI J-IRIDE, ANNO PROTOCOLLO: {annoProtocollo}, NUMERO PROTOCOLLO: {numeroProtocollo}, OPERATORE: {operatore}, RUOLO: {ruolo}, CODICE AMMINISTRAZIONE: {codiceAmministrazione}");
                    var responseXml = ws.LeggiProtocolloString(annoProtocollo, numeroProtocollo, operatore, ruolo, codiceAmministrazione, codiceAoo, "");

                    if (this._protocolloLogs.IsDebugEnabled)
                    {
                        this._protocolloLogs.InfoFormat($"RISPOSTA A LEGGI PROTOCOLLO STRING DI J-IRIDE SALVATA IN : {ProtocolloLogsConstants.LeggiProtocolloResponseFileName}");
                        this._protocolloSerializer.Serialize(ProtocolloLogsConstants.LeggiProtocolloResponseFileName, responseXml);
                    }

                    this._protocolloLogs.Info("DESERIALIZZAZIONE DELLA RISPOSTA A LEGGI PROTOCOLLO STRING DI J-IRIDE");
                    var response = this._protocolloSerializer.Deserialize<DocumentoOutXml>(responseXml);
                    this._protocolloLogs.Info("DESERIALIZZAZIONE DELLA RISPOSTA A LEGGI PROTOCOLLO STRING AVVENUTA CON SUCCESSO");

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE DURANTE LA LETTURA DEL PROTOCOLLO NUMERO {numeroProtocollo}, ANNO {annoProtocollo}, {ex.Message}", ex);
            }
        }

        public DocumentoOutXml LeggiDocumento(int idProtocollo, string operatore, string ruolo, string codiceAmministrazione, string codiceAoo)
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    this._protocolloLogs.InfoFormat($"CHIAMATA A LEGGI DOCUMENTO STRING DI J-IRIDE, IDPROTOCOLLO: {idProtocollo}, OPERATORE: {operatore}, RUOLO: {ruolo}, CODICE AMMINISTRAZIONE: {codiceAmministrazione}");
                    var responseXml = ws.LeggiDocumentoString(idProtocollo, operatore, ruolo, codiceAmministrazione, codiceAoo);
                    this._protocolloLogs.Info("DESERIALIZZAZIONE DELLA RISPOSTA A LEGGI DOCUMENTO STRING DI J-IRIDE");
                    var response = this._protocolloSerializer.Deserialize<DocumentoOutXml>(responseXml);
                    this._protocolloLogs.Info("DESERIALIZZAZIONE DELLA RISPOSTA A LEGGI DOCUMENTO STRING AVVENUTA CON SUCCESSO");

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE DURANTE LA CHIAMATA A LEGGI DOCUMENTO, {ex.Message}", ex);
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
