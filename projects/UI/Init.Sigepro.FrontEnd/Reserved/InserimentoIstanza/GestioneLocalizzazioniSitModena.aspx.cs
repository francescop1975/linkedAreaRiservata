using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.GestioneLocalizzazioni;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneLocalizzazioni;
using Init.Sigepro.FrontEnd.AppLogic.IntegrazioneSit;
using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.HelperGestioneLocalizzazioni;
using Ninject;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    public partial class GestioneLocalizzazioniSitModena : IstanzeStepPage
    {
        private static class Constants
        {
            public const int IdColonnaCivico = 0;
            public const int IdColonnaRiferimentiCatastali = 5;
        }

        [Inject]
        protected LocalizzazioniService _localizzazioniService { get; set; }

        [Inject]
        protected ISitService _sitService { get; set; }

        #region dati letti dai parametri del workflow
        public string CivicoEtichetta
        {
            set { _formLocalizzazioni.Civico.Etichetta = value; }
        }

        public bool CivicoObbligatorio
        {
            set { _formLocalizzazioni.Civico.Obbligatorio = value; }
        }
        //---------------------------------------

        public string EsponenteEtichetta
        {
            set { _formLocalizzazioni.Esponente.Etichetta = value; }
        }

        public bool EsponenteObbligatorio
        {
            set { _formLocalizzazioni.Esponente.Obbligatorio = value; }
        }

        //-------------------------------------------

        public string ColoreEtichetta
        {
            set { _formLocalizzazioni.Colore.Etichetta = value; }
        }

        public bool ColoreObbligatorio
        {
            set { _formLocalizzazioni.Colore.Obbligatorio = value; }
        }

        //-------------------------------------------------


        public string ScalaEtichetta
        {
            set { _formLocalizzazioni.Scala.Etichetta = value; }
        }

        public bool ScalaObbligatorio
        {
            set { _formLocalizzazioni.Scala.Obbligatorio = value; }
        }

        //--------------------------------------------------


        public string PianoEtichetta
        {
            set { _formLocalizzazioni.Piano.Etichetta = value; }
        }

        public bool PianoObbligatorio
        {
            set { _formLocalizzazioni.Piano.Obbligatorio = value; }
        }

        //--------------------------------------------------

        public string InternoEtichetta
        {
            set { _formLocalizzazioni.Interno.Etichetta = value; }
        }

        public bool InternoObbligatorio
        {
            set { _formLocalizzazioni.Interno.Obbligatorio = value; }
        }

        //--------------------------------------------------

        public string EsponenteInternoEtichetta
        {
            set { _formLocalizzazioni.EsponenteInterno.Etichetta = value; }
        }

        public bool EsponenteInternoObbligatorio
        {
            set { _formLocalizzazioni.EsponenteInterno.Obbligatorio = value; }
        }

        //--------------------------------------------------

        public string FabbricatoEtichetta
        {
            set { _formLocalizzazioni.Fabbricato.Etichetta = value; }
        }

        public bool FabbricatoObbligatorio
        {
            set { _formLocalizzazioni.Fabbricato.Obbligatorio = value; }
        }

        //--------------------------------------------------

        public string KmEtichetta
        {
            set { _formLocalizzazioni.Km.Etichetta = value; }
        }

        public bool KmObbligatorio
        {
            set { _formLocalizzazioni.Km.Obbligatorio = value; }
        }

        //--------------------------------------------------

        public string NoteEtichetta
        {
            set { _formLocalizzazioni.Note.Etichetta = value; }
        }

        public bool NoteObbligatorio
        {
            set { _formLocalizzazioni.Note.Obbligatorio = value; }
        }


        //--------------------------------------------------

        public string TipoLocalizzazione
        {
            get { object o = ViewState["TipoLocalizzazione"]; return o == null ? String.Empty : (string)o; }
            set { ViewState["TipoLocalizzazione"] = value; }
        }


        //--------------------------------------------------



        public bool CoordinateObbligatorie
        {
            set
            {
                _formLocalizzazioni.Longitudine.Etichetta = "Longitudine";
                _formLocalizzazioni.Latitudine.Etichetta = "Latitudine";
                _formLocalizzazioni.Longitudine.Obbligatorio = value;
                _formLocalizzazioni.Latitudine.Obbligatorio = value;
            }
        }

        public string CoordinateEtichettaLongitudine
        {
            set
            {
                _formLocalizzazioni.Longitudine.Etichetta = value;
            }
        }
        public string CoordinateEtichettaLatitudine
        {
            set
            {
                _formLocalizzazioni.Latitudine.Etichetta = value;
            }
        }
        public string CoordinateEspressioneRegolare
        {
            set
            {
                _formLocalizzazioni.Longitudine.EspressioneRegolare = value;
                _formLocalizzazioni.Latitudine.EspressioneRegolare = value;
            }
        }

        public string CoordinateLongitudineValoreMin
        {
            set { _formLocalizzazioni.Longitudine.ValoreMin = value; }
        }

        public string CoordinateLatitudineValoreMin
        {
            set { _formLocalizzazioni.Latitudine.ValoreMin = value; }
        }

        public string CoordinateLongitudineValoreMax
        {
            set { _formLocalizzazioni.Longitudine.ValoreMax = value; }
        }

        public string CoordinateLatitudineValoreMax
        {
            set { _formLocalizzazioni.Latitudine.ValoreMax = value; }
        }


        //--------------------------------------------------

        public bool DatiCatastaliVisibili
        {
            get
            {
                return ViewstateGet("DatiCatastaliVisibili", true);
            }
            set
            {
                ViewStateSet("DatiCatastaliVisibili", value);
                _formLocalizzazioni.TipoCatasto.Visibile = value;
                _formLocalizzazioni.Foglio.Visibile = value;
                _formLocalizzazioni.Particella.Visibile = value;
                _formLocalizzazioni.Sub.Visibile = value;
            }
        }

        public bool DatiCatastaliObbligatori
        {
            get
            {
                return ViewstateGet("DatiCatastaliObbligatori", true);
            }

            set
            {
                ViewStateSet("DatiCatastaliObbligatori", value);
                _formLocalizzazioni.TipoCatasto.Etichetta = "TipoCatasto";
                _formLocalizzazioni.Foglio.Etichetta = "Foglio";
                _formLocalizzazioni.Particella.Etichetta = "Particella";
                _formLocalizzazioni.Sub.Etichetta = "Subalterno";

                _formLocalizzazioni.TipoCatasto.Obbligatorio = value;
                _formLocalizzazioni.Foglio.Obbligatorio = value;
                _formLocalizzazioni.Particella.Obbligatorio = value;
                _formLocalizzazioni.Sub.Obbligatorio = value;
            }
        }

        //--------------------------------------------------
        public string AttivaConEndo
        {
            get { object o = ViewState["AttivaConEndo"]; return o == null ? String.Empty : (string)o; }
            set { ViewState["AttivaConEndo"] = value; }
        }



        #endregion

        FormLocalizzazioni _formLocalizzazioni;


        public string CodiceComune
        {
            get { object o = ViewState["CodiceComune"]; return o == null ? String.Empty : (string)o; }
            set { ViewState["CodiceComune"] = value; }
        }

        public bool MostraLocalizzazioneDaIndirizzo
        {
            get { object o = ViewState["MostraLocalizzazioneDaIndirizzo"]; return o == null ? false : (bool)o; }
            set { ViewState["MostraLocalizzazioneDaIndirizzo"] = value; }
        }

        public bool MostraLocalizzazioneDaMappali
        {
            get { object o = ViewState["MostraLocalizzazioneDaMappali"]; return o == null ? false : (bool)o; }
            set { ViewState["MostraLocalizzazioneDaMappali"] = value; }
        }

        public string UrlLocalizzazioneDaIndirizzo
        {
            get { object o = ViewState["UrlLocalizzazioneDaIndirizzo"]; return o == null ? String.Empty : (string)o; }
            set { ViewState["UrlLocalizzazioneDaIndirizzo"] = value; }
        }

        public string UrlLocalizzazioneDaMappali
        {
            get { object o = ViewState["UrlLocalizzazioneDaMappali"]; return o == null ? String.Empty : (string)o; }
            set { ViewState["UrlLocalizzazioneDaMappali"] = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            _formLocalizzazioni = new FormLocalizzazioni(ViewState)
            {
                Civico = new CampoLabeled { ControlloEdit = txtCivico },
                Esponente = new CampoLabeled { ControlloEdit = txtEsponente },
                Colore = new CampoLabeled { ControlloEdit = ddlColore },
                Scala = new CampoLabeled { ControlloEdit = txtScala },
                Piano = new CampoLabeled { ControlloEdit = txtPiano },
                Interno = new CampoLabeled { ControlloEdit = txtInterno },
                EsponenteInterno = new CampoLabeled { ControlloEdit = txtEsponenteInterno },
                Fabbricato = new CampoLabeled { ControlloEdit = txtFabbricato },
                Km = new CampoLabeled { ControlloEdit = txtKm },
                Latitudine = new CampoLabeled { ControlloEdit = txtLatitudine },
                Longitudine = new CampoLabeled { ControlloEdit = txtLongitudine },
                TipoCatasto = new CampoDropDownLabeled(ddlTipoCatasto),
                Foglio = new CampoLabeled { ControlloEdit = txtFoglio },
                Particella = new CampoLabeled { ControlloEdit = txtParticella },
                Sub = new CampoLabeled { ControlloEdit = txtSub },
                Note = new CampoNull(),
                Sezione = new CampoHidden { ControlloEdit = hiddenCodCivico },
                CodiceCivico = new CampoHidden { ControlloEdit = hiddenCodCivico },
                CodiceViario = new CampoHidden { ControlloEdit = hiddenCodViario },

                AccessoTipo = new CampoHidden { ControlloEdit = txtAccessoTipo },
                AccessoNumero = new CampoHidden { ControlloEdit = txtAccessoNumero },
                AccessoDescrizione = new CampoHidden { ControlloEdit = txtAccessoDescrizione }
            };

            base.OnInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            // il service si occupa del salvataggio dei dati
            Master.IgnoraSalvataggioDati = true;

            if (!IsPostBack)
            {
                var listaColori = ReadFacade.Stradario.GetListaColori(IdComune);

                listaColori.Insert(0, new StradarioColore { CODICECOLORE = String.Empty, COLORE = String.Empty, IDCOMUNE = IdComune });

                ddlColore.DataSource = listaColori;
                ddlColore.DataBind();

                ddlTipoCatasto.Items.Add(new ListItem(String.Empty, String.Empty));
                ddlTipoCatasto.Items.Add(new ListItem("Fabbricati", "F"));
                ddlTipoCatasto.Items.Add(new ListItem("Terreni", "T"));

                var features = _sitService.GetFeatures();
                var campiSupportati = features.CampiSupportati;

                _formLocalizzazioni.Civico.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Civico);
                _formLocalizzazioni.Esponente.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Esponente);
                _formLocalizzazioni.Colore.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Colore);
                _formLocalizzazioni.Scala.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Scala);
                _formLocalizzazioni.Piano.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Piano);
                _formLocalizzazioni.Interno.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Interno);
                _formLocalizzazioni.EsponenteInterno.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.EsponenteInterno);
                _formLocalizzazioni.Fabbricato.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Fabbricato);
                _formLocalizzazioni.Km.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Km);
                _formLocalizzazioni.Latitudine.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Coordinate);
                _formLocalizzazioni.Longitudine.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Coordinate);


                _formLocalizzazioni.Foglio.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Foglio);
                _formLocalizzazioni.Particella.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Particella);
                _formLocalizzazioni.Sub.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.Sub);

                _formLocalizzazioni.TipoCatasto.Visibile = campiSupportati.Supporta(SitCampiSupportati.Campi.TipoCatasto);

                MostraLocalizzazioneDaIndirizzo = features.VisualizzazioniSupportate.SupportaVisualizzazioneMappaDaIndirizzo();
                MostraLocalizzazioneDaMappali = features.VisualizzazioniSupportate.SupportaVisualizzazioneMappaDaMappale();
                UrlLocalizzazioneDaIndirizzo = features.VisualizzazioniSupportate.UrlVisualizzazioneMappaDaIndirizzo();
                UrlLocalizzazioneDaMappali = features.VisualizzazioniSupportate.UrlVisualizzazioneMappaDaMappale();

                DataBind();
            }

            CodiceComune = ReadFacade.Domanda.AltriDati.CodiceComune;
        }


        #region Ciclo della vita dello step

        public override bool CanEnterStep()
        {
            if (String.IsNullOrEmpty(AttivaConEndo))
                return true;

            var codiciEndoSelezionati = ReadFacade.Domanda.Endoprocedimenti.Endoprocedimenti.Select(x => x.Codice);
            var codiciEndoAttivazioneStep = AttivaConEndo.Split(',').Select(x => Convert.ToInt32(x.Trim()));

            foreach (var endoSelezionato in codiciEndoSelezionati)
            {
                if (codiciEndoAttivazioneStep.Contains(endoSelezionato))
                    return true;
            }

            return false;
        }

        public override bool CanExitStep()
        {
            if (ReadFacade.Domanda.Localizzazioni.Indirizzi.Count() == 0)
            {
                Errori.Add("Inserire almeno una localizzazione");
                return false;
            }

            return true;
        }

        #endregion

        public override void DataBind()
        {
            multiView.ActiveViewIndex = 0;

            dgStradario.DataSource = ReadFacade.Domanda.Localizzazioni.Indirizzi.Where(x => x.TipoLocalizzazione == TipoLocalizzazione);
            dgStradario.DataBind();

            Master.MostraPaginatoreSteps = true;
            Master.MostraBottoneAvanti = ReadFacade.Domanda.Localizzazioni.Indirizzi.Count() > 0;

        }

        /// <summary>
        /// Svuota tutti i controlli del panel di inserimento
        /// </summary>
        private void ClearDettaglio()
        {
            acIndirizzo.Value = String.Empty;
            acIndirizzo.Text = String.Empty;

            _formLocalizzazioni.SvuotaCampiEdit();
            /*
			txtCivico.Value = String.Empty;
			txtEsponente.Value = String.Empty;
			ddlColore.Item.SelectedIndex = 0;
			txtScala.Value = String.Empty;
			txtInterno.Value = String.Empty;
			txtEsponenteInterno.Value = String.Empty;
			txtPiano.Value = String.Empty;
			txtFabbricato.Value = String.Empty;
			txtKm.Value = String.Empty;
			*/
            dgIndirizzi.Visible = false;
            hiddenIdLocalizzazione.Value = "";
        }

        protected void Edit()
        {
            multiView.ActiveViewIndex = 1;
            Master.MostraPaginatoreSteps = false;
        }

        protected void Edit(IndirizzoStradario indirizzoStradario)
        {
            Edit();

            hiddenIdLocalizzazione.Value = indirizzoStradario.Id.ToString();
            acIndirizzo.Text = indirizzoStradario.Indirizzo;
            acIndirizzo.Value = indirizzoStradario.CodiceStradario.ToString();

            hiddenCodViario.Value = indirizzoStradario.CodiceViario;

            txtCivico.Value = indirizzoStradario.Civico;
            hiddenCodCivico.Value = indirizzoStradario.CodiceCivico;
            txtScala.Value = indirizzoStradario.Scala;
            txtEsponente.Value = indirizzoStradario.Esponente;
            txtEsponenteInterno.Value = indirizzoStradario.EsponenteInterno;
            txtInterno.Value = indirizzoStradario.Interno;
            txtFabbricato.Value = indirizzoStradario.Fabbricato;

            var rifCatastali = indirizzoStradario.RiferimentiCatastali;
            if (rifCatastali.Count() > 0)
            {
                txtFoglio.Value = rifCatastali.FirstOrDefault().Foglio;
                txtParticella.Value = rifCatastali.FirstOrDefault().Particella;
                txtSub.Value = rifCatastali.FirstOrDefault().Sub;
            }
        }

        protected void EditNew()
        {
            ClearDettaglio();
            Edit();
        }

        /// <summary>
        /// Delegate della selezione di un elemento della dataGrid dei risultati della ricerca nello stradario
        /// </summary>
        public void dgIndirizzi_SelectedIndexChanged(object sender, EventArgs e)
        {
            var codiceStradario = Convert.ToInt32(dgIndirizzi.DataKeys[dgIndirizzi.SelectedIndex].Value);

            InserisciVoceStradario(ReadFacade.Stradario.GetByCodiceStradario(IdComune, codiceStradario));
        }

        /// <summary>
        /// Aggiunge la voce di stradario trovata alla lista degli indirizzi dell'istanza
        /// </summary>
        /// <param name="stradarioTrovato">Voce dello stradario trovata o null se si deve effettuare una ricerca per match parziale</param>
        private void InserisciVoceStradario(Stradario stradarioTrovato)
        {
            if (stradarioTrovato != null)
            {
                var nomeVia = stradarioTrovato.PREFISSO + " " + stradarioTrovato.DESCRIZIONE;

                if (!String.IsNullOrEmpty(stradarioTrovato.LOCFRAZ))
                    nomeVia += " (" + stradarioTrovato.LOCFRAZ + ")";

                var localizzazione = _formLocalizzazioni.GetLocalizzazione(Convert.ToInt32(stradarioTrovato.CODICESTRADARIO), nomeVia, TipoLocalizzazione);
                var rifCatastali = _formLocalizzazioni.GetRiferimentiCatastali();

                if (!String.IsNullOrEmpty(hiddenIdLocalizzazione.Value))
                {
                    _localizzazioniService.EliminaLocalizzazione(IdDomanda, Convert.ToInt32(hiddenIdLocalizzazione.Value));
                }

                _localizzazioniService.AggiungiLocalizzazione(IdDomanda, localizzazione, rifCatastali);

                DataBind();

                return;
            }

            var comuneLocalizzazione = String.Empty;
            var listaIndirizzi = ReadFacade.Stradario.GetByMatchParziale(IdComune, CodiceComune, comuneLocalizzazione, acIndirizzo.Text);

            if (listaIndirizzi.Count > 0)
            {
                Errori.Add("Indirizzo non trovato. Sono però stati trovati i seguenti record simili");

                dgIndirizzi.DataSource = listaIndirizzi;
                dgIndirizzi.DataBind();

                dgIndirizzi.Visible = true;
            }
            else
            {
                Errori.Add("Indirizzo non trovato. Verificare i dati immessi");
                acIndirizzo.Value = String.Empty;
                acIndirizzo.Text = String.Empty;
            }

        }

        /// <summary>
        /// Handler dell'evento click sul bottone di eliminazione riga della datagrid di riepilogo
        /// </summary>
        public void dgStradario_DeleteCommand(object source, GridViewDeleteEventArgs e)
        {
            int key = Convert.ToInt32(dgStradario.DataKeys[e.RowIndex].Value);

            _localizzazioniService.EliminaLocalizzazione(IdDomanda, key);

            DataBind();
        }


        public void cmdAddNew_Click(object sender, EventArgs e)
        {
            EditNew();
        }
        public void cmdConfirm_Click(object sender, EventArgs e)
        {
            dgIndirizzi.Visible = false;

            var erroriCompilazione = _formLocalizzazioni.GetErroriValidazione();
            var erroriEspressioniRegolari = _formLocalizzazioni.GetErroriEspressioniRegolari();
            var erroriValidazioneRange = _formLocalizzazioni.GetErroriValidazioneRange();

            var erroriValidazione = erroriCompilazione.Union(erroriEspressioniRegolari).Union(erroriValidazioneRange);

            if (erroriValidazione.Count() > 0)
            {
                Errori.AddRange(erroriValidazione);

                return;
            }

            Stradario stradarioTrovato = CodiceStradarioTrovato() ? TrovaStradarioDaCodiceStradario() : TrovaStradarioDaIndirizzo();

            InserisciVoceStradario(stradarioTrovato);
        }

        private Stradario TrovaStradarioDaCodiceStradario()
        {
            return ReadFacade.Stradario.GetByCodiceStradario(IdComune, Convert.ToInt32(acIndirizzo.Value));
        }

        private Stradario TrovaStradarioDaIndirizzo()
        {
            return ReadFacade.Stradario.GetByIndirizzo(IdComune, CodiceComune, acIndirizzo.Text);
        }

        private bool CodiceStradarioTrovato()
        {
            return !String.IsNullOrEmpty(acIndirizzo.Value);
        }
        public void cmdCancel_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        protected void dgStradario_SelectedIndexChanged(object sender, EventArgs e)
        {
            int key = Convert.ToInt32(dgStradario.DataKeys[dgStradario.SelectedIndex].Value);

            var indirizzoStradario = ReadFacade.Domanda.Localizzazioni.Indirizzi.Where(x => x.Id == key).FirstOrDefault();
            Edit(indirizzoStradario);
        }

        protected void dgIndirizzi_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int key = Convert.ToInt32(dgStradario.DataKeys[e.RowIndex].Value);

                this._localizzazioniService.EliminaLocalizzazione(IdDomanda, key);

                DataBind();
            }
            catch (Exception ex)
            {
                Errori.Add(ex.Message);
            }
        }
    }
}