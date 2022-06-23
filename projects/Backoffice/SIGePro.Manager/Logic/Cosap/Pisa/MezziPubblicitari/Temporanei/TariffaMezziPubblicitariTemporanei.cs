using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari.Temporanei
{
    internal class TariffaMezziPubblicitariTemporanei : ITariffaMezziPubblicitari
    {
        private int _cifreDecimali;
        public Importo TariffaBase { get; }
        public IEnumerable<Soglia> Soglie { get; }
        public Coefficiente CoefficienteStrada { get; }
        public Coefficiente CoefficienteOccupazione { get; }
        public Coefficiente CoefficienteBifacciale { get; }
        public Coefficiente CoefficienteLuminoso { get; }
        public TariffaMezziPubblicitariTemporanei(double tariffa, IEnumerable<Soglia> soglie, double coefficienteStrada, double coefficienteOccupazione, double? coefficienteBifacciale, double? coefficienteLuminoso, int cifreDecimali)
        {
            this._cifreDecimali = cifreDecimali;
            this.TariffaBase = new Importo(tariffa, cifreDecimali);
            this.CoefficienteStrada = new Coefficiente(coefficienteStrada, cifreDecimali);
            this.CoefficienteOccupazione = new Coefficiente(coefficienteOccupazione, cifreDecimali);
            this.Soglie = soglie.OrderBy(x => x.A);
            if (coefficienteBifacciale.HasValue)
            {
                this.CoefficienteBifacciale = new Coefficiente(coefficienteBifacciale.Value, cifreDecimali);
            }
            if (coefficienteLuminoso.HasValue)
            {
                this.CoefficienteLuminoso = new Coefficiente(coefficienteLuminoso.Value, cifreDecimali);
            }
        }
    }
}
