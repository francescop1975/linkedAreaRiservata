using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public abstract class FormulaCosapBase : IFormulaCosap
    {
        public Calcolo Calcola()
        {
            var stringa = GetStringaCalcolo();
            var importo = GetImporto();

            return new Calcolo(stringa, importo);
        }

        protected abstract string GetStringaCalcolo();
        protected abstract double GetImporto();

        protected string ImportoToString(double importo)
        {
            return new Importo(importo).ToString();
        }
    }
}
