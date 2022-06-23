using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali
{
    public class CalcoloConcessioniStradaliRequest
    {
        public string TipoOccupazione { get; internal set; }
        public string CategoriaStrada { get; internal set; }

        public CalcoloConcessioniStradaliRequest(string tipoOccupazione, string categoriaStrada)
        {
            if (string.IsNullOrEmpty(tipoOccupazione))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(tipoOccupazione));
            }

            if (string.IsNullOrEmpty(categoriaStrada))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(categoriaStrada));
            }


            this.TipoOccupazione = tipoOccupazione;
            this.CategoriaStrada = categoriaStrada;
        }
    }
}
