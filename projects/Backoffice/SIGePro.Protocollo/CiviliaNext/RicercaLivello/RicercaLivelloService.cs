using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.RicercaLivello
{
   public class RicercaLivelloService
    {
        private ParametriRegoleInfo _parametri;

        public RicercaLivelloService(ParametriRegoleInfo parametri)
        {
            this._parametri = parametri;
        }

        public RicercaLivelloResponse RicercaLivello(RicercaLivelloRequest requestCercaPratiche)
        {
            this._parametri.Logger.Debug("RicercaLivello: Serializzazione della request");
            var jsonRequest = JsonConvert.SerializeObject(requestCercaPratiche);
            this._parametri.Logger.Debug(jsonRequest);

            var client = new RestClient(this._parametri.RicercaLivelloURL);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {this._parametri.Token}");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("application/json; charset=utf-8", jsonRequest, ParameterType.RequestBody);
            
            this._parametri.Logger.Debug("RicercaLivello: Chiamata al ws");
            IRestResponse restResponse = client.Execute(request);
            this._parametri.Logger.Debug(restResponse.Content);
            this._parametri.Logger.Debug("RicercaLivello: Fine chiamata al ws");

            var response = JsonConvert.DeserializeObject<RicercaLivelloResponse>(restResponse.Content);
            if (!response.IsOk)
            {
                throw new Exception($"Errore generico restituito durante la chiamata al servizio {this._parametri.CercaPraticheURL}");
            }
            return response;
        }
    }
}
