using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Auriga.UD.ExtractOne
{
    public class ServiceWrapper : ProxyServiceWrapper
    {
        private static class Constants
        {
            public const string Titolo = "ESTRAZIONE ALLEGATO SINGOLO";
            public const string NameSpace = @"http://extractone.webservices.repository2.auriga.eng.it";
            public const string ServiceName = "WSExtractOne";
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

        public ResponseInfo EstraiPrimario(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            try
            {
                using (var ws = CreaWebService())
                {



                    EstremiXidentificazioneVerDocType estraiAllegatoRequest = new EstremiXidentificazioneVerDocType
                    {
                        EstremiXIdentificazioneUD = new EstremiXIdentificazioneUDType
                        {
                            Item = !String.IsNullOrEmpty(idProtocollo) ? (object)idProtocollo : new EstremiRegNumType
                            {
                                AnnoReg = annoProtocollo,
                                NumReg = numeroProtocollo,
                                CategoriaReg = Constants.registroDefault,
                                SiglaReg = null
                            },
                        },
                        EstremixIdentificazioneFileUD = new EstremiXIdentificazioneFileUDType
                        {
                            ItemElementName = ItemChoiceType1.FlagPrimario
                        }
                    };

                    var estraiAllegatoRequestXML = Utility.HtmlEncodeContent(this._serializer.Serialize("EstraiAllegatoPrimarioRequest.xml", estraiAllegatoRequest));

                    this._request.xml = estraiAllegatoRequestXML;
                    this._request.hash = getHashSHA1();

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = this._serializer.Serialize(ProtocolloLogsConstants.AllegatoRequestFileName, service);

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize("EstraiAllegatoPrimarioResponse.xml", serviceResponse);

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

        public ResponseInfo EstraiAllegato(string idProtocollo, string annoProtocollo, string numeroProtocollo, string indice)
        {
            try
            {
                using (var ws = CreaWebService())
                {

                    

                    EstremiXidentificazioneVerDocType estraiAllegatoRequest = new EstremiXidentificazioneVerDocType
                    {
                        EstremiXIdentificazioneUD = new EstremiXIdentificazioneUDType
                        {
                            Item = !String.IsNullOrEmpty(idProtocollo) ? (object)idProtocollo : new EstremiRegNumType
                            {
                                AnnoReg = annoProtocollo,
                                NumReg = numeroProtocollo,
                                CategoriaReg = Constants.registroDefault,
                                SiglaReg = null
                            },
                        },
                        EstremixIdentificazioneFileUD = new EstremiXIdentificazioneFileUDType
                        {
                            Item = indice,
                            ItemElementName = ItemChoiceType1.NroAllegato
                        }
                    };

                    var estraiAllegatoRequestXML = Utility.HtmlEncodeContent(this._serializer.Serialize($"EstraiAllegatoSecondario{indice}Request.xml", estraiAllegatoRequest));

                    this._request.xml = estraiAllegatoRequestXML;
                    this._request.hash = getHashSHA1();

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = this._serializer.Serialize(ProtocolloLogsConstants.AllegatoRequestFileName, service);

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize($"EstraiAllegatoSecondario{indice}Response.xml", serviceResponse);

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
