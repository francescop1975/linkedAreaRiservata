using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;

namespace Init.SIGePro.Protocollo.Auriga.UD.ExtractMulti
{
    public class ServiceWrapper : ProxyServiceWrapper
    {
        private static class Constants
        {
            public const string Titolo = "ESTRAZIONE ALLEGATI";
            public const string NameSpace = @"http://extractmulti.webservices.repository2.auriga.eng.it";
            public const string ServiceName = "WSExtractMulti";
            public const string RequestServiceName = "ext";
            public const EstremiRegNumTypeCategoriaReg registroDefault = EstremiRegNumTypeCategoriaReg.PG;
        }

        public ServiceWrapper(ParametriRegoleInfo parametri, ProtocolloSerializer serializer, ProtocolloLogs log, ProxyRequestInfo request)
        {
            this._parametri = parametri;
            this._serializer = serializer;
            this._logs = log;
            this._request = request;
            this._titolo = Constants.Titolo;
            this._serviceName = Constants.ServiceName;
        }

        public ResponseInfo EstraiAllegati(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    EstremiXIdentificazioneUDAllegati estraiAllegatiRequest = new EstremiXIdentificazioneUDAllegati
                    {
                        Item = !String.IsNullOrEmpty(idProtocollo) ? (object)idProtocollo : new EstremiRegNumType
                        {
                            AnnoReg = annoProtocollo,
                            NumReg = numeroProtocollo,
                            CategoriaReg = Constants.registroDefault,
                            SiglaReg = null
                        },
                    };

                    var estraiAllegatiRequestXML = Utility.HtmlEncodeContent(this._serializer.Serialize("estraiAllegatiRequest.xml", estraiAllegatiRequest));

                    this._request.xml = estraiAllegatiRequestXML;
                    this._request.hash = getHashSHA1();

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = this._serializer.Serialize(ProtocolloLogsConstants.AllegatoRequestFileName, service);

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize(ProtocolloLogsConstants.AllegatoResponseFileName, serviceResponse);

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
