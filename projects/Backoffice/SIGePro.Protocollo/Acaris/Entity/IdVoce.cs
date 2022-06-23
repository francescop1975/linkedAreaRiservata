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
    public class IdVoce : IIDAcaris
    {
        private static class Constants
        {
            public const string CodiceParamName = "codice";
            public const string DbKeyTitolarioParamName = "dbKeyTitolario";
            public const string DbKeyPadreParamName = "dbKeyPadre";
            public const string DbKeyParamName = "dbKey";
        }

        public int Voce { get; }

        public string IdAcaris { get; }

        public int DbKey { get; }

        public IdVoce(IProtocolloSerializer serializer, ObjectWSClient client, RepositoryId repositoryId, PrincipalId principalId, int voce, int? idTitolario = null, int? vocePadre = null)
        {
            try
            {
                this.Voce = voce;

                using (var ws = client.CreaWebService())
                {

                    List<QueryConditionType> criteri = new List<QueryConditionType>();
                    criteri.Add(new QueryConditionType
                    {
                        propertyName = Constants.CodiceParamName,
                        @operator = enumQueryOperator.equals,
                        value = this.Voce.ToString()
                    });

                    if (idTitolario.HasValue)
                    {
                        criteri.Add(new QueryConditionType
                        {
                            propertyName = Constants.DbKeyTitolarioParamName,
                            @operator = enumQueryOperator.equals,
                            value = idTitolario.Value.ToString()
                        });
                    }

                    if (vocePadre.HasValue)
                    {
                        criteri.Add(new QueryConditionType
                        {
                            propertyName = Constants.DbKeyPadreParamName,
                            @operator = enumQueryOperator.equals,
                            value = vocePadre.Value.ToString()
                        });
                    }

                    var request = new query
                    {
                        repositoryId = new ObjectIdType { value = repositoryId.IdAcaris },
                        principalId = new PrincipalIdType { value = principalId.IdAcaris },
                        target = new QueryableObjectType { @object = enumObjectType.VocePropertiesType.ToString() },
                        filter = new PropertyFilterType { filterType = enumPropertyFilter.none },
                        criteria = criteri.ToArray(),
                        maxItems = 1
                    };


                    serializer.Serialize("QueryRequest.xml", request, "Inizio chiamata a query");

                    var response = ws.query(request);

                    serializer.Serialize("QueryResponse.xml", response, "Fine chiamata a query");

                    this.IdAcaris = response.@object.objects[0].objectId.value;
                    this.DbKey = response.@object.objects[0].properties
                                    .Where(x => x.queryName.propertyName == Constants.DbKeyParamName)
                                    .Select(x => Convert.ToInt32(x.value[0]))
                                    .FirstOrDefault();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string ToString()
        {
            return $"IdVoce: [voce={this.Voce} idAcaris={this.IdAcaris} dbKey={this.DbKey}]";
        }
    }
}
