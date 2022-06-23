using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class Calcolo
    {
        public string SpiegazioneFormula { get; }
        public Importo Importo { get; }

        public Calcolo(string spiegazione, double valore)
        {
            this.SpiegazioneFormula = spiegazione;
            this.Importo = new Importo(valore);
        }
    }
}
