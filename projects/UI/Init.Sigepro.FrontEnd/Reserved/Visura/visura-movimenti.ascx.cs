using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved.Visura
{
    public partial class visura_movimenti : System.Web.UI.UserControl
    {
        public class VisuraMovimentiAllegato
        {
            public string Descrizione { get; set; }
            public string NomeFile { get; set; }
            public string UrlDownload { get; set; }
        }

        public class VisuraMovimentiListItem
        {
            public int Id { get; set; }
            public int CodiceIstanza { get; set; }
            public int NumeroAllegati => this.Allegati?.Count() ?? 0;
            public bool HaAllegati => this.NumeroAllegati > 0;
            public string Descrizione { get; set; }
            public DateTime? Data { get; set; }
            public string Parere { get; set; }
            public string NumeroProtocollo { get; set; }
            public DateTime? DataProtocollo { get; set; }
            public string UuidPraticaCollegata { get; set; }
            public bool HaPraticaCollegata { get { return !String.IsNullOrEmpty(this.UuidPraticaCollegata); } }
            public IEnumerable<VisuraMovimentiAllegato> Allegati { get; set; } = Enumerable.Empty<VisuraMovimentiAllegato>();
        }


        public IEnumerable<VisuraMovimentiListItem> DataSource { get; set; }

        public bool MostraPraticheCollegate
        {
            get { object o = this.ViewState["MostraPraticheCollegate"]; return o == null ? true : (bool)o; }
            set { this.ViewState["MostraPraticheCollegate"] = value; }
        }


        public string UrlAllegati
        {
            get
            {
                var pagina = this.Page as ReservedBasePage;
                var url = UrlBuilder.Url("~/Reserved/ListaAllegatiMovimento.aspx", qs =>
                {
                    qs.Add(new QsAliasComune(pagina.IdComune));
                    qs.Add(new QsSoftware(pagina.Software));
                });

                return ResolveUrl(url);
            }
        }

        public bool PermettiDownload
        {
            get { var o = this.ViewState["PermettiDownload"]; return o == null ? true : (bool)o; }
            set { this.ViewState["PermettiDownload"] = value; }
        }
        public string UrlPopupVisura
        {
            get
            {
                var pagina = this.Page as ReservedBasePage;

                var url = UrlBuilder.Url("~/Reserved/sub-visura.aspx", qs =>
                {
                    qs.Add(new QsAliasComune(pagina.IdComune));
                    qs.Add(new QsSoftware(pagina.Software));
                });

                return ResolveClientUrl(url);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void DataBind()
        {
            dgMovimenti.DataSource = this.DataSource;
            dgMovimenti.DataBind();


            dgMovimenti.Columns[dgMovimenti.Columns.Count - 1].Visible = this.MostraPraticheCollegate;
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.dgMovimenti.Columns[this.dgMovimenti.Columns.Count - 2].Visible = PermettiDownload;
        }

        protected void dgMovimenti_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var dataItem = (VisuraMovimentiListItem)e.Row.DataItem;
                var rptListaAllegati = (Repeater)e.Row.FindControl("rptListaAllegati");

                rptListaAllegati.DataSource = dataItem.Allegati;
                rptListaAllegati.DataBind();

            }
        }
    }
}