using Init.SIGePro.Protocollo.
    AcarisRepositoryServicePort;
using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Client
{
    public class RepositoryWsClient
    {
        private string _url { get; }

        public RepositoryWsClient(string url)
        {
            this._url = url;
        }

        public RepositoryServicePortClient CreaWebService()
        {
            try
            {
                if (String.IsNullOrEmpty(this._url))
                    throw new Exception("Il parametro URL del servizio Repository non è valorizzato.");


                var endPointAddress = new EndpointAddress(this._url);
                var binding = new BasicHttpBinding("defaultHttpBinding");


                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                {
                    binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };
                }

                return new RepositoryServicePortClient(binding, endPointAddress);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
