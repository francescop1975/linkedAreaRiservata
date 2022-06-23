using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari
{
    public interface ITariffaMezziPubblicitari
    {
        Importo TariffaBase { get; }
        IEnumerable<Soglia> Soglie { get; }
        Coefficiente CoefficienteStrada { get; }
        Coefficiente CoefficienteOccupazione { get; }
        Coefficiente CoefficienteBifacciale { get;  }
        Coefficiente CoefficienteLuminoso { get; }
    }
}
