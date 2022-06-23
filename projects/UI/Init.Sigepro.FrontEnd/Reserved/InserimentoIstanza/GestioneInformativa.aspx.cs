using Init.Sigepro.FrontEnd.AppLogic.GestioneComuni;
using Init.Sigepro.FrontEnd.AppLogic.GestioneDatiExtra;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Exceptions;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    public partial class GestioneInformativa : IstanzeStepPage
    {
        protected static class Constants
        {
            public const string FlagInformativa = "FlagInformativa";
            public const string ErroreInformativaNonAccettata = "Per proseguire è necessario leggere ed accettare le condizioni riportate";
        }

        [Inject]
        public IComuniService _comuniService { get; set; }

        [Inject]
        public DatiDomandaService _datiDomandaService { get; set; }

        [Inject]
        public DatiExtraService _datiExtraService { get; set; }

        public string CodiceComuneDiRiferimento
        {
            get { return ViewstateGet("CodiceComuneDiRiferimento", ""); }
            set { ViewStateSet("CodiceComuneDiRiferimento", value); }
        }

        public string MessaggioErrore
        {
            get { return ViewstateGet("MessaggioErrore", ""); }
            set { ViewStateSet("MessaggioErrore", value); }
        }

        public string TestoAccettazioneInformativa
        {
            get
            {
                return chkAccetto.Text;
            }
            set
            {
                chkAccetto.Text = "&nbsp;<b>" + value + "</b>";
            }
        }
        public string TestoInformativa
        {
            get
            {
                return ltrTestoInformativa.Text;
            }
            set
            {
                ltrTestoInformativa.Text = String.Format(value, GetNomeComune());
            }
        }
        public bool MostraCheckAccettazione
        {
            get { object o = this.ViewState["MostraCheckAccettazione"]; return o == null ? true : (bool)o; }
            set { this.ViewState["MostraCheckAccettazione"] = value; }
        }




        protected void Page_Load(object sender, EventArgs e)
        {
            // il service si occupa del salvataggio dei dati
            this.Master.IgnoraSalvataggioDati = true;

            if (!IsPostBack)
                DataBind();
        }

        public override void DataBind()
        {
            var f = _datiExtraService.Get<string>(IdDomanda, Constants.FlagInformativa);
            chkAccetto.Checked = !String.IsNullOrEmpty(f) ? Convert.ToBoolean(f) : false;

            if (!this.MostraCheckAccettazione)
            {
                chkAccetto.Checked = true;
                chkAccetto.Visible = false;
            }
        }

        public override bool CanEnterStep()
        {
            return (ReadFacade.Domanda.AltriDati.CodiceComune == CodiceComuneDiRiferimento);
        }

        public override bool CanExitStep()
        {
            try
            {
                if (!this.chkAccetto.Checked)
                    throw new StepException(Constants.ErroreInformativaNonAccettata);

                return this.chkAccetto.Checked;
            }
            catch (StepException ex)
            {
                Errori.AddRange(ex.ErrorMessages);

                return false;
            }
        }

        public override void OnBeforeExitStep()
        {
            _datiExtraService.Set<string>(IdDomanda, Constants.FlagInformativa, chkAccetto.Checked.ToString());
        }

        private string GetNomeComune()
        {
            var comune = _comuniService.GetDatiComune(ReadFacade.Domanda.AltriDati.CodiceComune);

            if (comune == null)
            {
                return String.Empty;
            }

            return comune.Comune;
        }
    }
}
