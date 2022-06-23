using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche;
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
    public partial class DatiAnagrafici : GestioneAnagraficheStepPage
    {
        [Inject]
        public CondizioniUscitaGestioneAnagrafiche _condizioneUscita { get; set; }

        //public delegate void ErrorDelegate(string message);

        public string ComboRicercaTestoElementoSocieta
        {
            get { object o = this.ViewState["ComboRicercaTestoElementoSocieta"]; return o == null ? "Società" : (string)o; }
            set { this.ViewState["ComboRicercaTestoElementoSocieta"] = value; }
        }


        public bool VerificaPresenzaUtenteLoggato
        {
            get { object o = this.ViewState["VerificaPresenzaUtenteLoggato"]; return o == null ? true : (bool)o; }
            set { this.ViewState["VerificaPresenzaUtenteLoggato"] = value; }
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
            get { object o = this.ViewState["GestioneSoggettoUnico"]; return o == null ? false : (bool)o; }
            set { this.ViewState["GestioneSoggettoUnico"] = value; }
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

        public bool EmailoPecObbligatori
        {
            get { object o = this.ViewState["EmailoPecObbligatori"]; return o == null ? false : (bool)o; }
            set { this.ViewState["EmailoPecObbligatori"] = value; }
        }

        public bool TelefonooCellulareObbligatori
        {
            get { object o = this.ViewState["TelefonooCellulareObbligatori"]; return o == null ? false : (bool)o; }
            set { this.ViewState["TelefonooCellulareObbligatori"] = value; }
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


        protected override void OnPreRender(EventArgs e)
        {
            this.cmdNuovo.Visible = true;
            this.DettagliPf.GestioneSoggettoUnico = true;

            if (this.GestioneSoggettoUnico && ReadFacade.Domanda.Anagrafiche.Anagrafiche.Any())
            {
                this.cmdNuovo.Visible = false;
                this.DettagliPf.GestioneSoggettoUnico = false;
            }

            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // il service si occupa del salvataggio dei dati
            this.Master.IgnoraSalvataggioDati = true;

            _condizioneUscita.FlagVerificaPecObbligatoria = VerificaPecObbligatoria;
            _condizioneUscita.MessaggioUtenteNonPresente = MessaggioUtenteNonPresente;

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

            if (!IsPostBack)
            {
                if (VerificaRichiedenteAutomatico())
                {
                    ImpostaDatiUtenteCorrente();
                }
                else
                {
                    DataBind();
                }

                cmbTipoPersona.Items[1].Text = this.ComboRicercaTestoElementoSocieta;

            }
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
                this._condizioneUscita.VerificaPresenzaUtenteLoggato = this.VerificaPresenzaUtenteLoggato;

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
                AnagraficheService.SalvaAnagrafica(IdDomanda, row);

                OnEndEdit(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                m_logger.ErrorFormat("Errore in OnAcceptEdit: {0}", ex.ToString());

                Errori.Add(ex.Message);
            }
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
        }

        public void cmdNuovo_Click(object sender, EventArgs e)
        {
            multiview.ActiveViewIndex = PageViews.NuovaAnagrafica;
            this.Master.MostraPaginatoreSteps = false;
            cmbTipoPersona.SelectedValue = "F";
            txtCodiceFiscale.Text = "";

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
                DettagliPf.DataSource = row.ToAnagrafeRow();
                DettagliPf.DataBind();

            }
            else
            {
                multiview.ActiveViewIndex = PageViews.EditPersonaGiuridica;

                DettagliPg.PermettiModificaDatiAnagrafici = permettiModificheAdAnagrafiche;
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

            this.Master.MostraBottoneAvanti = true;
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
        }

        public void dgRichiedenti_DeleteCommand(object source, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(dgRichiedenti.DataKeys[e.RowIndex].Value);

            AnagraficheService.RimuoviAnagrafica(IdDomanda, id);

            DataBind();
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


        public void multiview_ActiveViewChanged(object sender, EventArgs e)
        {
        }
    }
}
