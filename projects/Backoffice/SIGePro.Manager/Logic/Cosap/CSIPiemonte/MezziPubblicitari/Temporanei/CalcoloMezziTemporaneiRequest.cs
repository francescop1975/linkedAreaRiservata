using System;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Temporanei
{
    public class CalcoloMezziTemporaneiRequest
    {
        public int NumeroMezzi { get; }
        public string TipoImpianto { get; }
        public string ClasseDimensione { get; }
        public string CategoriaStrada { get; }
        public string OccupazioneStrada { get; }
        public bool Illuminazione { get; }
        public bool AreaServizio { get; }

        public int NumeroGiorni { get; }

        public CalcoloMezziTemporaneiRequest(int numeroMezzi, string tipoImpianto, string classeDimensione, string categoriaStrada, string occupazioneStrada,
                                    bool illuminazione, bool areaServizio, int numeroGiorni)
        {
            if (numeroMezzi < 0)
            {
                throw new ArgumentException($"Valore non valido: {numeroMezzi}", nameof(numeroMezzi));
            }

            if (string.IsNullOrEmpty(tipoImpianto))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(tipoImpianto));
            }

            if (string.IsNullOrEmpty(classeDimensione))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(classeDimensione));
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

            this.NumeroMezzi = numeroMezzi;
            this.TipoImpianto = tipoImpianto;
            this.ClasseDimensione = classeDimensione;
            this.CategoriaStrada = categoriaStrada;
            this.OccupazioneStrada = occupazioneStrada;
            this.Illuminazione = illuminazione;
            this.AreaServizio = areaServizio;
            this.NumeroGiorni = numeroGiorni;

        }
    }
}