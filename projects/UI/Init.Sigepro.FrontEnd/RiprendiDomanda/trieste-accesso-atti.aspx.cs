using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.AreaRiservata;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.PresentazioneIstanze.Workflow;
using Init.Sigepro.FrontEnd.AppLogic.ReadInterface.Imp;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Ninject;
using System;
using System.Web;
using System.Web.UI;

namespace Init.Sigepro.FrontEnd.RiprendiDomanda
{
    public partial class trieste_accesso_atti : System.Web.UI.Page
    {
        [Inject]
        protected WorkflowFromCodiceIntervento _workflowService { get; set; }

        [Inject]
        protected ISalvataggioDomandaStrategy _salvataggioStrategy { get; set; }
        [Inject]
        protected IAreaRiservataAuthenticationService _areaRiservataAuthenticationService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var serializationCode = Page.RouteData.Values["idDomanda"].ToString();
            var token = (Page.RouteData.Values["token"] ?? String.Empty).ToString();
            var dataKey = PresentazioneIstanzaDataKey.FromSerializationCode(serializationCode);

            HttpContext.Current.Items["alias"] = dataKey.IdComune;
            HttpContext.Current.Items["software"] = dataKey.Software;

            var domanda = this._salvataggioStrategy.GetById(dataKey.IdPresentazione);

            this._workflowService.ResetReadDatiDomanda(new StaticReadDatiDomanda(domanda.ReadInterface, dataKey));

            var stepId = _workflowService.GetCurrentWorkflow().TrovaIndiceStepDaUrlParziale("triesteaccessoatti.aspx");

            var url = UrlBuilder.Url("~/reserved/inserimentoistanza/triesteaccessoatti.aspx", pars =>
            {
                pars.Add("idComune", dataKey.IdComune);
                pars.Add("software", dataKey.Software);
                pars.Add("idPresentazione", dataKey.IdPresentazione);
                pars.Add("returning", "1");
                pars.Add("stepId", stepId);
            });

            this._areaRiservataAuthenticationService.AuthenticateUser(token);

            Response.Redirect(url);
        }
    }
}