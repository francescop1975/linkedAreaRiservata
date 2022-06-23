using Init.Sigepro.FrontEnd.AppLogic.Services.Navigation;
using Ninject;
using System;
using System.Web;

namespace Init.Sigepro.FrontEnd
{


    public partial class Login : BasePage
    {
        [Inject]
        public RedirectService _redirectService { get; set; }


        protected string ReturnTo
        {
            get
            {
                string returnTo = Request.QueryString["ReturnTo"];
                return returnTo;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ReturnTo))
                Context.Response.Cookies.Add(new HttpCookie("ReturnTo", ReturnTo));

            this._redirectService.RedirectToHomeAreaRiservata(Server.UrlEncode(ReturnTo));
        }
    }
}