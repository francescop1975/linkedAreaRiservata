using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.Database;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.DebugConfiguration;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.ManagedData;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.ManagedData.MappingDaManagedData;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.IoC
{
    internal static class ConfigurazioneServiziFVG
    {
        public static IKernel RegistraServiziFVG(this IKernel kernel)
        {
            kernel.Bind<ServiziFVGService>().ToSelf().InRequestScope();
            kernel.Bind<IFvgDatabaseFactory>().To<FvgDatabaseFactory>().InRequestScope();
            kernel.Bind<IFvgManagedDataRepository>().To<FvgManagedDataRepository>().InRequestScope();
            kernel.Bind<IFVGDebugConfiguration>().To<FVGDebugConfiguration>();

            return kernel;
        }
    }
}
