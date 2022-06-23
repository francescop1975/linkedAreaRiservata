using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte
{
    public class Soglia
    {
        public double Valore { get; internal set; }
        public string Chiave { get; internal set; }

        public Soglia(DecodificaDTO decodifica)
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

            this.Valore = valore;
            this.Chiave = decodifica.Chiave;
        }
    }
}