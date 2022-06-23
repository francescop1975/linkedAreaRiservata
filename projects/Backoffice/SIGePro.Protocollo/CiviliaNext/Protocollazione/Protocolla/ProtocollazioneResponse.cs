using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.Protocollazione.Protocolla
{

    public class ProtocollazioneResponse
    {
        [JsonProperty(PropertyName = "resultType")]
        public int ResultType { get; set; }

        [JsonProperty(PropertyName = "resultDescription")]
        public string ResultDescription { get; set; }

        [JsonProperty(PropertyName = "result")]
        public Pratica Result { get; set; }

        [JsonProperty(PropertyName = "totalCount")]
        public int TotalCount { get; set; }

    }
}
