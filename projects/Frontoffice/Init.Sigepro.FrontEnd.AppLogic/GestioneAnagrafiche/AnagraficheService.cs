﻿// -----------------------------------------------------------------------
// <copyright file="AnagraficheService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneAnagrafiche
{
	using System.Linq;
	using CuttingEdge.Conditions;
	using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
	using Init.Sigepro.FrontEnd.AppLogic.Common;
	using Init.Sigepro.FrontEnd.AppLogic.GestioneAnagrafiche.RicercaAnagrafiche;
	using Init.Sigepro.FrontEnd.AppLogic.GestioneComuni;
	using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche;
	using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
	using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche.Sincronizzazione;
	using Init.Sigepro.FrontEnd.AppLogic.Adapters;
	using Init.Sigepro.FrontEnd.AppLogic.GestioneTipiSoggetto;
	using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
	using System.Collections.Generic;
    using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;

    public interface IAnagraficheService
	{
		bool IgnoraRicercaBackofficePerPersoneFisiche { get; set; }

		void CollegaAziendaAdAnagrafica(int idDomanda, int idAnagrafica, int idAziendaCollegata);
		//AnagraficaDomanda NuovaAnagraficaDomanda(int idDomanda);
		AnagraficaDomanda RicercaAnagrafica(int idDomanda, TipoPersonaEnum tipoPersona, string codiceFiscalePartitaIva);
		void RimuoviAnagrafica(int idDomanda, int idAnagrafica);
		void RimuoviAnagrafica(DomandaOnline domanda, int idAnagrafica);
		int SalvaAnagrafica(int idDomanda, AnagraficaDomanda row);
		int SalvaAnagrafica(DomandaOnline domanda, AnagraficaDomanda row);
		void SincronizzaFlagsTipiSoggetto(int idDomanda);
		void VerificaFlagsCittadiniExtracomunitari(int idDomanda);


		Anagrafe RicercaAnagraficaBackoffice(string aliasComune, TipoPersonaEnum tipoPersona, string codiceFiscale);

		// TODO: Autenticazione/account utente - spostare in un service differente
		Anagrafe GetPersonaFisicaByUserId(string idComune, string userId);
        Anagrafe GetPersonaGiuridicaByUserId(string idComune, string userId);
        CreazioneAnagraficaResult CreaAnagrafica(RichiestaCreazioneAnagraficaDto richiesta);
		//void NuovaRegistrazione(string idComune, Anagrafe anagrafe);
		void ModificaDatianagrafici(string idComune, AnagraficaUtente anagrafica);
		void ModificaPassword(string idComune, int codiceAnagrafe, string vecchiaPassword, string nuovaPassword, string confermaPassword);
		void AggiungiAnagraficaConSoggettoCollegato(int idDomanda, string cfAnagrafica, int codiceTipoSoggettoAnagrafica, string cfAnagraficaCollegata, int codiceTipoSoggettoAnagraficaCollegata);
        AnagraficaDomanda GetById(int idDomanda, int idAnagrafica);
        AnagraficaDomanda GetRichiedente(int idDomanda);
        AnagraficaDomanda GetTecnico(int idDomanda);
	}


	public class AnagraficheService : IAnagraficheService
	{
		IAnagraficheRepository _anagrafeRepository;
		IAliasSoftwareResolver _aliasSoftwareResolver;
		ISalvataggioDomandaStrategy _salvataggioStrategy;
		ICittadinanzeService _cittadinanzeService;
		ILogicaSincronizzazioneTipiSoggetto _logicaSincronizzazioneTipiSoggetto;
		ITipiSoggettoService _tipiSoggettoService;

        public AnagraficheService(IAliasSoftwareResolver aliasSoftwareResolver,
									ISalvataggioDomandaStrategy salvataggioStrategy,
									IAnagraficheRepository anagrafeRepository,
									ICittadinanzeService cittadinanzeService,
									ILogicaSincronizzazioneTipiSoggetto logicaSincronizzazioneTipiSoggetto,
									ITipiSoggettoService tipiSoggettoService,
                                    IConfigurazione<ParametriPresentazioneDomanda> configurazione)
		{
			Condition.Requires(aliasSoftwareResolver, "aliasSoftwareResolver").IsNotNull();
			Condition.Requires(salvataggioStrategy, "salvataggioStrategy").IsNotNull();
			Condition.Requires(anagrafeRepository, "anagrafeRepository").IsNotNull();
			Condition.Requires(cittadinanzeService, "cittadinanzeService").IsNotNull();
			Condition.Requires(logicaSincronizzazioneTipiSoggetto, "logicaSincronizzazioneTipiSoggetto").IsNotNull();
			Condition.Requires(tipiSoggettoService, "tipiSoggettoService").IsNotNull();


			this._aliasSoftwareResolver				= aliasSoftwareResolver;
			this._salvataggioStrategy				= salvataggioStrategy;
			this._anagrafeRepository				= anagrafeRepository;
			this._cittadinanzeService				= cittadinanzeService;
			this._logicaSincronizzazioneTipiSoggetto = logicaSincronizzazioneTipiSoggetto;
			this._tipiSoggettoService				= tipiSoggettoService;
            this.IgnoraRicercaBackofficePerPersoneFisiche = false;
		}

		public bool IgnoraRicercaBackofficePerPersoneFisiche { get; set; }
		

		public void CollegaAziendaAdAnagrafica(int idDomanda, int idAnagrafica, int idAziendaCollegata)
		{
			var domanda = _salvataggioStrategy.GetById(idDomanda);

			domanda.WriteInterface.Anagrafiche.CollegaAziendaAdAnagrafica(idAnagrafica, idAziendaCollegata);

			_salvataggioStrategy.Salva(domanda);
		}

		public void AggiungiAnagraficaConSoggettoCollegato(int idDomanda,string cfAnagrafica, int codiceTipoSoggettoAnagrafica,string cfAnagraficaCollegata, int codiceTipoSoggettoAnagraficaCollegata)
		{
			var anagrafica = RicercaAnagraficaBackoffice(_aliasSoftwareResolver.AliasComune, GestionePresentazioneDomanda.GestioneAnagrafiche.TipoPersonaEnum.Fisica, cfAnagrafica);
			var anagraficaCollegata = RicercaAnagraficaBackoffice(_aliasSoftwareResolver.AliasComune, GestionePresentazioneDomanda.GestioneAnagrafiche.TipoPersonaEnum.Giuridica, cfAnagraficaCollegata);

			var anagraficaDomanda = AnagrafeToAnagraficaDomanda(anagrafica, codiceTipoSoggettoAnagrafica);
			var anagraficaCollegataDomanda = AnagrafeToAnagraficaDomanda(anagraficaCollegata, codiceTipoSoggettoAnagraficaCollegata);

			var domanda = _salvataggioStrategy.GetById(idDomanda);

			domanda.WriteInterface.Anagrafiche.AggiungiAnagraficaConSoggettoCollegato(anagraficaDomanda, anagraficaCollegataDomanda, this._logicaSincronizzazioneTipiSoggetto);

			_salvataggioStrategy.Salva(domanda);
		}

		private AnagraficaDomanda AnagrafeToAnagraficaDomanda(Anagrafe anagrafica, int codiceTipoSoggetto)
		{
			var anagraficaAdapter = new AnagrafeAdapter(anagrafica);
			var anagraficaDomanda = anagraficaAdapter.ToAnagraficaDomanda();
			var tipoSoggettoAnagrafica = _tipiSoggettoService.GetById(codiceTipoSoggetto);

			anagraficaDomanda.TipoSoggetto = tipoSoggettoAnagrafica.ToTipoSoggettoDomanda();

			return anagraficaDomanda;
		}

		public void RimuoviAnagrafica(int idDomanda, int idAnagrafica)
		{
			var domanda = _salvataggioStrategy.GetById(idDomanda);

			RimuoviAnagrafica(domanda, idAnagrafica);

			_salvataggioStrategy.Salva(domanda);
		}

		public void RimuoviAnagrafica(DomandaOnline domanda, int idAnagrafica)
		{
			domanda.WriteInterface.Anagrafiche.Elimina(idAnagrafica);
		}

		public AnagraficaDomanda RicercaAnagrafica(int idDomanda, TipoPersonaEnum tipoPersona, string codiceFiscalePartitaIva)
		{
			var domanda = _salvataggioStrategy.GetById(idDomanda);
			var finders = new List<AbstractAnagrafeFinder>();

			finders.Add(new DomandaAnagrafeFinder(domanda.ReadInterface.Anagrafiche));

			if (tipoPersona == TipoPersonaEnum.Giuridica || !this.IgnoraRicercaBackofficePerPersoneFisiche)
			{
				finders.Add(new BackendAnagrafeFinder(_aliasSoftwareResolver.AliasComune, this));
			}

			finders.Add(new NuovaAnagraficaFinder(domanda));

			var finder = new CompositeAnagrafeFinder(finders);

			return finder.Find(tipoPersona, codiceFiscalePartitaIva);
		}

		public int SalvaAnagrafica(int idDomanda, AnagraficaDomanda row)
		{
			var domanda = _salvataggioStrategy.GetById(idDomanda);

			var idAna = SalvaAnagrafica(domanda, row);

			_salvataggioStrategy.Salva(domanda);

            return idAna;
		}

		public int SalvaAnagrafica(DomandaOnline domanda, AnagraficaDomanda row)
		{
			return domanda.WriteInterface.Anagrafiche.AggiungiOAggiorna(row, this._logicaSincronizzazioneTipiSoggetto);
		}


		public void SincronizzaFlagsTipiSoggetto(int idDomanda)
		{
			var domanda = _salvataggioStrategy.GetById(idDomanda);

			domanda.WriteInterface.Anagrafiche.Sincronizza(this._logicaSincronizzazioneTipiSoggetto);

			_salvataggioStrategy.Salva(domanda);
		}

		public void VerificaFlagsCittadiniExtracomunitari(int idDomanda)
		{
			var domanda = _salvataggioStrategy.GetById(idDomanda);

			domanda.WriteInterface.Anagrafiche.VerificaFlagsCittadiniExtracomunitari(this._cittadinanzeService);

			_salvataggioStrategy.Salva(domanda);
		}


		#region IAnagraficheService Members


        public Anagrafe GetPersonaFisicaByUserId(string idComune, string userId)
		{
			return this._anagrafeRepository.GetByUserId(idComune, userId, TipoPersonaEnum.Fisica);
		}

        public Anagrafe GetPersonaGiuridicaByUserId(string idComune, string userId)
        {
            return this._anagrafeRepository.GetByUserId(idComune, userId, TipoPersonaEnum.Giuridica);
        }


        public CreazioneAnagraficaResult CreaAnagrafica(RichiestaCreazioneAnagraficaDto richiesta)
        {
            return this._anagrafeRepository.CreaAnagrafica(this._aliasSoftwareResolver.AliasComune, richiesta);
        }

        //public void NuovaRegistrazione(string idComune, Anagrafe anagrafe)
        //{
        //    this._anagrafeRepository.NuovaRegistrazione(idComune, anagrafe);
        //}



        public void ModificaDatianagrafici(string idComune, AnagraficaUtente anagrafica)
		{
			this._anagrafeRepository.ModificaDatianagrafici(idComune, anagrafica);
		}


		public void ModificaPassword(string idComune, int codiceAnagrafe, string vecchiaPassword, string nuovaPassword, string confermaPassword)
		{
			this._anagrafeRepository.ModificaPassword(idComune, codiceAnagrafe, vecchiaPassword, nuovaPassword, confermaPassword);
		}

		#endregion

		#region IAnagraficheService Members


		public Anagrafe RicercaAnagraficaBackoffice(string aliasComune, TipoPersonaEnum tipoPersona, string codiceFiscale)
		{
			return this._anagrafeRepository.RicercaAnagrafica(aliasComune, tipoPersona, codiceFiscale);
		}

        public AnagraficaDomanda GetById(int idDomanda, int idAnagrafica)
        {
            var domanda = _salvataggioStrategy.GetById(idDomanda);

            return domanda.ReadInterface.Anagrafiche.GetById(idAnagrafica);
        }

        public AnagraficaDomanda GetRichiedente(int idDomanda)
        {
            var domanda = _salvataggioStrategy.GetById(idDomanda);

            return domanda.ReadInterface.Anagrafiche.GetRichiedente();
        }

        public AnagraficaDomanda GetTecnico(int idDomanda)
        {
            var domanda = _salvataggioStrategy.GetById(idDomanda);

            return domanda.ReadInterface.Anagrafiche.GetTecnico();

        }

        #endregion

    }
}
