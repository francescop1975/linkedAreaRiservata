using Init.SIGePro.Protocollo.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.Iride.Configuration;
using Init.SIGePro.Protocollo.Serialize;
using Init.SIGePro.Protocollo.JIride.Protocollazione.LeggiProtocollo;

namespace Init.SIGePro.Protocollo.JIride.Protocollazione.CreaCopie
{
    public class CreaCopieInfo
    {
        public CreaCopieInXml Request { get; internal set; }
        public string CodiceAmministrazione { get; internal set; }
        public string AOO { get; internal set; }
        public string Url { get; internal set; }
    }
}
