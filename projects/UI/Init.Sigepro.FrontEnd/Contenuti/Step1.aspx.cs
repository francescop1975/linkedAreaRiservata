﻿using System;

namespace Init.Sigepro.FrontEnd.Contenuti
{
    public partial class Step1 : ContenutiBasePage
    {
        public Step1()
        {
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.StepId = 1;
            this.Master.MostraHelp = true;
        }

        public void alberoAteco_FogliaSelezionata(object sender, int idAteco)
        {
            Response.Redirect("~/Contenuti/Step2.aspx?alias=" + AliasComune + "&Software=" + Software + "&IdAteco=" + idAteco);
        }
    }
}
