using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.AutenticazioneUtente;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Ninject;
using System;
using System.Web.UI;

namespace Init.Sigepro.FrontEnd.servizi
{
    public partial class PresentaDomandaLocale : System.Web.UI.Page
    {
        [Inject]
        protected VbgAuthenticationService _authenticationService { get; set; }
        [Inject]
        protected ITokenResolver _tokenResolver { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var alias = Page.RouteData.Values["alias"].ToString();
            var software = Page.RouteData.Values["software"].ToString();
            var codiceIntervento = Page.RouteData.Values["codiceIntervento"].ToString();

            var url = UrlBuilder.Url("~/Reserved/InserimentoIstanza/Benvenuto.aspx", x =>
            {
                x.Add(new QsAliasComune(alias));
                x.Add(new QsSoftware(software));
                x.Add(new QsSelezionaIntervento(codiceIntervento));
            });

            Response.Redirect(url);
        }
    }
}