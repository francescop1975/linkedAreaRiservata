using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte
{
    public class FasciaSuperficie
    {
        public string Codice { get; internal set; }

        public double SogliaDiRiferimento { get; internal set; }

        public double Percentuale { get; internal set; }


        public FasciaSuperficie(string codice, double sogliaDiRiferimento, double percentuale )
        {
            this.Codice = codice;
            this.SogliaDiRiferimento = sogliaDiRiferimento;
            this.Percentuale = percentuale;
        }

        public FasciaSuperficie(DecodificaDTO decodifica, double sogliaDiRiferimento)
        {
            if (decodifica == null)
            {
                throw new ArgumentException($"Valore non valido: {decodifica}", nameof(decodifica));
            }

            var isDouble = Double.TryParse(decodifica.Valore, out var valore);

            if (!isDouble)
            {
                throw new ArgumentException($"Valore non valido: {decodifica.Valore}", nameof(decodifica.Valore));
            }

            this.SogliaDiRiferimento = sogliaDiRiferimento;
            this.Percentuale = valore;
            this.Codice = decodifica.Chiave;
        }
    }
}