using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneCittadinanze
{
    public class CittadinanzaCompatto
    {
        public int Codice { get; set; }
        public string Descrizione { get; set; }
        public string Cf { get; set; }
        public bool FlgPaeseComunitario { get; set; }
        public bool Disabilitato { get; set; }
    }
}
