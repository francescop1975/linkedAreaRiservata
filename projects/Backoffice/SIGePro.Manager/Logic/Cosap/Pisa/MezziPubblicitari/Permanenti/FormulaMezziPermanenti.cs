using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari.Permanenti
{
    public class FormulaMezziPermanenti : FormulaCosapBase
    {
        private readonly CalcoloMezziPermanentiRequest _request;
        private readonly ITariffaMezziPubblicitari _tariffe;
        private double _qtRimanente = 0.0d;


        public FormulaMezziPermanenti(CalcoloMezziPermanentiRequest request, ITariffaMezziPubblicitari tariffe)
        {
            this._request = request;
            this._tariffe = tariffe;
            this._qtRimanente = this._request.Quantita;
        }

        protected override double GetImporto()
        {
            this._qtRimanente = this._request.Quantita;
            return this._tariffe.Soglie
                .Sum(x =>
                    this._tariffe.TariffaBase.Valore *
                    x.Coefficiente.Valore *
                    this._tariffe.CoefficienteStrada.Valore *
                    this._tariffe.CoefficienteOccupazione.Valore *
                    ( this._tariffe.CoefficienteBifacciale != null ? this._tariffe.CoefficienteBifacciale.Valore : 1.00d ) *
                    ( this._tariffe.CoefficienteLuminoso != null ? this._tariffe.CoefficienteLuminoso.Valore : 1.00d) *
                    this.DeterminaQuantita(x).Valore
                );
        }

        protected override string GetStringaCalcolo()
        {
            this._qtRimanente = this._request.Quantita;
            var retVal = $"[Coefficiente base] * [Coefficiente Soglia] * [Coefficiente Strada] * [Coefficiente Occupazione]{(this._tariffe.CoefficienteBifacciale != null ? " * [Coefficiente impianto bifacciale]" : "")} {(this._tariffe.CoefficienteLuminoso != null ? " * [Coefficiente impianto luminoso]" : "")} * [Qt]<br>";
            retVal += String.Join(" +<br>", this._tariffe.Soglie
                .Select(x =>
                    $"[{this._tariffe.TariffaBase} * {x.Coefficiente} * {this._tariffe.CoefficienteStrada} * {this._tariffe.CoefficienteOccupazione}{(this._tariffe.CoefficienteBifacciale != null ? " * " + this._tariffe.CoefficienteBifacciale : "")}{(this._tariffe.CoefficienteLuminoso != null ? " * "+ this._tariffe.CoefficienteLuminoso : "")} * {this.DeterminaQuantita(x).Valore} ] {x.Descrizione}"
                ));

            return retVal;

        }

        private Importo DeterminaQuantita(Soglia sogliaDiRiferimento)
        {
            if (sogliaDiRiferimento.Ciascuna || this._qtRimanente == 0.0d)
            {
                return new Importo(this._qtRimanente);
            }

            if (this._qtRimanente > sogliaDiRiferimento.A)
            {
                this._qtRimanente -= (sogliaDiRiferimento.A - sogliaDiRiferimento.Da);

                return new Importo(sogliaDiRiferimento.A - sogliaDiRiferimento.Da);
            }

            var qt = this._qtRimanente;

            this._qtRimanente = 0.0d;

            return new Importo(qt);
        }
    }
}
