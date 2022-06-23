using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneComuni
{
    public interface IComuniAssociatiService
    {
        IEnumerable<DatiComuneCompatto> GetComuniAssociatiFrontoffice(string idComune, string software);
    }
}
