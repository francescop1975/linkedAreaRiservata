using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Manager.Logic.Visura
{
    public class RisultatoVisuraPraticaV3
    {
        [XmlElement(Order = 0)]
        public bool LimiteRecordsSuperato { get; internal set; } = true;
        [XmlElement(Order = 1)]
        public VisuraListItemV3[] Pratiche { get; internal set; } = Enumerable.Empty<VisuraListItemV3>().ToArray();
    }

    public class VisuraListItemV3
    {
        [XmlElement(Order = 0)]
        public int CodiceIstanza { get; internal set; }
        [XmlElement(Order = 1)]
        public string Uuid { get; internal set; }
        [XmlElement(Order = 2)]
        public string Oggetto { get; internal set; }
        [XmlElement(Order = 3)]
        public string Progressivo { get; internal set; }
        [XmlElement(Order = 4)]
        public string TipoIntervento { get; internal set; }
        [XmlElement(Order = 5)]
        public string NumeroProtocollo { get; internal set; }
        [XmlElement(Order = 6)]
        public string Operatore { get; internal set; }
        [XmlElement(Order = 7)]
        public string CodiceArea { get; internal set; }
        [XmlElement(Order = 8)]
        public string Civico { get; internal set; }
        [XmlElement(Order = 9)]
        public string Stato { get; internal set; }
        [XmlElement(Order = 10)]
        public DateTime DataPresentazione { get; internal set; }
        [XmlElement(Order = 11)]
        public string Subalterno { get; internal set; }
        [XmlElement(Order = 12)]
        public string NumeroIstanza { get; internal set; }
        [XmlElement(Order = 13)]
        public string TipoProcedura { get; internal set; }
        [XmlElement(Order = 14)]
        public string LocalizzazioneConCivico { get; internal set; }
        [XmlElement(Order = 15)]
        public DateTime? DataProtocollo { get; internal set; }
        [XmlElement(Order = 16)]
        public string Particella { get; internal set; }
        [XmlElement(Order = 17)]
        public string Software { get; internal set; }
        [XmlElement(Order = 18)]
        public string Richiedente { get; internal set; }
        [XmlElement(Order = 19)]
        public string Foglio { get; internal set; }
        [XmlElement(Order = 20)]
        public string TipoCatasto { get; internal set; }
        [XmlElement(Order = 21)]
        public string Azienda { get; internal set; }
        [XmlElement(Order = 22)]
        public string PosizioneArchivio { get; internal set; }
    }
}
