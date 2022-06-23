using Init.SIGePro.Protocollo.AcarisObjectServicePort;
using Init.SIGePro.Protocollo.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Client
{
    public class ObjectWSClient
    {
        public string _url { get; }

        public ObjectWSClient(string url)
        {
            this._url = url;
        }

        public ObjectServicePortClient CreaWebService() 
        {
            try
            {
                if (String.IsNullOrEmpty(this._url))
                    throw new Exception("Il parametro URL del servizio Object non è valorizzato.");


                var endPointAddress = new EndpointAddress(this._url);
                var binding = new BasicHttpBinding("defaultHttpBinding");
                binding.MessageEncoding = WSMessageEncoding.Mtom;
                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                {
                    binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };
                }

                return new ObjectServicePortClient(binding, endPointAddress);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
