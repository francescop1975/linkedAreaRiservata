using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using Init.SIGePro.Protocollo.Constants;
using Init.SIGePro.Protocollo.InsielMercato2.Services;
using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.InsielMercato2.Verticalizzazioni;

namespace Init.SIGePro.Protocollo.InsielMercato2.Protocollazione
{
    public class ProtocollazioneFactory
    {
        public static IProtocollazione Create(IDatiProtocollo datiProto, ProtocollazioneService wrapper, VerticalizzazioniConfiguration vert, Istanze istanza)
        {
            if (datiProto.ProtoIn.Flusso == ProtocolloConstants.COD_ARRIVO)
                return new ProtocollazioneArrivo(datiProto);
            else if (datiProto.ProtoIn.Flusso == ProtocolloConstants.COD_PARTENZA)
                return new ProtocollazionePartenza(datiProto, wrapper, vert, istanza);
            else
                throw new Exception(String.Format("FLUSSO {0} NON DEFINITO", datiProto.Flusso));
        }
    }
}
