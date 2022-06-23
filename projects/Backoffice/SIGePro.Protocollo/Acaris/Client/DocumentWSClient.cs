using Init.SIGePro.Protocollo.AcarisDocumentServicePort;
using Init.SIGePro.Protocollo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Client
{
    public class DocumentWSClient
    {
        private string _url;

        public DocumentWSClient(string url)
        {
            this._url = url;
        }

        public DocumentServicePortClient CreaWebService()
        {
            try
            {

                if (String.IsNullOrEmpty(this._url))
                    throw new Exception("Il parametro URL del servizio Document non è valorizzato.");


                var endPointAddress = new EndpointAddress(this._url);
                var binding = new BasicHttpBinding("defaultHttpBinding");
                binding.MessageEncoding = WSMessageEncoding.Mtom;


                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                {
                    binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };
                }

                return new DocumentServicePortClient(binding, endPointAddress);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
