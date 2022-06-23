using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari.Temporanei
{
    public class FormulaMezziTemporanei : FormulaCosapBase
    {
        private readonly CalcoloMezziTemporaneiRequest _request;
        private readonly ITariffaMezziPubblicitari _tariffe;
        private double _qtRimanente = 0.0d;

        public FormulaMezziTemporanei(CalcoloMezziTemporaneiRequest request, ITariffaMezziPubblicitari tariffe)
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
                        this.DeterminaQuantita(x).Valore * 
                        (double)this._request.NumeroGiorni
                    );
        }

        protected override string GetStringaCalcolo()
        {
            this._qtRimanente = this._request.Quantita;
            var retVal = "[Coefficiente base] * [Coefficiente soglia] * [Coefficiente Strada] * [Qt] * [Giorni]<br>";

            retVal += String.Join(" +<br>", this._tariffe.Soglie
                .Select(x =>
                    $"[{this._tariffe.TariffaBase} * {x.Coefficiente} * {this._tariffe.CoefficienteStrada} * {this.DeterminaQuantita(x).Valore} * {this._request.NumeroGiorni}] {x.Descrizione}"
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
