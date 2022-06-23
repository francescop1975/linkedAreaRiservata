using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Protocollo.Acaris;
using Init.SIGePro.Protocollo.Acaris.Allegati;
using Init.SIGePro.Protocollo.Acaris.Document;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.Acaris.Folder;
using Init.SIGePro.Protocollo.Acaris.Protocollazione;
using Init.SIGePro.Protocollo.Acaris.Protocollazione.Lettura;
using Init.SIGePro.Protocollo.Acaris.Protocollazione.Registrazioni;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocolloServices;
using Init.SIGePro.Protocollo.WsDataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo
{
    public class PROTOCOLLO_ACARIS : ProtocolloBase
    {
        public override void InizializzaProtocolloBase(ResolveDatiProtocollazioneService datiProtocolloService)
        {
            base.InizializzaProtocolloBase(datiProtocolloService);
            base._protocolloSerializer  = new AcarisSerializer(this._protocolloLogs);
        }

        public override DatiProtocolloRes Protocollazione(DatiProtocolloIn protoIn) 
        {
            try
            {
                var serializer = new AcarisSerializer(this._protocolloLogs);

                this._protocolloLogs.DebugFormat("Inizio protocollazione");
                serializer.Serialize(ProtocolloLogsConstants.DatiProtocolloInFileName, protoIn);

                var datiProtocollo = new ProtocolloExt(serializer, this._protocolloLogs, protoIn, this);


                var folderResolver = new FascicoloRealeLiberoResolver(datiProtocollo);
                var folderService = new FolderService(serializer, this._protocolloLogs, folderResolver);

                IdFolder idFolder;
                if (this.DatiProtocollo.TipoAmbito == ProtocolloEnumerators.ProtocolloEnum.AmbitoProtocollazioneEnum.DA_MOVIMENTO &&
                    !String.IsNullOrEmpty(this.DatiProtocollo.Istanza.FKIDPROTOCOLLO))
                {
                    //recupero del folder
                    idFolder = folderService.GetFolderDaIdProtocollo(this, this.DatiProtocollo.Istanza.FKIDPROTOCOLLO);
                }
                else
                {
                    //creazione del folder
                    var folderResponse = folderService.CreaFascicolo();
                    idFolder = new IdFolder(folderResponse.objectId.value);
                }

                var allegatiService = new AllegatiAcarisService(serializer, datiProtocollo.Configurazione);

                var resolver = new DocumentResolver(datiProtocollo.Configurazione, protoIn);

                var metadati = this.GetMetadati(protoIn.Allegati[0].CODICEOGGETTO);
                var allegato = new AllegatoAcaris(protoIn.Allegati[0], metadati);
                var classificazionePrincipale = allegatiService.CaricaAllegatoPrincipale(resolver, idFolder, allegato).info.objectIdClassificazione;

                foreach (var a in protoIn.Allegati.Skip(1))
                {
                    metadati = this.GetMetadati(a.CODICEOGGETTO);
                    allegato = new AllegatoAcaris(a, metadati);
                    allegatiService.CaricaAllegatoSecondario(resolver, classificazionePrincipale, allegato);
                }

                //protocollazione
                IRegistrazioneAPIResolver registrazioneAPIResolver;

                switch (protoIn.Flusso)
                {
                    case "A":
                        registrazioneAPIResolver = new RegistrazioneAPIArrivoResolver(datiProtocollo, classificazionePrincipale.value);
                        break;
                    case "I":
                        throw new NotImplementedException($"Impossibile specificare il flusso {protoIn.Flusso} per la protocollazione");
                    case "P":
                        registrazioneAPIResolver = new RegistrazioneAPIPartenzaResolver(datiProtocollo, classificazionePrincipale.value);
                        break;
                    default:
                        throw new NotImplementedException($"Impossibile specificare il flusso {protoIn.Flusso} per la protocollazione");
                }

                var protocolloResolver = new ProtocollazioneDocumentoEsistenteResolver(registrazioneAPIResolver, datiProtocollo.Configurazione);

                var protocolloService = new ProtocollazioneService(serializer, this._protocolloLogs, protocolloResolver);

                var registrazioneResponse = protocolloService.Protocolla();

                this._protocolloLogs.DebugFormat("Fine protocollazione");

                return protocolloService.CreaRegistrazioneResponseToDatiProtocolloRes(registrazioneResponse);
            }
            catch (Exception ex) 
            {
                this._protocolloLogs.Error(ex);
                throw (ex);
            }
        }

        public override DatiProtocolloLetto LeggiProtocollo(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {

            this._protocolloLogs.DebugFormat("Inizio lettura protocollazione");

            var protocolloService = new LeggiProtocolloService(this._protocolloSerializer, this, idProtocollo);

            var datiProtocollo = protocolloService.LeggiProtocollo();

            this._protocolloLogs.DebugFormat("Fine lettura protocollazione");

            return datiProtocollo;
        }

        public override AllOut LeggiAllegato()
        {
            
            this._protocolloLogs.DebugFormat($"Inizio download dell'allegato con id {IdAllegato}");


            var allegatiService = new AllegatiAcarisService(this._protocolloSerializer, this, IdProtocollo);

            var contenutoFisico = allegatiService.GetContenutoFisico(IdAllegato);

            var allegato = allegatiService.GetAllegato(IdAllegato);

            this._protocolloLogs.DebugFormat($"Fine download dell'allegato con id {IdAllegato}");
            
            return new AllOut 
            { 
                IDBase = IdAllegato,
                Commento = this.ProperyValue(contenutoFisico.response.objects.FirstOrDefault().properties, "contentStreamFilename"),
                Serial = this.ProperyValue(contenutoFisico.response.objects.FirstOrDefault().properties, "contentStreamFilename"),
                Image = allegato.streamMTOM,
                ContentType = this.ProperyValue(contenutoFisico.response.objects.FirstOrDefault().properties, "contentStreamMimeType")
            };
            
        }

        private IEnumerable<OggettiMetadati> GetMetadati(string codiceOggetto) 
        {
            return new OggettiMetadatiMgr(this.DatiProtocollo.Db).GetMetadatiVerificaFirma(this.DatiProtocollo.IdComune, Convert.ToInt32( codiceOggetto));
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
    }
}
