using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.Acaris.Allegati;
using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Acaris.Document;
using Init.SIGePro.Protocollo.Acaris.Document.CSIPiemonte;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.Acaris.Protocollazione.Lettura;
using Init.SIGePro.Protocollo.AcarisDocumentServicePort;
using Init.SIGePro.Protocollo.AcarisManagementServicePort;
using Init.SIGePro.Protocollo.AcarisNavigationServicePort;
using Init.SIGePro.Protocollo.AcarisObjectServicePort;
using Init.SIGePro.Protocollo.AcarisOfficialBookServicePort;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using PropertyType = Init.SIGePro.Protocollo.AcarisObjectServicePort.PropertyType;

namespace Init.SIGePro.Protocollo.Acaris.Folder
{
    public class FolderService
    {
        private IProtocolloSerializer _serializer;
        private ProtocolloLogs _logger;
        private IFolderTypeResolver _resolver;
        private ObjectWSClient _objectWSClient;
        private ManagementWSClient _managementWSClient;
        private DocumentWSClient _documentWSClient;
        private NavigationWSClient _navigationWSClient;

        public FolderService(IProtocolloSerializer serializer, ProtocolloLogs logger, IFolderTypeResolver resolver)
        {
            this._serializer = serializer;
            this._logger = logger;
            this._resolver = resolver;


            this._objectWSClient = new ObjectWSClient(this._resolver.Configurazione.ObjectPortUrl);

            this._managementWSClient = new ManagementWSClient(this._resolver.Configurazione.ManagementPortUrl);

            this._documentWSClient = new DocumentWSClient(this._resolver.Configurazione.DocumentPortUrl);

            this._navigationWSClient = new NavigationWSClient(this._resolver.Configurazione.NavigationPortUrl);
        }


        public moveDocumentResponse AggiungiProtocolloAFolder(createFolderResponse folderResponse, creaRegistrazioneResponse registrazioneResponse)
        {
            try
            {
                using (var ws = this._objectWSClient.CreaWebService())
                {

                    var request = new moveDocument
                    {
                        repositoryId = new AcarisObjectServicePort.ObjectIdType { value = this._resolver.RepositoryId.IdAcaris },
                        principalId = new AcarisObjectServicePort.PrincipalIdType { value = this._resolver.PrincipalId.IdAcaris },
                        associativeObjectId = new AcarisObjectServicePort.ObjectIdType { value = registrazioneResponse.identificazioneCreazione.registrazioneId.value }
                    };

                    this._logger.DebugFormat($"Inizio chiamata a moveDocument");
                    this._serializer.Serialize("moveDocumentRequest.xml", request);

                    var response = ws.moveDocument(request);

                    this._serializer.Serialize("moveDocumentResponse.xml", response);
                    this._logger.DebugFormat($"Fine chiamata a moveDocument");

                    return response;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"CHIAMATA FALLITA, {ex.Message}", ex);
            }
        }

        public createFolderResponse CreaFolder(string folderIdParent)
        {
            try
            {
                using (var ws = this._objectWSClient.CreaWebService())
                {

                    var request = new createFolder
                    {
                        typeId = this._resolver.TypeId,
                        principalId = new AcarisObjectServicePort.PrincipalIdType { value = this._resolver.PrincipalId.IdAcaris },
                        repositoryId = new AcarisObjectServicePort.ObjectIdType { value = this._resolver.RepositoryId.IdAcaris },
                        properties = this._resolver.Properties,
                        folderId = new AcarisObjectServicePort.ObjectIdType { value = folderIdParent }
                    };



                    this._serializer.Serialize("CreateFolderRequest.xml", request, "Inizio chiamata a createFolder");

                    var response = ws.createFolder(request);

                    this._serializer.Serialize("CreateFolderResponse.xml", response, "Fine chiamata a createFolder");

                    return response;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"CHIAMATA FALLITA, {ex.Message}", ex);
            }
        }
        /*
        public string CaricaAllegati(IDocumentResolver documentResolver,IdFolder idFolder, string flusso)
        {


            var allegatiService = new AllegatiService(this._serializer, documentResolver, documentResolver.Configurazione);

            var documentoPrincipaleResponse = allegatiService.CaricaAllegatoPrincipale(idFolder, this._resolver.AllegatoPrincipale.CodiceOggetto, flusso);

            allegatiService.CaricaAllegatiSecondari(documentoPrincipaleResponse.info.objectIdClassificazione, flusso);

            return documentoPrincipaleResponse.info.objectIdClassificazione.value;
        }
        */
        public createFolderResponse CreaFascicolo()
        {
            //recupero la voce padre
            var vocePadre = new IdVoceComposta( this._serializer, this._objectWSClient, this._resolver.RepositoryId, this._resolver.PrincipalId, this._resolver.Voce, this._resolver.IdTitolario);
            //recupero la serie padre
            var seriePadre = new IdSerieFascicoli(this._serializer, this._objectWSClient, this._resolver.RepositoryId, this._resolver.PrincipalId, this._resolver.CodiceSerie, vocePadre);
            //creo il folder
            return this.CreaFolder(seriePadre.IdAcaris);
        }

        public IdFolder GetFolderDaIdProtocollo(ProtocolloBase protocollo, String idProtocollo) 
        {
            var leggiProtocolloResponse = new LeggiProtocolloService(this._serializer, protocollo, idProtocollo).GetProtocolloProperties();

            var leggiProtocolloProperties = leggiProtocolloResponse
                                       .FirstOrDefault()
                                       .properties;


            var idClassificazione = this.ProperyValue(leggiProtocolloProperties, "idClassificazione");

            var folderDaClassificazioneResponse = this.GetFolderDaClassificazione(idClassificazione);

            return new IdFolder(folderDaClassificazioneResponse.@object.objectId.value);
        }

        private getFolderParentResponse GetFolderDaClassificazione(string idClassificazione) 
        {
            try
            {
                using (var ws = this._navigationWSClient.CreaWebService())
                {

                    var request = new getFolderParent
                    {
                        repositoryId = new AcarisNavigationServicePort.ObjectIdType { value = this._resolver.RepositoryId.IdAcaris },
                        principalId = new AcarisNavigationServicePort.PrincipalIdType { value = this._resolver.PrincipalId.IdAcaris },
                        folderId = new AcarisNavigationServicePort.ObjectIdType { value = idClassificazione },
                        filter = new AcarisNavigationServicePort.PropertyFilterType { filterType = AcarisNavigationServicePort.enumPropertyFilter.none }

                    };

                    this._serializer.Serialize("getFolderParentRequest.xml", request, "Inizio chiamata a getFolderParent");

                    var response = ws.getFolderParent(request);

                    this._serializer.Serialize("getFolderParentResponse.xml", response, "Fine chiamata a getFolderParent");

                    return response;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"CHIAMATA FALLITA, {ex.Message}", ex);
            }
        }

        private string ProperyValue(PropertyType[] props, string propertyName)
        {
            return props
                    .Where(x => x.queryName.propertyName == propertyName)
                    .Select(y => y.value)
                    .FirstOrDefault()
                    .FirstOrDefault()
                    .ToString();
        }
    }
}
