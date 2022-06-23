using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.AcarisObjectServicePort;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Classificazione
{
    public class ClassificazioneService
    {
        private readonly IProtocolloSerializer _serializer;
        private readonly ParametriRegoleInfo _configurazione;

        public ClassificazioneService(IProtocolloSerializer serializer, ParametriRegoleInfo configurazione)
        {
            this._serializer = serializer;
            this._configurazione = configurazione;
        }

        public Classificazione GetClassificazione(string idClassificazione) 
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
                                value = idClassificazione
                            }
                       },
                        filter = new PropertyFilterType { filterType = enumPropertyFilter.all }
                    };

                    this._serializer.Serialize("ClassificazioneRequest.xml", request, "Inizio chiamata a getPropertiesMassive");

                    var response = ws.getPropertiesMassive(request);

                    this._serializer.Serialize("ClassificazioneResponse.xml", response, "Fine chiamata a getPropertiesMassive");

                    var properties = response
                                       .FirstOrDefault()
                                       .properties;

                    return new Classificazione
                    {
                        IndiceClassificazione = this.ProperyValue(properties, "indiceClassificazione"),
                        IndiceClassificazioneEstesa = this.ProperyValue(properties, "indiceClassificazioneEstesa")
                    };
                }
            }
            catch (Exception)
            {

                throw;
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
    }
}
