using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class JsonTokenSoftwareHeader
    {
        [JsonProperty("token")]
        public string Token { get; set;  }

        [JsonProperty("software")]
        public string Software { get; set; }

        public JsonTokenSoftwareHeader(string token, string software)
        {
            this.Token = token;
            this.Software = software;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
