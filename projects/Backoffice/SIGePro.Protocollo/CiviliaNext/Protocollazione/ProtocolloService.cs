using Init.SIGePro.Protocollo.CiviliaNext.Protocollazione.AnnullaProtocollo;
using Init.SIGePro.Protocollo.CiviliaNext.Protocollazione.InvioProtocollo;
using Init.SIGePro.Protocollo.CiviliaNext.Protocollazione.Protocolla;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Init.SIGePro.Protocollo.CiviliaNext.Protocollazione
{
    public class ProtocolloService
    {
        private class OAuthConstants
        {
            public const string ClientIdParameter = "client_id";
            public const string SecretParameter = "client_secret";
            public const string GrantTypeParameter = "grant_type";
            public const string GrantTypeValue = "client_credentials";
        }

        ParametriRegoleInfo _parametri;

        public ProtocolloService(ParametriRegoleInfo parametri)
        {
            this._parametri = parametri;
        }

        public ProtocollazioneResponse Protocolla(ProtocollazioneRequest protocollazioneRequest)
        {
            try
            {
                this._parametri.Logger.Debug("Protocolla: Serializzazione della request");
                var jsonRequest = JsonConvert.SerializeObject(protocollazioneRequest);
                this._parametri.Logger.Debug(jsonRequest);

                var client = new RestClient(this._parametri.ProtocollazioneURL);

                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {this._parametri.Token}");
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddParameter("application/json; charset=utf-8", jsonRequest, ParameterType.RequestBody);
                
                this._parametri.Logger.Debug("Protocolla: Chiamata al ws");
                IRestResponse restResponse = client.Execute(request);
                this._parametri.Logger.Debug(restResponse.Content);
                this._parametri.Logger.Debug("Protocolla: Fine chiamata al ws");

                var response = JsonConvert.DeserializeObject<ProtocollazioneResponse>(restResponse.Content);
                if (response.ResultType != 1)
                {
                    throw new Exception(response.ResultDescription);
                }
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE GENERATO DURANTE LA CHIAMATA A PROTOCOLLAZIONE, {ex.Message}");
            }
        }

        public AnnullaProtocolloResponse AnnullaProtocollo(AnnullaProtocolloRequest annullaProtocolloRequest)
        {
            try
            {
                this._parametri.Logger.Info("AnnullaProtocollo: Serializzazione della request");
                var jsonRequest = JsonConvert.SerializeObject(annullaProtocolloRequest);

                var client = new RestClient(this._parametri.AnnullaProtocolloURL);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {this._parametri.Token}");
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddParameter("application/json; charset=utf-8", jsonRequest, ParameterType.RequestBody);
                this._parametri.Logger.Info("AnnullaProtocollo: Chiamata al ws");
                IRestResponse restResponse = client.Execute(request);
                this._parametri.Logger.Info("AnnullaProtocollo: Fine chiamata al ws");
                var response = JsonConvert.DeserializeObject<AnnullaProtocolloResponse>(restResponse.Content);
                if (response.ResultType != 1)
                {
                    throw new Exception(response.ResultDescription);
                }
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception($"ERRORE GENERATO DURANTE LA CHIAMATA A PROTOCOLLAZIONE, {ex.Message}");
            }
        }

        public InvioProtocolloResponse InvioProtocollo(InvioProtocolloRequest invioProtocolloRequest) 
        {
            try
            {
                this._parametri.Logger.Info("InvioProtocollo: Serializzazione della request");
                var jsonRequest = JsonConvert.SerializeObject(invioProtocolloRequest);

                var client = new RestClient(this._parametri.InviaProtocolloURL);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {this._parametri.Token}");
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                request.AddParameter("application/json; charset=utf-8", jsonRequest, ParameterType.RequestBody);
                this._parametri.Logger.Info("InvioProtocollo: Chiamata al ws");
                IRestResponse restResponse = client.Execute(request);
                this._parametri.Logger.Info("InvioProtocollo: Fine chiamata al ws");
                var response = JsonConvert.DeserializeObject<InvioProtocolloResponse>(restResponse.Content);
                if (response.ResultType != 1)
                {
                    throw new Exception(response.ResultDescription);
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
