using System.Collections.Generic;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali
{
    public interface ITariffeConcessioniStradali
    {
        Importo Tariffa { get; }

        IEnumerable<FasciaSuperficie> FasceDiSuperfici { get; }
    }
}