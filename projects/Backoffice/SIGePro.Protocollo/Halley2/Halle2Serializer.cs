using Ikriv.Xml;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocolloHalley2Service;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.Halley2
{
    public class Halle2Serializer: ProtocolloSerializer
    {
        public Halle2Serializer(ProtocolloLogs protocolloLog) : base(protocolloLog)
        {

        }
        public override XmlAttributeOverrides GetOverrides()
        {
            return new OverrideXml()
                    .Override<FileType>()
                    .Commit();
        }

        public override string Serialize(string sFileName, object pProtocollo, string messaggio)
        {
            var newFileNAme = $"{DateTime.Now.ToString("HHmmss")}-{sFileName}";

            return base.Serialize(newFileNAme, pProtocollo, messaggio);
        }
    }
}
