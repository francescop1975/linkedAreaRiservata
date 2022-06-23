﻿using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.JIride.Protocollazione;
using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Fascicolazione.Lettura
{
    public class LeggiFascicoloRequestBuilder : IJIrideRequestBuilder<LeggiFascicoloRequest>
    {
        private readonly VerticalizzazioneProtocolloJIride _verticalizzazione;
        private readonly Fascicolo _fascicolo;
        private readonly string _operatore;
        private readonly string _ruolo;
        private readonly int? _idFascicolo;

        public LeggiFascicoloRequestBuilder(VerticalizzazioneProtocolloJIride verticalizzazione, Fascicolo fascicolo, int? idFascicolo, string operatore, string ruolo)
        {
            this._verticalizzazione = verticalizzazione;
            this._fascicolo = fascicolo;
            this._operatore = operatore;
            this._ruolo = ruolo;
            this._idFascicolo = idFascicolo.HasValue && idFascicolo.Value != 0 ? idFascicolo : null;
        }

        public LeggiFascicoloRequest Build()
        {
            return new LeggiFascicoloRequest
            {
                Aoo = this._verticalizzazione.Aoo,
                CodiceAmministrazione = this._verticalizzazione.Codiceamministrazione,
                Url = this._verticalizzazione.Urlfasc,
                Request = new LeggiFascicoloWSRequest 
                {
                    Anno = this._fascicolo.AnnoFascicolo.Value.ToString(),
                    Classifica = this._fascicolo.Classifica,
                    Id = this._idFascicolo,
                    Numero = this._fascicolo.NumeroFascicolo,
                    Ruolo = this._ruolo,
                    Utente = this._operatore
                }
            };
        }
    }
}
