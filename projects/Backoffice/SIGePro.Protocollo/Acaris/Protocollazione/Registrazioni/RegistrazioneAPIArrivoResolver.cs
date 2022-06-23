﻿using Init.SIGePro.Protocollo.Acaris.Client;
using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisOfficialBookServicePort;
using Init.SIGePro.Protocollo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Protocollazione.Registrazioni
{
    public class RegistrazioneAPIArrivoResolver : IRegistrazioneAPIResolver
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


        public RegistrazioneAPIArrivoResolver(ProtocolloExt datiProtocollo, String classificazione)
        {
            this._datiProtocollo = datiProtocollo;
            this._parametri = datiProtocollo.Configurazione;
            this._classificazione = classificazione;

            this._backofficeWSClient = new BackofficeWSClient(this._parametri.BackOfficePortUrl);
        }

        public RegistrazioneAPI ResolveRegistrazione()
        {
            return new RegistrazioneArrivo
            {
                tipoRegistrazione = enumTipoAPI.Arrivo,
                infoCreazione = new InfoCreazioneRegistrazione {
                    forzareSePresenzaInviti = Constants.ForzareSePresenzaInviti,
                    forzareSePresenzaDaInoltrare = Constants.ForzareSePresenzaDaInoltrare,
                    forzareSeRegistrazioneSimile = Constants.ForzareSeRegistrazioneSimile,
                    protocollante = new IdentificazioneProtocollante 
                    { 
                        nodoId =  new ObjectIdType { value = this._parametri.IdNodo.IdAcaris },
                        strutturaId = new ObjectIdType { value = this._parametri.IdStruttura.IdAcaris }
                    },
                    oggetto = this._datiProtocollo.DatiProtocollo.Oggetto,
                    destinatarioInterno = this.ListaMittDestToIEnumerableDestinatarioInterno( this._datiProtocollo.DatiProtocollo.Destinatari ).ToArray()
                },
                mittenteEsterno = this.ListaMittDestToListMittenteEsterno(this._datiProtocollo.DatiProtocollo.Mittenti).ToArray()
            };
        }

        private List<MittenteEsterno> ListaMittDestToListMittenteEsterno(ListaMittDest mittenti) {
            if (mittenti == null || ( mittenti.Amministrazione?.Count == 0 && mittenti.Anagrafe?.Count == 0))
            {
                throw new Exception("Nessun mittente presente per la protocollazione in arrivo");
            }

            var elenco = new List<MittenteEsterno>();

            elenco.AddRange( mittenti.Amministrazione?.Select(x => new MittenteEsterno
            {
                corrispondente = new InfoCreazioneCorrispondente
                { 
                    denominazione = x.AMMINISTRAZIONE
                }
            }));

            elenco.AddRange(mittenti.Anagrafe?.Select(x => new MittenteEsterno
            {
                corrispondente = new InfoCreazioneCorrispondente
                {
                    cognome = x.NOMINATIVO,
                    nome = x.NOME
                }
            }));

            return elenco;
        }

        private IEnumerable<DestinatarioInterno> ListaMittDestToIEnumerableDestinatarioInterno(ListaMittDest destinatari ) {
            if (destinatari == null || destinatari.Amministrazione == null || destinatari.Amministrazione.Count == 0)
            {
                throw new Exception("Nessun destinatario presente per la protocollazione in arrivo");
            }

            return destinatari.Amministrazione.Select(x => new DestinatarioInterno
            {
                corrispondente = new InfoCreazioneCorrispondente
                {
                    infoSoggettoAssociato = new RiferimentoSoggettoEsistente
                    {
                        tipologia = enumTipologiaSoggettoAssociato.Nodo,
                        soggettoId = new ObjectIdType { value = new IdNodo(this._datiProtocollo.Serialier ,_backofficeWSClient, this._parametri.RepositoryID, this._parametri.PrincipalID, Convert.ToInt32(x.PROT_RUOLO)).IdAcaris }
                    },
                    denominazione = x.AMMINISTRAZIONE
                },
                idRuoloCorrispondente = Constants.IdRuoloPerCompetenza
            });
        }

    }
}
