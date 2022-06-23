using System;
using System.Globalization;
using Init.SIGePro.Data;
using System.IO;
using PersonalLib2.Data;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using log4net;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System.ServiceModel;
using System.Reflection;
using Init.SIGePro.Manager;
using Init.SIGePro.Protocollo.Manager;
using Init.SIGePro.Verticalizzazioni;
using System.Linq;
using Init.SIGePro.Protocollo.ProtocolloServices;
using Init.SIGePro.Protocollo.ProtocolloEnumerators;
using Init.SIGePro.Protocollo.WsDataClass;
using System.Collections;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using Init.SIGePro.Protocollo.Storico;

namespace Init.SIGePro.Protocollo
{
    /// <summary>
    /// Descrizione di riepilogo per ProtocolloBase.
    /// </summary>
    public abstract class ProtocolloBase : IProtocolloStorico
    {
        public DateTime? DataProtocollo { get; set; }
        
        //public string CodIstanza { get; set; }
        //public string CodMovimento { get; set; }
        public string CodAmministrazione { get; set; }
        public string MotivoAnnullamento { get; set; }
        public string Stampante { get; set; }
        public int NumeroCopie { get; set; }
        public string NumProtocollo { get; set; }
        public string IdProtocollo { get; set; }
        public string Operatore { get; set; }
        public string Ruolo { get; set; }
        public string Uo { get; set; }
        public bool AggiungiAnno { get; set; }
        public string TempPath { get; set; }
        public string ProxyAddress { get; set; }
        public ProtocolloEnum.TipoProvenienza Provenienza { get; set; }
        public bool ModificaNumero { get; set; }
        public bool GestisciFascicolazione { get; set; }
        public string IdAllegato { get; set; }
        public ProtocolloEnum.Source TipoInserimento { get; set; }
        public ResolveDatiProtocollazioneService DatiProtocollo { get; set; }
        public List<IAnagraficaAmministrazione> Anagrafiche { get; set; }
        public bool EstraiEml { get; set; }
        public string[] EscludiFileDaEml { get; set; }
        public bool EstraiZip { get; set; }
        public string[] ZipExtensions { get; set; }
        public bool ValorizzaDataRicezioneSpedizione { get; set; }
        public string EncodingCharset { get; set; }

        public string AnnoProtocollo
        {
            get
            {
                if (String.IsNullOrEmpty(_annoProtocollo))
                    return DataProtocollo.HasValue ? DataProtocollo.Value.Year.ToString() : "";

                return _annoProtocollo;
            }
            set{ _annoProtocollo = value; }
        }

        protected ProtocolloLogs _protocolloLogs = null;
        protected ProtocolloSerializer _protocolloSerializer = null;

        string _annoProtocollo;

        public ProtocolloBase()
        {
            //_protocolloLogs = new ProtocolloLogs(this.DatiProtocollo, this.GetType());
            //_protocolloSerializer = new ProtocolloSerializer(_protocolloLogs);
        }

        public virtual void InizializzaProtocolloBase(ResolveDatiProtocollazioneService datiProtocolloService)
        {
            this.DatiProtocollo = datiProtocolloService;
            _protocolloLogs = new ProtocolloLogs(this.DatiProtocollo, this.GetType());
            _protocolloSerializer = new ProtocolloSerializer(_protocolloLogs);
        }

        protected string GetUfficioRegistro(string codiceRegistro)
        {
            if (String.IsNullOrEmpty(codiceRegistro))
                throw new Exception("IL CODICE REGISTRO NON E' STATO VALORIZZATO");

            var mgr = new ProtocolloUfficiRegistriMgr(this.DatiProtocollo.Db);
            var uffReg = mgr.GetById(codiceRegistro, this.DatiProtocollo.IdComune, this.DatiProtocollo.Software, this.DatiProtocollo.CodiceComune);

            if (uffReg == null)
                throw new Exception(String.Format("UFFICIO NON TROVATO PER IL REGISTRO {0}", codiceRegistro));

            if (String.IsNullOrEmpty(uffReg.Codiceufficio))
                throw new Exception(String.Format("CODICE UFFICIO NON VALORIZZATO PER IL REGISTRO {0}", codiceRegistro));

            return uffReg.Codiceufficio;
        }

        #region Metodi virtuali

        public abstract DatiProtocolloRes Protocollazione(DatiProtocolloIn protoIn);

        public virtual CreaUnitaDocumentaleResponse CreaUnitaDocumentale(string tipoDocumento, IEnumerable<ProtocolloAllegati> allegati)
        {
            string message = "Metodo CreaUnitaDocumentale non implementato";
            _protocolloLogs.Error(message);

            throw new NotImplementedException(message);
        }

