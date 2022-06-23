using Init.SIGePro.Manager.Logic.GestioneComuni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneConsole.UrlAccesso
{
    public interface IUrlAccessoConsoleService
    {
        ConfigurazioneUrlConsole GetUrlAccessoConsole(string software);
    }
}
