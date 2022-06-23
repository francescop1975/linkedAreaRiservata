using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Temporanei
{

    internal class TariffaMezziPubblicitariTemporanei : ITariffaMezziPubblicitari
    {
        public Importo Tariffa { get; }

        public Importo CoefficienteIlluminazione { get; }

        public Importo CoefficienteAreaServizio { get; }

        public TariffaMezziPubblicitariTemporanei(double tariffa, double coeffIlluminazione, double coeffCoeffAreaServizio)
        {
            this.Tariffa = new Importo(tariffa);
            this.CoefficienteIlluminazione = new Importo(coeffIlluminazione);
            this.CoefficienteAreaServizio = new Importo(coeffCoeffAreaServizio);
        }
    }
}