        public virtual DatiProtocolloRes Registrazione(string registro, DatiProtocolloIn request)
        {
            string message = "Metodo Registrazione non implementato";
            _protocolloLogs.Error(message);

            throw new NotImplementedException(message);
        }

        public virtual void AggiungiAllegati(string idProtocollo, string numeroProtocollo, DateTime? dataProtocollo, IEnumerable<ProtocolloAllegati> allegati)
        {
            string message = "Metodo AggiungiAllegati non implementato";
            _protocolloLogs.Error(message);

            throw new NotImplementedException(message);
        }

        public virtual void InvioPec(string idProtocollo, string numeroProtocollo, string annoProtocollo)
        {
            string message = "Metodo Invio Pec";
            _protocolloLogs.Error(message);

            throw new NotImplementedException(message);
        }

        #region Metodi per la lettura di un protocollo
        public virtual DatiProtocolloLetto LeggiProtocollo(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            string message = "Il sistema di protocollazione attivo non supporta il metodo per la lettura di un protocollo";
            _protocolloLogs.Error(message);

            throw new Exception(message);
        }
        #endregion

        #region Metodi per la lettura degli allegati

        public virtual AllOut LeggiAllegato()
        {
            string message = "IL SISTEMA DI PROTOCOLLAZIONE ATTIVO NON SUPPORTA IL METODO PER LA LETTURA DEGLI ALLEGATI";
            throw new Exception(message);
        }

        public AllOut GetAllegato()
        {
            DatiProtocolloLetto protLetto = this.LeggiProtocollo(IdProtocollo, AnnoProtocollo, NumProtocollo);
            
            foreach (AllOut allegati in protLetto.Allegati)
            {
                if (allegati.IDBase == IdAllegato)
                {
                    return allegati;
                }
            }
            return null;
        }

        public AllOut GetAllegatoStorico()
        {
            this._protocolloLogs.Debug($"Inizio recupero allegato storico con IDBase: {IdAllegato}");
            this._protocolloLogs.Debug($"---- Inizio Chiamata a leggiprotocollostorico con IdProtocollo: {IdProtocollo}, AnnoProtocollo: {AnnoProtocollo}, NumProtocollo: {NumProtocollo}");
            DatiProtocolloLetto protLetto = this.LeggiProtocolloStorico(IdProtocollo, AnnoProtocollo, NumProtocollo);
            this._protocolloLogs.Debug($"---- Fine Chiamata a leggiprotocollostorico con IdProtocollo: {IdProtocollo}, AnnoProtocollo: {AnnoProtocollo}, NumProtocollo: {NumProtocollo}");

            foreach (AllOut allegati in protLetto.Allegati)
            {
                this._protocolloLogs.Debug($"IDBase allegato: {allegati.IDBase}");
                if (allegati.IDBase == IdAllegato)
                {
                    this._protocolloLogs.Debug($"Fine recupero allegato storico trovato IDBase: {IdAllegato}");
                    return allegati;
                }
            }
            this._protocolloLogs.Debug($"Fine recupero allegato storico con IDBase: {IdAllegato}, ALLEGATO NON TROVATO");
            return null;
        }



        #endregion

        #region Metodi per creare la copia di un protocollo
        public virtual DatiProtocolloRes CreaCopie()
        {
            DatiProtocolloRes pProtocollo = new DatiProtocolloRes();
            return pProtocollo;
        }

        #endregion

        #region Metodi per mettere alla firma un documento
        public virtual DatiProtocolloRes MettiAllaFirma(Data.DatiProtocolloIn pProt)
        {
            string message = "il sistema di protocollazione attivo non supporta la possibilit� di mettere alla firma un documento";
            _protocolloLogs.Error(message);
            throw new Exception(message);
        }

        #endregion

        #region Metodi per la stampa di un'etichetta

        public virtual DatiEtichette StampaEtichette(string idProtocollo, DateTime? dataProtocollo, string numeroProtocollo, int numeroCopie, string stampante)
        {
            string message = "Il sistema di protocollazione attivo non supporta il metodo per la stampa dell'etichetta";
            _protocolloLogs.Error(message);

            throw new Exception(message);
        }

        #endregion

        public virtual int GetIdClassificaByCodice( string codiceClassifica)
        {
            _protocolloLogs.Debug("Recupero PROTOCOLLO_CLASSIFICHE.ID di una singola classifica partendo da PROTOCOLLO_CLASSIFICHE.CODICE");
            var mgr = new ProtocolloClassificheMgr(this.DatiProtocollo.Db);

            var classifica = mgr.GetByCodice(this.DatiProtocollo.IdComune, this.DatiProtocollo.Software, codiceClassifica.ToUpper() );

            if( classifica == null )
            {
                classifica = mgr.GetByCodice(this.DatiProtocollo.IdComune, "TT", codiceClassifica.ToUpper());
            }

            if (classifica == null)
            {
                var m = $"LA CLASSIFICA CON IDCOMUNE {this.DatiProtocollo.IdComune} E CODICE {codiceClassifica} NON ESISTE NELLA TABELLA PROTOCOLLO_CLASSIFICHE";
                _protocolloLogs.Debug(m);
                throw new Exception(m);
            }

            return classifica.Id.Value;
        }

