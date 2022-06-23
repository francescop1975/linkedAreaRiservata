using Init.SIGePro.Manager.Authentication;
using Init.SIGePro.Manager.Verticalizzazioni;
using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneIntegrazioneLDP
{
    public class VerticalizzazioneSitLdpAttiva : IVerticalizzazioneAttiva<VerticalizzazioneSitLdp>
    {
        private readonly IAuthenticationInfoResolver _authInfoResolver;

        public VerticalizzazioneSitLdpAttiva(IAuthenticationInfoResolver authInfoResolver)
        {
            this._authInfoResolver = authInfoResolver;
        }

        public bool IsAttiva(string software)
        {
            var authenticationInfo = this._authInfoResolver.Resolve();

            var s1 = new VerticalizzazioneSitAttivo(authenticationInfo.Alias, software);

            if (!s1.Attiva)
            {
                return false;
            }

            return new VerticalizzazioneSitLdp(authenticationInfo.Alias, software).Attiva;
        }
    }
}
