using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Acaris.Protocollazione;
using Init.SIGePro.Protocollo.AcarisBackofficeServicePort;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Entity
{
    public class PrincipalId : IIDAcaris
    {
        public string IdAcaris { get; }

        public PrincipalId(IProtocolloSerializer serializer,  BackofficeWSClient client, RepositoryId repositoryId, string idUtente, int idAoo, int idStruttura, int idNodo, string chiave)
        { 
            try
            {
                using (var ws = client.CreaWebService())
                {
                    var request = new getPrincipalExt
                    {
                        repositoryId = new ObjectIdType { value = repositoryId.IdAcaris },
                        idUtente  = new CodiceFiscaleType { value = idUtente },
                        idAOO = new IdAOOType { value = idAoo },
                        idStruttura = new IdStrutturaType { value = idStruttura },
                        idNodo = new IdNodoType { value = idNodo },
                        clientApplicationInfo = new ClientApplicationInfo { appKey = chiave }
                    };

                    serializer.Serialize("GetPrincipalExtRequest.xml", request, "Inizio chiamata a getPrincipalExt");

                    var response = ws.getPrincipalExt(request);

                    serializer.Serialize("GetPrincipalExtResponse.xml", response, "Fine chiamata a getPrincipalExt");

                    if (response.Length != 1)
                    {
                        throw new InvalidOperationException($"La chiamata al metodo getPrincipalExt ha restituito {response.Length} risultati. Impossibile individuare univocamente la configurazione da utilizzare");
                    }

                    this.IdAcaris = response[0].principalId.value;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore nel tentativo di recuperare il principal id del repository {repositoryId.Nome}", ex);
            }
        }

        public override string ToString()
        {
            return $"PrincipalId: [idAcaris={this.IdAcaris}]";
        }
    }
}
