using PersonalLib2.Sql.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Data
{
    public partial class IstanzeProcure : BaseDataClass
    {
        /// <summary>
        /// Risoluzione FK, Procuratore
        /// </summary>
        [ForeignKey("IdComune,FkAnagProc", "IDCOMUNE,CODICEANAGRAFE")]
        [XmlElement()]
        public Anagrafe Procuratore { get; set; }

        /// <summary>
        /// Risoluzione FK, Rappresentato
        /// </summary>
        [ForeignKey("IdComune,FkAnagRapp", "IDCOMUNE,CODICEANAGRAFE")]
        [XmlElement()]
        public Anagrafe Rappresentato { get; set; }
    }
}
