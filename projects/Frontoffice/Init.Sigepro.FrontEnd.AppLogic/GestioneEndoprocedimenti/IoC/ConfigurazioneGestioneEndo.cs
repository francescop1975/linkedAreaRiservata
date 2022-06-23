using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.Allegati;
using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.EndoAcquisiti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.Frontoffice;
using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.Incompatibilita;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.IoC
{
    internal static class ConfigurazioneGestioneEndo
    {
        public static IKernel RegistraGestioneEndo(this IKernel k)
        {
            // Endo area riservata
            k.Bind<IEndoprocedimentiRepository>().To<WsEndoprocedimentiRepository>().InRequestScope();
            k.Bind<IEndoprocedimentiService>().To<EndoprocedimentiService>().InRequestScope();
            k.Bind<IEndoAcquisitiService>().To<EndoprocedimentiService>().InRequestScope();
            k.Bind<EndoprocedimentiServiceCreator>().ToSelf().InRequestScope();

            // Endo frontoffice (servizi js)
            k.Bind<IEndoprocedimentiFrontofficeRepository>().To<WsEndoprocedimentiFrontofficeRepository>().InRequestScope();
            k.Bind<EndoprocedimentiFrontofficeService>().ToSelf().InRequestScope();
            k.Bind<EndoFrontofficeServiceCreator>().ToSelf().InRequestScope();

            // Compatibilità procedimenti
            k.Bind<IEndoprocedimentiIncompatibiliRepository>().To<WsEndoprocedimentiRepository>().InRequestScope();
            k.Bind<IEndoprocedimentiIncompatibiliService>().To<EndoprocedimentiIncompatibiliService>().InRequestScope();

            // allegati
            k.Bind<IAllegatiEndoprocedimentiRepository>().To<WsAllegatiEndoprocedimentiRepository>().InRequestScope();
            k.Bind<IAllegatiEndoprocedimentiService>().To<AllegatiEndoprocedimentiService>().InRequestScope();

            return k;
        }
    }
}
