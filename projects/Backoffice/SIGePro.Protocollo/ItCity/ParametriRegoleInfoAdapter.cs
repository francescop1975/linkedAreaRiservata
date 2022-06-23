using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.ItCity
{
    public class ParametriRegoleInfoAdapter
    {

        protected VerticalizzazioneProtocolloItCity _vert;
        protected VerticalizzazioneProtocolloAttivo _base;
        protected string _token;

        public ParametriRegoleInfoAdapter(string token, string idComuneAlias, string software, string codiceComune)
        {
            this._vert = new VerticalizzazioneProtocolloItCity(idComuneAlias, software, codiceComune);
            this._base = new VerticalizzazioneProtocolloAttivo(idComuneAlias, software, codiceComune);
            this._token = token;
        }

        public ParametriRegoleInfo Adatta()
        {

            if (!this._vert.Attiva)
                throw new Exception($"La verticalizzazione {this._vert.Nome} non è attiva");
            try
            {
                var par = new ParametriRegoleInfo();
                par.Password = this._vert.Password;
                par.Sigla = this._vert.Sigla;
                par.Url = this._vert.Url;
                par.Username = this._vert.Username;
                par.Noallegati = this._base.Noallegati;

                return par;
            }
            catch (Exception ex)
            {
                throw new Exception($"RECUPERO DEI VALORI DALLA VERTICALIZZAZIONE {this._vert.Nome} FALLITO, {ex.Message}", ex);
            }
        }
    }
}
