using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using Init.SIGePro.Protocollo.ProtocolloServices;
using System.Globalization;
using Init.SIGePro.Protocollo.ProtocolloEnumerators;

namespace Init.SIGePro.Protocollo.SidUmbria.Protocollazione
{
    public class RequestAdapter20 : RequestAdapterBase, IRequestAdapter
    {
        public RequestAdapter20(IDatiProtocollo datiProto, VerticalizzazioniConfiguration vert, ResolveDatiProtocollazioneService datiIstanzaService, ProtocolloEnum.TipoProvenienza provenienza) : base(datiProto, vert, datiIstanzaService, provenienza)
        {

        }

        public string Token
        {
            get
            {
                return this._datiProto.Uo;
            }
        }

        public string Service
        {
            get
            {
                return this._datiProto.Ruolo;
            }
        }

        public infoProtocollo Adatta()
        {
            return base.AdattaDatiBase();
        }
    }
}
