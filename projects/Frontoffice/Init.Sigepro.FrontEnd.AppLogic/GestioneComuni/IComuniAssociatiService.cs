using Init.Sigepro.FrontEnd.AppLogic.WsComuniService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneComuni
{
    public interface IComuniAssociatiService
    {
        IEnumerable<DatiComuneCompatto> GetComuniAssociati(string[] codiciComuneDaEscludere = null);
    }
}
