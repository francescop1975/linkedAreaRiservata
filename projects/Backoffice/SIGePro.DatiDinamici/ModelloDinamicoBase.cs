using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Scripts;
using Init.SIGePro.DatiDinamici.ValidazioneValoriCampi;
using Init.SIGePro.DatiDinamici.VisibilitaCampi;
using Init.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Init.SIGePro.DatiDinamici
{
    public abstract class ModelloDinamicoBase
    {
        public class FlagsModello
        {
            public readonly string NomeModello;
            public readonly bool InSolaLetturaNelBo;       // Potrebbe non essere usato
            public bool Storicizza { get; private set; }
            public readonly bool ModelloMultiplo;

            public FlagsModello(string nomeModello, bool inSolaLetturaNelBo, bool storicizza, bool modelloMultiplo)
            {
                this.NomeModello = nomeModello;
                this.InSolaLetturaNelBo = inSolaLetturaNelBo;
                this.Storicizza = storicizza;
                this.ModelloMultiplo = modelloMultiplo;
            }

            internal void DisabilitaStoricizzazione()
            {
                this.Storicizza = false;
            }
        }

        public class ScriptsModello
        {
            public ScriptCampoDinamico ScriptCaricamento { get; }
            public ScriptCampoDinamico ScriptSalvataggio { get; }
            public ScriptCampoDinamico ScriptModifica { get; }
            public ScriptCampoDinamico ScriptElaborazioneMassiva { get; }

            public ScriptsModello(ScriptCampoDinamico scriptCaricamento, ScriptCampoDinamico scriptSalvataggio, ScriptCampoDinamico scriptModifica, ScriptCampoDinamico scriptElaborazioneMassiva)
            {
                this.ScriptCaricamento = scriptCaricamento;
                this.ScriptSalvataggio = scriptSalvataggio;
                this.ScriptModifica = scriptModifica;
                this.ScriptElaborazioneMassiva = scriptElaborazioneMassiva;
            }
        }

        public class StrutturaModello
        {
            internal readonly List<RigaModelloDinamico> RigheMultiple;
            internal readonly List<RigaModelloDinamico> Righe;
            internal readonly List<CampoDinamicoBase> Campi;
            internal readonly GruppoRigheCollection Gruppi;

            internal StrutturaModello(List<RigaModelloDinamico> righeMultiple, List<RigaModelloDinamico> righe, List<CampoDinamicoBase> campi, GruppoRigheCollection gruppi)
            {
                this.RigheMultiple = righeMultiple;
                this.Righe = righe;
                this.Gruppi = gruppi;
                this.Campi = campi;
            }
        }

        private readonly FlagsModello _flags;
        private readonly StrutturaModello _struttura;
        private readonly Lazy<ContestoModelloDinamico> _contesto;
        private readonly Dictionary<int, CampoDinamico> _campiModificati = new Dictionary<int, CampoDinamico>();
        private readonly List<ErroreValidazione> _erroriScript = new List<ErroreValidazione>();
        private readonly List<string> _debug = new List<string>();

        public IEnumerable<string> DebugMessages
        {
            get
            {
                return this._debug;
            }
        }

        /// <summary>
        /// Imposta o restutuisce l'id della versione storica del modello (se il modello è caricato da uno storico)
        /// </summary>
        protected int? IdVersioneStorico { get; private set; }

        /// <summary>
        /// Imposta o restutuisce il Token utilizzato per l'autenticazione
        /// </summary>
        public string Token { get { return this.Loader.Token; } }

        /// <summary>
        /// Imposta o restutuisce l'Alias del comune
        /// </summary>
        public string IdComune { get; }

        /// <summary>
        /// Imposta o restutuisce l'Id del modello corrente
        /// </summary>
        public int IdModello { get; }

        /// <summary>
        /// Imposta o restutuisce l'indice di molteplicità del modello (in caso di modello multiplo)
        /// </summary>
        public int IndiceModello { get; }

        public string NomeModello
        {
            get
            {
                return this._flags.NomeModello;
            }
        }

        /// <summary>
        /// Imposta o restutuisce se il modello è un modello multiplo
        /// </summary>
        public bool ModelloMultiplo
        {
            get
            {
                return this._flags.ModelloMultiplo;
            }
        }

        /// <summary>
        /// Imposta o restituisce se il modello è in modalità "sola lettura"
        /// </summary>
        protected bool ReadOnly { get; }

        /// <summary>
        /// Lista dei campi contenuti nel modello
        /// </summary>
        public IEnumerable<CampoDinamicoBase> Campi
        {
            get
            {
                return this._struttura.Campi;
            }
        }

        /// <summary>
        /// Righe all'interno del modello
        /// </summary>
        public IEnumerable<RigaModelloDinamico> Righe
        {
            get { return this._struttura.Righe; }
        }

        /// <summary>
        /// Contesto del modello
        /// </summary>
        public ContestoModelloDinamico Contesto => this._contesto.Value;

        /// <summary>
        /// Dictionary che contiene tutti i raggruppamenti presenti nel modello
        /// </summary>
        internal GruppoRigheCollection Gruppi { get { return this._struttura.Gruppi; } }

        /// <summary>
        /// Imposta o rilegge se la scheda viene utilizzata come modello per il frontoffice
        /// </summary>
        public bool ModelloFrontoffice => this.Loader.IsModelloFrontoffice;

        private bool _necessitaReloadInterfaccia = false;
        public bool NecessitaReloadInterfaccia
        {
            get
            {
                var val = this._necessitaReloadInterfaccia;
                this._necessitaReloadInterfaccia = false;
                return val;
            }
        }

        public bool SupportaScriptModifica => this._scriptModifica != null;
        public bool SupportaScriptElaborazioneMassiva => this._scriptElaborazioneMassiva != null;

        private readonly StrutturaModelloBuilder _strutturaModelloBuilder;
        private readonly IScriptModelloBuilder _scriptModelloBuilder;
        private readonly IFlagsModelloBuilder _flagsModelloBuilder;
        public IModelloDinamicoLoader Loader;

        private ScriptCampoDinamico _scriptCaricamento;
        private ScriptCampoDinamico _scriptSalvataggio;
        private ScriptCampoDinamico _scriptModifica;
        private ScriptCampoDinamico _scriptElaborazioneMassiva;

        /// <summary>
        /// Lista dei campi che sono stati modificati dall'ultimo salvataggio
        /// </summary>
        public List<CampoDinamico> CampiModificati
        {
            get
            {
                List<CampoDinamico> ret = new List<CampoDinamico>();

                foreach (int key in this._campiModificati.Keys)
                    ret.Add(this._campiModificati[key]);

                return ret;
            }
        }

        private readonly ILog _log = LogManager.GetLogger(typeof(ModelloDinamicoBase));

        /// <summary>
        /// Crea un'istanza della classe
        /// </summary>
        /// <param name="idModello">id del modello</param>
        /// <param name="indiceModello">indice della molteplicità del modello</param>
        /// <param name="readOnly">imposta se il modello deve essere in sola lettura</param>
        /// <param name="idVersioneStorico">Se impostato specifica quale versione dello storico deve essere visualizzata</param>
        /// <param name="loader">permette il caricamento dei dati del modello</param>
        protected ModelloDinamicoBase(int idModello, int indiceModello, bool readOnly, int? idVersioneStorico, IModelloDinamicoLoader loader)
        {
            this._log.DebugFormat("Caricamento scheda {0}, indice modello {1}", idModello, indiceModello);

            this.IdVersioneStorico = idVersioneStorico;
            this.IdModello = idModello;
            this.IndiceModello = indiceModello;
            this.ReadOnly = readOnly;
            this.Loader = loader;

            this.IdComune = loader.Idcomune;

            // Builder della struttura modello

            this._strutturaModelloBuilder = new StrutturaModelloBuilder(loader.Idcomune, loader.DataAccessFactory);

            // Builder degli script del modello
            var scriptManager = loader.DataAccessFactory.GetScriptModelliManager();
            var scriptRepository = new ScriptModelloRepository(scriptManager, loader.Idcomune);

            this._scriptModelloBuilder = new ScriptModelloBuilder(scriptRepository);

            // Flags modello
            this._flagsModelloBuilder = new FlagsModelloBuilder(this.Loader.DataAccessFactory.GetModelliManager());

            // Effettua l'inizializzazone del modello.
            // - Verifica che sia presente un contesto
            // - Carica i dati del modello
            // - Carica e organizza i campi del modello
            // - Se il modello non è in sola lettura carica i valori dei campi
            // - Se il modello proviene dallo storico carica i valori dallo storico
            this._flags = this._flagsModelloBuilder.CostruisciFlags(this);
            this._contesto = new Lazy<ContestoModelloDinamico>(() => this.InizializzaContesto());

            using (var cp = new CodeProfiler("ModelloDinamicoBase.PrecaricaCampi"))
            {
                this._struttura = this._strutturaModelloBuilder.CostruisciStrutturaModello(this);
            }

            this.InizializzaScripts();
            this.InizializzaValoriCampi();

            this._visibilitaCampiService = new VisibilitaCampiService(this.Campi);

            if (!this.ReadOnly || this.IdVersioneStorico.HasValue)
            {
                foreach (var gruppo in this.Gruppi.GetRaggruppamenti())
                    gruppo.VerificaConsistenzaMolteplicita();
            }
        }

        private void InizializzaValoriCampi()
        {
            IDyn2DatiRepositoryBase datiRepository = this.GetDatiRepository();

            if (this.IdVersioneStorico.HasValue)
            {
                datiRepository = this.GetDatiStoricoRepository(this.IdVersioneStorico.Value);
            }

            this.PopolaValoriCampi(datiRepository);

            if (!this.ReadOnly)
            {
                this.AgganciaChangedHandler();
            }
        }

        private void InizializzaScripts()
        {
            using (var cp = new CodeProfiler("ModelloDinamicoBase.CaricaModello"))
            {
                if (this.ReadOnly)
                {
                    this._scriptCaricamento = null;
                    this._scriptModifica = null;
                    this._scriptSalvataggio = null;
                    this._scriptElaborazioneMassiva = null;

                    foreach (CampoDinamicoBase campo in this.Campi)
                        campo.DisabilitaScript();
                }
                else
                {
                    var scripts = this._scriptModelloBuilder.CostruisciScripts(this);

                    this._scriptCaricamento = scripts.ScriptCaricamento;
                    this._scriptModifica = scripts.ScriptModifica;
                    this._scriptSalvataggio = scripts.ScriptSalvataggio;
                    this._scriptElaborazioneMassiva = scripts.ScriptElaborazioneMassiva;
                }
            }
        }

        /// <summary>
        /// Aggancia a tutti i campi dinamici del modello l'handler delle modifiche.
        /// L'handler delle modifiche viene invocato ogni volta che un campo del modello viene modificato,
        /// serve ad eseguire gli script di modifica dei campi e a tenere traccia dei campi che hanno subito modifiche
        /// </summary>
        private void AgganciaChangedHandler()
        {
            foreach (CampoDinamicoBase campo in this.Campi)
            {
                if (!(campo is CampoDinamico))
                    continue;

                campo.ListaValori.ValoreModificato += (ListaValoriDatiDinamici sender, int indiceElementoModificato, bool perIncrementoMolteplicita) =>
                {
                    this.EseguiScriptModifica((CampoDinamico)sender.Campo, indiceElementoModificato, perIncrementoMolteplicita);
                };
            }
        }

        public bool InSolaLetturaNelBo => this._flags.InSolaLetturaNelBo;

        /// <summary>
        /// Effettua la validazione formale del modello
        /// </summary>
        public void ValidaModello()
        {
            var erroriValidazione = this.Campi.Where(x => x is CampoDinamico).SelectMany(x => x.Valida());

            if (erroriValidazione.Any())
                throw new ValidazioneModelloDinamicoException(erroriValidazione);
        }

        /// <summary>
        /// Disabilita la storicizzazione del modello
        /// </summary>
        public void DisabilitaStoricizzazione()
        {
            this._flags.DisabilitaStoricizzazione();
        }

        /// <summary>
        /// Effettua il salvataggio del modello eseguendo lo script di salvataggio.
        /// <remarks>Il salvataggio viene effettuato solamente se non si sono verificati errori negli script</remarks>
        /// </summary>
        public void Salva()
        {
            this.Salva(true);
        }

        public void SalvaEAggiornaInterfaccia()
        {
            this.Salva(true, false);
            this.RichiediReloadInterfaccia();
        }

        /// <summary>
        /// Effettua il salvataggio del modello specificando se eseguire gli script di salvataggio.
        /// <remarks>Il salvataggio viene effettuato solamente se non si sono verificati errori negli script (nel caso in cui gli script vengano eseguiti)</remarks>
        /// </summary>
        /// <param name="eseguiScriptSalvataggio"></param>
        public void Salva(bool eseguiScriptSalvataggio, bool svuotaCampiModificati = true)
        {
            this._log.DebugFormat("Salvataggio della scheda {0}", this.IdModello);

            using (var cp = new CodeProfiler("ModelloDinamicoBase.Salva"))
            {
                // Esegue gli script di salvataggio
                if (eseguiScriptSalvataggio)
                    this.EseguiScriptSalvataggio();

                var salvaStorico = false;

                if (!this.ErroriScript.Any())
                {
                    try
                    {
                        // Se il modello non è in sola lettura, esistono campi modificati e il modello supporta la storicizzazione
                        // Allora salvo lo storico dei valori allo stato attuale
                        if (!this.ReadOnly && this.CampiModificati.Count > 0 && this._flags.Storicizza)
                            salvaStorico = true;

                        // Salva i valori nel db
                        var campiDaSalvare = this.Campi.Where(c => c is CampoDinamico)
                                                       .Select(c => c as CampoDinamico);

                        this.SalvaValoriCampi(salvaStorico, campiDaSalvare);

                        if (svuotaCampiModificati)
                        {
                            this.SvuotaCampiModificati();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        this._log.DebugFormat("Salvataggio della scheda {0} completato", this.IdModello);
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un modello e tutti i relativi campi dalla base dati
        /// </summary>
        public void Elimina()
        {
            var campiDaEliminare = this.Campi.Where(c => c is CampoDinamico)
                                             .Select(c => new DatiIdentificativiCampo(c.Id));

            var repo = this.GetDatiRepository();
            var idModello = new DatiIdentificativiModello(this.IdModello, this.IndiceModello);
            repo.EliminaValoriCampi(idModello, campiDaEliminare);
        }

        /// <summary>
        /// Copia la struttura del modello da un modello di riferimento
        /// </summary>
        /// <param name="modelloRiferimento">Modello da usare come riferimento per la copia</param>
        public void CopiaDa(ModelloDinamicoBase modelloRiferimento)
        {
            foreach (CampoDinamicoBase campo in this.Campi)
            {
                CampoDinamicoBase rif = modelloRiferimento.TrovaCampoDaId(campo.Id);

                if (rif != null)
                {
                    while (campo.ListaValori.Count < rif.ListaValori.Count)
                        campo.ListaValori.IncrementaMolteplicita();

                    for (int i = 0; i < rif.ListaValori.Count; i++)
                    {
                        campo.ListaValori[i].Valore = rif.ListaValori[i].Valore;
                        campo.ListaValori[i].ValoreDecodificato = rif.ListaValori[i].ValoreDecodificato;
                    }
                }
            }
        }

        /// <summary>
        /// Svuota la lista interna di campi modificati
        /// </summary>
        public void SvuotaCampiModificati()
        {
            this._campiModificati.Clear();
        }

        /// <summary>
        /// Ricerca un campo in base al nome
        /// </summary>
        /// <param name="nomeCampo">nome del campo da cercare</param>
        /// <returns>Campo corrispondente al nome specificato o null se il campo non viene trovato</returns>
        public CampoDinamicoBase TrovaCampo(string nomeCampo)
        {
            return this.TrovaCampo(nomeCampo, false);
        }

        public CampoDinamicoBase TrovaCampo(string nomeCampo, bool ignoraEccezioni)
        {
            var campoTrovato = this.Campi.FirstOrDefault(x => x.NomeCampo == nomeCampo);

            if (campoTrovato == null && !ignoraEccezioni)
                throw new Exception("Impossibile trovare il campo \"" + nomeCampo + "\" nel modello corrente");

            return campoTrovato;
        }

        /// <summary>
        /// Ricerca un campo in base all'id
        /// </summary>
        /// <param name="id">id del campo da cercare</param>
        /// <returns>Campo corrispondente all'id specificato o null se il campo non viene trovato</returns>
        public CampoDinamicoBase TrovaCampoDaId(int id)
        {
            // if (id < 0) return null;
            return this.Campi.FirstOrDefault(campo => campo.Id == id);
        }

        /// <summary>
        /// Restituisce true se il modello contiene errori
        /// </summary>
        public bool HaErroriScript => this._erroriScript.Count > 0;

        /// <summary>
        /// Restituisce una lista contenente tutti gli errori del modello
        /// </summary>
        public IEnumerable<ErroreValidazione> ErroriScript => this._erroriScript.Union(this.Campi.SelectMany(x => x.ErroriScript));

        #region gestione dei campi visibili / non visibili
        private readonly VisibilitaCampiService _visibilitaCampiService;
        private SessioneModificaVisibilitaCampi _sessioneModificaVisibilitaCampi;

        private SessioneModificaVisibilitaCampi IniziaModificheVisibilita()
        {
            this._sessioneModificaVisibilitaCampi = this._visibilitaCampiService.IniziaModificaVisibilita();

            return this._sessioneModificaVisibilitaCampi;
        }

        /// <summary>
        /// Restituisce la lista degli id dei campi dinamici che sono stati resi visibili successivamente alla chiamata a IniziaModificheVisibilita
        /// La lista comprende sia campi dinamici che campi testuali
        /// </summary>
        public IEnumerable<IdValoreCampo> GetIdCampiVisibiliDopoModifiche()
        {
            return this._sessioneModificaVisibilitaCampi.GetCampiVisibili();
        }

        /// <summary>
        /// Restituisce la lista degli id dei campi dinamici che sono stati resi non visibili successivamente alla chiamata a IniziaModificheVisibilita
        /// La lista comprende sia campi dinamici che campi testuali
        /// </summary>
        public IEnumerable<IdValoreCampo> GetIdCampiNonVisibiliDopoModifiche()
        {
            return this._sessioneModificaVisibilitaCampi.GetCampiNonVisibili();
        }

        public IEnumerable<IdValoreCampo> GetIdCampiDaStatoVisibilita(StatoVisibilitaCampoEnum statoVisibilita)
        {
            return this._visibilitaCampiService.GetIdCampiByStatoVisibilita(statoVisibilita);
        }
        #endregion

        /// <summary>
        /// Esegue lo script di caricamento sul modello e su tutti i campi in esso contenuti
        /// </summary>
        public void EseguiScriptCaricamento()
        {
            this._log.DebugFormat("Esecuzione script caricamento della scheda {0}", this.IdModello);

            System.Diagnostics.Debug.WriteLine("Esecuzione script caricamento");

            try
            {
                // Viene richiamato all'interno dello script solamente qui
                this.BeginFrame();

                foreach (CampoDinamicoBase campo in this.Campi)
                    campo.EseguiScriptCaricamento();

                this.EseguiScript(this._scriptCaricamento);
            }
            finally
            {
                // Viene richiamato all'interno dello script solamente qui
                this.EndFrame();
                System.Diagnostics.Debug.WriteLine("Fine script caricamento");
                this._log.DebugFormat("Fine esecuzione script caricamento della scheda {0}", this.IdModello);
            }
        }

        private void EseguiScript(ScriptCampoDinamico script, CampoDinamicoBase campoModificato = null, int indiceCampoModificato = -1, bool perIncrementoMolteplicita = false)
        {
            this._erroriScript.Clear();
            this._debug.Clear();

            try
            {
                script?.Esegui(campoModificato, indiceCampoModificato, perIncrementoMolteplicita);
            }
            catch (TargetInvocationException ex)
            {
                this._erroriScript.Add(new ErroreValidazione(ex.InnerException.Message, -1, -1));
            }
            catch (Exception ex)
            {
                this._erroriScript.Add(new ErroreValidazione(ex.Message, -1, -1));
            }

            if (script != null)
            {
                var erroriValidazione = script.GetErroriValidazione();

                foreach (var errore in erroriValidazione)
                {
                    if (!this._erroriScript.Contains(errore))
                        this._erroriScript.Add(errore);
                }
            }
        }

        /// <summary>
        /// Esegue lo script di salvataggio sul modello e su tutti i campi in esso contenuti
        /// </summary>
        public void EseguiScriptSalvataggio()
        {
            this._log.DebugFormat("Esecuzione script salvataggio della scheda {0}", this.IdModello);
            try
            {
                System.Diagnostics.Debug.WriteLine("Esecuzione script salvataggio");

                foreach (CampoDinamicoBase campo in this.Campi)
                    campo.EseguiScriptSalvataggio();

                this.EseguiScript(this._scriptSalvataggio);
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("Fine esecuzione script salvataggio");
                this._log.DebugFormat("Fine esecuzione script salvataggio della scheda {0}", this.IdModello);
            }
        }

        /// <summary>
        /// Esegue lo script di elaborazione massiva sul modello (i campi in esso contenuti non lo espongono)
        /// </summary>
        public void EseguiScriptElaborazioneMassiva()
        {
            this._log.DebugFormat("Esecuzione script elaborazione massiva della scheda {0}", this.IdModello);
            try
            {
                System.Diagnostics.Debug.WriteLine("Esecuzione script elaborazione massiva");

                this.EseguiScript(this._scriptElaborazioneMassiva);
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("Fine esecuzione script elaborazione massiva");
                this._log.DebugFormat("Fine esecuzione script elaborazione massiva della scheda {0}", this.IdModello);
            }
        }

        private void EseguiScriptModifica(CampoDinamico campo, int indiceCampoModificato, bool perIncrementoMolteplicita)
        {
            this._log.DebugFormat("Esecuzione script modifica della scheda {0}, campo {1}", this.IdModello, campo == null ? String.Empty : campo.NomeCampo);
            System.Diagnostics.Debug.WriteLine("Esecuzione script modifica per il campo " + campo.NomeCampo);

            try
            {
                if (campo == null)
                    return;

                if (!this._campiModificati.ContainsKey(campo.Id))
                    this._campiModificati.Add(campo.Id, campo);

                //				this.BeginFrame();

                try
                {
                    campo.EseguiScriptModifica();
                }
                catch (Exception ex)
                {
                    this._erroriScript.Add(new ErroreValidazione(ex.Message));
                }

                this.EseguiScript(this._scriptModifica, campo, indiceCampoModificato, perIncrementoMolteplicita);
            }
            finally
            {
                //				this.EndFrame();

                System.Diagnostics.Debug.WriteLine("Fine esecuzione script modifica" + campo.NomeCampo);
                this._log.DebugFormat("Fine script modifica della scheda {0}, campo {1}", this.IdModello, campo == null ? String.Empty : campo.NomeCampo);
            }
        }

        /// <summary>
        /// Popola i valori dei campi con i dati letti dal db
        /// </summary>
        protected virtual void PopolaValoriCampi(IDyn2DatiRepositoryBase repository)
        {
            var listaValoriNelDb = repository.GetValoriCampiDaIdModello(this.IdModello, this.IndiceModello);

            foreach (var riga in this.Righe)
            {
                for (int idxColonna = 0; idxColonna < riga.NumeroColonne; idxColonna++)
                {
                    CampoDinamicoBase cdb = riga[idxColonna];

                    if (cdb == null) continue;
                    if (cdb.Id < 0) continue;

                    if (listaValoriNelDb.TryGetValue(cdb.Id, out IEnumerable<IValoreCampo> valoriUtenteAttuale))
                    {
                        for (int idxCampo = 0; idxCampo < valoriUtenteAttuale.Count(); idxCampo++)
                        {
                            var element = valoriUtenteAttuale.ElementAt(idxCampo);
                            var indice = element.IndiceMolteplicita ?? 0;
                            var valore = element.Valore;
                            var valoreDecodificato = element.Valoredecodificato;

                            while (cdb.ListaValori.Count < (indice + 1))
                                cdb.ListaValori.IncrementaMolteplicita();

                            cdb.ListaValori[indice].Valore = valore;
                            cdb.ListaValori[indice].ValoreDecodificato = valoreDecodificato;
                        }
                    }
                    // Per compatibilità con i vecchi dati dinamici
                    if (cdb.ListaValori.Count == 0)
                        cdb.ListaValori.IncrementaMolteplicita();
                }
            }
        }

        #region metodi astratti

        /// <summary>
        /// Imposta i valori relativi al contesto attuale
        /// </summary>
        protected abstract ContestoModelloDinamico InizializzaContesto();

        protected IDyn2DatiRepository GetDatiRepository()
        {
            return this.Loader.DataAccessFactory.GetRepository();
        }

        protected IDyn2DatiStoricoRepository GetDatiStoricoRepository(int idVersioneStorico)
        {
            return this.Loader.DataAccessFactory.GetStoricoRepository(idVersioneStorico);
        }

        /// <summary>
        /// Salva il valore ddei campi del modello
        /// </summary>
        /// <param name="campo"></param>
        protected void SalvaValoriCampi(bool salvaStorico, IEnumerable<CampoDinamico> campiDaSalvare)
        {
            if (salvaStorico)
            {
                var storicoRepository = this.GetDatiStoricoRepository(-1);

                storicoRepository.SalvaStoricoModello(this);
            }

            var repository = this.GetDatiRepository();
            var idModello = new DatiIdentificativiModello(this.IdModello, this.IndiceModello);

            repository.SalvaValoriCampi(idModello, campiDaSalvare.Select(x => new CampoDaSalvare(x)));
        }

        #endregion

        // UTILIZZATO DA AR/FVG. Nel bo lo stato di visibilità dei campi non viene salvato
        public void SalvaCampiNonVisibili()
        {
            var repository = this.GetDatiRepository();
            var idModello = new DatiIdentificativiModello(this.IdModello, this.IndiceModello);

            repository.SalvaCampiNonVisibili(idModello, this.GetIdCampiDaStatoVisibilita(StatoVisibilitaCampoEnum.NonVisibile));
        }

        internal void BeginFrame()
        {
            System.Diagnostics.Debug.WriteLine("ModelloDinamicoBase->BeginFrame");

            var sessioneModificaVisibilita = this.IniziaModificheVisibilita();
            var scripts = this.GetScriptsPresenti();

            foreach (var s in scripts)
                s.PreExecute(sessioneModificaVisibilita);
        }

        internal void EndFrame()
        {
            System.Diagnostics.Debug.WriteLine("ModelloDinamicoBase->EndFrame");

            var scripts = this.GetScriptsPresenti();

            foreach (var s in scripts)
                s.PostExecute();
        }

        private IEnumerable<ScriptCampoDinamico> GetScriptsPresenti() => new[]
        {
            this._scriptCaricamento,
            this._scriptModifica,
            this._scriptSalvataggio,
            this._scriptElaborazioneMassiva
        }.Where(x => x != null);

        public void Debug(string message)
        {
            this._debug.Add(message);
            System.Diagnostics.Debug.WriteLine($"DEBUG da script: {message}");
        }

        public void Debug(CampoDinamicoBase campo)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Campo {0} ({1})\r\n", campo.Id, campo.NomeCampo);

            for (var i = 0; i < campo.ListaValori.Count; i++)
            {
                sb.AppendFormat("valore {0}: \"{1}\" (\"{2}\")\r\n", i, campo.ListaValori[i].Valore, campo.ListaValori[i].ValoreDecodificato);
            }

            this.Debug(sb.ToString());
        }

        public void RichiediReloadInterfaccia()
        {
            this._necessitaReloadInterfaccia = true;
        }

        public void SvuotaValoriCampi(bool notificaModifica = true)
        {
            this.Campi.Where(x => x is CampoDinamico).ToList().ForEach(x => x.SvuotaValori(notificaModifica));
        }
    }
}
