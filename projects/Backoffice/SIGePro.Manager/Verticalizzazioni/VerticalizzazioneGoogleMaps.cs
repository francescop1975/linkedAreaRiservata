using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Verticalizzazioni
{
    public partial class VerticalizzazioneGoogleMaps : Verticalizzazione
    {
        private static class Constants
        {
            public const string ApiKey = "API_KEY";
            public const string MapBounds = "MAP_BOUNDS";
        }

        private const string NOME_VERTICALIZZAZIONE = "GOOGLE_MAPS";

        public VerticalizzazioneGoogleMaps()
        {

        }

        public VerticalizzazioneGoogleMaps(string idComuneAlias, string software) : base(idComuneAlias, NOME_VERTICALIZZAZIONE, software) { }

        public string ApiKey => GetString(Constants.ApiKey);
        public string MapBounds => GetString(Constants.MapBounds);
    }
}
