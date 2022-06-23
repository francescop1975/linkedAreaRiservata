﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.JIride.PEC
{
    [XmlRoot(ElementName = "messaggioOut")]
    public class MessaggioOut
    {
        [XmlElement(ElementName = "codice")]
        public string Codice { get; set; }

        [XmlElement(ElementName = "descrizione")]
        public string Descrizione { get; set; }
    }
}
