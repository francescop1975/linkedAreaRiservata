using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.GenerazionePdfModelli;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Ricerche;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.StrutturaModelli;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Visura;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.IoC
{
    public static class DatiDinamiciNinjectModule
    {
        public static IKernel RegistraDatiDinamici(this IKernel k)
        {
            k.Bind<IDatiDinamiciRepository>().To<WsDatiDinamiciRepository>().InRequestScope();
            k.Bind<IDatiDinamiciService>().To<DatiDinamiciService>().InRequestScope();
            k.Bind<IModelliDinamiciService>().To<ModelliDinamiciService>().InRequestScope();
            k.Bind<WsDatiDinamiciServiceCreator>().ToSelf().InTransientScope();
            k.Bind<IRicercheDatiDinamiciService>().To<RicercheDatiDinamiciService>();
            k.Bind<IVisuraDatiDinamiciService>().To<VisuraDatiDinamiciService>().InRequestScope();
            k.Bind<IGeneratoreRiepilogoModelloDinamico>().To<GeneratoreRiepilogoModelloDinamico>().InRequestScope();
            k.Bind<IRiepilogoModelloInHtmlFactory>().To<RiepilogoModelloInHtmlFactory>().InRequestScope();
            k.Bind<IStrutturaModelloReader>().To<StrutturaModelloReader>().InRequestScope();

            return k;
        }

    }
}
