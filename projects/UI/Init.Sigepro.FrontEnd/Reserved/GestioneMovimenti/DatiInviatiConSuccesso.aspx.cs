using System;

namespace Init.Sigepro.FrontEnd.Reserved.GestioneMovimenti
{
    public partial class DatiInviatiConSuccesso : ReservedBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdChiudi_Click(object sender, EventArgs e)
        {
            Redirect("~/reserved/benvenuto.aspx", x => { });
        }
    }
}