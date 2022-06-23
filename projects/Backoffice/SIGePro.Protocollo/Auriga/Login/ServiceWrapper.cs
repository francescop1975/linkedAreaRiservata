using Init.SIGePro.Protocollo.AurigaProxyService;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;

namespace Init.SIGePro.Protocollo.Auriga.Login
{
    public class ServiceWrapper : ProxyServiceWrapper
    {
        private static class Constants
        {
            public const string Titolo = "LOGIN";
            public const string NameSpace = @"http://login.webservices.repository2.auriga.eng.it";
            public const string ServiceName = "WSLogin";
            public const string RequestServiceName = "log";
        }

        public ServiceWrapper(ParametriRegoleInfo parametri, ProtocolloSerializer serializer, ProtocolloLogs logs, ProxyRequestInfo request)
        {
            this._parametri = parametri;
            this._serializer = serializer;
            this._logs = logs;
            this._request = request;
            this._titolo = Constants.Titolo;
            this._serviceName = Constants.ServiceName;
        }
        public ResponseInfo Login()
        {
            try
            {
                using (var ws = CreaWebService())
                {

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = Utility.HtmlEncodeContent(this._serializer.Serialize(ProtocolloLogsConstants.LoginRequest, service));

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize(ProtocolloLogsConstants.LoginResponse, serviceResponse);

                    this.LogInfoResponseWS(responseXml);

                    var response = new ResponseInfoAdapter(serviceResponse).Adatta();
                    if (response.WsResult != "1")
                        throw new Exception(response.WsError);

                    this.LogSuccess();

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{this._titolo} fallita: {ex.Message}", ex);
            }
        }

    }
}
