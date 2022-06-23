using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class MovimentoRestBean
    {
        public int codice_movimento { get; set; }
        public int codice_istanza { get; set; }
        public string tipomovimento { get; set; }
        public int? codice_inventario { get; set; }
        public int? codice_amministrazione { get; set; }
        public DateTime data { get; set; }
        public string parere { get; set; }
        public bool? esito { get; set; }
        public string note { get; set; }
        public bool pubblica { get; set; }
        public string numeroprotocollo { get; set; }
        public DateTime? dataprotocollo { get; set; }
        public int codice_responsabile { get; set; }
        public string movimento { get; set; }
        public bool? pubblicaparere { get; set; }
        public int? dataScadenza { get; set; }
    }
}
