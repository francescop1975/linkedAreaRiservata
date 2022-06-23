using Init.SIGePro.Protocollo.ProtocolloEnumerators;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using Init.SIGePro.Protocollo.ProtocolloServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.SidUmbria.Protocollazione
{
    public class RequestAdapterFactory
    {
        private class Constants
        {
            public const string Versione_40 = "4.0";
        }

        public static IRequestAdapter Create(IDatiProtocollo datiProto, VerticalizzazioniConfiguration vert, ResolveDatiProtocollazioneService datiIstanzaService, ProtocolloEnum.TipoProvenienza provenienza)
        {
            if (vert.Versione == Constants.Versione_40)
            {
                return new RequestAdapter40(datiProto, vert, datiIstanzaService, provenienza);
            }

            return new RequestAdapter20(datiProto, vert, datiIstanzaService, provenienza);
        }
    }
}
