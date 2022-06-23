using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.CommissioniWs;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.WebService
{
    public class CommissioniWsProxyCreator : ServiceCreatorBase<WsCommissioniClient>
    {
        public CommissioniWsProxyCreator(IConfigurazione<ParametriSigeproSecurity> cfg, ITokenApplicazioneService tokenApplicazioneService, IAliasResolver aliasResolver)
            : base(cfg, tokenApplicazioneService, aliasResolver)
        {

        }

        protected override WsCommissioniClient CreateClient(EndpointAddress address, BasicHttpBinding binding)
        {
            return new WsCommissioniClient(binding, address);
        }

        protected override string GetEndpointUrl(ParametriSigeproSecurity config)
        {
            return config.UrlServizioCommissioni;
        }
    }
}
