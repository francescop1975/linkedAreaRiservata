using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneOneri;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Init.Sigepro.FrontEnd.AppLogic.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Adapters.SigeproPartialAdapters
{
    public class OneriSigeproAdapter : IIstanzaSigeproPartialAdapter
    {
        private readonly IDateTimeServiceWrapper _dateTimeServiceWrapper;

        public OneriSigeproAdapter(IDateTimeServiceWrapper dateTimeServiceWrapper)
        {
            _dateTimeServiceWrapper = dateTimeServiceWrapper;
        }

        public void Adatta(IDomandaOnlineReadInterface src, Istanze dst, IstanzaSigeproAdapterFlags flags)
        {
            if (src.Oneri.Oneri.Count() == 0)
                return;

            dst.Oneri = src.Oneri
                           .Oneri
                            .Select(x => new IstanzeOneri
                            {
                                CODICEINVENTARIO = x.Provenienza == OnereFrontoffice.ProvenienzaOnereEnum.Endoprocedimento ? x.EndoOInterventoOrigine.ToString() : String.Empty,
                                PREZZO = (double?)x.ImportoPagato,
                                FLENTRATAUSCITA = "1",
                                DATA = this._dateTimeServiceWrapper.NewDate(),
                                ImportoPagato = src.Oneri.AttestazioneDiPagamento.Presente ? 1 : 0,
                                CausaleOnere = new TipiCausaliOneri
                                {
                                    CoDescrizione = x.Causale.Descrizione
                                }
                            })
                            .ToArray();
        }
    }
}
