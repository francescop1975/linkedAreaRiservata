using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Verticalizzazioni
{
    public class VerticalizzazioneProtocolloAuriga: Verticalizzazione
    {
        private const string NOME_VERTICALIZZAZIONE = "PROTOCOLLO_AURIGA";

        public VerticalizzazioneProtocolloAuriga()
        {

        }

        public VerticalizzazioneProtocolloAuriga(string idComuneAlias, string software, string codiceComune) : base(idComuneAlias, NOME_VERTICALIZZAZIONE, software, codiceComune)
        {

        }

        /// <summary>
        /// Endpoint del web service Auriga; nell'endpoint bisogna sostituire il servizio con {servicename} es: http://aurigatest.comune.genova.it:8080/AurigaBusiness/soap/{servicename}?wsdl
        /// </summary>
        public string Url
        {
            get { return GetString("URL"); }
            set { SetString("URL", value); }
        }

        /// <summary>
        /// End point del web service Java che fa da proxy verso Auriga
        /// </summary>
        public string ProxyUrl
        {
            get { return GetString("PROXY_URL"); }
            set { SetString("PROXY_URL", value); }
        }


        /// <summary>
        /// Username relativo alle credenziali per autenticarsi al web service, da non confondere con gli utenti del protocollo.
        /// </summary>
        public string Username
        {
            get { return GetString("USERNAME"); }
            set { SetString("USERNAME", value); }

        }

        /// <summary>
        /// Password relativa alle credenziali per autenticarsi al web service, da non confondere con gli utenti del protocollo.
        /// </summary>
        public string Password
        {
            get { return GetString("PASSWORD"); }
            set { SetString("PASSWORD", value); }
        }

        /// <summary>
        /// Codice identificativo dell’applicazione che chiama il WS (obbligatorio): se la chiamata è dall’interno dello stesso catalogo servizi va valorizzata come “AURIGA”
        /// </summary>
        public string CodApplicazione
        {
            get { return GetString("CODAPPLICAZIONE"); }
            set { SetString("CODAPPLICAZIONE", value); }
        }

        /// <summary>
        /// Codice identificativo dell’istanza dell’applicazione esterna che chiama il WS (se applicazione multi-istanza)
        /// </summary>
        public string IstanzaApplicazione
        {
            get { return GetString("ISTANZAAPPLICAZIONE"); }
            set { SetString("ISTANZAAPPLICAZIONE", value); }
        }
    }
}
