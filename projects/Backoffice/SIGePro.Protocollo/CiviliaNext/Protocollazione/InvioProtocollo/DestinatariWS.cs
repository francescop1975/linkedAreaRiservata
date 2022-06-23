using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.Protocollazione.InvioProtocollo
{
    public class DestinatariWS
    {
        [JsonProperty(PropertyName = "indirizzo")]
        public string Indirizzo { get; set; }
    }
}
