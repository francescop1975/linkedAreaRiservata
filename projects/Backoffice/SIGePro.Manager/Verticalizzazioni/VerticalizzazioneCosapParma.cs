using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Verticalizzazioni
{
    public class VerticalizzazioneCosapParma : Verticalizzazione
    {
        public static class Constants
        {
            public const string NomeVerticalizzazione = "COSAP_PARMA";
            public const string WebServiceUrl = "WEB_SERVICE_URL";
            public const string UtenteInserimento = "UTENTE_INSERIMENTO";
            public const string Fonte = "FONTE";
            public const string Anno = "ANNO";
            public const string User = "WS_USER";
            public const string Password = "WS_PASSWORD";

        }

        public VerticalizzazioneCosapParma()
        {
        }

        public VerticalizzazioneCosapParma(string idComuneAlias, string software) : base(idComuneAlias, Constants.NomeVerticalizzazione, software) { }

        public string WebServiceUrl => GetString(Constants.WebServiceUrl);
        public string UtenteInserimento => GetString(Constants.UtenteInserimento);
        public string Fonte => GetString(Constants.Fonte);
        public string User => GetString(Constants.User);
        public string Password => GetString(Constants.Password);
        public int Anno => GetInt(Constants.Anno).GetValueOrDefault(DateTime.Now.Year);
    }
}
