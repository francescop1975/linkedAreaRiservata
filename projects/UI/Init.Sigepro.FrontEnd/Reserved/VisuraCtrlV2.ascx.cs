using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
using Init.Sigepro.FrontEnd.AppLogic.StcService;
using Init.Sigepro.FrontEnd.Infrastructure.IOC;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved
{
    public partial class VisuraCtrlV2 : Ninject.Web.UserControlBase
    {
        ILog _log = LogManager.GetLogger(typeof(VisuraCtrlV2));

        public delegate void ErroreRenderingDelegate(object sender, string testoErrore);
        public event ErroreRenderingDelegate ErroreRendering;

        //public delegate void ScadenzaSelezionataDelegate(object sender, int idScadenza);
        //public event ScadenzaSelezionataDelegate ScadenzaSelezionata;

        [Inject]
        public IUrlDownloadOggettiService _urlDownloadOggettiService { get; set; }

        public bool DaArchivio
        {
            get { object o = this.ViewState["DaArchivio"]; return o == null ? false : (bool)o; }
            set { this.ViewState["DaArchivio"] = value; }
        }

        public bool TempisticheVisibili
        {
            get { object o = this.ViewState["TempisticheVisibili"]; return o == null ? false : (bool)o; }
            set { this.ViewState["TempisticheVisibili"] = value; }
        }

        protected string IdComune
        {
            get { return Request.QueryString["IdComune"]; }
        }

        protected string Software
        {
            get { return Request.QueryString["Software"]; }
        }

        public DettaglioPraticaVisuraType DataSource { get; set; }

        public VisuraCtrlV2()
        {
            FoKernelContainer.Inject(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void DataBind()
        {
            try
            {
                // Tempistica
                if (DataSource.tempisticaProcedimento != null)
                {
                    var t = DataSource.tempisticaProcedimento;

                    this.TempisticheVisibili = true;

                    if (t.dataInizioSpecified)
                    {
                        this.lblDataInizio.Text = t.dataInizio.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        this.lblDataInizio.Text = "Non disponibile";
                    }

                    if (t.dataFineSpecified)
                    {
                        this.lblDataFine.Text = t.dataFine.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        this.lblDataFine.Text = "Non disponibile";
                    }

                    if (t.durataGGSpecified)
                    {
                        this.lblDurataGiorni.Text = t.durataGG.ToString() + " giorni";
                    }
                    else
                    {
                        this.lblDurataGiorni.Text = "Non disponibile";
                    }
                }

                // Dati domanda
                lblIntervento.Text = DataSource.dettaglioPratica.intervento.descrizione;
                lblNumeroPratica.Text = DataSource.dettaglioPratica.numeroPratica;
                lblOggetto.Text = DataSource.dettaglioPratica.oggetto;
                lblProtocollo.Text = DataSource.dettaglioPratica.numeroProtocolloGenerale;
                lblStatoPratica.Text = DataSource.statoPratica.ToString();
                lblDataPresentazione.Text = DataSource.dettaglioPratica.dataPratica.ToString("dd/MM/yyyy");

                if (DataSource.dettaglioPratica.dataProtocolloGeneraleSpecified)
                    lblDataProtocollo.Text = DataSource.dettaglioPratica.dataProtocolloGenerale.ToString("dd/MM/yyyy");

                // Riferimenti
                lblResponsabileProc.Text = DataSource.responsabileProcedimento;
                lblIstruttore.Text = DataSource.istruttorePratica;
                //lblOperatore.Text = DataSource.dettaglioPratica.re

                dgSoggetti.DataSource = EstraiSoggettiDomanda();
                dgSoggetti.DataBind();

                //localizzazioni
                dgLocalizzazioni.DataSource = DataSource.dettaglioPratica.localizzazione;
                dgLocalizzazioni.DataBind();
                divLocalizzazioni.Visible = (dgLocalizzazioni.DataSource != null);

                dgDatiCatastali.DataSource = EstraiDatiCatastali();
                dgDatiCatastali.DataBind();

                //allegati
                divAllegati.Visible = false;
                if (DataSource.dettaglioPratica.documenti != null)
                {
                    gvAllegati.DataSource = DataSource.dettaglioPratica.documenti.Where(x => x.allegati != null);
                    gvAllegati.DataBind();
                    divAllegati.Visible = (gvAllegati.DataSource != null);
                }

                //endoprocedimenti
                dgProcedimenti.DataSource = DataSource.dettaglioPratica.procedimenti;
                dgProcedimenti.DataBind();
                divProcedimenti.Visible = (dgProcedimenti.DataSource != null);

                //onrei
                dgOneri.DataSource = DataSource.dettaglioPratica.oneri;
                dgOneri.DataBind();
                divOneri.Visible = (dgOneri.DataSource != null);

                //movimenti
                dgMovimenti.DataSource = DataSource.listaAttivita;
                dgMovimenti.DataBind();
                divMovimenti.Visible = (dgMovimenti.DataSource != null);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Errore in Visuractrlv2.DataBind: {0}", ex.ToString());

                if (ErroreRendering != null)
                    ErroreRendering(this, ex.Message);
            }

        }

        protected void dgScadenze_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region allegati

        protected void gvAllegati_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hlDownloadAllegato = (HyperLink)e.Row.FindControl("hlDownloadAllegato");

                var doc = (DocumentiType)e.Row.DataItem;

                if (String.IsNullOrEmpty(doc.allegati?.id))
                {
                    hlDownloadAllegato.Visible = false;
                }
                else
                {
                    bool documentoFirmato = (Path.GetExtension(doc.allegati.allegato).ToUpper() == ".P7M");

                    hlDownloadAllegato.NavigateUrl = documentoFirmato ?
                        this._urlDownloadOggettiService.GetUrlDownloadFirmato(Convert.ToInt32(doc.allegati.id)) :
                        this._urlDownloadOggettiService.GetUrlDownload(Convert.ToInt32(doc.allegati.id));
                }
            }
        }

        protected string ProcessaDescrizione(object descr)
        {
            if (descr == null) return "";
            return descr.ToString().Replace("\n", "<br />");

        }

        protected string DescrizioneAttivita(object objAttivita)
        {
            if (objAttivita == null)
            {
                return String.Empty;
            }

            return ((TipoAttivitaType)objAttivita).descrizione;
        }

        protected bool VerificaEsistenzaAllegatiProcedimento(object objProcedimento)
        {
            var endo = (ProcedimentoType)objProcedimento;

            if (endo.documenti != null && endo.documenti.Length > 0)
            {
                foreach (var allegatoEndo in endo.documenti)
                {
                    if (allegatoEndo.allegati != null)
                        return true;
                }
            }

            return false;
        }

        protected bool VerificaEsistenzaAllegatiMovimento(object objMovimentiAllegatiList)
        {
            return false;
            /*var listaAllegati = (MovimentiAllegati[])objMovimentiAllegatiList;

			if (listaAllegati == null || listaAllegati.Length == 0)
				return false;

			for (int i = 0; i < listaAllegati.Length; i++)
			{
				var allegato = listaAllegati[i];

				if (allegato.FlagPubblica.GetValueOrDefault(0) == 0)
					continue;

				if (String.IsNullOrEmpty(allegato.CODICEOGGETTO))
					continue;

				return true;
			}

			return false;*/
        }

        #endregion

        private List<RiferimentoCatastaleType> EstraiDatiCatastali()
        {
            var rVal = new List<RiferimentoCatastaleType>();

            if (DataSource.dettaglioPratica.localizzazione != null)
            {
                foreach (var indirizzo in DataSource.dettaglioPratica.localizzazione)
                {
                    if (indirizzo.riferimentoCatastale != null)
                        rVal.AddRange(indirizzo.riferimentoCatastale);
                }
            }

            return rVal;
        }

        #region dati anagrafici

        protected class DatiRichiedente
        {
            public string Nominativo { get; set; }
            public string InQualitaDi { get; set; }
            public string NominativoCollegato { get; set; }
            public string Procuratore { get; set; }
        }

        private List<DatiRichiedente> EstraiSoggettiDomanda()
        {
            var listaRichiedenti = new List<DatiRichiedente>();

            listaRichiedenti.Add(new DatiRichiedente
            {
                Nominativo = DataSource.dettaglioPratica.richiedente.anagrafica.ToString(),
                InQualitaDi = DataSource.dettaglioPratica.richiedente.ruolo == null ? String.Empty : DataSource.dettaglioPratica.richiedente.ruolo.ToString()
            });

            if (DataSource.dettaglioPratica.aziendaRichiedente != null)
            {
                listaRichiedenti.Add(new DatiRichiedente
                {
                    Nominativo = DataSource.dettaglioPratica.aziendaRichiedente.ToString()
                });
            }
            if (DataSource.dettaglioPratica.intermediario != null && DataSource.dettaglioPratica.intermediario.Item != null)
            {
                listaRichiedenti.Add(new DatiRichiedente
                {
                    Nominativo = DataSource.dettaglioPratica.intermediario.Item.ToString()
                });
            }

            if (DataSource.dettaglioPratica.altriSoggetti != null)
            {
                foreach (var it in DataSource.dettaglioPratica.altriSoggetti)
                {
                    listaRichiedenti.Add(new DatiRichiedente
                    {
                        Nominativo = it.soggetto.Item.ToString(),
                        InQualitaDi = it.tipoRapporto.ruolo,
                        NominativoCollegato = (it.anagraficaCollegata != null && it.anagraficaCollegata.Item != null) ? it.anagraficaCollegata.Item.ToString() : String.Empty
                    });
                }
            }

            return listaRichiedenti;
        }

        #endregion
    }
}