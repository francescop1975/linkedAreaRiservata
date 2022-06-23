using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali
{
    public class CalcoloConcessioniStradaliPermanentiRequest : CalcoloConcessioniStradaliRequest
    {

        public double Quantita { get; internal set; }

        public CalcoloConcessioniStradaliPermanentiRequest(string tipoOccupazione, string categoriaStrada, double quantita ): base(tipoOccupazione, categoriaStrada)
        {


            if (quantita < 0)
            {
                throw new ArgumentException($"Valore non valido: {quantita}", nameof(quantita));
            }


            this.Quantita = quantita;
        }

    }
}
