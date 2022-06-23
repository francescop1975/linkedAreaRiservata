using Init.SIGePro.Manager.Authentication;
using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneIntegrazioneLDP
{
    public class LdpProxyServiceWrapperFactory : ILdpProxyServiceWrapperFactory
    {
        private readonly IAuthenticationInfoResolver _authenticationInfoResolver;

        public LdpProxyServiceWrapperFactory( IAuthenticationInfoResolver authenticationInfoResolver )
        {
            this._authenticationInfoResolver = authenticationInfoResolver;
        }

        public ILdpAnnullamentoServiceWrapper GetAnnullamentoService( string software )
        {

            var verticalizzazioneSitLdp = GetVerticalizzazione(software);
            var serviceUrl = verticalizzazioneSitLdp.UrlServizioDomande;
            var serviceUserName = verticalizzazioneSitLdp.Username;
            var servicePassword = verticalizzazioneSitLdp.Password;

            return new LdpProxyServiceWrapper( serviceUrl, serviceUserName, servicePassword);
        }

        protected VerticalizzazioneSitLdp GetVerticalizzazione(string software)
        {
            return new VerticalizzazioneSitLdp(this._authenticationInfoResolver.Resolve().Alias, software);
        }
    }
}
