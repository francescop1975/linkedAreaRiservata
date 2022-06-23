using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Storico
{
    public class ParametriStoriciRequest
    {
        public string Token { get; internal set; }
        public string IdComuneAlias { get; internal set; }
        public string Software { get; internal set; }
        public string CodiceComune { get; internal set; }
        public string IdProtocollo { get; internal set; }
        public short AnnoProtocollo { get; internal set; }
        public int NumeroProtocollo { get; internal set; }
        public ProtocolloLogs Logger { get; internal set; }
        public ProtocolloSerializer Serializer { get; internal set; }
    }
}
