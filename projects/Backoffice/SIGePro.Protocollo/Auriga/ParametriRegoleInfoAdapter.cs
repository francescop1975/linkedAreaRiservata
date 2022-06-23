using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Auriga
{
    public class ParametriRegoleInfoAdapter
    {
        protected VerticalizzazioneProtocolloAuriga _vert;
        protected VerticalizzazioneProtocolloAttivo _base;
        protected string _token;

        public ParametriRegoleInfoAdapter( string token, string idComuneAlias, string software, string codiceComune)
        {
            this._vert = new VerticalizzazioneProtocolloAuriga(idComuneAlias, software, codiceComune);
            this._base = new VerticalizzazioneProtocolloAttivo(idComuneAlias, software, codiceComune);
            this._token = token;
        }

        public ParametriRegoleInfo Adatta()
        {

            if ( !this._vert.Attiva )
                throw new Exception($"La verticalizzazione {this._vert.Nome} non è attiva");
            try
            {
                var par = new ParametriRegoleInfo();
                par.CodiceApplicazione = this._vert.CodApplicazione;
                par.IstanzaApplicazione = this._vert.IstanzaApplicazione;
                par.Password = this._vert.Password;
                par.ProxyUrl = this._vert.ProxyUrl;
                par.Token = this._token;
                par.Url = this._vert.Url;
                par.Username = this._vert.Username;
                par.CaratteriDaEliminare = this._base.ListaCaratteriDaEliminare;

                return par;
            }
            catch (Exception ex)
            {
                throw new Exception($"RECUPERO DEI VALORI DALLA VERTICALIZZAZIONE {this._vert.Nome} FALLITO, {ex.Message}", ex);
            }
        }
    }
}
