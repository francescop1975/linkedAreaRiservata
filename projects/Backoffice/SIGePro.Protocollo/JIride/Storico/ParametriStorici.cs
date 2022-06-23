using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Storico
{
    public class ParametriStorici
    {
        public bool IsCopia { get; internal set; }
        public ProtocolloLogs Logger { get; internal set; }
        public ProtocolloSerializer Serializer { get; internal set; }
        public short AnnoProtocollo { get; internal set; }
        public int? IdProtocollo { get; internal set; }
        public int NumeroProtocollo { get; internal set; }
        public string Operatore { get; internal set; }
        public string Ruolo { get; internal set; }
        public string CodiceAmministrazione { get; internal set; }
        public string CodiceAOO { get; internal set; }
        public string Url { get; internal set; }
    }
}
