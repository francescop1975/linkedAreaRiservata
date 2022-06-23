using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.Auriga
{
    public class ProxyRequestInfo
    {
        [XmlElement("token")]
        public string token { get; set; }

        [XmlElement("codiceApplicazione")]
        public string codiceApplicazione { get; set; }

        [XmlElement("istanzaApplicazione")]
        public string istanzaApplicazione { get; set; }

        [XmlElement("userName")]
        public string userName { get; set; }

        [XmlElement("password")]
        public string password { get; set; }

        [XmlElement("xml")]
        public string xml { get; set; }

        [XmlElement("hash")]
        public string hash { get; set; }

        [XmlElement("namespace")]
        public string nameSpace { get; set; }

        [XmlElement("serviceName")]
        public string serviceName { get; set; }

        [XmlElement("wsUrl")]
        public string wsUrl { get; set; }

        [XmlElement("codiciOggetto")]
        public List<int> codiciOggetto { get; set; }
    }
}
