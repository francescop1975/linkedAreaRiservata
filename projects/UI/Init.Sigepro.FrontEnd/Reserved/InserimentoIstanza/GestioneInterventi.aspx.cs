﻿using System;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    public partial class GestioneInterventi : IstanzeStepPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            Master.IgnoraSalvataggioDati = true;

            base.OnInit(e);
        }

        public override bool CanEnterStep()
        {
            Response.Redirect("~/Reserved/InserimentoIstanza/GestioneInterventiAteco.aspx" + Request.QueryString);

            throw new Exception("Step non supportato, utilizzare GestioneEndoV2");
        }

    }
}
