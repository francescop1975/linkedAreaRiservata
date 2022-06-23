using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Protocollo.Acaris.Allegati;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisObjectServicePort;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Manager;
using Init.SIGePro.Protocollo.Metadati;
using Init.SIGePro.Protocollo.ProtocolloServices.MailTipo;
using Init.SIGePro.Protocollo.Serialize;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Folder
{
    public class FascicoloRealeLiberoResolver : FolderResolver
    {
        private class Constants
        {
            public const string Separatore = " - ";
            public const string Annotazione = "Documentazione presentata ai sensi dell'art.65 comma 1, lettera b) del Codice dell'Amministrazione Digitale e s.m.i.";
        }

        private ProtocolloExt _protocollo;

        public FascicoloRealeLiberoResolver(ProtocolloExt protocollo) : base(protocollo.Configurazione)
        {
            this._protocollo = protocollo;

        }

        public override enumFolderObjectType TypeId => enumFolderObjectType.FascicoloRealeLiberoPropertiesType;

        public override string Voce => this._protocollo.DatiProtocollo.Classifica;

        public override string OggettoDocumentoPrincipale => this._protocollo.DatiProtocollo.Oggetto;

        public override PropertiesType Properties => new FascicoloRealeLiberoPropertiesType
        {
            anno = this._protocollo.DatiProtocollo.DataRegistrazione.GetValueOrDefault(DateTime.Now).Year,
            archivioCorrente = true,
            conservazioneCorrente = this.Parametri.AnniConservazioneCorrente,
            conservazioneGenerale = this.Parametri.AnniConservazioneGenerale,
            descrizione = String.IsNullOrEmpty(this._protocollo.DescrizioneLavori) ? this._protocollo.DatiProtocollo.Oggetto : this._protocollo.DescrizioneLavori,
            dataCreazione = DateTime.Now,
            datiPersonali = true,
            datiSensibili = false,
            datiRiservati = false,
            idAOORespMat = new IdAOOType { value = this.Parametri.IdAoo.Id },
            idNodoRespMat = new IdNodoType { value = this.Parametri.IdNodo.Id },
            idStrutturaRespMat = new IdStrutturaType { value = this.Parametri.IdStruttura.Id },
            idVitalRecordCode = new IdVitalRecordCodeType { value = this.Parametri.IdGradoVitalita },
            numero = this._protocollo.NumeroIstanza,
            objectTypeId = enumArchiveObjectType.FascicoloRealeLiberoPropertiesType,
            oggetto = this.OggettoFascicolo,
            paroleChiave = String.Join(",", this._protocollo.InterventoIstanza.Split(Convert.ToChar("-")).Select(x => x.Trim())),
            soggetto = String.Join(Constants.Separatore, this._protocollo.Soggetti),
            utenteCreazione = new CodiceFiscaleType { value = this.Parametri.CodiceFiscale }
        };

        public override List<string> MittentiPG
        {
            get
            {
                List<string> retVal = new List<string>();

                if (this._protocollo.DatiProtocollo.Mittenti.Amministrazione.Count > 0)
                {
                    retVal.AddRange(this._protocollo.DatiProtocollo.Mittenti.Amministrazione.Select(x => x.AMMINISTRAZIONE));
                }

                var pg = this._protocollo.DatiProtocollo.Mittenti.Anagrafe?.Where(x => x.TIPOANAGRAFE == "G").Select(x => x.GetNomeCompleto());
                if (pg != null && pg.Count() > 0)
                {
                    retVal.AddRange(pg);
                }
                return retVal;
            }
        }
        public override List<string> MittentiPF
        {
            get 
            {
                List<string> retVal = new List<string>();
                var pf = this._protocollo.DatiProtocollo.Mittenti.Anagrafe?.Where(x => x.TIPOANAGRAFE == "F").Select(x => x.GetNomeCompleto());
                if (pf != null && pf.Count() > 0)
                {
                    retVal.AddRange(pf);
                }
                return retVal;
            }
        }

        public override List<string> DestinatariPG
        {
            get
            {
                List<string> retVal = new List<string>();

                if (this._protocollo.DatiProtocollo.Destinatari?.Amministrazione.Count > 0)
                {
                    retVal.AddRange(this._protocollo.DatiProtocollo.Destinatari.Amministrazione.Select(x => x.AMMINISTRAZIONE));
                }

                var pg = this._protocollo.DatiProtocollo.Destinatari.Anagrafe?.Where(x => x.TIPOANAGRAFE == "G").Select(x => x.GetNomeCompleto());
                if (pg != null && pg.Count() > 0)
                {
                    retVal.AddRange(pg);
                }
                return retVal;
            }
        }

        public override List<string> DestinatariPF
        {

            get
            {
                List<string> retVal = new List<string>();

                var pf = this._protocollo.DatiProtocollo.Destinatari?.Anagrafe.Where(x => x.TIPOANAGRAFE == "F").Select(x => x.GetNomeCompleto());
                if (pf != null && pf.Count() > 0)
                {
                    retVal.AddRange(pf);
                }

                return retVal;
            }
        }

        public override int NumeroAllegati => this._protocollo.DatiProtocollo.Allegati.Count;

        public override string NomeFilePrincipale
        {
            get
            {
                if (this._protocollo.DatiProtocollo.Allegati?.Count == 0)
                {
                    return null;
                }

                return this._protocollo.DatiProtocollo.Allegati[0].NOMEFILE;
            }
        }

        public override string AnnotazioneDocPrincipale
        {
            get 
            {
                return Constants.Annotazione;
            }
        }

        public override string UtenteEsteso 
        { 
            get 
            {
                if (this.MittentiPF.Any()) 
                {
                    var pf = this._protocollo.DatiProtocollo.Mittenti.Anagrafe?.Where(x => x.TIPOANAGRAFE == "F").First();
                    return new StringBuilder()
                                .Append(pf.NOMINATIVO)
                                .Append(" ")
                                .Append(pf.NOME)
                                .Append(" ")
                                .Append(pf.CODICEFISCALE)
                                .ToString()
                                .Trim();
                }

                var pg = this._protocollo.DatiProtocollo.Mittenti.Anagrafe.Where(x => x.TIPOANAGRAFE == "G");
                if (pg.Any()) 
                {
                    return new StringBuilder()
                                .Append(pg.First().NOMINATIVO)
                                .Append(" ")
                                .Append( String.IsNullOrEmpty(pg.First().PARTITAIVA) ? pg.First().CODICEFISCALE : pg.First().PARTITAIVA)
                                .ToString()
                                .Trim();
                }

                return this._protocollo.DatiProtocollo.Mittenti.Amministrazione
                        .Select(x => new StringBuilder()
                                            .Append(x.AMMINISTRAZIONE)
                                            .Append(" ")
                                            .Append(x.PARTITAIVA)
                        )
                        .First()
                        .ToString()
                        .Trim();
            }
        }

        public override string OggettoFascicolo 
        {
            get
            {
                var codiceMailTipo = new IstanzeMgr(this._protocollo.Db).GetTestoTipoFascicoloFromIstanza(this._protocollo.Idcomune, this._protocollo.CodiceIstanza.Value);

                if (String.IsNullOrEmpty(codiceMailTipo)) 
                {
                    return null;
                }
                var mailTipo = new MailTipoCustomService(this._protocollo, Convert.ToInt32(codiceMailTipo)).GetMailTipo();
                return mailTipo.Oggetto;
            }
        }

        public override string CodiceSerie
        {
            get 
            {
                var serie = new IstanzeMgr(this._protocollo.Db).GetNumeroFascicoloFromIstanza(this._protocollo.Idcomune, this._protocollo.CodiceIstanza.Value);

                if (String.IsNullOrEmpty(serie)) 
                {
                    serie = this._protocollo.Configurazione.SerieFascicoliVerticalizzazione;
                }

                return serie;
            }
        }

        public override IdFolder idFolder { get; }

        public override IEnumerable<OggettiMetadati> GetMetadati(int codiceOggetto)
        {
            return new OggettiMetadatiMgr(this._protocollo.Db).GetMetadatiVerificaFirma(this._protocollo.Idcomune, codiceOggetto);
        }
    }
}
