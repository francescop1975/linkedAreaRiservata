using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Adapters
{
    public class IstanzaSigeproAdapterFlags
    {
        public static IstanzaSigeproAdapterFlags Default = new IstanzaSigeproAdapterFlags { AggiungiPdfSchedeAListaAllegati = true };

        public bool AggiungiPdfSchedeAListaAllegati { get; internal set; }
    }
}
