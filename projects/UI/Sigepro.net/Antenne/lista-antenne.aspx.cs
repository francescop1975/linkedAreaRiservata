using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneAntenneLucca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigepro.net.Antenne
{
    public partial class lista_antenne : System.Web.UI.Page
    {
        public string Alias => Request.QueryString["alias"].ToString();
        public string Software => Request.QueryString["software"].ToString();
        public string Id => Request.QueryString["id"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            var vert = new VerticalizzazioneAntenneLucca(this.Alias, this.Software);
            var authInfo = AuthenticationManager.GetTokenApplicativo(this.Alias);

            using (var db = authInfo.CreateDatabase())
            {
                var svc = new AntenneLuccaService(db, authInfo.IdComune, vert);

                var dataSource = svc.GetListaPraticheByIdAntenna(this.Id);

                this.rptPratiche.DataSource = dataSource;
                this.rptPratiche.DataBind();
            }
        }

        protected void rptPratiche_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var elementoListaAntenne = (elemento_lista_antenne)e.Item.FindControl("elementoListaAntenne");

                elementoListaAntenne.Pratica = (PraticaPerAntenna)e.Item.DataItem;
                elementoListaAntenne.DataBind();

            }
        }
    }
}