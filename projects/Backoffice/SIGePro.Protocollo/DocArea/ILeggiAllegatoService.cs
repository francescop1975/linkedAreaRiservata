using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.DocArea
{
    public interface ILeggiAllegatoService
    {
        byte[] Download(string idBase);
    }
}
