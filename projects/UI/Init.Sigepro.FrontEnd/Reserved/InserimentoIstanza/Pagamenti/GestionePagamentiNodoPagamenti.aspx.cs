﻿using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.PostedFileSpecifications;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOneri;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.NODOPAGAMENTI;
using Init.Sigepro.FrontEnd.AppLogic.Services.Navigation;
using Init.Sigepro.FrontEnd.Infrastructure;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Pagamenti.Specifications;
using log4net;
using Ninject;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Pagamenti
{
    public partial class GestionePagamentiNodoPagamenti : IstanzeStepPage
    {
        private static class VistaFile
        {
            public const int Caricamento = 0;
            public const int DettagliFile = 1;
        }


        [Inject]
        public OneriDomandaService OneriDomandaService { get; set; }
        [Inject]
        public PagamentiNodoPagamentiService NodoPagamentiService { get; set; }
        [Inject]
        public ValidPostedFileSpecification _validPostedFileSpecification { get; set; }
        [Inject]
        public RedirectService _redirectService { get; set; }
        [Inject]
        public IUrlDownloadOggettiService _urlDownloadService { get; set; }

        ILog _logger = LogManager.GetLogger(typeof(GestioneOneri));


        #region parametri letti dal file xml
        public bool VerificaFirmaDigitaleBollettino
        {
            get { object o = this.ViewState["VerificaFirmaDigitaleBollettino"]; return o == null ? false : (bool)o; }
            set { this.ViewState["VerificaFirmaDigitaleBollettino"] = value; }
        }

        public string EtichettaColonnaEndoprocedimento
        {
            get { object o = this.ViewState["EtichettaColonnaEndoprocedimento"]; return o == null ? "" : (string)o; }
            set { this.ViewState["EtichettaColonnaEndoprocedimento"] = value; }
        }

        public string EtichettaColonnaCausaleEndo
        {
            get { object o = this.ViewState["EtichettaColonnaCausaleEndo"]; return o == null ? "" : (string)o; }
            set { this.ViewState["EtichettaColonnaCausaleEndo"] = value; }
        }

        public string EtichettaColonnaIntervento
        {
            get { object o = this.ViewState["EtichettaColonnaIntervento"]; return o == null ? "" : (string)o; }
            set { this.ViewState["EtichettaColonnaIntervento"] = value; }
        }

        public string EtichettaColonnaCausaleIntervento
        {
            get { object o = this.ViewState["EtichettaColonnaCausaleIntervento"]; return o == null ? "" : (string)o; }
            set { this.ViewState["EtichettaColonnaCausaleIntervento"] = value; }
        }

        public string TitoloCaricamentoBollettino
        {
            get { object o = this.ViewState["TitoloCaricamentoBollettino"]; return o == null ? "" : (string)o; }
            set { this.ViewState["TitoloCaricamentoBollettino"] = value; }
        }

        public string DescrizioneCaricamentoBollettino
        {
            get { object o = this.ViewState["DescrizioneCaricamentoBollettino"]; return o == null ? "" : (string)o; }
            set { this.ViewState["DescrizioneCaricamentoBollettino"] = value; }
        }

        public string DescrizioneCaricamentoBollettinoEffettuato
        {
            get { object o = this.ViewState["DescrizioneCaricamentoBollettinoEffettuato"]; return o == null ? "" : (string)o; }
            set { this.ViewState["DescrizioneCaricamentoBollettinoEffettuato"] = value; }
        }

        public string TestoDichiarazioneAssenzaOneri
        {
            get { object o = this.ViewState["TestoDichiarazioneAssenzaOneri"]; return o == null ? "Dichiaro di non avere oneri da pagare" : (string)o; }
            set { this.ViewState["TestoDichiarazioneAssenzaOneri"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // Il Service si occupa del salvataggio dei dati
            Master.IgnoraSalvataggioDati = true;

            if (!IsPostBack)
                DataBind();
        }


        #region Ciclo di vita dello step
        public override void OnInitializeStep()
        {
            OneriDomandaService.SincronizzaOneri(IdDomanda, ComportamentoSincronizzazioneOneriSenzaImporto.Includi);
        }

        public override bool CanEnterStep()
        {
            if (!this.NodoPagamentiService.NodoPagamentoAttivo(this.IdDomanda))
            {
                var url = UrlBuilder.Url("~/reserved/inserimentoistanza/gestioneoneri.aspx", pb =>
                {
                    pb.Add(new QsAliasComune(this.IdComune));
                    pb.Add(new QsSoftware(this.Software));
                    pb.Add(new QsStepId(Request.QueryString));
                    pb.Add(new QsIdDomandaOnline(this.IdDomanda));
                });

                Response.Redirect(url);
                return true;
            }

            // TODO ????
            if (NodoPagamentiService.GetStatoPagamentiInSospeso(IdDomanda).Pagamenti.Count() > 0)
            {
                this.Master.cmdPrevStep_Click(this, EventArgs.Empty);
                return false;
            }

            return new DomandaContieneAlmenoUnOnereSpecification().IsSatisfiedBy(ReadFacade.Domanda);
        }

        public override bool CanExitStep()
        {
            var oneri = ReadFacade.Domanda.Oneri;

            if (Errori.Count > 0)
                return false;

            if (new TuttiGliOneriPagatiOnlineSpecification().IsSatisfiedBy(oneri))
                return true;

            if (new NessunOnereDovutoSpecification().AndNot(new UtenteDichiaraDiNonAvereOneriSpecification()).IsSatisfiedBy(oneri))
            {
                Errori.Add("Per proseguire è necessario dichiarare che la pratica non contiene oneri da pagare");

                return false;
            }

            if (new AlmenoUnOnerePagatoOffline().AndNot(new DomandaContieneAttestazioneDiPagamentoSpecification()).IsSatisfiedBy(oneri))
            {
                Errori.Add("Caricare l'attestazione di pagamento per gli oneri già pagati fuori dal circuito Pago PA");

                return false;
            }

            if (VerificaFirmaDigitaleBollettino && new DomandaContieneAttestazioneDiPagamentoSpecification().AndNot(new AttestazioneDiPagamentoFirmataDigitalmenteSpecification()).IsSatisfiedBy(oneri))
            {
                Errori.Add("Il documento \"attestazione di pagamento\" non è stato firmato digitalmente");

                return false;
            }

            // VerificaFirmaDigitaleBollettino

            return true;



            if (Errori.Count > 0)
                return false;

            if (new NessunOnereDovutoSpecification().And(new UtenteDichiaraDiNonAvereOneriSpecification()).IsSatisfiedBy(ReadFacade.Domanda.Oneri))
                return true;

            return true;


            if (new NessunOnereDovutoSpecification().AndNot(new UtenteDichiaraDiNonAvereOneriSpecification()).IsSatisfiedBy(oneri))
            {
                Errori.Add("Per proseguire è necessario dichiarare che la pratica non contiene oneri da pagare");

                return false;
            }
            /*
            if (new TuttiGliOneriPagatiOnlineSpecification().IsSatisfiedBy(ReadFacade.Domanda.Oneri))
                return true;



            if (VerificaFirmaDigitaleBollettino && new DomandaContieneAttestazioneDiPagamentoSpecification().And(new AttestazioneDiPagamentoFirmataDigitalmenteSpecification()).IsSatisfiedBy(ReadFacade.Domanda.Oneri))
                return true;

            if (!VerificaFirmaDigitaleBollettino && new DomandaContieneAttestazioneDiPagamentoSpecification().IsSatisfiedBy(ReadFacade.Domanda.Oneri))
                return true;

            Errori.Add("Per poter proseguire è necessario allegare una copia firmata digitalmente della ricevuta attestante l'avvenuto pagamento");

            return false;
            */
        }

        public override void OnBeforeExitStep()
        {
            var estremi = new EstremiPagamentoDataExtractor(this.grigliaOneriIntervento.Repeater, this.grigliaOneriEndo.Repeater).EstraiDati(false);

            this.Errori.AddRange(estremi.Errori);

            var oneriConImporto0 = estremi.Estremi.Where(x => x.ModalitaPagamento == ModalitaPagamentoOnereEnum.Online && x.EstremiPagamento.ImportoPagato == 0);

            oneriConImporto0.ToList().ForEach(x => this.Errori.Add($"L'onere {x.Descrizione} non ha un importo specificato. Utilizzare l'opzione \"Non dovuto\" se l'onere non è dovuto."));

            if (this.Errori.Count == 0)
            {
                OneriDomandaService.SpecificaEstremiPagamentoOneriNonPagatiOnline(IdDomanda, estremi.Estremi);
            }

        }

        #endregion

        public override void DataBind()
        {
            var modalitaPagamento = this.OneriDomandaService.GetListaModalitaPagamento().ToList();
            modalitaPagamento.Insert(0, new TipoPagamento("", ""));

            grigliaOneriIntervento.Visible = new DomandaContieneOneriDaInterventoSpecification().IsSatisfiedBy(ReadFacade.Domanda);
            grigliaOneriIntervento.EtichettaColonnaCausale = EtichettaColonnaCausaleIntervento;
            grigliaOneriIntervento.ModalitaPagamento = modalitaPagamento;
            grigliaOneriIntervento.DataSource = ReadFacade.Domanda.Oneri.OneriIntervento;
            grigliaOneriIntervento.DataBind();

            grigliaOneriEndo.Visible = new DomandaContieneOneriDaEndoSpecification().IsSatisfiedBy(ReadFacade.Domanda);
            grigliaOneriEndo.EtichettaColonnaCausale = EtichettaColonnaCausaleEndo;
            grigliaOneriEndo.ModalitaPagamento = modalitaPagamento;
            grigliaOneriEndo.DataSource = ReadFacade.Domanda.Oneri.OneriEndoprocedimenti;
            grigliaOneriEndo.DataBind();

            chkAssenzaOneri.Checked = ReadFacade.Domanda.Oneri.DichiaraDiNonAvereOneriDaPagare;

            if (!new DomandaContieneAttestazioneDiPagamentoSpecification().IsSatisfiedBy(ReadFacade.Domanda.Oneri))
            {
                MostraVistaCaricamentoFile();
            }
            else
            {
                MostraVistaDettaglioFile();
            }
        }

        private void SalvaValoriEstremiOneri()
        {
            var estremi = new EstremiPagamentoDataExtractor(this.grigliaOneriIntervento.Repeater, this.grigliaOneriEndo.Repeater).EstraiDati(true);

            OneriDomandaService.SpecificaEstremiPagamento(IdDomanda, estremi.Estremi);
        }

        protected void cmdUpload_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaValoriEstremiOneri();


                var file = new BinaryFile(fuCaricaFile, this._validPostedFileSpecification);

                OneriDomandaService.InserisciAttestazioneDiPagamento(IdDomanda, file);

                DataBind();
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Errore durante il caricamento del bollettino in gestioneoneri.aspx: {0}", ex.ToString());

                Errori.Add(ex.Message);
            }

        }

        protected void cmdRimuovi_Click(object sender, EventArgs e)
        {
            try
            {
                SalvaValoriEstremiOneri();

                OneriDomandaService.EliminaAttestazioneDiPagamento(IdDomanda);

                DataBind();
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Errore durante la rimozione del bollettino in gestioneoneri.aspx: {0}", ex.ToString());

                Errori.Add(ex.Message);
            }
        }

        protected void cmdFirma_Click(object sender, EventArgs e)
        {
            var codiceOggetto = ReadFacade.Domanda.Oneri.AttestazioneDiPagamento.CodiceOggetto.Value;

            this._redirectService.ToFirmaDigitale(IdDomanda, codiceOggetto);
        }

        protected void chkAssenzaOneri_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAssenzaOneri.Checked)
            {
                OneriDomandaService.ImpostaDichiarazioneDiAssenzaOneriDaPagare(IdDomanda);
            }
            else
            {
                OneriDomandaService.RimuoviDichiarazioneDiAssenzaOneriDaPagare(IdDomanda);
            }
        }

        #region Gestione delle views di caricamento/Visualizzazione attestazione oneri
        private void MostraVistaCaricamentoFile()
        {
            mvCaricamentoBollettino.ActiveViewIndex = VistaFile.Caricamento;
        }

        private void MostraVistaDettaglioFile()
        {
            mvCaricamentoBollettino.ActiveViewIndex = VistaFile.DettagliFile;

            var attestazionediPagamento = ReadFacade.Domanda.Oneri.AttestazioneDiPagamento;

            var codiceOggetto = attestazionediPagamento.CodiceOggetto;
            var nomeFile = attestazionediPagamento.NomeFile;

            // Dati del file caricato
            var url = this._urlDownloadService.GetUrlDownload(codiceOggetto.Value);

            hlFileCaricato.Text = nomeFile;
            hlFileCaricato.NavigateUrl = ResolveClientUrl(url);

            // Etichetta di errore se firma digitale mancante
            //cmdFirma.Visible =
            lblErroreFirma.Visible = false;

            if (this.VerificaFirmaDigitaleBollettino && !attestazionediPagamento.FirmatoDigitalmente)
                /*cmdFirma.Visible = */
                lblErroreFirma.Visible = true;

            Master.MostraBottoneAvanti = true;
        }
        #endregion
    }
}