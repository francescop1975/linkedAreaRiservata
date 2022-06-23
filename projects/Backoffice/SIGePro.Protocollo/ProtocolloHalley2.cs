using Init.SIGePro.Manager.Verticalizzazioni;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Halley2;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocolloHalley2Service;
using Init.SIGePro.Protocollo.WsDataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo
{
    public class PROTOCOLLO_HALLEY2 : ProtocolloBase
    {
        public PROTOCOLLO_HALLEY2()
        {

        }

        public override DatiProtocolloRes Protocollazione(DatiProtocolloIn protoIn)
        {
            try
            {
                var serializer = new Halle2Serializer(this._protocolloLogs);

                this._protocolloLogs.DebugFormat("Inizio protocollazione");
                serializer.Serialize(ProtocolloLogsConstants.DatiProtocolloInFileName, protoIn);

                var verticalizzazione = new VerticalizzazioneProtocolloHalley2(DatiProtocollo.IdComuneAlias, DatiProtocollo.Software, DatiProtocollo.CodiceComune);

                var parametri = Parametri.FromVerticalizzazione(verticalizzazione);

                var resolver = new Halley2ProtocollazioneResolver(parametri);

                var request = new ProtocolloHalley2Request
                {
                    Segnatura = this.GeneraSegnatura(protoIn, resolver),
                    Allegati = protoIn
                                .Allegati?
                                .Select(x => new Halle2FileRequest
                                {
                                    File = new FileType
                                    {
                                        CidFile = x.OGGETTO,
                                        NomeFile = x.NOMEFILE
                                    }
                                })
                                .ToList()
                };

                //setto principale sul primo allegato
                request.Allegati.First().Principale = true;

                var response = new Halley2ProxyService(serializer, resolver).Protocolla(request);

                return new DatiProtocolloRes
                {
                    AnnoProtocollo =response.Anno,
                    DataProtocollo = DateTime.Now.ToString("dd/MM/yyyy"),
                    Errore = 
                        response.CodErrore == "0" ? 
                            null : 
                            new ErroreProtocollo 
                            { 
                                Descrizione = $"{response.CodErrore}-{response.DesErrore}"
                            },
                    IdProtocollo = null,
                    Messaggio = null,
                    NumeroProtocollo = response.NumeroProtocollo,
                    Warning = null,
                };
            }
            catch (Exception ex)
            {
                this._protocolloLogs.Error(ex);
                throw (ex);
            }
        }

        private byte[] GeneraSegnatura(DatiProtocolloIn protoIn, Halley2ProtocollazioneResolver resolver)
        {
            return new HalleySegnaturaBuilder(this._protocolloSerializer,protoIn, resolver.CasellaEmail).Build();
        }
    }
}
