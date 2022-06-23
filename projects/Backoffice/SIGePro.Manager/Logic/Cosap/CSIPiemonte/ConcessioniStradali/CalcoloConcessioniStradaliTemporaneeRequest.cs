using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali
{
    public class CalcoloConcessioniStradaliTemporaneeRequest : CalcoloConcessioniStradaliRequest
    {

        public double Quantita { get; internal set; }
        public int OreOccupazione { get; internal set; }

        public CalcoloConcessioniStradaliTemporaneeRequest(string tipoOccupazione, string categoriaStrada, double quantita, int oreOccupazione) : base(tipoOccupazione, categoriaStrada)
        {

            if (quantita < 0)
            {
                throw new ArgumentException($"Valore non valido: {quantita}", nameof(quantita));
            }

            if (oreOccupazione < 0)
            {
                throw new ArgumentException($"Valore non valido: {oreOccupazione}", nameof(oreOccupazione));
            }

            this.Quantita = quantita;
            this.OreOccupazione = oreOccupazione;
        }
    }
}
