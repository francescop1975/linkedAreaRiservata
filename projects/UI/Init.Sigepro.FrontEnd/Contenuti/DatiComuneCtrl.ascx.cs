using Init.Sigepro.FrontEnd.AppLogic.GestioneContenuti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneContenuti.Configurazione;
using Init.Sigepro.FrontEnd.Infrastructure.IOC;
using Ninject;
using System;
using System.Linq;
//using Init.Sigepro.FrontEnd.AppLogic.Validation;

namespace Init.Sigepro.FrontEnd.Contenuti
{
    public partial class DatiComuneCtrl : System.Web.UI.UserControl
    {
        [Inject]
        protected ConfigurazioneContenuti _configurazione { get; set; }

        protected BoxDatiComune DataSource { get; set; }

        public string AliasComune
        {
            get
            {
                return Request.QueryString["alias"];
            }
        }

        public string Software
        {
            get
            {
                var sw = Request.QueryString["software"];

                if (String.IsNullOrEmpty(sw))
                    return "SS";
                return sw;
            }
        }

        public DatiComuneCtrl()
        {
            FoKernelContainer.Inject(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                DataBind();
        }

        public override void DataBind()
        {
            this.DataSource = this._configurazione.DatiComune;

            this.mvSottotitoloComune.ActiveViewIndex = GetIndiceViewAttivo();

            base.DataBind();
        }

        private int GetIndiceViewAttivo()
        {
            if (DataSource.CodiciAccreditamento.Count() == 0)
                return 0;

            if (DataSource.CodiciAccreditamento.Count() == 1)
                return 1;

            return 2;
        }

    }
}