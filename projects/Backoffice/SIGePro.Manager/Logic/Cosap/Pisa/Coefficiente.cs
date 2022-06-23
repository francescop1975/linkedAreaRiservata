using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class Coefficiente
    {
        public double Valore { get; }
        private readonly int _cifreDecimali;


        public Coefficiente(double valore, int cifreDecimali = 2)
        {
            if (cifreDecimali < 0)
            {
                throw new ArgumentException($"Valore non valido: {cifreDecimali}", nameof(cifreDecimali));
            }

            this.Valore = valore;
            this._cifreDecimali = cifreDecimali;
        }


        public override string ToString()
        {
            var fi = new System.Globalization.NumberFormatInfo();
            fi.NumberDecimalSeparator = ",";
            fi.NumberGroupSeparator = "";

            string format = $"N{this._cifreDecimali}";
            return $"{this.Valore.ToString(format, fi)} %";
        }

        public Importo ImportoRidotto(double percentuale)
        {
            return new Importo(this.Valore * percentuale / 100, this._cifreDecimali);
        }
    }
}
