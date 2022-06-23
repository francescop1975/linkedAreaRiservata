using Init.SIGePro.Protocollo.Data;
using System;
using System.Collections.Generic;

namespace Init.SIGePro.Protocollo.Halley2
{
    public class ProtocolloHalley2Request
    {
        public byte[] Segnatura { get; set; }
        public List<Halle2FileRequest> Allegati { get; set; }

    }
}