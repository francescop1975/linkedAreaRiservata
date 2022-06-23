using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.OccupazioniStradali
{
    public class CalcoloOccupazioniStradaliRequest
    {
        public string TipoOccupazione { get; internal set; }
        public string CategoriaStrada { get; internal set; }
        public string DurataOccupazione { get; internal set; }

        public CalcoloOccupazioniStradaliRequest()
        {
            
        }

        public CalcoloOccupazioniStradaliRequest(string tipoOccupazione, string categoriaStrada, string durataOccupazione)
        {
            if (string.IsNullOrEmpty(tipoOccupazione))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(tipoOccupazione));
            }

            if (string.IsNullOrEmpty(categoriaStrada))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(categoriaStrada));
            }

            if (string.IsNullOrEmpty(durataOccupazione))
            {
                throw new ArgumentException("Il parametro non può essere vuoto", nameof(durataOccupazione));
            }


            this.TipoOccupazione = tipoOccupazione;
            this.CategoriaStrada = categoriaStrada;
            this.DurataOccupazione = durataOccupazione;
        }
    }
}
