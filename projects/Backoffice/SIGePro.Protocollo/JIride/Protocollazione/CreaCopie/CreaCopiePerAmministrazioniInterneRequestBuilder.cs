﻿using Init.SIGePro.Data;
using Init.SIGePro.Protocollo.JIride.Protocollazione.LeggiProtocollo;
using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Protocollazione.CreaCopie
{
    public class CreaCopiePerAmministrazioniInterneRequestBuilder : IJIrideRequestBuilder<CreaCopieInfo>
    {
        private readonly VerticalizzazioneProtocolloJIride _verticalizzazione;
        private readonly ProtocolloOutXml _protocollo;
        private readonly UODestinatariaXml[] _uoDestinatarie;
        private readonly string _operatore;
        private readonly string _ruolo;

        public CreaCopiePerAmministrazioniInterneRequestBuilder(VerticalizzazioneProtocolloJIride verticalizzazione, ProtocolloOutXml protocollo, Amministrazioni mittente, List<Amministrazioni> destinatari, string operatore )
        {
            this._verticalizzazione = verticalizzazione;
            this._protocollo = protocollo;

            var ammList = destinatari
                            .Where(amm => !String.IsNullOrEmpty(amm.PROT_UO) && !String.IsNullOrEmpty(amm.PROT_RUOLO))
                            .ToList();

            this._operatore = operatore;

            this._ruolo = mittente.PROT_RUOLO;

            if (ammList.Count == 0) { return; }

            this._uoDestinatarie = ammList
                                    .Select(x => new UODestinatariaXml
                                    {
                                        Carico = x.PROT_UO,
                                        Data = DateTime.Now.ToString("dd/MM/yyyy"),
                                        TipoUO = "UO",
                                        NumeroCopie = "1"
                                    })
                                    .ToArray();

        }
        public CreaCopieInfo Build()
        {
            return new CreaCopieInfo
            {
                AOO = this._verticalizzazione.Aoo,
                CodiceAmministrazione = this._verticalizzazione.Codiceamministrazione,
                Request = new CreaCopieInXml
                {
                    AnnoProtocollo = this._protocollo.AnnoProtocollo.ToString(),
                    NumeroProtocollo = this._protocollo.NumeroProtocollo.ToString(),
                    IdDocumento = this._protocollo.IdDocumento.ToString(),
                    UODestinatarie = this._uoDestinatarie,
                    Utente = this._operatore,
                    Ruolo = this._ruolo
                },
                Url = this._verticalizzazione.Url,
            };
        }
    }
}
