﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Init.SIGePro.Manager.DTO.DatiDinamici
{
    [DataContract]
    public class DecodificaDto
    {
        [DataMember]
        public string Idcomune { get; set; }
        [DataMember]
        public string Tabella { get; set; }
        [DataMember]
        public string Chiave { get; set; }
        [DataMember]
        public string Valore { get; set; }
        [DataMember]
        public bool FlgDisabilitato { get; set; }
        [DataMember]
        public string Raggruppamento { get; set; }
    }
}
