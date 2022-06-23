using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Manager.Logic.GestioneOneri.GestioneConti
{
    [Serializable]
    public class ContoDto
    {
        [XmlElement(Order = 10)]
        public int Id { get; set; }

        [XmlElement(Order = 20)]
        public string CodiceConto { get; set; }

        [XmlElement(Order = 25)]
        public string CodiceSottoConto { get; set; }

        [XmlElement(Order = 30)]
        public string NumeroAccertamento { get; set; }

        [XmlElement(Order = 40)]
        public string Conto { get; set; }

        [XmlElement(Order = 50)]
        public int? AnnoAccertamento { get; set; }

        [XmlElement(Order = 60)]
        public int? Iva { get; set; }

        [XmlElement(Order = 70)]
        public string CodiceRiferimentoCausale { get; set; }

        [XmlElement(Order = 80)]
        public string NumeroSottoAccertamento { get; set; }
    }
}
