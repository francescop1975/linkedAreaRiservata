using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.OccupazioniStradali
{
    public class CalcoloOccupazioniStradaliTemporaneeRequest : CalcoloOccupazioniStradaliRequest
    {
        public double Quantita { get; }
        public int NumeroGiorni { get; }

        public CalcoloOccupazioniStradaliTemporaneeRequest(string tipoOccupazione, string categoriaStrada, double quantita, int numeroGiorni) : base(tipoOccupazione, categoriaStrada, "DUT")
        {
            if (quantita < 0)
            {
                throw new ArgumentException($"Valore non valido: {quantita}", nameof(quantita));
            }

            if (numeroGiorni < 0)
            {
                throw new ArgumentException($"Valore non valido: {numeroGiorni}", nameof(numeroGiorni));
            }

            this.Quantita = quantita;
            this.NumeroGiorni = numeroGiorni;
        }
    }
}
