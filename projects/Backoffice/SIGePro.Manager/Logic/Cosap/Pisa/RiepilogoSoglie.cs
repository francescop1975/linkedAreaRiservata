using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class RiepilogoSoglie
    {
        public readonly string Soglia;
        public readonly double Importo;
        public readonly string UnitaMisura;
        public readonly double Qt;
        public double Totale => this.Importo * this.Qt;

        public RiepilogoSoglie(string soglia, double importo, string unitaMisura, double qt)
        {
            this.Soglia = soglia;
            this.Importo = importo;
            this.UnitaMisura = unitaMisura;
            this.Qt = qt;
        }

        public override string ToString()
        {
            return $"{this.Soglia} {this.Importo.ToString("0.00")} {this.UnitaMisura} x {this.Qt}  = {(this.Importo * this.Qt)} €";
        }

    }
}
