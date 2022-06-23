using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2.Builders
{
    public class ParametriSITBuilder : IConfigurazioneBuilder<ParametriSIT>
    {
        private readonly IConfigurazione<ParametriSigeproSecurity> _securityConfig;
        private readonly IConfigurazioneAreaRiservataRepository _configurazioneAreaRiservataRepository;
        private readonly IAliasSoftwareResolver _aliasResolver;

        public ParametriSITBuilder(IConfigurazione<ParametriSigeproSecurity> securityConfig, IConfigurazioneAreaRiservataRepository configurazioneAreaRiservataRepository, IAliasSoftwareResolver aliasResolver)
        {
            _securityConfig = securityConfig;
            _configurazioneAreaRiservataRepository = configurazioneAreaRiservataRepository;
            _aliasResolver = aliasResolver;
        }

        public ParametriSIT Build()
        {

            var cfg = this._configurazioneAreaRiservataRepository.DatiConfigurazione(this._aliasResolver.AliasComune, this._aliasResolver.Software);

            var sitAttivo = cfg.ConfigurazioneSit?.Attivo ?? false;
            var sitUrl = cfg.ConfigurazioneSit?.UrlWsSit ?? "";
            var forzaStepLocalizzazioniSit = sitAttivo ? cfg.ConfigurazioneSit.ForzaStepLocalizzazioniSit : false;
            var aspNetBaseUrl = this._securityConfig.Parametri.AspNetBaseUrl;

            return new ParametriSIT(sitAttivo, sitUrl, forzaStepLocalizzazioniSit, aspNetBaseUrl);
        }
    }
}
