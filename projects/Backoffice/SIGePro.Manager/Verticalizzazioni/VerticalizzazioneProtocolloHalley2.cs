using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Verticalizzazioni
{
    public class VerticalizzazioneProtocolloHalley2: Verticalizzazione
    {
        private const string NOME_VERTICALIZZAZIONE = "PROTOCOLLO_HALLEY2";

        public VerticalizzazioneProtocolloHalley2(string idComuneAlias, string software, string codiceComune) : base(idComuneAlias, NOME_VERTICALIZZAZIONE, software, codiceComune) { }

        public string Url
        {
            get { return GetString("URL"); }
            set { SetString("URL", value); }
        }

        public string CasellaEmail
        {
            get { return GetString("CASELLAEMAIL"); }
            set { SetString("CASELLAEMAIL", value); }
        }

        public string UserName
        {
            get { return GetString("USERNAME"); }
            set { SetString("USERNAME", value); }
        }

        public string Password
        {
            get { return GetString("PASSWORD"); }
            set { SetString("PASSWORD", value); }
        }
    }
}
