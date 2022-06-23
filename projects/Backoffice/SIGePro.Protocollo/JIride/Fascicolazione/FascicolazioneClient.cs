using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.FascicolazioneJIrideService;
using Init.SIGePro.Protocollo.JIride.Fascicolazione.Lettura;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using Init.SIGePro.Protocollo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using static Init.SIGePro.Protocollo.Validation.ProtocolloValidation;

namespace Init.SIGePro.Protocollo.JIride.Fascicolazione
{
    public class FascicolazioneClient
    {

        private ProtocolloLogs _protocolloLogs;
        private ProtocolloSerializer _protocolloSerializer;
        private string _url;

        public FascicolazioneClient(ProtocolloLogs protocolloLogs, ProtocolloSerializer protocolloSerializer, string url)
        {
            this._protocolloLogs = protocolloLogs;
            this._protocolloSerializer = protocolloSerializer;
            this._url = url;
        }

        public FascicoloOutXml CreaFascicolo(FascicoloInXml request, string codiceamministrazione, string codiceAoo)
        {
            try
            {
                using (var ws = CreaWebService())
                {

                    this._protocolloLogs.Info("SERIALIZZAZIONE DELL'OGGETTO FASCICOLOIN");
                    var requestXML = this._protocolloSerializer.Serialize(ProtocolloLogsConstants.CreaFascicoloRequestFileName, request, TipiValidazione.NO_NAMESPACE);
                    this._protocolloLogs.InfoFormat("SERIALIZZAZIONE DELL'OGGETTO FASCICOLOIN AVVENUTA CORRETTAMENTE, XML: {0}", requestXML);

                    this._protocolloLogs.Info("CHIAMATA A CREAFASCICOLOSTRING");
                    var response = ws.CreaFascicoloString(requestXML, codiceamministrazione, codiceAoo);
                    this._protocolloLogs.InfoFormat("RISPOSTA DA CREAFASCICOLOSTRING: {0}", response);

                    this._protocolloLogs.Info("DESERIALIZZAZIONE DELLA RISPOSTA DA CREAFASCICOLOSTRING");
                    var fascicoloOut = this._protocolloSerializer.Deserialize<FascicoloOutXml>(response);
                    this._protocolloLogs.Info("DESERIALIZZAZIONE DELLA RISPOSTA DA CREAFASCICOLOSTRING AVVENUTA CON SUCCESSO");

                    if (fascicoloOut.Id == 0 || !String.IsNullOrEmpty(fascicoloOut.Errore))
                    {
                        throw new Exception(fascicoloOut.Errore);
                    }

                    this._protocolloLogs.InfoFormat($"CREAZIONE FASCICOLO AVVENUTA CON SUCCESSO, ID FASCICOLO: {fascicoloOut.Id}, NUMERO FASCICOLO: {fascicoloOut.Numero}, ANNO FASCICOLO: {fascicoloOut.Anno}");

                    return fascicoloOut;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE DURANTE LA CREAZIONE DEL FASCICOLO, {ex.Message}", ex);
            }
        }

        public FascicoloOutXml LeggiFascicolo( LeggiFascicoloWSRequest request, string codiceAmministrazione, string codiceAoo) 
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    string fascicoloOutXml;

                    if (request.Id.HasValue)
                    {
                        this._protocolloLogs.InfoFormat($"CHIAMATA A LEGGI FASCICOLO J_IRIDE, ID: {request.Id}");
                        fascicoloOutXml = ws.LeggiFascicoloString(request.Id.ToString(), "", "", request.Utente, request.Ruolo, codiceAmministrazione, codiceAoo, request.Classifica);
                    }
                    else
                    {
                        this._protocolloLogs.InfoFormat($"CHIAMATA A LEGGI FASCICOLO J_IRIDE, ANNO FASCICOLO: {request.Anno}, NUMERO FASCICOLO: {request.Numero}, UTENTE: {request.Utente}, RUOLO: {request.Ruolo}, CODICE AMMINISTRAZIONE: {codiceAmministrazione}, CODICE AOO: {codiceAoo}, CLASSIFICA: {request.Classifica}");
                        fascicoloOutXml = ws.LeggiFascicoloString("", request.Anno, request.Numero, request.Utente, request.Ruolo, codiceAmministrazione, codiceAoo, request.Classifica);
                    }

                    this._protocolloLogs.Info("DESERIALIZZAZIONE DELLA RISPOSTA DA LEGGIFASCICOLOSTRING");
                    var fascicoloOut = this._protocolloSerializer.Deserialize<FascicoloOutXml>(fascicoloOutXml);
                    this._protocolloLogs.Info("DESERIALIZZAZIONE DELLA RISPOSTA DA LEGGIFASCICOLOSTRING AVVENUTA CON SUCCESSO");

                    this._protocolloSerializer.Serialize(ProtocolloLogsConstants.LeggiFascicoloResponseFileName, fascicoloOut);

                    if (fascicoloOut.Id == 0 || !String.IsNullOrEmpty(fascicoloOut.Errore))
                    {
                        throw new Exception(fascicoloOut.Errore);
                    }

                    this._protocolloLogs.InfoFormat("CHIAMATA A LEGGIFASCICOLOSTRING AVVENUTA CORRETTAMENTE, ID FASCICOLO: {0}", fascicoloOut.Id);

                    return fascicoloOut;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE GENERATO DURANTE LA LETTURA DEL FASCICOLO ID: {request.Id}, NUMERO: {request.Numero}, ANNO: {request.Anno}, {ex.Message}", ex);
            }
        }
        private DocWSFascicoliSoapClient CreaWebService()
        {

            try
            {
                if (String.IsNullOrEmpty(this._url))
                    throw new Exception("IL PARAMETRO URL_FASC DELLA VERTICALIZZAZIONE PROTOCOLLO_JIRIDE NON È STATO VALORIZZATO.");

                this._protocolloLogs.Debug("Creazione del webservice di fascicolazione J-IRIDE");

                var endPointAddress = new EndpointAddress(this._url);
                var binding = new BasicHttpBinding("defaultHttpBinding");


                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                    binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };

                var ws = new DocWSFascicoliSoapClient(binding, endPointAddress);

                this._protocolloLogs.Debug("Fine creazione del web service di fascicolazione J-IRIDE");

                return ws;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("ERRORE DURANTE LA CREAZIONE DEL WEB SERVICE, {0}", ex.Message), ex);
            }
        }
    }
}
