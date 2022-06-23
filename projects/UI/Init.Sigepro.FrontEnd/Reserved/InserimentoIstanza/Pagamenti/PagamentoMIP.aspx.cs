using Init.Sigepro.FrontEnd.AppLogic.GestioneOneri;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.MIP;
using Ninject;
using System;
using System.Configuration;
using System.Linq;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Pagamenti
{
    public partial class PagamentoMIP : IstanzeStepPage
    {
        public enum CallingReason
        {
            InizioPagamento,
            HOME,
            BACK,
            ERRORE,
            OK
        }

        private static class Constants
        {
            public const int ViewNuovoPagamento = 0;
            public const int ViewPagamentoFallito = 1;
            public const int ViewPagamentoRiuscito = 2;
            public const int ViewPagamentoAnnullato = 3;
            public const int ViewPagamentoInCorso = 4;
        }


        [Inject]
        protected PagamentiMIPService PagamentiService { get; set; }

        [Inject]
        protected OneriDomandaService OneriDomandaService { get; set; }

        public bool ErrorePagamento = false;
        protected CallingReason Reason
        {
            get
            {
                var reason = Request.QueryString["reason"];

                if (String.IsNullOrEmpty(reason))
                {
                    return CallingReason.InizioPagamento;
                }

                return (CallingReason)Enum.Parse(typeof(CallingReason), reason);
            }
        }

        protected string BufferMIP
        {
            get { return Request.QueryString["buffer"]; }
        }

        protected string UrlPagamenti
        {
            get;
            set;
        }

        public string TestoPagamentoFallito
        {
            get => VsGet("TestoPagamentoFallito",
                "Il pagamento è fallito oppure l'operazione di pagamento è stata annullata dall'utente." +
                "<br />" +
                "L'esito dell'operazione è: <b>{errore}</b>" +
                "<br /><br />" +
                "Fai click su <b>\"Torna Indietro\"</b> per tornare allo step precedente.");
            set => VsSet("TestoPagamentoFallito", value);
        }

        public string TestoPagamentoRiuscito
        {
            get => VsGet("TestoPagamentoRiuscito",
                "A breve riceverai una mail contenente i dettagli del pagamento.<br />" +
                "Fai click su<b>\"Vai Avanti\"</b> per proseguire con la presentazione della domanda.");
            set => VsSet("TestoPagamentoRiuscito", value);
        }

        public string TestoPagamentoAnnullato
        {
            get => VsGet("TestoPagamentoAnnullato",
                "Il pagamento è stato annullato dall'utente e nessuna somma verrà prelevata.<br />" +
                "Fai click su <b>\"Torna Indietro\"</b> per tornare allo step precedente.");
            set => VsSet("TestoPagamentoAnnullato", value);
        }

        protected string UrlReload { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override bool CanEnterStep()
        {
            if(!this.PagamentiService.VerticalizzazioneAttiva())
            {
                return false;
            }

            var tipoPagamentoDefault = this.PagamentiService.GetTipoPagamentoDefault();

            if (tipoPagamentoDefault == null)
            {
                throw new ConfigurationErrorsException("Non è stato configurato un tipo pagamento. Verificare la configurazione del modulo PAGAMENTI_MIP_RPCSUAP e riprovare");
            }

            if (this.Reason == CallingReason.InizioPagamento && ReadFacade.Domanda.Oneri.GetOneriOnlineProntiPerPagamento().Count() == 0)
            {
                return false;
            }

            return true;
        }

        public override void DataBind()
        {
            if (Reason == CallingReason.InizioPagamento)
            {
                this.IniziaPagamento();
                return;
            }

            if (Reason == CallingReason.HOME || Reason == CallingReason.BACK)
            {
                PagamentoAnnullato();
                return;
            }

            var esitoPagamento = PagamentiService.VerificaStatoPagamento(this.IdDomanda);

            switch (esitoPagamento.Stato)
            {
                case EsitoPagamentoMip.StatoPagamentoMipEnum.OK:
                    PagamentiService.PagamentoRiuscito(IdDomanda, esitoPagamento.EsitoWs);

                    multiView.ActiveViewIndex = Constants.ViewPagamentoRiuscito;
                    break;

                case EsitoPagamentoMip.StatoPagamentoMipEnum.KO:
                case EsitoPagamentoMip.StatoPagamentoMipEnum.UK:
                case EsitoPagamentoMip.StatoPagamentoMipEnum.ER:
                    var descrizioneErrore = PagamentiService.PagamentoFallito(IdDomanda, esitoPagamento.EsitoWs);

                    ltrTestoPagamentoFallito.Text = this.TestoPagamentoFallito.Replace("{errore}", descrizioneErrore);

                    multiView.ActiveViewIndex = Constants.ViewPagamentoFallito;
                    Master.MostraBottoneAvanti = false;
                    break;

                case EsitoPagamentoMip.StatoPagamentoMipEnum.OP:
                    this.UrlReload = Request.Url.AbsoluteUri;
                    multiView.ActiveViewIndex = Constants.ViewPagamentoInCorso;
                    Master.MostraBottoneAvanti = false;
                    break;
            }

        }

        private void PagamentoAnnullato()
        {
            PagamentiService.AnnullaPagamento(IdDomanda, this.BufferMIP);

            multiView.ActiveViewIndex = Constants.ViewPagamentoAnnullato;
            Master.MostraBottoneAvanti = false;
        }


        private void IniziaPagamento()
        {
            Master.MostraBottoneAvanti = false;

            var email = UserAuthenticationResult.DatiUtente.Email;

            if (String.IsNullOrEmpty(email))
            {
                email = ReadFacade.Domanda.AltriDati.DomicilioElettronico;
            }

            if (String.IsNullOrEmpty(email))
            {
                this.Errori.Add("Impossibile inizializzare il pagamento perchè l'indirizzo email dell'utente è mancante. Tornare allo step di inserimento anagrafiche e inserire un indirizzo email valido.");
                this.ErrorePagamento = true;
                return;
            }

            var estremiDomanda = new EstremiDomanda(IdDomanda, Master.LastStep, email, UserAuthenticationResult.DatiUtente.Codicefiscale);
            var datiAvvioPagamento = PagamentiService.InizializzaPagamento(estremiDomanda);

            PagamentiService.AvviaPagamento(IdDomanda, datiAvvioPagamento.NumeroOperazione, datiAvvioPagamento.Oneri);

            this.UrlPagamenti = datiAvvioPagamento.UrlAvvioPagamento;

            multiView.ActiveViewIndex = Constants.ViewNuovoPagamento;
        }

        public override void OnInitializeStep()
        {

        }

        private string VsGet(string vsKey, string defaultVal)
        {
            var str = this.ViewState[vsKey];

            if (str != null)
            {
                return str.ToString();
            }

            return defaultVal;
        }

        private void VsSet(string key, string val)
        {
            this.ViewState[key] = val;
        }
    }
}