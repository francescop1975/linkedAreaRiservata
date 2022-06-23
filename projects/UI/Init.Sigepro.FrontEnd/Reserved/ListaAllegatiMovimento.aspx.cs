﻿using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Init.Sigepro.FrontEnd.Reserved
{
    public partial class ListaAllegatiMovimento : Ninject.Web.PageBase
    {
        [Inject]
        public IVisuraService _visuraService { get; set; }
        [Inject]
        public IUrlDownloadOggettiService _urlDownloadOggettiService { get; set; }

        string IdComune
        {
            get
            {
                return Request.QueryString["IdComune"].ToString();
            }
        }

        int Movimento
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["Movimento"]);
            }
        }

        int Istanza
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["Istanza"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                DataBind();
        }

        public override void DataBind()
        {
            var istanza = _visuraService.GetById(Istanza, new VisuraIstanzaFlags { LeggiDatiConfigurazione = false });

            for (int i = 0; i < istanza.Movimenti.Length; i++)
            {
                var movimento = istanza.Movimenti[i];

                if (Convert.ToInt32(movimento.CODICEMOVIMENTO) == Movimento)
                {
                    var ds = ElaboraListaAllegati(movimento.MovimentiAllegati);

                    rptListaAllegati.DataSource = ds;
                    rptListaAllegati.DataBind();
                }
            }
        }

        private IEnumerable<MovimentiAllegati> ElaboraListaAllegati(MovimentiAllegati[] movimentiAllegati)
        {
            foreach (var allegato in movimentiAllegati)
            {
                if (allegato.FlagPubblica.GetValueOrDefault(0) == 0)
                    continue;

                yield return allegato;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            pnlContenuti.RenderControl(writer);
        }

        protected string GeneraUrlDownload(object objDataItem)
        {
            var allegato = (MovimentiAllegati)objDataItem;

            if (String.IsNullOrEmpty(allegato.CODICEOGGETTO))
            {
                return String.Empty;
            }

            return _urlDownloadOggettiService.GetUrlDownload(Convert.ToInt32(allegato.CODICEOGGETTO));
        }
    }
}
