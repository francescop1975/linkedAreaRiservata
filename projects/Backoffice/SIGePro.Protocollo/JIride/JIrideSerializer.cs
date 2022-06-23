using Ikriv.Xml;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.JIride
{
    public class JIrideSerializer : ProtocolloSerializer
    {

        public JIrideSerializer(ProtocolloLogs protocolloLog) : base(protocolloLog)
        {
        }


        public override string Serialize(string sFileName, object pProtocollo, string messaggio)
        {
            var newFileNAme = $"{DateTime.Now.ToString("HHmmss")}-{sFileName}";

            return base.Serialize(newFileNAme, pProtocollo, messaggio);
        }

    }
}
