
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali
{
    public class FormulaConcessioniStradaliPermanenti : IFormulaCosap
    {

        private CalcoloConcessioniStradaliPermanentiRequest _request;
        private ITariffeConcessioniStradali _tariffe;

        public FormulaConcessioniStradaliPermanenti(CalcoloConcessioniStradaliPermanentiRequest request, ITariffeConcessioniStradali tariffe )
        {
            this._request = request;
            this._tariffe = tariffe;
        }

        public Calcolo Calcola()
        {
            var stringa = this.GetStringaCalcolo();
            var importo = this.GetImporto();

            return new Calcolo(stringa, importo);
        }


        protected string GetStringaCalcolo()
        {
            

            if (this._tariffe.FasceDiSuperfici == null || this._tariffe.FasceDiSuperfici.Count() == 0)
            {
                return $"Tariffa * Quantità\r\n{this._tariffe.Tariffa} * {this._request.Quantita}";
            }

            string descrizione = "[Tariffa * (Fascia * Percentuale)]";
            List<string> calcolo = new List<string>();

            double quantita = this._request.Quantita;

            this._tariffe.FasceDiSuperfici
                .ToList()
                .ForEach(f => 
                {
                    if (quantita != 0.0d && f.SogliaDiRiferimento <= quantita)
                    {
                        var differenza = quantita - f.SogliaDiRiferimento;
                        calcolo.Add($"[{this._tariffe.Tariffa.Valore} * ({differenza} * {f.Percentuale})]");
                        quantita -= differenza;
                    }
                });

            return $"{descrizione}\r\n{String.Join(" + ", calcolo)}";
        }
        protected double GetImporto()
        {
            if (this._tariffe.FasceDiSuperfici == null || this._tariffe.FasceDiSuperfici.Count() == 0 )
                return this._tariffe.Tariffa.Valore * this._request.Quantita;

            double valore = 0.0d;
            double quantita = this._request.Quantita;

            this._tariffe.FasceDiSuperfici
                .ToList()
                .ForEach(f => 
                {
                    if (quantita != 0.0d && f.SogliaDiRiferimento <= quantita)
                    {
                        var differenza = quantita - f.SogliaDiRiferimento;
                        valore += this._tariffe.Tariffa.Valore * differenza * f.Percentuale;
                        quantita -= differenza;
                    }
                });

            return valore;
        }

        protected string ImportoToString(double importo)
        {
            return new Importo(importo).ToString();
        }
    }
}
