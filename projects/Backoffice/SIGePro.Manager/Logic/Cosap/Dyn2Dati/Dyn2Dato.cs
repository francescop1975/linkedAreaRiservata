using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Dyn2Dati
{
    public class Dyn2Dato
    {
        public string ContestoCampo { get; internal set; }
        public int? CodiceIstanza { get; internal set; }
        public int? IdCampoDinamico { get; internal set; }
        public string NomeCampoDinamico { get; set; }
        public string Valore { get; internal set; }
        public string ValoreDecodificato { get; internal set; }
        public int? Indice { get; internal set; }
        public int? IndiceMolteplicita { get; internal set; }

        internal DatoDinamicoNelModello ToDatoDinamicoNelModello()
        {
            if (!this.IndiceMolteplicita.HasValue) 
            {
                throw new Exception("Impossibile richiamare ToDatoDinamicoNelModello() senza aver prima popolato IndiceMolteplicita");
            }

            if (String.IsNullOrEmpty(NomeCampoDinamico))
            {
                throw new Exception("Impossibile richiamare ToDatoDinamicoNelModello() senza aver prima popolato il riferimento del campo dinamico");
            }

            return new DatoDinamicoNelModello
            {
                NomeCampo = this.NomeCampoDinamico,
                IndiceMolteplicita = this.IndiceMolteplicita.Value,
                Valore = this.Valore
            };
        }
    }
}
