using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.ServiceCreators
{
    public abstract class ServiceCreatorBase<WsClientType> where WsClientType:IDisposable, ICommunicationObject
    {
        private readonly IConfigurazione<ParametriSigeproSecurity> _cfg;
        private readonly ITokenApplicazioneService _tokenApplicazioneService;
        private readonly IAliasResolver _aliasResolver;
        private readonly ILog _log = LogManager.GetLogger(typeof(ServiceCreatorBase<WsClientType>));

        protected ServiceCreatorBase(IConfigurazione<ParametriSigeproSecurity> cfg, ITokenApplicazioneService tokenApplicazioneService, IAliasResolver aliasResolver)
        {
            _cfg = cfg;
            _tokenApplicazioneService = tokenApplicazioneService;
            _aliasResolver = aliasResolver;
        }


        /// <summary>
        /// Nome del binding da utilizzare per istanziare il web service. 
        /// Se non viene overridato nelle classi derivate utilizza areaRiservataServiceBinding
        /// </summary>
        /// <returns></returns>
        protected virtual string GetBindingName() => "areaRiservataServiceBinding";

        /// <summary>
        /// Restituisce l'url del web service che dovrà essere istanziato
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        protected abstract string GetEndpointUrl(ParametriSigeproSecurity config);

        /// <summary>
        /// Istanzia un'implementazione concreta del web service utilizzando l'endpoint e il binding passati
        /// </summary>
        /// <param name="address"></param>
        /// <param name="binding"></param>
        /// <returns></returns>
        protected abstract WsClientType CreateClient(EndpointAddress address, BasicHttpBinding binding);

        /// <summary>
        /// Crea un istanza del web service specificato nel tipo
        /// </summary>
        /// <returns>Istanza del web service e del token applicativo da utilizzare per invocarlo</returns>
        public ServiceInstance<WsClientType> CreateClient()
        {
            var bindingName = GetBindingName();
            var endpointUrl = GetEndpointUrl(this._cfg.Parametri);

            var address = new EndpointAddress(endpointUrl);
            var binding = new BasicHttpBinding(bindingName);

            _log.Debug($"Inizializzazione del web service all'endpoint {endpointUrl} utilizzando il binding {bindingName}");

            if (address.Uri.Scheme.ToUpper() == "HTTPS")
            {
                binding.Security.Mode = BasicHttpSecurityMode.Transport;
            }

            var ws = this.CreateClient(address, binding);
            var token = this._tokenApplicazioneService.GetToken(this._aliasResolver.AliasComune);

            return new ServiceInstance<WsClientType>(ws, token);
        }

        public T Call<T>(Func<WsClientType, string, T> callback)
        {
            return Call((ws) => callback(ws.Service, ws.Token));
        }

        /// <summary>
        /// Istanza un oggetto che rappresenta il web service e utilizza la callback passata per 
        /// invocare un metodo che restituisce risultati
        /// </summary>
        /// <param name="callback">Callback da invocare alla creazione del servizio web referenziato</param>
        public T Call<T>(Func<ServiceInstance<WsClientType>, T> callback)
        {
            using(var ws = CreateClient())
            {
                try
                {
                    return callback(ws);
                }
                catch(Exception ex)
                {
                    this._log.Error($"Errore nella chiamata al web service {typeof(WsClientType)}: {ex}");

                    ws.Service.Abort();

                    throw;
                }
            }
        }

        /// <summary>
        /// Istanza un oggetto che rappresenta il web service e utilizza la callback passata per 
        /// invocare un metodo che non restituisce risultati
        /// </summary>
        /// <param name="callback">Callback da invocare alla creazione del servizio web referenziato</param>
        public void CallVoid(Action<ServiceInstance<WsClientType>> callback)
        {
            using (var ws = CreateClient())
            {
                try
                {
                    callback(ws);
                }
                catch (Exception ex)
                {
                    this._log.Error($"Errore nella chiamata al web service {typeof(WsClientType)}: {ex}");

                    ws.Service.Abort();

                    throw;
                }
            }
        }
    }
}
