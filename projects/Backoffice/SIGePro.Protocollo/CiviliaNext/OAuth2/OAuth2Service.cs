using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.OAuth2
{
    public class OAuth2Service
    {
        private string _urlOAuth;
        private string _clientID;
        private string _clientSercret;
        private string _grantType;
        private string _resource;


        public OAuth2Service( string urlOAuth, string clientID, string clientSercret, string grantType, string resource)
        {
            this._urlOAuth = urlOAuth;
            this._clientID = clientID;
            this._clientSercret = clientSercret;
            this._grantType = grantType;
            this._resource = resource;
        }

        public string GetTokenOAuth2()
        {
            try
            {
                var client = new RestClient(this._urlOAuth);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("client_id", this._clientID);
                request.AddParameter("client_secret", this._clientSercret);
                request.AddParameter("grant_type", this._grantType);
                request.AddParameter("resource", this._resource);
                IRestResponse response = client.Execute(request);

                var oauth2Response = JsonConvert.DeserializeObject<OAuth2Response>(response.Content);

                return oauth2Response.AccessToken;

            }
            catch (Exception ex)
            {
                throw new Exception($"PROBLEMI DURANTE L'AUTENTICAZIONE OAUTH 2.0, {ex.Message}", ex);
            }
        }
    }
}
