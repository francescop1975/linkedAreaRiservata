using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.JIride.Protocollazione;
using Init.SIGePro.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.JIride.Fascicolazione
{
    public class FascicoloNuovoRequestBuilder : IJIrideRequestBuilder<FascicoloNuovoRequest>
    {
        private readonly Fascicolo _fascicolo;
        private readonly string _operatore;
        private readonly string _ruolo;
        private readonly VerticalizzazioneProtocolloJIride _verticalizzazione;

        public FascicoloNuovoRequestBuilder(VerticalizzazioneProtocolloJIride verticalizzazione, Fascicolo fascicolo, string operatore, string ruolo)
        {
            this._fascicolo = fascicolo;
            this._operatore = operatore;
            this._ruolo = ruolo;
            this._verticalizzazione = verticalizzazione;
        }


        public FascicoloNuovoRequest Build()
        {
            var isValidDate = DateTime.TryParse(this._fascicolo.DataFascicolo, out DateTime dtFasc);

            return new FascicoloNuovoRequest
            {
                Aoo = this._verticalizzazione.Aoo,
                CodiceAmministrazione = this._verticalizzazione.Codiceamministrazione,
                Url = this._verticalizzazione.Urlfasc,
                Request = new FascicoloInXml
                {
                    Anno = this._fascicolo.AnnoFascicolo.Value.ToString(),
                    Data = !isValidDate && String.IsNullOrEmpty(this._verticalizzazione.FormatoDataFasc)
                        ? this._fascicolo.DataFascicolo
                        : dtFasc.ToString(this._verticalizzazione.FormatoDataFasc),
                    Numero = this._fascicolo.NumeroFascicolo,
                    Oggetto = this._fascicolo.Oggetto,
                    Classifica = this._fascicolo.Classifica,
                    Utente = this._operatore,
                    Ruolo = this._ruolo,
                    Eterogeneo = true
                }

            };
        }
    }
}
