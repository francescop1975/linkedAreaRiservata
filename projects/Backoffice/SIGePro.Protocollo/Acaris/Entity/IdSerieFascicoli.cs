using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.AcarisObjectServicePort;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Entity
{
    public class IdSerieFascicoli : IIDAcaris
    {
        public string Codice { get; }
        public string IdAcaris { get; }

        private IdVoceComposta _idVoce;

        public IdSerieFascicoli(IProtocolloSerializer serializer, ObjectWSClient client, RepositoryId repositoryId, PrincipalId principalId, string codiceSerie, IdVoceComposta idVoce)
        {
            try
            {
                this.Codice = codiceSerie;
                this._idVoce = idVoce;

                using (var ws = client.CreaWebService()) 
                {
                    var request = new query
                    {
                        repositoryId = new ObjectIdType { value = repositoryId.IdAcaris },
                        principalId = new PrincipalIdType { value = principalId.IdAcaris },
                        target = new QueryableObjectType { @object = enumObjectType.SerieFascicoliPropertiesType.ToString() },
                        filter = new PropertyFilterType { filterType = enumPropertyFilter.none },
                        criteria = new QueryConditionType[]
                        {
                            new QueryConditionType
                            {
                                    propertyName = "codice",
                                    @operator = enumQueryOperator.equals,
                                    value = this.Codice
                            }
                        },
                        navigationLimits = new NavigationConditionInfoType 
                        { 
                            limitToChildren = true,
                            parentNodeId = new ObjectIdType { value = idVoce.IdAcaris }
                        },
                        maxItems = 1
                    };


                    serializer.Serialize("QueryRequest.xml", request, "Inizio chiamata a query");

                    var response = ws.query(request);

                    serializer.Serialize("QueryResponse.xml", response, "Fine chiamata a query");

                    this.IdAcaris = response.@object.objects[0].objectId.value;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string ToString()
        {
            return $"IdSerieFascicoli: [codice={this.Codice} voce={this._idVoce.Voce} idAcaris={this.IdAcaris}]";
        }
    }
}
