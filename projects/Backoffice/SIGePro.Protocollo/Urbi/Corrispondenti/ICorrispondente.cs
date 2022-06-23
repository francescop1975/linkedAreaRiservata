using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Urbi.Corrispondenti
{
    public interface ICorrispondente
    {
        xapirestTypeCorrispondenti SearchCorrispondente();
        xapirestTypeInsertCorrispondenti InsertCorrispondente();
    }
}
