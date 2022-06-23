
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GestioneDatiExtra;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePercorsiGoogleMaps
{


    public class PercorsiGoogleMapsService
    {
        private readonly ISalvataggioDomandaStrategy _salvataggioStrategy;
        private readonly IConfigurazione<ParametriGoogleMaps> _config;

        public PercorsiGoogleMapsService(ISalvataggioDomandaStrategy salvataggioStrategy, IDatiExtraService datiExtraService, IConfigurazione<ParametriGoogleMaps> config)
        {
            _salvataggioStrategy = salvataggioStrategy;
            _config = config;
        }

        public void SalvaDatiDomanda(int idDomanda, string datiBase64, DatoStepGoogleMaps[] steps, MappaturaCampiDinamiciGoogleMaps mappaturaCampiDinamici)
        {
            var domanda = this._salvataggioStrategy.GetById(idDomanda);

            domanda.WriteInterface.DatiExtra.Set(this._config.Parametri.DatiExtraKey, datiBase64);

            domanda.WriteInterface.DatiDinamici.EliminaValoriCampo(mappaturaCampiDinamici.IdCampoMetriPercorsi);
            domanda.WriteInterface.DatiDinamici.EliminaValoriCampo(mappaturaCampiDinamici.IdCampoNomeVia);

            for (int i = 0; i < steps.Length; i++)
            {
                var step = steps[i];
                domanda.WriteInterface.DatiDinamici.AggiornaOCrea(mappaturaCampiDinamici.IdCampoMetriPercorsi, 0, i, step.Distanza.ToString(), step.Distanza.ToString(), "");
                domanda.WriteInterface.DatiDinamici.AggiornaOCrea(mappaturaCampiDinamici.IdCampoNomeVia, 0, i, step.Descrizione, step.Descrizione, "");
            }

            this._salvataggioStrategy.Salva(domanda);
        }

        public string GetBase64Percorso(int idDomanda)
        {
            var domanda = this._salvataggioStrategy.GetById(idDomanda);

            return domanda.ReadInterface.DatiExtra.Get<string>(this._config.Parametri.DatiExtraKey);
        }
    }
}
