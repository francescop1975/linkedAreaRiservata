using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.RicercaLivello
{
    public class Dipendente
    {
        [JsonProperty(PropertyName = "IdIndividuo")]
        public long IdIndividuo { get; set; }

        [JsonProperty(PropertyName = "Account")]
        public string Account { get; set; }
    }
}
