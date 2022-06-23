using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Configuration;
using Init.SIGePro.Manager.IstanzeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class IstanzeServiceWrapper
    {
        /*
        string _token;

        public IstanzeServiceWrapper(string token)
        {
            this._token = token;
        }


        protected IstanzeClient CreaServizio()
        {

            var endpoint = new EndpointAddress(ParametriConfigurazione.Get.WsIstanzeServiceUrl);

            var binding = new BasicHttpBinding("defaultHttpBinding");

            if (endpoint.Uri.Scheme.ToLower() == "https")
            {
                binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };
            }

            return new IstanzeClient(binding, endpoint);
        }
        */
    }
}
