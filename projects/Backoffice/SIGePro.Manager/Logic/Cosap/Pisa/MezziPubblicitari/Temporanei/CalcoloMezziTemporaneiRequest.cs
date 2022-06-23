using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari.Temporanei
{
    public class CalcoloMezziTemporaneiRequest
    {

        public string TipoOccupazione { get; }
        public string CategoriaStrada { get; }
        public double Quantita { get; }
        public string OccupazioneStrada { get; }
        public bool Luminoso { get;  }
        public bool Bifacciale { get; }
        public int NumeroGiorni { get; }

        public CalcoloMezziTemporaneiRequest(string tipoOccupazione, string categoriaStrada, double quantita, string occupazioneStrada, int numeroGiorni, bool bifacciale, bool luminoso)
        {
            if (quantita < 0)
            {
                throw new ArgumentException($"Valore non valido: {quantita}", nameof(quantita));
            }

            if (string.IsNullOrEmpty(tipoOccupazione))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(tipoOccupazione));
            }

            if (string.IsNullOrEmpty(categoriaStrada))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(categoriaStrada));
            }

            if (string.IsNullOrEmpty(occupazioneStrada))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(occupazioneStrada));
            }

            if (numeroGiorni < 0)
            {
                throw new ArgumentException($"Valore non valido: {numeroGiorni}", nameof(numeroGiorni));
            }

            this.Quantita = quantita;
            this.TipoOccupazione = tipoOccupazione;
            this.CategoriaStrada = categoriaStrada;
            this.OccupazioneStrada = occupazioneStrada;
            this.NumeroGiorni = numeroGiorni;
            this.Bifacciale = bifacciale;
            this.Luminoso = luminoso;

        }
    }
}
