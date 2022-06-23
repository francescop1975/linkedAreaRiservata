using Init.Sigepro.FrontEnd.AppLogic.Adapters.StcPartialAdapters;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.StrutturaModelli;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.GestioneTipiSoggetto;
using Init.Sigepro.FrontEnd.AppLogic.StcService;

namespace Init.Sigepro.FrontEnd.AppLogic.Adapters
{
    public interface IIstanzaStcAdapter
    {
        DettaglioPraticaType Adatta(DomandaOnline domandaFo);
    }


    internal class IstanzaStcAdapter : IIstanzaStcAdapter
    {
        private readonly ITipiSoggettoService _tipiSoggettoService;
        private readonly ICodiceAccreditamentoHelper _codiceAccreditamentoHelper;
        private readonly IStrutturaModelloReader _strutturaModelloReader;
        private readonly IConfigurazione<ParametriStc> _parametriStc;

        public IstanzaStcAdapter(IAliasResolver aliasSoftwareResolver, ITipiSoggettoService tipiSoggettoRepository, ICodiceAccreditamentoHelper codiceAccreditamentoHelper, IStrutturaModelloReader strutturaModelloReader, IConfigurazione<ParametriStc> parametriStc)
        {
            this._tipiSoggettoService = tipiSoggettoRepository;
            this._codiceAccreditamentoHelper = codiceAccreditamentoHelper;
            this._strutturaModelloReader = strutturaModelloReader;
            this._parametriStc = parametriStc;
        }


        public DettaglioPraticaType Adatta(DomandaOnline domandaFo)
        {
            var readInterface = domandaFo.ReadInterface;
            var dettaglioPratica = new DettaglioPraticaType();


            var adapters = new IStcPartialAdapter[]
            {
                new DatiPraticaAdapter(),
                new ComuniAssociatiAdapter(),
                new RichiedenteAdapter(),
                new AziendaAdapter(this._tipiSoggettoService),
                new TecnicoAdapter(),
                new AltriSoggettiAdapter(this._parametriStc),
                new ProcureAdapter(),
                new LocalizzazioneAdapter(),
                new DocumentiAdapter(),
                new AltriDatiAdapter(this._codiceAccreditamentoHelper),
                new ProcedimentiAdapter(),
                new OneriAdapter(),
                new DatiDinamiciAdapter(this._strutturaModelloReader)
            };

            foreach (var adapter in adapters)
                adapter.Adapt(readInterface, dettaglioPratica);

            return dettaglioPratica;
        }


    }
}
