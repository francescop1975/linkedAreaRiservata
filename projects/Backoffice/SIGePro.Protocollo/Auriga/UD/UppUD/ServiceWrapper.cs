using Init.SIGePro.Protocollo.Auriga.Folder.GetMetadataFolder.Response;
using Init.SIGePro.Protocollo.AurigaProxyService;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;

namespace Init.SIGePro.Protocollo.Auriga.UD.UppUD
{
    public class ServiceWrapper : ProxyServiceWrapper
    {


        private static class Constants
        {
            public const string Titolo = "AGGIORNA PROTOCOLLO PER FASCICOLAZIONE";
            public const string NameSpace = @"http://updunitadoc.webservices.repository2.auriga.eng.it";
            public const string ServiceName = "WSUpdUd";
            public const string RequestServiceName = "upd";
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

        public ResponseInfo FascicolaProtocollo( string idProtocollo, string idFascicolo )
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    UDDaAgg updateProtocolloRequest = new UDDaAgg
                    {
                        EstremiXIdentificazioneUD = new EstremiXIdentificazioneUDType
                        {
                            Item = idProtocollo
                        },
                        AggCollocazioneClassificazioneUD = new UDDaAggAggCollocazioneClassificazioneUD
                        {
                            AggClassifFascicoli = new UDDaAggAggCollocazioneClassificazioneUDAggClassifFascicoli
                            {
                                Items = new ClassifFascicoloType[]
                                {
                                    new ClassifFascicoloType
                                    {
                                        Item = new EstremiXIdentificazioneFolderNoLibType
                                        {
                                            Item = idFascicolo,
                                            ItemElementName = ItemChoiceType6.IdFolder
                                        }
                                    }
                                },
                                ItemsElementName = new ItemsChoiceType1[]
                                {
                                    ItemsChoiceType1.ClassifFascicoloDaAggiungere
                                }
                            }
                        }
                    };

                    var updateProtocolloRequestXML = Utility.HtmlEncodeContent(this._serializer.Serialize("updateProtocolloRequest.xml", updateProtocolloRequest));

                    this._request.xml = updateProtocolloRequestXML;
                    this._request.hash = getHashSHA1();

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = this._serializer.Serialize(ProtocolloLogsConstants.UpdateProtocolloRequestFileName, service);

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize(ProtocolloLogsConstants.UpdateProtocolloResponseFileName, serviceResponse);

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

        public ResponseInfo FascicolaProtocollo( string idProtocollo, string numeroFascicolo, string annoFascicolo, string codClassifica )
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    UDDaAgg updateProtocolloRequest = new UDDaAgg
                    {
                        EstremiXIdentificazioneUD = new EstremiXIdentificazioneUDType
                        {
                            Item = idProtocollo
                        },
                        AggCollocazioneClassificazioneUD = new UDDaAggAggCollocazioneClassificazioneUD
                        {
                            AggClassifFascicoli = new UDDaAggAggCollocazioneClassificazioneUDAggClassifFascicoli
                            {
                                Items = new ClassifFascicoloType[]
                                {
                                    new ClassifFascicoloType
                                    {
                                        Item = new ClassifUAType
                                        {
                                           AnnoAperturaUA = annoFascicolo,
                                           NroProgrUA = numeroFascicolo,
                                           LivelloClassificazione = Utility.GetLivelloGerarchiaDaClassifica(codClassifica)
                                        }
                                    }
                                },
                                ItemsElementName = new ItemsChoiceType1[]
                                {
                                    ItemsChoiceType1.ClassifFascicoloDaAggiungere
                                }
                            }
                        }
                    };

                    var updateProtocolloRequestXML = Utility.HtmlEncodeContent(this._serializer.Serialize("updateProtocolloRequest.xml", updateProtocolloRequest));

                    this._request.xml = updateProtocolloRequestXML;
                    this._request.hash = getHashSHA1();

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = this._serializer.Serialize(ProtocolloLogsConstants.UpdateProtocolloRequestFileName, service);

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize(ProtocolloLogsConstants.UpdateProtocolloResponseFileName, serviceResponse);

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
