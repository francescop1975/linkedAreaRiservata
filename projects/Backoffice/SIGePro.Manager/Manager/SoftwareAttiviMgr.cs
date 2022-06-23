
using System;
using System.Collections.Generic;
using System.Text;
using Init.SIGePro.Data;
using Init.SIGePro.Exceptions;
using System.Data;
using System.ComponentModel;
using Init.SIGePro.Authentication;

using PersonalLib2.Sql;
using Init.Utils.Sorting;
using PersonalLib2.Data;

namespace Init.SIGePro.Manager
{


    [DataObject(true)]
    public partial class SoftwareAttiviMgr
    {
        public SoftwareAttiviList GetSoftwareAttivi(string idComune)
        {
            var filtro = new SoftwareAttivi
            {
                Idcomune = idComune
            };

            return new SoftwareAttiviList(db.GetClassList(filtro).ToList<SoftwareAttivi>());
        }
	}
}
				