// -----------------------------------------------------------------------
// <copyright file="BandiUmbriaService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneBandiUmbria
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Linq;
	using Init.Sigepro.FrontEnd.AppLogic.AllegatiDomanda;
	using Init.Sigepro.FrontEnd.AppLogic.GestioneBandiUmbria.Validazione;
	using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
	using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
	using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche;
	using Init.Sigepro.FrontEnd.AppLogic.PrecompilazionePDF;
	using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;

	
	public class BandiUmbriaService : IBandiUmbriaService
	{
		private static class Constants
		{
			public const string ChiaveDbDataDomanda = "BANDI_DATA_DOMANDA";
			public const string TagTipoModello = "Tipo_modello";
			public const string ChiaveConfigurazioneVerificaTipoAllegati = "BandiUmbriaService.validaAllegati";
		}

		IValidazioneBandoA1Service _validazioneBandoA1Service;
		IValidazioneBandoB1Service _validazioneBandoB1Service;
		ISalvataggioDomandaStrategy _persistenzaStrategy;
		IPdfUtilsService _pdfUtilsService;
		IDatiProgettoReader _datiProgettoReader;
		IOggettiService _oggettiService;
		IAllegatiDomandaFoRepository _allegatiRepository;

		public BandiUmbriaService(IValidazioneBandoA1Service validazioneBandoA1Service, IValidazioneBandoB1Service validazioneBandoB1Service, ISalvataggioDomandaStrategy persistenzaStrategy, IPdfUtilsService pdfUtilsService, IDatiProgettoReader datiProgettoReader, IOggettiService oggettiService, IAllegatiDomandaFoRepository allegatiDomandaFoRepository)
		{
			this._validazioneBandoA1Service = validazioneBandoA1Service;
			this._validazioneBandoB1Service = validazioneBandoB1Service;
			this._persistenzaStrategy = persistenzaStrategy;
			this._pdfUtilsService = pdfUtilsService;
			this._datiProgettoReader = datiProgettoReader;
			this._oggettiService = oggettiService;
			this._allegatiRepository = allegatiDomandaFoRepository;
		}

		public EsitoValidazione ValidaBandoA1(int idDomanda)
		{
			// var idModelloAllegato1 = configurazione.IdModelloAllegato1;
			// var datiModello1 = _pdfUtilsService.EstraiDatiPdf(domanda.Documenti, idModelloAllegato1);

			var domanda = this._persistenzaStrategy.GetById(idDomanda);
			var readInterface = domanda.ReadInterface.Documenti.Intervento.Documenti;
			var datiBando = domanda.ReadInterface.BandiUmbria.DatiDomanda;

			if (!datiBando.Allegato1.IdAllegato.HasValue)
			{
				throw new Exception("Impossibile trovare il modello 1 nella domanda corrente");
			}

			if (!datiBando.Allegato2.IdAllegato.HasValue)
			{
				throw new Exception("Impossibile trovare il modello 2 nella domanda corrente");
			}

			var datiModello1 = this._pdfUtilsService.EstraiDatiPdf(datiBando.Allegato1.IdAllegato.Value);
			var datiModello2 = this._pdfUtilsService.EstraiDatiPdf(datiBando.Allegato2.IdAllegato.Value);

			return new EsitoValidazione
			{
				DatiProgetto = EstraiDatiProgetto(datiModello2),
				Avvisi = this._validazioneBandoA1Service.GetAvvertimenti(domanda.ReadInterface, datiModello2),
				Errori = this._validazioneBandoA1Service.GetErrori(domanda.ReadInterface, datiModello1, datiModello2)
			};
		}

		public EsitoValidazione ValidaBandoB1(int idDomanda)
		{
			// var idModelloAllegato1 = configurazione.IdModelloAllegato1;
			// var datiModello1 = _pdfUtilsService.EstraiDatiPdf(domanda.Documenti, idModelloAllegato1);

			var domanda = this._persistenzaStrategy.GetById(idDomanda);
			var readInterface = domanda.ReadInterface.Documenti.Intervento.Documenti;
			var datiBando = domanda.ReadInterface.BandiUmbria.DatiDomanda;

			if (!datiBando.Allegato3.IdAllegato.HasValue)
			{
				throw new Exception("Impossibile trovare il modello 3 nella domanda corrente");
			}

			if (!datiBando.Allegato4.IdAllegato.HasValue)
			{
				throw new Exception("Impossibile trovare il modello 4 nella domanda corrente");
			}

			var datiModello3 = this._pdfUtilsService.EstraiDatiPdf(datiBando.Allegato3.IdAllegato.Value);
			var datiModello4 = this._pdfUtilsService.EstraiDatiPdf(datiBando.Allegato4.IdAllegato.Value);

			return new EsitoValidazione
			{
				Avvisi = this._validazioneBandoB1Service.GetAvvertimenti(domanda.ReadInterface, datiModello4),
				Errori = this._validazioneBandoB1Service.GetErrori(domanda.ReadInterface, datiModello3, datiModello4)
			};
		}

		private void AggiungiDataDomanda(DomandaOnline domanda)
		{
			domanda.WriteInterface.DatiExtra.SetValoreDato(Constants.ChiaveDbDataDomanda, DateTime.Now.ToString());
		}

		private DatiProgettoModello2 EstraiDatiProgetto(DatiPdfCompilabile allegato2)
		{
			return this._datiProgettoReader.ReadDatiProgetto(allegato2);
		}

		public DomandaBando GetDatiDomandaB1(int idDomanda, int idModelloAllegato3, int idModelloAllegato4, int idModelloAllegato10, int idModelloAllegatoAltreSedi)
		{
			var domanda = this._persistenzaStrategy.GetById(idDomanda);
			var datiBando = domanda.ReadInterface.BandiUmbria.DatiDomanda;

			if (datiBando == null)
			{
				var azienda = domanda.ReadInterface.Anagrafiche.Anagrafiche.Where(x => x.TipoSoggetto.Ruolo == RuoloTipoSoggettoDomandaEnum.Azienda).FirstOrDefault();

				datiBando = DomandaBando.B1(idModelloAllegato3, idModelloAllegato4, idModelloAllegato10, idModelloAllegatoAltreSedi, azienda);

				domanda.WriteInterface.BandiUmbria.ImpostaDatiDomanda(datiBando);

				AggiungiDataDomanda(domanda);

				this._persistenzaStrategy.Salva(domanda);
			}

			if (datiBando.AllegatoAltreSediOperative == null)
			{
				datiBando.AllegatoAltreSediOperative = new AllegatoDomandaBandi(CategoriaAllegatoBandoEnum.AllegatoAltreSediOperative, idModelloAllegatoAltreSedi);
				domanda.WriteInterface.BandiUmbria.ImpostaDatiDomanda(datiBando);
				this._persistenzaStrategy.Salva(domanda);
			}

			return datiBando;
		}

		public DomandaBando GetDatiDomandaA1(int idDomanda, int idModelloAllegato1, int idModelloAllegato2, int idModelloAllegato7, int idModelloAllegatoAltreSedi)
		{
			var domanda = this._persistenzaStrategy.GetById(idDomanda);
			var datiBando = domanda.ReadInterface.BandiUmbria.DatiDomanda;

			if (datiBando == null)
			{
				var aziende = domanda.ReadInterface.Anagrafiche.Anagrafiche.Where(x => x.TipoPersona == TipoPersonaEnum.Giuridica);

				datiBando = DomandaBando.A1(idModelloAllegato1, idModelloAllegato2, idModelloAllegato7, idModelloAllegatoAltreSedi, aziende);

				domanda.WriteInterface.BandiUmbria.ImpostaDatiDomanda(datiBando);

				AggiungiDataDomanda(domanda);

				this._persistenzaStrategy.Salva(domanda);
			}

			if (datiBando.AllegatoAltreSediOperative == null)
			{
				datiBando.AllegatoAltreSediOperative = new AllegatoDomandaBandi(CategoriaAllegatoBandoEnum.AllegatoAltreSediOperative, idModelloAllegatoAltreSedi);
				domanda.WriteInterface.BandiUmbria.ImpostaDatiDomanda(datiBando);
				this._persistenzaStrategy.Salva(domanda);
			}

			return datiBando;
		}

		public void AllegaADomanda(int idDomanda, string idAllegato, BinaryFile file, string verificaModello)
		{
			var domanda = this._persistenzaStrategy.GetById(idDomanda);
			var datiBando = domanda.ReadInterface.BandiUmbria.DatiDomanda;

			var allegato = datiBando.TrovaAllegato(idAllegato);

			if (!String.IsNullOrEmpty(verificaModello) && !VerificaTipoModello(file, allegato.TagModello))
			{
				throw new Exception(String.Format("Il file che è stato caricato ({0}) non corrisponde al modello corretto. Verificare il file caricato", file.FileName));
			}

			if (allegato.IdAllegato.HasValue)
			{
				domanda.WriteInterface.Documenti.EliminaAllegatoADocumentoDaCodiceOggetto(allegato.IdAllegato.Value);
			}

			var result = this._allegatiRepository.SalvaAllegato(idDomanda, file, false);
			domanda.WriteInterface.Documenti.AggiungiDocumentoInterventoLibero(allegato.Descrizione, result.CodiceOggetto, file.FileName, -1, "Altri allegati", result.FirmatoDigitalmente);

			allegato.SetRiferimentiFile(result.CodiceOggetto, file.FileName);

			domanda.WriteInterface.BandiUmbria.ImpostaDatiDomanda(datiBando);

			this._persistenzaStrategy.Salva(domanda);
		}

		private bool VerificaTipoModello(BinaryFile file, string tipoModelloCercato)
		{
			if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[Constants.ChiaveConfigurazioneVerificaTipoAllegati]))
				return true;

			var datiPdf = this._pdfUtilsService.EstraiDatiPdf(file);

			var tipoModello = datiPdf.Valore(Constants.TagTipoModello);

			if (tipoModello == null)
			{
				tipoModello = String.Empty;
			}


			return tipoModello.ToUpperInvariant() == tipoModelloCercato.ToUpperInvariant();
		}

		public void RimuoviAllegatoDaDomanda(int idDomanda, string idAllegato)
		{
			var domanda = this._persistenzaStrategy.GetById(idDomanda);
			var datiBando = domanda.ReadInterface.BandiUmbria.DatiDomanda;

			var allegato = datiBando.TrovaAllegato(idAllegato);

			if (allegato != null)
			{
				domanda.WriteInterface.Documenti.EliminaAllegatoADocumentoDaCodiceOggetto(allegato.IdAllegato.Value);
				allegato.EliminaRiferimentiFile();
			}

			domanda.WriteInterface.BandiUmbria.ImpostaDatiDomanda(datiBando);

			this._persistenzaStrategy.Salva(domanda);
		}

		public IEnumerable<string> ValidaPresenzaAllegati(int idDomanda)
		{
			var domanda = this._persistenzaStrategy.GetById(idDomanda);
			var datiBando = domanda.ReadInterface.BandiUmbria.DatiDomanda;

			var allegati = new []{
				datiBando.Allegato1,
				datiBando.Allegato2,
				datiBando.Allegato3,
				datiBando.Allegato4
			};

			return allegati
					.Where( x => x != null)
					.Union
					(
						datiBando
							.Aziende
							.SelectMany(x => x.Allegati)
							.Where(x => x.IdModello.HasValue)
					)
					.Where(x => !x.IdAllegato.HasValue)
					.Select( x => String.Format("Nella domanda non è stato caricato l'allegato \"{0}\"",x.Descrizione) );
		}
		
		public IEnumerable<AllegatoDomandaBandi> GetAllegatiCheNecessitanoFirma(int idDomanda)
		{
			return GetAllegati(idDomanda).Where(x => x.IdModello.HasValue && x.IdAllegato.HasValue);
		}

		private IEnumerable<AllegatoDomandaBandi> GetAllegati(int idDomanda)
		{
			var domanda = this._persistenzaStrategy.GetById(idDomanda);
			var datiBando = domanda.ReadInterface.BandiUmbria.DatiDomanda;

			var allegati = new[]{
				datiBando.Allegato1,
				datiBando.Allegato2,
				datiBando.Allegato3,
				datiBando.Allegato4,
				datiBando.AllegatoAltreSediOperative
			};

			return allegati
					.Where(x => x != null)
					.Union
					(
						datiBando
							.Aziende
							.SelectMany(x => x.Allegati)
					);
		}
		
		public void AggiungiFileFirmatoAdAllegato(int idDomanda, string idAllegato, BinaryFile file)
		{
			var domanda = this._persistenzaStrategy.GetById(idDomanda);
			var datiBando = domanda.ReadInterface.BandiUmbria.DatiDomanda;

			var result = this._allegatiRepository.SalvaAllegato(idDomanda, file, false);

			if (!result.FirmatoDigitalmente)
			{
				throw new Exception(String.Format("Il file caricato ({0}) non è firmato digitalmente",file.FileName));
			}

			var allegato = datiBando.TrovaAllegato(idAllegato);

			if (allegato != null)
			{
				if (allegato.IdAllegatoFirmatoDigitalmente.HasValue)
				{
					this._allegatiRepository.EliminaAllegato(idDomanda, allegato.IdAllegato.Value);
				}

				allegato.EliminaRiferimentiFileFirmatoDigitalemnte();
			}

			allegato.SetRiferimentiFileFirmatoDigitalemente(result.CodiceOggetto, result.NomeFile);

			domanda.WriteInterface.BandiUmbria.ImpostaDatiDomanda(datiBando);
			
			this._persistenzaStrategy.Salva(domanda);
		}
		
		public void RimuoviFileFirmatoDaAllegato(int idDomanda, string idAllegato)
		{
			var domanda = this._persistenzaStrategy.GetById(idDomanda);
			var datiBando = domanda.ReadInterface.BandiUmbria.DatiDomanda;

			var allegato = datiBando.TrovaAllegato(idAllegato);

			if (allegato != null)
			{
				if (allegato.IdAllegatoFirmatoDigitalmente.HasValue)
				{
					this._allegatiRepository.EliminaAllegato(idDomanda, allegato.IdAllegatoFirmatoDigitalmente.Value);
				}

				allegato.EliminaRiferimentiFileFirmatoDigitalemnte();
			}

			domanda.WriteInterface.BandiUmbria.ImpostaDatiDomanda(datiBando);
			this._persistenzaStrategy.Salva(domanda);
		}
		
		public void PreparaAllegatiDomanda(int idDomanda)
		{
			var domanda = this._persistenzaStrategy.GetById(idDomanda);

			var allegati = GetAllegati(idDomanda);

			foreach (var allegato in allegati)
			{
				if (allegato.IdAllegatoFirmatoDigitalmente.HasValue)
				{
					var descrizione = allegato.NomeFileFirmatoDigitalemnte;
					var nomeFile = allegato.NomeFileFirmatoDigitalemnte;
					var codiceOggetto = allegato.IdAllegatoFirmatoDigitalmente.Value;

					domanda.WriteInterface.Documenti.AggiungiDocumentoInterventoLibero(descrizione, codiceOggetto, nomeFile, -1, "Allegati della domanda", true);
				}

				if (allegato.IdAllegato.HasValue)
				{
					var descrizione = allegato.NomeFile;
					var nomeFile = allegato.NomeFile;
					var codiceOggetto = allegato.IdAllegato.Value;

					domanda.WriteInterface.Documenti.AggiungiDocumentoInterventoLibero(descrizione, codiceOggetto, nomeFile, -1, "Allegati della domanda", false);
				}
			}

			this._persistenzaStrategy.Salva(domanda);
		}
	}
}
