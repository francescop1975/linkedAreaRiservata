// -----------------------------------------------------------------------
// <copyright file="IriepilogoModelloInHtml.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.GenerazionePdfModelli
{
    using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
    using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo.LetturaDatiDinamici;
    using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo;
    using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo.LetturaDatiDinamici.LetturaDaDomandaOnline;
    using Init.Sigepro.FrontEnd.AppLogic.Adapters;

    public class RiepilogoModelloInHtmlFactory : IRiepilogoModelloInHtmlFactory
    {
        private readonly ISalvataggioDomandaStrategy _salvataggioDomandaStrategy;
        private readonly IGeneratoreHtmlSchedeDinamiche _generatoreHtml;
        private readonly IDatiDinamiciRepository _datiDinamiciRepository;
        private readonly IIstanzaSigeproAdapterService _istanzaSigeproAdapterService;

        public RiepilogoModelloInHtmlFactory(ISalvataggioDomandaStrategy salvataggioDomandaStrategy, IGeneratoreHtmlSchedeDinamiche generatoreHtml, 
            IDatiDinamiciRepository datiDinamiciRepository, IIstanzaSigeproAdapterService istanzaSigeproAdapterService)
        {
            this._salvataggioDomandaStrategy = salvataggioDomandaStrategy;
            this._generatoreHtml = generatoreHtml;
            this._datiDinamiciRepository = datiDinamiciRepository;
            _istanzaSigeproAdapterService = istanzaSigeproAdapterService;
        }

        public IRiepilogoModelloInHtml FromDomanda(IDatiDinamiciRiepilogoReader reader, int idModello, int indiceMolteplicita = -1)
        {
            return new RiepilogoModelloInHtmlDaDomanda(this._generatoreHtml, reader, idModello, indiceMolteplicita);
        }

        public IRiepilogoModelloInHtml FromIdDomandaOnline(int idDomanda, int idModello, int indiceMolteplicita = -1)
        {
            var domanda = this._salvataggioDomandaStrategy.GetById(idDomanda);

            var reader = new DomandaOnlineDatiDinamiciReader(domanda, this._datiDinamiciRepository, this._istanzaSigeproAdapterService);


            return this.FromDomanda(reader, idModello, indiceMolteplicita);
        }

    }
}
