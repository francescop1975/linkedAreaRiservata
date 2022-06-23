using Init.SIGePro.Manager.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class JsonClient
    {
        private readonly Authorization _auth;
        private readonly string _url;

        public JsonClient(Authorization auth, string url)
        {
            
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException($"'{nameof(url)}' non può essere Null o vuoto", nameof(url));
            }

            this._auth = auth ?? throw new ArgumentNullException(nameof(auth));
            this._url = url;
        }

        private WebClient GetHttpClient()
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this._auth))));
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            return client;
        }

        public MovimentoRestBeanResponse InserisciMovimento(MovimentoRestBean request)
        {
            try
            {
                using (var client = GetHttpClient())
                {

                    var data = client.UploadString(_url, "POST", JsonConvert.SerializeObject(request));

                    return JsonConvert.DeserializeObject<MovimentoRestBeanResponse>(data);

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore nella chiamata al servizio {ex.Message}");
            }
        }

        protected T downloadSerializedJsonData<T>() where T : new()
        {
            using (var w = new WebClient())
            {

                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString(this._url);
                }
                catch (Exception) { json_data = null; }

                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }

        protected T downloadSerializedJsonDataAuthenticated<T>() where T : new()
        {

            var jsonHeader = new JsonTokenSoftwareHeader(this._auth.token, this._auth.software).ToJson();

            using (var w = new WebClient())
            {
                w.Headers.Add("Authorization",Base64Utils.Base64Encode(jsonHeader));

                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString(this._url);
                }
                catch (Exception) { json_data = null; }

                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }

        protected T downloadSerializedJsonDataPOST<T>(Object content) where T : new()
        {

            var w = (HttpWebRequest)WebRequest.Create(new Uri(this._url));
            w.Accept = "application/json";
            w.ContentType = "application/json";
            w.Method = "POST";

            string parsedContent = JsonConvert.SerializeObject(content);
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent);

            Stream newStream = w.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = w.GetResponse();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var json_data = sr.ReadToEnd();

            return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
        }

        protected T downloadSerializedJsonDataPOSTAuthenticated<T>(Object content) where T : new()
        {

            var w = (HttpWebRequest)WebRequest.Create(new Uri(this._url));
            w.Accept = "application/json";
            w.ContentType = "application/json";
            w.Method = "POST";

            var jsonHeader = new JsonTokenSoftwareHeader(this._auth.token, this._auth.software).ToJson();
            w.Headers.Add("Authorization", Base64Utils.Base64Encode(jsonHeader));

            string parsedContent = JsonConvert.SerializeObject(content);
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent);

            Stream newStream = w.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = w.GetResponse();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var json_data = sr.ReadToEnd();

            return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
        }

    }
}
