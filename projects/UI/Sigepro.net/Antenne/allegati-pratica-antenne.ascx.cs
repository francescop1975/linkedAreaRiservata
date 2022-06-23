using Init.SIGePro.Manager.Logic.GestioneAntenneLucca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigepro.net.Antenne
{
    public partial class allegati_pratica_antenne : System.Web.UI.UserControl
    {
        public IEnumerable<FilePratica> Allegati { get; set; }
        public string Titolo
        {
            get { object o = this.ViewState["Titolo"]; return o == null ? String.Empty : (string)o; }
            set { this.ViewState["Titolo"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void DataBind()
        {
            this.Visible = this.Allegati != null && this.Allegati.Any();
            this.rptAllegati.DataSource = this.Allegati;
            this.rptAllegati.DataBind();
        }
    }
}