using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Init.SIGePro.Manager.DTO.DatiDinamici
{
    [DataContract]
    public class ListaModelliDinamiciDomandaDto
    {
        [DataMember]
        public List<SchedaDinamicaInterventoDto> SchedeIntervento { get; set; }
        
        [DataMember]
        public List<SchedaDinamicaEndoprocedimentoDto> SchedeEndoprocedimenti { get; set; }
    }
}
