using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GestioneRisorseTestuali;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using Init.Sigepro.FrontEnd.Infrastructure.Caching;
using Init.Sigepro.FrontEnd.Infrastructure.IOC;
using Init.Sigepro.FrontEnd.WebControls.Common;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.WebControls.Visura
{
    public abstract class ListaVisuraDataGridBase : GridView, IDatabaseSoftwareControl
    {
        [Inject]
        public ICampiRicercaVisuraRepository _campiRicercaVisuraRepository { get; set; }

        [Inject]
        protected IConfigurazione<ParametriPresentazioneDomanda> _parPresentazioneDomanda { get; set; }

        [Inject]
        protected IRisorseTestualiService _risorseTestualiService { get; set; }

        public string NavigateUrlFormatString
        {
            get { return m_selectColumn.DataNavigateUrlFormatString; }
            set { m_selectColumn.DataNavigateUrlFormatString = value; }
        }

        public bool PermettiUsaComeModello
        {
            get { object o = ViewState["PermettiUsaComeModello"]; return o == null ? false : (bool)o; }
            set { ViewState["PermettiUsaComeModello"] = value; }
        }



        public class CachedDataSource
        {
            public int LastPage { get; set; }
            public IEnumerable<VisuraListItem> Dati { get; set; }
        }

        public delegate void IstanzaSelezionataDelegate(object sender, string idComune, string software, string idIstanza);

        public event IstanzaSelezionataDelegate IstanzaSelezionata;

        /// <summary>
        /// Restituisce liindice della colonna "Data presentazione". 
        /// Utilizzare il valore per forzare il parsing di tipi "date" nell'eventuale DataTables utilizzato nella visualizzazione a lista
        /// </summary>
        public int IndiceColonnaDataIstanza { get; private set; } = -1;

        /// <summary>
        /// Restituisce liindice della colonna "Data protocollo". 
        /// Utilizzare il valore per forzare il parsing di tipi "date" nell'eventuale DataTables utilizzato nella visualizzazione a lista
        /// </summary>
        public int IndiceColonnaDataProtocollo { get; private set; } = -1;

        private ILog _log = LogManager.GetLogger(typeof(ListaVisuraDataGridBase));

        private Dictionary<int, BoundField> m_columns = new Dictionary<int, BoundField>();

        private BoundField m_oggetto = new BoundField();
        private BoundField m_progressivo = new BoundField();
        private BoundField m_tipoIntervento = new BoundField();
        private BoundField m_numeroProtocollo = new BoundField();
        private BoundField m_operatore = new BoundField();
        private BoundField m_codiceArea = new BoundField();
        private BoundField m_civico = new BoundField();
        private BoundField m_stato = new BoundField();
        private BoundField m_dataPresentazione = new BoundField();
        private BoundField m_subalterno = new BoundField();
        private BoundField m_numeroIstanza = new BoundField();
        private BoundField m_tipoProcedura = new BoundField();
        private BoundField m_localizzazione = new BoundField();
        private BoundField m_dataProtocollo = new BoundField();
        private BoundField m_particella = new BoundField();
        private BoundField m_software = new BoundField();
        private BoundField m_richiedente = new BoundField();
        private BoundField m_foglio = new BoundField();
        private BoundField m_tipoCatasto = new BoundField();
        private BoundField _ragioneSociale = new BoundField();
        private BoundField m_posizioneArchivio = new BoundField();
        private HyperLinkField m_selectColumn = new HyperLinkField();

        IFiltriVisuraControlProvider m_provider = null;

        public new IEnumerable<VisuraListItem> DataSource
        {
            get { return (IEnumerable<VisuraListItem>)base.DataSource; }
            set
            {
                base.DataSource = value.ToList();
                SessionDataSource.Dati = value.ToList();
                PageIndex = 0;
            }
        }

        public override int PageIndex
        {
            get
            {
                return SessionDataSource.LastPage;
            }
            set
            {
                SessionDataSource.LastPage = value;
                base.PageIndex = value;
            }
        }

        protected CachedDataSource SessionDataSource
        {
            get
            {
                return SessionHelper.GetEntry("VisuraDataGrid.DataSource", () => new CachedDataSource());
            }
            set { SessionHelper.AddEntry("VisuraDataGrid.DataSource", value); }
        }


        protected void Rebind()
        {
            base.DataSource = SessionDataSource.Dati;
            base.DataBind();
        }


        public string IdComune
        {
            get
            {
                object o = ViewState["IdComune"];
                return o == null ? "" : o.ToString();
            }
            set
            {
                EnsureChildControls();
                ViewState["IdComune"] = value;
            }
        }

        public string Software
        {
            get
            {
                object o = ViewState["Software"];
                return o == null ? "" : o.ToString();
            }
            set { ViewState["Software"] = value; }
        }

        internal ListaVisuraDataGridBase(IFiltriVisuraControlProvider provider)
        {
            FoKernelContainer.Inject(this);

            AutoGenerateColumns = false;

            m_provider = provider;

            Init += VisuraDataGrid_Init;
            Load += OnLoad;
            RowCreated += OnRowCreated;

            GridLines = GridLines.None;
            CssClass = "table";

            EmptyDataText = "";
        }

        private void OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
                PermettiUsaComeModello &&
                _parPresentazioneDomanda.Parametri.AbilitaTemplateDomanda)
            {
                var cell = e.Row.Cells.Cast<TableCell>().Last();

                var ul = new HtmlGenericControl("ul");
                var li1 = new HtmlGenericControl("li");
                var li2 = new HtmlGenericControl("li");
                var link = new HyperLink
                {
                    Text = "Usa come modello",
                    ID = "hlUsaComeModello"
                };

                ul.Attributes.Add("class", "azioni-tabella");
                ul.Controls.Add(li1);
                ul.Controls.Add(li2);


                li1.Controls.Add(cell.Controls[cell.Controls.Count - 1]);
                li2.Controls.Add(link);

                cell.Controls.Add(ul);
            }
        }


        private void OnLoad(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    int recPerPagina = _campiRicercaVisuraRepository.GetRecordPerPagina(IdComune, Software);
                }
                catch (Exception)
                { /*potrebbe dare errori nel designer*/
                }
            }


            try
            {
                Columns.Clear();
                // l'indice della colonna data istanza e data protocollo è utilizzato su DataTables 
                // per forzare un ordinamento di tipo data
                IndiceColonnaDataIstanza = -1;
                IndiceColonnaDataProtocollo = -1;

                var campiLista = m_provider.GetCampiTabella(IdComune, Software);

                for (int i = 0; i < campiLista.Length; i++)
                {
                    var campo = campiLista[i];

                    if (!m_columns.ContainsKey(campo.Codice))
                    {
                        Debug.WriteLine("Lista pratiche: Il dizionario non contiene l'id " + campo.Codice);
                    }
                    else
                    {
                        var col = m_columns[campo.Codice];

                        if (col == m_dataPresentazione)
                        {
                            IndiceColonnaDataIstanza = i;
                        }

                        if (col == m_dataProtocollo)
                        {
                            IndiceColonnaDataProtocollo = i;
                        }

                        var bc = col;

                        bc.HeaderText = this._risorseTestualiService.GetRisorsa(campo.IdRisorsa, campo.Etichetta);
                        Columns.Add(bc);
                    }
                }
                m_selectColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                m_selectColumn.Text = "Mostra dettagli";
                m_selectColumn.DataNavigateUrlFields = new string[] { "Uuid" };
                m_selectColumn.ItemStyle.CssClass = "colonna-azioni vbg-show-spinner";

                Columns.Add(m_selectColumn);

                Rebind();
            }
            catch (Exception)
            { /*potrebbe dare errori nel designer*/
            }
        }


        private void VisuraDataGrid_Init(object sender, EventArgs e)
        {
            AutoGenerateColumns = false;

            m_oggetto.DataField = "Oggetto";
            m_progressivo.DataField = "Progressivo";
            m_tipoIntervento.DataField = "TipoIntervento";
            m_numeroProtocollo.DataField = "NumeroProtocollo";
            m_operatore.DataField = "Operatore";
            m_codiceArea.DataField = "CodiceArea";
            m_civico.DataField = "Civico";
            m_stato.DataField = "Stato";
            m_dataPresentazione.DataField = "DataPresentazione";
            m_dataPresentazione.DataFormatString = "{0:dd/MM/yyyy}";
            m_subalterno.DataField = "Subalterno";
            m_numeroIstanza.DataField = "NumeroIstanza";
            m_tipoProcedura.DataField = "TipoProcedura";
            m_localizzazione.DataField = "LocalizzazioneConCivico";
            m_dataProtocollo.DataField = "DataProtocollo";
            m_dataProtocollo.DataFormatString = "{0:dd/MM/yyyy}";
            m_particella.DataField = "Particella";
            m_software.DataField = "Software";
            m_richiedente.DataField = "Richiedente";
            m_foglio.DataField = "Foglio";
            m_tipoCatasto.DataField = "TipoCatasto";
            _ragioneSociale.DataField = "Azienda";
            m_posizioneArchivio.DataField = "PosizioneArchivio";

            m_columns.Add(m_provider.ListaIdOperatore, m_operatore);
            m_columns.Add(m_provider.ListaIdRichiedente, m_richiedente);
            m_columns.Add(m_provider.ListaIdTipoprocedura, m_tipoProcedura);
            m_columns.Add(m_provider.ListaIdOggetto, m_oggetto);
            m_columns.Add(m_provider.ListaIdParticella, m_particella);
            m_columns.Add(m_provider.ListaIdSubalterno, m_subalterno);
            m_columns.Add(m_provider.ListaIdProgressivo, m_progressivo);
            m_columns.Add(m_provider.ListaIdLocalizzazione, m_localizzazione);
            m_columns.Add(m_provider.ListaIdCodicearea, m_codiceArea);
            m_columns.Add(m_provider.ListaIdFoglio, m_foglio);
            m_columns.Add(m_provider.ListaIdNumeroistanza, m_numeroIstanza);
            m_columns.Add(m_provider.ListaIdDatapresentazione, m_dataPresentazione);
            m_columns.Add(m_provider.ListaIdTipointervento, m_tipoIntervento);
            m_columns.Add(m_provider.ListaIdStato, m_stato);
            m_columns.Add(m_provider.ListaIdNumeroprotocollo, m_numeroProtocollo);
            m_columns.Add(m_provider.ListaIdDataprotocollo, m_dataProtocollo);
            m_columns.Add(m_provider.ListaIdTipocatasto, m_tipoCatasto);
            m_columns.Add(m_provider.ListaIdRagioneSociale, _ragioneSociale);
            m_columns.Add(m_provider.ListaIdPosizioneArchivio, m_posizioneArchivio);

            RowDataBound += ListaVisuraDataGridBase_RowDataBound;
        }

        private void ListaVisuraDataGridBase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var dataItem = e.Row.DataItem as VisuraListItem;
                var uuid = dataItem.Uuid;
                var hlUsaComeModello = (HyperLink)e.Row.FindControl("hlUsaComeModello");

                if (hlUsaComeModello != null)
                {
                    hlUsaComeModello.NavigateUrl = $"~/copia-da-istanza-presentata/{IdComune}/{Software}/{uuid}";
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                FooterRow.TableSection = TableRowSection.TableFooter;
            }

            base.OnPreRender(e);
        }

    }
}
