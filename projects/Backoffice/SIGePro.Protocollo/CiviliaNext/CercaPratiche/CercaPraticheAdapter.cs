using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.CercaPratiche
{
    public class CercaPraticheAdapter
    {
        private ParametriRegoleInfo _parametri;
        private long _idProtocollo;


        public CercaPraticheAdapter(ParametriRegoleInfo parametri, long idProtocollo)
        {
            this._parametri = parametri;
            this._idProtocollo = idProtocollo;
        }

        public CercaPraticheRequest Adatta()
        {
            return new CercaPraticheRequest
            {
                CodiceLivelloOrganigramma = _parametri.CodiceLivelloOrganigramma,
                IdPratica = this._idProtocollo,
                IdOperatore = this._parametri.IdOperatore,
                Pagina = 1
            };
        }
    }
}
