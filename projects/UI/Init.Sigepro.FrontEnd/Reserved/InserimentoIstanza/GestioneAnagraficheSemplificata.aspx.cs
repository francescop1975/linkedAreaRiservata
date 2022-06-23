using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche;
using Init.Sigepro.FrontEnd.Infrastructure.PropertiesResolver;
using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.CondizioniUscitaSteps;
using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Exceptions;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{


    public partial class GestioneAnagraficheSemplificata : GestioneAnagraficheStepPage
    {

        [Inject]
        public CondizioniUscitaGestioneAnagraficheSemplificata _condizioneUscita { get; set; }

        [Inject]
        protected IConfigurazione<ParametriPresentazioneDomanda> _configurazione { get; set; }

        #region parametri letti dalla configurazione steps

        public string TestoInserimentoUtenteLoggato
        {
            get
            {
                return (this.ViewState["TestoInserimentoUtenteLoggato"] == null) ? null : this.ViewState["TestoInserimentoUtenteLoggato"].ToString();
            }
            set
            {
                this.ViewState["TestoInserimentoUtenteLoggato"] = value;
            }
        }

        public string TestoInserimentoAziendaIntermediario
        {
            get
            {
                return (this.ViewState["TestoInserimentoAziendaIntermediario"] == null) ? null : this.ViewState["TestoInserimentoAziendaIntermediario"].ToString();
            }
            set
            {
                this.ViewState["TestoInserimentoAziendaIntermediario"] = value;
            }
        }

        public string TestoInserimentoRichiedente
        {
            get
            {
                return (this.ViewState["TestoInserimentoRichiedente"] == null) ? null : this.ViewState["TestoInserimentoRichiedente"].ToString();
            }
            set
            {
                this.ViewState["TestoInserimentoRichiedente"] = value;
            }
        }

        public string TestoInserimentoAziendaRichiedente
        {
            get
            {
                return (this.ViewState["TestoInserimentoAziendaRichiedente"] == null) ? null : this.ViewState["TestoInserimentoAziendaRichiedente"].ToString();
            }
            set
            {
                this.ViewState["TestoInserimentoAziendaRichiedente"] = value;
            }
        }

        public string TestoTuttiISoggettiPresenti
        {
            get
            {
                return (this.ViewState["TestoTuttiISoggettiPresenti"] == null) ? null : this.ViewState["TestoTuttiISoggettiPresenti"].ToString();
            }
            set
            {
                this.ViewState["TestoTuttiISoggettiPresenti"] = value;
            }
        }

        public bool UtenteLoggatoPresente
        {
            get
            {
                return ReadFacade.Domanda.Anagrafiche.Anagrafiche.Where(x => x.Codicefiscale.ToUpper() == UserAuthenticationResult.DatiUtente.Codicefiscale.ToUpper()).Any();
            }
        }

        public int PFCampoFax
        {
            set
            {
                this.DettagliPf.FaxVisible = value > 0;
                this.DettagliPf.FaxObbligatorio = value > 1;
            }
        }

        public int PFCampoEmail
        {
            set
            {
                this.DettagliPf.EmailVisible = value > 0;
                this.DettagliPf.EmailObbligatoria = value > 1;
            }
        }

        public int PFCampoPec
        {
            set
            {
                this.DettagliPf.PecVisible = value > 0;
                this.DettagliPf.PecObbligatoria = value > 1;
            }
        }

        public int PFCampoCorrispondenza
        {
            set
            {
                this.DettagliPf.CorrispondenzaVisibile = value > 0;
                this.DettagliPf.CorrispondenzaObbligatoria = value > 1;
            }
        }

        public int PGSedeLegale
        {
            set
            {
                this.DettagliPg.SedeLegaleVisibile = value > 0;
                this.DettagliPg.SedeLegaleObbligatoria = value > 1;
            }
        }

        public int PGDataCostituzione
        {
            set
            {
                this.DettagliPg.DataCostituzioneVisibile = value > 0;
                this.DettagliPg.DataCostituzioneObbligatoria = value > 1;
            }
        }

        public int PGTelefono
        {
            set
            {
                this.DettagliPg.TelefonoVisibile = value > 0;
                this.DettagliPg.TelefonoObbligatorio = value > 1;
            }
        }

        public int PGCellulare
        {
            set
            {
                this.DettagliPg.CellulareVisibile = value > 0;
                this.DettagliPg.CellulareObbligatorio = value > 1;
            }
        }

        public int PGFax
        {
            set
            {
                this.DettagliPg.FaxVisibile = value > 0;
                this.DettagliPg.FaxObbligatorio = value > 1;
            }
        }

        public int PGCciaa
        {
            set
            {
                this.DettagliPg.CciaaVisibile = value > 0;
                this.DettagliPg.CciaaObbligatoria = value > 1;
            }
        }

        public int PGRegTrib
        {
            set
            {
                this.DettagliPg.RegTribVisibile = value > 0;
                this.DettagliPg.RegTribObbligatorio = value > 1;
            }
        }

        public int PGRea
        {
            set
            {
                this.DettagliPg.ReaVisibile = value > 0;
                this.DettagliPg.ReaObbligatoria = value > 1;
            }
        }

        public int PGInps
        {
            set
            {
                this.DettagliPg.InpsVisibile = value > 0;
                this.DettagliPg.InpsObbligatoria = value > 1;
            }
        }

        public int PGInail
        {
            set
            {
                this.DettagliPg.InailVisibile = value > 0;
                this.DettagliPg.InailObbligatoria = value > 1;
            }
        }

        public int PGEmail
        {
            set
            {
                this.DettagliPg.EmailVisibile = value > 0;
                this.DettagliPg.EmailObbligatoria = value > 1;
            }
        }

        public int PGPec
        {
            set
            {
                this.DettagliPg.PecVisibile = value > 0;
                this.DettagliPg.PecObbligatoria = value > 1;
            }
        }

        public int PGPartitaIva
        {
            set
            {
                this.DettagliPg.PartitaIvaVisibile = value > 0;
                this.DettagliPg.PartitaIvaObbligatoria = value > 1;
            }
        }

        public int PGCampoCorrispondenza
        {
            set
            {
                this.DettagliPg.CorrispondenzaVisibile = value > 0;
                this.DettagliPg.CorrispondenzaObbligatoria = value > 1;
            }
        }

        public string LimitaDatiAlbo
        {
            set
            {
                this.DettagliPf.LimitaDatiAlbo = value;
            }
        }

        public string PFTitoloBloccoIndirizzoCorrispondenza
        {
            get { return this.DettagliPf.TitoloBloccoIndirizzoCorrispondenza; }
            set { this.DettagliPf.TitoloBloccoIndirizzoCorrispondenza = value; }
        }

        public int PFCampoCittadinanza
        {
            set
            {
                this.DettagliPf.CittadinanzaVisible = value > 0;
                this.DettagliPf.CittadinanzaObbligatoria = value > 1;
            }
        }

        public int PFCampoTitolo
        {
            set
            {
                this.DettagliPf.TitoloVisibile = value > 0;
                this.DettagliPf.TitoloObbligatorio = value > 1;
            }
        }

        public bool GestioneSoggettoUnico
        {
            set
            {
                this.cmdNuovo.Visible = !value;
                this.DettagliPf.GestioneSoggettoUnico = !value;
            }
        }

        public int PFCampoResidenza
        {
            set
            {
                this.DettagliPf.ResidenzaVisible = value > 0;
                this.DettagliPf.ResidenzaObbligatoria = value > 1;
            }
        }

        public int PFCampoTelefono
        {
            set
            {
                this.DettagliPf.TelefonoVisible = value > 0;
                this.DettagliPf.TelefonoObbligatorio = value > 1;
            }
        }

        public int PFCampoCellulare
        {
            set
            {
                this.DettagliPf.CellulareVisible = value > 0;
                this.DettagliPf.CellulareObbligatorio = value > 1;
            }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            this.cmbTipoPersona.Inner.SelectedIndexChanged += (pippo, pluto) =>
            {
                BindTipoSoggetto();
            };
            base.OnInit(e);
        }

        private RuoloTipoSoggettoDomandaEnum? GetTipoSoggettoDaInserire()
        {
            List<AnagraficaDomanda> anagrafiche = new List<AnagraficaDomanda>();

            var soggettoLoggatoPresente = ReadFacade.Domanda.Anagrafiche.Anagrafiche.FirstOrDefault(x => x.Codicefiscale.Equals(UserAuthenticationResult.DatiUtente.Codicefiscale, StringComparison.InvariantCultureIgnoreCase));

            if (soggettoLoggatoPresente == null)
            {
                return RuoloTipoSoggettoDomandaEnum.Richiedente;
            }
            else if (soggettoLoggatoPresente.TipoSoggetto.RichiedeAnagraficaCollegata && !soggettoLoggatoPresente.IdAnagraficaCollegata.HasValue)
            {
                return (soggettoLoggatoPresente.TipoSoggetto.Ruolo == RuoloTipoSoggettoDomandaEnum.Richiedente) ? RuoloTipoSoggettoDomandaEnum.Azienda : RuoloTipoSoggettoDomandaEnum.Altro;
            }



            var richiedentePresente = ReadFacade.Domanda.Anagrafiche.Anagrafiche.Where(x => x.TipoSoggetto.Ruolo == RuoloTipoSoggettoDomandaEnum.Richiedente).Any();

            if (!richiedentePresente)
                return RuoloTipoSoggettoDomandaEnum.Richiedente;

            var aziendaRichiesta = ReadFacade.Domanda.Anagrafiche.Anagrafiche.Where(x => x.TipoSoggetto.Ruolo == RuoloTipoSoggettoDomandaEnum.Richiedente && x.TipoSoggetto.RichiedeAnagraficaCollegata).Any();

            if (!aziendaRichiesta)
                return null;

            var aziendaPresente = ReadFacade.Domanda.Anagrafiche.Anagrafiche.Where(x => x.TipoSoggetto.Ruolo == RuoloTipoSoggettoDomandaEnum.Azienda).Any();

            if (!aziendaPresente)
                return RuoloTipoSoggettoDomandaEnum.Azienda;

            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // il service si occupa del salvataggio dei dati
            this.Master.IgnoraSalvataggioDati = true;

            this.TipoSoggetto.CodiceIntervento = CodiceIntervento;

            _condizioneUscita.FlagVerificaPecObbligatoria = VerificaPecObbligatoria;
            _condizioneUscita.MessaggioUtenteNonPresente = MessaggioUtenteNonPresente;

            InizializzaDettagliPFPG();

            if (!IsPostBack)
            {
                if (!this.UtenteLoggatoPresente)
                {
                    ImpostaDatiUtenteCorrente();
                }
                else
                {
                    DataBind();
                }

                this.cmbTipoPersona.Enabled = this.cmbTipoPersona.Visible = !this._configurazione.Parametri.RichiedenteSoloPersoneFisiche;

                BindTipoSoggetto();
            }

            SettaTestoSostitutivo();
        }

        protected void InizializzaDettagliPFPG()
        {
            DettagliPf.CancelEdit += new EventHandler(OnEndEdit);
            DettagliPf.AcceptEdit += new DettagliAnagraficaPf.OnAcceptEdit(OnAcceptEdit);
            DettagliPf.GetAnagrafeRow += new DettagliAnagraficaPf.OnGetAnagrafeRow(GetAnagrafeRow);
            DettagliPf.GetTipiSoggetto += new DettagliAnagraficaPf.OnGetTipiSoggettoPfDelegate(GetTipiSoggettoPf);
            DettagliPf.GetTipoSoggetto += new DettagliAnagraficaPf.OnGetTipoSoggetto(GetTipoSoggetto);
            DettagliPf.GetDatiComune += new DettagliAnagraficaPf.OnGetDatiComune(GetDatiComune);
            DettagliPf.GetDatiProvincia += new DettagliAnagraficaPf.OnGetDatiProvincia(GetDatiProvincia);
            DettagliPf.GetDatiCittadinanza += new DettagliAnagraficaPf.OnGetDatiCittadinanza(GetDatiCittadinanza);

            DettagliPg.CancelEdit += new EventHandler(OnEndEdit);
            DettagliPg.AcceptEdit += new DettagliAnagraficaPg.OnAcceptEdit(OnAcceptEdit);
            DettagliPg.GetAnagrafeRow += new DettagliAnagraficaPg.OnGetAnagrafeRow(GetAnagrafeRow);
            DettagliPg.GetDatiComune += new DettagliAnagraficaPg.OnGetDatiComune(GetDatiComune);
            DettagliPg.GetDatiProvincia += new DettagliAnagraficaPg.OnGetDatiProvincia(GetDatiProvincia);

            DettagliPf.ErroreInserimento += new ErrorDelegate(OnErroreInserimento);
            DettagliPg.ErroreInserimento += new ErrorDelegate(OnErroreInserimento);

            DettagliPf.CodiceIntervento = this.CodiceIntervento;
            DettagliPg.CodiceIntervento = this.CodiceIntervento;

            DettagliPf.EmailoPecObbligatori = this.EmailoPecObbligatori;
            DettagliPg.EmailoPecObbligatori = this.EmailoPecObbligatori;

            DettagliPf.TelefonooCellulareObbligatori = this.TelefonooCellulareObbligatori;
            DettagliPg.TelefonooCellulareObbligatori = this.TelefonooCellulareObbligatori;
        }

        #region ciclo di vita dello step

        public override void OnInitializeStep()
        {
            AnagraficheService.SincronizzaFlagsTipiSoggetto(this.IdDomanda);
            AnagraficheService.VerificaFlagsCittadiniExtracomunitari(this.IdDomanda);
        }

        public override bool CanEnterStep()
        {
            return _condizioneIngresso.Verificata();
        }

        public override bool CanExitStep()
        {
            try
            {
                this._condizioneUscita.VerificaPresenzaUtenteLoggato = this.UtenteLoggatoPresente;

                return _condizioneUscita.Verificata();
            }
            catch (StepException ex)
            {
                Errori.AddRange(ex.ErrorMessages);
            }

            return false;
        }

        #endregion


        void OnAcceptEdit(AnagraficaDomanda row)
        {
            try
            {
                var idAnagrafica = AnagraficheService.SalvaAnagrafica(IdDomanda, row);

                if (row.TipoPersona == TipoPersonaEnum.Giuridica)
                {
                    CercaECollegaPersonaFisica(idAnagrafica);
                }

                InizializzaRicercaUtenti();

                if (!GetTipoSoggettoDaInserire().HasValue)
                {
                    OnEndEdit(this, EventArgs.Empty);
                }

                SettaTestoSostitutivo();
            }
            catch (Exception ex)
            {
                m_logger.ErrorFormat("Errore in OnAcceptEdit: {0}", ex.ToString());

                Errori.Add(ex.Message);
            }
        }

        private void CercaECollegaPersonaFisica(int idAnagrafica)
        {
            var anagraficaCorrente = AnagraficheService.GetById(IdDomanda, idAnagrafica);
            var anagraficaCollegabile = (anagraficaCorrente.TipoSoggetto.Ruolo == RuoloTipoSoggettoDomandaEnum.Altro) ? AnagraficheService.GetTecnico(IdDomanda) : AnagraficheService.GetRichiedente(IdDomanda);

            if (anagraficaCorrente.Id != anagraficaCollegabile.Id)
            {
                AnagraficheService.CollegaAziendaAdAnagrafica(IdDomanda, anagraficaCollegabile.Id.Value, anagraficaCorrente.Id.Value);
            }
        }

        private void BindTipoSoggetto()
        {
            var tsdi = GetTipoSoggettoDaInserire();

            if (tsdi.HasValue)
                BindTipoSoggetto(tsdi.Value);
        }

        private void BindTipoSoggetto(RuoloTipoSoggettoDomandaEnum ruolo)
        {
            TipoSoggetto.TipoSoggetto = cmbTipoPersona.SelectedValue;
            TipoSoggetto.RuoloTipoSoggetto = ruolo;
            TipoSoggetto.DataBind();
        }

        void OnErroreInserimento(string message)
        {
            Errori.Add(message);
        }

        public void OnEndEdit(object sender, EventArgs e)
        {
            multiview.ActiveViewIndex = PageViews.Lista;
            this.Master.MostraPaginatoreSteps = true;

            DataBind();

            SettaTestoSostitutivo();

        }

        private void InizializzaBottoneAggiungiSoggetto()
        {
            if (GetTipoSoggettoDaInserire() != null)
            {
                this.cmdNuovo.Visible = true;
                SettaTestoBottoneAggiungiSoggetto();
            }
            else
            {
                this.cmdNuovo.Visible = false;
            }
        }

        private void SettaTestoSostitutivo()
        {
            this.Master.MostraBottoneAvanti = false;

            if (!this.UtenteLoggatoPresente)
            {
                this.ltrTestoSostitutivo.Text = this.TestoInserimentoUtenteLoggato;
            }
            else
            {
                var t = GetTipoSoggettoDaInserire();

                if (t.HasValue)
                {
                    if (t.Value == RuoloTipoSoggettoDomandaEnum.Azienda)
                    {
                        this.ltrTestoSostitutivo.Text = this.TestoInserimentoAziendaRichiedente;
                    }
                    else if (t.Value == RuoloTipoSoggettoDomandaEnum.Richiedente)
                    {
                        this.ltrTestoSostitutivo.Text = this.TestoInserimentoRichiedente;
                    }
                    else if (t.Value == RuoloTipoSoggettoDomandaEnum.Altro)
                    {
                        this.ltrTestoSostitutivo.Text = this.TestoInserimentoAziendaIntermediario;
                    }
                }
                else
                {
                    this.ltrTestoSostitutivo.Text = this.TestoTuttiISoggettiPresenti;
                    this.Master.MostraBottoneAvanti = true;
                }
            }

            this.ltrTestoSostitutivo.Text = new SostituisciStringaResolver(this.ltrTestoSostitutivo.Text).Risolvi(this);
        }

        private void SettaTestoBottoneAggiungiSoggetto()
        {

            if (!this.UtenteLoggatoPresente)
            {
                cmdNuovo.Text = Constants.TestoAggiungiUtenteLoggato;
            }
            else
            {
                var t = GetTipoSoggettoDaInserire();

                if (t.HasValue)
                {
                    if (t.Value == RuoloTipoSoggettoDomandaEnum.Azienda)
                    {
                        cmdNuovo.Text = Constants.TestoAggiungiAzienda;
                    }
                    else if (t.Value == RuoloTipoSoggettoDomandaEnum.Richiedente)
                    {
                        cmdNuovo.Text = Constants.TestoAggiungiRichiedente;
                    }
                    else if (t.Value == RuoloTipoSoggettoDomandaEnum.Altro)
                    {
                        cmdNuovo.Text = Constants.TestoAggiungiAziendaUtenteLoggato;
                    }
                }
            }
        }

        public void cmdNuovo_Click(object sender, EventArgs e)
        {

            this.Master.MostraPaginatoreSteps = false;

            InizializzaRicercaUtenti();

            this.Master.MostraBottoneAvanti = false;
        }

        public void cmdCercaCf_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCodiceFiscale.Text))
            {
                if (cmbTipoPersona.SelectedValue == "F")
                    Errori.Add("Inserire un codice fiscale");
                else
                    Errori.Add("Inserire un codice fiscale o una partita iva");
                return;
            }

            txtCodiceFiscale.Text = txtCodiceFiscale.Text.ToUpper();

            if (!Regex.IsMatch(txtCodiceFiscale.Text, "^[a-zA-Z0-9]+$"))
            {
                var messaggioErrore = new StringBuilder();

                messaggioErrore.Append(cmbTipoPersona.SelectedValue == "F" ? "Il codice fiscale immesso" : "La partita iva immessa");
                messaggioErrore.Append(" contiene caratteri non validi. Verificare i dati immessi.");

                Errori.Add(messaggioErrore.ToString());

                return;
            }

            try
            {
                var codiceFiscale = txtCodiceFiscale.Text;
                var tipoPersona = cmbTipoPersona.SelectedValue;
                var tipoPersonaEnum = tipoPersona == "F" ? TipoPersonaEnum.Fisica : TipoPersonaEnum.Giuridica;

                AnagraficheService.IgnoraRicercaBackofficePerPersoneFisiche = this.IgnoraRicercaBackofficePerPersoneFisiche;

                var anagrafe = AnagraficheService.RicercaAnagrafica(IdDomanda, tipoPersonaEnum, codiceFiscale);

                anagrafe.TipoSoggetto = GetTipoSoggetto(Convert.ToInt32(this.TipoSoggetto.SelectedValue)).ToTipoSoggettoDomanda();

                Edit(anagrafe);
            }
            catch (Exception ex) // Errore di comunicazione con il web service... Come lo gestiamo?
            {
                Errori.Add("Si è verificato un errore durante la ricerca dell'anagrafica");
                LogManager.GetLogger(this.GetType()).Error(ex.ToString());
                m_logger.ErrorFormat(ex.ToString());
            }
        }

        private void ImpostaDatiUtenteCorrente()
        {
            var newRow = new AnagrafeAdapter(UserAuthenticationResult.DatiUtente.ToWsAnagrafe()).ToAnagrafeRow();

            Edit(AnagraficaDomanda.FromAnagrafeRow(newRow));
        }

        private void InizializzaRicercaUtenti()
        {
            if (!this.UtenteLoggatoPresente)
            {
                ImpostaDatiUtenteCorrente();
            }
            else
            {
                var tsdi = GetTipoSoggettoDaInserire();

                if (tsdi.HasValue)
                {
                    var tsd = new TipoSoggettoDomanda();
                    tsd.Ruolo = tsdi.Value;


                    cmbTipoPersona.SelectedValue = (tsd.Ruolo == RuoloTipoSoggettoDomandaEnum.Richiedente || tsd.Ruolo == RuoloTipoSoggettoDomandaEnum.Tecnico) ? "F" : "G";
                    BindTipoSoggetto(tsd.Ruolo);

                    if (TipoSoggetto.Items.Count == 1 && cmbTipoPersona.SelectedValue == "F" && !this._configurazione.Parametri.RichiedenteSoloPersoneFisiche)
                    {
                        cmbTipoPersona.SelectedValue = "G";
                        BindTipoSoggetto(tsd.Ruolo);
                    }

                    if (TipoSoggetto.Items.Count == 2)
                    {
                        TipoSoggetto.SelectedIndex = 1;
                    }

                    multiview.ActiveViewIndex = PageViews.NuovaAnagrafica;
                }
            }

        }

        #region gestione della modifica dati

        protected void Edit(AnagraficaDomanda row)
        {
            var nuovaAnagrafica = new NuovaAnagraficaSpecification().IsSatisfiedBy(row);

            var permettiModificheAdAnagrafiche = nuovaAnagrafica || (!nuovaAnagrafica && RendiModificabiliDatiAnagraficheEsistenti);

            if (row.TipoPersona == TipoPersonaEnum.Fisica) // PersonaFisica
            {
                multiview.ActiveViewIndex = PageViews.EditPersonaFisica;

                if (VerificaPecObbligatoria)
                    DettagliPf.MessaggioVerificaPec = MessaggioAvvertimentoVerificaPEC;

                DettagliPf.PermettiModificaDatiAnagrafici = permettiModificheAdAnagrafiche;
                DettagliPf.PermettiModificaTipoSoggetto = (String.IsNullOrEmpty(TipoSoggetto.SelectedValue) && row.TipoSoggetto.Id == -1);
                DettagliPf.DataSource = row.ToAnagrafeRow();
                DettagliPf.DataBind();

            }
            else
            {
                multiview.ActiveViewIndex = PageViews.EditPersonaGiuridica;

                DettagliPg.PermettiModificaDatiAnagrafici = permettiModificheAdAnagrafiche;
                DettagliPg.PermettiModificaTipoSoggetto = (String.IsNullOrEmpty(TipoSoggetto.SelectedValue) && row.TipoSoggetto.Id == -1);
                DettagliPg.DataSource = row.ToAnagrafeRow();
                DettagliPg.DataBind();
            }

            Master.MostraPaginatoreSteps = false;
        }

        #endregion

        #region gestione della griglia

        public class RichiedentiBindingItem
        {
            public int Id { get; set; }
            public string Nominativo { get; set; }
            public string InQualitaDi { get; set; }
            public string AziendaCollegata { get; set; }
            public string TestoLinkCollegaAzienda { get; set; }
            public bool MostraLinkCollegaAzienda { get; set; }
            public IEnumerable<KeyValuePair<int, string>> AziendeCollegabili { get; set; }
        }

        public override void DataBind()
        {
            var aziendeCollegabili = ReadFacade.Domanda
                                               .Anagrafiche
                                               .GetAnagraficheCollegabili()
                                               .Select(x => new KeyValuePair<int, string>(x.Id.Value, x.ToString()))
                                               .ToList();

            if (aziendeCollegabili.Count == 0)
                aziendeCollegabili.Add(new KeyValuePair<int, string>(-1, "Tra i soggetti dell'istanza non sono presenti aziende"));

            dgRichiedenti.DataSource = ReadFacade.Domanda
                                                 .Anagrafiche
                                                 .Anagrafiche
                                                 .OrderBy(x => x.TipoPersona)
                                                 .ThenBy(x => x.Nominativo)
                                                 .Select(x => new RichiedentiBindingItem
                                                 {
                                                     Id = x.Id.Value,
                                                     Nominativo = x.ToString(),
                                                     InQualitaDi = x.TipoSoggetto.ToString(),
                                                     AziendaCollegata = x.AnagraficaCollegata != null ? x.AnagraficaCollegata.ToString() : String.Empty,
                                                     MostraLinkCollegaAzienda = x.TipoSoggetto.RichiedeAnagraficaCollegata,
                                                     TestoLinkCollegaAzienda = x.IdAnagraficaCollegata.HasValue ? "Modifica collegamento" : "Collega azienda",
                                                     AziendeCollegabili = aziendeCollegabili
                                                 });
            dgRichiedenti.DataBind();

            InizializzaBottoneAggiungiSoggetto();

            BindTipiSoggetto();


            //this.Master.MostraBottoneAvanti = true;
        }

        protected void dgRichiedenti_CancelCommand(object source, GridViewCancelEditEventArgs e)
        {
            Master.MostraPaginatoreSteps = true;
            dgRichiedenti.EditIndex = -1;
            DataBind();
        }

        protected void dgRichiedenti_UpdateCommand(object source, GridViewUpdateEventArgs e)
        {
            var ddl = (DropDownList)dgRichiedenti.Rows[e.RowIndex].FindControl("ddlAziendeCollegabili");
            var idAziendaColl = Convert.ToInt32(ddl.SelectedValue);

            var key = Convert.ToInt32(dgRichiedenti.DataKeys[e.RowIndex].Value);

            AnagraficheService.CollegaAziendaAdAnagrafica(IdDomanda, key, idAziendaColl);

            Master.MostraPaginatoreSteps = true;
            dgRichiedenti.EditIndex = -1;
            DataBind();

            SettaTestoSostitutivo();
        }

        public void dgRichiedenti_DeleteCommand(object source, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(dgRichiedenti.DataKeys[e.RowIndex].Value);

            var anagrafica = AnagraficheService.GetById(IdDomanda, id);

            if (anagrafica.IdAnagraficaCollegata.HasValue)
            {
                AnagraficheService.RimuoviAnagrafica(IdDomanda, anagrafica.IdAnagraficaCollegata.Value);
            }

            AnagraficheService.RimuoviAnagrafica(IdDomanda, id);

            DataBind();

            SettaTestoSostitutivo();
        }

        protected void dgRichiedenti_EditCommand(object source, GridViewEditEventArgs e)
        {
            Master.IgnoraSalvataggioDati = true;
            Master.MostraPaginatoreSteps = false;

            dgRichiedenti.EditIndex = e.NewEditIndex;
            DataBind();
        }

        public void dgRichiedenti_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pk = Convert.ToInt32(dgRichiedenti.DataKeys[dgRichiedenti.SelectedIndex].Value);

            Edit(ReadFacade.Domanda.Anagrafiche.GetById(pk));
        }


        #endregion

        private void BindTipiSoggetto()
        {
            TipoSoggetto.DataBind();
        }

        public void multiview_ActiveViewChanged(object sender, EventArgs e)
        {
        }
    }
}