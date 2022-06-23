using System;
using System.Xml.Serialization;

namespace Init.SIGePro.Manager.Logic.Visura
{

    public class FiltroPersonaAventeTitoloDiVisura
    {
        [XmlElement(Order = 0)]
        public string CodiceFiscale { get; set; }
        /*
        [XmlElement(Order = 1)]
        public bool CercaNeiSoggettiCollegati { get; set; }
        */
    }

    public class FiltriDatiCatastali
    {
        [XmlElement(Order = 0)]
        public string TipoCatasto { get; set; }
        [XmlElement(Order = 1)]
        public string Foglio { get; set; }
        [XmlElement(Order = 2)]
        public string Particella { get; set; }
        [XmlElement(Order = 3)]
        public string Subalterno { get; set; }
        [XmlIgnore]
        public bool AlmenoUnDatoSpecificato => !String.IsNullOrEmpty(TipoCatasto) || !String.IsNullOrEmpty(Foglio) || !String.IsNullOrEmpty(Particella) || !string.IsNullOrEmpty(Subalterno);
    }

    public class FiltroPeriodoPresentazione
    {
        public class DateRange
        {
            public readonly DateTime Min;
            public readonly DateTime Max;

            public DateRange(DateTime min, DateTime max)
            {
                Min = min;
                Max = max;
            }
        }

        [XmlElement(Order = 0)]
        public int Anno { get; set; } = DateTime.Now.Year;
        [XmlElement(Order = 1)]
        public int? Mese { get; set; }

        public DateRange ToDateRange()
        {
            var min = new DateTime(Anno, Mese.GetValueOrDefault(1), 1, 0,0,0);
            var max = new DateTime(Anno, Mese.GetValueOrDefault(12), DateTime.DaysInMonth(Anno, Mese.GetValueOrDefault(12)), 23,59,59);

            return new DateRange(min, max);
        }
    }

    public class FiltroIndirizzo
    {
        [XmlElement(Order = 0)]
        public int CodiceStradario { get; set; }
        [XmlElement(Order = 1)]
        public string Civico { get; set; }
    }

    public class FiltroDatiProtocollo
    {
        [XmlElement(Order = 0)]
        public DateTime? Data { get; set; }

        [XmlElement(Order = 1)]
        public string Numero { get; set; }

        [XmlIgnore]
        public bool AlmenoUnDatoSpecificato => Data.HasValue || !String.IsNullOrEmpty(Numero);
    }

    public class RichiestaListaPraticheV3
    {
        [XmlElement(Order = 1)]
        public string Software { get; set; }
        [XmlElement(Order = 2)]
        public string NumeroIstanza { get; set; }
        [XmlElement(Order = 3)]
        public FiltriDatiCatastali DatiCatastali { get; set; }
        [XmlElement(Order = 4)]
        public FiltroPersonaAventeTitoloDiVisura PersonaAventeTitolo { get; set; }
        [XmlElement(Order = 5)]
        public FiltroPeriodoPresentazione PeriodoPresentazione { get; set; }
        [XmlElement(Order = 6)]
        public FiltroIndirizzo Indirizzo { get; set; }
        [XmlElement(Order = 7)]
        public FiltroDatiProtocollo DatiProtocollo { get; set; }
        [XmlElement(Order = 8)]
        public int? CodiceIntervento { get; set; }
        [XmlElement(Order = 9)]
        public string NumeroAutorizzazione { get; set; }
        [XmlElement(Order = 10)]
        public string StatoPratica { get; set; }
        [XmlElement(Order = 11)]
        public string Oggetto { get; set; }
        [XmlElement(Order = 13)]
        public string NomeOCfRichiedente { get; set; }
        [XmlElement(Order = 14)]
        public string Fabbricato { get; set; }
        [XmlElement(Order = 15)]
        public string PosizioneArchivio { get; set; }
    }
}