        public virtual ListaTipiClassificaClassifica GetClassificaById( int idClassifica )
        {
            _protocolloLogs.Debug("Recupero del titolario dalla tabella PROTOCOLLO_CLASSIFICHE di una singola classifica");
            var mgr = new ProtocolloClassificheMgr(this.DatiProtocollo.Db);

            var classifica = mgr.GetById(this.DatiProtocollo.IdComune, idClassifica);

            if (classifica == null)
            {
                _protocolloLogs.Debug($"LA CLASSIFICA CON IDCOMUNE {this.DatiProtocollo.IdComune} E ID {idClassifica} NON ESISTE NELLA TABELLA PROTOCOLLO_CLASSIFICHE");
            }

            return new ListaTipiClassificaClassifica {
                Codice = classifica.Codice,
                Descrizione = classifica.Descrizione,
                Ordinamento = ( String.IsNullOrEmpty(classifica.Ordinamento) ) ? 0 : Convert.ToInt32(classifica.Ordinamento)
            };
        }

        public virtual ListaTipiClassifica GetClassifiche()
        {
            _protocolloLogs.Debug("Recupero del titolario dalla tabella PROTOCOLLO_CLASSIFICHE");

            var mgr = new ProtocolloClassificheMgr(this.DatiProtocollo.Db);
            var list = mgr.GetBySoftwareCodiceComune(this.DatiProtocollo.IdComune, this.DatiProtocollo.Software, this.DatiProtocollo.CodiceComune);

            var titolario = list.Select(x => new ListaTipiClassificaClassifica { Codice = x.Codice, Descrizione = x.Descrizione, Ordinamento = String.IsNullOrEmpty(x.Ordinamento) ? 0 : Convert.ToInt32(x.Ordinamento) }).OrderBy(x => x.Ordinamento);

            _protocolloLogs.DebugFormat("Numero classifiche recuperate: {0}", titolario.Count());

            if (titolario.Count() == 0)
            {
                _protocolloLogs.Debug("LA TABELLA PROTOCOLLO_CLASSIFICHE E' VUOTA");
                return new ListaTipiClassifica();
            }

            return new ListaTipiClassifica { Classifica = titolario.ToArray() };
        }
        #endregion

        #region Metodi usati per l'annullamento di un protocollo
        public virtual ListaMotiviAnnullamento GetMotivoAnnullamento()
        {
            ListaMotiviAnnullamento pListaMotiviAnnullamento = new ListaMotiviAnnullamento();
            return pListaMotiviAnnullamento;
        }

        public virtual DatiProtocolloAnnullato IsAnnullato(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            DatiProtocolloAnnullato datiProtAnn = new DatiProtocolloAnnullato();
            datiProtAnn.NoteAnnullamento = "";
            return datiProtAnn;
        }

        public virtual void AnnullaProtocollo(string idProtocollo, string annoProtocollo, string numeroProtocollo, string motivoAnnullamento, string noteAnnullamento)
        {
            string message = "Il sistema di protocollazione attivo non supporta il metodo per l'annullamento di un protocollo";
            _protocolloLogs.Error(message);
            throw new Exception(message);
        }
        #endregion

        #region Metodi usati per la fascicolazione di un protocollo
        public virtual ListaFascicoli GetFascicoli(Fascicolo fascicolo)
        {
            var listaFascicolo = new ListaFascicoli();
            return listaFascicolo;
        }

        public virtual DatiProtocolloFascicolato IsFascicolato(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            DatiProtocolloFascicolato datiProtFasc = new DatiProtocolloFascicolato();
            datiProtFasc.NoteFascicolo = "";
            return datiProtFasc;
        }

        public virtual DatiFascicolo Fascicola(Fascicolo fascicolo)
        {
            string message = "il sistema di protocollazione attivo non supporta il metodo per la creazione di un fascicolo";
            _protocolloLogs.Error(message);
            throw new Exception(message);
        }

