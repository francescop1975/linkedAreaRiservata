using Init.Sigepro.FrontEnd.AppLogic.Adapters.SigeproPartialAdapters;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Adapters
{
    public class IstanzaSigeproAdapterServiceFactory
    {
        private readonly List<IIstanzaSigeproPartialAdapter> _adapters = new List<IIstanzaSigeproPartialAdapter>();
        private readonly IConfigurazioneVbgRepository _configurazioneVbgRepository;

        public IstanzaSigeproAdapterServiceFactory(DatiGeneraliSigeproAdapter datiGeneraliSigeproAdapter, AnagraficheSigeproAdapter anagraficheSigeproAdapter,
            StradarioSigeproAdapter stradarioSigeproAdapter, ProcedimentiSigeproAdapter procedimentiSigeproAdapter,
            DocumentiSigeproAdapter documentiSigeproAdapter, OneriSigeproAdapter oneriSigeproAdapter,
            DatiCatastaliSigeproAdapter datiCatastaliSigeproAdapter, EventiSigeproAdapter eventiSigeproAdapter,
            DatiDinamiciSigeproAdapter datiDinamiciSigeproAdapter, IConfigurazioneVbgRepository configurazioneVbgRepository)
        {
            this._adapters.Add(datiGeneraliSigeproAdapter);
            this._adapters.Add(anagraficheSigeproAdapter);
            this._adapters.Add(stradarioSigeproAdapter);
            this._adapters.Add(procedimentiSigeproAdapter);
            this._adapters.Add(documentiSigeproAdapter);
            this._adapters.Add(oneriSigeproAdapter);
            this._adapters.Add(datiCatastaliSigeproAdapter);
            this._adapters.Add(eventiSigeproAdapter);
            this._adapters.Add(datiDinamiciSigeproAdapter);
            this._configurazioneVbgRepository = configurazioneVbgRepository;
        }

        public IIstanzaSigeproAdapterService Create()
        {
            return new IstanzaSigeproAdapterService(this._adapters, this._configurazioneVbgRepository);
        }
    }
}
