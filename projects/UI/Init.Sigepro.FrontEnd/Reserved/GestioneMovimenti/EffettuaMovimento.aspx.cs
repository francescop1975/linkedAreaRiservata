using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.GestioneMovimenti.GestioneMovimento.GestioneMovimentoDaEffettuare;
using Init.Sigepro.FrontEnd.GestioneMovimenti.GestioneMovimento.GestioneMovimentoDiOrigine;
using Init.Sigepro.FrontEnd.GestioneMovimenti.GestioneWorkflowMovimento;
using Init.Sigepro.FrontEnd.GestioneMovimenti.ViewModels;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved.GestioneMovimenti
{
    public partial class EffettuaMovimento : MovimentiBasePage
    {
        private static class Constants
        {
            public const string TestoIntestazione = "In riferimento alla pratica numero {numeroPratica} del {dataPratica}" +
                                                    "{datiProtocollo} relativamente all'attività istruttoria in seguito riportata";
            public const string SegnapostoNumeroPratica = "{numeroPratica}";
            public const string SegnapostoDataPratica = "{dataPratica}";
            public const string SegnapostoDatiProtocollo = "{datiProtocollo}";
            public const string ReturnToSessionKey = "EffettuaMovimento.ReturnToSessionKey";
        }

        [Inject]
        protected RiepilogoMovimentoDiOrigineViewModel _viewModel { get; set; }

        [Inject]
        protected IConfigurazione<ParametriIntegrazioniDocumentali> _parametriIntegrazione { get; set; }

        public bool PubblicaNote
        {
            get { return !_parametriIntegrazione.Parametri.NascondiNoteMovimento; }
        }

        private QsReturnTo ReturnTo => new QsReturnTo(Request.QueryString);

        protected MovimentoDaEffettuare MovimentoDaEffettuare { get; set; }
        protected MovimentoDiOrigine DataSource { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove(Constants.ReturnToSessionKey);

                if (this.ReturnTo.HasValue)
                {
                    Session[Constants.ReturnToSessionKey] = this.ReturnTo.Value;
                }

                DataBind();
            }
        }

        public override void DataBind()
        {
            var movimentoDiOrigine = this._viewModel.GetMovimentoDiOrigine(IdMovimento);

            this.DataSource = movimentoDiOrigine;

            ltrDescrizioneStep.Text = GeneraDescrizioneStep(movimentoDiOrigine);

            base.DataBind();
        }

        public string GetAttivitaRichiesta()
        {
            return this._viewModel.GetAttivitaRichiesta(IdMovimento);
        }

        private static string GeneraDescrizioneStep(MovimentoDiOrigine movimentoDiOrigine)
        {
            var numeroPratica = movimentoDiOrigine.DatiIstanza.NumeroIstanza;
            var dataPratica = movimentoDiOrigine.DatiIstanza.DataIstanza.ToString("dd/MM/yyyy");
            var datiprotocollo = String.Empty;

            if (movimentoDiOrigine.DatiIstanza.Protocollo.DatiPresenti)
                datiprotocollo = String.Format(" (prot n.{0} del {1}) ",
                                                movimentoDiOrigine.DatiIstanza.Protocollo.Numero,
                                                movimentoDiOrigine.DatiIstanza.Protocollo.Data.Value.ToString("dd/MM/yyyy"));

            var str = Constants.TestoIntestazione.Replace(Constants.SegnapostoNumeroPratica, numeroPratica);
            str = str.Replace(Constants.SegnapostoDataPratica, dataPratica);
            str = str.Replace(Constants.SegnapostoDatiProtocollo, datiprotocollo);

            return str;
        }

        protected void cmdProcedi_Click(object sender, EventArgs e)
        {
            this._viewModel.CreaMovimento(IdMovimento);

            GoToNextStep();
        }

        protected void cmdChiudi_Click(object sender, EventArgs e)
        {
            var returnTo = Session[Constants.ReturnToSessionKey]?.ToString();

            if (!String.IsNullOrEmpty(returnTo))
            {
                Session.Remove(Constants.ReturnToSessionKey);

                Response.Redirect(returnTo);

                return;
            }



            Response.Redirect(UrlBuilder.Url("~/reserved/scadenzario.aspx", qs =>
            {
                qs.Add(new QsAliasComune(this.IdComune));
                qs.Add(new QsSoftware(this.Software));
            }));
        }


        protected override IStepViewModel GetViewmodel()
        {
            return this._viewModel;
        }
    }
}