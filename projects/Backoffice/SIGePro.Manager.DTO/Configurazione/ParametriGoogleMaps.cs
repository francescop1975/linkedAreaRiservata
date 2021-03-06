using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Manager.DTO.Configurazione
{
    [Serializable]
    public class ParametriGoogleMaps
    {
        [XmlElement(Order = 1)]
        public string ApiKey { get; set; }
        [XmlElement(Order = 2)]
        public string MapBounds { get; set; }
    }
}
