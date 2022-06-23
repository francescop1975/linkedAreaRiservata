using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System.Net;
using Init.SIGePro.Protocollo.SidUmbria.Protocollazione;

namespace Init.SIGePro.Protocollo.SidUmbria
{
    public class ProtocolloService
    {
        VerticalizzazioniConfiguration _vert;
        ProtocolloLogs _logs;
        ProtocolloSerializer _serializer;
        authToken _token;

        public ProtocolloService(VerticalizzazioniConfiguration vert, string uo, string ruolo, ProtocolloLogs logs, ProtocolloSerializer serializer)
        {
            _vert = vert;
            _logs = logs;
            _serializer = serializer;

            _token = new authToken { token = uo, service = ruolo };
        }

        private WSProtocollazioneSEIClient CreaWebService()
        {
            try
            {
                if (String.IsNullOrEmpty(_vert.Url))
                    throw new Exception("IL PARAMETRO URL DELLA VERTICALIZZAZIONE PROTOCOLLO_SIDUMBRIA NON È STATO VALORIZZATO, NON È POSSIBILE CONTATTARE IL WEB SERVICE");

                var endPointAddress = new EndpointAddress(_vert.Url);
                var binding = new BasicHttpBinding("defaultHttpBinding");

                var ws = new WSProtocollazioneSEIClient(binding, endPointAddress);
                return ws;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE AVVENUTO DURANTE LA CREAZIONE DEL WEB SERVICE DI PROTOCOLLAZIONE, ERRORE: {ex.Message}", ex);
            }
        }

        public identificatore LeggiEstremi(string idRichiesta)
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    _logs.Info($"RICHIESTA DEGLI ESTREMI DEL DOCUMENTO {idRichiesta}");
                    var response = ws.getEstremiRichiesta(_token, idRichiesta);
                    _logs.Info("RISPOSTA OTTENUTA DA getEstremiRichiesta");

                    if (response == null)
                    {
                        _logs.Info($"LA CHIAMATA A getEstremiRichiesta della richiesta {idRichiesta} NON CONTIENE ALCUN VALORE (VALORE NULL)");
                        return null;
                    }

                    _serializer.Serialize(ProtocolloLogsConstants.ProtocollazioneResponseFileName, response);

                    if (response.elCode == ProtocolloSidUmbriaConstants.MessaggioErroreOK04Coda)
                        return null;

                    if (response.esito == ProtocolloSidUmbriaConstants.EsitoKo)
                        throw new Exception($"ERRORE RESTITUITO DAL WEB SERVICE DI PROTOCOLLAZIONE DURANTE IL RECUPERO DEGLI ESTREMI DEL PROTOCOLLO, CODICE ERRORE: {response.elCode}, DESCRIZIONE ERRORE: {response.elMessage}");

                    if (response.identificatore == null)
                        throw new Exception($"NON E' POSSIBILE RECUPERARE I DATI DALL'IDENTIFICATORE, CODICE MESSAGGIO RICEVUTO DAL WEB SERVICE: {response.elCode}, MESSAGGIO: {response.elMessage}");

                    _logs.Info($"ESTREMI DEL DOCUMENTO {idRichiesta} RECUPERATI CORRETTAMENTE, NUMERO {response.identificatore.numero}, ANNO {response.identificatore.anno}, DATA: {response.identificatore.data}");

                    return response.identificatore;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE GENERATO DURANTE LA CHIAMATA AL WEB SERVICE, ERRORE: {ex.Message}", ex);
            }
        }

        public esitoElaborazione Protocolla(infoProtocollo request)
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    _serializer.Serialize(ProtocolloLogsConstants.ProtocollazioneRequestFileName, request);
                    _logs.InfoFormat("CHIAMATA A PROTOCOLLAZIONE, TOKEN: {0}, SERVICE: {1}", _token.token, _token.service);
                    var response = ws.protocollazioneDocumento(_token, request);

                    if (response.esito == ProtocolloSidUmbriaConstants.EsitoKo)
                        throw new Exception($"ERRORE RESTITUITO DAL WEB SERVICE DI PROTOCOLLAZIONE, CODICE ERRORE: {response.elCode}, DESCRIZIONE ERRORE: {response.elMessage}");

                    if (response.identificatore != null)
                        _logs.Info($"DATI IDENTIFICATORE: NUMERO {response.identificatore.numero}, ANNO {response.identificatore.anno}, DATA: {response.identificatore.data}, CODICE ERRORE: {response.elCode}");

                    _logs.Info("PROTOCOLLAZIONE AVVENUTA CORRETTAMENTE IN ATTESA DELLA RESTITUZIONE TRAMITE METODO GETESTREMIDOCUMENTO");

                    return response;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"ERRORE GENERATO DURANTE LA CHIAMATA AL WEB SERVICE DI PROTOCOLLAZIONE, ERRORE: {ex.Message}", ex);
            }
        }
    }
}
