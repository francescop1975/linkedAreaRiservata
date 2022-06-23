using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Storico
{
    public class ParametriStoriciAdapter
    {
        private ParametriStoriciRequest _request;
        protected VerticalizzazioneProtocolloStorico _vert;


        public ParametriStoriciAdapter(ParametriStoriciRequest request)
        {
            this._vert = new VerticalizzazioneProtocolloStorico(request.IdComuneAlias, request.Software, request.CodiceComune);
            this._request = request;
        }

        internal ParametriStorici Adatta()
        {
            bool isCopia = false;
            var idProtocollo = this._request.IdProtocollo;

            if (!String.IsNullOrEmpty(this._request.IdProtocollo))
            {
                var arrIdProtocollo = this._request.IdProtocollo.Split('-');
                isCopia = arrIdProtocollo.Length > 1 && arrIdProtocollo[1] == "COPIA";
                idProtocollo = arrIdProtocollo[0];
            }

            return new ParametriStorici
            {
                AnnoProtocollo = this._request.AnnoProtocollo,
                CodiceAmministrazione = "",
                CodiceAOO = "",
                //IdProtocollo = idProtocollo,
                IsCopia = isCopia,
                Logger = this._request.Logger,
                NumeroProtocollo = this._request.NumeroProtocollo,
                Operatore = "",
                Ruolo = "",
                Serializer = this._request.Serializer,
                Url = "",
            };

        }
    }
}
