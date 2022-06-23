using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GestioneAnagrafiche;
using Init.Sigepro.FrontEnd.AppLogic.GestioneComuni;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche;
using Init.Sigepro.FrontEnd.AppLogic.GestioneTipiSoggetto;
using Init.Sigepro.FrontEnd.AppLogic.WsComuniService;
using Init.Sigepro.FrontEnd.Infrastructure;
using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.CondizioniIngressoSteps;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    public class GestioneAnagraficheStepPage : IstanzeStepPage
    {
        protected static class Constants
        {
            public const string MSG_ERRORE_UTENTE_NON_PRESENTE = "L'utente con cui si è effettuato l'accesso (Nominativo: {0}, Codice fiscale: {1}) non è presente nella lista dei soggetti coinvolti nella domanda";
            public const string TestoAggiungiRichiedente = "Aggiungi il beneficiario";
            public const string TestoAggiungiAzienda = "Aggiungi società/ditta individuale";
            public const string TestoAggiungiUtenteLoggato = "Aggiungi i tuoi dati";
            public const string TestoAggiungiAziendaUtenteLoggato = "Aggiungi l'ente o l'associazione a cui appartieni";
        }

        public enum FlagVisualizzazioneCampo
        {
            Nascondi = 0,
            Mostra = 1,
            Obbligatorio = 2
        }

        public static class PageViews
        {
            public const int Lista = 0;
            public const int NuovaAnagrafica = 1;
            public const int EditPersonaFisica = 2;
            public const int EditPersonaGiuridica = 3;
        }

        public class NuovaAnagraficaSpecification : ISpecification<AnagraficaDomanda>
        {
            public bool IsSatisfiedBy(AnagraficaDomanda item)
            {
                return item.Nominativo == null || String.IsNullOrEmpty(item.Nominativo.Trim());
            }
        }

        #region parametri letti dalla configurazione steps
        public bool VerificaPecObbligatoria
        {
            get { object o = this.ViewState["VerificaPecObbligatoria"]; return o == null ? false : (bool)o; }
            set { this.ViewState["VerificaPecObbligatoria"] = value; }
        }

        public string MessaggioAvvertimentoVerificaPEC
        {
            get { object o = this.ViewState["MessaggioAvvertimentoVerificaPEC"]; return o == null ? String.Empty : o.ToString(); }
            set { this.ViewState["MessaggioAvvertimentoVerificaPEC"] = value; }
        }

        public string MessaggioUtenteNonPresente
        {
            get { object o = this.ViewState["MessaggioUtenteNonPresente"]; return o == null ? Constants.MSG_ERRORE_UTENTE_NON_PRESENTE : (string)o; }
            set { this.ViewState["MessaggioUtenteNonPresente"] = value; }
        }

        public bool RendiModificabiliDatiAnagraficheEsistenti
        {
            get { object o = this.ViewState["RendiModificabiliDatiAnagraficheEsistenti"]; return o == null ? true : (bool)o; }
            set { this.ViewState["RendiModificabiliDatiAnagraficheEsistenti"] = value; }
        }

        public bool IgnoraRicercaBackofficePerPersoneFisiche
        {
            get { object o = this.ViewState["IgnoraRicercaBackofficePerPersoneFisiche"]; return o == null ? false : (bool)o; }
            set { this.ViewState["IgnoraRicercaBackofficePerPersoneFisiche"] = value; }
        }



        public bool EmailoPecObbligatori
        {
            get { object o = this.ViewState["EmailoPecObbligatori"]; return o == null ? false : (bool)o; }
            set { this.ViewState["EmailoPecObbligatori"] = value; }
        }

        public bool TelefonooCellulareObbligatori
        {
            get { object o = this.ViewState["TelefonooCellulareObbligatori"]; return o == null ? false : (bool)o; }
            set { this.ViewState["TelefonooCellulareObbligatori"] = value; }
        }


        #endregion






        [Inject]
        public CondizioneIngressoStepSempreVera _condizioneIngresso { get; set; }
        [Inject]
        public IAnagraficheService AnagraficheService { get; set; }
        [Inject]
        public IConfigurazione<ParametriWorkflow> ConfigurazioneWorkflow { get; set; }
        [Inject]
        public IsUtenteAnonimoSpecification IsUtenteAnonimo { get; set; }
        [Inject]
        public IComuniService _comuniService { get; set; }


        protected ILog m_logger = LogManager.GetLogger(typeof(DatiAnagrafici));

        public delegate void ErrorDelegate(string message);

        private int? _codiceIntervento = null;
        protected int? CodiceIntervento
        {
            get
            {
                if (this._codiceIntervento == null)
                {
                    this._codiceIntervento = ReadFacade.Domanda.AltriDati.Intervento == null ? (int?)null : ReadFacade.Domanda.AltriDati.Intervento.Codice;
                }

                return this._codiceIntervento;
            }
        }

        protected DatiProvinciaCompatto GetDatiProvincia(string siglaProvincia)
        {
            return this._comuniService.GetDatiProvincia(siglaProvincia);
        }

        protected CittadinanzaCompatto GetDatiCittadinanza(string strIdCittadinanza)
        {
            if (String.IsNullOrEmpty(strIdCittadinanza))
            {
                return null;
            }

            return ReadFacade.Cittadinanze.GetCittadinanzaDaId(Convert.ToInt32(strIdCittadinanza));
        }

        protected DatiComuneCompatto GetDatiComune(string codiceComune)
        {
            if (String.IsNullOrEmpty(codiceComune))
            {
                return null;
            }

            return this._comuniService.GetDatiComune(codiceComune);
        }

        protected TipoSoggetto GetTipoSoggetto(int idTipoSoggetto)
        {
            return ReadFacade.TipiSoggetto.GetById(idTipoSoggetto);
        }

        protected AnagraficaDomanda GetAnagrafeRow(int idAnagrafica)
        {
            var anagrafica = ReadFacade.Domanda.Anagrafiche.GetById(idAnagrafica);

            if (anagrafica != null)
                return anagrafica;

            return AnagraficaDomanda.New(idAnagrafica);
        }

        protected IEnumerable<TipoSoggetto> GetTipiSoggettoPf()
        {
            return ReadFacade.TipiSoggetto.GetTipiSoggettoPersonaFisica(this.CodiceIntervento);
        }

        protected bool VerificaRichiedenteAutomatico()
        {
            if (!ConfigurazioneWorkflow.Parametri.ImpostaAutomaticamenteAnagraficaUtenteCorrente)
            {
                return false;
            }

            if (ReadFacade.Domanda.Anagrafiche.Anagrafiche.Count() > 0)
            {
                return false;
            }

            if (IsUtenteAnonimo.IsSatisfiedBy(UserAuthenticationResult))
            {
                return false;
            }

            return true;
        }
    }
}