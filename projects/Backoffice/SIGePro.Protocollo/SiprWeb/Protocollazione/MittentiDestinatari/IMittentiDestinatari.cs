using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.SiprWeb.Protocollazione.MittentiDestinatari
{
    public interface IMittentiDestinatari
    {
        string Mittente { get; }
        string[] Destinatari { get; }
        string[] DestinatariCC { get; }
    }
}
