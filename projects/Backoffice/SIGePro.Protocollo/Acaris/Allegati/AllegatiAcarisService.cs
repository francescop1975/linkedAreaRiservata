using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Acaris.Document;
using Init.SIGePro.Protocollo.Acaris.Document.CSIPiemonte;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisDocumentServicePort;
using Init.SIGePro.Protocollo.AcarisManagementServicePort;
using Init.SIGePro.Protocollo.AcarisNavigationServicePort;
using Init.SIGePro.Protocollo.AcarisObjectServicePort;
using Init.SIGePro.Protocollo.AcarisRelationshipsServicePort;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Metadati;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Allegati
{
    public class AllegatiAcarisService
    {
        private readonly IProtocolloSerializer _serializer;
        private readonly ParametriRegoleInfo _configurazione;
        private DocumentWSClient _documentWSClient;
        private ManagementWSClient _managementWSClient;

        public AllegatiAcarisService(IProtocolloSerializer serializer, ProtocolloBase protocollo, String idProtocollo)
        {
            this._serializer = serializer;
            this._configurazione = this.GetConfigurazione(protocollo, idProtocollo);

            this._documentWSClient = new DocumentWSClient(this._configurazione.DocumentPortUrl);

            this._managementWSClient = new ManagementWSClient(this._configurazione.ManagementPortUrl);
        }

        public AllegatiAcarisService(IProtocolloSerializer serializer, ParametriRegoleInfo configurazione)
        {
            this._serializer = serializer;
            this._configurazione = configurazione;

            this._documentWSClient = new DocumentWSClient(this._configurazione.DocumentPortUrl);

            this._managementWSClient = new ManagementWSClient(this._configurazione.ManagementPortUrl);
        }

        public List<AllegatoAcaris> GetAllegati(string idClassificazionePrincipale)
        {
            List<AllegatoAcaris> retVal = new List<AllegatoAcaris>();

            //allegato principale
            var docPrincipaleClassificazione = this.GetDocumentoDaClassificazione(idClassificazionePrincipale);

            var idDocPrincipale = docPrincipaleClassificazione.response
                .objects
                .Where(x => x.properties
                               .Where(y => y.queryName.className == "DocumentoSemplicePropertiesType")
                               .FirstOrDefault() != null
                       )
                .FirstOrDefault()
                .objectId
                .value;

            var idDocumentoFisicoPrincipale = this.GetIdDocumentoFisico(idDocPrincipale);

            var contenutoFisicoPrincipale = this.GetContenutoFisico(idDocumentoFisicoPrincipale);


            retVal.Add(new AllegatoAcaris
            {
                Id = idDocumentoFisicoPrincipale,
                NomeFile = this.ProperyValue(contenutoFisicoPrincipale.response.objects.FirstOrDefault().properties, "contentStreamFilename"),
                MimeType = this.ProperyValue(contenutoFisicoPrincipale.response.objects.FirstOrDefault().properties, "contentStreamMimeType"),
            });

            //altri allegati
            var idGruppoAllegati = docPrincipaleClassificazione.response
                .objects
                .Where(x => x.properties
                               .Where(y => y.queryName.className == "GruppoAllegatiPropertiesType")
                               .FirstOrDefault() != null
                       )
                .FirstOrDefault()?
                .objectId
                .value;

            try
            {

                var descendants = this.GetDescendants(idGruppoAllegati);

                foreach (var desc in descendants.objects.objects)
                {
                    var idClassificazione = desc.objectId.value;
                    var docClassificazione = this.GetDocumentoDaClassificazione(idClassificazione);
                    var idDocumento = docClassificazione.response
                                                .objects
                                                .Where(x => x.properties
                                                               .Where(y => y.queryName.className == "DocumentoSemplicePropertiesType")
                                                               .FirstOrDefault() != null
                                                       )
                                                .FirstOrDefault()
                                                .objectId
                                                .value;
                    var idDocumentoFisico = this.GetIdDocumentoFisico(idDocumento);
                    var contenutoFisico = this.GetContenutoFisico(idDocumentoFisico);

                    retVal.Add(new AllegatoAcaris
                    {
                        Id = idDocumentoFisico,
                        NomeFile = this.ProperyValue(contenutoFisico.response.objects.FirstOrDefault().properties, "contentStreamFilename"),
                        MimeType = this.ProperyValue(contenutoFisico.response.objects.FirstOrDefault().properties, "contentStreamMimeType"),
                    });
                }
            }
            catch (Exception) 
            { 
            
            }
            return retVal;
        }

        private getChildrenResponse GetDocumentoDaClassificazione(string idClassificazione)
        {
            using (var ws = new NavigationWSClient(this._configurazione.NavigationPortUrl).CreaWebService())
            {
                var request = new getChildren
                {
                    repositoryId = new AcarisNavigationServicePort.ObjectIdType { value = this._configurazione.RepositoryID.IdAcaris },
                    principalId = new AcarisNavigationServicePort.PrincipalIdType { value = this._configurazione.PrincipalID.IdAcaris },
                    folderId = new AcarisNavigationServicePort.ObjectIdType { value = idClassificazione },
                    filter = new AcarisNavigationServicePort.PropertyFilterType { filterType = AcarisNavigationServicePort.enumPropertyFilter.none }
                };

                this._serializer.Serialize("GetDocumentoDaClassificazioneRequest.xml", request, "GetDocumentoDaClassificazione: Inizio chiamata a getChildren");

                var response = ws.getChildren(request);

                this._serializer.Serialize("GetDocumentoDaClassificazioneResponse.xml", response, "GetDocumentoDaClassificazione: Fine chiamata a getChildren");

                return response;
            }
        }

        private string GetIdDocumentoFisico(string idDocumento)
        {
            using (var ws = new RelationshipsWSClient(this._configurazione.RelationshipsPortUrl).CreaWebService())
            {
                var request = new getObjectRelationships
                {
                    repositoryId = new AcarisRelationshipsServicePort.ObjectIdType { value = this._configurazione.RepositoryID.IdAcaris },
                    principalId = new AcarisRelationshipsServicePort.PrincipalIdType { value = this._configurazione.PrincipalID.IdAcaris },
                    objectId = new AcarisRelationshipsServicePort.ObjectIdType { value = idDocumento },
                    typeId = AcarisRelationshipsServicePort.enumRelationshipObjectType.DocumentCompositionPropertiesType,
                    typeIdSpecified = true,
                    direction = enumRelationshipDirectionType.source,
                    directionSpecified = true,
                    filter = new AcarisRelationshipsServicePort.PropertyFilterType { filterType = AcarisRelationshipsServicePort.enumPropertyFilter.all }

                };

                this._serializer.Serialize("GetDocumentoFisicoRequest.xml", request, "GetIdDocumentoFisico: Inizio chiamata a getObjectRelationships");

                var response = ws.getObjectRelationships(request);

                this._serializer.Serialize("GetDocumentoFisicoResponse.xml", response, "GetIdDocumentoFisico: Fine chiamata a getObjectRelationships");


                return response
                        .FirstOrDefault()?
                        .targetId.value;
            }
        }

        public getChildrenResponse GetContenutoFisico(string idDocumentoFisico)
        {
            using (var ws = new NavigationWSClient(this._configurazione.NavigationPortUrl).CreaWebService())
            {
                var request = new getChildren
                {
                    repositoryId = new AcarisNavigationServicePort.ObjectIdType { value = this._configurazione.RepositoryID.IdAcaris },
                    principalId = new AcarisNavigationServicePort.PrincipalIdType { value = this._configurazione.PrincipalID.IdAcaris },
                    folderId = new AcarisNavigationServicePort.ObjectIdType { value = idDocumentoFisico },
                    filter = new AcarisNavigationServicePort.PropertyFilterType
                    {
                        filterType = AcarisNavigationServicePort.enumPropertyFilter.list,
                        propertyList = new AcarisNavigationServicePort.QueryNameType[]
                        {
                            new AcarisNavigationServicePort.QueryNameType
                            {
                                className = "ContenutoFisicoPropertiesType",
                                propertyName = "contentStreamFilename"
                            },
                            new AcarisNavigationServicePort.QueryNameType
                            {
                                className = "ContenutoFisicoPropertiesType",
                                propertyName = "contentStreamMimeType"
                            }
                        }
                    }
                };

                this._serializer.Serialize("GetContenutoFisicoRequest.xml", request, "GetContenutoFisico: Inizio chiamata a getChildren");

                var response = ws.getChildren(request);

                this._serializer.Serialize("GetContenutoFisicoiResponse.xml", response, "GetContenutoFisico: Fine chiamata a getChildren");

                return response;
            }
        }

        public AcarisObjectServicePort.acarisContentStreamType GetAllegato(string idDocumentoFisico)
        {
            using (var ws = new ObjectWSClient(this._configurazione.ObjectPortUrl).CreaWebService())
            {
                var request = new getContentStream
                {
                    repositoryId = new AcarisObjectServicePort.ObjectIdType { value = this._configurazione.RepositoryID.IdAcaris },
                    principalId = new AcarisObjectServicePort.PrincipalIdType { value = this._configurazione.PrincipalID.IdAcaris },
                    documentId = new AcarisObjectServicePort.ObjectIdType { value = idDocumentoFisico },
                    streamId = AcarisObjectServicePort.enumStreamId.primary,
                    streamIdSpecified = true
                };


                this._serializer.Serialize("GetAllegatoRequest.xml", request, "GetAllegato: Inizio chiamata a getContentStream");

                var response = ws.getContentStream(request);

                this._serializer.Serialize("GetAllegatoResponse.xml", response, "GetAllegato: Fine chiamata a getContentStream");

                return response.FirstOrDefault();
            }
        }

        private getDescendantsResponse GetDescendants(string idGruppoAllegati)
        {
            using (var ws = new NavigationWSClient(this._configurazione.NavigationPortUrl).CreaWebService())
            {
                var request = new getDescendants
                {
                    repositoryId = new AcarisNavigationServicePort.ObjectIdType { value = this._configurazione.RepositoryID.IdAcaris },
                    principalId = new AcarisNavigationServicePort.PrincipalIdType { value = this._configurazione.PrincipalID.IdAcaris },
                    folderId = new AcarisNavigationServicePort.ObjectIdType { value = idGruppoAllegati },
                    filter = new AcarisNavigationServicePort.PropertyFilterType
                    {
                        filterType = AcarisNavigationServicePort.enumPropertyFilter.none
                    }
                };

                this._serializer.Serialize("GetDescendantsRequest.xml", request, "GetDescendants: Inizio chiamata a getDescendants");

                var response = ws.getDescendants(request);

                this._serializer.Serialize("GetDescendantsResponse.xml", response, "GetDescendants: Fine chiamata a getDescendants");

                return response;
            }
        }

        private string ProperyValue(AcarisNavigationServicePort.PropertyType[] props, string propertyName)
        {
            return props
                    .Where(x => x.queryName.propertyName == propertyName)
                    .Select(y => y.value)
                    .FirstOrDefault()
                    .FirstOrDefault()
                    .ToString();
        }

        private ParametriRegoleInfo GetConfigurazione(ProtocolloBase protocollo, string idProtocollo)
        {
            var db = protocollo.DatiProtocollo.Db;
            var idComune = protocollo.DatiProtocollo.IdComune;

            var metadati = new ProtocolloMetadatiMgr(db).GetMetadati(idComune, idProtocollo);

            var operatore = protocollo.Operatore;
            var software = protocollo.DatiProtocollo.Software;
            var codiceComune = protocollo.DatiProtocollo.CodiceComune;
            var idAoo = metadati.Where(x => x.Metadato == "IDAOO").Select(x => x.Valore).FirstOrDefault();
            var idStruttura = metadati.Where(x => x.Metadato == "IDSTRUTTURA").Select(x => x.Valore).FirstOrDefault();
            var idNodo = metadati.Where(x => x.Metadato == "IDNODO").Select(x => x.Valore).FirstOrDefault();

            if (String.IsNullOrEmpty(idAoo))
            {
                throw new ArgumentNullException($"Impossibile risalire al parametro AOO associato al protocollo con identificativo {idProtocollo}");
            }

            if (String.IsNullOrEmpty(idStruttura))
            {
                throw new ArgumentNullException($"Impossibile risalire al parametro STRUTTURA associato al protocollo con identificativo {idProtocollo}");
            }


            if (String.IsNullOrEmpty(idNodo))
            {
                throw new ArgumentNullException($"Impossibile risalire al parametro NODO associato al protocollo con identificativo {idProtocollo}");
            }


            return new ParametriRegoleInfoAdapter(this._serializer, operatore, idComune, software, codiceComune, Convert.ToInt32(idAoo), Convert.ToInt32(idStruttura), Convert.ToInt32(idNodo)).Adatta();

        }

        public creaDocumentoResponse CaricaAllegatoPrincipale(IDocumentResolver resolver, IdFolder idFolder, AllegatoAcaris allegato)
        {
            try
            {

                using (var ws = this._documentWSClient.CreaWebService())
                {


                    var documentoArchivisticoIRC = new DocumentFactory(this._serializer, resolver, idFolder).GetDocumento(allegato, true);

                    var request = new creaDocumento
                    {

                        repositoryId = new AcarisDocumentServicePort.ObjectIdType { value = resolver.RepositoryId.IdAcaris },
                        principalId = new AcarisDocumentServicePort.PrincipalIdType { value = resolver.PrincipalId.IdAcaris },
                        datiCreazione = documentoArchivisticoIRC.Documento,
                        tipoOperazione = enumTipoOperazione.elettronico
                    };


                    this._serializer.Serialize($"CreaDocumentoPrincipaleRequest.xml", request, $"Inizio chiamata a creaDocumento (principale)");

                    var response = ws.creaDocumento(request);

                    this._serializer.Serialize($"CreaDocumentoPrincipaleResponse.xml", response, $"Fine chiamata a creaDocumento (principale)");

                    foreach (var annotazione in documentoArchivisticoIRC.Annotazioni)
                    {
                        this.AggiungiAnnotazioneDocumento(resolver, response.info.objectIdDocumento.value, annotazione);

                    }

                    return response;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<creaDocumentoResponse> CaricaAllegatoSecondario(IDocumentResolver resolver,  AcarisDocumentServicePort.ObjectIdType idClassificazione, AllegatoAcaris allegato)
        {
            try
            {
                List<creaDocumentoResponse> retVal = new List<creaDocumentoResponse>();

                using (var ws = this._documentWSClient.CreaWebService())
                {
                    try
                    {
                        var documentoArchivisticoIRC = new DocumentFactory(this._serializer, resolver, idClassificazione).GetDocumento(allegato,false);

                        var request = new creaDocumento
                        {

                            repositoryId = new AcarisDocumentServicePort.ObjectIdType { value = resolver.RepositoryId.IdAcaris },
                            principalId = new AcarisDocumentServicePort.PrincipalIdType { value = resolver.PrincipalId.IdAcaris },
                            datiCreazione = documentoArchivisticoIRC.Documento,
                            tipoOperazione = enumTipoOperazione.elettronico
                        };


                        this._serializer.Serialize($"CreaDocumentoSecondarioRequest.xml", request, $"Inizio chiamata a creaDocumento (secondario)");

                        var response = ws.creaDocumento(request);

                        this._serializer.Serialize($"CreaDocumentoSecondarioResponse.xml", response, $"Fine chiamata a creaDocumento (secondario)");


                        foreach (var annotazione in documentoArchivisticoIRC.Annotazioni)
                        {
                            this.AggiungiAnnotazioneDocumento(resolver, response.info.objectIdDocumento.value, annotazione);

                        }
                        retVal.Add(response);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }


                    return retVal;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void AggiungiAnnotazioneDocumento(IDocumentResolver resolver, String idDocumentoAcaris, Annotazione annotazione)
        {
            try
            {
                using (var ws = this._managementWSClient.CreaWebService())
                {
                    var request = new addAnnotazioni
                    {
                        repositoryId = new AcarisManagementServicePort.ObjectIdType { value = resolver.RepositoryId.IdAcaris },
                        principalId = new AcarisManagementServicePort.PrincipalIdType { value = resolver.PrincipalId.IdAcaris },
                        objectId = new AcarisManagementServicePort.ObjectIdType { value = idDocumentoAcaris },
                        annotazioni = new AcarisManagementServicePort.AnnotazioniPropertiesType
                        {
                            annotazioneFormale = annotazione.Formale,
                            data = DateTime.Now,
                            descrizione = annotazione.Testo
                        }

                    };

                    this._serializer.Serialize($"AddAnnotazioniRequest.xml", request, $"Inizio chiamata a addAnnotazioni");

                    var response = ws.addAnnotazioni(request);

                    this._serializer.Serialize($"AddAnnotazioniResponse.xml", response, $"Fine chiamata a addAnnotazioni");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
