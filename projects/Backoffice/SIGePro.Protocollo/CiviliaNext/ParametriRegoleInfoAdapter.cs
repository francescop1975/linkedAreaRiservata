using Init.SIGePro.Manager.Verticalizzazioni;
using Init.SIGePro.Protocollo.CiviliaNext.OAuth2;
using Init.SIGePro.Protocollo.CiviliaNext.RicercaLivello;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.CiviliaNext
{
    public class ParametriRegoleInfoAdapter
    {
        protected VerticalizzazioneProtocolloCiviliaNext _vert;
        protected VerticalizzazioneProtocolloAttivo _base;
        protected string _token;
        protected string _matricola;

        private ProtocolloLogs _protocolloLogs;
        private ProtocolloSerializer _protocolloSerializer;

        public ParametriRegoleInfoAdapter(ProtocolloLogs protocolloLogs, ProtocolloSerializer protocolloSerializer, 
            string token, string idComuneAlias, string software, string codiceComune, string matricola)
        {
            this._protocolloLogs = protocolloLogs;
            this._protocolloSerializer = protocolloSerializer;
            this._vert = new VerticalizzazioneProtocolloCiviliaNext(idComuneAlias, software, codiceComune);
            this._base = new VerticalizzazioneProtocolloAttivo(idComuneAlias, software, codiceComune);
            this._token = token;
            this._matricola = matricola;

        }

        public ParametriRegoleInfo Adatta()
        {

            if (!this._vert.Attiva)
                throw new Exception($"La verticalizzazione {this._vert.Nome} non è attiva");
            try
            {

                var parametri = new ParametriRegoleInfo
                {
                    AggiungiAllegatoURL = this._vert.UrlWSAggiungiAllegato,
                    AnnullaProtocolloURL = this._vert.UrlWSAnnullaProtocollo,
                    AssegnaPraticaURL = this._vert.UrlWsAssegnazione,
                    CercaPraticheURL = this._vert.UrlWSCercaPratiche,
                    ClientID = this._vert.ClientID,
                    ClientSercret = this._vert.CliendSecret,
                    //CodiceLivelloOrganigramma = this._vert.CodiceLivelloOrganigramma,
                    EstraiTitolarioURL = this._vert.UrlWSEstraiTitolario,
                    GetAllegatiURL = this._vert.UrlWSGetAllegati,
                    GetAllegatoURL = this._vert.UrlWSGetAllegato,
                    //IdAutore = this._idOperatore.ToString(),
                    IdCasellaEmail = this._vert.IdCasellaEmail,
                    IdCodiceAOO = long.Parse(this._vert.IdCodiceAOO),
                    //IdOperatore = this._idOperatore,
                    IdRegistro = long.Parse(this._vert.IdRegistro),
                    InviaMail = this._base.AbilitaIndirizziEmail,
                    InviaProtocolloURL = this._vert.UrlWsInviaProtocollo,
                    UrlOAuth = this._vert.UrlOAuth2,
                    ProtocollazioneURL = this._vert.UrlWSProtocollo,
                    Logger = this._protocolloLogs,
                    Serializer = this._protocolloSerializer,
                    RicercaLivelloURL = this._vert.UrlWSRicercaLivello,
                    Resource = this._vert.UrlWSResource,
                    
                };

                //token
                parametri.Token = new OAuth2Service(parametri.UrlOAuth, parametri.ClientID, parametri.ClientSercret, parametri.GrantType, parametri.Resource).GetTokenOAuth2();

                parametri.IdOperatore = Convert.ToInt32(this._matricola);
                parametri.CodiceLivelloOrganigramma = this._vert.CodiceLivelloOrganigramma;

                return parametri;

            }
            catch (Exception ex)
            {
                throw new Exception($"RECUPERO DEI VALORI DALLA VERTICALIZZAZIONE {this._vert.Nome} FALLITO, {ex.Message}", ex);
            }
        }
    }
}
