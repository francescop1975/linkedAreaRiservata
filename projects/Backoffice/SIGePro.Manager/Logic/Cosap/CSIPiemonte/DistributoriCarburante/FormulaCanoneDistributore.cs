using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    public class FormulaCanoneDistributore : FormulaCosapBase
    {
        private readonly CalcolaImportiRequest _request;
        private readonly TariffeDistributoriCarburante _tariffe;

        public FormulaCanoneDistributore(CalcolaImportiRequest request, TariffeDistributoriCarburante tariffe)
        {
            _request = request;
            _tariffe = tariffe;
        }

        protected override double GetImporto()
        {
            return (this._tariffe.Base.Valore +
                        ((double)this._request.Distributori.Singoli * this._tariffe.Singoli.Valore) +
                        ((double)this._request.Distributori.Doppi * this._tariffe.Doppi.Valore) +
                        ((double)this._request.Distributori.Multiprodotto * this._tariffe.Multiprodotto.Valore)
                    ) * this._tariffe.CoefficienteServiziAccessori;
        }

        protected override string GetStringaCalcolo()
        {
            return $"[{this._tariffe.Base} + " +
                        $"({this._request.Distributori.Singoli} * {this._tariffe.Singoli}) + " +
                        $"({this._request.Distributori.Doppi} * {this._tariffe.Doppi}) + " +
                        $"({this._request.Distributori.Multiprodotto} * {this._tariffe.Multiprodotto})" +
                   $"] * {ImportoToString(this._tariffe.CoefficienteServiziAccessori)}";
        }
    }
}
