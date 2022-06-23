
using System;
using System.Collections.Generic;
using System.Text;
using Init.SIGePro.Attributes;
using PersonalLib2.Sql.Attributes;
using System.Data;
//using Init.SIGePro.DatiDinamici.Interfaces.Anagrafe;
using Init.SIGePro.DatiDinamici.Interfaces;

namespace Init.SIGePro.Data
{
    public partial class AnagrafeDyn2DatiStorico : IValoreCampo
    {
        public string Valoredecodificato => this.Valore;
    }
}
				