using Init.SIGePro.Manager.Utils;
using Init.SIGePro.Protocollo.AurigaProxyService;
using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.IO;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.Auriga
{
    public class ProxyServiceWrapper
    {
        protected string _titolo;
        protected string _serviceName;
        protected ParametriRegoleInfo _parametri { get; set; }
        protected ProtocolloSerializer _serializer { get; set; }
        protected ProtocolloLogs _logs { get; set; }
        protected ProxyRequestInfo _request;

        protected AurigaProxyClient CreaWebService()
        {
            try
            {
                var endPointAddress = new EndpointAddress(this._parametri.ProxyUrl);
                var binding = new BasicHttpBinding("defaultHttpBinding");
                binding.MessageEncoding = WSMessageEncoding.Mtom;
                if (String.IsNullOrEmpty(this._parametri.Url))
                    throw new Exception("IL PARAMETRO URL DELLA VERTICALIZZAZIONE PROTOCOLLO_AURIGA NON È STATO VALORIZZATO.");

                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                {
                    binding.Security = new BasicHttpSecurity { Mode = BasicHttpSecurityMode.Transport };
                }

                return new AurigaProxyClient(binding, endPointAddress);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected AurigaProxyRequestType getServiceRequest( string nameSpace, string requestServiceName)
        {
            return new AurigaProxyRequestType()
            {
                codiceApplicazione = this._request.codiceApplicazione,
                hash = this._request.hash,
                istanzaApplicazione = this._request.istanzaApplicazione,
                password = this._request.password,
                userName = this._request.userName,
                xml = this._request.xml,
                @namespace = nameSpace,
                token = this._request.token,
                codiciOggetto = ( this._request.codiciOggetto != null ) ? this._request.codiciOggetto.ToArray() : null,
                wsUrl = this._parametri.Url.Replace("{servicename}", this._serviceName),
                serviceName = requestServiceName
            };
        }

        protected string getHashSHA1( )
        {
            byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes( this._request.xml ));
            return Convert.ToBase64String(hash);
        }

        protected string getBase64FromXMLString(string logsFolder, string xmlFileName)
        {
            var buffer = File.ReadAllBytes(Path.Combine(logsFolder, xmlFileName));
            return Base64Utils.Base64Encode(buffer);
        }

        public void LogInfoRequestWS(string xmlInviato)
        {
            this._logs.Info($"RICHIESTA {_titolo}: USERNAME WS: {this._request.userName}, PASSWORD WS: {this._request.password}, ISTANZAAPPLICAZIONE: {this._request.istanzaApplicazione}, CODAPPLICAZIONE: {this._request.codiceApplicazione}, REQUEST: {xmlInviato}");
        }

        public void LogInfoResponseWS(string xmlInviato)
        {
            this._logs.Info($"RISPOSTA {_titolo}: USERNAME WS: {this._request.userName}, PASSWORD WS: {this._request.password}, ISTANZAAPPLICAZIONE: {this._request.istanzaApplicazione}, CODAPPLICAZIONE: {this._request.codiceApplicazione}, REQUEST: {xmlInviato}");
        }

        public void LogSuccess()
        {
            this._logs.Info($"CHIAMATA A {_titolo} AVVENUTA CORRETTAMENTE");
        }
    }
}
