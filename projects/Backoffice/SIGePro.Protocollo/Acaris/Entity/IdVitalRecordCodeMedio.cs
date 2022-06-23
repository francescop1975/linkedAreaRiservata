using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.AcarisManagementServicePort;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Entity
{
    public class IdVitalRecordCodeMedio
    {
        public string Descrizione = "MEDIO";
        public int Id { get; }

        public IdVitalRecordCodeMedio(IProtocolloSerializer serializer, ManagementWSClient client, RepositoryId repositoryId )
        {
            try
            {
                using (var ws = client.CreaWebService())
                {

                    var request = new getVitalRecordCode
                    {
                        repositoryId = new ObjectIdType { value = repositoryId.IdAcaris }
                    };


                    serializer.Serialize("GetVitalRecordCodeRequest.xml", request, "Inizio chiamata a getVitalRecordCode");

                    var response = ws.getVitalRecordCode(request);

                    serializer.Serialize("GetVitalRecordCodeResponse.xml", response, "Fine chiamata a getVitalRecordCode");

                    this.Id = response
                                .Where(x => x.descrizione.ToLower() == this.Descrizione.ToLower())
                                .Select(x => Convert.ToInt32(x.idVitalRecordCode.value))
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
            return $"IdVitalRecordCodeMedio: [id={this.Id} Descrizione={this.Descrizione}]";
        }
    }
}
