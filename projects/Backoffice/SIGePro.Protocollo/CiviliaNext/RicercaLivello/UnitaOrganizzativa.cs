using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.RicercaLivello
{
    public class UnitaOrganizzativa
    {
        [JsonProperty(PropertyName = "Codice")]
        public string Codice { get; set; }
    }
}
