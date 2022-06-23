using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.SidUmbria.Protocollazione
{
    public interface IRequestAdapter
    {
        infoProtocollo Adatta();
        string Token { get; }
        string Service { get; }
    }
}

