using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.CercaPratiche
{
    public class DatiAnnullamento
    {
        [JsonProperty(PropertyName = "annullata")]
        public bool Annullata { get; set; }

        [JsonProperty(PropertyName = "motivoAnnullamento")]
        public string MotivoAnnullamento { get; set; }

        [JsonProperty(PropertyName = "dataAnnullamento")]
        public DateTime? DataAnnullamento { get; set; }
    }
}