        /// <summary>
        /// Controlla se il numero e anno passato coincidono con i dati letti dal sistema di protocollazione.
        /// In Iride non c'� bisogno di questo controllo perch� il numero restituito se viene creata la copia � 0/0
        /// </summary>
        /// <param name="annoProtocollo"></param>
        /// <param name="numeroProtocollo"></param>
        /// <param name="idProtocollo"></param>
        /// <param name="pDatiProtocolloLetto"></param>
        public virtual void CheckProtocolloLetto(string annoProtocollo, string numeroProtocollo, string idProtocollo, DatiProtocolloLetto pDatiProtocolloLetto)
        {
            try
            {
                if (!string.IsNullOrEmpty(idProtocollo) && !string.IsNullOrEmpty(annoProtocollo) && !string.IsNullOrEmpty(numeroProtocollo))
                {
                    string[] aNumProtSplit = numeroProtocollo.Split(new Char[] { '/' });

                    int resNum;
                    var parse = int.TryParse(aNumProtSplit[0], out resNum);

                    if (!parse)
                    {
                        throw new Exception("IL NUMERO DI PROTOCOLLO INVIATO NON E' DI TIPO NUMERICO");
                    }

                    int resNumLetto;
                    var parseLetto = int.TryParse(pDatiProtocolloLetto.NumeroProtocollo, out resNumLetto);

                    if (!parseLetto)
                    {
                        throw new Exception("IL NUMERO DI PROTOCOLLO RILETTO NON E' DI TIPO NUMERICO");
                    }

                    if (resNumLetto != resNum)
                    {
                        throw new Exception("IL NUMERO DEL PROTOCOLLO RILETTO NON COINCIDE CON QUELLO PASSATO!" + "NUMERO RILETTO/ANNO RILETTO: " + pDatiProtocolloLetto.NumeroProtocollo + "/" + pDatiProtocolloLetto.AnnoProtocollo + ", numero passato/anno passato: " + aNumProtSplit[0] + "/" + annoProtocollo);
                    }

                    if (pDatiProtocolloLetto.AnnoProtocollo != annoProtocollo)
                    {
                        throw new Exception("L'ANNO DEL PROTOCOLLO RILETTO NON COINCIDE CON QUELLO PASSATO!" + "NUMERO RILETTO/ANNO RILETTO: " + pDatiProtocolloLetto.NumeroProtocollo + "/" + pDatiProtocolloLetto.AnnoProtocollo + ", numero passato/anno passato: " + aNumProtSplit[0] + "/" + annoProtocollo);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual DatiFascicolo CambiaFascicolo(Fascicolo fascicolo)
        {
            string message = "il sistema di protocollazione attivo non supporta il metodo per il cambiamento di un fascicolo";
            _protocolloLogs.Error(message);
            throw new Exception(message);
        }
        #endregion

        #region Metodi per ottenere la lista dei tipi documento

        public virtual ListaTipiDocumento GetTipiDocumento()
        {
            _protocolloLogs.Debug("Recupero dei tipi documento dalla tabella PROTOCOLLO_TIPIDOCUMENTO");

            var mgr = new ProtocolloTipiDocumentoMgr(DatiProtocollo.Db);
            var list = mgr.GetBySoftwareCodiceComune(DatiProtocollo.IdComune, DatiProtocollo.Software, DatiProtocollo.CodiceComune);

            var tipiDocs = list.Select(x => new ListaTipiDocumentoDocumento{ Codice = x.Codice, Descrizione = x.Descrizione });

            _protocolloLogs.DebugFormat("Numero documenti recuperato: {0}", tipiDocs.Count());
            if (tipiDocs.Count() == 0)
                return new ListaTipiDocumento();

            return new ListaTipiDocumento { Documento = tipiDocs.ToArray() };
        }

        public virtual ListaTipiDocumento CreaListaTipiDocumento(object ds)
        {
            ListaTipiDocumento pListaTipiDocumento = new ListaTipiDocumento();

            pListaTipiDocumento.Documento = new ListaTipiDocumentoDocumento[((DataSet)ds).Tables[0].Rows.Count];

            int iCount = 0;
            foreach (DataRow dr in ((DataSet)ds).Tables[0].Rows)
            {
                pListaTipiDocumento.Documento[iCount] = new ListaTipiDocumentoDocumento();

                pListaTipiDocumento.Documento[iCount].Codice = dr["CODICE"].ToString();
                pListaTipiDocumento.Documento[iCount].Descrizione = dr["DESCRIZIONE"].ToString();
                iCount++;
            }

            return pListaTipiDocumento;
        }

        public virtual DatiProtocolloLetto LeggiProtocolloStorico(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            throw new NotImplementedException("Fare l'ovveride del LeggiProtocolloStorico nello specifico connettore attivato in Verticalizzazione");
        }

        public virtual AllOut LeggiAllegatoStorico()
        {
            throw new NotImplementedException("Fare l'ovveride del LeggiAllegatoStorico nello specifico connettore attivato in Verticalizzazione");
        }

        #endregion

    }
}