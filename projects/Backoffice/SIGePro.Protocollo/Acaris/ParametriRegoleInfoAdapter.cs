using Init.SIGePro.Manager;
using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.ProtocolloServices;
using Init.SIGePro.Protocollo.Serialize;
using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris
{
    public class ParametriRegoleInfoAdapter
    {
        private class Constants
        {
            public const string BackOfficePortUrl = "backofficeWS";
            public const string OfficialBookPortUrl = "officialbookWS";
            public const string ObjectPortUrl = "objectWS";
            public const string RepositoryPortUrl = "repositoryWS";
            public const string DocumentPortUrl = "documentWS";
            public const string ManagementPortUrl = "managementWS";
            public const string NavigationPortUrl = "navigationWS";
            public const string RelationshipsPortUrl = "relationshipsWS";

            public const int ConservazioneIllimitata = 99;
        }

        private VerticalizzazioneProtocolloAcaris _vert;
        private VerticalizzazioneProtocolloAttivo _base;
        private RepositoryWsClient _repositoryClient;
        private BackofficeWSClient _backofficeWsClient;
        private string _codiceFiscale;
        private int _idAoo;
        private int _idNodo;
        private int _idStruttura;
        private IProtocolloSerializer _serializer;

        public ParametriRegoleInfoAdapter(IProtocolloSerializer serializer, String operatore, string idComuneAlias, string software, string codiceComune, int idAoo, int idStruttura, int idNodo)
        {
            this._serializer = serializer;

            this._vert = new VerticalizzazioneProtocolloAcaris(idComuneAlias, software, codiceComune);
            this._base = new VerticalizzazioneProtocolloAttivo(idComuneAlias, software, codiceComune);

            this._repositoryClient = new RepositoryWsClient(this._vert.Url + Constants.RepositoryPortUrl);
            this._backofficeWsClient = new BackofficeWSClient(this._vert.Url + Constants.BackOfficePortUrl);
            this._codiceFiscale = operatore;

            this._idAoo = idAoo;
            this._idStruttura = idStruttura;
            this._idNodo = idNodo;

        }

        public ParametriRegoleInfoAdapter(IProtocolloSerializer serializer, String operatore, int codiceAmministrazione, ResolveDatiProtocollazioneService resolveDatiProtocollazione)
        {
            this._serializer = serializer;

            this._vert = new VerticalizzazioneProtocolloAcaris(resolveDatiProtocollazione.IdComuneAlias, resolveDatiProtocollazione.Software, resolveDatiProtocollazione.CodiceComune);
            this._base = new VerticalizzazioneProtocolloAttivo(resolveDatiProtocollazione.IdComuneAlias, resolveDatiProtocollazione.Software, resolveDatiProtocollazione.CodiceComune);

            this._repositoryClient = new RepositoryWsClient(this._vert.Url + Constants.RepositoryPortUrl);
            this._backofficeWsClient = new BackofficeWSClient(this._vert.Url + Constants.BackOfficePortUrl);

             
            this._codiceFiscale = operatore;
            this._idAoo = this._vert.IdAOO.Value;

            var amministrazione = new AmministrazioniProtocolloMgr(resolveDatiProtocollazione.Db).GetById(resolveDatiProtocollazione.IdComune, codiceAmministrazione);
            this._idStruttura = Convert.ToInt32(amministrazione.ProtUo);
            this._idNodo = Convert.ToInt32(amministrazione.ProtRuolo);
        }



        public ParametriRegoleInfo Adatta()
        {

            if (!this._vert.Attiva)
                throw new Exception($"La verticalizzazione {this._vert.Nome} non è attiva");
            try
            {

                var configurazione = new ParametriRegoleInfo
                {
                    BackOfficePortUrl = this._vert.Url + Constants.BackOfficePortUrl,
                    OfficialBookPortUrl = this._vert.Url + Constants.OfficialBookPortUrl,
                    ObjectPortUrl = this._vert.Url + Constants.ObjectPortUrl,
                    DocumentPortUrl = this._vert.Url + Constants.DocumentPortUrl,
                    ManagementPortUrl = this._vert.Url + Constants.ManagementPortUrl,
                    NavigationPortUrl = this._vert.Url + Constants.NavigationPortUrl,
                    RelationshipsPortUrl = this._vert.Url + Constants.RelationshipsPortUrl,
                    AppKey = this._vert.AppKey,
                    CodiceFiscale = this._codiceFiscale,
                    AnniConservazioneCorrente = this._vert.AnniConservazioneCorrente.HasValue ? this._vert.AnniConservazioneCorrente.Value : Constants.ConservazioneIllimitata,
                    AnniConservazioneGenerale = this._vert.AnniConservazioneGenerale.HasValue ? this._vert.AnniConservazioneGenerale.Value : Constants.ConservazioneIllimitata,
                    IdGradoVitalita = this._vert.IdGradoVitalita.Value,
                    RepositoryID = new RepositoryId(this._serializer, this._repositoryClient, this._vert.Repository),
                    IdTitolario = this._vert.IdTitolario.Value,
                    SerieFascicoliVerticalizzazione = this._vert.SerieFascicoli
                };

                configurazione.PrincipalID = new PrincipalId(this._serializer, this._backofficeWsClient, configurazione.RepositoryID, this._codiceFiscale, this._vert.IdAOO.Value, this._idStruttura, this._idNodo, this._vert.AppKey);
                configurazione.IdAoo = new IdAoo(this._serializer, this._backofficeWsClient, configurazione.RepositoryID, configurazione.PrincipalID, this._idAoo);
                configurazione.IdStruttura = new IdStruttura(this._serializer, this._backofficeWsClient, configurazione.RepositoryID, configurazione.PrincipalID, this._idStruttura);
                configurazione.IdNodo = new IdNodo(this._serializer, this._backofficeWsClient, configurazione.RepositoryID, configurazione.PrincipalID, this._idNodo);

                return configurazione;
            }
            catch (Exception ex)
            {
                throw new Exception($"RECUPERO DEI VALORI DALLA VERTICALIZZAZIONE {this._vert.Nome} FALLITO, {ex.Message}", ex);
            }
        }
    }
}
