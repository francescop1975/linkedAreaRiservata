using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using Init.SIGePro.Data;
using Init.SIGePro.Exceptions.Protocollo;
using Init.SIGePro.Manager.Manager;
using Init.SIGePro.Verticalizzazioni;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System.Linq;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using Init.SIGePro.Protocollo.ProtocolloFactories;
using Init.SIGePro.Protocollo.ProtocolloServices;
using System.Collections;
using Init.SIGePro.Authentication;
using Init.SIGePro.Protocollo.ProtocolloEnumerators;
using Init.SIGePro.Protocollo.WsDataClass;
using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.Metadati;
using Init.SIGePro.Protocollo.ProtocolloServices.MailTipo;

namespace Init.SIGePro.Protocollo.Manager
{
    public partial class ProtocolloMgr : IDisposable
    {
        ResolveDatiProtocollazioneService _datiProtocollazione;
        ProtocolloLogs _protocolloLogs;
        ProtocolloSerializer _protocolloSerializer;

        DatiProtocolloIn _protoIn;
        Fascicolo _fascicolo;
        Dati _dati;

        VerticalizzazioneProtocolloAttivo _verticalizzazioneProtocolloAttivo;
        VerticalizzazioneProtocolloStorico _verticalizzazioneProtocolloStorico;

        // Istanza dell'oggetto protocollo corrente creata via reflection
        ProtocolloBase _protocolloAttivo;
        // Istanza dell'oggetto protocollo da utilizzare per legere i dati di protocollazione storici
        ProtocolloBase _protocolloStorico;

        public ProtocolloMgr(AuthenticationInfo authInfo, string software, string codiceComune = "", ProtocolloEnum.AmbitoProtocollazioneEnum ambito = ProtocolloEnum.AmbitoProtocollazioneEnum.NESSUNO, Istanze istanza = null, Movimenti movimento = null, PecInbox datiPec = null)
        {
            if (String.IsNullOrEmpty(software))
                throw new ArgumentNullException("software");

            if (authInfo == null)
                throw new ArgumentNullException("authInfo");

            if (String.IsNullOrEmpty(authInfo.IdComune))
                throw new ArgumentNullException("authInfo.IdComune");

            var dataBase = authInfo.CreateDatabase();

            var idComune = authInfo.IdComune;
            var idComuneAlias = authInfo.Alias;
            var codOperatore = authInfo.CodiceResponsabile;
            var token = authInfo.Token;

            this._datiProtocollazione = new ResolveDatiProtocollazioneService(idComune, idComuneAlias, software, dataBase, istanza, movimento, codOperatore, ambito, token, codiceComune, datiPec);

            this._protocolloLogs = new ProtocolloLogs(this._datiProtocollazione, this.GetType());
            this._protocolloSerializer = new ProtocolloSerializer(_protocolloLogs);

            var attivazioneProtoService = new AttivazioneProtocolloService(this._datiProtocollazione, this._protocolloLogs);
            _verticalizzazioneProtocolloAttivo = attivazioneProtoService.VertProtocolloAttivo;
            _verticalizzazioneProtocolloStorico = attivazioneProtoService.VertProtocolloStorico;

            _protocolloAttivo = attivazioneProtoService.AttivaProtocollo();
            _protocolloAttivo.ProxyAddress = _verticalizzazioneProtocolloAttivo.ProxyAddress;

            if (this._verticalizzazioneProtocolloStorico.Attiva)
            {
                _protocolloStorico = attivazioneProtoService.AttivaProtocolloStorico();
            }
        }

        private void SetProtocollo(ProtocolloEnum.TipoProvenienza provenienza, int iSource)
        {

            var tipoInserimento = (ProtocolloEnum.Source)iSource;

            var datiProtoAmbito = AmbitoFactory.Create(this._datiProtocollazione);

            _protocolloLogs.DebugFormat("La variabile datiProtoAmbito è stata istanziata. L'ambito è {0}", this._datiProtocollazione.TipoAmbito);


            _protocolloLogs.Debug("Valorizzazione dei dati di base relativi alla classe ProtocolloBase");

            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo.Operatore;
            _protocolloAttivo.Provenienza = provenienza;
            _protocolloAttivo.AggiungiAnno = GetParamVertBool(_verticalizzazioneProtocolloAttivo.Aggiungianno, "AGGIUNGIANNO");
            _protocolloAttivo.GestisciFascicolazione = GetParamVertBool(_verticalizzazioneProtocolloAttivo.GestisciFascicolazione, "GESTISCIFASCICOLAZIONE");
            _protocolloAttivo.TempPath = ConfigurationManager.AppSettings["TempPath"];
            _protocolloAttivo.TipoInserimento = tipoInserimento;
            _protocolloAttivo.EncodingCharset = _verticalizzazioneProtocolloAttivo.Encoding;

            _protocolloLogs.DebugFormat("Valorizzazione dei dati relativi al TipoDocumento, valore TipoDocumento: {0}", _dati.TipoDocumento);

            if (!String.IsNullOrEmpty(_dati.TipoDocumento))
                _protoIn.TipoDocumento = _dati.TipoDocumento;
            else
            {
                string tipoDocumento = "";

                if (_datiProtocollazione.Istanza != null)
                {
                    // var mgr = new ProtocolloTipiDocumentoMgr(this._datiProtocollazione.Db);
                    // tipoDocumento = mgr.GetCodiceFromAlberoProcProtocollo(_datiProtocollazione.CodiceInterventoProc.Value, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
                    tipoDocumento = RepositoryGetCodiceTipoDocumentoFromAlberoProcProtocollo(_datiProtocollazione.CodiceInterventoProc.Value);
                }

                if (String.IsNullOrEmpty(tipoDocumento))
                {
                    string tipoDocumentoDefault = tipoInserimento == ProtocolloEnum.Source.ON_LINE ? _verticalizzazioneProtocolloAttivo.Tipodocumentodefault : _verticalizzazioneProtocolloAttivo.Tipodocumentodefaultbo;

                    if (!String.IsNullOrEmpty(tipoDocumentoDefault))
                        _protoIn.TipoDocumento = tipoDocumentoDefault;
                    else
                        _protocolloLogs.Warn("TIPO DOCUMENTO DEFAULT BACKOFFICE NON VALORIZZATO");
                }
                else
                    _protoIn.TipoDocumento = tipoDocumento;
            }
            _protocolloLogs.Debug("Fine valorizzazione dei dati relativi al TipoDocumento");

            _protocolloLogs.DebugFormat("Tipo Smistamento: {0}, parametro TipoSmistamentoDefault della verticalizzazione Protocollo_Attivo: {1}", _dati.TipoSmistamento, _verticalizzazioneProtocolloAttivo.Tiposmistamentodefault);
            _protoIn.TipoSmistamento = !String.IsNullOrEmpty(_dati.TipoSmistamento) ? _dati.TipoSmistamento : _verticalizzazioneProtocolloAttivo.Tiposmistamentodefault;
            _protocolloLogs.DebugFormat("Valorizzazione dei dati relativi all'oggetto del protocollo, valore oggetto: {0}", _dati.Oggetto);

            _protoIn.DataRegistrazione = datiProtoAmbito.DataRegistrazione;

            _protocolloLogs.Debug("Inizio creazione del WS per l'invio della mail");
            _protoIn.Oggetto = _dati.Oggetto;
            _protoIn.Corpo = String.Empty;

            IMailTipoService mailTipoSrv = MailTipoServiceFactory.Create(this._datiProtocollazione, _protocolloLogs, _protocolloSerializer);
            _protocolloLogs.DebugFormat("Fine creazione del WS per il recupero di oggetto e corpo dalla mailtipo. Il servizio è valorizzato? {0}", mailTipoSrv != null);

            if (mailTipoSrv != null) 
            {
                var mailTipo = mailTipoSrv.GetMailTipo();
                _protocolloLogs.DebugFormat("Fine recupero della mail tipo via web service. L'oggetto è valorizzato? {0}", mailTipo != null);

                if (mailTipo != null)
                {
                    _protoIn.Oggetto = String.IsNullOrEmpty(_dati.Oggetto) ? mailTipo.Oggetto : _dati.Oggetto;
                    _protoIn.Corpo = mailTipo.Corpo;
                }
            }
            

            if (_verticalizzazioneProtocolloAttivo.OggettoUppercase == "1")
                _protoIn.Oggetto = _protoIn.Oggetto.ToUpper();

            if (!String.IsNullOrEmpty(_verticalizzazioneProtocolloAttivo.NumCaratteriOggetto))
            {
                int resultParse = Int32.MinValue;
                bool parseOggetto = Int32.TryParse(_verticalizzazioneProtocolloAttivo.NumCaratteriOggetto, out resultParse);

                if (parseOggetto && _protoIn.Oggetto.Length > resultParse)
                    throw new Exception("LA LUNGHEZZA DELL'OGGETTO SUPERA LA QUOTA MASSIMA CONSENTITA DAL PARAMETRO NUM_CARATTERI_OGGETTO DELLA VERTICALIZZAZIONE PROTOCOLLO_ATTIVO");

            }

            _protocolloLogs.Debug("Fine valorizzazione dei dati relativi all'oggetto del protocollo");
            _protocolloLogs.DebugFormat("Valorizzazione dei dati relativi al flusso del protocollo");

            if (!String.IsNullOrEmpty(_dati.Flusso))
                _protoIn.Flusso = _dati.Flusso;
            else
            {
                if (this._datiProtocollazione.Movimento != null && _dati.Mittenti == null)
                {
                    if (this._datiProtocollazione.Movimento.CREATO_DA_STC == 1)
                    {
                        _protoIn.Flusso = "A";
                    }

                    if (!String.IsNullOrEmpty(this._datiProtocollazione.Movimento.TIPOMOVIMENTO))
                    {
                        // var tipiMovimentoMgr = new TipiMovimentoMgr(this._datiProtocollazione.Db);
                        // var tipiMovimento = tipiMovimentoMgr.GetById(this._datiProtocollazione.Movimento.TIPOMOVIMENTO, this._datiProtocollazione.IdComune);
                        var tipiMovimento = RepositoryGetTipoMovimentoById(this._datiProtocollazione.Movimento.TIPOMOVIMENTO);


                        if (tipiMovimento != null && tipiMovimento.FLAG_STC == 1)
                        {
                            _protocolloLogs.Info($"VERIFICA DELLA PRESENZA DEL PROTOCOLLO NELL'ISTANZA, MOVIMENTO: {tipiMovimento.Tipomovimento}, MOVIMENTO IMPOSTATO NEI PARAMETRI DELLA REGOLA PROTOCOLLO_ATTIVO: {_verticalizzazioneProtocolloAttivo.TipoMovRicevuta}");
                            if (_verticalizzazioneProtocolloAttivo.TipoMovRicevuta == tipiMovimento.Tipomovimento)
                            {
                                if (this._datiProtocollazione.Istanza != null && String.IsNullOrEmpty(this._datiProtocollazione.Istanza.NUMEROPROTOCOLLO))
                                {
                                    throw new Exception("NON E' POSSIBILE PROTOCOLLARE LA RICEVUTA IN QUANTO L'ISTANZA NON E' STATA PROTOCOLLATA");
                                }
                            }

                            _protocolloLogs.InfoFormat("ESTRAPOLAZIONE DEI TIPI MOVIMENTI STC MAPPING RELATIVAMENTE AL TIPO MOVIMENTO {0}", tipiMovimento.Tipomovimento);
                            // var mgrMapping = new TipiMovimentoStcMappingProtocolloMgr(this._datiProtocollazione.Db, this._datiProtocollazione.IdComune);
                            // var mappingProtocollo = mgrMapping.GetDatiProtocollo(tipiMovimento.Tipomovimento);
                            var mappingProtocollo = RepositoryGetDatiProtocolloByTipoMovimento(tipiMovimento.Tipomovimento);
                            _protocolloLogs.InfoFormat("MAPPING RESTITUITO, FLUSSO: {0}, TIPODOCUMENTO: {1}, CODICE MAIL TIPO PER l'OGGETTO: {2}", mappingProtocollo.Flusso, mappingProtocollo.TipoDocumento, mappingProtocollo.OggettoMailTipo);

                            _protoIn.Flusso = mappingProtocollo.Flusso; ///IN GENERE IMPOSTATO A "P" (PARTENZA), IN TEORIA POTREBBE ESSERE ANCHE INTERNO.
                            _protoIn.TipoDocumento = mappingProtocollo.TipoDocumento;

                            if (mappingProtocollo.OggettoMailTipo.HasValue)
                            {
                                _protocolloLogs.Info("RECUPERO DELLA MAIL TIPO DA INSERIRE NELL'OGGETTO");
                                var param = new ParametriInterventoProtocolloProtocolloService(mappingProtocollo.OggettoMailTipo.Value);
                                
                                var wsMailTipo = new MailTipoMovimentoService(param, this._datiProtocollazione, _protocolloSerializer, _protocolloLogs);
                                var testoTipo = wsMailTipo.GetMailTipo();

                                _protocolloLogs.InfoFormat("MAIL TIPO {0}: OGGETTO [{1}] CORPO [{2}]", mappingProtocollo.OggettoMailTipo.Value, testoTipo.Oggetto, testoTipo.Corpo);

                                _protoIn.Oggetto = String.IsNullOrEmpty(testoTipo.Oggetto) ? _protoIn.Oggetto : testoTipo.Oggetto;
                                _protoIn.Corpo = String.IsNullOrEmpty(testoTipo.Corpo) ? _protoIn.Corpo : testoTipo.Corpo;

                            }

                            if (mappingProtocollo.AmministrazioneMittente.HasValue)
                            {
                                _dati.Mittenti = new DatiMittenti
                                {
                                    Amministrazione = new DatiAnagrafici[]
                                    {
                                    new DatiAnagrafici
                                    {
                                        Cod = mappingProtocollo.AmministrazioneMittente.ToString(),
                                        Mezzo = _verticalizzazioneProtocolloAttivo.MezzoDefault,
                                        ModalitaTrasmissione = _verticalizzazioneProtocolloAttivo.ModalitaTrasmissioneDefault
                                    }
                                    }.ToList()
                                };
                            }

                        }
                    }
                }
                if (String.IsNullOrEmpty(_protoIn.Flusso))
                {
                    if (!String.IsNullOrEmpty(_verticalizzazioneProtocolloAttivo.Flussodefault))
                    {
                        _protoIn.Flusso = _verticalizzazioneProtocolloAttivo.Flussodefault;
                        if (_dati.Mittenti != null && _dati.Mittenti.Amministrazione != null && _dati.Mittenti.Amministrazione.Count() > 0)
                        {
                            // var ammMgr = new AmministrazioniMgr(this._datiProtocollazione.Db);
                            // var amm = ammMgr.GetByIdProtocollo(this._datiProtocollazione.IdComune, Convert.ToInt32(_dati.Mittenti.Amministrazione[0].Cod), this._datiProtocollazione.Software, this._datiProtocollazione.CodiceComune);
                            var codiceAmministrazione = Convert.ToInt32(_dati.Mittenti.Amministrazione[0].Cod);
                            var amm = RepositoryGetAmministrazioneByIdProtocollo(codiceAmministrazione);

                            if (!String.IsNullOrEmpty(amm.PROT_UO))
                                _protoIn.Flusso = ProtocolloConstants.COD_INTERNO;
                        }
                    }
                    else
                        throw new Exception("LA VERTICALIZZAZIONE PROTOCOLLO_ATTIVO NON HA SETTATO IL CAMPO FLUSSODEFAULT");
                }
            }
            _protocolloLogs.Debug("Fine valorizzazione dei dati relativi al flusso del protocollo");

            if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA && (_protoIn.Flusso == "P"))
                throw new Exception("NON È POSSIBILE PROTOCOLLARE UN'ISTANZA CON UN FLUSSO IN USCITA!");

            _protocolloLogs.DebugFormat("Recupero dei dati relativi alla classifica: {0}", _dati.Classifica);
            string classifica = _dati.Classifica;

            if (!String.IsNullOrEmpty(classifica))
                _protoIn.Classifica = classifica;
            else
            {
                if (_datiProtocollazione.Istanza != null)
                {
                    // var alberoProcMgr = new AlberoProcMgr(this._datiProtocollazione.Db);
                    // _protoIn.Classifica = alberoProcMgr.GetClassificaProtocolloFromAlberoProcProtocollo(_datiProtocollazione.CodiceInterventoProc.Value, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
                    _protoIn.Classifica = RepositoryGetClassificaProtocolloByCodiceIntervento(_datiProtocollazione.CodiceInterventoProc.Value);

                    if (String.IsNullOrEmpty(_protoIn.Classifica))
                        _protoIn.Classifica = _verticalizzazioneProtocolloAttivo.ClassificadefaultBo;
                }
            }

            _protocolloLogs.DebugFormat("Fine recupero dei dati relativi alla classifica: {0}", _protoIn.Classifica);

            if (String.IsNullOrEmpty(_protoIn.Classifica))
                throw new Exception("NON È STATA SETTATA LA CLASSIFICA DI PROTOCOLLAZIONE NELL'ALBERO DEI PROCEDIMENTI");

            if (!String.IsNullOrEmpty(_dati.NumProtMitt))
                _protoIn.NumProtMitt = _dati.NumProtMitt;

            if (!String.IsNullOrEmpty(_dati.DataProtMitt))
                _protoIn.DataProtMitt = _dati.DataProtMitt;

            _protocolloLogs.Debug("Recupero dei dati relativi agli allegati");
            //Setto per l'oggetto Protocollo la proprietà Allegati
            SetAllegati(iSource);
            _protocolloLogs.Debug("Fine recupero dei dati relativi agli allegati");

            _protocolloLogs.Debug("Recupero dei dati relativi ai mittenti");
            //Setto per l'oggetto Protocollo la proprietà Mittenti
            SetMittenti();
            _protocolloLogs.Debug("Fine recupero dei dati relativi ai mittenti");
            _protocolloLogs.Debug("Recupero dei dati relativi ai destinatari");
            //Setto per l'oggetto Protocollo la proprietà Destinatari
            SetDestinatari();
            _protocolloLogs.Debug("Fine recupero dei dati relativi ai destinatari");
            //Setto per l'oggetto Protocollo la proprietà DestinatariPerConoscenza
            //SetDestinatariPerConoscenza();
            _protocolloLogs.Debug("Fine settaggio della classe DatiProtocolloIn");

            if (_verticalizzazioneProtocolloAttivo.ValorizDataRicSpediz)
            {
                if (this._datiProtocollazione.Movimento != null)
                {
                    _protocolloAttivo.ValorizzaDataRicezioneSpedizione = (this._datiProtocollazione.Movimento.CREATO_DA_STC == 1);
                }
                else
                {
                    _protocolloAttivo.ValorizzaDataRicezioneSpedizione = (this._datiProtocollazione.Istanza != null);
                }
            }
        }

