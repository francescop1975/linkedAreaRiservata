using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Auriga.UD.GetMetadataUd
{
    public class ServiceWrapper : ProxyServiceWrapper
    {
        private static class Constants
        {
            public const string Titolo = "LEGGI PROTOCOLLO";
            public const string NameSpace = @"http://getmetadataud.webservices.repository2.auriga.eng.it";
            public const string ServiceName = "WSGetMetadataUd";
            public const string RequestServiceName = "get";
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

        public ResponseInfo LeggiProtocollo( string idProtocollo, string annoProtocollo, string numeroProtocollo )
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    EstremiXIdentificazioneUD letturaRequest = new EstremiXIdentificazioneUD
                    {
                        Item = !String.IsNullOrEmpty(idProtocollo) ? (object)idProtocollo : new EstremiRegNumType
                            {
                                AnnoReg = annoProtocollo,
                                NumReg = numeroProtocollo,
                                CategoriaReg = Constants.registroDefault,
                                SiglaReg = null
                            },
                    };

                    var letturaRequestXML = this._serializer.Serialize("letturaRequest.xml", letturaRequest);

                    this._request.xml = Utility.HtmlEncodeContent(letturaRequestXML);
                    this._request.hash = getHashSHA1();

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = this._serializer.Serialize(ProtocolloLogsConstants.LeggiProtocolloRequestFileName, service);

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize(ProtocolloLogsConstants.LeggiProtocolloResponseFileName, serviceResponse);

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
