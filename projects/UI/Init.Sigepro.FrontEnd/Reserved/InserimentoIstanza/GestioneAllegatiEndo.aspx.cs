namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
    using Init.Sigepro.FrontEnd.AppLogic.GestioneConversioneFiles;
    using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.Allegati;
    using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
    using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.PostedFileSpecifications;
    using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
    using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneDocumenti;
    using Init.Sigepro.FrontEnd.AppLogic.Services.Navigation;
    using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Allegati;
    using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Allegati.EventArguments;
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;

    public partial class GestioneAllegatiEndo : IstanzeStepPage
    {
        [Inject]
        public IAllegatiEndoprocedimentiService AllegatiEndoprocedimentiService { get; set; }
        [Inject]
        protected PathUtils _pathUtils { get; set; }
        [Inject]
        public ValidPostedFileSpecification _validPostedFileSpecification { get; set; }
        [Inject]
        public RedirectService _redirectService { get; set; }
        [Inject]
        public IConfigurazione<ParametriDimensioneAllegatiLiberi> _parametriAllegatiLiberi { get; set; }

        [Inject]
        public IUrlDownloadOggettiService _urlDownloadOggettiService { get; set; }

        #region Parametri letti dal file di workflow

        public bool RichiediFirmaSuAllegatiLiberi
        {
            get
            {
                object o = this.ViewState["RichiediFirmaSuAllegatiLiberi"];
                return o == null ? false : (bool)o;
            }

            set
            {
                this.ViewState["RichiediFirmaSuAllegatiLiberi"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // Il service si occupa del salvataggio dei dati
            Master.IgnoraSalvataggioDati = true;
            Master.ResetValidatorsOnLoad = false;

            if (!IsPostBack)
            {
                DataBind();
            }
        }

        #region Ciclo di vita dello step
        public override void OnInitializeStep()
        {
            AllegatiEndoprocedimentiService.SincronizzaAllegati(IdDomanda);
        }

        public override bool CanEnterStep()
        {
            return ReadFacade.Domanda.Documenti.Endo.Documenti.Count() > 0;
        }

        public override bool CanExitStep()
        {
            var listaNomiFilesNonPresenti = ReadFacade.Domanda.Documenti.Endo.GetNomiDocumentiRichiestiENonPresenti();

            Errori.AddRange(listaNomiFilesNonPresenti.Select(x => string.Format("L'allegato \"{0}\" � obbligatorio", x)));

            var listaFilesNonFirmati = ReadFacade.Domanda.Documenti.Endo.Documenti.Where(x => x.RichiedeFirmaDigitale && x.AllegatoDellUtente != null && !x.AllegatoDellUtente.FirmatoDigitalmente);

            Errori.AddRange(listaFilesNonFirmati.Select(x => string.Format("L'allegato \"{0}\" deve essere firmato digitalmente", x.Descrizione)));

            return listaNomiFilesNonPresenti.Count() == 0 && listaFilesNonFirmati.Count() == 0;
        }

        #endregion

        #region binding dei dati

        public class RepeaterEndoBindingItem
        {
            public int Codice { get; set; }

            public string Descrizione { get; set; }

            public IEnumerable<GrigliaAllegatiBindingItem> Allegati { get; set; }

            public RepeaterEndoBindingItem()
            {
                this.Allegati = new List<GrigliaAllegatiBindingItem>();
            }
        }

        /*
		public class TabellaAllegatiBindingItem
		{
			public int Id { get; set; }
			public bool Richiesto { get; set; }
			public bool RichiedeFirmaDigitale { get; set; }
			public string Descrizione { get; set; }
			public bool HaLinkInformazioni { get { return !String.IsNullOrEmpty(LinkInformazioni); } }
			public string LinkInformazioni { get; set; }
			public bool HaLinkPdf { get { return !String.IsNullOrEmpty(LinkPdf); } }
			public string LinkPdf { get; set; }
			public bool HaLinkPdfCompilabile { get { return !String.IsNullOrEmpty(LinkPdfCompilabile); } }
			public string LinkPdfCompilabile { get; set; }
			public bool HaLinkRtf { get { return !String.IsNullOrEmpty(LinkRtf); } }
			public string LinkRtf { get; set; }
			public bool HaLinkDoc { get { return !String.IsNullOrEmpty(LinkDoc); } }
			public string LinkDoc { get; set; }
			public bool HaLinkOO { get { return !String.IsNullOrEmpty(LinkOO); } }
			public string LinkOO { get; set; }
			public bool HaLinkDownloadSenzaPrecompilazione { get { return !String.IsNullOrEmpty(LinkDownloadSenzaPrecompilazione); } }
			public string LinkDownloadSenzaPrecompilazione { get; set; }
			public bool HaFile { get { return this.CodiceOggetto.HasValue; } }
			public string LinkDownloadFile { get; set; }
			public string NomeFile { get; set; }
			public int? CodiceOggetto { get; set; }
			public int Ordine { get; set; }
			public string Note { get; set; }
			public bool HaNote { get { return !String.IsNullOrEmpty(this.Note); } }
			public bool FirmatoDigitalmente { get; set; }
			public bool MostraBottoneFirma { get { return this.RichiedeFirmaDigitale && this.HaFile && !this.FirmatoDigitalmente; } }
		}
		*/
        public override void DataBind()
        {
            var dataSource = new List<RepeaterEndoBindingItem>();

            foreach (var endo in ReadFacade.Domanda.Endoprocedimenti.NonAcquisiti)
            {
                var allegatiEndo = ReadFacade.Domanda.Documenti.Endo.GetByIdEndo(endo.Codice);

                if (allegatiEndo.Count() == 0)
                {
                    continue;
                }

                var endoBindingItem = new RepeaterEndoBindingItem
                {
                    Codice = endo.Codice,
                    Descrizione = endo.Descrizione
                };

                endoBindingItem.Allegati = allegatiEndo.Select(allegatoEndo => new GrigliaAllegatiBindingItem
                {
                    CodiceOggetto = allegatoEndo.AllegatoDellUtente == null ? (int?)null : allegatoEndo.AllegatoDellUtente.CodiceOggetto,
                    Descrizione = allegatoEndo.Descrizione,
                    Id = allegatoEndo.Id,
                    LinkDoc = GetLinkPrecompilazione(allegatoEndo, DocumentoDomanda.TipoFormatoConversione.DOC),
                    ////LinkInformazioni = allegatoEndo.LinkInformazioni,
                    LinkOO = GetLinkPrecompilazione(allegatoEndo, DocumentoDomanda.TipoFormatoConversione.OPN),
                    LinkPdf = GetLinkPrecompilazione(allegatoEndo, DocumentoDomanda.TipoFormatoConversione.PDF),
                    LinkPdfCompilabile = GetLinkPrecompilazione(allegatoEndo, DocumentoDomanda.TipoFormatoConversione.PDFC),
                    LinkRtf = GetLinkPrecompilazione(allegatoEndo, DocumentoDomanda.TipoFormatoConversione.RTF),
                    LinkDownloadFile = GetLinkDownloadFile(allegatoEndo),
                    LinkDownloadSenzaPrecompilazione = GetLinkSenzaPrecompilazione(allegatoEndo),
                    NomeFile = allegatoEndo.AllegatoDellUtente == null ? string.Empty : allegatoEndo.AllegatoDellUtente.NomeFile,
                    Richiesto = allegatoEndo.Richiesto,
                    RichiedeFirmaDigitale = allegatoEndo.RichiedeFirmaDigitale,
                    Ordine = allegatoEndo.Ordine,
                    Note = allegatoEndo.Note,
                    FirmatoDigitalmente = allegatoEndo.AllegatoDellUtente == null ? false : allegatoEndo.AllegatoDellUtente.FirmatoDigitalmente,
                    DimensioneMassima = allegatoEndo.DimensioneMassima * 1024,
                    EstensioniAmmesse = allegatoEndo.EstensioniAmmesse
                })
                                                        .OrderBy(x => x.Ordine);

                dataSource.Add(endoBindingItem);
            }

            dataSource.Sort((a, b) => a.Descrizione.CompareTo(b.Descrizione));

            rptEndo.DataSource = dataSource;
            rptEndo.DataBind();

            ddlTipoAllegato.DataSource = dataSource;
            ddlTipoAllegato.DataBind();

            ddlNumeroPagine.Items.AddRange(Enumerable.Range(1, 30)
                                                     .Select(x => new ListItem(x.ToString()))
                                                     .ToArray());

            ddlFormato.DataSource = this._parametriAllegatiLiberi.Parametri.FormatiAllegatiLiberi;
            ddlFormato.DataBind();
            ddlFormato.Items.Insert(0, new ListItem());

            pnlDimensioniFile.Visible = this._parametriAllegatiLiberi.Parametri.FormatiAllegatiLiberi.Length > 0;
        }

        private string GetLinkDownloadFile(DocumentoDomanda allegatoEndo)
        {
            if (allegatoEndo.AllegatoDellUtente == null)
            {
                return string.Empty;
            }

            return this._urlDownloadOggettiService.GetUrlDownload(allegatoEndo.AllegatoDellUtente.CodiceOggetto);
        }

        private string GetLinkPrecompilazione(DocumentoDomanda documento, DocumentoDomanda.TipoFormatoConversione formato)
        {
            if (!documento.SupportaPrecompilazioneNelFormato(formato))
            {
                return string.Empty;
            }

            if (formato == DocumentoDomanda.TipoFormatoConversione.PDFC)
            {
                return _urlDownloadOggettiService.GetUrlDownloadPdfCompilabile(documento.CodiceOggettoModello.Value, IdDomanda);
            }

            var formatoEnum = (FormatoConversioneEnum)Enum.Parse(typeof(FormatoConversioneEnum), formato.ToString());

            return _urlDownloadOggettiService.GetUrlDownloadCompilato(documento.CodiceOggettoModello.Value, IdDomanda, formatoEnum);
        }

        private string GetLinkSenzaPrecompilazione(DocumentoDomanda documento)
        {
            if (!documento.CodiceOggettoModello.HasValue || documento.SupportaPrecompilazione())
            {
                return string.Empty;
            }

            return this._urlDownloadOggettiService.GetUrlDownload(documento.CodiceOggettoModello.Value);
        }

        protected void OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var grigliaAllegati = (GrigliaAllegati)e.Item.FindControl("grigliaAllegati");

                grigliaAllegati.DataSource = ((RepeaterEndoBindingItem)e.Item.DataItem).Allegati;
                grigliaAllegati.DataBind();
            }
        }

        #endregion

        #region Gestione degli eventi delle gridview

        protected void OnAllegaDocumento(object sender, AllegaDocumentoEventArgs e)
        {
            try
            {


                AllegatiEndoprocedimentiService.AggiungiAllegatoAEndo(IdDomanda, e.IdAllegato, e.File);

                DataBind();
            }
            catch (Exception ex)
            {
                Errori.Add(ex.Message);
            }
        }

        protected void ErroreGriglia(object sender, string messaggioErrore)
        {
            this.Errori.Add(messaggioErrore);
        }

        protected void OnRimuoviDocumento(object sender, RimuoviDocumentoEventArgs e)
        {
            try
            {
                AllegatiEndoprocedimentiService.EliminaOggettoUtente(IdDomanda, e.IdAllegato);

                DataBind();
            }
            catch (Exception ex)
            {
                Errori.Add(ex.Message);
            }
        }

        protected void OnCompilaDocumento(object sender, CompilaDocumentoEventArgs e)
        {
            this._redirectService.RedirectToPaginaCompilazioneOggetti(IdDomanda, e.IdAllegato, PathUtils.UrlParametersValues.TipoAllegatoEndo);
        }

        protected void OnFirmaDocumento(object sender, FirmaDocumentoEventArgs e)
        {
            this._redirectService.ToFirmaDigitale(IdDomanda, e.CodiceOggetto);
        }
        #endregion

        #region inserimento di un nuovo allegato

        protected void cmdNuovoAllegato_Click(object sender, EventArgs e)
        {
            ddlTipoAllegato.SelectedIndex = 0;
            txtDescrizioneAllegato.Value = string.Empty;

            Master.MostraPaginatoreSteps = false;
        }

        protected void cmdAggiungi_Click(object sender, EventArgs e)
        {
            try
            {
                var specification = (IValidPostedFileSpecification)this._validPostedFileSpecification;

                if (this._parametriAllegatiLiberi.Parametri.FormatiAllegatiLiberi.Length > 0)
                {
                    var formato = this._parametriAllegatiLiberi.Parametri.FormatiAllegatiLiberi.Where(x => x.Id == Convert.ToInt32(ddlFormato.Value)).FirstOrDefault();
                    var numeroPagine = Convert.ToInt32(ddlNumeroPagine.Value);
                    var dimensioneMassima = formato.DimensioneMaxPagina * 1024 * numeroPagine;

                    specification = new SizeBasedValidPostedFileSpecification(dimensioneMassima);
                }

                BinaryFile file = new BinaryFile(fuUploadNuovo.PostedFile, specification);

                var idDomanda = this.IdDomanda;
                var descrizione = txtDescrizioneAllegato.Value;
                var tipoAllegato = Convert.ToInt32(ddlTipoAllegato.Value);
                var richiedeFirma = this.RichiediFirmaSuAllegatiLiberi;

                AllegatiEndoprocedimentiService.AggiungiAllegatoLibero(idDomanda, tipoAllegato, descrizione, file, richiedeFirma);

                DataBind();

                cmdCancel_Click(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Errori.Add(ex.Message);
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            Master.MostraPaginatoreSteps = true;
        }
        #endregion

        protected IValidPostedFileSpecification grigliaAllegati_GetValidationSpecification(object sender, int idAllegato)
        {
            var allegato = this.ReadFacade.Domanda.Documenti.Endo.Documenti.Where(x => x.Id == idAllegato).FirstOrDefault();

            var specification = (IValidPostedFileSpecification)_validPostedFileSpecification;

            if (allegato.DimensioneMassima > 0)
            {
                specification = new SizeBasedValidPostedFileSpecification(allegato.DimensioneMassima * 1024);
            }

            return specification.And(new EstensioneValidaPostedFileSpecification(allegato.EstensioniAmmesse));
        }
    }
}
