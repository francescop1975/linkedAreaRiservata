using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOneri;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneOneri;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using Init.Sigepro.FrontEnd.AppLogic.Utils.SerializationExtensions;
using Init.Sigepro.FrontEnd.Pagamenti;
using Init.Sigepro.FrontEnd.Pagamenti.ENTRANEXT;
using Init.Sigepro.FrontEnd.Pagamenti.EntraNextService;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.EntraNext
{
    public class PagamentiEntraNextService
    {
        public class PagamentiDatiExtra
        {
            public string IdTransazione { get; set; }
            public string CodicePagamento { get; set; }
            public string Iuv { get; set; }
        }


        public static class Constants
        {
            public const string DatiPagamentiExtra = "DatiPagamentiExtra";
        }

        private readonly EntraNextPaymentService _entraNextService;
        private readonly ISalvataggioDomandaStrategy _salvataggioDomandaStrategy;
        private readonly ILog _log = LogManager.GetLogger(typeof(PagamentiEntraNextService));
        private readonly OneriDomandaService _oneriDomandaService;
        private readonly AllegatiInterventoService _oggettiService;

        public enum VerificaPagamentoEnum { DIFFERITO, OK, FALLITO }

        public PagamentiEntraNextService(EntraNextPaymentService entraNextService, ISalvataggioDomandaStrategy salvataggioDomandaStrategy, OneriDomandaService oneriDomandaService, AllegatiInterventoService oggettiService)
        {
            _entraNextService = entraNextService;
            _salvataggioDomandaStrategy = salvataggioDomandaStrategy;
            _oneriDomandaService = oneriDomandaService;
            _oggettiService = oggettiService;
        }

        public DatiAvvioPagamentiEntraNext InizializzaPagamento(EstremiDomandaEntraNext estremiDomanda)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(estremiDomanda.IdDomanda);
            var ri = domanda.ReadInterface;
            var numeroOperazione = DateTime.Now.Ticks.ToString();
            var oneriPerPagamentoOnline = ri.Oneri.GetOneriOnlineProntiPerPagamento();

            if (oneriPerPagamentoOnline.Count() == 0)
            {
                throw new InvalidOperationException("La domanda non contiene oneri pagabili tramite pagamento online");
            }

            var oneri = oneriPerPagamentoOnline.Select(x => new OneriEntraNextDTO(
                x.Causale.Descrizione,
                x.ImportoPagato,
                1,
                0,
                null,
                null,
                _oneriDomandaService.GetCodiceCausaleOnereTraslazione(x.Causale.Codice)
                ));

            var riferimentiDomanda = new RiferimentiDomandaEntranext(domanda.DataKey, estremiDomanda.StepId, domanda.ReadInterface.AltriDati.Intervento.DescrizioneBreve);
            var riferimentiUtente = new RiferimentiUtenteEntraNext(estremiDomanda.Email, estremiDomanda.CodiceFiscale, estremiDomanda.CodiceFiscale, estremiDomanda.RagioneSociale, estremiDomanda.Nome, estremiDomanda.Cognome, estremiDomanda.PartitaIva, estremiDomanda.Indirizzo, estremiDomanda.Comune, estremiDomanda.Provincia, estremiDomanda.Cap, estremiDomanda.Localita, estremiDomanda.TipoSoggetto);
            var riferimentiOperazione = new RiferimentiOperazioneEntraNext(numeroOperazione, oneri);

            var request = new IniziaPagamentoEntraNextRequest(riferimentiDomanda, riferimentiUtente, riferimentiOperazione);

            var response = _entraNextService.IniziaPagamento(request);

            domanda.WriteInterface.DatiExtra.SetValoreDato(Constants.DatiPagamentiExtra, new PagamentiDatiExtra { CodicePagamento = numeroOperazione, IdTransazione = response.IdentificativoTransazione }.ToXmlString());
            _salvataggioDomandaStrategy.Salva(domanda);

            return new DatiAvvioPagamentiEntraNext(response.Url, numeroOperazione, oneriPerPagamentoOnline);
        }

        public void AnnullaPagamento(int idDomanda)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            domanda.WriteInterface.Oneri.AnnullaPagamentiFalliti();
            domanda.WriteInterface.Oneri.AnnullaPagamentiInCorso();

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public void AvviaPagamento(int idDomanda, string numeroOperazione, IEnumerable<OnereFrontoffice> oneri, bool debugPagamento = true)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            domanda.WriteInterface.Oneri.AvviaPagamentoOneriOnline(numeroOperazione, oneri);

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        private void PagamentoAccettato(int idDomanda, string codicePagamento, string idTransazione)
        {
            try
            {
                if (VerificaPagamento(idDomanda, idTransazione))
                {
                    _log.Info($"Gli oneri della domanda {idDomanda} risultano essere già pagati");
                    return;
                }

                var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);
                var verifica = _entraNextService.VerificaPosizione(codicePagamento);

               

                if (verifica.Esito == "OK" )
                {
                    var dataOraTransazione = verifica.PosizioneDebitoria.DataFinePeriodo.GetValueOrDefault(DateTime.Now);

                    var tipoPagamento = _oneriDomandaService.GetListaModalitaPagamento().Where(x => x.Codice == _entraNextService.Settings.TipoPagamento).FirstOrDefault();
                    _log.Info($"tipopagamento is null? {tipoPagamento == null}");

                    var rifPraticaEsterna = verifica.PosizioneDebitoria.RiferimentoPraticaEsterna;
                    var iuv = verifica.PosizioneDebitoria.IUV;

                    //inserisce dati extra
                    domanda.WriteInterface.DatiExtra.SetValoreDato(Constants.DatiPagamentiExtra, new PagamentiDatiExtra { CodicePagamento = rifPraticaEsterna, IdTransazione = idTransazione, Iuv = iuv }.ToXmlString());

                    _log.Info($"Pagamento per la domanda {idDomanda} riuscito, Id pagamento: {iuv}.");
                    domanda.WriteInterface.Oneri.PagamentoRiuscito(dataOraTransazione, rifPraticaEsterna, iuv, idTransazione, tipoPagamento);

                    _salvataggioDomandaStrategy.Salva(domanda);

                    // aggiungo gli eventuali allegati alla domanda
                    var responseRicevutaPagamento = _entraNextService.GetRicevutaPagamento(verifica.PosizioneDebitoria.IUV);

                    if (responseRicevutaPagamento.PagamentiPosizioniDebitorie?.Length > 0)
                    {
                        // Il pagamento è andato a buon fine ma non esiste una ricevuta. Considero comunque il pagamento come andato a buon fine
                        if (responseRicevutaPagamento.PagamentiPosizioniDebitorie[0].Documento != null)
                        {
                            var binaryFileXml = BinaryFile.FromFileData("RicevutaPagamentoOneri.xml", "application/xml", responseRicevutaPagamento.PagamentiPosizioniDebitorie[0].Documento);
                            _log.Info($"Inserimento dell'allegato della ricevuta di pagamento xml come allegato libero, id domanda: {idDomanda}, id transazione: {idTransazione}");
                            _oggettiService.AggiungiAllegatoLibero(idDomanda, "Ricevuta pagamento oneri", binaryFileXml);
                        }
                        else
                        {
                            _log.Error($"Ricevuta pagamento oneri non presente, id domanda: {idDomanda}, id transazione: {idTransazione}");
                            //throw new Exception("Documento quietanza non presente");
                        }

                        _log.Info($"Inserimento dell'allegato della ricevuta di pagamento xml come allegato libero inserito con successo, id domanda: {idDomanda}, id transazione: {idTransazione}");
                        if (responseRicevutaPagamento.PagamentiPosizioniDebitorie[0].DocumentoQuietanza != null)
                        {
                            var binaryFilePdf = BinaryFile.FromFileData("RicevutaPagamentoOneri.pdf", "application/pdf", responseRicevutaPagamento.PagamentiPosizioniDebitorie[0].DocumentoQuietanza);
                            _log.Info($"Inserimento dell'allegato della ricevuta di pagamento pdf come allegato libero, id domanda: {idDomanda}, id transazione: {idTransazione}");
                            _oggettiService.AggiungiAllegatoLibero(idDomanda, "Ricevuta pagamento oneri", binaryFilePdf);
                            _log.Info($"Inserimento dell'allegato della ricevuta di pagamento pdf come allegato libero inserito con successo, id domanda: {idDomanda}, id transazione: {idTransazione}");
                        }
                        else
                        {
                            _log.Error($"Documento quietanza non presente, id domanda: {idDomanda}, id transazione: {idTransazione}");
                            //throw new Exception("Documento quietanza non presente");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error($"Errore generato durante il salvataggio del pagamento, iddomanda: {idDomanda}, codicepagamento: {codicePagamento}, idtransazione: {idTransazione}, errore: {ex.ToString()}");
            }
        }

        public void SalvaPagamentoNotificato(int idDomanda, string codicePagamento, string idTransazione)
        {
            try
            {
                var stato = GetEsitoTransazione(idTransazione);
                if (stato != StatoPagamentoPagoPA.PagamentoAccettato)
                {
                    return;
                }

                PagamentoAccettato(idDomanda, codicePagamento, idTransazione);
            }
            catch (Exception ex)
            {
                _log.Info($"Errore nella notifica del Pagamento per la domanda {idDomanda}, Codice pagamento: {codicePagamento}, errore: {ex.ToString()}");
            }
        }

        public StatoPagamentoPagoPA GetEsitoTransazione(string idTransazione)
        {
            var stato = _entraNextService.GetEsitoTransazione(idTransazione);
            return stato.EsitoTransazione.Stato;
        }

        public bool VerificaPagamento(int idDomanda, string idTransazione)
        {
            try
            {
                var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

                if (domanda.ReadInterface.DatiExtra == null)
                {
                    // ? se non sono presenti dati extra contenenti i riferimenti dell'operazione allora li crea... perché???
                    var operazioniInSospeso = domanda.ReadInterface.Oneri.GetOneriOnlineConPagamentoAvviato();

                    if (operazioniInSospeso != null && operazioniInSospeso.Any())
                    {
                        domanda.WriteInterface.DatiExtra.SetValoreDato(Constants.DatiPagamentiExtra,
                            new PagamentiDatiExtra
                            {
                                CodicePagamento = operazioniInSospeso.ElementAt(0).IdOperazionePagamento,
                                IdTransazione = idTransazione
                            }.ToXmlString());

                        _salvataggioDomandaStrategy.Salva(domanda);
                    }
                }

                _log.Info($"VerificaPagamento per la domanda={idDomanda} e idTransazione={idTransazione}, TotalePagato={domanda.ReadInterface.Oneri.TotalePagato}");

                return domanda.ReadInterface.Oneri.TotalePagato > 0;
            }
            catch (Exception ex)
            {
                _log.Error($"Errore in VerificaPagamento per idDomanda={idDomanda} e idTransazione={idTransazione}: {ex.ToString()}");
                return false;
            }
        }

        public void AggiornaStatoPagamentiInSospeso(int idDomanda)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);
            var datiExtra = domanda.ReadInterface.DatiExtra.Get<PagamentiDatiExtra>(Constants.DatiPagamentiExtra);

            if (datiExtra == null)
            {
                _log.Info($"Id domanda {idDomanda}: Non è stato possibile recuperare i dati extra della domanda {idDomanda}");
                return;
            }

            var stato = GetEsitoTransazione(datiExtra.IdTransazione);

            if (stato == StatoPagamentoPagoPA.PagamentoAccettato)
            {
                _log.Info($"Id domanda {idDomanda}: pagamento con iuv {datiExtra.Iuv}, codice {datiExtra.CodicePagamento} in stato {stato}");
                PagamentoAccettato(idDomanda, datiExtra.CodicePagamento, datiExtra.IdTransazione);

                return;
            }

            if (stato == StatoPagamentoPagoPA.PagamentoAnnullato || stato == StatoPagamentoPagoPA.PagamentoRifiutato)
            {
                _log.Info($"Id domanda {idDomanda}: Pagamento con iuv {datiExtra.Iuv}, codice {datiExtra.CodicePagamento} risulta in stato: {stato}, l'onere sarà annullato");
                domanda.WriteInterface.Oneri.AnnullaPagamentiFalliti();
                domanda.WriteInterface.Oneri.AnnullaPagamentiInCorso();
                _salvataggioDomandaStrategy.Salva(domanda);

                return;
            }

            _log.Info($"Id domanda {idDomanda}: Il pagamento con iuv {datiExtra.Iuv}, codice {datiExtra.CodicePagamento} risulta essere ancora in sospeso, stato {stato}");
        }

        public IEnumerable<OnereConPagamentoInSospeso> GetPagamentiInSospeso(int idDomanda)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);
            var operazioni = domanda.ReadInterface.Oneri.GetOneriOnlineConPagamentoAvviato();

            var result = new List<OnereConPagamentoInSospeso>();

            var datiExtra = domanda.ReadInterface.DatiExtra.GetValoreDato(Constants.DatiPagamentiExtra).DeserializeXML<PagamentiDatiExtra>();

            foreach (var operazione in operazioni)
            {
                var o = new OnereConPagamentoInSospeso
                {
                    Causale = operazione.Causale.Descrizione,
                    Importo = operazione.ImportoPagato.ToString("N2"),
                    IdPosizioneNodoPagamenti = operazione.IdOperazionePagamento,
                    StatoNativo = ""
                };

                try
                {
                    var esito = GetEsitoTransazione(datiExtra.IdTransazione);
                    if (esito == StatoPagamentoPagoPA.PagamentoAccettato)
                    {
                        return null;
                    }

                    o.StatoNativo = esito.ToString();
                }
                catch (Exception ex)
                {
                    _log.Error($"PagamentiEntraNextService.GetPagamentiInSospeso: Non è stato possibile verificare lo stato" +
                                    $" dell'onere con id transazione={datiExtra.IdTransazione}, id operazione={operazione.IdOperazionePagamento}. Errore: {ex}");

                    o.StatoNativo = "Impossibile verificare lo stato del pagamento";
                }

                result.Add(o);
            }

            return result;
        }
    }
}
