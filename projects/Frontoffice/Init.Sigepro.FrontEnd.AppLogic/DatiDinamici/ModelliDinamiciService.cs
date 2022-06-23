using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.AllegatiDomanda;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.ConversionePDF;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.SchedeDomanda;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.GenerazionePdfModelli;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.StrutturaModelli;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo.LetturaDatiDinamici.LetturaDaDomandaOnline;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneDatiDinamici;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneDatiDinamici.Sincronizzazione;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using Init.Sigepro.FrontEnd.AppLogic.Utils;
using Init.SIGePro.DatiDinamici;
using Init.SIGePro.Manager.DTO.DatiDinamici;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici
{
    public class ModelliDinamiciService : IModelliDinamiciService
    {
        public class IdRiepilogoRichiesto
        {
            public readonly int IdModello;
            public readonly string Descrizione;
            public readonly int IndiceMolteplicita;

            public IdRiepilogoRichiesto(int idModello, string descrizione, int indiceMolteplicita=-1)
            {
                this.IdModello = idModello;
                this.Descrizione = descrizione;
                this.IndiceMolteplicita = indiceMolteplicita;
            }
        }


        private readonly IAliasResolver _aliasResolver;
        private readonly IAllegatiDomandaFoRepository _allegatiDomandaFoRepository;
        private readonly IConfigurazione<ParametriWorkflow> _configurazione;
        private readonly IConfigurazione<ParametriSchedaCittadiniExtracomunitari> _configurazioneSchedaCittadiniEC;
        private readonly IDatiDinamiciRepository _datiDinamiciRepository;
        private readonly ILog _log = LogManager.GetLogger(typeof(ModelliDinamiciService));
        private readonly RiepilogoModelloInHtmlFactory _riepilogoModelloInHtmlFactory;
        private readonly ISalvataggioDomandaStrategy _salvataggioDomandaStrategy;
        private readonly IStrutturaModelloReader _strutturaModelloReader;
        private readonly ITokenApplicazioneService _tokenApplicazioneService;
        private readonly IHtmlToPdfFileConverter _fileConverter;
        private readonly IIstanzaSigeproAdapterService _istanzaSigeproAdapterService;

        public ModelliDinamiciService(ISalvataggioDomandaStrategy salvataggioDomandaStrategy, IAliasResolver aliasResolver,
                                        IAllegatiDomandaFoRepository allegatiDomandaFoRepository, IDatiDinamiciRepository datiDinamiciRepository,
                                        IConfigurazione<ParametriWorkflow> configurazione, IConfigurazione<ParametriSchedaCittadiniExtracomunitari> configurazioneSchedaCittadiniEC,
                                        ITokenApplicazioneService tokenApplicazioneService, IStrutturaModelloReader strutturaModelloReader,
                                        RiepilogoModelloInHtmlFactory riepilogoModelloInHtmlFactory, IHtmlToPdfFileConverter fileConverter,
                                        IIstanzaSigeproAdapterService istanzaSigeproAdapterService)
        {
            this._salvataggioDomandaStrategy = salvataggioDomandaStrategy;
            this._aliasResolver = aliasResolver;
            this._allegatiDomandaFoRepository = allegatiDomandaFoRepository;
            this._datiDinamiciRepository = datiDinamiciRepository;
            this._configurazione = configurazione;
            this._configurazioneSchedaCittadiniEC = configurazioneSchedaCittadiniEC;
            this._tokenApplicazioneService = tokenApplicazioneService;
            this._strutturaModelloReader = strutturaModelloReader;
            this._riepilogoModelloInHtmlFactory = riepilogoModelloInHtmlFactory;
            this._fileConverter = fileConverter;
            _istanzaSigeproAdapterService = istanzaSigeproAdapterService;
        }

        public void AggiungiOggettoRiepilogo(int idDomanda, int idModello, int indiceMolteplicita, BinaryFile file, bool ignoraVerificaFirma = false)
        {
            try
            {
                var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

                var aliasComune = _aliasResolver.AliasComune;

                var datiModello = domanda.ReadInterface.DatiDinamici.Modelli.Where(x => x.IdModello == idModello).FirstOrDefault();
                var rigaRiepilogo = domanda.ReadInterface.RiepiloghiSchedeDinamiche.GetByIdModelloIndiceMolteplicita(idModello, indiceMolteplicita);

                var verificaFirma = datiModello.TipoFirma != ModelloDinamico.TipoFirmaEnum.Nessuna;
                SalvataggioAllegatoResult esitoSalvataggio = null;

                if (ignoraVerificaFirma)
                {
                    verificaFirma = false;
                }

                if (verificaFirma && _configurazione.Parametri.VerificaHashFilesFirmati)
                {
                    var md5Confronto = rigaRiepilogo.HashConfronto;

                    esitoSalvataggio = _allegatiDomandaFoRepository.SalvaAllegatoConfrontaHash(idDomanda, file, md5Confronto);
                }
                else
                {
                    esitoSalvataggio = _allegatiDomandaFoRepository.SalvaAllegato(idDomanda, file, verificaFirma);
                }

                domanda.WriteInterface.RiepiloghiSchedeDinamiche.SalvaOggettoRiepilogo(idModello, indiceMolteplicita, esitoSalvataggio.CodiceOggetto, esitoSalvataggio.NomeFile, esitoSalvataggio.FirmatoDigitalmente);

                _salvataggioDomandaStrategy.Salva(domanda);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Errore in AggiungiOggettoRiepilogo: {0}", ex.ToString());

                throw;
            }
        }

        public void EliminaModello(int idDomanda, int idModello, int indice)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            /*
			var idModello = modelloDinamico.IdModello;

			modelloDinamico.DataAccessProvider = new Dyn2DataAccessProvider(domanda, idModello, _tokenApplicazioneService);
			modelloDinamico.Elimina();
			*/
            var modello = GetModelloDinamico(domanda, idModello, indice);

            modello.Elimina();

            /*if (modello.ErroriScript.Count() > 0)
				throw new SalvataggioModelloDinamicoException(modello.ErroriScript);*/

            domanda.WriteInterface.RiepiloghiSchedeDinamiche.EliminaByIdModello(idModello);

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public void EliminaOggettoRiepilogo(int idDomanda, int idModello, int indiceMolteplicita)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            domanda.WriteInterface.RiepiloghiSchedeDinamiche.EliminaOggettoRiepilogo(idModello, indiceMolteplicita);

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public IEnumerable<int> GetIndiciScheda(int idDomanda, int idScheda)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            var strutturaModello = this._strutturaModelloReader.Read(idScheda);

            return domanda.ReadInterface.DatiDinamici.GetIndiciSchede(strutturaModello);
        }

        public ModelloDinamicoIstanza GetModelloDinamico(int idDomanda, int idScheda, int indiceScheda)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            return GetModelloDinamico(domanda, idScheda, indiceScheda);
        }

        private ModelloDinamicoIstanza GetModelloDinamico(DomandaOnline domanda, int idScheda, int indiceScheda)
        {
            var istanza = this._istanzaSigeproAdapterService.ToIstanzaBackoffice(domanda.ReadInterface);
            var cache = this._datiDinamiciRepository.GetCacheModelloDinamico(idScheda);
            var dap = new DomandaOnlineDataAccessFactory(cache, domanda, _tokenApplicazioneService, istanza);
            var loader = new ModelloDinamicoLoader(dap, _aliasResolver.AliasComune, ModelloDinamicoLoader.TipoModelloDinamicoEnum.Frontoffice);
            var scheda = new ModelloDinamicoIstanza(loader, idScheda, indiceScheda, false);

            return scheda;
        }

        public void RigeneraRiepiloghi(int idDomanda, bool generaRiepilogoSchedeCheNonRichiedonoFirma)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);
            var reader = new DomandaOnlineDatiDinamiciReader(domanda, this._datiDinamiciRepository, _istanzaSigeproAdapterService);
            var generatoreRiepilogo = new GeneratoreRiepilogoModelloDinamico(reader, this._riepilogoModelloInHtmlFactory, this._fileConverter);

            // 1. Elimino i modelli non compilati
            var modelliOld = domanda.ReadInterface.DatiDinamici.Modelli.Where(x => !x.Compilato).ToList();

            foreach(var modello in modelliOld)
            {
                domanda.WriteInterface.RiepiloghiSchedeDinamiche.EliminaByIdModello(modello.IdModello);
            }

            // 2. Genero i modelli che richiedono firma a blocchi
            var listaRiepiloghiRichiesti = new List<IdRiepilogoRichiesto>();
            var modelliConFirmaABlocchi = domanda.ReadInterface.DatiDinamici.Modelli.Where(x => x.Compilato && x.TipoFirma == ModelloDinamico.TipoFirmaEnum.ABlocchi);

            foreach(var modello in modelliConFirmaABlocchi)
            {
                for (int indiceMolteplicita = 0; indiceMolteplicita < modello.MaxMolteplicita + 1; indiceMolteplicita++)
                {
                    listaRiepiloghiRichiesti.Add(new IdRiepilogoRichiesto(modello.IdModello, modello.Descrizione, indiceMolteplicita));
                }
            }

            // 3. Genero i modelli che richiedono firma singola
            var modelliConFirmaSingola = domanda.ReadInterface.DatiDinamici.Modelli.Where(x => x.Compilato && x.TipoFirma == ModelloDinamico.TipoFirmaEnum.InteroDocumento);

            foreach (var modello in modelliConFirmaSingola)
            {
                listaRiepiloghiRichiesti.Add(new IdRiepilogoRichiesto(modello.IdModello, modello.Descrizione));
            }

            // 4. Se è richiesto anche il riepilogo delle schede che non richiedono firma rigenero e allego anche questi
            if (generaRiepilogoSchedeCheNonRichiedonoFirma)
            {
                var modelliCheNonRichiedonoFirma = domanda.ReadInterface.DatiDinamici.Modelli.Where(x => x.Compilato && x.TipoFirma == ModelloDinamico.TipoFirmaEnum.Nessuna);

                foreach (var modello in modelliCheNonRichiedonoFirma)
                {
                    listaRiepiloghiRichiesti.Add(new IdRiepilogoRichiesto(modello.IdModello, modello.Descrizione));
                }
            }

            // 5. Salvo i documenti generati
            foreach(var idRiepilogo in listaRiepiloghiRichiesti)
            {
                domanda.WriteInterface.RiepiloghiSchedeDinamiche.AggiungiOAggiorna(idRiepilogo.IdModello, idRiepilogo.IndiceMolteplicita, idRiepilogo.Descrizione);

                if (domanda.WriteInterface.RiepiloghiSchedeDinamiche.HaRiepilogo(idRiepilogo.IdModello, idRiepilogo.IndiceMolteplicita))
                {
                    continue;
                }

                var riepilogo = generatoreRiepilogo.GeneraRiepilogo(idRiepilogo.IdModello, idRiepilogo.Descrizione, idRiepilogo.IndiceMolteplicita);

                var esitoSalvataggio = this._allegatiDomandaFoRepository.SalvaAllegato(idDomanda, riepilogo, false);
                var md5 = new Hasher().ComputeHash(riepilogo.FileContent);
                var idAllegato = domanda.WriteInterface.Allegati.Allega(esitoSalvataggio.CodiceOggetto, esitoSalvataggio.NomeFile, md5, false, String.Empty);

                domanda.WriteInterface.RiepiloghiSchedeDinamiche.SetIdAllegato(idRiepilogo.IdModello, idRiepilogo.IndiceMolteplicita, idAllegato, md5);
            }
            
            //domanda.WriteInterface.RiepiloghiSchedeDinamiche.RigeneraRiepiloghiModelli(idDomanda, generatoreRiepilogoModelloDinamico, generaRiepilogoSchedeCheNonRichiedonoFirma, _allegatiDomandaFoRepository);

            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public void Salva(int idDomanda, ModelloDinamicoBase modelloDinamico)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            var idModello = modelloDinamico.IdModello;

            var cache = this._datiDinamiciRepository.GetCacheModelloDinamico(idModello);
            var istanza = _istanzaSigeproAdapterService.ToIstanzaBackoffice(domanda.ReadInterface);
            var dap = new DomandaOnlineDataAccessFactory(cache, domanda, _tokenApplicazioneService, istanza);
            var loader = new ModelloDinamicoLoader(dap, domanda.DataKey.IdComune, ModelloDinamicoLoader.TipoModelloDinamicoEnum.Frontoffice);

            modelloDinamico.Loader = loader;
            modelloDinamico.Salva();            

            if (modelloDinamico.ErroriScript.Count() > 0)
                throw new SalvataggioModelloDinamicoException(modelloDinamico.ErroriScript);

            // Salvo lo stato dei campi eventualmente non visibili
            modelloDinamico.SalvaCampiNonVisibili();

            // Elimino eventuali riepiloghi delle schede
            domanda.WriteInterface.RiepiloghiSchedeDinamiche.EliminaByIdModello(idModello);

            // Faccio persistere i dati della domanda
            _salvataggioDomandaStrategy.Salva(domanda);
        }

        public void SincronizzaModelliDinamici(int idDomanda, bool ignoraSchedaCittadinoExtracomunitario)
        {
            var domanda = _salvataggioDomandaStrategy.GetById(idDomanda);

            var alias = domanda.DataKey.IdComune;
            var idIntervento = domanda.ReadInterface.AltriDati.Intervento.Codice;
            var endoSelezionati = domanda.ReadInterface.Endoprocedimenti.NonAcquisiti.Select(x => x.Codice).ToList();
            var tipiLocalizzazioni = domanda.ReadInterface.Localizzazioni.Indirizzi.Select(x => x.TipoLocalizzazione).Distinct();

            var schedeDinamicheRichieste = _datiDinamiciRepository.GetSchedeDaInterventoEEndo(idIntervento, endoSelezionati, tipiLocalizzazioni, UsaTipiLocalizzazioniPerSelezionareSchedeDinamiche.Si);

            var schedeintervento = schedeDinamicheRichieste.SchedeIntervento.Select(x => new ModelloDinamicoInterventoDaSincronizzare(x.CodiceIntervento, x.Id, x.Descrizione, x.TipoFirma, x.Facoltativa, x.Ordine.GetValueOrDefault(999)));

            var schedeEndo = schedeDinamicheRichieste.SchedeEndoprocedimenti.Select(x => new ModelloDinamicoEndoprocedimentoDaSincronizzare(x.CodiceEndo, x.Id, x.Descrizione, x.TipoFirma, x.Facoltativa, x.Ordine.GetValueOrDefault(999)));



            bool richiedenteIsExtracomunitario = false;

            if (domanda.ReadInterface.Anagrafiche != null && domanda.ReadInterface.Anagrafiche.GetRichiedente() != null)
            {
                richiedenteIsExtracomunitario = domanda.ReadInterface.Anagrafiche.GetRichiedente().IsCittadinoExtracomunitario;
            }

            ModelloDinamicoPerCittadiniExtracomunitariDaSincronizzare modelloCittadiniExtracomunitari = null;

            if (!ignoraSchedaCittadinoExtracomunitario && richiedenteIsExtracomunitario && _configurazioneSchedaCittadiniEC.Parametri.EsisteSchedaDinamicaPerCittadiniExtracomunitari)
            {
                modelloCittadiniExtracomunitari = new ModelloDinamicoPerCittadiniExtracomunitariDaSincronizzare(
                    _configurazioneSchedaCittadiniEC.Parametri.IdSchedaDinamica.Value,
                    _configurazioneSchedaCittadiniEC.Parametri.NomeScheda,
                    _configurazioneSchedaCittadiniEC.Parametri.RichiedeFirma ? TipoFirmaEnum.SingoliBlocchi : TipoFirmaEnum.NessunaFirma,
                    !_configurazioneSchedaCittadiniEC.Parametri.RichiedeFirma
                );
            }

            var cmd = new SincronizzaModelliDinamiciCommand(schedeintervento, schedeEndo, modelloCittadiniExtracomunitari);

            domanda.WriteInterface.DatiDinamici.SincronizzaModelliDinamici(cmd);

            _salvataggioDomandaStrategy.Salva(domanda);
        }
    }
}
