using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisOfficialBookServicePort;
using Init.SIGePro.Protocollo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Protocollazione.Registrazioni
{
    public class RegistrazioneAPIPartenzaResolver : IRegistrazioneAPIResolver
    {
        private class Constants
        {
            public const bool ForzareSePresenzaInviti = true;
            public const bool ForzareSePresenzaDaInoltrare = true;
            public const bool ForzareSeRegistrazioneSimile = true;
            public const int IdRuoloPerCompetenza = 1;
        }

        private ProtocolloExt _datiProtocollo;
        private ParametriRegoleInfo _parametri;
        private BackofficeWSClient _backofficeWSClient;
        private readonly string _classificazione;
        public string ClassificazioneID => this._classificazione;

        public RegistrazioneAPIPartenzaResolver(ProtocolloExt datiProtocollo, String classificazione)
        {
            this._datiProtocollo = datiProtocollo;
            this._parametri = datiProtocollo.Configurazione;
            this._classificazione = classificazione;

            this._backofficeWSClient = new BackofficeWSClient(this._parametri.BackOfficePortUrl);
        }

        public RegistrazioneAPI ResolveRegistrazione()
        {
            return new RegistrazionePartenza
            {
                tipoRegistrazione = enumTipoAPI.Partenza,
                infoCreazione = new InfoCreazioneRegistrazione
                {
                    forzareSePresenzaInviti = Constants.ForzareSePresenzaInviti,
                    forzareSePresenzaDaInoltrare = Constants.ForzareSePresenzaDaInoltrare,
                    forzareSeRegistrazioneSimile = Constants.ForzareSeRegistrazioneSimile,
                    protocollante = new IdentificazioneProtocollante
                    {
                        nodoId = new ObjectIdType { value = this._parametri.IdNodo.IdAcaris },
                        strutturaId = new ObjectIdType { value = this._parametri.IdStruttura.IdAcaris }
                    },
                    oggetto = this._datiProtocollo.DatiProtocollo.Oggetto,
                    mittenteInterno = this.ListaMittDestToListMittenteInterno(this._datiProtocollo.DatiProtocollo.Mittenti).ToArray(),
                    destinatarioEsterno = this.ListaMittDestToListDestinatarioEsterno(this._datiProtocollo.DatiProtocollo.Destinatari).ToArray()
                }
            };
        }

        private List<MittenteInterno> ListaMittDestToListMittenteInterno(ListaMittDest mittenti)
        {
            if (mittenti == null )
            {
                throw new Exception("Nessun mittente presente per la protocollazione in partenza");
            }

            var elenco = new List<MittenteInterno>();

            elenco.AddRange(mittenti.Amministrazione?.Select(x => new MittenteInterno
            {
                corrispondente = new InfoCreazioneCorrispondente
                {
                    denominazione = x.AMMINISTRAZIONE
                }
            }));

            elenco.AddRange(mittenti.Anagrafe?.Select(x => new MittenteInterno
            {
                corrispondente = new InfoCreazioneCorrispondente
                {
                    cognome = x.NOMINATIVO,
                    nome = x.NOME
                }
            }));

            return elenco;
        }

        private List<DestinatarioEsterno> ListaMittDestToListDestinatarioEsterno(ListaMittDest destinatari)
        {
            if (destinatari == null )
            {
                throw new Exception("Nessun destinatario presente per la protocollazione in partenza");
            }

            var elenco = new List<DestinatarioEsterno>();

            elenco.AddRange(destinatari.Amministrazione?.Select(x => new DestinatarioEsterno
            {
                corrispondente = new InfoCreazioneCorrispondente
                {
                    infoSoggettoAssociato = new RiferimentoSoggettoEsistente
                    {
                        tipologia = enumTipologiaSoggettoAssociato.Nodo,
                        soggettoId = new ObjectIdType { value = new IdNodo( this._datiProtocollo.Serialier, _backofficeWSClient, this._parametri.RepositoryID, this._parametri.PrincipalID, Convert.ToInt32(x.PROT_RUOLO)).IdAcaris }
                    },
                    denominazione = x.AMMINISTRAZIONE
                },
                idRuoloCorrispondente = Constants.IdRuoloPerCompetenza
            }));

            elenco.AddRange(destinatari.Anagrafe?.Select(x => new DestinatarioEsterno
            {
                corrispondente = new InfoCreazioneCorrispondente
                {
                    cognome = x.NOMINATIVO,
                    nome = x.NOME
                },
                idRuoloCorrispondente = Constants.IdRuoloPerCompetenza
            }));

            return elenco;
        }
    }
}
