using Init.SIGePro.Protocollo.Acaris.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris
{
    public interface IPrincipalIdResolver
    {
        PrincipalId PrincipalId { get; }
    }
}
