using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.JIride.Protocollazione;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocollazioneJIrideService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Storico
{
    public class LeggiProtocolloStoricoService
    {
        private ParametriStorici _parametri;

        public LeggiProtocolloStoricoService(ParametriStorici parametri)
        {
            this._parametri = parametri;
        }

        public DocumentoOutXml LeggiProtocollo()
        {
            DocumentoOutXml response;
            if (this._parametri.IdProtocollo.HasValue)
            {
                response = this.LeggiProtocolloDaId();
            }
            else
            {
                response = this.LeggiProtocolloDaRiferimenti();
            }

            return response;
        }

        private DocumentoOutXml LeggiProtocolloDaRiferimenti()
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    this._parametri.Logger.InfoFormat("CHIAMATA A LEGGI PROTOCOLLO STRING DI J-IRIDE, ANNO PROTOCOLLO: {0}, NUMERO PROTOCOLLO: {1}, OPERATORE: {2}, RUOLO: {3}, CODICE AMMINISTRAZIONE: {4}", this._parametri.AnnoProtocollo, this._parametri.NumeroProtocollo, this._parametri.Operatore, this._parametri.Ruolo, this._parametri.CodiceAmministrazione);
                    var responseXml = ws.LeggiProtocolloString(this._parametri.AnnoProtocollo, this._parametri.NumeroProtocollo, this._parametri.Operatore, this._parametri.Ruolo, this._parametri.CodiceAmministrazione, this._parametri.CodiceAOO, "");

                    if (this._parametri.Logger.IsDebugEnabled)
                    {
                        this._parametri.Logger.InfoFormat("RISPOSTA A LEGGI PROTOCOLLO STRING DI J-IRIDE SALVATA IN : {0}", ProtocolloLogsConstants.LeggiProtocolloResponseFileName);
                        this._parametri.Serializer.Serialize(ProtocolloLogsConstants.LeggiProtocolloResponseFileName, responseXml);
                    }

                    this._parametri.Logger.Info("DESERIALIZZAZIONE DELLA RISPOSTA A LEGGI PROTOCOLLO STRING DI J-IRIDE");
                    var response = this._parametri.Serializer.Deserialize<DocumentoOutXml>(responseXml);
                    this._parametri.Logger.Info("DESERIALIZZAZIONE DELLA RISPOSTA A LEGGI PROTOCOLLO STRING AVVENUTA CON SUCCESSO");

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE DURANTE LA LETTURA DEL PROTOCOLLO NUMERO {this._parametri.NumeroProtocollo}, ANNO {this._parametri.AnnoProtocollo}, {ex.Message}", ex);
            }
        }

        private DocumentoOutXml LeggiProtocolloDaId()
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    this._parametri.Logger.InfoFormat("CHIAMATA A LEGGI DOCUMENTO STRING DI J-IRIDE, IDPROTOCOLLO: {0}, OPERATORE: {1}, RUOLO: {2}, CODICE AMMINISTRAZIONE: {3}", this._parametri.IdProtocollo, this._parametri.Operatore, this._parametri.Ruolo, this._parametri.CodiceAmministrazione);
                    var responseXml = ws.LeggiDocumentoString(this._parametri.IdProtocollo.Value, this._parametri.Operatore, this._parametri.Ruolo, this._parametri.CodiceAmministrazione, this._parametri.CodiceAOO);
                    this._parametri.Logger.Info("DESERIALIZZAZIONE DELLA RISPOSTA A LEGGI DOCUMENTO STRING DI J-IRIDE");
                    var response = this._parametri.Serializer.Deserialize<DocumentoOutXml>(responseXml);
                    this._parametri.Logger.Info("DESERIALIZZAZIONE DELLA RISPOSTA A LEGGI DOCUMENTO STRING AVVENUTA CON SUCCESSO");

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
                this._parametri.Logger.Debug("Creazione del webservice di protocollazione J-IRIDE");

                if (String.IsNullOrEmpty(this._parametri.Url))
                {
                    throw new Exception("IL PARAMETRO URL DELLA VERTICALIZZAZIONE PROTOCOLLO_JIRIDE NON È STATO VALORIZZATO.");
                }

                var endPointAddress = new EndpointAddress(this._parametri.Url);
                var binding = new BasicHttpBinding("defaultHttpBinding");


                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                {
                    binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };
                }

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                var ws = new ProtocolloSoapClient(binding, endPointAddress);

                this._parametri.Logger.Debug("Fine creazione del web service di protocollazione J-IRIDE");

                return ws;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("ERRORE DURANTE LA CREAZIONE DEL WEB SERVICE, {0}", ex.Message), ex);
            }
        }
    }
}
