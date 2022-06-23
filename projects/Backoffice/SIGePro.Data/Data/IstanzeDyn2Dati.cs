
using System;
using System.Collections.Generic;
using System.Text;
using Init.SIGePro.Attributes;
using PersonalLib2.Sql.Attributes;
using System.Data;
using Init.SIGePro.DatiDinamici.Interfaces;
using System.Runtime.Serialization;

namespace Init.SIGePro.Data
{
	public partial class IstanzeDyn2Dati : IValoreCampo
    {
        [DataMember]
        [ForeignKey("Idcomune,FkD2cId", "Idcomune,Id")]
        public Dyn2Campi CampoDinamico { get; set; }
    }
}
