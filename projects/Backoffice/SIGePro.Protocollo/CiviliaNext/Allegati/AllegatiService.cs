using Init.SIGePro.Protocollo.CiviliaNext.Allegati.AggiungiAllegati;
using Init.SIGePro.Protocollo.CiviliaNext.Allegati.GetAllegati;
using Init.SIGePro.Protocollo.CiviliaNext.Allegati.GetAllegato;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.Allegati
{
    public class AllegatiService
    {
        private ParametriRegoleInfo _parametri;

        public AllegatiService(ParametriRegoleInfo parametri)
        {
            this._parametri = parametri;
        }

        public AggiungiAllegatoResponse AggiungiAllegato(AggiungiAllegatiRequest requestAllegati)
        {
            this._parametri.Logger.Debug("AggiungiAllegato: Serializzazione della request");
            var jsonRequest = JsonConvert.SerializeObject(requestAllegati);
            this._parametri.Logger.Debug(jsonRequest);

            var client = new RestClient(this._parametri.AggiungiAllegatoURL);
            
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {this._parametri.Token}");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("application/json; charset=utf-8", jsonRequest, ParameterType.RequestBody);
            
            this._parametri.Logger.Debug("AggiungiAllegato: Chiamata al ws");
            IRestResponse restResponse = client.Execute(request);
            this._parametri.Logger.Debug(restResponse.Content);
            this._parametri.Logger.Debug("AggiungiAllegato: Fine chiamata al ws");
            
            var response = JsonConvert.DeserializeObject<AggiungiAllegatoResponse>(restResponse.Content);
            if (response.ResultType != 1)
            {
                throw new Exception(response.ResultDescription);
            }
            return response;
        }

        public GetAllegatiResponse GetAllegati(GetAllegatiRequest requestGetAllegati) 
        {
            this._parametri.Logger.Debug("GetAllegati: Serializzazione della request");
            var jsonRequest = JsonConvert.SerializeObject(requestGetAllegati);
            this._parametri.Logger.Debug(jsonRequest);

            var client = new RestClient(this._parametri.GetAllegatiURL);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {this._parametri.Token}");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("application/json; charset=utf-8", jsonRequest, ParameterType.RequestBody);
            
            this._parametri.Logger.Debug("GetAllegati: Chiamata al ws");
            IRestResponse restResponse = client.Execute(request);
            this._parametri.Logger.Debug(restResponse.Content);
            this._parametri.Logger.Debug("GetAllegati: Fine chiamata al ws");

            var response = JsonConvert.DeserializeObject<GetAllegatiResponse>(restResponse.Content);
            if (response.ResultType != 1)
            {
                throw new Exception(response.ResultDescription);
            }
            return response;
        }

        public GetAllegatoResponse DownloadAllegato(GetAllegatoRequest requestGetAllegato)
        {
            this._parametri.Logger.Debug("DownloadAllegato: Serializzazione della request");
            var jsonRequest = JsonConvert.SerializeObject(requestGetAllegato);
            this._parametri.Logger.Debug(jsonRequest);

            var client = new RestClient(this._parametri.GetAllegatoURL);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {this._parametri.Token}");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("application/json; charset=utf-8", jsonRequest, ParameterType.RequestBody);
            
            this._parametri.Logger.Debug("DownloadAllegato: Chiamata al ws");
            IRestResponse restResponse = client.Execute(request);
            this._parametri.Logger.Debug(restResponse.Content);
            this._parametri.Logger.Debug("DownloadAllegato: Fine chiamata al ws");

            var response = JsonConvert.DeserializeObject<GetAllegatoResponse>(restResponse.Content);
            if (response.ResultType != 1)
            {
                throw new Exception(response.ResultDescription);
            }
            return response;
        }
    }
}
