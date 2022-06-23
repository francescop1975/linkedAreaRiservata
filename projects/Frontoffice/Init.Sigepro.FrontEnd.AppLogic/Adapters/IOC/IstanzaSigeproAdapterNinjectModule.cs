using Init.Sigepro.FrontEnd.AppLogic.Adapters.SigeproPartialAdapters;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Adapters.IOC
{
    public static class IstanzaSigeproAdapterNinjectModule
    {
        public static IKernel RegistraSigeproAdapters(this IKernel k)
        {
            k.Bind<AnagraficheSigeproAdapter>().ToSelf().InRequestScope();
            k.Bind<DatiCatastaliSigeproAdapter>().ToSelf().InRequestScope();
            k.Bind<DatiDinamiciSigeproAdapter>().ToSelf().InRequestScope();
            k.Bind<DatiGeneraliSigeproAdapter>().ToSelf().InRequestScope();
            k.Bind<DocumentiSigeproAdapter>().ToSelf().InRequestScope();
            k.Bind<EventiSigeproAdapter>().ToSelf().InRequestScope();
            k.Bind<OneriSigeproAdapter>().ToSelf().InRequestScope();
            k.Bind<ProcedimentiSigeproAdapter>().ToSelf().InRequestScope();
            k.Bind<StradarioSigeproAdapter>().ToSelf().InRequestScope();
            k.Bind<IstanzaSigeproAdapterServiceFactory>().ToSelf().InRequestScope();
            k.Bind<IIstanzaSigeproAdapterService>().ToMethod(ctxt => {
                var factory = (IstanzaSigeproAdapterServiceFactory)ctxt.Kernel.GetService(typeof(IstanzaSigeproAdapterServiceFactory));

                return factory.Create();
            }).InRequestScope();


            return k;
        }
    }
}
