using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.CercaPratiche
{
    public class CercaPraticheRequest
    {
        [JsonProperty(PropertyName = "idPratica")]
        public long IdPratica { get; internal set; }

        [JsonProperty(PropertyName = "pagina")]
        public int Pagina { get; internal set; }

        [JsonProperty(PropertyName = "codiceLivelloOraganigramma")]
        public string CodiceLivelloOrganigramma { get; internal set; }

        [JsonProperty(PropertyName = "idOperatore")]
        public long IdOperatore { get; internal set; }
    }
}
