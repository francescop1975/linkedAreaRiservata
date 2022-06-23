using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.MailTipoService;
using PersonalLib2.Data;
using Init.SIGePro.Protocollo.Serialize;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using Init.SIGePro.Manager;

namespace Init.SIGePro.Protocollo.ProtocolloServices.MailTipo
{
    public class MailTipoIstanzaService : MailTipoService
    {
        ResolveDatiProtocollazioneService _datiProtocollazioneService;
        IParametriInterventoProtocolloService _param;
        ProtocolloSerializer _serializer;

        public MailTipoIstanzaService(IParametriInterventoProtocolloService param, ResolveDatiProtocollazioneService datiProtocollazioneService, ProtocolloLogs log, ProtocolloSerializer serializer) :
            base(log)
        {
            _datiProtocollazioneService = datiProtocollazioneService;
            _param = param;
            _serializer = serializer;

        }

        public override MailTipoType GetMailTipo()
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    var oggetto = "";
                    var corpo = "";

                    string retVal = string.Empty;
                    var request = new MailtipoRequest();
                    request.token = _datiProtocollazioneService.Db.ConnectionDetails.Token;
                    Logger.DebugFormat("Recupero Oggetto da mail e testi tipo, funzionalità GetOggetto, codice testo tipo restituito: {0}", _param.CodiceTestoTipo.HasValue ? _param.CodiceTestoTipo.Value.ToString() : "");

                    if (!_param.CodiceTestoTipo.HasValue)
                    {
                        Logger.WarnFormat("L'intervento e i suoi padri non hanno selezionato il codice e testo tipo, non è quindi possibile recuperare un valore per l'oggetto ed il corpo, sarà quindi inserito un valore di default, ma solo per l'oggetto e non per il corpo");
                        oggetto = string.Concat(_param.OggettoDefault, _datiProtocollazioneService.NumeroIstanza);
                    }
                    else
                    {
                        request.codicemailtipo = _param.CodiceTestoTipo.Value;
                        request.codiceistanza = Convert.ToInt32(_datiProtocollazioneService.CodiceIstanza);

                        _serializer.Serialize(ProtocolloLogsConstants.MailTipoProtocolloSoapRequestFileName, request);

                        Logger.InfoFormat("Chiamata a mail e testo tipo, vedi request su file {0}", ProtocolloLogsConstants.MailTipoProtocolloSoapResponseFileName);
                        var response = ws.Mailtipo(request);

                        _serializer.Serialize(ProtocolloLogsConstants.MailTipoProtocolloSoapResponseFileName, response);

                        if (response != null)
                        {
                            oggetto = response.oggetto;
                            corpo = response.corpo;
                        }
                    }

                    return new MailTipoType
                    {
                        Oggetto = oggetto,
                        Corpo = corpo
                    };
                }
            }
            catch (Exception ex)
            {
                throw Logger.LogErrorException("ERRORE GENERATO DURANTE IL RECUPERO DELL'OGGETTO DALLE MAIL E TESTI TIPO DA ISTANZA", ex);
            }

        }
    }
}
