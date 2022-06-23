using Init.SIGePro.DatiDinamici.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici.Interfaces
{
    public interface IScriptModelloRepository
    {
        IDyn2ScriptModello GetById(int idModello, TipoScriptEnum contesto);
    }
}