        private void SetMettiAllaFirma()
        {
            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo.Operatore;
            _protocolloAttivo.Provenienza = ProtocolloEnum.TipoProvenienza.BACKOFFICE;

            if (!string.IsNullOrEmpty(_dati.TipoDocumento))
                _protoIn.TipoDocumento = _dati.TipoDocumento;
            else
            {
                // var mgr = new ProtocolloTipiDocumentoMgr(this._datiProtocollazione.Db);
                // var tipoDocumentoBo = mgr.GetCodiceFromAlberoProcProtocollo(_datiProtocollazione.CodiceInterventoProc.Value, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
                var tipoDocumentoBo = RepositoryGetCodiceTipoDocumentoFromAlberoProcProtocollo(_datiProtocollazione.CodiceInterventoProc.Value);

                if (String.IsNullOrEmpty(tipoDocumentoBo))
                {
                    if (!String.IsNullOrEmpty(_verticalizzazioneProtocolloAttivo.Tipodocumentodefaultbo))
                        _protoIn.TipoDocumento = _verticalizzazioneProtocolloAttivo.Tipodocumentodefaultbo;
                    else
                        throw new ProtocolloException("LA VERTICALIZZAZIONE PROTOCOLLO_ATTIVO NON HA SETTATO IL CAMPO TIPODOCUMENTODEFAULTBO");
                }
                else
                    _protoIn.TipoDocumento = tipoDocumentoBo;
            }

            if (!String.IsNullOrEmpty(_dati.Oggetto))
            {
                _protoIn.Oggetto = _dati.Oggetto;
            }
            else
            {
                IMailTipoService mailTipoSrv = MailTipoServiceFactory.Create(this._datiProtocollazione, _protocolloLogs, _protocolloSerializer);
                var mailTipo = mailTipoSrv.GetMailTipo();
                _protoIn.Oggetto = mailTipo.Oggetto;
                _protoIn.Corpo = mailTipo.Corpo;
            }

            if (!string.IsNullOrEmpty(_dati.Flusso))
                _protoIn.Flusso = _dati.Flusso;
            else
            {
                if (!string.IsNullOrEmpty(_verticalizzazioneProtocolloAttivo.Flussodefault))
                    _protoIn.Flusso = _verticalizzazioneProtocolloAttivo.Flussodefault;
                else
                    throw new ProtocolloException("La verticalizzazione Protocollo_Attivo non ha settato il campo FlussoDefault");
            }

            if (!String.IsNullOrEmpty(_dati.Classifica))
                _protoIn.Classifica = _dati.Classifica;
            else
            {
                // var alberoMgr = new AlberoProcMgr(this._datiProtocollazione.Db);
                // _protoIn.Classifica = alberoMgr.GetClassificaProtocolloFromAlberoProcProtocollo(_datiProtocollazione.CodiceInterventoProc.Value, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
                _protoIn.Classifica = RepositoryGetClassificaProtocolloByCodiceIntervento(_datiProtocollazione.CodiceInterventoProc.Value);
            }

            if (String.IsNullOrEmpty(_protoIn.Classifica))
                throw new Exception("NON È STATA SETTATA LA CLASSIFICA DI PROTOCOLLAZIONE NELL'ALBERO DEI PROCEDIMENTI");

            if (!String.IsNullOrEmpty(_dati.NumProtMitt))
                _protoIn.NumProtMitt = _dati.NumProtMitt;

            if (!String.IsNullOrEmpty(_dati.DataProtMitt))
                _protoIn.DataProtMitt = _dati.DataProtMitt;

            SetAllegati();

            SetMittenti();

            SetDestinatari();
        }

        private List<ProtocolloAllegati> LeggiAllegatiDaNlaPec()
        {
            var nlaPec = new ProtocolloMgr.NlaPec(this._datiProtocollazione.Token, this._datiProtocollazione.Software, this._datiProtocollazione.IdComune);
            var identificativo = nlaPec.GetIdentificativo(Convert.ToInt32(this._datiProtocollazione.CodiceIstanza), this._datiProtocollazione.Db);
            if (!String.IsNullOrEmpty(identificativo))
            {
                return nlaPec.GetAllegatiEml(identificativo, DateTime.Now, DateTime.Now);
            }

            return new List<ProtocolloAllegati>();
        }

        private Allegato[] RisolviDocumentiIstanza()
        {
            //Protocollo un'istanza
            // var docIstanzaMgr = new DocumentiIstanzaMgr(this._datiProtocollazione.Db);
            // var docIstanza = new DocumentiIstanza();
            // docIstanza.CODICEISTANZA = this._datiProtocollazione.CodiceIstanza;
            // docIstanza.IDCOMUNE = this._datiProtocollazione.IdComune;
            // docIstanza.OthersWhereClause.Add("DOCUMENTIISTANZA.CODICEOGGETTO is not null");
            // 
            // List<DocumentiIstanza> pListDocIstanza = docIstanzaMgr.GetList(docIstanza);
            // int iCountDocIstanza = pListDocIstanza.Count;
            var pListDocIstanza = RepositoryGetDocumentiIstanzaConAllegato(Convert.ToInt32(this._datiProtocollazione.CodiceIstanza));
            var iCountDocIstanza = pListDocIstanza.Count;

            _protocolloLogs.DebugFormat("NUMERO ALLEGATI TROVATI SU DOCUMENTIISTANZA: {0}", iCountDocIstanza);

            // var istanzeAllMgr = new IstanzeAllegatiMgr(this._datiProtocollazione.Db);
            // var istanzeAll = new IstanzeAllegati();
            // istanzeAll.CODICEISTANZA = this._datiProtocollazione.CodiceIstanza;
            // istanzeAll.IDCOMUNE = this._datiProtocollazione.IdComune;
            // istanzeAll.OthersWhereClause.Add("ISTANZEALLEGATI.CODICEOGGETTO is not null");
            // List<IstanzeAllegati> pListIstanzeAll = istanzeAllMgr.GetList(istanzeAll);
            // int iCountIstanzeAll = pListIstanzeAll.Count;

            List<IstanzeAllegati> pListIstanzeAll = RepositoryGetAllegatiDaCodiceIstanza(Convert.ToInt32(this._datiProtocollazione.CodiceIstanza));
            int iCountIstanzeAll = pListIstanzeAll.Count;


            _protocolloLogs.DebugFormat("NUMERO ALLEGATI TROVATI SU ISTANZEALLEGATI: {0}", iCountIstanzeAll);


            // var istanzeProcureMgr = new IstanzeProcureMgr(this._datiProtocollazione.Db);
            // var istanzeProcure = new IstanzeProcure();
            // istanzeProcure.CodiceIstanza = Convert.ToInt32(this._datiProtocollazione.CodiceIstanza);
            // istanzeProcure.IdComune = this._datiProtocollazione.IdComune;
            // istanzeProcure.OthersWhereClause.Add("ISTANZEPROCURE.CODICEOGGETTOPROCURA is not null");
            // 
            // var listIstanzeProcure = istanzeProcureMgr.GetList(istanzeProcure);
            // int iCountIstanzeProcure = listIstanzeProcure.Count;
            var listIstanzeProcure = RepositoryGetProcureDaCodiceIstanza(Convert.ToInt32(this._datiProtocollazione.CodiceIstanza));
            int iCountIstanzeProcure = listIstanzeProcure.Count;

            _protocolloLogs.DebugFormat("NUMERO ALLEGATI TROVATI SU ISTANZEPROCURE: {0}", iCountIstanzeProcure);

            // var listaProcureSoggettiCollegati = new IstanzeRichiedentiMgr(this._datiProtocollazione.Db).GetList(new IstanzeRichiedenti
            // {
            //     IDCOMUNE = this._datiProtocollazione.IdComune,
            //     CODICEISTANZA = this._datiProtocollazione.CodiceIstanza,
            //     OthersWhereClause = new ArrayList { "codiceoggetto_procura is not null" }
            // }).ToList<IstanzeRichiedenti>();
            // 
            // int numeroProcureSoggettiCollegati = listaProcureSoggettiCollegati.Count;
            var listaProcureSoggettiCollegati = RepositoryGetProcureSoggettiCollegati(Convert.ToInt32(this._datiProtocollazione.CodiceIstanza));
            var numeroProcureSoggettiCollegati = listaProcureSoggettiCollegati.Count;

            _protocolloLogs.DebugFormat("NUMERO ALLEGATI TROVATI SU ISTANZERICHIEDENTI: {0}", numeroProcureSoggettiCollegati);

            int iCount = iCountDocIstanza + iCountIstanzeAll + iCountIstanzeProcure + numeroProcureSoggettiCollegati;

            _protocolloLogs.DebugFormat("NUMERO ALLEGATI TOTALI: {0}", iCount);

            var listaAllegati = new List<Allegato>();

            listaAllegati.AddRange(pListDocIstanza.Select(x => new Allegato
            {
                Cod = x.CODICEOGGETTO,
                Descrizione = x.DOCUMENTO
            }));

            _protocolloLogs.DebugFormat("INSERITI GLI ALLEGATI SU _dati.Allegati dopo il ciclo su DocumentiIstanza, numero Allegati: {0}", listaAllegati.Count);

            listaAllegati.AddRange(pListIstanzeAll.Select(x => new Allegato
            {
                Cod = x.CODICEOGGETTO,
                Descrizione = x.ALLEGATOEXTRA
            }));

            _protocolloLogs.DebugFormat("INSERITI GLI ALLEGATI SU _dati.Allegati dopo il ciclo su IstanzeAllegati, numero Allegati: {0}", listaAllegati.Count);

            // var oggMgr = new OggettiMgr(_datiProtocollazione.Db);

            listaAllegati.AddRange(listIstanzeProcure.Where(y => y.CodiceOggettoProcura.HasValue).Select(x => new Allegato
            {
                Cod = x.CodiceOggettoProcura.Value.ToString(),
                // Descrizione = oggMgr.GetNomeFile(_datiProtocollazione.IdComune, x.CodiceOggettoProcura.Value
                Descrizione = RepositoryGetNomeFile(x.CodiceOggettoProcura.Value)
            }));

            _protocolloLogs.DebugFormat("INSERITI GLI ALLEGATI SU _dati.Allegati dopo il ciclo su IstanzeProcure, numero Allegati: {0}", listaAllegati.Count);

            listaAllegati.AddRange(listaProcureSoggettiCollegati.Where(y => y.CodiceoggettoProcura.HasValue).Select(x => new Allegato
            {
                Cod = x.CodiceoggettoProcura.Value.ToString(),
                // Descrizione = oggMgr.GetNomeFile(_datiProtocollazione.IdComune, x.CodiceoggettoProcura.Value)
                Descrizione = RepositoryGetNomeFile(x.CodiceoggettoProcura.Value)
            }));

            _protocolloLogs.DebugFormat("INSERITI GLI ALLEGATI SU _dati.Allegati dopo il ciclo su IstanzeRichiedenti, numero Allegati: {0}", listaAllegati.Count);


            return listaAllegati.ToArray();
        }



