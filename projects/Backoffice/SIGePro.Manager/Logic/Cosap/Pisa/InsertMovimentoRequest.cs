using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class InsertMovimentoRequest
    {
        public int? CodiceAmministrazione { get; set; }
        public int CodiceResponsabile { get; set; }
        public int? DataScadenza { get; set; }
        public string DescrizioneMovimento { get; set; }
        public bool PubblicaMovimento { get; set; }
        public string TipoMovimento { get; set; }
    }
}
