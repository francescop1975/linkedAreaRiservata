using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2.Builders
{
    internal class ParametriQuestionarioBuilder : AreaRiservataWsConfigBuilder, IConfigurazioneBuilder<ParametriQuestionarioSoddisfazione>
    {
        protected ParametriQuestionarioBuilder(IAliasSoftwareResolver aliasResolver, IConfigurazioneAreaRiservataRepository configurazioneAreaRiservataRepository) 
            : base(aliasResolver, configurazioneAreaRiservataRepository)
        {
        }

        public ParametriQuestionarioSoddisfazione Build()
        {
            var cfg = GetConfig();

            return new ParametriQuestionarioSoddisfazione(cfg.QuestionarioSoddisfazione.Attivo);
        }
    }
}
