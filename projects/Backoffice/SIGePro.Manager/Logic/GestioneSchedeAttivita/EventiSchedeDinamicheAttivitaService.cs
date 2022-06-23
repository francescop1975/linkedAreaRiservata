﻿// -----------------------------------------------------------------------
// <copyright file="EventiSchedeDinamicheAttivitaService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.SIGePro.Manager.Logic.GestioneSchedeAttivita
{
    using Init.SIGePro.Authentication;
    using Init.SIGePro.Manager.Logic.GestioneSchedeAttivita.Eventi;
    using log4net;
    using Vbg.EventBus.Abstractions;
    using Init.SIGePro.Manager.Configuration;
    using Init.SIGePro.Manager.EventiAttivitaWebService;
    using System.ServiceModel;
    using System;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EventiSchedeDinamicheAttivitaService :
        IEventSubscriber<SchedaDinamicaAggiuntaAdAttivita>,
        IEventSubscriber<SchedaDinamicaAttivitaSalvata>,
        IEventSubscriber<SchedaDinamicaIstanzaEliminata>,
        IEventSubscriber<SchedaDinamicaIstanzaSalvata>,
        IEventSubscriber<SchedaDinamicaRimossaDaAttivita>
    {
        private readonly AuthenticationInfo _authInfo;
        private readonly ILog _log = LogManager.GetLogger(typeof(EventiSchedeDinamicheAttivitaService));

        private static class Constants
        {
            public const string BindingName = "AttivitaService";
        }

        public EventiSchedeDinamicheAttivitaService(AuthenticationInfo authInfo)
        {
            _authInfo = authInfo;
         }


        public void OnEvento(SchedaDinamicaAggiuntaAdAttivita e)
        {
            CallService(ws =>
            {
                var request = new schedaDinamicaAggiuntaAdAttivitaRequest
                {
                    codiceAttivita = e.IdAttivita,
                    codiceScheda = e.IdScheda,
                    token = this._authInfo.Token
                };

                this._log.Debug($"Invocazione di schedaDinamicaAggiuntaAdAttivita con i parametri { JsonConvert.SerializeObject(request)}");

                var result = ws.schedaDinamicaAggiuntaAdAttivita(request);

                this._log.Debug($"schedaDinamicaAggiuntaAdAttivita ha restituito { JsonConvert.SerializeObject(result)}");

                return result;
            });
        }

        public void OnEvento(SchedaDinamicaAttivitaSalvata e)
        {
            CallService(ws =>
            {
                var request = new schedaDinamicaAttivitaSalvataRequest
                {
                    codiceAttivita = e.IdAttivita,
                    codiceScheda = e.IdScheda,
                    token = this._authInfo.Token
                };

                this._log.Debug($"Invocazione di schedaDinamicaAttivitaSalvata con i parametri { JsonConvert.SerializeObject(request)}");

                var result = ws.schedaDinamicaAttivitaSalvata(request);

                this._log.Debug($"schedaDinamicaAttivitaSalvata ha restituito { JsonConvert.SerializeObject(result)}");

                return result;
            });
        }

        public void OnEvento(SchedaDinamicaIstanzaEliminata e)
        {
            CallService(ws =>
            {
                var request = new schedaDinamicaIstanzaEliminataRequest
                {
                    codiceIstanza = e.CodiceIstanza,
                    codiceScheda = e.IdScheda,
                    token = this._authInfo.Token
                };

                this._log.Debug($"Invocazione di schedaDinamicaIstanzaEliminata con i parametri { JsonConvert.SerializeObject(request)}");

                var result = ws.schedaDinamicaIstanzaEliminata(request);

                this._log.Debug($"schedaDinamicaIstanzaEliminata ha restituito { JsonConvert.SerializeObject(result)}");

                return result;
            });
        }

        public void OnEvento(SchedaDinamicaIstanzaSalvata e)
        {
            CallService(ws =>
            {
                var request = new schedaDinamicaIstanzaSalvataRequest
                {
                    codiceIstanza = e.CodiceIstanza,
                    codiceScheda = e.IdScheda,
                    token = this._authInfo.Token
                };

                this._log.Debug($"Invocazione di schedaDinamicaIstanzaSalvata con i parametri { JsonConvert.SerializeObject(request)}");

                var result = ws.schedaDinamicaIstanzaSalvata(request);

                this._log.Debug($"schedaDinamicaIstanzaSalvata ha restituito { JsonConvert.SerializeObject(result)}");

                return result;
            });
        }

        public void OnEvento(SchedaDinamicaRimossaDaAttivita e)
        {
            CallService(ws =>
            {
                var request = new schedaDinamicaAttivitaEliminataRequest
                {
                    codiceAttivita = e.CodiceAttivita,
                    codiceScheda = e.IdModello,
                    idCampiDinamiciDaEliminare = e.ListaCampiEliminati.ToArray(),
                    token = this._authInfo.Token
                };

                this._log.Debug($"Invocazione di schedaDinamicaAttivitaEliminata con i parametri { JsonConvert.SerializeObject(request)}");

                var result = ws.schedaDinamicaAttivitaEliminata(request);

                this._log.Debug($"schedaDinamicaAttivitaEliminata ha restituito { JsonConvert.SerializeObject(result)}");

                return result;
            });
        }

        private AttivitaClient CreateClient()
        {
            var address = ParametriConfigurazione.Get.WsEventiAttivitaServiceUrl;
            //var address = "http://10.50.55.45:8080/backend/services/attivita?wsdl";

            var binding = new BasicHttpBinding(Constants.BindingName);
            var endpoint = new EndpointAddress(address);


            return new AttivitaClient(binding, endpoint);
        }

        private T CallService<T>(Func<AttivitaClient, T> callback)
        {
            using (var ws = CreateClient())
            {
                try
                {
                    return callback(ws);
                }
                catch (Exception)
                {
                    ws.Abort();

                    throw;
                }
            }
        }
    }
}
