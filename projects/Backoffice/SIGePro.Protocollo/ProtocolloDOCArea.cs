using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.ProtocolloFactories;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.Validation;
using Init.SIGePro.Data;
using System.IO;
using Init.SIGePro.Verticalizzazioni;
using Init.SIGePro.Protocollo.DocArea.Adapters;
using Init.SIGePro.Protocollo.DocArea;
using Init.SIGePro.Protocollo.DocArea.Configurations;
using Init.SIGePro.Protocollo.DocArea.Builders;
using Init.SIGePro.Protocollo.DocArea.Services;
using Init.SIGePro.Protocollo.WsDataClass;
using Init.SIGePro.Protocollo.DocArea.Factories;
using Init.SIGePro.Protocollo.DocArea.PEC;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using Init.SIGePro.Protocollo.DocArea.LeggiProtocollo;
using Init.SIGePro.Protocollo.ProtocolloServices;

namespace Init.SIGePro.Protocollo
{
    public class PROTOCOLLO_DOCAREA : ProtocolloBase
    {

        public override void InizializzaProtocolloBase(ResolveDatiProtocollazioneService datiProtocolloService)
        {
            base.InizializzaProtocolloBase(datiProtocolloService);
            base._protocolloSerializer = new DocAreaSerializer(this._protocolloLogs);
        }

        public PROTOCOLLO_DOCAREA()
        {

        }

        public override DatiProtocolloRes Protocollazione(DatiProtocolloIn protoIn)
        {
            var vertParams = new DocAreaVerticalizzazioneParametriAdapter(_protocolloLogs, new VerticalizzazioneProtocolloDocarea(DatiProtocollo.IdComuneAlias, DatiProtocollo.Software, DatiProtocollo.CodiceComune));

            DateTime? dataRicevimento = DateTime.Now;
            string domicilioElettronico = "";

            if (this.DatiProtocollo.TipoAmbito == ProtocolloEnumerators.ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA)
            {
                dataRicevimento = this.DatiProtocollo.Istanza.DATA.Value;
                domicilioElettronico = this.DatiProtocollo.Istanza.DOMICILIO_ELETTRONICO;
            }

            var conf = new DocAreaSegnaturaParamConfiguration(vertParams, Operatore, protoIn, domicilioElettronico, dataRicevimento.Value, this.Provenienza, this.TipoInserimento);

            var protoSrv = ProtocollazioneServiceWrapperFactory.Create(conf, this.Anagrafiche, _protocolloLogs, _protocolloSerializer);
            string token = protoSrv.Login(vertParams.Codiceente, vertParams.Username, vertParams.Password);

            var datiProto = DatiProtocolloInsertFactory.Create(protoIn);

            var segnaturaBuilder = new DocAreaSegnaturaBuilder(datiProto, conf, _protocolloLogs, _protocolloSerializer, protoIn.Allegati);

            if (vertParams.InviaSegnatura && protoIn.Allegati.Count == 0)
                segnaturaBuilder.CreaSegnaturaFittizia();

            if (vertParams.InviaAllMovAvvio && protoIn.Allegati.Count == 0 && this.DatiProtocollo.TipoAmbito == ProtocolloEnumerators.ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA)
                protoSrv.InserisciAllegatiDaMovimentoAvvio(this.DatiProtocollo.Istanza, this.DatiProtocollo.Db, DatiProtocollo.IdComune, protoIn.Allegati);

            protoSrv.InserisciAllegati(protoIn.Allegati, token, vertParams.Username, vertParams.InviaNomeFileAttachment, vertParams.IsAbilitatoWarningVerificaFirma);
            segnaturaBuilder.SerializzaSegnatura();

            var response = protoSrv.Protocollazione(vertParams.Username, token);

            if (vertParams.GestionePec && protoIn.Flusso == "P")
            {
                try
                {
                    var factory = PECFactory.Create(this.Anagrafiche, vertParams.UtenteCreazionePec, vertParams.TipoFornitore, _protocolloLogs, _protocolloSerializer, vertParams.Username, vertParams.Password);
                    if (factory != null)
                    {
                        factory.InviaPec(vertParams.UrlPec, response);
                    }
                }
                catch (Exception ex)
                {
                    _protocolloLogs.WarnFormat("ERRORE GENERATO DURANTE L'INVIO DELLA PEC, ERRORE: {0}", ex.Message);
                }
            }

            return DocAreaProtocolloInsertOutputAdapter.Adatta(response, _protocolloLogs);
        }

        public override DatiProtocolloLetto LeggiProtocollo(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {

            this._protocolloLogs.Info($"Inizio chiamata a LeggiProtocollo({idProtocollo}, {annoProtocollo}, {numeroProtocollo})");

            if (string.IsNullOrEmpty(annoProtocollo))
            {
                throw new ArgumentException($"'{nameof(annoProtocollo)}' non può essere Null o vuoto", nameof(annoProtocollo));
            }

            if (string.IsNullOrEmpty(numeroProtocollo))
            {
                throw new ArgumentException($"'{nameof(numeroProtocollo)}' non può essere Null o vuoto", nameof(numeroProtocollo));
            }


            this._protocolloLogs.Info($"Inizio recupero parametri dalla verticalizzazione");

            var vertParams = new DocAreaVerticalizzazioneParametriAdapter(_protocolloLogs, new VerticalizzazioneProtocolloDocarea(DatiProtocollo.IdComuneAlias, DatiProtocollo.Software, DatiProtocollo.CodiceComune));

            this._protocolloLogs.Info($"Fine recupero parametri dalla verticalizzazione");

            var service = new LeggiProtocolloFactory(vertParams.TipoFornitore, vertParams.UrlLeggiProtocollo, vertParams.Username, vertParams.Password, this._protocolloSerializer).GetService();

            this._protocolloLogs.Info($"Inizio lettura del protocollo");

            var retVal = service.Leggi( Convert.ToInt32( annoProtocollo), Convert.ToInt32( numeroProtocollo ), null);

            this._protocolloLogs.Info($"Fine lettura del protocollo");

            this._protocolloLogs.Info($"Fine chiamata a LeggiProtocollo({idProtocollo}, {annoProtocollo}, {numeroProtocollo})");

            return retVal;
        }

        public override AllOut LeggiAllegato()
        {
            this._protocolloLogs.Info($"Inizio chiamata a LeggiAllegato({IdAllegato})");

            this._protocolloLogs.Info($"Inizio recupero parametri dalla verticalizzazione");

            var vertParams = new DocAreaVerticalizzazioneParametriAdapter(_protocolloLogs, new VerticalizzazioneProtocolloDocarea(DatiProtocollo.IdComuneAlias, DatiProtocollo.Software, DatiProtocollo.CodiceComune));

            this._protocolloLogs.Info($"Fine recupero parametri dalla verticalizzazione");

            var service = new LeggiAllegatoFactory(vertParams.TipoFornitore, vertParams.UrlDownloadAllegato, vertParams.Username, vertParams.Password, this._protocolloSerializer).GetService();

            this._protocolloLogs.Info($"Inizio download allegato");

            AllOut allegato = GetAllegato();

            allegato.Image = service.Download(allegato.IDBase);

            this._protocolloLogs.Info($"Fine download allegato");

            this._protocolloLogs.Info($"Inizio chiamata a LeggiAllegato({IdAllegato})");

            return allegato;
        }
    }
}

