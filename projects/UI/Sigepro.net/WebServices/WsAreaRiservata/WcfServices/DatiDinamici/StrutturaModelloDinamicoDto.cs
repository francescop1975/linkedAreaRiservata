using Init.SIGePro.Data;
using Init.SIGePro.Manager.DTO.DatiDinamici;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sigepro.net.WebServices.WsAreaRiservata.WcfServices.DatiDinamici
{
    [DataContract]
    public class StrutturaModelloDinamicoDto
    {
        [DataMember]
        public Dyn2ModelliT Modello { get; set; }
        [DataMember]
        public List<Dyn2ModelliD> Struttura { get; set; }
        [DataMember]
        public List<Dyn2ModelliScript> ScriptsModello { get; set; }
        [DataMember]
        public List<Dyn2CampiScript> ScriptsCampi { get; set; }
        [DataMember]
        public List<Dyn2Campi> CampiDinamici { get; set; }
        [DataMember]
        public List<Dyn2CampiProprieta> ProprietaCampiDinamici { get; set; }
        [DataMember]
        public List<Dyn2TestoModello> Testi { get; set; }
    }

}
