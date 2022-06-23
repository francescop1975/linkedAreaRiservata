using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.WsEndoprocedimenti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti
{
    internal class EndoprocedimentiServiceCreator : ServiceCreatorBase<WsEndoprocedimentiClient>
    {
        public EndoprocedimentiServiceCreator(IConfigurazione<ParametriSigeproSecurity> cfg, ITokenApplicazioneService tokenApplicazioneService, IAliasResolver aliasResolver) : base(cfg, tokenApplicazioneService, aliasResolver)
        {
        }

        protected override WsEndoprocedimentiClient CreateClient(EndpointAddress address, BasicHttpBinding binding)
        {
            return new WsEndoprocedimentiClient(binding, address);
        }

        protected override string GetEndpointUrl(ParametriSigeproSecurity config)
        {
            return config.UrlWsEndoprocedimenti;
        }
    }
}
