using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.CommissioniAccessoPINWs;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.AccessoPIN.WebService
{
    public class CommissioniAccessoPINServiceCreator : ServiceCreatorBase<WsCommissioniAccessoPINClient>
    {
        public CommissioniAccessoPINServiceCreator(IConfigurazione<ParametriSigeproSecurity> cfg, ITokenApplicazioneService tokenApplicazioneService, IAliasResolver aliasResolver)
            : base(cfg, tokenApplicazioneService, aliasResolver)
        {

        }

        protected override WsCommissioniAccessoPINClient CreateClient(EndpointAddress address, BasicHttpBinding binding)
        {
            return new WsCommissioniAccessoPINClient(binding, address);
        }

        protected override string GetEndpointUrl(ParametriSigeproSecurity config)
        {
            return config.UrlServizioPINCommissioni;
        }
    }
}
