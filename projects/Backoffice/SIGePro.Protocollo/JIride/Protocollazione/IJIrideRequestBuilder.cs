using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Protocollazione
{
    public interface IJIrideRequestBuilder<T>
    {
        T Build();
    }
}
