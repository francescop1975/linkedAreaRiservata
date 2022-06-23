using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.RicercaLivello
{
    public class DettagliOperatore
    {
        [JsonProperty(PropertyName = "TotalCount")]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = "Result")]
        public List<DettaglioOperatore> Dettagli { get; set; }
    }
}
