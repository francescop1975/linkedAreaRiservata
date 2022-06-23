﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti;
using CuttingEdge.Conditions;
using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.EndoAcquisiti;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneEndoprocedimenti.Sincronizzazione
{
    public class LogicaSincronizzazioneEndo
    {
        private readonly IEndoprocedimentiService _endoprocedimentiService;
        private readonly IEndoAcquisitiService _endoAcquisitiService;
        private readonly DomandaOnline _domanda;

        internal LogicaSincronizzazioneEndo(DomandaOnline domanda, IEndoprocedimentiService endoprocedimentiService, IEndoAcquisitiService endoAcquisitiService)
        {
            this._endoprocedimentiService = endoprocedimentiService ?? throw new ArgumentNullException(nameof(endoprocedimentiService));
            _endoAcquisitiService = endoAcquisitiService ?? throw new ArgumentNullException(nameof(endoAcquisitiService));
            this._domanda = domanda ?? throw new ArgumentNullException(nameof(domanda));
        }

        internal void Sincronizza(IEnumerable<int> idNuoviEndoprocedimenti)
        {
            //var nuoviEndoprocedimenti = EstraiListaEndoDaListaId(domanda.AliasComune, idNuoviEndoprocedimenti);
            var aliasComune = this._domanda.ReadInterface.AltriDati.AliasComune;
            var idIntervento = this._domanda.ReadInterface.AltriDati.Intervento.Codice;
            var codiceComune = this._domanda.ReadInterface.AltriDati.CodiceComune;

            var listaEndoDb = _endoprocedimentiService.GetListaEndoDaIdIntervento(codiceComune, idIntervento).FiltraEndoByListaId(idNuoviEndoprocedimenti);
            var listaTipiTitolo = _endoAcquisitiService.TipiTitoloWhereCodiceInventarioIn(idNuoviEndoprocedimenti);

            var nuoviEndoprocedimenti = from endoDb in listaEndoDb
                                        select new
                                        {
                                            Codice = endoDb.Codice,
                                            Descrizione = endoDb.Descrizione,
                                            Principale = endoDb.Principale,
                                            CodiceNatura = endoDb.CodiceNatura,
                                            DescrizioneNatura = endoDb.Natura,
                                            Facoltativo = false,// ???
                                            BinarioDipendenze = endoDb.BinarioDipendenze,
                                            PermetteVerificaAcquisizione = !endoDb.Principale && listaTipiTitolo.ContainsKey(endoDb.Codice),
                                            TipoTitoloObbligatorio = endoDb.TipoTitoloObbligatorio,
                                            Ordine = endoDb.Ordine,
                                            OrdineTipo = endoDb.OrdineTipo,
                                            OrdineFamiglia = endoDb.OrdineFamiglia
                                        };

            //var codiciEndoDaEliminare = new List<int>();

            // TODO: Effettuare qui la puizia dei dati della domanda.
            // Se un endo della domanda non esiste nella lista degli endo selezionati
            // 1. Elimina gli allegati relativi all'endo
            // 2. Elimina i modelli dinamici relativi all'endo
            // 3. Elimina l'endo dalla domanda
            var endoDaEliminare = this._domanda.ReadInterface
                                                .Endoprocedimenti
                                                .Endoprocedimenti
                                                .Where(r => nuoviEndoprocedimenti.FirstOrDefault(x => x.Codice == r.Codice) == null)
                                                .Select(r => r.Codice);

            foreach (var codiceEndo in endoDaEliminare)
                this._domanda.WriteInterface.Endoprocedimenti.Elimina(codiceEndo);


            // Aggiungo i nuovi endo alla domanda
            foreach (var nuovoEndo in nuoviEndoprocedimenti)
            {
                this._domanda.WriteInterface.Endoprocedimenti.AggiungiOAggiorna(nuovoEndo.Codice,
                                                                            nuovoEndo.Descrizione,
                                                                            nuovoEndo.Principale,
                                                                            nuovoEndo.CodiceNatura,
                                                                            nuovoEndo.DescrizioneNatura,
                                                                            nuovoEndo.Facoltativo,
                                                                            nuovoEndo.BinarioDipendenze,
                                                                            nuovoEndo.PermetteVerificaAcquisizione,
                                                                            nuovoEndo.TipoTitoloObbligatorio,
                                                                            new OrdinamentoEndo(nuovoEndo.OrdineFamiglia, nuovoEndo.OrdineTipo, nuovoEndo.Ordine)
                                                                            );
            }
        }
    }
}
