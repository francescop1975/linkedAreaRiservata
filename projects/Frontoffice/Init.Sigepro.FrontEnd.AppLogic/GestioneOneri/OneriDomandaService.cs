﻿// -----------------------------------------------------------------------
// <copyright file="OneriDomandaService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneOneri
{
    using Init.Sigepro.FrontEnd.AppLogic.AllegatiDomanda;
    using Init.Sigepro.FrontEnd.AppLogic.Common;
    using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
    using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class OneriDomandaService
    {

        ILog _log = LogManager.GetLogger(typeof(OneriDomandaService));
        ISalvataggioDomandaStrategy _salvataggioDomandaStrategy;
        IAllegatiDomandaFoRepository _allegatiDomandaFoRepository;
        IAliasSoftwareResolver _aliasSoftwareResolver;
        ILogicaSincronizzazioneOneri _logicaSincronizzazioneOneri;
        IOneriRepository _oneriRepository;

        public OneriDomandaService(ISalvataggioDomandaStrategy salvataggioDomandaStrategy, IAllegatiDomandaFoRepository allegatiDomandaFoRepository, IAliasSoftwareResolver aliasSoftwareResolver, ILogicaSincronizzazioneOneri logicaSincronizzazioneOneri, IOneriRepository oneriRepository)
        {
            _salvataggioDomandaStrategy = salvataggioDomandaStrategy;
            _allegatiDomandaFoRepository = allegatiDomandaFoRepository;
            _aliasSoftwareResolver = aliasSoftwareResolver;
            _logicaSincronizzazioneOneri = logicaSincronizzazioneOneri;
            _oneriRepository = oneriRepository;
        }

        public void SincronizzaOneri(int idDomanda, ComportamentoSincronizzazioneOneriSenzaImporto comportamentoOneriSenzaImporto)
        {
            try
            {
                _log.Debug("inizio sincronizzazione oneri domanda");

                var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

                _logicaSincronizzazioneOneri.ComportamentoOneriSenzaImporto = comportamentoOneriSenzaImporto;
                _logicaSincronizzazioneOneri.SincronizzaOneri(domanda);

                _salvataggioDomandaStrategy.Salva(domanda);

                _log.Debug("sincronizzazione oneri domanda terminata");
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Errore durante la sincronizzazione degli oneri della domanda: {0}", ex.ToString());

                throw;
            }
        }

        public void SpecificaEstremiPagamento(int idDomanda, IEnumerable<OnerePagato> oneriPagati)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            foreach (var o in oneriPagati)
            {
                domanda.WriteInterface.Oneri.ForzaEstremiPagamento(o.Id, o.ModalitaPagamento, o.EstremiPagamento);
            }

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public void SpecificaEstremiPagamentoOneriNonPagatiOnline(int idDomanda, IEnumerable<OnerePagato> oneriPagati)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            domanda.WriteInterface.Oneri.CancellaEstremiPagamentoOneriNonPagatiOnline();

            foreach (var o in oneriPagati)
            {
                domanda.WriteInterface.Oneri.ImpostaEstremiPagamentoOneriNonPagatiOnline(o.Id, o.ModalitaPagamento, o.EstremiPagamento);
            }

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public void InserisciAttestazioneDiPagamento(int idDomanda, BinaryFile allegato)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            var esitoSalvataggio = _allegatiDomandaFoRepository.SalvaAllegato(idDomanda, allegato, false);

            domanda.WriteInterface.Oneri.SalvaAttestazioneDiPagamento(esitoSalvataggio.CodiceOggetto, esitoSalvataggio.NomeFile, esitoSalvataggio.FirmatoDigitalmente);

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public void ImpostaDichiarazioneDiAssenzaOneriDaPagare(int idDomanda)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            domanda.WriteInterface.Oneri.ImpostaDichiarazioneAssenzaOneriDaPagare();

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public void RimuoviDichiarazioneDiAssenzaOneriDaPagare(int idDomanda)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            domanda.WriteInterface.Oneri.RimuoviDichiarazioneAssenzaOneriDaPagare();

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public void EliminaAttestazioneDiPagamento(int idDomanda)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            domanda.WriteInterface.Oneri.EliminaAttestazioneDiPagamento();

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public IEnumerable<TipoPagamento> GetListaModalitaPagamento()
        {
            return
                    _oneriRepository
                    .GetModalitaPagamento()
                    .Select(x => new TipoPagamento(x.Codice, x.Descrizione));

        }

        public string GetCodiceCausaleOnereTraslazione(int idCausale)
        {
            return _oneriRepository.GetCodiceCausaleOnereTraslazione(idCausale);
        }
    }
}
