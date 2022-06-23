using System;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici;
using Init.Sigepro.FrontEnd.GestioneMovimenti.DatiDinamici;
using Init.Sigepro.FrontEnd.Infrastructure.Dispatching;
using Init.SIGePro.DatiDinamici;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.StrutturaModelli;
using Init.Sigepro.FrontEnd.GestioneMovimenti.GestioneMovimento.GestioneMovimentoDaEffettuare;
using Init.Sigepro.FrontEnd.GestioneMovimenti.GestioneMovimento.GestioneMovimentoDiOrigine;
using Init.Sigepro.FrontEnd.AppLogic.ConversionePDF;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo;
using Init.Sigepro.FrontEnd.GestioneMovimenti.DatiDinamici.DataAccess;

namespace Init.Sigepro.FrontEnd.GestioneMovimenti.GenerazioneRiepiloghiSchedeDinamiche
{
	public class GenerazioneRiepilogoSchedeDinamicheService : IGenerazioneRiepilogoSchedeDinamicheService
	{
		private readonly IAliasResolver _aliasResolver;
		private readonly ICommandSender _commandSender;
		private readonly IDatiDinamiciRepository _datiDinamiciRepository;
		private readonly ITokenApplicazioneService _tokenService;
        private readonly IHtmlToPdfFileConverter _htmlToPdf;
		private readonly MovimentiIstanzeManager _istanzeManager;
        private readonly IMovimentiDiOrigineRepository _movimentiDiOrigineRepository;

		private static class Constants
		{
			public const string HtmlWrapper = "<html><head>{0}</head><body>{1}</body></html>";
			public const string NomeFiledaConvertire = "riepilogoScheda.htm";
			public const string Tipoconversione = "PDF";
		}

		public GenerazioneRiepilogoSchedeDinamicheService(	IAliasResolver aliasResolver, 
															ICommandSender commandSender, 
															IDatiDinamiciRepository datiDinamiciRepository, 
															ITokenApplicazioneService tokenService, 
                                                            IHtmlToPdfFileConverter htmlToPdf,
															MovimentiIstanzeManager istanzeManager,
                                                            IMovimentiDiOrigineRepository movimentiDiOrigineRepository)
		{
			this._aliasResolver = aliasResolver;
			this._commandSender = commandSender;
			this._datiDinamiciRepository = datiDinamiciRepository;
			this._tokenService = tokenService;
			this._istanzeManager = istanzeManager;
            this._movimentiDiOrigineRepository = movimentiDiOrigineRepository;
            this._htmlToPdf = htmlToPdf;
		}

		#region IGenerazioneRiepilogoSchedeDinamicheService Members

		public AppLogic.GestioneOggetti.BinaryFile GeneraRiepilogoScheda(MovimentoDaEffettuare movimento, int idScheda, string forzaNomeFile = "")
		{
            var movimentoDiOrigine = this._movimentiDiOrigineRepository.GetById(movimento);
			// var generatoreScheda = new GeneratoreHtmlSchedeDinamiche(this._tokenService, this._strutturaModelloReader);
            // var codiceIstanza = movimento.CodiceIstanza;
            var cacheModello = this._datiDinamiciRepository.GetCacheModelloDinamico(idScheda);

            var dap = new MovimentiDyn2DataAccessFactory(cacheModello, movimentoDiOrigine, movimento,this._commandSender, this._tokenService, this._istanzeManager);
			var loader = new ModelloDinamicoLoader(dap, this._aliasResolver.AliasComune, ModelloDinamicoLoader.TipoModelloDinamicoEnum.Frontoffice);
			var scheda = new ModelloDinamicoIstanza(loader, idScheda, 0, false);

			var html = String.Format(Constants.HtmlWrapper, GenerazioneHtmlDomandaConstants.CssModelliDinamici, new ModelloDinamicoHtmlRenderer(scheda).GetHtml());

            return this._htmlToPdf.Converti(String.IsNullOrEmpty(forzaNomeFile) ? Constants.NomeFiledaConvertire : forzaNomeFile, html);
		}

		#endregion
	}
}
