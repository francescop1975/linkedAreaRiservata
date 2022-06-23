using Init.SIGePro.Authentication;
using Init.SIGePro.Data;
using Init.SIGePro.Manager.Authentication;
using Init.SIGePro.Manager.Logic.GestioneIntegrazioneLDP;
using Init.SIGePro.Verticalizzazioni;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.GestioneIntegrazioneLDP
{
    public class FakeAuthenticationInfoResolver : IAuthenticationInfoResolver
    {
        public AuthenticationInfo RisultatoResolve;

        public AuthenticationInfo Resolve()
        {
            return RisultatoResolve;
        }
    }
}
