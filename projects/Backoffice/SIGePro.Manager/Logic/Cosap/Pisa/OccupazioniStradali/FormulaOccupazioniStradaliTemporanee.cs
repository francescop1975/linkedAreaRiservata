using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.OccupazioniStradali
{
    public class FormulaOccupazioniStradaliTemporanee : FormulaCosapBase
    {
        private CalcoloOccupazioniStradaliTemporaneeRequest _request;
        private ITariffeOccupazioniStradali _tariffe;

        public FormulaOccupazioniStradaliTemporanee(CalcoloOccupazioniStradaliTemporaneeRequest request, ITariffeOccupazioniStradali tariffe)
        {
            this._request = request;
            this._tariffe = tariffe;
        }

        protected override string GetStringaCalcolo()
        {
            var retVal = "[Tariffa base] * [Coefficiente Categoria Strada] * [Coefficiente Tipo Occupazione] * [mq/ml] * [Giorni]<br>";
            retVal += $"{this._tariffe.TariffaBase} * {this._tariffe.CoefficienteStrada} * {this._tariffe.CoefficienteOccupazione} * {this._request.Quantita} * {this._request.NumeroGiorni}";
            return retVal;
        }

        protected override double GetImporto()
        {
            return this._tariffe.Tariffa.Valore * (double)this._request.NumeroGiorni * this._request.Quantita;
        }


    }
}
