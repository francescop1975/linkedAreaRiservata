using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.Halley2.CustomEncoder;
using Init.SIGePro.Protocollo.ProtocolloHalley2Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Init.SIGePro.Protocollo.Halley2.Client
{
    public class ProtocolloHalley2Client
    {
        private string _url;

        public ProtocolloHalley2Client(string url)
        {
            this._url = url;
        }

        public fileDepotClient CreaWebService()
        {
            try
            {

                if (String.IsNullOrEmpty(this._url))
                    throw new Exception("Il parametro URL del servizio Backoffice non è valorizzato.");


                var endPointAddress = new EndpointAddress(this._url);
                CustomBinding binding;
                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                {
                    binding = new CustomBinding(
                                    new CustomTextMessageBindingElement("iso-8859-1", "text/xml", MessageVersion.Soap11),
                                    new HttpsTransportBindingElement());
                }
                else
                {
                    binding = new CustomBinding(
                                    new CustomTextMessageBindingElement("iso-8859-1", "text/xml", MessageVersion.Soap11),
                                    new HttpTransportBindingElement());
                }

                return new fileDepotClient(binding, endPointAddress);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
