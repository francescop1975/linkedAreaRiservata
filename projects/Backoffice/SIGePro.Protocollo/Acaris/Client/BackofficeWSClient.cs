using Init.SIGePro.Protocollo.AcarisBackofficeServicePort;
using Init.SIGePro.Protocollo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Client
{
    public class BackofficeWSClient
    {
        private string _url;

        public BackofficeWSClient(string url)
        {
            this._url = url;
        }

        public BackOfficeServicePortClient CreaWebService()
        {
            try
            {

                if (String.IsNullOrEmpty(this._url))
                    throw new Exception("Il parametro URL del servizio Backoffice non è valorizzato.");


                var endPointAddress = new EndpointAddress(this._url);
                var binding = new BasicHttpBinding("defaultHttpBinding");


                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                {
                    binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };
                }

                return new BackOfficeServicePortClient(binding, endPointAddress);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
