﻿namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
    using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti;
    using Init.Sigepro.FrontEnd.Infrastructure.StepsDomanda.Attributi;
    using Init.SIGePro.Manager.DTO.Endoprocedimenti;
    using Ninject;
    using System;
    using System.Linq;

    public partial class GestioneEndoV2 : IstanzeStepPage
    {
        private Lazy<EndoprocedimentiAreaRiservataDto> _dataSource = null;

        [Inject]
        public IEndoprocedimentiService _endoService { get; set; }
        [Inject]
        public IConfigurazione<ParametriVisura> _configVisura { get; set; }

        #region Testi e proprietà letti dall'xml

        [StepProperty("Titolo della sezione \"Endo principale\"")]
        public string TitoloEndoPrincipale
        {
            get
            {
                return ltrTitoloEndoPrincipale.Text;
            }

            set
            {
                ltrTitoloEndoPrincipale.Text = value;
            }
        }

        [StepProperty("Titolo della sezione \"Endo attivati\"")]
        public string TitoloEndoAttivati
        {
            get
            {
                return ltrTitoloEndoAttivati.Text;
            }

            set
            {
                ltrTitoloEndoAttivati.Text = value;
            }
        }

        [StepProperty("Titolo della sezione \"Endo attivabili\"")]
        public string TitoloEndoAttivabili
        {
            get
            {
                return ltrTitoloEndoAttivabili.Text;
            }

            set
            {
                ltrTitoloEndoAttivabili.Text = value;
            }
        }

        [StepProperty("Se impostato a true mostra l'albero degli endo a partire dalle famiglie", ValoreDefault = "true")]
        public bool MostraFamiglieEndo
        {
            get { return ViewstateGet<bool>("MostraFamiglieEndo", true); }
            set { ViewStateSet("MostraFamiglieEndo", value); }
        }

        [StepProperty("Se impostato a true permette di modificare la lista degli" +
            "endoprocedimenti proposti deselezionando gli endo spuntati di default",
            ValoreDefault = "false")]
        public bool ModificaProcedimentiProposti
        {
            get { return ViewstateGet<bool>("ModificaProcedimentiProposti", false); }
            set { ViewStateSet("ModificaProcedimentiProposti", value); }
        }

        [StepProperty("Titolo della sezione \"Altri endoprocedimenti attivabili\"")]
        public string TestoSezioneAltriEndo
        {
            get { return ltrTitoloAltriEndo.Text; }
            set { ltrTitoloAltriEndo.Text = value; }
        }

        [StepProperty("Testo del bottone \"Altri endoprocedimenti\"")]
        public string TestoBottoneAltriEndo
        {
            get { return cmdAttivaAltriEndo.Text; }
            set { cmdAttivaAltriEndo.Text = value; }
        }

        [StepProperty("Testo del bottone \"Torna alla lista degli endoprocedimenti\"")]
        public string TestoBottoneTornaAListaEndoEndo
        {
            get { return cmdTornaAllaLista.Text; }
            set { cmdTornaAllaLista.Text = value; }
        }

        [StepProperty("Titolo della sezione \"Altri endoprocedimenti attivabili\"")]
        public string TestoSezioneEditAltriEndo
        {
            get { return ltrTitoloAltriEndoAttivabili.Text; }
            set { ltrTitoloAltriEndoAttivabili.Text = value; }
        }

        [StepProperty("Se impostato a true ignora eventuali incompatibilità tra gli endoprocedimenti", ValoreDefault = "true")]
        public bool IgnoraIncompatibilitaEndoprocedimenti
        {
            get { object o = ViewState["IgnoraIncompatibilitaEndoprocedimenti"]; return o == null ? true : (bool)o; }
            set { ViewState["IgnoraIncompatibilitaEndoprocedimenti"] = value; }
        }

        [StepProperty("Se impostato a true è obbligatorio selezionare almeno un endoprocedimento della lista per poter proseguire allo step succcessivo", ValoreDefault = "false")]
        public bool SelezionareAlmenoUnEndo
        {
            get { object o = ViewState["SelezionareAlmenoUnEndo"]; return o == null ? false : (bool)o; }
            set { ViewState["SelezionareAlmenoUnEndo"] = value; }
        }

        [StepProperty("Testo dell'errore da mostrare se nella lista degli endo non è stato selezionato nessun endo. Dipende dal parametro \"SelezionareAlmenoUnEndo\"",
            ValoreDefault = "Selezionare almeno un elemento della lista")]
        public string MessaggioErroreSelezionareAlmenoUnEndo
        {
            get { object o = ViewState["MessaggioErroreSelezionareAlmenoUnendo"]; return o == null ? "Selezionare almeno un elemento della lista" : (string)o; }
            set { ViewState["MessaggioErroreSelezionareAlmenoUnendo"] = value; }
        }

        [StepProperty("Se impostato a true è possibile selezionare un solo endo per volta. La selezione di un endo deselezionerà altri endo già selezionati")]
        public bool SelezioneEsclusivaEndoAttivabili
        {
            get { object o = this.ViewState["SelezioneEsclusivaEndoAttivabili"]; return o == null ? false : (bool)o; }
            set { this.ViewState["SelezioneEsclusivaEndoAttivabili"] = value; }
        }


        #endregion

        public GestioneEndoV2()
        {
            _dataSource = new Lazy<EndoprocedimentiAreaRiservataDto>(() =>
            {
                var readInterface = ReadFacade.Domanda;
                var codIntervento = readInterface.AltriDati.Intervento.Codice;
                var codiceComune = readInterface.AltriDati.CodiceComune;

                return _endoService.GetListaEndoDaIdIntervento(codiceComune, codIntervento);
            });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Il salvataggio dei dati viene effettuato dal service
            Master.IgnoraSalvataggioDati = true;

            grigliaEndoPrincipale.MostraFamiglia = grigliaEndoPrincipale.MostraTipoEndo = MostraFamiglieEndo;
            grigliaEndoAttivati.MostraFamiglia = grigliaEndoAttivati.MostraTipoEndo = MostraFamiglieEndo;
            grigliaEndoAttivabili.MostraFamiglia = grigliaEndoAttivabili.MostraTipoEndo = MostraFamiglieEndo;
            grigliaAltriEndo.MostraFamiglia = grigliaAltriEndo.MostraTipoEndo = MostraFamiglieEndo;
            grigliaAltriEndoAttivabili.MostraFamiglia = grigliaAltriEndoAttivabili.MostraTipoEndo = MostraFamiglieEndo;

            if (!IsPostBack)
            {
                DataBind();
            }
        }

        #region ciclo di vita dello step

        public override void OnInitializeStep()
        {
            /*
			if (DataSource.IdSelezionatiDefault.Count == 0)
				EndoprocedimentiService.EliminaEndoprocedimenti(IdDomanda);
			*/
        }

        public override bool CanEnterStep()
        {
            return _dataSource.Value.Principali.Count > 0 ||
                _dataSource.Value.Altri.Count > 0 ||
                _dataSource.Value.Richiesti.Count > 0 ||
                _dataSource.Value.Ricorrenti.Count > 0;
        }

        public override void OnBeforeExitStep()
        {
            SincronizzaListaIdSelezionati();
        }

        public override bool CanExitStep()
        {
            if (this.SelezionareAlmenoUnEndo && !_endoService.AlmenoUnEndoPresente(IdDomanda))
            {
                Errori.Add(this.MessaggioErroreSelezionareAlmenoUnEndo);

                return false;
            }

            if (SelezioneEsclusivaEndoAttivabili && !grigliaEndoAttivabili.GetEndoSelezionati().Any())
            {
                Errori.Add(MessaggioErroreSelezionareAlmenoUnEndo);

                return false;
            }

            var endoIncompatibili = _endoService.GetEndoprocedimentiIncompatibili(IdDomanda);

            if (endoIncompatibili.Count() == 0 || IgnoraIncompatibilitaEndoprocedimenti)
            {
                return true;
            }

            Errori.AddRange(endoIncompatibili.Select(x => x.ToString()));

            return false;
        }

        #endregion

        protected void cmdAttivaAltriEndo_click(object sender, EventArgs e)
        {
            SincronizzaListaIdSelezionati();

            multiView.ActiveViewIndex = 1;
            Master.MostraPaginatoreSteps = false;
        }

        protected void cmdTornaAllaLista_click(object sender, EventArgs e)
        {
            // SincronizzaListaIdSelezionati();
            var endoPrecedenti = this._endoService.GetSubEndoSelezionati(this.IdDomanda)
                                                   .Endo
                                                   .Select(x => x.ToSubEndoprocedimentoSelezionato());

            // Rimuovo dalla lista gli endo che sarebbero facoltativi
            var codiciAltriEndo = this._dataSource.Value.Altri.SelectMany(x => x.ListaEndo).Select(x => x.Codice);
            endoPrecedenti = endoPrecedenti.Where(endo => !codiciAltriEndo.Contains(endo.Id));

            // Il totale degli endo selezionati è dato da tutti gli endo non facoltativi già attivati
            // più gli endo facoltativi attivati in questa fase
            var endoSelezionati = endoPrecedenti.Union(this.grigliaAltriEndoAttivabili.GetEndoSelezionati());

            this._endoService.SalvaEndoSelezionati(this.IdDomanda, endoSelezionati);

            multiView.ActiveViewIndex = 0;
            Master.MostraPaginatoreSteps = true;

            DataBind();
        }

        private void SincronizzaListaIdSelezionati()
        {
            // recupero gli id selezionati dall'utente
            var endoSelezionati = this.grigliaEndoPrincipale.GetEndoSelezionati()
                        .Union(this.grigliaEndoAttivati.GetEndoSelezionati())
                        .Union(this.grigliaEndoAttivabili.GetEndoSelezionati())
                        .Union(this.grigliaAltriEndo.GetEndoSelezionati());

            this._endoService.SalvaEndoSelezionati(this.IdDomanda, endoSelezionati);
        }

        public override void DataBind()
        {
            PopolaGriglieDegliEndoDellIntervento();

            PopolaGrigliaEndoFacoltativiSelezionati();

            PopolaGrigliaAltriEndoAttivabili();

            // Se non esistono endo principali o attivabili ma esistono altri endo attivabili 
            // apro direttamente la view di visualizzazione degli altri endo
            if (grigliaEndoPrincipale.DataSource.Length == 0 &&
                grigliaEndoAttivati.DataSource.Length == 0 &&
                grigliaEndoAttivabili.DataSource.Length == 0 &&
                grigliaAltriEndoAttivabili.DataSource.Length > 0)
            {
                cmdAttivaAltriEndo_click(this, EventArgs.Empty);

                Master.MostraBottoneAvanti = true;
                Master.MostraPaginatoreSteps = true;

                cmdTornaAllaLista.Visible = false;
            }
        }

        private void PopolaGrigliaAltriEndoAttivabili()
        {
            grigliaAltriEndoAttivabili.DataSource = _dataSource.Value.Altri.ToArray();
            grigliaAltriEndoAttivabili.DataBind();

            if (_dataSource.Value.Altri.Count == 0)
            {
                pnlAltriEndo.Visible = cmdAttivaAltriEndo.Visible = false;
            }
        }

        private void PopolaGrigliaEndoFacoltativiSelezionati()
        {
            var endoSelezionati = this._endoService
                                        .GetSubEndoSelezionati(this.IdDomanda)
                                        .Endo
                                        .Where(x => !x.IdPadre.HasValue)
                                        .Select(x => x.Id);

            var famiglieEndoFacoltativiSelezionate = _dataSource.Value.GetFamiglieEndoFacoltativiDaListaIdEndo(endoSelezionati).ToArray();

            grigliaAltriEndo.DataSource = famiglieEndoFacoltativiSelezionate;
            grigliaAltriEndo.DataBind();

            pnlAltriEndo.Visible = famiglieEndoFacoltativiSelezionate.Length > 0;
        }

        private void PopolaGriglieDegliEndoDellIntervento()
        {
            // é possibile modificare gli endo preselezionati tra gli endo attivati
            // o quelli principali?
            grigliaEndoPrincipale.ModificaProcedimentiProposti = ModificaProcedimentiProposti;
            grigliaEndoAttivati.ModificaProcedimentiProposti = ModificaProcedimentiProposti;

            // Se non esistono endoprocedimenti principali collegati all'intervento selezionato
            // nascondo l'intero pannello
            if (_dataSource.Value.Principali.Count == 0)
            {
                pnlEndoPrincipale.Visible = false;
            }

            grigliaEndoPrincipale.DataSource = _dataSource.Value.Principali.ToArray();
            grigliaEndoPrincipale.DataBind();

            // Se non esistono endoprocedimenti attivati collegati all'intervento selezionato
            // nascondo l'intero pannello
            if (_dataSource.Value.Richiesti.Count == 0)
            {
                pnlEndoAttivati.Visible = false;
            }

            grigliaEndoAttivati.DataSource = _dataSource.Value.Richiesti.ToArray();
            grigliaEndoAttivati.DataBind();

            // Se non esistono endoprocedimenti attivabili collegati all'intervento selezionato
            // nascondo l'intero pannello
            if (_dataSource.Value.Ricorrenti.Count == 0)
            {
                pnlEndoAttivabili.Visible = false;
            }

            grigliaEndoAttivabili.DataSource = _dataSource.Value.Ricorrenti.ToArray();
            grigliaEndoAttivabili.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            /*
            // Se non è stato ancora selezionato nessun endo aggiungo l'endo principale e gli endo attivati
            // Se non è possibile modificare la lista degli endo proposti 
            // aggiungo alla lista l'endo principale e gli endo attivati (se la lista non li contiene già)
            if (ListaIdSelezionati.Count == 0 || !ModificaProcedimentiProposti)
            {
                ListaIdSelezionati = ListaIdSelezionati.Union(_dataSource.Value.GetEndoSelezionatiDiDefault()).ToList();
            }
             */


            var strutturaSubEndo = this._endoService.GetSubEndoSelezionati(this.IdDomanda);

            var script = "window._endoAttivati = " + strutturaSubEndo.ToJsonString() + ";";

            // var endoAttivati = this.ReadFacade.Domanda.Endoprocedimenti.Endoprocedimenti.Select(x => x.Codice.ToString());
            // var script = "endoAttivati = [" + String.Join(",", endoAttivati) + "];";

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "endoAttivati", script, true);
        }
    }
}
