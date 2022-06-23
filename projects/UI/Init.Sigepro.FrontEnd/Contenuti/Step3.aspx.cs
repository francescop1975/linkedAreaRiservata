﻿using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using Init.SIGePro.Manager.DTO.Common;
using Ninject;
using System;
using System.Linq;
//using Init.Sigepro.FrontEnd.AppLogic.Validation;

namespace Init.Sigepro.FrontEnd.Contenuti
{
    public partial class Step3 : ContenutiBasePage
    {
        [Inject]
        protected IInterventiAllegatiRepository _interventiAllegatiRepository { get; set; }

        //		[RegExValidate("^[0-9]{1,10}$")]
        public int Id
        {
            get { return Convert.ToInt32(Request.QueryString["Id"]); }
        }

        public Step3()
        {
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.StepId = 3;

            if (!IsPostBack)
                DataBind();
        }

        public string GetUrlStampaPagina()
        {
            return GetBaseUrlAssoluto() + "Public/MostraDettagliIntervento.aspx?idComune=" + IdComune + "&Id=" + Id + "&Print=true";
        }

        public string GetUrlDownloadPagina()
        {
            var downloadUrl = GetUrlStampaPagina();

            return ResolveClientUrl("~/Public/DownloadPage.ashx") + "?IdComune=" + IdComune + "&url=" + Server.UrlEncode(downloadUrl);
        }

        public string GetUrlEndoAttivabili()
        {
            return ResolveClientUrl("~/Public/ListaEndoAttivabili.aspx") + "?IdComune=" + IdComune + "&intervento=" + Id + "&fromAreaRiservata=false";
        }

        public override void DataBind()
        {
            hlVisualizzaModello.Visible = false;
            //imgVisualizzaModello.Visible = false;

            var documentiIntervento = _interventiAllegatiRepository.GetAllegatiDaIdintervento(Id, AmbitoRicerca.AreaRiservata);

            var riepilogoDomanda = documentiIntervento.Where(x => x.RiepilogoDomanda).FirstOrDefault();

            if (riepilogoDomanda != null)
            {
                hlVisualizzaModello.Visible = true;
                //imgVisualizzaModello.Visible = true;
                hlVisualizzaModello.NavigateUrl = ResolveClientUrl("~/Public/ModelloDomanda/Visualizza.aspx") + "?idComune=" + IdComune + "&Intervento=" + Id + "&Software=" + this.Software;
                //imgVisualizzaModello.ImageUrl = ResolveClientUrl("~/images/contenuti/visualizza-modello.jpg");
            }
        }
    }
}
