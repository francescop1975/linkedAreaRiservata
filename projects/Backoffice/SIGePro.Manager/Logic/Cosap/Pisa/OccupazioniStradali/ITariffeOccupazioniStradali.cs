using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.OccupazioniStradali
{
    public interface ITariffeOccupazioniStradali
    {
        Importo Tariffa { get; }
        Importo TariffaBase { get; }
        Coefficiente CoefficienteStrada { get; }
        Coefficiente CoefficienteOccupazione { get; }
    }
}
