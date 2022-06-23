using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.OccupazioniStradali
{
    public class CalcoloOccupazioniStradaliPermanentiRequest : CalcoloOccupazioniStradaliRequest
    {
        public double Quantita { get; internal set; }

        public CalcoloOccupazioniStradaliPermanentiRequest(string tipoOccupazione, string categoriaStrada, double quantita) : base(tipoOccupazione,categoriaStrada,"DUP")
        {
            if (quantita < 0)
            {
                throw new ArgumentException($"Valore non valido: {quantita}", nameof(quantita));
            }


            this.Quantita = quantita;
        }
    }
}
