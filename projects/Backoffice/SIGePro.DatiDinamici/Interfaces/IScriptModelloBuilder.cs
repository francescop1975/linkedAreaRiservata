using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici.Interfaces
{
    interface IScriptModelloBuilder
    {
        ModelloDinamicoBase.ScriptsModello CostruisciScripts(ModelloDinamicoBase modello);
    }
}
