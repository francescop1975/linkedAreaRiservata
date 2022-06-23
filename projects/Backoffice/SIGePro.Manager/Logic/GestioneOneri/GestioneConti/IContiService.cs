using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneOneri.GestioneConti
{
    public interface IContiService
    {
        ContoDto GetContoDaIdCausaleOnere(int idCausaleOnere);
    }
}
