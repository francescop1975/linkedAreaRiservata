using Init.Sigepro.FrontEnd.GestioneMovimenti.ViewModels;
using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.WebControls.MaschereSolaLettura;
using Ninject;
using System;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved.GestioneMovimenti
{
    public partial class CompilaSchedeDinamiche : MovimentiBasePage
    {
        private static class Constants
        {
            public const int IdPannelloLista = 0;
            public const int IdPannelloDettaglio = 1;
        }

        [Inject]
        protected CompilazioneSchedeDinamicheViewModel _viewModel { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = this._viewModel.GetTitoloMovimentoDaEffettuare();

            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            rptSchedeDaCompilare.DataSource = this._viewModel.GetListaSchedeDinamiche();
            rptSchedeDaCompilare.DataBind();
        }

        protected void lnkSchedaDinamica_Click(object sender, EventArgs e)
        {
            var lnkSchedaDinamica = (LinkButton)sender;

            var idScheda = Convert.ToInt32(lnkSchedaDinamica.CommandArgument);
            var scheda = this._viewModel.CaricaSchedadinamica(idScheda);

            lblTitoloModello.Text = scheda.NomeModello;

            multiView.ActiveViewIndex = Constants.IdPannelloDettaglio;

            scheda.EseguiScriptCaricamento();

            renderer.ImpostaMascheraSolaLettura(new MascheraSolaLetturaVuota());
            //renderer.RicaricaModelloDinamico += (s,ea) => this._viewModel.RicaricaModelloDinamico(s,ea);
            renderer.DataSource = scheda;
            renderer.DataBind();

        }

        protected void cmdProcedi_Click(object sender, EventArgs e)
        {
            if (!this._viewModel.CanExitStep())
            {
                Errori.Add("Per poter proseguire è necessario compilare tutte le schede");
                return;
            }

            GoToNextStep();
        }

        protected void cmdTornaIndietro_Click(object sender, EventArgs e)
        {
            GoToPreviousStep();
        }


        protected void cmdSalva_Click(object sender, EventArgs e)
        {
            try
            {
                this._viewModel.SalvaSchedaDinamica(IdMovimento, renderer.DataSource);

                cmdChiudi_Click(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MostraErroreSalvataggio(ex);
            }
        }

        private void MostraErroreSalvataggio(Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "notifica", "alert('Si sono verificati errori durante il salvataggio');", true);
        }

        protected void cmdChiudi_Click(object sender, EventArgs e)
        {
            multiView.ActiveViewIndex = Constants.IdPannelloLista;

            DataBind();
        }


        protected override FrontEnd.GestioneMovimenti.GestioneWorkflowMovimento.IStepViewModel GetViewmodel()
        {
            return this._viewModel;
        }
    }
}