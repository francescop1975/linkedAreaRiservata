using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.AcarisOfficialBookServicePort;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Metadati;
using Init.SIGePro.Protocollo.Serialize;
using Init.SIGePro.Protocollo.WsDataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Protocollazione
{
    public class ProtocollazioneService
    {
        private IProtocollazioneResolver _resolver;
        private IProtocolloSerializer _serializer;
        private ProtocolloLogs _logger;
        private OfficialBookWSClient _officialBookClient;

        public ProtocollazioneService(IProtocolloSerializer serializer, ProtocolloLogs logger, IProtocollazioneResolver resolver)
        {
            this._serializer = serializer;
            this._logger = logger;
            this._resolver = resolver;

            this._officialBookClient = new OfficialBookWSClient(this._resolver.OfficialBookPortUrl);
        }

        internal creaRegistrazioneResponse Protocolla()
        {
            try
            {
                using (var ws = this._officialBookClient.CreaWebService())
                {

                    var request = new creaRegistrazione
                    {
                        repositoryId = new ObjectIdType { value = this._resolver.RepositoryId.IdAcaris },
                        principalId = new PrincipalIdType { value = this._resolver.PrincipalId.IdAcaris },
                        tipologiaCreazione = this._resolver.TipologiaCreazione,
                        infoRichiestaCreazione = this._resolver.InfoRichiestaCreazione
                    };


                    this._serializer.Serialize("CreaRegistrazioneRequest.xml", request, "Inizio chiamata a creaRegistrazione");

                    var response = ws.creaRegistrazione(request);

                    this._serializer.Serialize("CreaRegistrazioneResponse.xml", response, "Fine chiamata a creaRegistrazione");

                    return response;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"CHIAMATA FALLITA, {ex.Message}", ex);
            }

        }

        internal DatiProtocolloRes CreaRegistrazioneResponseToDatiProtocolloRes(creaRegistrazioneResponse response)
        {
            return new DatiProtocolloRes
            {
                AnnoProtocollo = DateTime.ParseExact(response.identificazioneCreazione.dataUltimoAggiornamento.value, "dd/MM/yyyy", null).Year.ToString(),
                DataProtocollo = response.identificazioneCreazione.dataUltimoAggiornamento.value,
                Errore = null,
                IdProtocollo = response.identificazioneCreazione.registrazioneId.value,
                Messaggio = null,
                NumeroProtocollo = response.identificazioneCreazione.numero,
                Warning = null,
                Metadati = new List<ProtocolloMetadati> 
                {
                    new ProtocolloMetadati
                    {
                        Metadato = "IDAOO",
                        Valore = this._resolver.IdAoo.Id.ToString()
                    },
                    new ProtocolloMetadati
                    {
                        Metadato = "IDNODO",
                        Valore = this._resolver.IdNodo.Id.ToString()
                    },
                    new ProtocolloMetadati
                    {
                        Metadato = "IDSTRUTTURA",
                        Valore = this._resolver.IdStruttura.Id.ToString()
                    },
                }
            };
        }
    }
}
