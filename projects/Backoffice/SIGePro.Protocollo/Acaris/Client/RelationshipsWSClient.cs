using Init.SIGePro.Protocollo.AcarisRelationshipsServicePort;
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
    public class RelationshipsWSClient
    {
        private string _url { get; }

        public RelationshipsWSClient(string url)
        {
            this._url = url;
        }

        public RelationshipsServicePortClient CreaWebService()
        {
            try
            {
                if (String.IsNullOrEmpty(this._url))
                    throw new Exception("Il parametro URL del servizio Relationships non è valorizzato.");


                var endPointAddress = new EndpointAddress(this._url);
                var binding = new BasicHttpBinding("defaultHttpBinding");


                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                {
                    binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };
                }

                return new RelationshipsServicePortClient(binding, endPointAddress);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
