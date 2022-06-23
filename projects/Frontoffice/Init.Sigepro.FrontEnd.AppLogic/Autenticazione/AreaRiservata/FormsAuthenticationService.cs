using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Init.Sigepro.FrontEnd.AppLogic.Autenticazione.AreaRiservata
{
    public class FormsAuthenticationService : IAreaRiservataAuthenticationService, ITokenResolver
    {
        private readonly IResolveHttpContext _httpContext;

        public string Token => GetCurrentUserIdentity();

        public FormsAuthenticationService(IResolveHttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public void AuthenticateUser(string token)
        {
            FormsAuthentication.SetAuthCookie(token, false);
        }

        public string GetCurrentUserIdentity()
        {
            if (this._httpContext.Get.User == null)
            {
                return String.Empty;
            }

            return this._httpContext.Get.User.Identity.Name;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
