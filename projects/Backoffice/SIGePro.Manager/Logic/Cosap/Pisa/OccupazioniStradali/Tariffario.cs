using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.OccupazioniStradali
{
    internal class Tariffario : ITariffeOccupazioniStradali
    {
        private int _cifreDecimali;
        public Importo TariffaBase { get; }
        public Coefficiente CoefficienteStrada { get; }
        public Coefficiente CoefficienteOccupazione { get; }
        public Importo Tariffa => new Importo(TariffaBase.Valore * CoefficienteStrada.Valore * CoefficienteOccupazione.Valore, this._cifreDecimali);

        public Tariffario(double tariffa, double coefficienteStrada, double coefficienteOccupazione, int cifreDecimali)
        {
            this._cifreDecimali = cifreDecimali;
            this.TariffaBase = new Importo(tariffa, cifreDecimali);
            this.CoefficienteStrada = new Coefficiente(coefficienteStrada, cifreDecimali);
            this.CoefficienteOccupazione = new Coefficiente(coefficienteOccupazione, cifreDecimali);
        }
    }
}
