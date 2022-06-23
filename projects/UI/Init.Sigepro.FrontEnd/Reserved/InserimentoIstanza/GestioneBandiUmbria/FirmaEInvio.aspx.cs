using Init.Sigepro.FrontEnd.AppLogic.GestioneBandiUmbria;
using Init.Sigepro.FrontEnd.AppLogic.GestioneConversioneFiles;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.PostedFileSpecifications;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
using Init.Sigepro.FrontEnd.AppLogic.InvioDomanda;
using Init.Sigepro.FrontEnd.AppLogic.InvioDomanda.MessaggiErroreInvio;
using Init.Sigepro.FrontEnd.AppLogic.Services.Navigation;
using Init.Sigepro.FrontEnd.AppLogic.WsInterventi;
using log4net;
using Ninject;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.GestioneBandiUmbria
{
    public partial class FirmaEInvio : IstanzeStepPage
    {
        private static class Constants
        {
            // public const string MsgErroreComuneNonSelezionato = "Per poter proseguire è necessario selezionare il comune per cui si vuole presentare l'istanza";
            // public const string MsgErroreBandoNonAttivo = "Il bando sarà attivo dalle ore {0} del {1}. Non è ancora possibile presentare domande";
            public const string MsgErroreBandoScaduto = "Il bando è scaduto e non è più possibile presentare domande";
        }

        ILog _log = LogManager.GetLogger(typeof(BenvenutoBandi));

        [Inject]
        protected IBandiUmbriaService _service { get; set; }

        [Inject]
        protected PathUtils _pathUtils { get; set; }

        [Inject]
        public InvioDomandaService _invioDomandaService { get; set; }

        [Inject]
        public IMessaggioErroreInvioService _messaggioErroreService { get; set; }

        [Inject]
        public ValidPostedFileSpecification _validPostedFileSpecification { get; set; }

        [Inject]
        public IUrlDownloadOggettiService _urlDownloadOggettiService { get; set; }

        public string TestoInvio
        {
            get { return ltrTestoInvio.Text; }
            set { ltrTestoInvio.Text = value; }
        }

        public bool BandoConcluso
        {
            get
            {
                var idIntervento = ReadFacade.Domanda.AltriDati.Intervento.Codice;
                var interventoAttivo = ReadFacade.Interventi.VerificaAccessoIntervento(idIntervento, UserAuthenticationResult.LivelloAutenticazione, UserAuthenticationResult.DatiUtente.UtenteTester);
                return interventoAttivo != RisultatoVerificaAccessoIntervento.Accessibile;
            }
        }

        public string DataFineBando
        {
            get { object o = this.ViewState["DataFineBando"]; return o == null ? "11/02/2015 23.59.59" : (string)o; }
            set { this.ViewState["DataFineBando"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                DataBind();
        }

        private void MostraErroreBandoScaduto()
        {
            this._log.ErrorFormat("L'utente {0} ha acceduto alle domande dei bandi ma il bando è scaduto (cod intervento: {1})", UserAuthenticationResult.DatiUtente.Codicefiscale, ReadFacade.Domanda.AltriDati.Intervento.Codice);


            var err = Constants.MsgErroreBandoScaduto;

            this.Errori.Add(err);
        }


        protected class AllegatoDaFirmareBindingItem
        {
            AllegatoDomandaBandi _allegato;

            public bool HaFileFirmato => this._allegato.IdAllegatoFirmatoDigitalmente.HasValue;
            public string Descrizione => this._allegato.Descrizione;
            public string Id => this._allegato.Id;
            public string NomeFile => this._allegato.NomeFile;
            public string NomeFileFirmatoDigitalmente => this._allegato.NomeFileFirmatoDigitalemnte;
            public string UrlDownloadFileFirmatoDigitalmente { get; set; }
            public string UrlDownloadFileDaFirmare { get; set; }

            public AllegatoDaFirmareBindingItem(AllegatoDomandaBandi allegato, IUrlDownloadOggettiService urlDownloadOggetti)
            {
                this._allegato = allegato;

                this.UrlDownloadFileDaFirmare = urlDownloadOggetti.GetUrlDownloadConvertito(allegato.IdAllegato.Value, FormatoConversioneEnum.PDF);

                if (allegato.IdAllegatoFirmatoDigitalmente.HasValue)
                {
                    this.UrlDownloadFileFirmatoDigitalmente = urlDownloadOggetti.GetUrlDownloadFirmato(allegato.IdAllegatoFirmatoDigitalmente.Value);
                }
            }
        }

        public override void DataBind()
        {
            var allegati = this._service.GetAllegatiCheNecessitanoFirma(IdDomanda);

            rptAllegatiDaFirmare.DataSource = allegati.Select(x => new AllegatoDaFirmareBindingItem(x, _urlDownloadOggettiService));
            rptAllegatiDaFirmare.DataBind();

            this.pnlInvio.Visible = allegati.Where(x => !x.IdAllegatoFirmatoDigitalmente.HasValue).Count() == 0;


            if (BandoConcluso)
            {
                MostraErroreBandoScaduto();
                this.Master.MostraBottoneAvanti = false;
                this.Master.MostraPaginatoreSteps = false;
                this.pnlInvio.Visible = false;
            }
        }

        protected void OnFileUploaded(object sender, EventArgs e)
        {
            try
            {
                var link = (Button)sender;
                var fuAllegato = (FileUpload)link.NamingContainer.FindControl("fuAllegato");
                var hidId = (HiddenField)link.NamingContainer.FindControl("hidId");

                this._service.AggiungiFileFirmatoAdAllegato(IdDomanda, hidId.Value, new BinaryFile(fuAllegato, this._validPostedFileSpecification));
            }
            catch (Exception ex)
            {
                this.Errori.Add(ex.Message);
            }
            finally
            {
                DataBind();
            }
        }

        protected void OnDeleteClicked(object sender, EventArgs e)
        {
            try
            {
                var link = (LinkButton)sender;
                var id = link.CommandArgument;

                this._service.RimuoviFileFirmatoDaAllegato(IdDomanda, id);
            }
            catch (Exception ex)
            {
                this.Errori.Add(ex.Message);
            }
            finally
            {
                DataBind();
            }
        }

        protected void InviaPratica(object sender, EventArgs e)
        {
            this._service.PreparaAllegatiDomanda(IdDomanda);

            var risultato = _invioDomandaService.Invia(IdDomanda, String.Empty);

            if (risultato.Esito == InvioIstanzaResult.TipoEsitoInvio.InvioRiuscito || risultato.Esito == InvioIstanzaResult.TipoEsitoInvio.InvioRiuscitoNoBackend)
            {
                Redirect("~/Reserved/InserimentoIstanza/CertificatoInvio.aspx", qs =>
                {
                    qs.Add("Id", risultato.CodiceIstanza);
                    qs.Add("IdPresentazione", this.IdDomanda);
                });

                return;
            }

            multiView.ActiveViewIndex = 1;
            Master.MostraPaginatoreSteps = false;

            lblErroreInvio.Text = this._messaggioErroreService.GeneraMessaggioErrore(IdDomanda);
        }
    }
}