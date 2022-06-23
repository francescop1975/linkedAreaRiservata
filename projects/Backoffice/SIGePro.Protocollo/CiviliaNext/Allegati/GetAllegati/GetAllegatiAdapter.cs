using Init.SIGePro.Protocollo.CiviliaNext.Allegati.GetAllegato;
using Init.SIGePro.Protocollo.CiviliaNext.CercaPratiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext.Allegati.GetAllegati
{
    public class GetAllegatiAdapter
    {
        private string _codiceLivelloOrganigramma;
        private long _idOperatore;
        private long _idPratica;
        public GetAllegatiAdapter(ParametriRegoleInfo parametri, long idProtocollo)
        {
            this._codiceLivelloOrganigramma = parametri.CodiceLivelloOrganigramma;
            this._idOperatore = parametri.IdOperatore;
            this._idPratica = idProtocollo;
        }

        public GetAllegatiRequest CreaRequest()
        {
            return new GetAllegatiRequest
            {
                CodiceLivelloOrganigramma = this._codiceLivelloOrganigramma,
                GetByteArray = false,
                IdOperatore = this._idOperatore,
                IdPratica = this._idPratica,
                IsMarcato = false,
                Pagina = 1
            };
        }

    }
}
