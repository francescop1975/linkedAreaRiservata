﻿using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.ConversionePDF;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOneri;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneOneri;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using Init.Sigepro.FrontEnd.Infrastructure.Serialization;
using Init.Sigepro.FrontEnd.Pagamenti;
using Init.Sigepro.FrontEnd.Pagamenti.MIP;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.MIP
{
    public static class MIPEsitoPagamentoExtensions
    {
        public static DateTime GetDataPagamento(this MIPEsitoPagamento datiPagamento)
        {
            ILog _log = LogManager.GetLogger(typeof(MIPEsitoPagamentoExtensions));

            if (datiPagamento == null)
            {
                throw new InvalidOperationException("dati pagamento non validi (null)");
            }

            if (!String.IsNullOrEmpty(datiPagamento.DataOraTransazione))
            {
                return DateTime.ParseExact(datiPagamento.DataOraTransazione, "yyyyMMddHHmmss", null);
            }

            if (!String.IsNullOrEmpty(datiPagamento.DataOraOrdine))
            {
                return DateTime.ParseExact(datiPagamento.DataOraOrdine, "yyyyMMddHHmmss", null);
            }

            _log.ErrorFormat("Impossibile ricavare la data e ora della transazione, esito pagamento: {0}", datiPagamento.ToXmlString());

            throw new Exception(String.Format("Impossibile ricavare la data e ora della transazione, riferimeno operazione: {0}", datiPagamento.NumeroOperazione));
        }
    }


    public class PagamentiMIPService
    {
        public class DatiAvvioPagamento
        {
            public readonly string UrlAvvioPagamento;
            public readonly string NumeroOperazione;
            public readonly OnereFrontoffice[] Oneri;

            public DatiAvvioPagamento(string urlAvvioPagamento, string numeroOperazione, IEnumerable<OnereFrontoffice> oneri)
            {
                this.UrlAvvioPagamento = urlAvvioPagamento;
                this.NumeroOperazione = numeroOperazione;
                this.Oneri = oneri.ToArray();
            }
        }



        private readonly MIPPaymentService _mipService;
        private readonly ISalvataggioDomandaStrategy _salvataggioDomandaStrategy;
        private readonly IHtmlToPdfFileConverter _fileConverter; 
        private readonly ILog _log = LogManager.GetLogger(typeof(PagamentiMIPService));
        private readonly IConfigurazione<ParametriConfigurazionePagamentiMIP> _settings;
        private readonly AllegatiInterventoService _oggettiService;
        private readonly OneriDomandaService _oneriDomandaService;

        public PagamentiMIPService(MIPPaymentService mipService, ISalvataggioDomandaStrategy salvataggioDomandaStrategy,
                                     IHtmlToPdfFileConverter fileConverter,
                                    IConfigurazione<ParametriConfigurazionePagamentiMIP> settings, AllegatiInterventoService oggettiService,
                                    OneriDomandaService oneriDomandaService)
        {
            this._mipService = mipService;
            this._salvataggioDomandaStrategy = salvataggioDomandaStrategy;
            this._fileConverter = fileConverter;
            this._settings = settings;
            this._oggettiService = oggettiService;
            this._oneriDomandaService = oneriDomandaService;
        }

        public EsitoPagamentoMip VerificaStatoPagamento(string mipBuffer)
        {
            var datiPagamento = this._mipService.DatiPagamento(mipBuffer);

            return new EsitoPagamentoMip(datiPagamento);
        }

        public EsitoPagamentoMip VerificaStatoPagamento(int idDomanda)
        {
            var domanda = this._salvataggioDomandaStrategy.GetById(idDomanda);
            var ri = domanda.ReadInterface;

            var idOperazione = ri.Oneri.GetOneriOnlineConPagamentoAvviato()
                                        .Select(x => x.IdOperazionePagamento)
                                        .Distinct()
                                        .FirstOrDefault();

            var datiPagamento = this._mipService.GetStatoPagamento(idOperazione);

            return new EsitoPagamentoMip(datiPagamento);
        }

        public DatiAvvioPagamento InizializzaPagamento(EstremiDomanda estremiDomanda)
        {
            var domanda = this._salvataggioDomandaStrategy.GetById(estremiDomanda.IdDomanda);
            var ri = domanda.ReadInterface;
            var numeroOperazione = Guid.NewGuid().ToString();
            var oneriPerPagamentoOnline = ri.Oneri.GetOneriOnlineProntiPerPagamento();

            if (oneriPerPagamentoOnline.Count() == 0)
            {
                throw new InvalidOperationException("La domanda non contiene oneri pagabili tramite pagamento online");
            }

            var importo = GetImporto(oneriPerPagamentoOnline);

            var riferimentiDomanda = new RiferimentiDomanda(domanda.DataKey, estremiDomanda.StepId);
            var riferimentiUtente = new RiferimentiUtente(estremiDomanda.EmailUtente, estremiDomanda.IdentificativoUtente, estremiDomanda.IdentificativoUtente);
            var riferimentiOperazione = new RiferimentiOperazione(numeroOperazione, importo);

            var request = new IniziaPagamentoRequest(riferimentiDomanda, riferimentiUtente, riferimentiOperazione);

            var url = this._mipService.IniziaPagamento(request);

            return new DatiAvvioPagamento(url, numeroOperazione, oneriPerPagamentoOnline);
        }


        public void AvviaPagamento(int idDomanda, string numeroOperazione, IEnumerable<OnereFrontoffice> oneri, bool debugPagamento = true)
        {
            var domanda = this._salvataggioDomandaStrategy.GetById(idDomanda);

            domanda.WriteInterface.Oneri.AvviaPagamentoOneriOnline(numeroOperazione, oneri);

            this._salvataggioDomandaStrategy.Salva(domanda);
        }


        public void AnnullaPagamento(int idDomanda, string mipBuffer)
        {
            try
            {
                var domanda = this._salvataggioDomandaStrategy.GetById(idDomanda);

                try
                {
                    var datiErrore = this._mipService.GetRagioneAnnullamentoPagamento(mipBuffer);

                    _log.ErrorFormat("Annullamento del pagamento per la domanda {0}, Id pagamento: {1}. Dati dell'errore: {2}", idDomanda, datiErrore.NumeroOperazione, datiErrore.ToXmlString());
                }
                catch(Exception ex)
                {
                    _log.Error($"Non è stato possibile reperire informazioni sulla ragione di annulllamento del pagamento con id buffer {mipBuffer}, tutti i pagamenti verranno comunque annullati: {ex}");
                }

                domanda.WriteInterface.Oneri.AnnullaPagamentiFalliti();
                domanda.WriteInterface.Oneri.AnnullaPagamentiInCorso();

                this._salvataggioDomandaStrategy.Salva(domanda);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Errore durante l'annullamento del pagamento per la domanda {0}, id buffer {1}: {2}", idDomanda, mipBuffer, ex.ToString());

                throw;
            }


        }

        public IEnumerable<OnereConPagamentoInSospeso> GetPagamentiInSospeso(int idDomanda)
        {
            var domanda = this._salvataggioDomandaStrategy.GetById(idDomanda);

            var operazioni = domanda.ReadInterface.Oneri.GetOneriOnlineConPagamentoAvviato();
            var result = new List<OnereConPagamentoInSospeso>();

            foreach (var operazione in operazioni)
            {
                var o = new OnereConPagamentoInSospeso
                {
                    Causale = operazione.Causale.Descrizione,
                    Importo = operazione.ImportoPagato.ToString("N2"),
                    IdPosizioneNodoPagamenti = operazione.IdOperazionePagamento,
                    StatoNativo = "PagamentoInCorso"
                };

                try
                {
                    var esito = this._mipService.GetStatoPagamento(o.IdPosizioneNodoPagamenti);

                    o.StatoNativo = esito.EsitoD;
                }
                catch (Exception ex)
                {
                    this._log.ErrorFormat(ex.ToString());
                    o.StatoNativo = "Impossibile verificare lo stato del pagamento";
                }

                result.Add(o);
            }

            return result;
        }

        public void AggiornaStatoPagamentiInSospeso(int idDomanda)
        {
            var domanda = this._salvataggioDomandaStrategy.GetById(idDomanda);
            var operazioni = domanda.ReadInterface.Oneri.GetOneriOnlineConPagamentoAvviato().Select(x => x.IdOperazionePagamento).Distinct();

            foreach (var o in operazioni)
            {
                try
                {
                    var esito = this._mipService.GetStatoPagamento(o);

                    if (esito.Esito == "OK")
                    {
                        PagamentoRiuscito(idDomanda, esito);
                    }

                    if (esito.Esito =="KO")
                    {
                        domanda.WriteInterface.Oneri.AnnullaPagamentiFalliti();
                        domanda.WriteInterface.Oneri.AnnullaPagamentiInCorso();

                        this._salvataggioDomandaStrategy.Salva(domanda);
                    }
                }
                catch (Exception ex)
                {
                    _log.ErrorFormat("Impossibile verificare lo stato del pagamento {0} per la domanda {1}: {2}", o, idDomanda, ex.ToString());
                }
            }

        }

        public void AnnullaPagamentiInSospeso(int idDomanda)
        {
            try
            {
                var domanda = this._salvataggioDomandaStrategy.GetById(idDomanda);

                domanda.WriteInterface.Oneri.AnnullaPagamentiFalliti();
                domanda.WriteInterface.Oneri.AnnullaPagamentiInCorso();

                this._salvataggioDomandaStrategy.Salva(domanda);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Errore durante l'annullamento del pagamento per la domanda {0}: {1}", idDomanda, ex.ToString());

                throw;
            }
        }

        private int GetImporto(IEnumerable<OnereFrontoffice> oneriPerPagamentoOnline)
        {
            var importo = 0.0m;

            foreach (var o in oneriPerPagamentoOnline)
            {
                importo += o.ImportoPagato;
            }

            return Convert.ToInt32(importo * 100.0m);
        }



        public string PagamentoFallito(int idDomanda, MIPEsitoPagamento datiPagamento)
        {
            try
            {
                var domanda = this._salvataggioDomandaStrategy.GetById(idDomanda);
                // var datiErrore = this._mipService.GetRagioneAnnullamentoPagamento(mipBuffer);

                _log.Error($"Pagamento per la domanda {idDomanda} fallito, Id pagamento: {datiPagamento.NumeroOperazione}, id transazione: {datiPagamento.IDTransazione}, numero operazione: {datiPagamento.NumeroOperazione}. Dati esito pagamento: {datiPagamento.ToXmlString()}");

                domanda.WriteInterface.Oneri.AnnullaPagamentiFalliti();
                domanda.WriteInterface.Oneri.AnnullaPagamentiInCorso();

                this._salvataggioDomandaStrategy.Salva(domanda);

                return datiPagamento.EsitoD;
            }
            catch (Exception ex)
            {
                _log.Error($"Errore durante l'annullamento del pagamento per la domanda {idDomanda}, id ordine {datiPagamento.IDOrdine}, id transazione: {datiPagamento.IDTransazione}, numero operazione: {datiPagamento.NumeroOperazione}: {ex}");

                throw;
            }
        }
        /*
        public void PagamentoRiuscito(int idDomanda, string mipBuffer)
        {
            try
            {
                var datiPagamento = this._mipService.DatiPagamento(mipBuffer);
                
                _log.InfoFormat("Pagamento per la domanda {0} riuscito, Id pagamento: {1}. Dati del pagamento: {2}", idDomanda, datiPagamento.NumeroOperazione, datiPagamento.ToXmlString());

                PagamentoRiuscito(idDomanda, datiPagamento);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Errore durante la conferma del pagamento per la domanda {0}, id buffer {1}: {2}", idDomanda, mipBuffer, ex.ToString());

                throw;
            }
        }
        */
        public void PagamentoRiuscito(int idDomanda, MIPEsitoPagamento datiPagamento)
        {
            _log.ErrorFormat("Pagamento riuscito, id domanda {0}, numero operazione: {1}, DataOraTransazione: {2}", idDomanda, datiPagamento.NumeroOperazione, datiPagamento.DataOraTransazione);

            var domanda = this._salvataggioDomandaStrategy.GetById(idDomanda);
            var numeroOperazione = datiPagamento.NumeroOperazione;
            var dataOraTransazione = datiPagamento.GetDataPagamento();
            var numeroTransazione = datiPagamento.NumeroOperazione;
            var idOrdine = datiPagamento.IDOrdine;
            var idTransazione = datiPagamento.IDTransazione;
            var tipoPagamento = GetTipoPagamentoDefault();

            domanda.WriteInterface.Oneri.PagamentoRiuscito(dataOraTransazione, numeroOperazione, idOrdine, idTransazione, tipoPagamento);

            this._salvataggioDomandaStrategy.Salva(domanda);

            // Se possibile genero una ricevuta e la allego alla domanda
            var xml = new RicevutaType(domanda.ReadInterface, datiPagamento, this._settings.Parametri.IntestazioneRicevuta).ToXmlString();
            var xsl = GetXslRicevutaDiPagamento();

            try
            {
                this._log.InfoFormat("Dati della ricevuta di pagamento: {0}", xml);
                this._log.InfoFormat("Formato della ricevuta di pagamento: {0}", xsl);

                // Attenzione, la conversione potrebbe fallire per qualunque ragione
                var ricevuta = this._fileConverter.TrasformaEConverti("RicevutaPagamentoOneri.pdf", xml, xsl);

                //ricevuta.FileName = "RicevutaPagamentoOneri.pdf";

                this._oggettiService.AggiungiAllegatoLibero(idDomanda, "Ricevuta pagamento oneri", ricevuta);

                this._log.DebugFormat("Ricevuta di pagamento allegata con successo");
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Non è stato pssibile allegare la ricevuta di pagamento oneri alla domanda {0}, dettagli dell'errore: {1}", idDomanda, ex.ToString());
            }
        }

        public TipoPagamento GetTipoPagamentoDefault()
        {
            return this._oneriDomandaService.GetListaModalitaPagamento().Where(x => x.Codice == this._settings.Parametri.TipoPagamento.ToString()).FirstOrDefault();
        }

        private string GetXslRicevutaDiPagamento()
        {
            var path = HttpContext.Current.Server.MapPath(this._settings.Parametri.XslRicevutaPagamento);

            return File.ReadAllText(path);
        }

        public void AnnullaTuttiIPagamenti(int idDomanda)
        {
            var domanda = this._salvataggioDomandaStrategy.GetById(idDomanda);

            domanda.WriteInterface.Oneri.AnnullaPagamentiFalliti();
            domanda.WriteInterface.Oneri.AnnullaPagamentiInCorso();

            this._salvataggioDomandaStrategy.Salva(domanda);
        }

        public bool VerticalizzazioneAttiva()
        {
            return _settings.Parametri.VerticalizzazioneAttiva;
        }
    }
}
