
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;

namespace Init.SIGePro.Protocollo.Auriga.Folder.TrovaDocFolder
{
    public class ServiceWrapper : ProxyServiceWrapper
    {
        private static class Constants
        {
            public const string Titolo = "RICERCA FASCICOLI";
            public const string NameSpace = @"http://trovadocfolder.webservices.repository2.auriga.eng.it";
            public const string ServiceName = "WSTrovaDocFolder";
            public const string RequestServiceName = "trov";
            public const string TipoOggettiDaCercareFolder = "F";
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

        public ResponseInfo CercaFascicoli(Fascicolo fascicolo)
        {
            try
            {
                using (var ws = CreaWebService())
                {
                    TrovaDocFolder trovaDocFolderRequest = new TrovaDocFolder
                    {
                        FiltriPrincipali = new TrovaDocFolderFiltriPrincipali
                        {
                            TipoOggettiDaCercare = Constants.TipoOggettiDaCercareFolder,
                            CercaInVistaUtente = null,
                            CercaInFolder = null,
                            SoloRecenti = null,
                            SoloDaLeggere = null,
                            FiltroFullText = new TrovaDocFolderFiltriPrincipaliFiltroFullText
                            {
                                FlagTutteLeParole = "1",
                            },
                        },
                        FiltriAvanzati = new TrovaDocFolderFiltriAvanzati
                        {
                            NewsConNotificheCondivisione = null,
                            NewsConNotificheAutomatiche = null,
                            NewsConOsservazioni = null,
                            TipoDocumento = null,
                            TipoFolder = null,
                            StatoDocumento = null,
                            StatoFolder = null,
                            DataAggiornamentoStatoDaSpecified = false,
                            DataAggiornamentoStatoASpecified = false,
                            SoloConLock = null,
                            ApplicazionePropietaria = null,
                            RegistrazioneDoc = null,
                            RuoloUtenteVsDocFolder = null,
                            AttributoAdd = SetCriteriRicerca(fascicolo)
                        }
                    };

                    var trovaDocFolderRequestXML = Utility.HtmlEncodeContent(this._serializer.Serialize("trovaDocFolderRequest.xml", trovaDocFolderRequest));

                    this._request.xml = trovaDocFolderRequestXML;
                    this._request.hash = getHashSHA1();

                    var service = getServiceRequest(Constants.NameSpace, Constants.RequestServiceName);
                    var xmlService = this._serializer.Serialize(ProtocolloLogsConstants.ListaFascicoliRequestFileName, service);

                    this.LogInfoRequestWS(xmlService);

                    var serviceResponse = ws.AurigaProxy(service);
                    var responseXml = this._serializer.Serialize(ProtocolloLogsConstants.ListaFascicoliResponseFileName, serviceResponse);

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

        private CriterioRicercaSuAttributoAddType[] SetCriteriRicerca(Fascicolo fascicolo)
        {
            var retVal = new List<CriterioRicercaSuAttributoAddType>();

            if(fascicolo != null)
            {
                if(fascicolo.Classifica != null)
                {
                    retVal.Add( new CriterioRicercaSuAttributoAddType
                    {
                        Nome = "Classificazione",
                        OperatoreLogico = CriterioRicercaSuAttributoAddTypeOperatoreLogico.uguale,
                        ValoreConfronto_1 = fascicolo.Classifica
                    });
                }

                if (fascicolo.AnnoFascicolo != null)
                {
                    retVal.Add(new CriterioRicercaSuAttributoAddType
                    {
                        Nome = "Anno fascicolo",
                        OperatoreLogico = CriterioRicercaSuAttributoAddTypeOperatoreLogico.uguale,
                        ValoreConfronto_1 = fascicolo.AnnoFascicolo.ToString()
                    });
                }
            }

            return retVal.ToArray();
        }
    }
}
