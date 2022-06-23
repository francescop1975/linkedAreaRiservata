using Init.SIGePro.Protocollo.Serialize;
using Init.SIGePro.Protocollo.WsDataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.DocArea
{
    public interface ILeggiProtocolloService
    {
        DatiProtocolloLetto Leggi(int anno, int numero, string tipoRegistro);
    }
}
