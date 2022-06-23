using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici.Interfaces
{
    public interface IFlagsModelloBuilder
    {
        ModelloDinamicoBase.FlagsModello CostruisciFlags(ModelloDinamicoBase modello);
    }
}
