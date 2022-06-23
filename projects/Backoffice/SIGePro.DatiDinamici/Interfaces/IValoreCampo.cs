using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici.Interfaces
{
    public interface IValoreCampo
    {
        int? Indice { get; set; }
        int? IndiceMolteplicita { get; set; }
        string Valore { get; set; }
        string Valoredecodificato { get; }
    }
}
