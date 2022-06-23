using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Acaris.Protocollazione;
using Init.SIGePro.Protocollo.AcarisBackofficeServicePort;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Init.SIGePro.Protocollo.Acaris.Entity
{
    public class IdNodo : IQueryObject
    {
        public int Id { get; }

        public string IdAcaris { get; }

        public IdNodo(IProtocolloSerializer serializer, BackofficeWSClient client, RepositoryId repositoryId, PrincipalId principalId, int idNodo)
        {
            try
            {
                this.Id = idNodo;

                using (var ws = client.CreaWebService())
                {
                    var request = new query
                    {
                        repositoryId = new ObjectIdType { value = repositoryId.IdAcaris },
                        principalId = new PrincipalIdType { value = principalId.IdAcaris },
                        target = new QueryableObjectType { @object = enumObjectType.NodoPropertiesType.ToString() },
                        filter = new PropertyFilterType { filterType = enumPropertyFilter.none },
                        criteria = new QueryConditionType[]
                        {
                            new QueryConditionType
                            {
                                    propertyName = "dbKey",
                                    @operator = enumQueryOperator.equals,
                                    value = this.Id.ToString()
                            }
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
            return $"IdNodo: [id={this.Id} idAcaris={this.IdAcaris}]";
        }
    }
}