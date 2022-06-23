using Init.SIGePro.Protocollo.Acaris.Allegati;
using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisDocumentServicePort;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Document.CSIPiemonte.Partenza
{

    public class DocumentPrincipalePartenzaFirmato : DocumentoArchivisticoIRC, IDocument
    {
        private class Constants 
        {
            public static List<int> VerificheDaEscludere = new List<int>{ 1, 2, 3, 4, 5, 6, 7 };
            public static string ApplicativoAlimentante = "GECONC";
            public static string Originatore = "GECONC";
            public static string DocumentoFisicoDescrizione = "documento fisico";
        }

        public InfoRichiestaCreazione Documento { get; internal set; }

        public List<Annotazione> Annotazioni { get; internal set; }

        private IDocumentResolver _resolver;
        private IdFolder _idFolder;
        private ObjectWSClient _objectWSClient;
        private ManagementWSClient _managementWSClient;



        public DocumentPrincipalePartenzaFirmato(IProtocolloSerializer serializer, IDocumentResolver resolver, IdFolder idFolder, AllegatoAcaris allegato)
        {
            this._resolver = resolver;
            this._idFolder = idFolder;

            this._objectWSClient = new ObjectWSClient(this._resolver.ObjectWSUrl);

            this._managementWSClient = new ManagementWSClient(this._resolver.ManagementWsUrl);

            this.Annotazioni = new List<Annotazione>();


            var mimeType = allegato.MimeType.FromReflectedXmlValue<enumMimeTypeType>();

            this.Documento = new DocumentoArchivisticoIRC
            {
                tipoDocumento = enumTipoDocumentoArchivistico.DocumentoSemplice,
                propertiesClassificazione = new ClassificazionePropertiesType
                {
                    copiaCartacea = false,
                    cartaceo = false,
                },
                propertiesDocumento = new DocumentoSemplicePropertiesType
                {
                    analogico = false,
                    applicativoAlimentante = Constants.ApplicativoAlimentante,
                    //autoreFisico = this._resolver.MittentiPF.ToArray(),
                    autoreGiuridico = this._resolver.MittentiPG.ToArray(),
                    composizione = enumDocPrimarioType.DocumentoSingolo,
                    contentStreamFilename = allegato.NomeFile,
                    contentStreamMimeType = mimeType,
                    contentStreamLength = allegato.ContentLenght,
                    datiPersonali = true,
                    datiRiservati = false,
                    datiSensibili = false,
                    dataCreazione = DateTime.Now,
                    dataDocCronica = DateTime.Now,
                    dataDocCronicaSpecified = true,
                    daConservarePrimaDelSpecified = false,
                    daConservareDopoIlSpecified = false,
                    definitivo = true,
                    destinatarioGiuridico = this._resolver.DestinatariPG.ToArray(),
                    destinatarioFisico = this._resolver.DestinatariPF.ToArray(),
                    docAutenticato = false,
                    docAutenticatoCopiaAutentica = false,
                    docAutenticatoFirmaAutenticata = false,
                    docConAllegati = this._resolver.NumeroAllegati > 0,
                    idStatoDiEfficacia = new IdStatoDiEfficaciaType { value = this.GetStatoEfficaciaDefault(serializer) },
                    idVitalRecordCode = new IdVitalRecordCodeType { value = new IdVitalRecordCodeMedio(serializer,this._managementWSClient, this._resolver.RepositoryId).Id },
                    modificabile = false,
                    multiplo = false,
                    objectTypeId = enumArchiveObjectType.DocumentoSemplicePropertiesType,
                    originatore = new string[] { Constants.Originatore },
                    oggetto = this._resolver.OggettoDocumentoPrincipale,
                    origineInterna = false,
                    rappresentazioneDigitale = true,
                    registrato = true,
                    tipoDocFisico = enumTipoDocumentoType.Firmato,
                },
                parentFolderId = new ObjectIdType { value = this._idFolder.IdAcaris },
                gruppoAllegati = new GruppoAllegatiPropertiesType
                {
                    numeroAllegati = this._resolver.NumeroAllegati,
                    dataInizio = DateTime.Now
                },
                documentiFisici = new DocumentoFisicoIRC[]
                {
                    new DocumentoFisicoIRC
                    {
                        propertiesDocumentoFisico = new DocumentoFisicoPropertiesType
                        {
                            descrizione = Constants.DocumentoFisicoDescrizione,
                            dataMemorizzazione = DateTime.Now
                        },
                        contenutiFisici = new ContenutoFisicoIRC[]
                        {
                            new ContenutoFisicoIRC
                            {
                                propertiesContenutoFisico = new ContenutoFisicoPropertiesType
                                {
                                    contentStreamLength = allegato.ContentLenght,
                                    numeroVersione = 0,
                                    workingCopy = false,
                                    sbustamento = false,
                                    modificabile = false,
                                    docPrimario = false  //viene sovrascritto da tipo = AcarisDocumentServicePort.enumStreamId.primary,
                                },
                                stream = new acarisContentStreamType
                                {
                                    filename = allegato.NomeFile,
                                    streamMTOM = allegato.Content,
                                    mimeType = mimeType,
                                    mimeTypeSpecified = true,
                                },
                                tipo = enumStreamId.primary,
                                azioniVerificaFirma = new StepErrorAction[]
                                {
                                    new StepErrorAction
                                    {
                                        action = enumStepErrorAction.insert,
                                        step = 0
                                    }
                                }
                            }
                        },
                        azioniVerificaFirma = Constants.VerificheDaEscludere.Select(x => new StepErrorAction{ step = x, action = enumStepErrorAction.insert } ).ToArray()
                    }
                }
            };
        }

        private int GetStatoEfficaciaDefault(IProtocolloSerializer serializer)
        {
            return new IdStatoEfficaciaPerfettoEdEfficace(serializer,this._objectWSClient, this._resolver.RepositoryId, this._resolver.PrincipalId).DbKey;
        }
    }
}
