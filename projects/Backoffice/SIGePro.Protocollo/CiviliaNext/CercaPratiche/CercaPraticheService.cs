using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.CercaPratiche
{
    public class CercaPraticheService
    {
        private ParametriRegoleInfo _parametri;

        public CercaPraticheService(ParametriRegoleInfo parametri)
        {
            this._parametri = parametri;
        }

        public CercaPraticheResponse CercaPratiche(CercaPraticheRequest requestCercaPratiche)
        {
            this._parametri.Logger.Debug("CercaPratiche: Serializzazione della request");
            var jsonRequest = JsonConvert.SerializeObject(requestCercaPratiche);
            this._parametri.Logger.Debug(jsonRequest);

            var client = new RestClient(this._parametri.CercaPraticheURL);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {this._parametri.Token}");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("application/json; charset=utf-8", jsonRequest, ParameterType.RequestBody);
            
            this._parametri.Logger.Debug("CercaPratiche: Chiamata al ws");
            IRestResponse restResponse = client.Execute(request);
            this._parametri.Logger.Debug(restResponse.Content);
            this._parametri.Logger.Debug("CercaPratiche: Fine chiamata al ws");

            var response = JsonConvert.DeserializeObject<CercaPraticheResponse>(restResponse.Content);
            if (response.ResultType != 1)
            {
                throw new Exception(response.ResultDescription);
            }
            return response;
        }
    }
}
