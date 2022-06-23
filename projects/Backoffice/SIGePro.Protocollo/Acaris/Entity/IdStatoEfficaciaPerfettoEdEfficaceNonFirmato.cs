﻿using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.AcarisObjectServicePort;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Entity
{
    public class IdStatoEfficaciaPerfettoEdEfficaceNonFirmato: IIDAcaris
    {
        public string Descrizione = "PERFETTO ED EFFICACE MA NON FIRMATO";

        public string IdAcaris { get; }

        public int DbKey { get; }

        public IdStatoEfficaciaPerfettoEdEfficaceNonFirmato(IProtocolloSerializer serializer, ObjectWSClient client, RepositoryId repositoryId, PrincipalId principalId)
        {
            using (var ws = client.CreaWebService())
            {
                var request = new query
                {
                    repositoryId = new ObjectIdType { value = repositoryId.IdAcaris },
                    principalId = new PrincipalIdType { value = principalId.IdAcaris },
                    target = new QueryableObjectType { @object = "StatoDiEfficaciaDecodifica" },
                    filter = new PropertyFilterType
                    {
                        filterType = enumPropertyFilter.list,
                        propertyList = new QueryNameType[]
                        {
                            new QueryNameType
                            { 
                                className = "StatoDiEfficaciaDecodifica",
                                propertyName = "dbKey"
                            }
                        }
                    },
                    criteria = new QueryConditionType[]
                    { 
                        new QueryConditionType
                        { 
                            propertyName = "descrizione",
                            @operator = enumQueryOperator.equals,
                            value = this.Descrizione
                        }
                    }
                };

                serializer.Serialize("QueryRequest.xml", request, "Inizio chiamata a query");

                var response = ws.query(request);

                serializer.Serialize("QueryResponse.xml", response, "Fine chiamata a query");

                this.IdAcaris = response.@object.objects[0].objectId.value;
                this.DbKey = response.@object.objects[0].properties
                .Where(x => x.queryName.propertyName == "dbKey")
                .Select(x => Convert.ToInt32(x.value[0]))
                .FirstOrDefault();
            }
        }

        public override string ToString()
        {
            return $"IdStatoEfficaciaPerfettoEdEfficaceNonFirmato: [Descrizione={this.Descrizione} DbKey={this.DbKey} IdAcaris={this.IdAcaris}]";
        }
    }
}
