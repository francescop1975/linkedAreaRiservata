using Ikriv.Xml;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.Prisma
{
    public class PrismaSerializer : ProtocolloSerializer
    {
        public PrismaSerializer(ProtocolloLogs protocolloLog) : base(protocolloLog)
        {
        }

        public override XmlAttributeOverrides GetOverrides()
        {
            return new OverrideXml()
                    .Override<AcarisDocumentServicePort.acarisContentStreamType>()
                    .Member("streamMTOM").XmlIgnore()
                    .Override<AcarisObjectServicePort.acarisContentStreamType>()
                    .Member("streamMTOM").XmlIgnore()
                    .Commit();
        }

        public override string Serialize(string sFileName, object pProtocollo, string messaggio)
        {
            var newFileNAme = $"{DateTime.Now:HHmmss}-{sFileName}";

            return base.Serialize(newFileNAme, pProtocollo, messaggio);
        }
    }
}