        private void SetAllegati(int iSource = (int)ProtocolloEnum.Source.NONPROTOCOLLARE)
        {
            try
            {
                // var protoAllegatiMgr = new ProtocolloAllegatiMgr(this._datiProtocollazione.Db);
                _protoIn.Allegati = new List<ProtocolloAllegati>();

                if (_protocolloAttivo.Provenienza == ProtocolloEnum.TipoProvenienza.ONLINE &&
                    this._datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA &&
                    _verticalizzazioneProtocolloAttivo.InviaEmlPecArrivo)
                {
                    _protoIn.Allegati = LeggiAllegatiDaNlaPec();
                }

                if (_dati.Allegati == null && _protoIn.Allegati.Count == 0)
                {
                    bool noAllegatiFO = GetParamVertBool(_verticalizzazioneProtocolloAttivo.Noallegatifo, "NOALLEGATIFO");
                    bool noAllegati = GetParamVertBool(_verticalizzazioneProtocolloAttivo.Noallegati, "NOALLEGATI");

                    _dati.Allegati = new Allegato[0];

                    var verificaRecuperoAllegati = (
                    (
                        (iSource == (int)ProtocolloEnum.Source.INSERIMENTO_NORMALE) ||
                        (iSource == (int)ProtocolloEnum.Source.INSERIMENTO_RAPIDO) ||
                        (iSource == (int)ProtocolloEnum.Source.PROT_IST_MOV_AUT_BO) ||
                        (iSource == (int)ProtocolloEnum.Source.NONPROTOCOLLARE)
                    ) && !noAllegati
                    ) || (
                        (iSource == (int)ProtocolloEnum.Source.ON_LINE) &&
                        !noAllegatiFO
                    );

                    _protocolloLogs.DebugFormat("SOURCE: {0}, NOALLEGATIFO: {1}, NOALLEGATI: {2}", iSource, noAllegatiFO, noAllegati);
                    _protocolloLogs.DebugFormat("VERIFICA RECUPERO ALLEGATI: {0}", verificaRecuperoAllegati);

                    if (verificaRecuperoAllegati)
                    {
                        if (this._datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA)
                        {
                            _dati.Allegati = RisolviDocumentiIstanza();
                        }
                        else if (this._datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_MOVIMENTO)
                        {
                            _dati.Allegati = RepositoryGetAllegatiMovimento(this._datiProtocollazione.CodiceMovimento);

                            _protocolloLogs.DebugFormat("INSERITI GLI ALLEGATI SU _dati.Allegati dopo il ciclo su MovimentiAllegati, numero Allegati: {0}", _dati.Allegati.Length);
                        }
                    }
                }

                _protocolloLogs.DebugFormat("NUMERO ALLEGATI INSERITI: {0}", _dati.Allegati.Length);

                foreach (Allegato allegato in _dati.Allegati)
                {
                    _protocolloLogs.DebugFormat("CHIAMATA AL WEB SERVICE CHE RESTITUISCE GLI OGGETTI, CODICE ALLEGATO: {0}", allegato.Cod);

                    //var recAnag = _datiProto.AnagraficheProtocollo.Select(x => x.Pec).Distinct().Select(x => new pecRecipientsRecipient { type = "to", Value = x });

                    //if (_protoIn.Allegati.Where(x => x.CODICEOGGETTO == allegato.Cod).Count() == 0)
                    //{


                    var codiceOggetto = Convert.ToInt32(allegato.Cod);
                    var oggetto = RepositoryGetOggettoById(codiceOggetto);
                    var percorso = RepositoryGetPercorsoFile(codiceOggetto);

                    if (oggetto == null)
                        throw new Exception(String.Format("IL CODICEOGGETTO {0} PER L'IDCOMUNE {1} NON È PRESENTE NELLA TABELLA OGGETTI", allegato.Cod, this._datiProtocollazione.IdComune));

                    var nomeAllegato = new NomeFileAllegato(this._datiProtocollazione.IdComune, this._datiProtocollazione.CodiceComune, oggetto, allegato.Descrizione, _verticalizzazioneProtocolloAttivo.NomeFileMaxLength);

                    var protoAllegati = new ProtocolloAllegati
                    {
                        MimeType = RepositoryGetContentType(oggetto),
                        Extension = nomeAllegato.GetEstensione(),
                        NOMEFILE = nomeAllegato.GetNomeCompleto(_verticalizzazioneProtocolloAttivo.NomefileOrigine, _protoIn.Allegati, _verticalizzazioneProtocolloAttivo.CreaCopiaFile),
                        Descrizione = nomeAllegato.GetDescrizioneFileCopia(allegato.Descrizione, _protoIn.Allegati, _verticalizzazioneProtocolloAttivo.CreaCopiaDescrFile, 0, _verticalizzazioneProtocolloAttivo.DescrFileMaxLength),
                        CODICEOGGETTO = oggetto.CODICEOGGETTO,
                        IDCOMUNE = oggetto.IDCOMUNE,
                        OGGETTO = oggetto.OGGETTO,
                        Percorso = percorso,
                        InviaTramitePec = allegato.InviaTramitePec.GetValueOrDefault(true)
                    };

                    /*var mgr = new OggettiMgr(_datiProtocollazione.Db);
                    protoAllegati.Percorso = mgr.GetPercorsoOggetto(oggetto.CODICEOGGETTO, _datiProtocollazione.IdComune);*/

                    protoAllegati.RimuoviCaratteriNonValidiDaNomeFile(_verticalizzazioneProtocolloAttivo.ListaCaratteriDaEliminare);

                    _protoIn.Allegati.Add(protoAllegati);
                    //}
                    //else
                    //    _protocolloLogs.WarnFormat("LA LISTA DEGLI ALLEGATI PRESENTA DUE OGGETTI CON LO STESSO CODICE, INDICE CHE ERANO PRESENTI DUE FILE IDENTICI, MOTIVO PER IL QUALE NE E' STATO PRESO IN CONSIDERAZIONE SOLAMENTE UNO");
                }

                _protocolloLogs.Debug($"VERIFICA SE DEVE ESSERE ALLEGATO ANCHE IL FILE XML DELLA DOMANDA: PARAMETRO ALLEGA_XMLDOMANDASTC = {_verticalizzazioneProtocolloAttivo.AllegaXmlDomandaStc}, AMBITO = {this._datiProtocollazione.TipoAmbito}");

                if (_verticalizzazioneProtocolloAttivo.AllegaXmlDomandaStc &&
                    this._datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA)
                {
                    // var mgrDomandeStc = new DomandeStcMgr(this._datiProtocollazione.Db);
                    // _protocolloLogs.Info($"RICERCA DELLA DOMANDA STC DELL'ISTANZA NUMERO: {this._datiProtocollazione.NumeroIstanza}, CODICE: {this._datiProtocollazione.CodiceIstanza}");
                    // var domande = mgrDomandeStc.GetDomandeByIstanza(this._datiProtocollazione.IdComune, Convert.ToInt32(this._datiProtocollazione.CodiceIstanza));

                    _protocolloLogs.Info($"RICERCA DELLA DOMANDA STC DELL'ISTANZA NUMERO: {this._datiProtocollazione.NumeroIstanza}, CODICE: {this._datiProtocollazione.CodiceIstanza}");

                    var domande = RepositoryGetDomandeStcByCodiceIstanza(Convert.ToInt32(this._datiProtocollazione.CodiceIstanza));

                    var numeroDomande = domande == null ? 0 : domande.Count();

                    _protocolloLogs.Info($"LA RICERCA DELLA DOMANDA STC DELL'ISTANZA NUMERO: {this._datiProtocollazione.NumeroIstanza}, CODICE: {this._datiProtocollazione.CodiceIstanza}, HA PRODOTTO {numeroDomande} RISULTATO/I");

                    if (numeroDomande > 0)
                    {
                        if (numeroDomande > 1)
                        {
                            _protocolloLogs.Info($"SONO STATE TROVATE PIU' DOMANDE RELATIVE ALL'ISTANZA NUMERO {this._datiProtocollazione.NumeroIstanza} L'OGGETTO DELLA DOMANDA NON SARA' INSERITO");
                        }
                        else
                        {
                            var domanda = domande.First();

                            if (domanda.CodiceOggetto.HasValue)
                            {

                                _protocolloLogs.Info($"ID DOMANDA: {domanda.Id}, CODICE OGGETTO DEL FILE XML DA SCARICARE: {domanda.CodiceOggetto}");
                                var oggetto = RepositoryGetOggettoById(domanda.CodiceOggetto.Value);

                                _protoIn.Allegati.Add(new ProtocolloAllegati
                                {
                                    CODICEOGGETTO = domanda.CodiceOggetto.ToString(),
                                    MimeType = RepositoryGetContentType(oggetto),
                                    Extension = Path.GetExtension(oggetto.NOMEFILE),
                                    NOMEFILE = oggetto.NOMEFILE,
                                    Descrizione = oggetto.NOMEFILE,
                                    IDCOMUNE = oggetto.IDCOMUNE,
                                    OGGETTO = oggetto.OGGETTO,
                                });
                                _protocolloLogs.Info($"ALLEGATO FILE XML DELLA DOMANDA STC INSERITO CON SUCCESSO TRA I FILE DA INVIARE AL PROTOCOLLO");
                            }
                            else
                            {
                                _protocolloLogs.Info($"CODICE OGGETTO DELLA DOMANDA {domanda.Id} NON PRESENTE");
                            }
                        }
                    }
                    else
                    {
                        _protocolloLogs.Info($"LA DOMANDA STC DELL'ISTANZA NUMERO: {this._datiProtocollazione.NumeroIstanza} NON E' STATA TROVATA");
                    }
                }

                SpostaAllegatoPrincipale(_protoIn.Allegati);

                if (_verticalizzazioneProtocolloAttivo.AllegatiObbligatori && _protoIn.Allegati.Count == 0)
                {
                    throw new Exception("E' OBBLIGATORIA LA PRESENZA DI ALMENO UN DOCUMENTO");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("ERRORE GENERATO DURANTE LA VALORIZZAZIONE DEGLI ALLEGATI, {0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// Questa funzionalità cerca nella tabella oggetti_metadati se è presente il file riepilogo domanda, in caso positivo lo sposta come primo elemento 
        /// facendolo diventare principale.
        /// </summary>
        /// <param name="allegati"></param>
        /// <param name="codiceIstanza"></param>
        /// <param name="db"></param>
        /// <param name="idComune"></param>
        protected void SpostaAllegatoPrincipale(List<ProtocolloAllegati> allegati)
        {
            if (allegati != null && allegati.Count > 0 && this._datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA)
            {
                int codiceIstanza = Convert.ToInt32(this._datiProtocollazione.CodiceIstanza);

                // var metadatoRiepilogoDomanda = String.IsNullOrEmpty(_verticalizzazioneProtocolloAttivo.MetadatoRiepilogoDomanda) ? Constants.RIEPILOGO_DOMANDA : _verticalizzazioneProtocolloAttivo.MetadatoRiepilogoDomanda;
                // int? codiceOggettoRiepilogoDomanda = new OggettiMetadatiMgr(_datiProtocollazione.Db).TrovaRiepilogoDomanda(codiceIstanza, _datiProtocollazione.IdComune, Constants.CHIAVE_TIPODOCUMENTO, metadatoRiepilogoDomanda);
                int? codiceOggettoRiepilogoDomanda = RepositoryGetCodiceOggettoRiepilogoDaMetadati(codiceIstanza);

                if (codiceOggettoRiepilogoDomanda.HasValue)
                {
                    var allegatoPrincipale = allegati.Where(x => x.CODICEOGGETTO == codiceOggettoRiepilogoDomanda.Value.ToString()).FirstOrDefault();

                    if (allegatoPrincipale != null)
                    {
                        allegati.Remove(allegatoPrincipale);
                        allegati.Insert(0, allegatoPrincipale);
                    }
                }
            }
        }

        #region Metodi da spostare su un repository

        private Allegato[] RepositoryGetAllegatiMovimento(string codiceMovimento)
        {
            //Protocollo un movimento
            MovimentiAllegatiMgr pMovAllMgr = new MovimentiAllegatiMgr(this._datiProtocollazione.Db);
            MovimentiAllegati pMovAll = new MovimentiAllegati();
            pMovAll.CODICEMOVIMENTO = codiceMovimento;
            pMovAll.IDCOMUNE = this._datiProtocollazione.IdComune;
            pMovAll.OthersWhereClause.Add("MOVIMENTIALLEGATI.CODICEOGGETTO is not null");
            List<MovimentiAllegati> pListMovAll = pMovAllMgr.GetList(pMovAll);

            if (pListMovAll.Count == 0)
            {
                return new Allegato[0];
            }

            return pListMovAll.Select(obj => new Allegato
            {
                Cod = obj.CODICEOGGETTO,
                Descrizione = obj.DESCRIZIONE
            }).ToArray();
        }

        private void RepositoryUpdateMovimentoNonElaborare(Movimenti movimento)
        {
            var movimentiMgr = new MovimentiMgr(this._datiProtocollazione.Db);
            movimentiMgr.Update(movimento, ComportamentoElaborazioneEnum.NonElaborare);
        }

        private bool RepositoryVerificaFascicolazioneAutomaticaDaCodiceIntervento(int codiceIntervento, int source)
        {
            var alberoMgr = new AlberoProcMgr(_datiProtocollazione.Db);
            return alberoMgr.IsFascicolazioneAutomatica(codiceIntervento, source, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
        }

        private bool RepositoryVerificaProtocollazioneAutomaticaDaCodiceIntervento(int codiceIntervento, int source)
        {
            var alberoMgr = new AlberoProcMgr(_datiProtocollazione.Db);
            return alberoMgr.IsProtocollazioneAutomatica(codiceIntervento, source, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
        }

        private int? RepositoryGetCodiceOggettoRiepilogoDaMetadati(int codiceIstanza)
        {
            var metadatoRiepilogoDomanda = String.IsNullOrEmpty(_verticalizzazioneProtocolloAttivo.MetadatoRiepilogoDomanda) ? Constants.RIEPILOGO_DOMANDA : _verticalizzazioneProtocolloAttivo.MetadatoRiepilogoDomanda;

            return new OggettiMetadatiMgr(this._datiProtocollazione.Db).TrovaRiepilogoDomanda(codiceIstanza, this._datiProtocollazione.IdComune, Constants.CHIAVE_TIPODOCUMENTO, metadatoRiepilogoDomanda);
        }


        private IEnumerable<DomandeStc> RepositoryGetDomandeStcByCodiceIstanza(int codiceIstanza)
        {
            var mgrDomandeStc = new DomandeStcMgr(this._datiProtocollazione.Db);
            //_protocolloLogs.Info($"RICERCA DELLA DOMANDA STC DELL'ISTANZA NUMERO: {this._datiProtocollazione.NumeroIstanza}, CODICE: {this._datiProtocollazione.CodiceIstanza}");
            return mgrDomandeStc.GetDomandeByIstanza(this._datiProtocollazione.IdComune, codiceIstanza);
        }

        private Oggetti RepositoryGetOggettoById(int codiceOggetto)
        {
            var protoAllegatiMgr = new ProtocolloAllegatiMgr(_datiProtocollazione.Db);
            return protoAllegatiMgr.GetById(this._datiProtocollazione.IdComune, codiceOggetto);
        }

        private string RepositoryGetPercorsoFile(int codiceOggetto)
        {
            var protoAllegatiMgr = new ProtocolloAllegatiMgr(_datiProtocollazione.Db);
            return protoAllegatiMgr.GetPercorsoOggetto(this._datiProtocollazione.IdComune, codiceOggetto);
        }

        private string RepositoryGetContentType(Oggetti oggetto)
        {
            var protoAllegatiMgr = new ProtocolloAllegatiMgr(_datiProtocollazione.Db);
            return protoAllegatiMgr.GetContentType(oggetto);
        }

        private string RepositoryGetNomeFile(int codiceOggetto)
        {
            var oggMgr = new OggettiMgr(_datiProtocollazione.Db);
            return oggMgr.GetNomeFile(_datiProtocollazione.IdComune, codiceOggetto);
        }

        private List<IstanzeRichiedenti> RepositoryGetProcureSoggettiCollegati(int codiceIstanza)
        {
            return new IstanzeRichiedentiMgr(this._datiProtocollazione.Db).GetList(new IstanzeRichiedenti
            {
                IDCOMUNE = this._datiProtocollazione.IdComune,
                CODICEISTANZA = codiceIstanza.ToString(),
                OthersWhereClause = new List<string> { "codiceoggetto_procura is not null" }
            }).ToList<IstanzeRichiedenti>();
        }

        private List<IstanzeProcure> RepositoryGetProcureDaCodiceIstanza(int codiceIstanza)
        {
            var istanzeProcureMgr = new IstanzeProcureMgr(this._datiProtocollazione.Db);
            var istanzeProcure = new IstanzeProcure();
            istanzeProcure.CodiceIstanza = codiceIstanza;
            istanzeProcure.IdComune = this._datiProtocollazione.IdComune;
            istanzeProcure.OthersWhereClause.Add("ISTANZEPROCURE.CODICEOGGETTOPROCURA is not null");

            return istanzeProcureMgr.GetList(istanzeProcure);
        }

        private Movimenti RepositoryGetMovimentoByTipoMovimento(int codiceIstanza, string tipoMovimento)
        {
            return new MovimentiMgr(this._datiProtocollazione.Db).GetByClass(new Movimenti
            {
                IDCOMUNE = this._datiProtocollazione.IdComune,
                TIPOMOVIMENTO = tipoMovimento,
                CODICEISTANZA = codiceIstanza.ToString()
            });
        }

        private void RepositoryMovimentiUpdateDatiProtocollo(int idMovimento, string idProtocollo, string numeroProtocollo, DateTime dataProtocollo)
        {
            var movMgr = new MovimentiMgr(this._datiProtocollazione.Db);
            movMgr.UpdateDatiProtocollo(idProtocollo, numeroProtocollo, dataProtocollo, this._datiProtocollazione.IdComune, idMovimento);
        }

        private string RepositoryGetNumeroFascicoloFromAlberoProcProtocollo(int codiceIntervento)
        {
            var alberoMgr = new AlberoProcMgr(this._datiProtocollazione.Db);
            return alberoMgr.GetNumeroFascicoloFromAlberoProcProtocollo(codiceIntervento, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
        }

        private List<DocumentiIstanza> RepositoryGetDocumentiIstanzaConAllegato(int codiceIstanza)
        {
            var docIstanzaMgr = new DocumentiIstanzaMgr(this._datiProtocollazione.Db);
            var docIstanza = new DocumentiIstanza();
            docIstanza.CODICEISTANZA = codiceIstanza.ToString();
            docIstanza.IDCOMUNE = this._datiProtocollazione.IdComune;
            docIstanza.OthersWhereClause.Add("DOCUMENTIISTANZA.CODICEOGGETTO is not null");

            return docIstanzaMgr.GetList(docIstanza);
        }

        private List<IstanzeAllegati> RepositoryGetAllegatiDaCodiceIstanza(int codiceIstanza)
        {
            var istanzeAllMgr = new IstanzeAllegatiMgr(this._datiProtocollazione.Db);
            var istanzeAll = new IstanzeAllegati();
            istanzeAll.CODICEISTANZA = codiceIstanza.ToString();
            istanzeAll.IDCOMUNE = this._datiProtocollazione.IdComune;
            istanzeAll.OthersWhereClause.Add("ISTANZEALLEGATI.CODICEOGGETTO is not null");

            return istanzeAllMgr.GetList(istanzeAll);
        }

        private string RepositoryGetClassificaProtocolloByCodiceIntervento(int codiceIntervento)
        {
            var alberoProcMgr = new AlberoProcMgr(this._datiProtocollazione.Db);
            return alberoProcMgr.GetClassificaProtocolloFromAlberoProcProtocollo(codiceIntervento, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
        }

        private string RepositoryGetClassificaFascicoloByCodiceIntervento(int codiceIntervento)
        {
            var alberoProcMgr = new AlberoProcMgr(this._datiProtocollazione.Db);
            return alberoProcMgr.GetClassificaFascicoloFromAlberoProcProtocollo(codiceIntervento, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);

        }

        private TipiMovimentoStcMappingProtocollo RepositoryGetDatiProtocolloByTipoMovimento(string tipoMovimento)
        {
            var mgrMapping = new TipiMovimentoStcMappingProtocolloMgr(this._datiProtocollazione.Db, this._datiProtocollazione.IdComune);
            return mgrMapping.GetDatiProtocollo(tipoMovimento);
        }

        private TipiMovimento RepositoryGetTipoMovimentoById(string tipoMoviemnto)
        {
            var tipiMovimentoMgr = new TipiMovimentoMgr(this._datiProtocollazione.Db);
            return tipiMovimentoMgr.GetById(tipoMoviemnto, this._datiProtocollazione.IdComune);
        }

        private Amministrazioni RepositoryGetAmministrazioneFromAlberoProcProtocollo(int codiceIntervento)
        {
            var ammMgr = new AmministrazioniMgr(_datiProtocollazione.Db);
            return ammMgr.GetFromAlberoProcProtocollo(codiceIntervento, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
        }

        private string RepositoryGetCodiceAmministrazioneFromAlberoProcProtocollo(int? codiceIntervento, string codiceDefault)
        {
            var ammMgr = new AmministrazioniMgr(_datiProtocollazione.Db);

            if (codiceIntervento.HasValue)
            {
                var codiceAmministrazione = ammMgr.GetCodiceFromAlberoProcProtocollo(codiceIntervento.Value, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);

                return string.IsNullOrEmpty(codiceAmministrazione) ? codiceDefault : codiceAmministrazione;
            }

            return codiceDefault;
        }

        private string RepositoryGetCodiceTipoDocumentoFromAlberoProcProtocollo(int codiceIntervento)
        {
            var mgr = new ProtocolloTipiDocumentoMgr(this._datiProtocollazione.Db);
            return mgr.GetCodiceFromAlberoProcProtocollo(codiceIntervento, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
        }

        private Amministrazioni RepositoryGetAmministrazioneByIdProtocollo(int codiceAmministrazione)
        {
            var ammMgr = new AmministrazioniMgr(_datiProtocollazione.Db);
            return ammMgr.GetByIdProtocollo(this._datiProtocollazione.IdComune, codiceAmministrazione, this._datiProtocollazione.Software, this._datiProtocollazione.CodiceComune);
        }

        private Anagrafe RepositoryGetAnagrafeById(int codiceAnagrafe)
        {
            var anagMgr = new ProtocolloAnagrafeMgr(_datiProtocollazione.Db);
            return anagMgr.GetById(this._datiProtocollazione.IdComune, codiceAnagrafe);
        }

        private string RepositoryGetCodiceIstat(string codiceComune)
        {
            var anagMgr = new ProtocolloAnagrafeMgr(_datiProtocollazione.Db);
            return anagMgr.GetCodiceIstat(codiceComune);
        }

        private string RepositoryGetCodiceStatoEstero(string codiceComune)
        {
            var anagMgr = new ProtocolloAnagrafeMgr(_datiProtocollazione.Db);
            return anagMgr.GetCodiceStatoEstero(codiceComune);
        }

        private Comuni RepositoryGetComuneById(string codComune)
        {
            return new ComuniMgr(this._datiProtocollazione.Db).GetById(codComune);
        }

        

        #endregion

        private ProtocolloEnum.TipoMittenteEnum RisolviTipoMittenteDestinatarioDaValoreVerticalizzazione(string valoreVerticalizzazione)
        {
            if (valoreVerticalizzazione == "1")
                return ProtocolloEnum.TipoMittenteEnum.RICHIEDENTE;

            if (valoreVerticalizzazione == "2")
                return ProtocolloEnum.TipoMittenteEnum.AZIENDA;

            if (valoreVerticalizzazione == "3")
                return ProtocolloEnum.TipoMittenteEnum.AZIENDA_TECNICO;

            if (valoreVerticalizzazione == "4")
                return ProtocolloEnum.TipoMittenteEnum.TECNICO;

            return ProtocolloEnum.TipoMittenteEnum.AZIENDA_RICHIEDENTE;
        }

        /// <summary>
        /// Metodo usato per settare la proprietà Mittenti
        /// </summary>
        private void SetMittenti()
        {
            try
            {
                var tipoMittente = RisolviTipoMittenteDestinatarioDaValoreVerticalizzazione(_verticalizzazioneProtocolloAttivo.TipoMittDestAuto);

                _protoIn.Mittenti = new ListaMittDest();
                _protoIn.Mittenti.Amministrazione = new List<Amministrazioni>();
                _protoIn.Mittenti.Anagrafe = new List<ProtocolloAnagrafe>();

                var codiciGiaPresenti = new List<string>();

                switch (_protoIn.Flusso)
                {
                    case "P":   // Flusso in partenza
                    case "I":   // Flusso interno
                        if (_dati.Mittenti == null)
                        {
                            var codiceAmministrazione = RepositoryGetCodiceAmministrazioneFromAlberoProcProtocollo(
                                _datiProtocollazione.CodiceInterventoProc,
                                _verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault
                            );

                            _dati.Mittenti = new DatiMittenti
                            {
                                Amministrazione = new DatiAnagrafici[]
                                {
                                    new DatiAnagrafici
                                    {
                                        Cod = codiceAmministrazione,
                                        Mezzo = _verticalizzazioneProtocolloAttivo.MezzoDefault,
                                        ModalitaTrasmissione = _verticalizzazioneProtocolloAttivo.ModalitaTrasmissioneDefault
                                    }
                                }.ToList()
                            };
                        }

                        if (_dati.Mittenti.Amministrazione == null || _dati.Mittenti.Amministrazione.Count == 0)
                        {
                            throw new Exception("UN PROTOCOLLO IN PARTENZA OPPURE INTERNO DEVE AVERE COME MITTENTE UN'AMMINISTRAZIONE CON UNITÀ ORGANIZZATIVA O RUOLO SETTATI!");
                        }

                        foreach (var datiAnagAmm in _dati.Mittenti.Amministrazione)
                        {
                            var amm = RepositoryGetAmministrazioneByIdProtocollo(Convert.ToInt32(datiAnagAmm.Cod));

                            if (amm == null)
                            {
                                throw new Exception(String.Format("IL CODICEAMMINISTRAZIONE  {0}, CODICE COMUNE: {1} NON È PRESENTE NELL'ANAGRAFICA DELL'AMMINISTRAZIONE", datiAnagAmm.Cod, this._datiProtocollazione.CodiceComune));
                            }

                            if (amm.HaUnitaOrganizzativaORuoloSettati)
                            {
                                if (!codiciGiaPresenti.Contains(amm.CODICEAMMINISTRAZIONE))
                                {
                                    amm.Mezzo = datiAnagAmm.Mezzo;
                                    amm.ModalitaTrasmissione = datiAnagAmm.ModalitaTrasmissione;

                                    _protoIn.Mittenti.Amministrazione.Add(amm);

                                    codiciGiaPresenti.Add(amm.CODICEAMMINISTRAZIONE);
                                }

                                if (_protoIn.Mittenti.Amministrazione.Count > 1)
                                {
                                    throw new Exception("UN PROTOCOLLO IN PARTENZA OPPURE INTERNO NON PUÒ AVERE COME MITTENTE PIÙ DI UNA AMMINISTRAZIONE CON UNITÀ ORGANIZZATIVA O RUOLO SETTATI!");
                                }
                            }

                            if ((String.IsNullOrEmpty(amm.PROT_UO)) && (String.IsNullOrEmpty(amm.PROT_RUOLO)))
                            {
                                throw new Exception(String.Format("IL CODICEAMMINISTRAZIONE {0} PER L'IDCOMUNE {1} NON HA VALORIZZATO NE' L'UNITÀ ORGANIZZATIVA NE' IL RUOLO", datiAnagAmm.Cod, this._datiProtocollazione.IdComune));
                            }
                        }

                        break;

                    case "A":  // Flusso in arrivo
                        if (_dati.Mittenti == null)
                        {
                            //var datiMittenti = new DatiMittenti();
                            var datiAnagraficiList = new List<DatiAnagrafici>();

                            if (_datiProtocollazione.Istanza == null)
                                throw new Exception("ISTANZA NON VALORIZZATA");

                            var datiAnagraficiRichiedente = new DatiAnagrafici
                            {
                                Cod = _datiProtocollazione.Istanza.CODICERICHIEDENTE,
                                Mezzo = _verticalizzazioneProtocolloAttivo.MezzoDefault,
                                ModalitaTrasmissione = _verticalizzazioneProtocolloAttivo.ModalitaTrasmissioneDefault
                            };

                            DatiAnagrafici datiAnagraficiAzienda = null;

                            if (!String.IsNullOrEmpty(_datiProtocollazione.Istanza.CODICETITOLARELEGALE))
                            {
                                datiAnagraficiAzienda = (new DatiAnagrafici
                                {
                                    Cod = _datiProtocollazione.Istanza.CODICETITOLARELEGALE,
                                    Mezzo = _verticalizzazioneProtocolloAttivo.MezzoDefault,
                                    ModalitaTrasmissione = _verticalizzazioneProtocolloAttivo.ModalitaTrasmissioneDefault
                                });
                            }

                            DatiAnagrafici datiAnagraficiTecnico = null;

                            if (!String.IsNullOrEmpty(_datiProtocollazione.Istanza.CODICEPROFESSIONISTA))
                            {
                                datiAnagraficiTecnico = (new DatiAnagrafici
                                {
                                    Cod = _datiProtocollazione.Istanza.CODICEPROFESSIONISTA,
                                    Mezzo = _verticalizzazioneProtocolloAttivo.MezzoDefault,
                                    ModalitaTrasmissione = _verticalizzazioneProtocolloAttivo.ModalitaTrasmissioneDefault
                                });
                            }

                            _protocolloLogs.Info($"TIPO MITTENTE: {tipoMittente}");

                            if (tipoMittente == ProtocolloEnum.TipoMittenteEnum.AZIENDA_RICHIEDENTE)
                            {
                                _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al mittente, del richiedente");
                                datiAnagraficiList.Add(datiAnagraficiRichiedente);

                                if (datiAnagraficiAzienda != null)
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al mittente, dell'azienda");
                                    datiAnagraficiList.Add(datiAnagraficiAzienda);
                                }
                            }
                            else if (tipoMittente == ProtocolloEnum.TipoMittenteEnum.RICHIEDENTE)
                            {
                                _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al mittente, del richiedente");
                                datiAnagraficiList.Add(datiAnagraficiRichiedente);
                            }
                            else if (tipoMittente == ProtocolloEnum.TipoMittenteEnum.AZIENDA)
                            {
                                if (datiAnagraficiAzienda != null)
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al mittente, dell'azienda");
                                    datiAnagraficiList.Add(datiAnagraficiAzienda);
                                }
                                else
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al mittente, del richiedente perchè azienda non presente");
                                    datiAnagraficiList.Add(datiAnagraficiRichiedente);
                                }
                            }
                            else if (tipoMittente == ProtocolloEnum.TipoMittenteEnum.AZIENDA_TECNICO)
                            {
                                if (datiAnagraficiAzienda != null)
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al mittente, dell'azienda");
                                    datiAnagraficiList.Add(datiAnagraficiAzienda);
                                }
                                else
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al mittente, del richiedente, perchè azienda non presente");
                                    datiAnagraficiList.Add(datiAnagraficiRichiedente);
                                }

                                if (datiAnagraficiTecnico != null)
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al mittente, del tecnico");
                                    datiAnagraficiList.Add(datiAnagraficiTecnico);
                                }
                            }
                            else if (tipoMittente == ProtocolloEnum.TipoMittenteEnum.TECNICO)
                            {
                                if (datiAnagraficiTecnico != null)
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al mittente, del tecnico");
                                    datiAnagraficiList.Add(datiAnagraficiTecnico);
                                }
                                else
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al mittente, del richiedente perchè tecnico non presente");
                                    datiAnagraficiList.Add(datiAnagraficiRichiedente);
                                }
                            }

                            // datiMittenti.Anagrafe = datiAnagraficiList;
                            _dati.Mittenti = new DatiMittenti
                            {
                                Anagrafe = datiAnagraficiList
                            };
                        }

                        if (_dati.Mittenti.Anagrafe != null)
                        {
                            codiciGiaPresenti.Clear();

                            foreach (var datiAnagrafici in _dati.Mittenti.Anagrafe)
                            {
                                // var anagMgr = new ProtocolloAnagrafeMgr(_datiProtocollazione.Db);
                                // var anag = anagMgr.GetById(this._datiProtocollazione.IdComune, Convert.ToInt32(datiAnagrafici.Cod));
                                var anag = RepositoryGetAnagrafeById(Convert.ToInt32(datiAnagrafici.Cod));

                                if (anag == null)
                                {
                                    throw new Exception(String.Format("IL CODICEANAGRAFE {0} PER L'IDCOMUNE {1} NON E' PRESENTE NELL'ANAGRAFICA", datiAnagrafici.Cod, this._datiProtocollazione.IdComune));
                                }

                                var protoAnag = new ProtocolloAnagrafe
                                {
                                    AnagrafeDocumenti = anag.AnagrafeDocumenti,
                                    CAP = anag.CAP,
                                    CAPCORRISPONDENZA = anag.CAPCORRISPONDENZA,
                                    CITTA = anag.CITTA,
                                    CITTACORRISPONDENZA = anag.CITTACORRISPONDENZA,

                                    CODCOMNASCITA = anag.CODCOMNASCITA,
                                    CODCOMREGDITTE = anag.CODCOMREGDITTE,
                                    CODCOMREGTRIB = anag.CODCOMREGTRIB,
                                    CODICEANAGRAFE = anag.CODICEANAGRAFE,
                                    CODICECITTADINANZA = anag.CODICECITTADINANZA,

                                    CODICEFISCALE = anag.CODICEFISCALE,
                                    COMUNECORRISPONDENZA = anag.COMUNECORRISPONDENZA,
                                    COMUNERESIDENZA = anag.COMUNERESIDENZA,
                                    DATA_DISABILITATO = anag.DATA_DISABILITATO,
                                    DATAISCRREA = anag.DATAISCRREA,

                                    DATANASCITA = anag.DATANASCITA,
                                    DATANOMINATIVO = anag.DATANOMINATIVO,
                                    DATAREGDITTE = anag.DATAREGDITTE,
                                    DATAREGTRIB = anag.DATAREGTRIB,
                                    EMAIL = anag.EMAIL,

                                    Pec = String.IsNullOrEmpty(datiAnagrafici.Email) ? anag.Pec : datiAnagrafici.Email,
                                    PecAnagrafica = anag.Pec,

                                    FAX = anag.FAX,
                                    FLAG_DISABILITATO = anag.FLAG_DISABILITATO,
                                    FLAG_NOPROFIT = anag.FLAG_NOPROFIT,
                                    FORMAGIURIDICA = anag.FORMAGIURIDICA,
                                    IDCOMUNE = anag.IDCOMUNE,

                                    INDIRIZZO = anag.INDIRIZZO,
                                    INDIRIZZOCORRISPONDENZA = anag.INDIRIZZOCORRISPONDENZA,
                                    INVIOEMAIL = anag.INVIOEMAIL,
                                    INVIOEMAILTEC = anag.INVIOEMAILTEC,
                                    NOME = anag.NOME,

                                    NOMINATIVO = anag.NOMINATIVO,
                                    NOTE = anag.NOTE,
                                    NUMISCRREA = anag.NUMISCRREA,
                                    PARTITAIVA = anag.PARTITAIVA,
                                    PASSWORD = anag.PASSWORD,

                                    PROVINCIA = anag.PROVINCIA,
                                    PROVINCIACORRISPONDENZA = anag.PROVINCIACORRISPONDENZA,
                                    PROVINCIAREA = anag.PROVINCIAREA,
                                    REGDITTE = anag.REGDITTE,

                                    REGTRIB = anag.REGTRIB,
                                    SESSO = anag.SESSO,
                                    TELEFONO = anag.TELEFONO,
                                    TELEFONOCELLULARE = anag.TELEFONOCELLULARE,
                                    TIPOANAGRAFE = anag.TIPOANAGRAFE,

                                    TIPOLOGIA = anag.TIPOLOGIA,
                                    TITOLO = anag.TITOLO,
                                };

                                if (this._datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA ||
                                    this._datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_MOVIMENTO)
                                {
                                    if (String.IsNullOrEmpty(datiAnagrafici.Email))
                                    {
                                        var gestionePec = (ProtocolloAnagrafe.TipoGestionePecEnum)Enum.Parse(typeof(ProtocolloAnagrafe.TipoGestionePecEnum), _verticalizzazioneProtocolloAttivo.GestionePec);
                                        protoAnag.SetPec(this._datiProtocollazione.Istanza, gestionePec);
                                    }
                                }

                                if (!String.IsNullOrEmpty(anag.CODCOMNASCITA))
                                {
                                    protoAnag.CodiceIstatComNasc = RepositoryGetCodiceIstat(anag.CODCOMNASCITA);
                                    protoAnag.CodiceStatoEsteroNasc = RepositoryGetCodiceStatoEstero(anag.CODCOMNASCITA);
                                    protoAnag.ComuneNascita = RepositoryGetComuneById(anag.CODCOMNASCITA);
                                }

                                if (!String.IsNullOrEmpty(anag.COMUNERESIDENZA))
                                {
                                    protoAnag.CodiceIstatComRes = RepositoryGetCodiceIstat(anag.COMUNERESIDENZA);
                                    protoAnag.CodiceStatoEsteroRes = RepositoryGetCodiceStatoEstero(anag.COMUNERESIDENZA);
                                    protoAnag.ComuneResidenza = RepositoryGetComuneById(anag.COMUNERESIDENZA);
                                }

                                if (!codiciGiaPresenti.Contains(protoAnag.CODICEANAGRAFE))
                                {
                                    protoAnag.Mezzo = datiAnagrafici.Mezzo;
                                    protoAnag.ModalitaTrasmissione = datiAnagrafici.ModalitaTrasmissione;

                                    _protoIn.Mittenti.Anagrafe.Add(protoAnag);

                                    codiciGiaPresenti.Add(protoAnag.CODICEANAGRAFE);
                                }

                                if (_protocolloAttivo.Anagrafiche == null)
                                    _protocolloAttivo.Anagrafiche = new List<IAnagraficaAmministrazione>();

                                _protocolloAttivo.Anagrafiche.Add(new AnagraficaService(protoAnag));
                            }
                        }

                        if (_dati.Mittenti.Amministrazione != null)
                        {
                            codiciGiaPresenti.Clear();

                            foreach (var datiAmministrazione in _dati.Mittenti.Amministrazione)
                            {
                                var amm = RepositoryGetAmministrazioneByIdProtocollo(Convert.ToInt32(datiAmministrazione.Cod));

                                if (amm == null)
                                    throw new Exception(String.Format("IL CODICEAMMINISTRAZIONE {0} PER IL CODICE COMUNE {1} NON E' PRESENTE NELL'ANAGRAFICA DELL'AMMINISTRAZIONE", datiAmministrazione.Cod, this._datiProtocollazione.CodiceComune));

                                if (amm.HaUnitaOrganizzativaORuoloSettati)
                                    throw new Exception("UN PROTOCOLLO IN ARRIVO NON PUÒ AVERE COME MITTENTE UNA AMMINISTRAZIONE CON UNITÀ ORGANIZZATIVA O RUOLO SETTATI! USARE IL FLUSSO INTERNO!");

                                if (!codiciGiaPresenti.Contains(amm.CODICEAMMINISTRAZIONE))
                                {
                                    amm.Mezzo = datiAmministrazione.Mezzo;
                                    amm.ModalitaTrasmissione = datiAmministrazione.ModalitaTrasmissione;

                                    if (!String.IsNullOrEmpty(datiAmministrazione.Email))
                                    {
                                        amm.PEC = datiAmministrazione.Email;
                                    }

                                    _protoIn.Mittenti.Amministrazione.Add(amm);

                                    codiciGiaPresenti.Add(amm.CODICEAMMINISTRAZIONE);
                                }

                                if (_protocolloAttivo.Anagrafiche == null)
                                {
                                    _protocolloAttivo.Anagrafiche = new List<IAnagraficaAmministrazione>();
                                }

                                _protocolloAttivo.Anagrafiche.Add(new AmministrazioneService(amm));

                            }
                        }

                        if ((_protoIn.Mittenti.Amministrazione.Count == 0) && (_protoIn.Mittenti.Anagrafe.Count == 0))
                        {
                            throw new Exception("UN PROTOCOLLO IN ARRIVO DEVE AVERE COME MITTENTE ALMENO UN'AMMINISTRAZIONE CON UNITÀ ORGANIZZATIVA O RUOLO NON SETTATI O ALMENO UN'ANAGRAFICA!");
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("ERRORE GENERATO DURANTE IL SETTAGGIO DEI MITTENTI NELL'OGGETTO, {0}", ex.Message), ex);
            }
        }


        /// <summary>
        /// Metodo usato per settare la proprietà Destinatari
        /// </summary>
        private void SetDestinatari()
        {
            try
            {
                _protoIn.Destinatari = new ListaMittDest();
                _protoIn.Destinatari.Amministrazione = new List<Amministrazioni>();
                _protoIn.Destinatari.Anagrafe = new List<ProtocolloAnagrafe>();

                var tipoDestinatario = RisolviTipoMittenteDestinatarioDaValoreVerticalizzazione(_verticalizzazioneProtocolloAttivo.TipoMittDestAuto);

                //Usati per verificare se l'anagrafica o l'amministrazione siano già stati caricati 
                //nelle rispettive collection
                List<string> codiciGiaTrovati = new List<string>();

                switch (_protoIn.Flusso)
                {
                    case "A":
                    case "I":
                        if (_dati.Destinatari == null)
                        {
                            var codiceAmministrazione = RepositoryGetCodiceAmministrazioneFromAlberoProcProtocollo(
                                _datiProtocollazione.CodiceInterventoProc,
                                _verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault
                            );
                            /*
                            var ammMgr = new AmministrazioniMgr(_datiProtocollazione.Db);
                            string codiceAmministrazione = "";

                            if (_datiProtocollazione.CodiceInterventoProc.HasValue)
                                codiceAmministrazione = ammMgr.GetCodiceFromAlberoProcProtocollo(_datiProtocollazione.CodiceInterventoProc.Value, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);

                            if (String.IsNullOrEmpty(codiceAmministrazione))
                                codiceAmministrazione = _verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault;
                            */
                            _dati.Destinatari = new DatiDestinatari
                            {
                                Amministrazione = new DatiAnagrafici[]
                                {
                                    new DatiAnagrafici
                                    {
                                        Cod = codiceAmministrazione,
                                        Mezzo = _verticalizzazioneProtocolloAttivo.MezzoDefault,
                                        ModalitaTrasmissione = _verticalizzazioneProtocolloAttivo.ModalitaTrasmissioneDefault
                                    }
                                }.ToList()
                            };
                        }

                        if (_dati.Destinatari.Amministrazione == null || _dati.Destinatari.Amministrazione.Count == 0)
                        {
                            throw new Exception("AMMINISTRAZIONE DESTINATARIO NON PRESENTE");
                        }

                        foreach (var datiAmministrazione in _dati.Destinatari.Amministrazione)
                        {
                            // var ammMgr = new AmministrazioniMgr(_datiProtocollazione.Db);
                            // var amm = ammMgr.GetByIdProtocollo(this._datiProtocollazione.IdComune, Convert.ToInt32(datiAmministrazione.Cod), this._datiProtocollazione.Software, this._datiProtocollazione.CodiceComune);
                            var amm = RepositoryGetAmministrazioneByIdProtocollo(Convert.ToInt32(datiAmministrazione.Cod));

                            if (amm == null)
                            {
                                throw new Exception(String.Format("IL CODICEAMMINISTRAZIONE {0} PER IL CODICE COMUNE {1} NON È PRESENTE NELLA TABELLA AMMINISTRAZIONI!", datiAmministrazione.Cod, this._datiProtocollazione.CodiceComune));
                            }

                            if (!amm.HaUnitaOrganizzativaORuoloSettati)
                            {
                                throw new Exception(String.Format("IL CODICEAMMINISTRAZIONE {0} PER IL CODICE COMUNE {1} NON HA SETTATO NE' L'UNITÀ ORGANIZZATIVA NE' IL RUOLO", datiAmministrazione.Cod, this._datiProtocollazione.CodiceComune));
                            }

                            if (!codiciGiaTrovati.Contains(amm.CODICEAMMINISTRAZIONE))
                            {
                                amm.Mezzo = datiAmministrazione.Mezzo;
                                amm.ModalitaTrasmissione = datiAmministrazione.ModalitaTrasmissione;
                                _protoIn.Destinatari.Amministrazione.Add(amm);

                                codiciGiaTrovati.Add(amm.CODICEAMMINISTRAZIONE);
                            }
                        }

                        break;


                    case "P":
                        //Verifico se i mittenti sono settati dal file xml

                        if (_dati.Destinatari == null)
                        {
                            //_dati.Destinatari = new DatiDestinatari();
                            var datiDestinatari = new DatiDestinatari();
                            var datiAnagraficiList = new List<DatiAnagrafici>();

                            if (_datiProtocollazione.Istanza == null)
                            {
                                throw new Exception("ISTANZA NON VALORIZZATA");
                            }

                            var datiAnagraficiRichiedente = new DatiAnagrafici
                            {
                                Cod = _datiProtocollazione.Istanza.CODICERICHIEDENTE,
                                Mezzo = _verticalizzazioneProtocolloAttivo.MezzoDefault,
                                ModalitaTrasmissione = _verticalizzazioneProtocolloAttivo.ModalitaTrasmissioneDefault
                            };

                            DatiAnagrafici datiAnagraficiAzienda = null;
                            if (!String.IsNullOrEmpty(_datiProtocollazione.Istanza.CODICETITOLARELEGALE))
                            {
                                datiAnagraficiAzienda = (new DatiAnagrafici
                                {
                                    Cod = _datiProtocollazione.Istanza.CODICETITOLARELEGALE,
                                    Mezzo = _verticalizzazioneProtocolloAttivo.MezzoDefault,
                                    ModalitaTrasmissione = _verticalizzazioneProtocolloAttivo.ModalitaTrasmissioneDefault
                                });
                            }

                            DatiAnagrafici datiAnagraficiTecnico = null;
                            if (!String.IsNullOrEmpty(_datiProtocollazione.Istanza.CODICEPROFESSIONISTA))
                            {
                                datiAnagraficiTecnico = (new DatiAnagrafici
                                {
                                    Cod = _datiProtocollazione.Istanza.CODICEPROFESSIONISTA,
                                    Mezzo = _verticalizzazioneProtocolloAttivo.MezzoDefault,
                                    ModalitaTrasmissione = _verticalizzazioneProtocolloAttivo.ModalitaTrasmissioneDefault
                                });
                            }

                            _protocolloLogs.Info($"TIPO DESTINATARIO: {tipoDestinatario.ToString()}");

                            if (tipoDestinatario == ProtocolloEnum.TipoMittenteEnum.AZIENDA_RICHIEDENTE)
                            {
                                _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al destinatario, del richiedente");
                                datiAnagraficiList.Add(datiAnagraficiRichiedente);

                                if (datiAnagraficiAzienda != null)
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al destinatario, dell'azienda");
                                    datiAnagraficiList.Add(datiAnagraficiAzienda);
                                }
                            }
                            else if (tipoDestinatario == ProtocolloEnum.TipoMittenteEnum.RICHIEDENTE)
                            {
                                _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al destinatario, del richiedente");
                                datiAnagraficiList.Add(datiAnagraficiRichiedente);
                            }
                            else if (tipoDestinatario == ProtocolloEnum.TipoMittenteEnum.AZIENDA)
                            {
                                if (datiAnagraficiAzienda != null)
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al destinatario, dell'azienda");
                                    datiAnagraficiList.Add(datiAnagraficiAzienda);
                                }
                                else
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al destinatario, del richiedente perchè azienda non presente");
                                    datiAnagraficiList.Add(datiAnagraficiRichiedente);
                                }
                            }
                            else if (tipoDestinatario == ProtocolloEnum.TipoMittenteEnum.AZIENDA_TECNICO)
                            {

                                if (datiAnagraficiAzienda != null)
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al destinatario, dell'azienda");
                                    datiAnagraficiList.Add(datiAnagraficiAzienda);
                                }
                                else
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al destinatario, del richiedente perchè l'azienda non è presente");
                                    datiAnagraficiList.Add(datiAnagraficiRichiedente);
                                }

                                if (datiAnagraficiTecnico != null)
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al destinatario, del tecnico");
                                    datiAnagraficiList.Add(datiAnagraficiTecnico);
                                }
                            }
                            else if (tipoDestinatario == ProtocolloEnum.TipoMittenteEnum.TECNICO)
                            {

                                if (datiAnagraficiTecnico != null)
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al destinatario, del tennico");
                                    datiAnagraficiList.Add(datiAnagraficiTecnico);
                                }
                                else
                                {
                                    _protocolloLogs.Info("Valorizzazione dei dati anagrafici, relativi al destinatario, del richiedente perchè il tennico non è presente");
                                    datiAnagraficiList.Add(datiAnagraficiRichiedente);
                                }
                            }

