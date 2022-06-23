using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.Pagamenti.NODOPAGAMENTI.Attivazione
{
    public class EsitoInserimentoPosizioneDebitoria : IEsitoAttivazionePagamento
    {
        OperazionePosizioniDebitorieResponseType _esitoInserimentoPosizioneDebitoria;

        public EsitoInserimentoPosizioneDebitoria(OperazionePosizioniDebitorieResponseType esitoInserimentoPosizioneDebitoria)
        {
            this._esitoInserimentoPosizioneDebitoria = esitoInserimentoPosizioneDebitoria ?? throw new ArgumentNullException(nameof(esitoInserimentoPosizioneDebitoria));
        }

        public bool Esito => this._esitoInserimentoPosizioneDebitoria.esito == EsitoType.OK;

        public string DescrizioneErrore => this._esitoInserimentoPosizioneDebitoria?.messaggio ?? "Dettagli dell'errore non disponibili";

        public string UrlSistemaPagamenti => throw new InvalidOperationException("L'attivazione di una posizione debitoria non prevede un url di pagamento");

        public IEnumerable<IEstremiPosizioneDebitoria> PosizioniAttivate => !this.Esito ?
            Enumerable.Empty< IEstremiPosizioneDebitoria >():
            this._esitoInserimentoPosizioneDebitoria.posizioniInserite.Select(x => new EstremiPosizioneDebitoria(x.riferimentoClient, x.idPosizione, x.IUV));
    }
}
