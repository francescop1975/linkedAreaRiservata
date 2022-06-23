using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Fascicolazione
{
    public class FascicoloNuovoRequest
    {
        public FascicoloInXml Request { get; internal set; }
        public string Url { get; internal set; }
        public string CodiceAmministrazione { get; internal set; }
        public string Aoo { get; internal set; }
    }
}
