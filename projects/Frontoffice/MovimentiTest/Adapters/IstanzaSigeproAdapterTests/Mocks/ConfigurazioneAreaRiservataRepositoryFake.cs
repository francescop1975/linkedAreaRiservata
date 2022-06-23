using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogicTests.Adapters.IstanzaSigeproAdapterTests.Mocks
{
    public class ConfigurazioneAreaRiservataRepositoryFake : IConfigurazioneAreaRiservataRepository
    {
        public ConfigurazioneAreaRiservataDto DatiConfigurazione(string idComune, string software)
        {
            return new ConfigurazioneAreaRiservataDto
            {
                StatoInizialeIstanza = "STATO_INIZIALE"
            };
        }
    }
}
