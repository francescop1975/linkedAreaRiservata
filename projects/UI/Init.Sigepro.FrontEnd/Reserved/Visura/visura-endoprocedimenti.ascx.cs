using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using System;
using System.Collections.Generic;

namespace Init.Sigepro.FrontEnd.Reserved.Visura
{
    public partial class visura_endoprocedimenti : System.Web.UI.UserControl
    {
        public class VisuraEndoListItem
        {
            public int Id { get; set; }
            public int CodiceIstanza { get; set; }
            public int NumeroAllegati { get; set; }
            public string Endoprocedimento { get; set; }
            public bool HaAllegati { get { return NumeroAllegati > 0; } }
        }

        public string UrlAllegatiEndo
        {
            get
            {
                var page = this.Page as ReservedBasePage;
                var url = UrlBuilder.Url("~/Reserved/ListaAllegatiEndo.aspx", qs =>
                {
                    qs.Add(new QsAliasComune(page.IdComune));
                    qs.Add(new QsSoftware(page.Software));
                });

                return ResolveClientUrl(url);
            }
        }

        public bool PermettiDownload
        {
            get { var o = this.ViewState["PermettiDownload"]; return o == null ? true : (bool)o; }
            set { this.ViewState["PermettiDownload"] = value; }
        }

        public bool MostraDocumentiNonValidi
        {
            get { object o = this.ViewState["MostraDocumentiNonValidi"]; return o == null ? true : (bool)o; }
            set { this.ViewState["MostraDocumentiNonValidi"] = value; }
        }


        public IEnumerable<VisuraEndoListItem> DataSource { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void DataBind()
        {
            this.dgProcedimenti.DataSource = this.DataSource;
            this.dgProcedimenti.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.dgProcedimenti.Columns[this.dgProcedimenti.Columns.Count - 1].Visible = PermettiDownload;
        }
    }
}