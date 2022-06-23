using Init.Sigepro.FrontEnd.AppLogic.GestioneVbgConsole.Authentication;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneVbgConsole.IoC
{
    public static class GestioneVbgConsoleIoCExtension
    {
        public static IKernel RegistraClassiVbgConsole(this IKernel kernel)
        {
            kernel.Bind<IVbgConsoleService>().To<VbgConsoleService>().InRequestScope();
            kernel.Bind<IVbgCrossLoginClient>().To<VbgCrossLoginClient>().InRequestScope();
            kernel.Bind<IUrlConsoleRepository>().To<UrlConsoleRepository>().InRequestScope();

            return kernel;
        }
    }
}
