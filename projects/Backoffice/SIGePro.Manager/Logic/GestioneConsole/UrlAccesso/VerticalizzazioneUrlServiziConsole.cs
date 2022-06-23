using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneConsole.UrlAccesso
{
    public class VerticalizzazioneUrlServiziConsole : Verticalizzazione
    {
        private static class Constants
        {
            public const string NomeVertivcalizzazione = "AR_URL_SERVIZI_CONSOLE";
            public const string CrossLoginUrl = "CROSS_LOGIN_URL";
            public const string UrlIstanzeInSospeso = "URL_ISTANZE_IN_SOSPESO";
            public const string UrlNuovaDomanda = "URL_NUOVA_DOMANDA";
        }

        public VerticalizzazioneUrlServiziConsole(string idComuneAlias, string software, string codiceComune) : base(idComuneAlias, Constants.NomeVertivcalizzazione, software, codiceComune)
        {
        }

        public string CrossLoginUrl => GetString(Constants.CrossLoginUrl);

        public string UrlIstanzeInSospeso => GetString(Constants.UrlIstanzeInSospeso);

        public string UrlNuovaDomanda => GetString(Constants.UrlNuovaDomanda);
    }
}
