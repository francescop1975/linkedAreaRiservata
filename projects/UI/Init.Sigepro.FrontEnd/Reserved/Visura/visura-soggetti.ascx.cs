﻿using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using System;
using System.Collections.Generic;

namespace Init.Sigepro.FrontEnd.Reserved.Visura
{
    public partial class visura_soggetti : System.Web.UI.UserControl
    {
        public class VisuraSoggettiListItem
        {
            public string Nominativo { get; set; }
            public string InQualitaDi { get; set; }
            public string NominativoCollegato { get; set; }
            public string Procuratore { get; set; }

            public VisuraSoggettiListItem()
            {

            }

            public VisuraSoggettiListItem(Anagrafe soggetto, TipiSoggetto tipoSoggetto, Anagrafe anagrafeCollegata = null, Anagrafe procuratore = null)
            {
                Nominativo = soggetto.NOMINATIVO + " " + soggetto.NOME;

                if (tipoSoggetto != null)
                    InQualitaDi = tipoSoggetto.TIPOSOGGETTO;

                if (anagrafeCollegata != null)
                    NominativoCollegato = anagrafeCollegata.NOMINATIVO + " " + anagrafeCollegata.NOME;

                if (procuratore != null)
                    Procuratore = procuratore.NOMINATIVO + " " + procuratore.NOME;
            }
        }

        public IEnumerable<VisuraSoggettiListItem> DataSource;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void DataBind()
        {
            this.dgSoggetti.DataSource = this.DataSource;
            this.dgSoggetti.DataBind();
        }
    }
}