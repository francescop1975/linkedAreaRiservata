using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Manager.Utils
{
    public abstract class WebServiceProxyBase<TService> where TService: IDisposable, ICommunicationObject
    {
        ILog _log = LogManager.GetLogger(typeof(WebServiceProxyBase<TService>));

        protected abstract string WebServiceUrl { get; }
        protected virtual string BindingName  => "defaultHttpBinding";
        protected abstract TService CreateService(BasicHttpBinding binding, EndpointAddress endpoint);

        protected TReturned CallService<TReturned>(Func<TService, TReturned> callback)
        {
            var serviceUrl = this.WebServiceUrl;
            var bindingName = this.BindingName;
            /*
            if (!String.IsNullOrEmpty(this._overrideWebServiceUrl))
            {
                serviceUrl = this._overrideWebServiceUrl;
            }
            */
            var endpoint = new EndpointAddress(serviceUrl);
            var binding = new BasicHttpBinding(bindingName);

            if (string.Equals(serviceUrl.Substring(0, 5), "https", StringComparison.CurrentCultureIgnoreCase))
            {
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
            }

            using (var ws = CreateService(binding, endpoint))
            {
                try
                {
                    return callback(ws);
                }
                catch (Exception ex)
                {
                    _log.Error($"Errore durante la creazione del web service con url {serviceUrl} e binding {bindingName}: {ex}");

                    ws.Abort();

                    throw;
                }
            }
        }
    }
}
