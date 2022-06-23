using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.AccessoPIN;
using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.AccessoPIN.WebService;
using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.Votazioni;
using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.Votazioni.WebService;
using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.WebService;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.IoC
{
    public static class CommissioniNinjectExtensions
    {
        public static IKernel RegistraClassiCommissioni(this IKernel k)
        {
            k.Bind<CommissioniWsProxyCreator>().ToSelf().InRequestScope();
            k.Bind<ICommissioniService>().To<CommissioniService>().InRequestScope();
            k.Bind<ICommissioniDao>().To<CommissioniWsDao>().InRequestScope();

            // Votazioni
            k.Bind<VotazioniCommissioneWsProxyCreator>().ToSelf().InRequestScope();
            k.Bind<IVotazioniCommissioniService>().To<VotazioniCommissioneService>().InRequestScope();
            k.Bind<IVotazioniCommissioneDao>().To<VotazioniCommissioneWsDao>().InRequestScope();

            // Accesso PIN
            k.Bind<CommissioniAccessoPINServiceCreator>().ToSelf().InSingletonScope();
            k.Bind<IAccessoPINService>().To<AccessoPINService>().InRequestScope();
            k.Bind<IAccessoPINDao>().To<AccessoPINWsDao>().InRequestScope();

            return k;
        }
    }
}
