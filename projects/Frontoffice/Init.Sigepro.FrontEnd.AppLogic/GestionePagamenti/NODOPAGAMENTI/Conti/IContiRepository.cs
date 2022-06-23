using Init.Sigepro.FrontEnd.Pagamenti.NODOPAGAMENTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.NODOPAGAMENTI.Conti
{
    public interface IContiRepository
    {
        IContoDTO GetDatiContoDaCausaleOnere(string codiceComune, int causaleOnereId);
    }
}
