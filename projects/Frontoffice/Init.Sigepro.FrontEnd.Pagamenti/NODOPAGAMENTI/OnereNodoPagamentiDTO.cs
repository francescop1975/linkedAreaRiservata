using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.Pagamenti.NODOPAGAMENTI
{
    public class OnereNodoPagamentiDTO
    {
        public readonly string UniqueId;
        public readonly string CodiceCausale;
        public readonly string Descrizione;
        public readonly decimal Importo;
        public readonly IContoDTO Conto;

        public OnereNodoPagamentiDTO(string uniqueId, string codiceCausale, string descrizione, decimal importo, IContoDTO conto)
        {
            if (string.IsNullOrEmpty(uniqueId))
            {
                throw new ArgumentException($"'{nameof(uniqueId)}' cannot be null or empty", nameof(uniqueId));
            }

            if (string.IsNullOrEmpty(codiceCausale))
            {
                throw new ArgumentException($"'{nameof(codiceCausale)}' cannot be null or empty", nameof(codiceCausale));
            }

            if (string.IsNullOrEmpty(descrizione))
            {
                throw new ArgumentException($"'{nameof(descrizione)}' cannot be null or empty", nameof(descrizione));
            }

            if (importo < 0)
                throw new ArgumentException($"{nameof(importo)} non può essere minore di 0. Controllare il valore dell'onere {descrizione}");

            this.Conto = conto ?? throw new ArgumentNullException(nameof(conto));

            if (!this.Conto.AnnoAccertamento.HasValue)
            {
                throw new ArgumentException($"'{nameof(conto)}' Anno accertamento non specificato per la causale {descrizione}", nameof(conto));
            }

            //if (String.IsNullOrEmpty(this.Conto.NumeroAccertamento))
            //{
            //    throw new ArgumentException($"'{nameof(conto)}' Numero accertamento non specificato per la causale {descrizione}", nameof(conto));
            //}

            this.UniqueId = uniqueId;
            this.CodiceCausale = codiceCausale;
            this.Descrizione = descrizione;
            this.Importo = importo;            
        }

        internal ImportoPagamentoType ToImportoPagamentoType()
        {
            return new ImportoPagamentoType
            {
                annoAccertamento = this.Conto.AnnoAccertamento.ToString(),
                codiceConto = this.Conto.CodiceConto,
                datiRiscossione = this.Conto.CodiceSottoConto,
                descrizioneCausale = this.Conto.Conto,
                importo = (decimal)this.Importo,
                numeroAccertamento = this.Conto.NumeroAccertamento,
                numeroSottoAccertamento = this.Conto.NumeroSottoAccertamento
            };
        }

        internal RateizzazioneType ToRateizzazioneType(DateTime dataScadenza)
        {
            return new RateizzazioneType
            {
                rata = new[]
                {
                    new PosizioneDebitoriaType
                    {
                        riferimentoClient = this.UniqueId,
                        numeroRata = "1",
                        importo = new[]{this.ToImportoPagamentoType() },
                        dataScadenza = dataScadenza
                    }
                }
            };
        }

        internal RegistrazioneContabileType ToRegistrazioneContabileType(SoggettoDebitoreType soggettoDebitore, string riferimentoPratica, DateTime dataScadenza)
        {
            return new RegistrazioneContabileType
            {
                descrizione = $"Pagamento {this.CodiceCausale} per la pratica {riferimentoPratica}",
                causale = new CausaleRegistrazioneType
                {
                    id = this.CodiceCausale,
                    descrizione = this.Descrizione
                },
                soggettoDebitore = soggettoDebitore,
                rate = this.ToRateizzazioneType(dataScadenza),
            };
        }
    }
}
