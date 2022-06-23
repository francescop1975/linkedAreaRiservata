using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.WsVbgDatiDinamici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici
{
    public class WsDatiDinamiciServiceCreator : ServiceCreatorBase<WsDatiDinamiciClient>
    {
        public WsDatiDinamiciServiceCreator(IConfigurazione<ParametriSigeproSecurity> cfg, ITokenApplicazioneService tokenApplicazioneService, IAliasResolver aliasResolver) : base(cfg, tokenApplicazioneService, aliasResolver)
        {
        }

        protected override WsDatiDinamiciClient CreateClient(EndpointAddress address, BasicHttpBinding binding)
        {
            return new WsDatiDinamiciClient(binding, address);
        }

        protected override string GetEndpointUrl(ParametriSigeproSecurity config)
        {
            return $"{config.AspNetBaseUrl}/WebServices/WsAreaRiservata/WcfServices/DatiDinamici/WsDatiDinamici.svc";
        }
    }
}
