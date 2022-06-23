using Init.SIGePro.Protocollo.Auriga.Folder.GetMetadataFolder.Request;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Auriga.Folder.GetMetadataFolder
{
    public class ServiceWrapper : ProxyServiceWrapper
    {
        private static class Constants
        {
            public const string Titolo = "LEGGI FASCICOLO";
            public const string NameSpace = @"http://getmetadatafolder.webservices.repository2.auriga.eng.it";
            public const string ServiceName = "WSGetMetadataFolder";
            public const string RequestServiceName = "get";
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

        public ResponseInfo LeggiFascicoloDaID(string idFascicolo)
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    var request = new EstremiXIdentificazioneFolderType
                    {
                        Items = new object[] { idFascicolo },
                        ItemsElementName = new ItemsChoiceType[] { ItemsChoiceType.IdFolder }
                    };

                    var requestXml = this._serializer.Serialize("letturaFascicoloRequest.xml", request);

                    this._request.xml = requestXml;
                    this._request.hash = getHashSHA1();

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = this._serializer.Serialize(ProtocolloLogsConstants.LeggiFascicoloRequestFileName, service);

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize(ProtocolloLogsConstants.LeggiFascicoloRequestFileName, serviceResponse);

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

        public ResponseInfo LeggiFascicoloDaPath(string libreria, string pathNome)
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    var request = new EstremiXIdentificazioneFolderType
                    {
                        Items = new object[] {
                            new OggDiTabDiSistemaType
                            {
                                Item = libreria,
                                ItemElementName = ItemChoiceType.Decodifica_Nome
                            },
                            pathNome
                        },
                        ItemsElementName = new ItemsChoiceType[] 
                        {
                            ItemsChoiceType.Libreria,
                            ItemsChoiceType.Path_Nome
                        }
                    };

                    var requestXml = Utility.HtmlEncodeContent(this._serializer.Serialize("letturaFascicoloRequest.xml", request));

                    this._request.xml = requestXml;
                    this._request.hash = getHashSHA1();

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = this._serializer.Serialize(ProtocolloLogsConstants.LeggiFascicoloRequestFileName, service);

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize(ProtocolloLogsConstants.LeggiFascicoloRequestFileName, serviceResponse);

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
