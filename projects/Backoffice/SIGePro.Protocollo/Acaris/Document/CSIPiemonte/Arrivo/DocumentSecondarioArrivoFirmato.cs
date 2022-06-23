using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.Acaris.Allegati;
using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisDocumentServicePort;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Document.CSIPiemonte.Arrivo
{
    public class DocumentSecondarioArrivoFirmato : DocumentoArchivisticoIRC, IDocument
    {
        private class Constants
        {
            public static List<int> VerificheDaEscludere = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
        }

        private IDocumentResolver _resolver;
        private ObjectIdType _idClassificazionePrincipale;
        private ObjectWSClient _objectWSClient;
        private readonly ManagementWSClient _managementWSClient;

        public InfoRichiestaCreazione Documento { get; internal set; }

        public List<Annotazione> Annotazioni { get; internal set; }

        public DocumentSecondarioArrivoFirmato(IProtocolloSerializer serializer, IDocumentResolver resolver, ObjectIdType idClassificazionePrincipale, AllegatoAcaris allegato)
        {
            this._resolver = resolver;
            this._idClassificazionePrincipale = idClassificazionePrincipale;

            this._objectWSClient = new ObjectWSClient(this._resolver.ObjectWSUrl);

            this._managementWSClient = new ManagementWSClient(this._resolver.ManagementWsUrl);

            var mimeType = allegato.MimeType.FromReflectedXmlValue<enumMimeTypeType>();

            this.Annotazioni = new List<Annotazione>()
            {
                new Annotazione
                {
                    Testo = "Documentazione presentata ai sensi dell'art.65 comma 1, lettera b) del Codice dell'Amministrazione Digitale e s.m.i.",
                    Formale = true,
                    AnnotaInteroDocumento = true,
                    AnnotaClassificazioneCorrente = false
                }
            };

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
                    applicativoAlimentante = "GECONC",
                    //autoreFisico = this._resolver.MittentiPF.ToArray(),
                    //autoreGiuridico = this._resolver.MittentiPG.ToArray(),
                    composizione = enumDocPrimarioType.DocumentoSingolo,
                    contentStreamFilename = allegato.NomeFile,
                    contentStreamMimeType = mimeType,
                    contentStreamLength = allegato.ContentLenght,
                    datiPersonali = true,
                    datiRiservati = false,
                    datiSensibili = false,
                    dataCreazione = DateTime.Now,
                    dataDocCronicaSpecified = false,
                    daConservarePrimaDelSpecified = false,
                    daConservareDopoIlSpecified = false,
                    definitivo = true,
                    destinatarioGiuridico = this._resolver.DestinatariPG.ToArray(),
                    docAutenticato = false,
                    docAutenticatoCopiaAutentica = false,
                    docAutenticatoFirmaAutenticata = false,
                    docConAllegati = false,
                    idStatoDiEfficacia = new IdStatoDiEfficaciaType { value = this.GetStatoEfficaciaDefault(serializer) },
                    idVitalRecordCode = new IdVitalRecordCodeType { value = new IdVitalRecordCodeMedio(serializer,this._managementWSClient, this._resolver.RepositoryId).Id },
                    modificabile = false,
                    multiplo = false,
                    objectTypeId = enumArchiveObjectType.DocumentoSemplicePropertiesType,
                    originatore = new string[] { "GECONC" },
                    oggetto = allegato.NomeFile,
                    origineInterna = false,
                    rappresentazioneDigitale = true,
                    registrato = true,
                    tipoDocFisico = enumTipoDocumentoType.Firmato,
                },
                allegato = true,
                classificazionePrincipale = this._idClassificazionePrincipale,
                documentiFisici = new DocumentoFisicoIRC[]
                {
                    new DocumentoFisicoIRC
                    {
                        propertiesDocumentoFisico = new DocumentoFisicoPropertiesType
                        {
                            descrizione = "documento fisico",
                            dataMemorizzazione = DateTime.Now
                        },
                        contenutiFisici = new ContenutoFisicoIRC[]
                        {
                            new ContenutoFisicoIRC
                            {
                                propertiesContenutoFisico = new ContenutoFisicoPropertiesType
                                {
                                    contentStreamLength = allegato.ContentLenght,
                                    contentStreamMimeType = mimeType,
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
