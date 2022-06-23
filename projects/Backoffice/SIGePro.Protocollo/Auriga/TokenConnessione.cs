using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.Auriga
{
    
    public class TokenConnessione
    {
        [XmlElement("TokenConnessione")]
        public TokenConnessioneElement Token { get; set; }
    }

    public class TokenConnessioneElement
    {
        [XmlAttribute]
        public string DesUser { get; set; }

        [XmlAttribute]
        public string IdDominio { get; set; }

        [XmlText]
        public string Valore { get; set; }
    }
}
