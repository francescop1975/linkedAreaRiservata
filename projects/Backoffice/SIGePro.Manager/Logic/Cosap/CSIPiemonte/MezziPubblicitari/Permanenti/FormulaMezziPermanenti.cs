using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Permanenti
{
    public class FormulaMezziPermanenti : FormulaCosapBase
    {
        private readonly CalcoloMezziPermanentiRequest _request;
        private readonly ITariffaMezziPubblicitari _tariffe;

        public FormulaMezziPermanenti(CalcoloMezziPermanentiRequest request, ITariffaMezziPubblicitari tariffe)
        {
            _request = request;
            _tariffe = tariffe;
        }

        protected override double GetImporto()
        {
            return this._tariffe.Tariffa.Valore *
                    new Importo(this._request.NumeroMezzi).Valore *
                    this._tariffe.CoefficienteIlluminazione.Valore *
                    this._tariffe.CoefficienteAreaServizio.Valore;
        }

        protected override string GetStringaCalcolo()
        {
            return $"{this._tariffe.Tariffa} * {this._request.NumeroMezzi} * {this._tariffe.CoefficienteIlluminazione} * {this._tariffe.CoefficienteAreaServizio}";
        }
    }
}
