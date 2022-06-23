using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2.Builders;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.Configurazione
{
    internal class ParametriGenerazioneCertificatoDiInvioBuilder : AreaRiservataWsConfigBuilder, IConfigurazioneBuilder<ParametriGenerazioneCertificatoInvio>
    {
        public ParametriGenerazioneCertificatoDiInvioBuilder(IAliasSoftwareResolver aliasResolver, IConfigurazioneAreaRiservataRepository configurazioneAreaRiservataRepository)
            : base(aliasResolver, configurazioneAreaRiservataRepository)
        {
        }

        public ParametriGenerazioneCertificatoInvio Build()
        {
            var cfg = GetConfig();

            return new ParametriGenerazioneCertificatoInvio(cfg.GenerazioneCertificatoDiInvio.NomeFile, cfg.GenerazioneCertificatoDiInvio.DescrizioneFile);
        }
    }
}
