using System.Collections.Generic;
using System.Linq;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali
{
    internal class Tariffario : ITariffeConcessioniStradali
    {
        public Importo Tariffa { get; }

        public IEnumerable<FasciaSuperficie> FasceDiSuperfici { get; }

        public Tariffario(double tariffa, double percentuale, int cifreDecimali)
        {
            this.Tariffa = new Importo(tariffa, cifreDecimali).ImportoRidotto(percentuale);
        }

        public Tariffario(double tariffa, int cifreDecimali)
        {
            this.Tariffa = new Importo(tariffa, cifreDecimali);
        }

        public Tariffario(double tariffa, int cifreDecimali, IEnumerable<FasciaSuperficie> fasceDiSuperfici)
        {
            this.Tariffa = new Importo(tariffa, cifreDecimali);
            this.FasceDiSuperfici = fasceDiSuperfici;
        }
    }
}