                            datiDestinatari.Anagrafe = datiAnagraficiList;
                            _dati.Destinatari = datiDestinatari;
                        }

                        //Verifico le anagrafiche per i destinatari
                        if (_dati.Destinatari.Anagrafe != null)
                        {
                            codiciGiaTrovati.Clear();

                            foreach (var datiAnagrafe in _dati.Destinatari.Anagrafe)
                            {
                                // var anagMgr = new ProtocolloAnagrafeMgr(_datiProtocollazione.Db);
                                // var a = anagMgr.GetById(this._datiProtocollazione.IdComune, Convert.ToInt32(datiAnagrafe.Cod));
                                var a = RepositoryGetAnagrafeById(Convert.ToInt32(datiAnagrafe.Cod));

                                if (a == null)
                                    throw new Exception(String.Format("IL CODICEANAGRAFE {0} PER L'IDCOMUNE {1} NON È PRESENTE IN ANAGRAFICA", datiAnagrafe.Cod, this._datiProtocollazione.IdComune));

                                var protoAnag = new ProtocolloAnagrafe
                                {
                                    AnagrafeDocumenti = a.AnagrafeDocumenti,
                                    CAP = a.CAP,
                                    CAPCORRISPONDENZA = a.CAPCORRISPONDENZA,
                                    CITTA = a.CITTA,
                                    CITTACORRISPONDENZA = a.CITTACORRISPONDENZA,

                                    CODCOMNASCITA = a.CODCOMNASCITA,
                                    CODCOMREGDITTE = a.CODCOMREGDITTE,
                                    CODCOMREGTRIB = a.CODCOMREGTRIB,
                                    CODICEANAGRAFE = a.CODICEANAGRAFE,
                                    CODICECITTADINANZA = a.CODICECITTADINANZA,

                                    CODICEFISCALE = a.CODICEFISCALE,
                                    COMUNECORRISPONDENZA = a.COMUNECORRISPONDENZA,
                                    COMUNERESIDENZA = a.COMUNERESIDENZA,
                                    DATA_DISABILITATO = a.DATA_DISABILITATO,
                                    DATAISCRREA = a.DATAISCRREA,

                                    DATANASCITA = a.DATANASCITA,
                                    DATANOMINATIVO = a.DATANOMINATIVO,
                                    DATAREGDITTE = a.DATAREGDITTE,
                                    DATAREGTRIB = a.DATAREGTRIB,
                                    EMAIL = a.EMAIL,

                                    Pec = String.IsNullOrEmpty(datiAnagrafe.Email) ? a.Pec : datiAnagrafe.Email,
                                    PecAnagrafica = a.Pec,

                                    FAX = a.FAX,
                                    FLAG_DISABILITATO = a.FLAG_DISABILITATO,
                                    FLAG_NOPROFIT = a.FLAG_NOPROFIT,
                                    FORMAGIURIDICA = a.FORMAGIURIDICA,
                                    IDCOMUNE = a.IDCOMUNE,

                                    INDIRIZZO = a.INDIRIZZO,
                                    INDIRIZZOCORRISPONDENZA = a.INDIRIZZOCORRISPONDENZA,
                                    INVIOEMAIL = a.INVIOEMAIL,
                                    INVIOEMAILTEC = a.INVIOEMAILTEC,
                                    NOME = a.NOME,

                                    NOMINATIVO = a.NOMINATIVO,
                                    NOTE = a.NOTE,
                                    NUMISCRREA = a.NUMISCRREA,
                                    PARTITAIVA = a.PARTITAIVA,
                                    PASSWORD = a.PASSWORD,

                                    PROVINCIA = a.PROVINCIA,
                                    PROVINCIACORRISPONDENZA = a.PROVINCIACORRISPONDENZA,
                                    PROVINCIAREA = a.PROVINCIAREA,
                                    REGDITTE = a.REGDITTE,

                                    REGTRIB = a.REGTRIB,
                                    SESSO = a.SESSO,
                                    TELEFONO = a.TELEFONO,
                                    TELEFONOCELLULARE = a.TELEFONOCELLULARE,
                                    TIPOANAGRAFE = a.TIPOANAGRAFE,

                                    TIPOLOGIA = a.TIPOLOGIA,
                                    TITOLO = a.TITOLO,
                                };

                                if (this._datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA || this._datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_MOVIMENTO)
                                {
                                    if (String.IsNullOrEmpty(datiAnagrafe.Email))
                                    {
                                        var gestionePec = (ProtocolloAnagrafe.TipoGestionePecEnum)Enum.Parse(typeof(ProtocolloAnagrafe.TipoGestionePecEnum), _verticalizzazioneProtocolloAttivo.GestionePec);
                                        protoAnag.SetPec(this._datiProtocollazione.Istanza, gestionePec);
                                    }
                                }

                                if (!String.IsNullOrEmpty(a.CODCOMNASCITA))
                                {
                                    protoAnag.CodiceIstatComNasc = RepositoryGetCodiceIstat(a.CODCOMNASCITA);
                                    protoAnag.CodiceStatoEsteroNasc = RepositoryGetCodiceStatoEstero(a.CODCOMNASCITA);
                                    protoAnag.ComuneNascita = RepositoryGetComuneById(a.CODCOMNASCITA);

                                }
                                if (!String.IsNullOrEmpty(a.COMUNERESIDENZA))
                                {
                                    protoAnag.CodiceIstatComRes = RepositoryGetCodiceIstat(a.COMUNERESIDENZA);
                                    protoAnag.CodiceStatoEsteroRes = RepositoryGetCodiceStatoEstero(a.COMUNERESIDENZA);
                                    protoAnag.ComuneResidenza = RepositoryGetComuneById(a.COMUNERESIDENZA);
                                }

                                if (!codiciGiaTrovati.Contains(protoAnag.CODICEANAGRAFE))
                                {
                                    protoAnag.Mezzo = datiAnagrafe.Mezzo;
                                    protoAnag.ModalitaTrasmissione = datiAnagrafe.ModalitaTrasmissione;

                                    _protoIn.Destinatari.Anagrafe.Add(protoAnag);

                                    codiciGiaTrovati.Add(protoAnag.CODICEANAGRAFE);
                                }

                                if (_protocolloAttivo.Anagrafiche == null)
                                {
                                    _protocolloAttivo.Anagrafiche = new List<IAnagraficaAmministrazione>();
                                }

                                _protocolloAttivo.Anagrafiche.Add(new AnagraficaService(protoAnag));

                            }
                        }
                        if (_dati.Destinatari.Amministrazione != null)
                        {
                            codiciGiaTrovati.Clear();

                            foreach (var datiAmministrazione in _dati.Destinatari.Amministrazione)
                            {
                                // var ammMgr = new AmministrazioniMgr(_datiProtocollazione.Db);
                                // var amm = ammMgr.GetByIdProtocollo(this._datiProtocollazione.IdComune, Convert.ToInt32(datiAmministrazione.Cod), this._datiProtocollazione.Software, this._datiProtocollazione.CodiceComune);
                                var amm = RepositoryGetAmministrazioneByIdProtocollo(Convert.ToInt32(datiAmministrazione.Cod));

                                if (amm == null)
                                    throw new Exception(String.Format("IL CODICEAMMINISTRAZIONE {0} PER IL CODICE COMUNE {1} NON E' PRESENTE NELLA TABELLA DELLE AMMINISTRAZIONI", datiAmministrazione.Cod, this._datiProtocollazione.CodiceComune));

                                if (!codiciGiaTrovati.Contains(amm.CODICEAMMINISTRAZIONE))
                                {
                                    amm.Mezzo = datiAmministrazione.Mezzo;
                                    amm.ModalitaTrasmissione = datiAmministrazione.ModalitaTrasmissione;

                                    if (!String.IsNullOrEmpty(datiAmministrazione.Email))
                                    {
                                        amm.PEC = datiAmministrazione.Email;
                                    }

                                    _protoIn.Destinatari.Amministrazione.Add(amm);

                                    codiciGiaTrovati.Add(amm.CODICEAMMINISTRAZIONE);
                                }

                                if (_protocolloAttivo.Anagrafiche == null)
                                    _protocolloAttivo.Anagrafiche = new List<IAnagraficaAmministrazione>();

                                _protocolloAttivo.Anagrafiche.Add(new AmministrazioneService(amm));

                            }
                        }

                        if ((_protoIn.Destinatari.Amministrazione.Count == 0) && (_protoIn.Destinatari.Anagrafe.Count == 0))
                        {
                            throw new Exception("UN PROTOCOLLO IN PARTENZA DEVE AVERE COME DESTINATARIO ALMENO UN'AMMINISTRAZIONE O UN'ANAGRAFICA!");
                        }

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("ERRORE GENERATO DURANTE IL SETTAGGIO DEI DESTINATARI, {0}", ex.Message), ex);
            }
        }

        public DatiEtichette StampaEtichette(string idProtocollo, DateTime? dataProtocollo, string numeroProtocollo, int numeroCopie, string stampante)
        {
            if (String.IsNullOrEmpty(idProtocollo) && !dataProtocollo.HasValue && String.IsNullOrEmpty(numeroProtocollo))
                throw new Exception("L'ID, IL NUMERO E LA DATA DEL PROTOCOLLO SONO VUOTI!");

            if (String.IsNullOrEmpty(idProtocollo))
            {
                if (!dataProtocollo.HasValue)
                    throw new ProtocolloException("LA DATA DEL PROTOCOLLO È VUOTA");

                if (String.IsNullOrEmpty(numeroProtocollo))
                    throw new ProtocolloException("IL NUMERO DEL PROTOCOLLO È VUOTO!");
            }

            SetStampaEtichette();
            var datiEtichette = _protocolloAttivo.StampaEtichette(idProtocollo, dataProtocollo, numeroProtocollo, numeroCopie, stampante);

            if (datiEtichette != null)
                _protocolloLogs.InfoFormat("ID ETICHETTA RESTITUITO: {0}", datiEtichette.IdEtichetta);

            return datiEtichette;
        }

        private void SetStampaEtichette()
        {
            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo.Operatore;

            var amm = RepositoryGetAmministrazioneByIdProtocollo(Convert.ToInt32(_verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault));

            if (amm == null)
                throw new Exception(String.Format("IL CODICEAMMINISTRAZIONE {0} PER IL CODICE COMUNE {1} NON È PRESENTE NELLA TABELLA AMMINISTRAZIONI OPPURE NON È STATO SETTATO NELLA VERTICALIZZAZIONE PROTOCOLLO_ATTIVO!", _verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault, this._datiProtocollazione.CodiceComune));

            _protocolloAttivo.Ruolo = string.IsNullOrEmpty(amm.PROT_RUOLO) ? amm.PROT_UO : amm.PROT_RUOLO;

        }

        public ListaFascicoli ListaFascicoli(DatiFasc datiFascicolo)
        {
            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo.Operatore;
            _fascicolo = new Fascicolo
            {
                Classifica = datiFascicolo.ClassificaFascicolo,
                NumeroFascicolo = datiFascicolo.NumeroFascicolo,
                Oggetto = datiFascicolo.OggettoFascicolo,
                DataFascicolo = datiFascicolo.DataFascicolo,
            };

            if (!String.IsNullOrEmpty(datiFascicolo.AnnoFascicolo))
                _fascicolo.AnnoFascicolo = Convert.ToInt32(datiFascicolo.AnnoFascicolo);

            var listaFascicoli = _protocolloAttivo.GetFascicoli(_fascicolo);

            if (listaFascicoli != null)
                _protocolloSerializer.Serialize(ProtocolloLogsConstants.ListaFascicoliResponseFileName, listaFascicoli);

            return listaFascicoli;
        }

        public DatiProtocolloFascicolato IsFascicolato(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            DatiProtocolloFascicolato protoFascicolato = null;

            try
            {
                if (GetParamVertBool(_verticalizzazioneProtocolloAttivo.GestisciFascicolazione, "GESTISCIFASCICOLAZIONE"))
                {
                    if (string.IsNullOrEmpty(idProtocollo) && string.IsNullOrEmpty(annoProtocollo) && string.IsNullOrEmpty(numeroProtocollo))
                    {
                        protoFascicolato = new DatiProtocolloFascicolato();
                        protoFascicolato.Fascicolato = EnumFascicolato.warning;
                        protoFascicolato.NoteFascicolo = "L'id, il numero e la data del protocollo sono vuoti!";

                        //if (_protocolloLogs.IsDebugEnabled)
                        //    _protocolloSerializer.SerializeAndValidateStream(protoFascicolato);

                        return protoFascicolato;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(idProtocollo))
                        {
                            if (string.IsNullOrEmpty(annoProtocollo))
                            {
                                protoFascicolato = new DatiProtocolloFascicolato();
                                protoFascicolato.Fascicolato = EnumFascicolato.warning;
                                protoFascicolato.NoteFascicolo = "La data del protocollo è vuota!";

                                //if (_protocolloLogs.IsDebugEnabled)
                                //    _protocolloSerializer.SerializeAndValidateStream(protoFascicolato);

                                return protoFascicolato;
                            }
                            if (string.IsNullOrEmpty(numeroProtocollo))
                            {
                                protoFascicolato = new DatiProtocolloFascicolato();
                                protoFascicolato.Fascicolato = EnumFascicolato.warning;
                                protoFascicolato.NoteFascicolo = "Il numero del protocollo è vuoto!";

                                //if (_protocolloLogs.IsDebugEnabled)
                                //    _protocolloSerializer.SerializeAndValidateStream(protoFascicolato);

                                return protoFascicolato;
                            }
                        }
                    }

                    //Per ricavare informazioni utili per la fascicolazione del protocollo
                    SetEFascicolato();
                    protoFascicolato = _protocolloAttivo.IsFascicolato(idProtocollo, annoProtocollo, numeroProtocollo);
                }
                else
                {
                    protoFascicolato = new DatiProtocolloFascicolato();
                    protoFascicolato.Fascicolato = EnumFascicolato.nondefinito;
                    protoFascicolato.NoteFascicolo = "Con il sistema di protocollazione attivo non è possibile fascicolare il protocollo";

                    //if (_protocolloLogs.IsDebugEnabled)
                    //    _protocolloSerializer.SerializeAndValidateStream(protoFascicolato);

                    return protoFascicolato;
                }

                //if (protoFascicolato != null)
                //{
                //    if (_protocolloLogs.IsDebugEnabled)
                //        _protocolloSerializer.SerializeAndValidateStream(protoFascicolato);
                //}

                return protoFascicolato;
            }
            catch (Exception ex)
            {
                protoFascicolato = new DatiProtocolloFascicolato();
                protoFascicolato.Fascicolato = EnumFascicolato.warning;
                protoFascicolato.NoteFascicolo = "Verifica fascicolazione terminata con errore: " + ex.Message;

                //if (_protocolloLogs.IsDebugEnabled)
                //    _protocolloSerializer.SerializeAndValidateStream(protoFascicolato);

                return protoFascicolato;
            }
        }

        public DatiFascicolo Fascicola(DatiFasc dati, int source = (int)ProtocolloEnum.SourceFascicolazione.FASC_IST_MOV_AUT_BO,
            string idProtocollo = null, string numeroProtocollo = null, string annoProtocollo = null, ProtocolloEnum.TipoProvenienza provenienza = ProtocolloEnum.TipoProvenienza.BACKOFFICE)
        {
            _protocolloAttivo.Provenienza = provenienza;
            DatiFascicolo rVal = null;
            bool gestisciFascicolazione = GetParamVertBool(_verticalizzazioneProtocolloAttivo.GestisciFascicolazione, "GESTISCIFASCICOLAZIONE");

            if (!gestisciFascicolazione)
                return new DatiFascicolo();

            if (source != (int)ProtocolloEnum.SourceFascicolazione.FASC_IST_MOV_AUT_BO)
            {
                // var alberoMgr = new AlberoProcMgr(_datiProtocollazione.Db);
                // bool fascicolazioneAutomatica = alberoMgr.IsFascicolazioneAutomatica(_datiProtocollazione.CodiceInterventoProc.Value, source, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
                var fascicolazioneAutomatica = RepositoryVerificaFascicolazioneAutomaticaDaCodiceIntervento(_datiProtocollazione.CodiceInterventoProc.Value, source);

                if (!fascicolazioneAutomatica)
                    return new DatiFascicolo();
            }

            if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA)
            {
                if (String.IsNullOrEmpty(_datiProtocollazione.Istanza.FKIDPROTOCOLLO) && (String.IsNullOrEmpty(_datiProtocollazione.Istanza.NUMEROPROTOCOLLO) || !_datiProtocollazione.Istanza.DATAPROTOCOLLO.HasValue))
                    return new DatiFascicolo();
                else
                {

                    string sNumProtocollo = "";
                    if (!String.IsNullOrEmpty(_datiProtocollazione.Istanza.NUMEROPROTOCOLLO))
                    {
                        string[] sNumProtSplit = _datiProtocollazione.Istanza.NUMEROPROTOCOLLO.Split(new Char[] { '/' });
                        sNumProtocollo = sNumProtSplit[0];

                        _protocolloAttivo.IdProtocollo = _datiProtocollazione.Istanza.FKIDPROTOCOLLO;
                        _protocolloAttivo.NumProtocollo = sNumProtocollo;
                        _protocolloAttivo.AnnoProtocollo = _datiProtocollazione.Istanza.DATAPROTOCOLLO.GetValueOrDefault(DateTime.Now).Year.ToString();

                    }
                }
            }

            if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_MOVIMENTO)
            {
                if (String.IsNullOrEmpty(_datiProtocollazione.Movimento.FKIDPROTOCOLLO) && (String.IsNullOrEmpty(_datiProtocollazione.Movimento.NUMEROPROTOCOLLO) || !_datiProtocollazione.Movimento.DATAPROTOCOLLO.HasValue))
                    return new DatiFascicolo();

                // string sNumProtocollo = "";
                if (!String.IsNullOrEmpty(_datiProtocollazione.Movimento.NUMEROPROTOCOLLO))
                {
                    var sNumProtSplit = _datiProtocollazione.Movimento.NUMEROPROTOCOLLO.Split(new Char[] { '/' });
                    var sNumProtocollo = sNumProtSplit[0];

                    _protocolloAttivo.IdProtocollo = _datiProtocollazione.Movimento.FKIDPROTOCOLLO;
                    _protocolloAttivo.NumProtocollo = sNumProtocollo;
                    _protocolloAttivo.AnnoProtocollo = _datiProtocollazione.Movimento.DATAPROTOCOLLO.GetValueOrDefault(DateTime.Now).Year.ToString();
                }
            }

            if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.NESSUNO)
            {
                if (String.IsNullOrEmpty(idProtocollo) && (String.IsNullOrEmpty(numeroProtocollo) || String.IsNullOrEmpty(annoProtocollo)))
                    throw new Exception("NON E' POSSIBILE FASCICOLARE, NON SONO STATI SPECIFICATI I DATI RELATIVI AL PROTOCOLLO DA FASCICOLARE");

                _protocolloAttivo.IdProtocollo = idProtocollo;
                _protocolloAttivo.NumProtocollo = numeroProtocollo;
                _protocolloAttivo.AnnoProtocollo = annoProtocollo;
            }

            if (dati == null)
                dati = new DatiFasc();
            else
                _protocolloSerializer.Serialize(ProtocolloLogsConstants.FascicolazioneSoapRequestFileName, dati);

            _fascicolo = new Fascicolo();

            bool isRichiestaDiFascicolazionePronta = PreparaLaRichiestaPerCreazioneOCambiamentoFascicolo(dati, ProtocolloEnum.TipiFascicolazione.CREA);

            if (isRichiestaDiFascicolazionePronta)
            {
                rVal = _protocolloAttivo.Fascicola(_fascicolo);

                if (rVal != null)
                    _protocolloSerializer.Serialize(ProtocolloLogsConstants.FascicolazioneSoapResponseFileName, rVal);
            }
            else
            {
                rVal = new DatiFascicolo { Warning = "LA PRATICA NON È STATA FASCICOLATA!!" };
                _protocolloSerializer.Serialize(ProtocolloLogsConstants.ResponseFileName, rVal);
            }

            return rVal;
        }

        public DatiFascicolo CambiaFascicolo(DatiFasc datiFascicolo)
        {
            var datiFascicoloRes = new DatiFascicolo();

            if (datiFascicolo == null)
                throw new ArgumentNullException("datiFascicolo");

            _protocolloSerializer.Serialize(ProtocolloLogsConstants.FascicolazioneSoapRequestFileName, datiFascicolo);

            _fascicolo = new Fascicolo();

            if (PreparaLaRichiestaPerCreazioneOCambiamentoFascicolo(datiFascicolo))
                datiFascicoloRes = _protocolloAttivo.CambiaFascicolo(_fascicolo);
            else
                datiFascicoloRes.Warning = "La pratica non è stata fascicolata!!";

            _protocolloSerializer.Serialize(ProtocolloLogsConstants.FascicolazioneSoapResponseFileName, datiFascicoloRes);

            return datiFascicoloRes;
        }

        public void AnnullaProtocollo(string idProtocollo, string annoProtocollo, string numeroProtocollo, string motivoAnnullamento, string noteAnnullamento)
        {
            if (string.IsNullOrEmpty(idProtocollo) && string.IsNullOrEmpty(annoProtocollo) && string.IsNullOrEmpty(numeroProtocollo))
                throw new ProtocolloException("L'id, il numero e la data del protocollo sono vuoti!");
            else
            {
                if (string.IsNullOrEmpty(idProtocollo))
                {
                    if (string.IsNullOrEmpty(annoProtocollo))
                        throw new ProtocolloException("La data del protocollo è vuota!");
                    if (string.IsNullOrEmpty(numeroProtocollo))
                        throw new ProtocolloException("Il numero del protocollo è vuoto!");
                }
            }

            //Per ricavare informazioni utili per la stampa delle etichette
            SetAnnullaProtocollo();

            _protocolloAttivo.AnnullaProtocollo(idProtocollo, annoProtocollo, numeroProtocollo, motivoAnnullamento, noteAnnullamento);
        }

        public DatiProtocolloAnnullato IsAnnullato(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            DatiProtocolloAnnullato pProtAnnullato = null;

            try
            {
                if (string.IsNullOrEmpty(idProtocollo) && string.IsNullOrEmpty(annoProtocollo) && string.IsNullOrEmpty(numeroProtocollo))
                {
                    pProtAnnullato = new DatiProtocolloAnnullato();
                    pProtAnnullato.Annullato = EnumAnnullato.warning;
                    pProtAnnullato.NoteAnnullamento = "L'id, il numero e la data del protocollo sono vuoti!";

                    //if (_protocolloLogs.IsDebugEnabled)
                    //    _protocolloSerializer.SerializeAndValidateStream(pProtAnnullato);

                    return pProtAnnullato;
                }
                else
                {
                    if (string.IsNullOrEmpty(idProtocollo))
                    {
                        if (string.IsNullOrEmpty(annoProtocollo))
                        {
                            pProtAnnullato = new DatiProtocolloAnnullato();
                            pProtAnnullato.Annullato = EnumAnnullato.warning;
                            pProtAnnullato.NoteAnnullamento = "La data del protocollo è vuota!";

                            //if (_protocolloLogs.IsDebugEnabled)
                            //    _protocolloSerializer.SerializeAndValidateStream(pProtAnnullato);

                            return pProtAnnullato;
                        }
                        if (string.IsNullOrEmpty(numeroProtocollo))
                        {
                            pProtAnnullato = new DatiProtocolloAnnullato();
                            pProtAnnullato.Annullato = EnumAnnullato.warning;
                            pProtAnnullato.NoteAnnullamento = "Il numero del protocollo è vuoto!";

                            //if (_protocolloLogs.IsDebugEnabled)
                            //    _protocolloSerializer.SerializeAndValidateStream(pProtAnnullato);

                            return pProtAnnullato;
                        }
                    }
                }
                //Per ricavare informazioni utili per l'annullamento del protocollo
                SetAnnullaProtocollo();
                pProtAnnullato = _protocolloAttivo.IsAnnullato(idProtocollo, annoProtocollo, numeroProtocollo);

                //if (pProtAnnullato != null)
                //{
                //    if (_protocolloLogs.IsDebugEnabled)
                //        _protocolloSerializer.SerializeAndValidateStream(pProtAnnullato);
                //}
            }
            catch (Exception ex)
            {
                pProtAnnullato = new DatiProtocolloAnnullato();
                pProtAnnullato.Annullato = EnumAnnullato.warning;
                pProtAnnullato.NoteAnnullamento = String.Format("Verifica nullabilità terminata con errore: {0}", ex.Message);

                //if (_protocolloLogs.IsDebugEnabled)
                //    _protocolloSerializer.SerializeAndValidateStream(pProtAnnullato);

                return pProtAnnullato;
            }

            return pProtAnnullato;
        }

        public ListaMotiviAnnullamento ListaMotivoAnnullamento()
        {
            SetAnnullaProtocollo();

            var listMotivoAnn = _protocolloAttivo.GetMotivoAnnullamento();

            //if (listMotivoAnn != null && _protocolloLogs.IsDebugEnabled)
            //    _protocolloSerializer.SerializeAndValidateStream(listMotivoAnn, "ListaMotiviAnnullamento.xml");

            return listMotivoAnn;
        }

        public void AggiungiAllegati(string numeroProtocollo, DateTime? dataProtocollo, string idProtocollo, int[] codiciAllegati)
        {
            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo.Operatore;
            var protoAllegatiList = new List<ProtocolloAllegati>();

            foreach (var codice in codiciAllegati)
            {
                // var mgr = new ProtocolloAllegatiMgr(_datiProtocollazione.Db);
                // var oggetto = mgr.GetById(_datiProtocollazione.IdComune, codice);
                // string percorso = mgr.GetPercorsoOggetto(_datiProtocollazione.IdComune, codice);
                var oggetto = RepositoryGetOggettoById(codice);
                var percorso = RepositoryGetPercorsoFile(codice);

                var nomeAllegato = new NomeFileAllegato(this._datiProtocollazione.IdComune, this._datiProtocollazione.CodiceComune, oggetto, oggetto.NOMEFILE, _verticalizzazioneProtocolloAttivo.NomeFileMaxLength);

                var protoAllegato = new ProtocolloAllegati
                {
                    MimeType = RepositoryGetContentType(oggetto),
                    Extension = nomeAllegato.GetEstensione(),
                    NOMEFILE = nomeAllegato.GetNomeCompleto(_verticalizzazioneProtocolloAttivo.NomefileOrigine, protoAllegatiList, _verticalizzazioneProtocolloAttivo.CreaCopiaFile),
                    Descrizione = nomeAllegato.GetDescrizioneFileCopia(oggetto.NOMEFILE, protoAllegatiList, _verticalizzazioneProtocolloAttivo.CreaCopiaDescrFile, 0, _verticalizzazioneProtocolloAttivo.DescrFileMaxLength),
                    CODICEOGGETTO = oggetto.CODICEOGGETTO,
                    IDCOMUNE = oggetto.IDCOMUNE,
                    OGGETTO = oggetto.OGGETTO,
                    Percorso = percorso
                };

                protoAllegatiList.Add(protoAllegato);
            }

            _protocolloAttivo.AggiungiAllegati(idProtocollo, numeroProtocollo, dataProtocollo, protoAllegatiList);
        }

        private void SetEFascicolato()
        {
            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo.Operatore;

            // var ammMgr = new AmministrazioniMgr(this._datiProtocollazione.Db);
            // var amm = ammMgr.GetByIdProtocollo(this._datiProtocollazione.IdComune, Convert.ToInt32(_verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault), this._datiProtocollazione.Software, this._datiProtocollazione.CodiceComune);
            var codiceAmministrazione = Convert.ToInt32(_verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault);
            var amm = RepositoryGetAmministrazioneByIdProtocollo(codiceAmministrazione);

            if (amm == null)
                throw new ProtocolloException(String.Format("IL CODICEAMMINISTRAZIONE {0} PER L'IDCOMUNE {1} NON È PRESENTE NELLA TABELLA AMMINISTRAZIONI OPPURE NON È STATO SETTATO NELLA VERTICALIZZAZIONE PROTOCOLLO_ATTIVO", _verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault, this._datiProtocollazione.IdComune));

            _protocolloAttivo.Ruolo = string.IsNullOrEmpty(amm.PROT_RUOLO) ? amm.PROT_UO : amm.PROT_RUOLO;
        }

        private bool PreparaLaRichiestaPerCreazioneOCambiamentoFascicolo(DatiFasc datiFasc, ProtocolloEnum.TipiFascicolazione tipoFasc = ProtocolloEnum.TipiFascicolazione.CAMBIA)
        {
            _protocolloLogs.Debug("Preparazione della richiesta per creazione / cambio fascicolazione");

            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo.Operatore;
            _protocolloLogs.DebugFormat("Operatore: {0}", _protocolloAttivo.Operatore);
            if (String.IsNullOrEmpty(datiFasc.DataFascicolo) && String.IsNullOrEmpty(datiFasc.AnnoFascicolo))
            {
                datiFasc.DataFascicolo = DateTime.Now.ToString("dd/MM/yyyy");
            }


            int? annoRiferimentoFascicolo = !String.IsNullOrEmpty(datiFasc.AnnoFascicolo) ? Convert.ToInt32(datiFasc.AnnoFascicolo) : DateTime.ParseExact(datiFasc.DataFascicolo, "dd/MM/yyyy", null).Year;

            

            if (datiFasc != null)
            {
                _protocolloLogs.DebugFormat("Tipo fascicolazione: {0}", tipoFasc);

                switch (tipoFasc)
                {
                    case ProtocolloEnum.TipiFascicolazione.CREA:
                        if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.NESSUNO)
                        {
                            if (!String.IsNullOrEmpty(datiFasc.DataFascicolo))
                                _fascicolo.DataFascicolo = datiFasc.DataFascicolo;
                            else
                                _fascicolo.DataFascicolo = DateTime.Now.ToString("dd/MM/yyyy");

                            _fascicolo.AnnoFascicolo = annoRiferimentoFascicolo;

                            if (!String.IsNullOrEmpty(datiFasc.NumeroFascicolo))
                                _fascicolo.NumeroFascicolo = datiFasc.NumeroFascicolo;

                            if (String.IsNullOrEmpty(datiFasc.OggettoFascicolo))
                                throw new Exception("OGGETTO NON VALORIZZATO");

                            _fascicolo.Oggetto = datiFasc.OggettoFascicolo;
                            _fascicolo.Classifica = datiFasc.ClassificaFascicolo;

                            if (String.IsNullOrEmpty(datiFasc.ClassificaFascicolo))
                                _fascicolo.Classifica = _verticalizzazioneProtocolloAttivo.ClassificaFascDefaultBo;

                            if (String.IsNullOrEmpty(datiFasc.NumeroFascicolo) && String.IsNullOrEmpty(_fascicolo.Classifica))
                                throw new Exception("NON E' STATA VALORIZZATA LA CLASSIFICA DEL FASCICOLO");
                        }

                        if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA)
                        {
                            if (!String.IsNullOrEmpty(datiFasc.DataFascicolo))
                                _fascicolo.DataFascicolo = datiFasc.DataFascicolo;
                            else
                                _fascicolo.DataFascicolo = DateTime.Now.ToString("dd/MM/yyyy");

                            _fascicolo.AnnoFascicolo = annoRiferimentoFascicolo;

                            if (!String.IsNullOrEmpty(datiFasc.NumeroFascicolo))
                                _fascicolo.NumeroFascicolo = datiFasc.NumeroFascicolo;
                            else
                            {
                                // var alberoMgr = new AlberoProcMgr(this._datiProtocollazione.Db);
                                // _fascicolo.NumeroFascicolo = alberoMgr.GetNumeroFascicoloFromAlberoProcProtocollo(_datiProtocollazione.CodiceInterventoProc.Value, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
                                _fascicolo.NumeroFascicolo = RepositoryGetNumeroFascicoloFromAlberoProcProtocollo(_datiProtocollazione.CodiceInterventoProc.Value);
                            }


                            if (String.IsNullOrEmpty(datiFasc.OggettoFascicolo))
                            {
                                IMailTipoService mailTipoSrv = MailTipoServiceFactory.Create(this._datiProtocollazione, _protocolloLogs, _protocolloSerializer);
                                var mailTipo = mailTipoSrv.GetMailTipo();
                                _fascicolo.Oggetto = mailTipo.Oggetto;

                                _protocolloLogs.DebugFormat("Oggetto restituito: {0}", _fascicolo.Oggetto);
                            }
                            else
                                _fascicolo.Oggetto = datiFasc.OggettoFascicolo;

                            if (!String.IsNullOrEmpty(datiFasc.ClassificaFascicolo))
                                _fascicolo.Classifica = datiFasc.ClassificaFascicolo;
                            else
                            {
                                // var alberoMgr = new AlberoProcMgr(this._datiProtocollazione.Db);
                                // _fascicolo.Classifica = alberoMgr.GetClassificaFascicoloFromAlberoProcProtocollo(_datiProtocollazione.CodiceInterventoProc.Value, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
                                _fascicolo.Classifica = RepositoryGetClassificaFascicoloByCodiceIntervento(_datiProtocollazione.CodiceInterventoProc.Value);

                                if (String.IsNullOrEmpty(_fascicolo.Classifica))
                                    _fascicolo.Classifica = _verticalizzazioneProtocolloAttivo.ClassificaFascDefaultBo;
                            }

                            if (String.IsNullOrEmpty(datiFasc.NumeroFascicolo) && String.IsNullOrEmpty(_fascicolo.Classifica))
                                throw new ProtocolloException("NON È STATA SETTATA LA CLASSIFICA NELL'ALBERO DEI PROCEDIMENTI");
                        }

                        if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_MOVIMENTO)
                        {
                            if (!String.IsNullOrEmpty(datiFasc.NumeroFascicolo))
                            {
                                _fascicolo.NumeroFascicolo = datiFasc.NumeroFascicolo;

                                if (!String.IsNullOrEmpty(datiFasc.DataFascicolo))
                                    _fascicolo.DataFascicolo = datiFasc.DataFascicolo;
                                else
                                    _fascicolo.DataFascicolo = DateTime.Now.ToString("dd/MM/yyyy");

                                _fascicolo.AnnoFascicolo = annoRiferimentoFascicolo;
                                _fascicolo.Classifica = datiFasc.ClassificaFascicolo;
                                _fascicolo.Oggetto = datiFasc.OggettoFascicolo;

                            }
                            else
                            {
                                var datiProtFasc = IsFascicolato(_datiProtocollazione.Istanza.FKIDPROTOCOLLO, _datiProtocollazione.Istanza.DATAPROTOCOLLO.GetValueOrDefault(DateTime.MinValue).Year.ToString(), _datiProtocollazione.Istanza.NUMEROPROTOCOLLO);
                                _fascicolo.NumeroFascicolo = datiProtFasc.NumeroFascicolo;
                                _fascicolo.DataFascicolo = datiProtFasc.DataFascicolo;
                                _fascicolo.AnnoFascicolo = string.IsNullOrEmpty(datiProtFasc.AnnoFascicolo) ? 0 : Convert.ToInt32(datiProtFasc.AnnoFascicolo);
                                _fascicolo.Classifica = datiProtFasc.Classifica;
                                _fascicolo.Oggetto = datiProtFasc.Oggetto;
                            }

                            if (String.IsNullOrEmpty(_fascicolo.NumeroFascicolo) || (_fascicolo.AnnoFascicolo == 0))
                            {
                                //throw new ProtocolloException("La pratica non è stata fascicolata!!");
                                //Per evitare di sollevare un'eccezione in corrispondenza di questo "finto" problema
                                return false;
                            }
                        }
                        break;
                    case ProtocolloEnum.TipiFascicolazione.CAMBIA:
                        if (!string.IsNullOrEmpty(datiFasc.DataFascicolo))
                            _fascicolo.DataFascicolo = datiFasc.DataFascicolo;
                        else
                            _fascicolo.DataFascicolo = DateTime.Now.ToString("dd/MM/yyyy");

                        _fascicolo.AnnoFascicolo = annoRiferimentoFascicolo;

                        if (String.IsNullOrEmpty(datiFasc.NumeroFascicolo))
                        {
                            //throw new ProtocolloException("Non è stata settato il numero di fascicolo");
                            _fascicolo.NumeroFascicolo = "";
                        }
                        else
                        {
                            _fascicolo.NumeroFascicolo = datiFasc.NumeroFascicolo;
                        }

                        if (!String.IsNullOrEmpty(datiFasc.ClassificaFascicolo))
                        {
                            _fascicolo.Classifica = datiFasc.ClassificaFascicolo;
                        }

                        break;
                }


                // var ammMgr = new AmministrazioniMgr(this._datiProtocollazione.Db);
                Amministrazioni amm = null;

                if (_datiProtocollazione.TipoAmbito != ProtocolloEnum.AmbitoProtocollazioneEnum.NESSUNO)
                {
                    // amm = ammMgr.GetFromAlberoProcProtocollo(this._datiProtocollazione.CodiceInterventoProc.Value, this._datiProtocollazione.IdComune, this._datiProtocollazione.Software, this._datiProtocollazione.CodiceComune);
                    amm = RepositoryGetAmministrazioneFromAlberoProcProtocollo(this._datiProtocollazione.CodiceInterventoProc.Value);
                }

                if (amm == null)
                {
                    amm = RepositoryGetAmministrazioneByIdProtocollo(Convert.ToInt32(_verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault));
                }

                if (amm == null)
                {
                    throw new Exception("AMMINISTRAZIONE NON TROVATA");
                }

                _protocolloAttivo.Ruolo = String.IsNullOrEmpty(amm.PROT_RUOLO) ? amm.PROT_UO : amm.PROT_RUOLO;
            }

            return true;
        }

        //private string RepositoryGetNumeroFascicoloFromAlberoProcProtocollo(int codiceIntervento)
        //{
        //    var alberoMgr = new AlberoProcMgr(this._datiProtocollazione.Db);
        //    return  alberoMgr.GetNumeroFascicoloFromAlberoProcProtocollo(codiceIntervento, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
        //}

        private void SetAnnullaProtocollo()
        {
            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo.Operatore;
        }

        public void InvioPec()
        {
            _protocolloAttivo.InvioPec(_datiProtocollazione.Movimento.FKIDPROTOCOLLO, _datiProtocollazione.Movimento.NUMEROPROTOCOLLO, _datiProtocollazione.Movimento.DATAPROTOCOLLO.Value.Year.ToString());
        }

        public DatiProtocolloRes CreaCopie(string codiceAmministrazione)
        {
            if (String.IsNullOrEmpty(_datiProtocollazione.Istanza.NUMEROPROTOCOLLO) || !_datiProtocollazione.Istanza.DATAPROTOCOLLO.HasValue)
                throw new Exception("IL NUMERO O LA DATA PROTOCOLLO NON E' VALORIZZATO");

            _protocolloLogs.DebugFormat("Valorizzazione dati tramite la chiamata a SetCreaCopie");
            SetCreaCopie(codiceAmministrazione);
            _protocolloLogs.DebugFormat("Fine valorizzazione dati tramite la chiamata a SetCreaCopie");

            _protocolloLogs.DebugFormat("Chiamata a CreaCopie da ProtocolloMgr");
            DatiProtocolloRes datiProtocollo = _protocolloAttivo.CreaCopie();
            _protocolloLogs.DebugFormat("Fine chiamata a CreaCopie da ProtocolloMgr");

            if (datiProtocollo != null)
            {
                _protocolloSerializer.Serialize(ProtocolloLogsConstants.CreaCopieReturnFileName, datiProtocollo);

                _datiProtocollazione.Istanza.FKIDPROTOCOLLO = String.IsNullOrEmpty(datiProtocollo.IdProtocollo) ? null : datiProtocollo.IdProtocollo;

                if (!String.IsNullOrEmpty(datiProtocollo.NumeroProtocollo) && datiProtocollo.NumeroProtocollo != "0")
                {
                    _datiProtocollazione.Istanza.NUMEROPROTOCOLLO = datiProtocollo.NumeroProtocollo;
                    _datiProtocollazione.Istanza.DATAPROTOCOLLO = DateTime.ParseExact(datiProtocollo.DataProtocollo, "dd/MM/yyyy", null);
                }

                // _datiProtocollazione.Istanza.FKIDPROTOCOLLO, _datiProtocollazione.Istanza.NUMEROPROTOCOLLO, _datiProtocollazione.Istanza.DATAPROTOCOLLO.Value, _datiProtocollazione.IdComune, Convert.ToInt32(_datiProtocollazione.CodiceIstanza))
                var codiceIstanza = Convert.ToInt32(_datiProtocollazione.CodiceIstanza);
                var fkIdProtocollo = _datiProtocollazione.Istanza.FKIDPROTOCOLLO;
                var numeroProtocollo = _datiProtocollazione.Istanza.NUMEROPROTOCOLLO;
                var dataProtocollo = _datiProtocollazione.Istanza.DATAPROTOCOLLO.Value;

                RepositoryAggiornaRiferimentoProtocolloIstanza(codiceIstanza, fkIdProtocollo, numeroProtocollo, dataProtocollo);

                // var movMgr = new MovimentiMgr(this._datiProtocollazione.Db);
                // var mov = new Movimenti();
                // mov.IDCOMUNE = this._datiProtocollazione.IdComune;
                // mov.TIPOMOVIMENTO = _datiProtocollazione.Istanza.TIPOMOVAVVIO;
                // mov.CODICEISTANZA = _datiProtocollazione.CodiceIstanza;
                // mov = movMgr.GetByClass(mov);
                // 
                // if (mov != null)
                // {
                //     //Prima con il protocollo Sigepro non veniva aggiornato il campo FKIDPROTOCOLLO per il movimento di avvio
                //     mov.FKIDPROTOCOLLO = string.IsNullOrEmpty(datiProtocollo.IdProtocollo) ? null : datiProtocollo.IdProtocollo;
                //     if (!String.IsNullOrEmpty(datiProtocollo.NumeroProtocollo) && datiProtocollo.NumeroProtocollo != "0")
                //     {
                //         mov.NUMEROPROTOCOLLO = datiProtocollo.NumeroProtocollo;
                //         mov.DATAPROTOCOLLO = _datiProtocollazione.Istanza.DATAPROTOCOLLO;
                //     }
                //     movMgr.UpdateDatiProtocollo(mov.FKIDPROTOCOLLO, mov.NUMEROPROTOCOLLO, mov.DATAPROTOCOLLO.Value, mov.IDCOMUNE, Convert.ToInt32(mov.CODICEMOVIMENTO));
                // }
                // Prima con il protocollo Sigepro non veniva aggiornato il campo FKIDPROTOCOLLO per il movimento di avvio
                RepositoryAggiornaRiferimentoProtocolloMovimentoAvvio(codiceIstanza, _datiProtocollazione.Istanza.TIPOMOVAVVIO, datiProtocollo, dataProtocollo);
            }

            return datiProtocollo;
        }

        private void RepositoryAggiornaRiferimentoProtocolloMovimentoAvvio(int codiceIstanza, string tipoMovimento, DatiProtocolloRes datiProtocollo, DateTime dataProtocollo)
        {
            // var movMgr = new MovimentiMgr(this._datiProtocollazione.Db);
            // var mov = new Movimenti();
            // mov.IDCOMUNE = this._datiProtocollazione.IdComune;
            // mov.TIPOMOVIMENTO = tipoMovimento;
            // mov.CODICEISTANZA = codiceIstanza.ToString();
            // 
            // mov = movMgr.GetByClass(mov);
            var mov = RepositoryGetMovimentoByTipoMovimento(codiceIstanza, tipoMovimento);

            if (mov != null)
            {
                //Prima con il protocollo Sigepro non veniva aggiornato il campo FKIDPROTOCOLLO per il movimento di avvio
                mov.FKIDPROTOCOLLO = string.IsNullOrEmpty(datiProtocollo.IdProtocollo) ? null : datiProtocollo.IdProtocollo;

                if (!String.IsNullOrEmpty(datiProtocollo.NumeroProtocollo) && datiProtocollo.NumeroProtocollo != "0")
                {
                    mov.NUMEROPROTOCOLLO = datiProtocollo.NumeroProtocollo;
                    mov.DATAPROTOCOLLO = dataProtocollo;
                }

                // movMgr.UpdateDatiProtocollo(mov.FKIDPROTOCOLLO, mov.NUMEROPROTOCOLLO, mov.DATAPROTOCOLLO.Value, mov.IDCOMUNE, Convert.ToInt32(mov.CODICEMOVIMENTO));
                RepositoryMovimentiUpdateDatiProtocollo(Convert.ToInt32(mov.CODICEMOVIMENTO), mov.FKIDPROTOCOLLO, mov.NUMEROPROTOCOLLO, mov.DATAPROTOCOLLO.Value);
            }
        }

        private void RepositoryAggiornaRiferimentoProtocolloIstanza(int codiceIstanza, string idProtocollo, string numeroProtocollo, DateTime dataProtocollo)
        {
            var istanzeMgr = new IstanzeMgr(this._datiProtocollazione.Db);
            // istanzeMgr.UpdateDatiProtocollo(_datiProtocollazione.Istanza.FKIDPROTOCOLLO, _datiProtocollazione.Istanza.NUMEROPROTOCOLLO, _datiProtocollazione.Istanza.DATAPROTOCOLLO.Value, _datiProtocollazione.IdComune, Convert.ToInt32(_datiProtocollazione.CodiceIstanza));
            istanzeMgr.UpdateDatiProtocollo(idProtocollo, numeroProtocollo, dataProtocollo, _datiProtocollazione.IdComune, codiceIstanza);
        }

        private void SetCreaCopie(string codiceAmministrazione)
        {
            // var ammMgr = new AmministrazioniMgr(this._datiProtocollazione.Db);
            Amministrazioni amm = null;

            if (String.IsNullOrEmpty(codiceAmministrazione))
            {
                // amm = ammMgr.GetFromAlberoProcProtocollo(this._datiProtocollazione.CodiceInterventoProc.Value, this._datiProtocollazione.IdComune, this._datiProtocollazione.Software, this._datiProtocollazione.CodiceComune);
                amm = RepositoryGetAmministrazioneFromAlberoProcProtocollo(this._datiProtocollazione.CodiceInterventoProc.Value);

                if (amm == null)
                {
                    codiceAmministrazione = _verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault;
                }
            }

            if (amm == null)
            {
                // amm = ammMgr.GetByIdProtocollo(this._datiProtocollazione.IdComune, Convert.ToInt32(codiceAmministrazione), this._datiProtocollazione.Software, this._datiProtocollazione.CodiceComune);
                amm = RepositoryGetAmministrazioneByIdProtocollo(Convert.ToInt32(codiceAmministrazione));
            }

            if (amm == null)
            {
                throw new Exception("AMMINISTRAZIONE NON TROVATA");
            }

            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo.Operatore;
            _protocolloAttivo.Ruolo = amm.PROT_RUOLO;
            _protocolloAttivo.CodAmministrazione = amm.CODICEAMMINISTRAZIONE;

        }

        public DatiProtocolloLetto LeggiProtocollo(string idProtocollo, string annoProtocollo, string numeroProtocollo)
        {
            if (String.IsNullOrEmpty(idProtocollo) && String.IsNullOrEmpty(annoProtocollo) && String.IsNullOrEmpty(numeroProtocollo))
            {
                throw new Exception("L'ID, IL NUMERO E L'ANNO DEL PROTOCOLLO SONO VUOTI!");
            }

            if (string.IsNullOrEmpty(idProtocollo) && (String.IsNullOrEmpty(annoProtocollo) || String.IsNullOrEmpty(numeroProtocollo)))
            {
                throw new Exception($"E'obbligatorio specificare un anno protocollo e un numero protocollo quando non si specifica un id protocollo (anno: {annoProtocollo}, numero: {numeroProtocollo})");
            }

            SetLettura();

            var protoLetto = _protocolloAttivo.LeggiProtocollo(idProtocollo, annoProtocollo, numeroProtocollo);

            if (protoLetto != null)
            {
                _protocolloAttivo.CheckProtocolloLetto(annoProtocollo, numeroProtocollo, idProtocollo, protoLetto);

                SetAllegatiWsReturn(protoLetto);

                _protocolloSerializer.Serialize(ProtocolloLogsConstants.ResponseFileName, protoLetto);
            }

            return protoLetto;
        }

        public DatiProtocolloLetto LeggiProtocolloConData(string idProtocollo, DateTime dataProtocollo, string numeroProtocollo)
        {

            if (dataProtocollo == null)
                throw new Exception("LA DATA DEL PROTOCOLLO E' VUOTA!!");

            if (String.IsNullOrEmpty(idProtocollo) && String.IsNullOrEmpty(numeroProtocollo))
                throw new Exception("ID E NUMERO PROTOCOLLO SONO VUOTI, E' OBBLIGATORIO VALORIZZARNE ALMENO UNO DEI DUE!!");

            var annoProtocollo = dataProtocollo.Year.ToString();

            if (this._verticalizzazioneProtocolloStorico.Attiva)
            {
                if (!this._verticalizzazioneProtocolloStorico.DataUltimaProtocollazione.HasValue)
                {
                    throw new Exception("E' ATTIVA LA VERTICALIZZAZIONE DEL PROTOCOLLO STORICO MA NON E' IMPOSTATO IL PARAMETRO DATAULTIMAPROTOCOLLAZIONE. CONTROLLARE LA CONFIGURAZIONE!!");
                }

                if (dataProtocollo.Date <= this._verticalizzazioneProtocolloStorico.DataUltimaProtocollazione.Value.Date)
                {
                    var protoLetto = _protocolloStorico.LeggiProtocolloStorico(idProtocollo, annoProtocollo, numeroProtocollo);
                    if (protoLetto != null)
                    {
                        _protocolloAttivo.CheckProtocolloLetto(annoProtocollo, numeroProtocollo, idProtocollo, protoLetto);
                        SetAllegatiWsReturn(protoLetto);
                        _protocolloSerializer.Serialize(ProtocolloLogsConstants.ResponseFileName, protoLetto);
                    }

                    return protoLetto;
                }
            }

            return LeggiProtocollo(idProtocollo, annoProtocollo, numeroProtocollo);
        }

        /// <summary>
        /// Setta l'id dell'allegato con il formato IDPROTOCOLLO|NUMERO|ANNO|IDALLEGATO e elimina dal tag Image dell'Xml il file in formato binario, vengono letti successivamente con il metodo LeggiAllegato
        /// </summary>
        /// <param name="allout"></param>
        private void SetAllegatiWsReturn(DatiProtocolloLetto datiProtLetto)
        {
            AllOut[] allout = datiProtLetto.Allegati;

            if (allout != null)
            {
                for (int i = 0; i < allout.Length; i++)
                {

                    string idprotocollo = String.IsNullOrEmpty(datiProtLetto.IdProtocollo) ? "0" : datiProtLetto.IdProtocollo;
                    string numeroProtocollo = datiProtLetto.NumeroProtocollo;
                    string annoProtocollo = datiProtLetto.AnnoProtocollo;
                    string idAllegato = allout[i].IDBase;
                    allout[i].IDBase = idprotocollo + "|" + numeroProtocollo + "|" + annoProtocollo + "|" + idAllegato + "|" + _datiProtocollazione.Software;
                    allout[i].Image = null;
                }
            }
        }

        public AllOut LeggiAllegato(string IdProtocollo, string numProtocollo, string annoProtocollo, string idAllegato)
        {
            _protocolloAttivo.IdProtocollo = IdProtocollo;
            _protocolloAttivo.NumProtocollo = numProtocollo;
            _protocolloAttivo.AnnoProtocollo = annoProtocollo;
            _protocolloAttivo.IdAllegato = idAllegato;
            _protocolloAttivo.EstraiEml = _verticalizzazioneProtocolloAttivo.EstraiEml;
            _protocolloAttivo.EstraiZip = _verticalizzazioneProtocolloAttivo.EstraiZip;
            _protocolloAttivo.EscludiFileDaEml = _verticalizzazioneProtocolloAttivo.EscludiFilesDaEml.Split('|');
            _protocolloAttivo.ZipExtensions = _verticalizzazioneProtocolloAttivo.ExtFileZip.Split(',');

            SetLettura();

            _protocolloLogs.DebugFormat("Lettura dell'allegato, ID Protocollo: {0}, Numero Protocollo: {1}, Anno Protocollo: {2}, Id Allegato: {3}", IdProtocollo, numProtocollo, annoProtocollo, idAllegato);
            AllOut res = _protocolloAttivo.LeggiAllegato();

            return res;
        }

        public AllOut LeggiAllegatoStorico(string IdProtocollo, string numProtocollo, string annoProtocollo, string idAllegato)
        {
            _protocolloStorico.IdProtocollo = IdProtocollo;
            _protocolloStorico.NumProtocollo = numProtocollo;
            _protocolloStorico.AnnoProtocollo = annoProtocollo;
            _protocolloStorico.IdAllegato = idAllegato;

            _protocolloStorico.EstraiEml = _verticalizzazioneProtocolloAttivo.EstraiEml;
            _protocolloStorico.EstraiZip = _verticalizzazioneProtocolloAttivo.EstraiZip;
            _protocolloStorico.EscludiFileDaEml = _verticalizzazioneProtocolloAttivo.EscludiFilesDaEml.Split('|');
            _protocolloStorico.ZipExtensions = _verticalizzazioneProtocolloAttivo.ExtFileZip.Split(',');

            _protocolloLogs.DebugFormat("Lettura dell'allegato dal protocollo storico, ID Protocollo: {0}, Numero Protocollo: {1}, Anno Protocollo: {2}, Id Allegato: {3}", IdProtocollo, numProtocollo, annoProtocollo, idAllegato);
            AllOut res = _protocolloStorico.LeggiAllegatoStorico();

            return res;
        }

        private void SetLettura()
        {
            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo.Operatore;
            _protocolloAttivo.EstraiEml = _verticalizzazioneProtocolloAttivo.EstraiEml;
            _protocolloAttivo.EstraiZip = _verticalizzazioneProtocolloAttivo.EstraiZip;
            _protocolloAttivo.EscludiFileDaEml = _verticalizzazioneProtocolloAttivo.EscludiFilesDaEml.Split('|');
            _protocolloAttivo.ZipExtensions = _verticalizzazioneProtocolloAttivo.ExtFileZip.Split(',');

            // var ammMgr = new AmministrazioniMgr(this._datiProtocollazione.Db);
            // var amm = ammMgr.GetByIdProtocollo(this._datiProtocollazione.IdComune, Convert.ToInt32(_verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault), this._datiProtocollazione.Software, this._datiProtocollazione.CodiceComune);
            var amm = RepositoryGetAmministrazioneByIdProtocollo(Convert.ToInt32(_verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault));

            if (amm == null)
                throw new Exception(String.Format("IL CODICEAMMINISTRAZIONE {0} PER L'IDCOMUNE {1} NON È PRESENTE NELLA TABELLA AMMINISTRAZIONI OPPURE NON È STATO SETTATO NELLA VERTICALIZZAZIONE PROTOCOLLO_ATTIVO", _verticalizzazioneProtocolloAttivo.Codiceamministrazionedefault, this._datiProtocollazione.IdComune));

            _protocolloAttivo.Ruolo = String.IsNullOrEmpty(amm.PROT_RUOLO) ? amm.PROT_UO : amm.PROT_RUOLO;
            _protocolloAttivo.Uo = amm.PROT_UO;
        }

        public ListaTipiDocumento ListaTipiDocumento()
        {
            var listaTipiDocumento = _protocolloAttivo.GetTipiDocumento();

            if (listaTipiDocumento != null && _protocolloLogs.IsDebugEnabled)
                _protocolloSerializer.Serialize(ProtocolloLogsConstants.TipiDocumentoSoapResponseFileName, listaTipiDocumento);

            return listaTipiDocumento;
        }

        #region Metodo usato per le classifiche

        public ListaTipiClassificaClassifica GetClassificaById(int IdClassifica)
        {
            try
            {
                return _protocolloAttivo.GetClassificaById(IdClassifica);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public ListaTipiClassifica ListaClassifiche()
        {
            try
            {
                SetListaClassifiche();

                var response = _protocolloAttivo.GetClassifiche();

                if (response != null)
                {
                    if (_protocolloLogs.IsDebugEnabled)
                        _protocolloSerializer.Serialize(ProtocolloLogsConstants.ResponseFileName, response);
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetListaClassifiche()
        {
            _protocolloAttivo.Operatore = _verticalizzazioneProtocolloAttivo != null ? _verticalizzazioneProtocolloAttivo.Operatore : String.Empty;
        }
        #endregion

        public DatiProtocolloRes MettiAllaFirma(Dati dati)
        {
            _dati = dati;

            if (!String.IsNullOrEmpty(_datiProtocollazione.Movimento.FKIDPROTOCOLLO))
                throw new Exception("ID PROTOCOLLO VALORIZZATO");

            //Deserializzo il file xml che ricevo in ingresso all'interno dell'oggetto Dati
            if (_dati != null)
                _protocolloSerializer.Serialize(ProtocolloLogsConstants.RequestFileName, _dati);
            else
                _dati = new Dati();

            _protoIn = new DatiProtocolloIn();
            SetMettiAllaFirma();
            var response = _protocolloAttivo.MettiAllaFirma(_protoIn);

            if (response != null)
            {
                _protocolloSerializer.Serialize(ProtocolloLogsConstants.ResponseFileName, response);

                // var movimentiMgr = new MovimentiMgr(this._datiProtocollazione.Db);
                // _datiProtocollazione.Movimento.FKIDPROTOCOLLO = string.IsNullOrEmpty(response.IdProtocollo) ? null : response.IdProtocollo;
                // movimentiMgr.Update(_datiProtocollazione.Movimento, ComportamentoElaborazioneEnum.NonElaborare);
                _datiProtocollazione.Movimento.FKIDPROTOCOLLO = string.IsNullOrEmpty(response.IdProtocollo) ? null : response.IdProtocollo;
                RepositoryUpdateMovimentoNonElaborare(_datiProtocollazione.Movimento);
            }

            return response;

        }



        public DatiProtocolloRes Protocollazione(ProtocolloEnum.TipoProvenienza provenienza, Dati dati, int iSource)
        {
            DatiProtocolloRes datiProtocollo = null;
            _dati = dati;

            if ((iSource != (int)ProtocolloEnum.Source.PROT_IST_MOV_AUT_BO))
            {
                // var alberoMgr = new AlberoProcMgr(_datiProtocollazione.Db);
                // bool isProtocollazioneAutomatica = alberoMgr.IsProtocollazioneAutomatica(_datiProtocollazione.CodiceInterventoProc.Value, iSource, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
                bool isProtocollazioneAutomatica = RepositoryVerificaProtocollazioneAutomaticaDaCodiceIntervento(_datiProtocollazione.CodiceInterventoProc.Value, iSource);

                if (!isProtocollazioneAutomatica)
                {
                    _protocolloLogs.InfoFormat("LA PROTOCOLLAZIONE AUTOMATICA NON E' CONFIGURATA, VERIFICARE LA CONFIGURAZIONE DEI PARAMETRI DEL PROTOCOLLO SULLA GESTIONE DELL'ALBERO DEGLI INTERVENTI, CODICE INTERVENTO PROC: {0}, SOURCE: {1}, IDCOMUNE: {2}, SOFTWARE: {3}, CODICE COMUNE: {4}", _datiProtocollazione.CodiceInterventoProc.Value, iSource, _datiProtocollazione.IdComune, _datiProtocollazione.Software, _datiProtocollazione.CodiceComune);
                    return new DatiProtocolloRes();
                }
            }

            if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA)
            {
                if (!String.IsNullOrEmpty(_datiProtocollazione.Istanza.FKIDPROTOCOLLO) || !String.IsNullOrEmpty(_datiProtocollazione.Istanza.NUMEROPROTOCOLLO) || _datiProtocollazione.Istanza.DATAPROTOCOLLO.HasValue)
                    throw new Exception(String.Format("L'ISTANZA CODICE {0} RISULTA AVERE ALCUNI O TUTTI I DATI PROTOCOLLATI, FKIDPROTOCOLLO: {1}, NUMEROPROTOCOLLO: {2}, DATAPROTOCOLLO: {3}", _datiProtocollazione.CodiceIstanza, _datiProtocollazione.Istanza.FKIDPROTOCOLLO, _datiProtocollazione.Istanza.NUMEROPROTOCOLLO, _datiProtocollazione.Istanza.DATAPROTOCOLLO.HasValue ? _datiProtocollazione.Istanza.DATAPROTOCOLLO.Value.ToString("dd/MM/yyyy") : String.Empty));
            }

            if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_MOVIMENTO)
            {
                if (!String.IsNullOrEmpty(_datiProtocollazione.Movimento.FKIDPROTOCOLLO) || !String.IsNullOrEmpty(_datiProtocollazione.Movimento.NUMEROPROTOCOLLO) || _datiProtocollazione.Movimento.DATAPROTOCOLLO.HasValue)
                    throw new Exception(String.Format("IL MOVIMENTO CODICE {0} RISULTA AVERE ALCUNI O TUTTI I DATI PROTOCOLLATI, FKIDPROTOCOLLO: {1}, NUMEROPROTOCOLLO: {2}, DATAPROTOCOLLO: {3}", _datiProtocollazione.CodiceMovimento, _datiProtocollazione.Movimento.FKIDPROTOCOLLO, _datiProtocollazione.Movimento.NUMEROPROTOCOLLO, _datiProtocollazione.Movimento.DATAPROTOCOLLO.HasValue ? _datiProtocollazione.Movimento.DATAPROTOCOLLO.Value.ToString("dd/MM/yyyy") : String.Empty));
            }

            if (_dati == null)
                _dati = new Dati();

            _protocolloSerializer.Serialize(ProtocolloLogsConstants.RequestFileName, _dati);

            _protoIn = new DatiProtocolloIn();

            SetProtocollo(provenienza, iSource);

            if (_protocolloLogs.IsDebugEnabled)
                _protocolloSerializer.Serialize(ProtocolloLogsConstants.DatiProtocolloInFileName, _protoIn);

            datiProtocollo = _protocolloAttivo.Protocollazione(_protoIn);
            _protocolloLogs.InfoFormat("PROTOCOLLAZIONE AVVENUTA CON SUCCESSO: Numero Protocollo: {0} | DataProtocollo: {1} | AnnoProtocollo: {2}", datiProtocollo.NumeroProtocollo, datiProtocollo.DataProtocollo, datiProtocollo.AnnoProtocollo);

            if (datiProtocollo == null)
            {
                return null;
            }
            _protocolloSerializer.SerializeAndValidateStream(datiProtocollo, ProtocolloLogsConstants.ResponseFileName);

            if (String.IsNullOrEmpty(datiProtocollo.DataProtocollo))
                throw new Exception(String.Format("DATA PROTOCOLLO NON VALORIZZATA DOPO AVER EFFETTUATO LA PROTOCOLLAZIONE DELL'ISTANZA CODICE: {0}", _datiProtocollazione.CodiceIstanza));

            if (String.IsNullOrEmpty(datiProtocollo.NumeroProtocollo))
                throw new Exception(String.Format("NUMERO PROTOCOLLO NON VALORIZZATO DOPO AVER EFFETTUATO LA PROTOCOLLAZIONE DELL'ISTANZA CODICE: {0}", _datiProtocollazione.CodiceIstanza));

            var numeroProtocollo = datiProtocollo.NumeroProtocollo;
            var dataProtocollo = DateTime.ParseExact(datiProtocollo.DataProtocollo, "dd/MM/yyyy", null);
            var idProtocollo = datiProtocollo.IdProtocollo;

            if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_ISTANZA)
            {
                var codiceIstanza = Convert.ToInt32(this._datiProtocollazione.CodiceIstanza);
                var tipoMovimento = this._datiProtocollazione.Istanza.TIPOMOVAVVIO;

                _protocolloLogs.DebugFormat("Prima dell'Update dell'istanza {0} con i dati del protocollo, id protocollo: {1}, data protocollo: {2}, numero protocollo: {3}", this._datiProtocollazione.CodiceIstanza, idProtocollo, dataProtocollo, numeroProtocollo);

                RepositoryAggiornaRiferimentoProtocolloIstanza(codiceIstanza, idProtocollo, numeroProtocollo, dataProtocollo);

                _protocolloLogs.DebugFormat("Update dell'istanza {0} con i dati del protocollo, id protocollo: {1}, data protocollo: {2}, numero protocollo: {3}", this._datiProtocollazione.CodiceIstanza, idProtocollo, dataProtocollo, numeroProtocollo);

                var mov = RepositoryGetMovimentoByTipoMovimento(codiceIstanza, tipoMovimento);

                if (mov != null)
                {
                    _protocolloLogs.DebugFormat("Prima dell'update del movimento di avvio, codice movimento {0}", mov.CODICEMOVIMENTO);

                    var idMovimento = Convert.ToInt32(mov.CODICEMOVIMENTO);
                    RepositoryMovimentiUpdateDatiProtocollo(idMovimento, idProtocollo, numeroProtocollo, dataProtocollo);

                    _protocolloLogs.DebugFormat("Update del movimento di avvio, codice movimento {0}", mov.CODICEMOVIMENTO);
                }
            }
            else if (_datiProtocollazione.TipoAmbito == ProtocolloEnum.AmbitoProtocollazioneEnum.DA_MOVIMENTO)
            {

                _protocolloLogs.DebugFormat("Prima dell'update del movimento {0} con i dati del protocollo, idprotocollo: {1}, data protocollo: {2}, numero protocollo: {3}", this._datiProtocollazione.CodiceMovimento, idProtocollo, dataProtocollo, numeroProtocollo);

                var idMovimento = Convert.ToInt32(this._datiProtocollazione.CodiceMovimento);
                RepositoryMovimentiUpdateDatiProtocollo(idMovimento, idProtocollo, numeroProtocollo, dataProtocollo);

                _protocolloLogs.DebugFormat("Update del movimento {0} con i dati del protocollo, idprotocollo: {1}, data protocollo: {2}, numero protocollo: {3}", this._datiProtocollazione.CodiceMovimento, idProtocollo, dataProtocollo, numeroProtocollo);
            }

            datiProtocollo.CodiciOggettoAllegati = _protoIn.Allegati
                                                    .Select(x => Convert.ToInt32(x.CODICEOGGETTO))
                                                    .ToArray();

            RepositoryProtocolloMetadatiInsert(idProtocollo, datiProtocollo.Metadati);

            return datiProtocollo;
        }

        private void RepositoryProtocolloMetadatiInsert(string idProtocollo, List<ProtocolloMetadati> metadati)
        {
            if (metadati != null)
            {
                var mgr = new ProtocolloMetadatiMgr(this._datiProtocollazione.Db);
                mgr.Insert(this._datiProtocollazione.IdComune, idProtocollo, metadati);
            }
        }

        public DatiProtocolloRes Registrazione(string registro, Dati dati, ProtocolloEnum.TipoProvenienza provenienza = ProtocolloEnum.TipoProvenienza.BACKOFFICE, int iSource = (int)ProtocolloEnum.Source.PROT_IST_MOV_AUT_BO)
        {
            _dati = dati;

            if (_dati == null)
                _dati = new Dati();

            _protocolloSerializer.Serialize(ProtocolloLogsConstants.RequestFileName, _dati);

            _protoIn = new DatiProtocolloIn();
            SetProtocollo(provenienza, iSource);

            if (_protocolloLogs.IsDebugEnabled)
                _protocolloSerializer.Serialize(ProtocolloLogsConstants.DatiProtocolloInFileName, _protoIn);

            return _protocolloAttivo.Registrazione(registro, _protoIn);
        }

        public CreaUnitaDocumentaleResponse CreaUnitaDocumentale(CreaUnitaDocumentaleRequest request)
        {
            //var protoAllegatiMgr = new ProtocolloAllegatiMgr(this._datiProtocollazione.Db);
            var protocolloAllegati = new List<ProtocolloAllegati>();

            foreach (var allegato in request.Allegati)
            {
                var codiceOggetto = Convert.ToInt32(allegato.Cod);
                var oggetto = RepositoryGetOggettoById(codiceOggetto);
                var percorso = RepositoryGetPercorsoFile(codiceOggetto);

                if (oggetto == null)
                {
                    throw new Exception(String.Format("IL CODICEOGGETTO {0} PER L'IDCOMUNE {1} NON È PRESENTE NELLA TABELLA OGGETTI", allegato.Cod, _datiProtocollazione.IdComune));
                }

                var contentType = RepositoryGetContentType(oggetto);
                var nomeAllegato = new NomeFileAllegato(_datiProtocollazione.IdComune, _datiProtocollazione.CodiceComune, oggetto, allegato.Descrizione, _verticalizzazioneProtocolloAttivo.NomeFileMaxLength);

                var protocolloAllegato = allegato.ToProtocolloAllegati(oggetto, percorso, contentType, nomeAllegato, _verticalizzazioneProtocolloAttivo, protocolloAllegati);

                protocolloAllegati.Add(protocolloAllegato);
            }

            return _protocolloAttivo.CreaUnitaDocumentale(request.TipoDocumento, protocolloAllegati);
        }

        private bool GetParamVertBool(string sParamVertValue, string sParamVertName)
        {
            switch (sParamVertValue)
            {
                case "":
                case "0":
                    return false;
                case "1":
                    return true;
                default:
                    throw new Exception("Il valore del parametro " + sParamVertName + " non è corretto! Valori ammissibili 0 ed 1, valore settato: " + sParamVertValue);
            }
        }

        public void Dispose()
        {
            if (this._datiProtocollazione.Db == null)
                throw new Exception("ERRORE DURANTE IL DISPOSE DEL MANAGER, IL DATABASE NON E' STATO GENERATO");

            this._datiProtocollazione.Db.Dispose();
        }
    }
}