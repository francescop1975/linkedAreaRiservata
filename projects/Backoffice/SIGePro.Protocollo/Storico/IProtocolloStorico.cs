using Init.SIGePro.Protocollo.WsDataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Storico
{
    public interface IProtocolloStorico
    {
        DatiProtocolloLetto LeggiProtocolloStorico(string idProtocollo, string annoProtocollo, string numeroProtocollo);
        AllOut LeggiAllegatoStorico();
    }
}
