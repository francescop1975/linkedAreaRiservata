using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;

namespace Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2.Builders
{
    internal class ParametriPresentazioneDomandaBuilder : AreaRiservataWsConfigBuilder, IConfigurazioneBuilder<ParametriPresentazioneDomanda>
    {
        protected ParametriPresentazioneDomandaBuilder(IAliasSoftwareResolver aliasResolver, IConfigurazioneAreaRiservataRepository configurazioneAreaRiservataRepository) : base(aliasResolver, configurazioneAreaRiservataRepository)
        {

        }

        public ParametriPresentazioneDomanda Build()
        {
            var cfg = GetConfig();

            var richiedentePf = cfg.RichiedenteSoloPersonaFisica;
            var abilitaTemplateDomanda = cfg.AbilitaTemplateDomanda;

            return new ParametriPresentazioneDomanda(richiedentePf, abilitaTemplateDomanda);
        }
    }
}
