using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.AcarisRepositoryServicePort;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Entity
{
    public class RepositoryId : IIDAcaris
    {
        public string Nome { get; }
         
        public string IdAcaris { get; }

        public RepositoryId(IProtocolloSerializer serializer, RepositoryWsClient client, string nome )
        {
            try
            {
                using (var ws = client.CreaWebService())
                {
                    var request = new getRepositories();


                    serializer.Serialize("GetRepositoriesRequest.xml", request, "Inizio chiamata a getRepositories");

                    var response = ws.getRepositories(request);

                    serializer.Serialize("GetRepositoriesResponse.xml", response, "Fine chiamata a getRepositories");


                    var repo = response
                                .Where(x => x.repositoryName.Trim().ToUpper() == nome.Trim().ToUpper())
                                .FirstOrDefault();

                    this.IdAcaris = repo.repositoryId.value;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore nel tentativo di recuperare l'id del repository {nome}",ex);
            }
        }

        public override string ToString()
        {
            return $"RepositoryId: [Nome={this.Nome} IdAcaris={this.IdAcaris}]";
        }
    }
}
