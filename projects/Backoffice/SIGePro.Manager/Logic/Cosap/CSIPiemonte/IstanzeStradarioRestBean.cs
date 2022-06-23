using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte
{
    public class IstanzeStradarioRestBean
    {
        public int? id { get; set; }
        public int codice_istanza { get; set; }
        public int tipi_localizzazioni_id { get; set; }
        public int codice_stradario { get; set; }
        public string civico { get; set; }
        public string colore { get; set; }
        public string note { get; set; }
        public bool primario { get; set; }
        public string frazione { get; set; }
        public string circoscrizione { get; set; }
        public string cap { get; set; }
        public string km { get; set; }
        public string esponente { get; set; }
        public string scala { get; set; }
        public string interno { get; set; }
        public string esponente_interno { get; set; }
        public string fabbricato { get; set; }
        public string piano { get; set; }
        public string quartiere { get; set; }
        public string codice_civico { get; set; }
        public string uuid { get; set; }
        public string latitudine { get; set; }
        public string longitudine { get; set; }
        public string accesso_tipo { get; set; }
        public string accesso_numero { get; set; }
        public string accesso_descrizione { get; set; }
        public bool valido { get; set; }
        public string id_punto_sit { get; set; }
        public string id_comune { get; set; }
    }
}
