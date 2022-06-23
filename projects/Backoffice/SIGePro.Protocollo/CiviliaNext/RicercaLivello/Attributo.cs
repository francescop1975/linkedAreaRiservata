using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.RicercaLivello
{
    public class Attributo
    {
        [JsonProperty(PropertyName = "Codice")]
        public string Codice { get; set; }

        [JsonProperty(PropertyName = "Valore")]
        public string Valore { get; set; }
    }
}
