using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.Titolario
{
    public class EstraiTitolarioService
    {
        private ParametriRegoleInfo _parametri;

        public EstraiTitolarioService(ParametriRegoleInfo parametri)
        {
            this._parametri = parametri;
        }

        public EstraiTitolarioResponse EstraiTitolario(EstraiTitolarioRequest requestEstraiTitolario)
        {
            this._parametri.Logger.Debug("EstraiTitolario: Serializzazione della request");
            var jsonRequest = JsonConvert.SerializeObject(requestEstraiTitolario);
            this._parametri.Logger.Debug(jsonRequest);

            var client = new RestClient(this._parametri.EstraiTitolarioURL);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {this._parametri.Token}");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("application/json; charset=utf-8", jsonRequest, ParameterType.RequestBody);
            
            this._parametri.Logger.Debug("EstraiTitolario: Chiamata al ws");
            IRestResponse restResponse = client.Execute(request);
            this._parametri.Logger.Debug(jsonRequest);
            this._parametri.Logger.Debug("EstraiTitolario: Fine chiamata al ws");

            var response = JsonConvert.DeserializeObject<EstraiTitolarioResponse>(restResponse.Content);
            if (!response.IsOk)
            {
                throw new Exception("Errore generico del servizio che fornisce l'elenco delle classifiche");
            }
            return response;

        }
    }
}
