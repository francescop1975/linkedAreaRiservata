using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.Pagamenti.ESED
{
    public interface IGetStatoPagamento
    {
        string GetDatiPagamento(string numeroOperazione);
    }
}
