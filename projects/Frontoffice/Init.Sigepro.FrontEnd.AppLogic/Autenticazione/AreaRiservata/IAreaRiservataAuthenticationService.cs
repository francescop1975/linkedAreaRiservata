using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Autenticazione.AreaRiservata
{
    public interface IAreaRiservataAuthenticationService
    {
        void AuthenticateUser(string token);

        void SignOut();

        string GetCurrentUserIdentity();
    }
}
