using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Verticalizzazioni
{
    public class VerticalizzazioneProtocolloCiviliaNext: Verticalizzazione
    {
        private static class Constants
        {
            public const string NOME_VERTICALIZZAZIONE = "PROTOCOLLO_CIVILIANEXT";
            public const string ClientID = "CLIENT_ID";
            public const string CliendSecret = "CLIENT_SECRET";
            public const string CodiceLivelloOrganigramma = "CODICELIVELLOORGANIGRAMMA";
            public const string IdCasellaEmail = "IDCASELLAEMAIL";
            public const string IdCodiceAOO = "IDCODICEAOO";
            public const string IdRegistro = "IDREGISTRO";
            public const string UrlOAuth2 = "URL_OAUTH";
            public const string UrlWsAssegnazione = "URL_WSASSEGNAZIONE";
            public const string UrlWSAggiungiAllegato = "URL_WSAGGIUNGIALLEGATO";
            public const string UrlWSAnnullaProtocollo = "URL_WSANNULLAPROTOCOLLO";
            public const string UrlWSCercaPratiche = "URL_WSCERCAPRATICHE";
            public const string UrlWSEstraiTitolario = "URL_WSESTRAITITOLARIO";
            public const string UrlWSGetAllegati = "URL_WSGETALLEGATI";
            public const string UrlWSGetAllegato = "URL_WSGETALLEGATO";
            public const string UrlWSInviaProtocollo = "URL_WSINVIAPROTOCOLLO";
            public const string UrlWSProtocollo = "URL_WSPROTOCOLLO";
            public const string UrlWSRicercaLivello = "URL_WSRICERCALIVELLO";
            public const string UrlWSResource = "URL_WSRESOURCE";
        }

        public VerticalizzazioneProtocolloCiviliaNext(string idComuneAlias, string software, string codiceComune) : base(idComuneAlias, Constants.NOME_VERTICALIZZAZIONE, software, codiceComune) { }

        public string ClientID => GetString(Constants.ClientID);

        public string CliendSecret => GetString(Constants.CliendSecret);

        public string CodiceLivelloOrganigramma => GetString(Constants.CodiceLivelloOrganigramma);

        public string IdCasellaEmail => GetString(Constants.IdCasellaEmail);

        public string IdCodiceAOO => GetString(Constants.IdCodiceAOO);

        public string IdRegistro => GetString(Constants.IdRegistro);

        public string UrlOAuth2 => GetString(Constants.UrlOAuth2);


        public string UrlWSAggiungiAllegato => GetString(Constants.UrlWSAggiungiAllegato);

        public string UrlWSAnnullaProtocollo => GetString(Constants.UrlWSAnnullaProtocollo);

        public string UrlWsAssegnazione => GetString(Constants.UrlWsAssegnazione);

        public string UrlWSCercaPratiche => GetString(Constants.UrlWSCercaPratiche);

        public string UrlWSEstraiTitolario => GetString(Constants.UrlWSEstraiTitolario);

        public string UrlWSGetAllegati => GetString(Constants.UrlWSGetAllegati);

        public string UrlWSGetAllegato => GetString(Constants.UrlWSGetAllegato);

        public string UrlWsInviaProtocollo => GetString(Constants.UrlWSInviaProtocollo);

        public string UrlWSProtocollo => GetString(Constants.UrlWSProtocollo);

        public string UrlWSResource => GetString(Constants.UrlWSResource);

        public string UrlWSRicercaLivello => GetString(Constants.UrlWSRicercaLivello);

        
    }
}
