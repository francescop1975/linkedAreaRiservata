using System;
using System.Collections.Generic;

namespace Init.SIGePro.Manager.Logic.GestioneAntenneLucca
{
    public class PraticaPerAntenna
    {
        public int CodiceIstanza { get; internal set; }
        public string NumeroIstanza { get; internal set; }
        public DateTime DataIstanza { get; internal set; }

        public string NumeroProtocollo { get; internal set; }
        public DateTime? DataProtocollo { get; internal set; }

        public string Richiedente { get; internal set; }
        public string Tecnico { get; internal set; }
        public string Azienda { get; internal set; }

        public string Intervento { get; internal set; }
        public IEnumerable<FilePratica> AllegatiIntervento { get; internal set; }
        public IEnumerable<FilePratica> AllegatiEndoprocedimenti { get; internal set; }
        public IEnumerable<FilePratica> AllegatiMovimenti { get; internal set; }
    }
}