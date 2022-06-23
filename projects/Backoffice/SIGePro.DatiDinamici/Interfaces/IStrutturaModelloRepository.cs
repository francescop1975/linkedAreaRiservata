using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici.Interfaces
{
    public interface IStrutturaModelloRepository
    {
        ModelloDinamicoBase.StrutturaModello CostruisciStrutturaModello(ModelloDinamicoBase modello);
    }
}
