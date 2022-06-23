using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.Acaris.Allegati;
using Init.SIGePro.Protocollo.Acaris.Classificazione;
using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Acaris.Document;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisObjectServicePort;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Metadati;
using Init.SIGePro.Protocollo.Serialize;
using Init.SIGePro.Protocollo.WsDataClass;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Protocollazione.Lettura
{
    public class LeggiProtocolloService
    {
        private readonly IProtocolloSerializer _serializer;
        private readonly ParametriRegoleInfo _configurazione;
        private readonly String _idProtocollo;


        public LeggiProtocolloService(IProtocolloSerializer serializer, ProtocolloBase protocollo, String idProtocollo)
        {
            this._serializer = serializer;
            this._idProtocollo = idProtocollo;
            this._configurazione = this.GetConfigurazione(protocollo);
        }

        internal DatiProtocolloLetto LeggiProtocollo()
        {

            var leggiProtocolloResponse = this.GetProtocolloProperties();

            var leggiProtocolloProperties = leggiProtocolloResponse
                                       .FirstOrDefault()
                                       .properties;


            var idClassificazione = this.ProperyValue(leggiProtocolloProperties, "idClassificazione");
            var classificazioneResponse = new ClassificazioneService(this._serializer, this._configurazione).GetClassificazione(idClassificazione);

            var allegatiResponse = new AllegatiAcarisService(this._serializer, this._configurazione).GetAllegati(idClassificazione);

            return new DatiProtocolloLetto
            {
                Allegati = allegatiResponse.Select(x => new AllOut
                {
                    IDBase = x.Id,
                    Commento = x.NomeFile,
                    Serial = x.NomeFile,
                    //Image = x.Content,
                    ContentType = x.MimeType
                }).ToArray(),
                AnnoNumeroPratica = "",
                AnnoProtocollo = this.ProperyValue(leggiProtocolloProperties, "dataProtocollo").Substring(6),
                Classifica = classificazioneResponse.IndiceClassificazione,
                Classifica_Descrizione = classificazioneResponse.IndiceClassificazioneEstesa,
                DataAnnullamento = "",
                DataInserimento = "",
                DataProtocollo = this.ProperyValue(leggiProtocolloProperties, "dataProtocollo"),
                DataProtocolloMittente = "",
                DocAllegati = "",
                Errore = null,
                IdProtocollo = this._idProtocollo,
                InCaricoA = "",
                InCaricoA_Descrizione = "",
                MittenteInterno = "",
                MittenteInterno_Descrizione = "",
                MittentiDestinatari = null,
                MotivoAnnullamento = "",
                NumeroPratica = "",
                NumeroProtocollo = this.ProperyValue(leggiProtocolloProperties, "codice"),
                NumeroProtocolloMittente = "",
                Oggetto = this.ProperyValue(leggiProtocolloProperties, "oggetto"),
                Origine = "",
                TipoDocumento = "",
                TipoDocumento_Descrizione = "",
                Warning = ""
            };

        }


        public ObjectResponseType[] GetProtocolloProperties() 
        {
            try
            {

                using (var ws = new ObjectWSClient(this._configurazione.ObjectPortUrl).CreaWebService())
                {

                    var request = new getPropertiesMassive
                    {
                        repositoryId = new ObjectIdType { value = this._configurazione.RepositoryID.IdAcaris },
                        principalId = new PrincipalIdType { value = this._configurazione.PrincipalID.IdAcaris },
                        identifiers = new ObjectIdType[]
                        {
                            new ObjectIdType
                            {
                                value = this._idProtocollo
                            }
                        },
                        filter = new PropertyFilterType { filterType = enumPropertyFilter.all }
                    };

                    this._serializer.Serialize("LeggiProtocolloRequest.xml", request, "Inizio chiamata a getPropertiesMassive");

                    var response = ws.getPropertiesMassive(request);

                    this._serializer.Serialize("LeggiProtocolloResponse.xml", response, "Fine chiamata a getPropertiesMassive");


                    return response;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string ProperyValue(PropertyType[] props, string propertyName)
        {
            return props
                    .Where(x => x.queryName.propertyName == propertyName)
                    .Select(y => y.value)
                    .FirstOrDefault()
                    .FirstOrDefault()
                    .ToString();
        }

        private ParametriRegoleInfo GetConfigurazione(ProtocolloBase protocollo) 
        {
            var db = protocollo.DatiProtocollo.Db;
            var idComune = protocollo.DatiProtocollo.IdComune;

            var metadati = new ProtocolloMetadatiMgr(db).GetMetadati(idComune, this._idProtocollo);

            var operatore = protocollo.Operatore;
            var software = protocollo.DatiProtocollo.Software;
            var codiceComune = protocollo.DatiProtocollo.CodiceComune;
            var idAoo = metadati.Where(x => x.Metadato == "IDAOO").Select(x => x.Valore).FirstOrDefault();
            var idStruttura = metadati.Where(x => x.Metadato == "IDSTRUTTURA").Select(x => x.Valore).FirstOrDefault();
            var idNodo = metadati.Where(x => x.Metadato == "IDNODO").Select(x => x.Valore).FirstOrDefault();

            if (String.IsNullOrEmpty(idAoo))
            {
                throw new ArgumentNullException($"Impossibile risalire al parametro AOO associato al protocollo con identificativo {this._idProtocollo}");
            }

            if (String.IsNullOrEmpty(idStruttura))
            {
                throw new ArgumentNullException($"Impossibile risalire al parametro STRUTTURA associato al protocollo con identificativo {this._idProtocollo}");
            }


            if (String.IsNullOrEmpty(idNodo))
            {
                throw new ArgumentNullException($"Impossibile risalire al parametro NODO associato al protocollo con identificativo {this._idProtocollo}");
            }


            return new ParametriRegoleInfoAdapter(this._serializer, operatore, idComune, software, codiceComune, Convert.ToInt32(idAoo), Convert.ToInt32(idStruttura), Convert.ToInt32(idNodo)).Adatta();

        }
    }
}
