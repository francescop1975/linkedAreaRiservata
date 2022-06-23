using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Init.SIGePro.Authentication;

namespace Init.SIGePro.Manager.Authentication
{
    public class HttpContextAuthenticationInfoResolver : IAuthenticationInfoResolver
    {
        public static class Constants
        {
            public const string ContextKeyName = "HttpContextAuthenticationInfoResolver:CurrentRequestAuthenticationInfo";
        }

        public AuthenticationInfo Resolve()
        {
            var authInfo = (AuthenticationInfo)HttpContext.Current.Items[Constants.ContextKeyName];

            return authInfo;
        }

        public static void SetAuthenticationInfo(AuthenticationInfo authenticationInfo)
        {
            HttpContext.Current.Items[Constants.ContextKeyName] = authenticationInfo;
        }
    }
}
