using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.Pagamenti.NODOPAGAMENTI.Attivazione
{
    public class EsitoAttivazioneSessionePagamento : IEsitoAttivazionePagamento
    {
        public bool Esito { get; }
        public string UrlSistemaPagamenti { get; }
        public string DescrizioneErrore { get; } = string.Empty;

        public IEnumerable<IEstremiPosizioneDebitoria> PosizioniAttivate { get; } = Enumerable.Empty<IEstremiPosizioneDebitoria>();

        public static EsitoAttivazioneSessionePagamento AttivazioneFallita(string messaggio) => new EsitoAttivazioneSessionePagamento(false, messaggio);

        private EsitoAttivazioneSessionePagamento(bool esito, string messaggio)
        {
            this.Esito = esito;
            this.DescrizioneErrore = messaggio;
        }

        public EsitoAttivazioneSessionePagamento(AttivaSessionePagamentoResponseType response, EsitoOperazionePosizioneDebitoriaType riferimentiPosizioneInserita)
        {
            this.Esito = response.esito;

            if (response.esito)
            {
                this.UrlSistemaPagamenti = response.payUrl;
                this.PosizioniAttivate = new[]
                {
                    new EstremiPosizioneDebitoria(riferimentiPosizioneInserita.riferimentoClient, riferimentiPosizioneInserita.idPosizione, riferimentiPosizioneInserita.IUV)
                };
            }
            else
            {
                DescrizioneErrore = response.descEsito;
            }
        }
    }
}
