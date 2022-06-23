using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2
{
    public class ParametriGoogleMaps: IParametriConfigurazione
    {
        public readonly string ApiKey;
        public readonly string MapBounds;
        public readonly string NomeTagAltriDati = "PERCORSO-GOOGLE-MAPS";
        public readonly string DatiExtraKey = "gestione_percorsi_google_maps.datiExtra";

        internal ParametriGoogleMaps(string apiKey, string mapBounds)
        {
            this.ApiKey = apiKey;
            this.MapBounds = mapBounds;
        }
    }
}
