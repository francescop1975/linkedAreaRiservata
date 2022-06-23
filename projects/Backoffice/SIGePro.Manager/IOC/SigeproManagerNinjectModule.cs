using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDomandaOnLine;
using Init.SIGePro.Manager.Logic.GestioneIntegrazioneLDP;
using Init.SIGePro.Manager.Logic.GestioneIntegrazioneLDP.EventHandler;
using Init.SIGePro.Manager.Logic.GestioneSchedeAttivita;
using Init.SIGePro.Manager.Utils;
using Init.SIGePro.Manager.Verticalizzazioni;
using Init.SIGePro.Verticalizzazioni;
using Ninject.Modules;

namespace Init.SIGePro.Manager.IOC
{
    public class SigeproManagerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //domanda on line
            Kernel.Bind<IDomandaOnlineService>().To<DomandaOnlineService>().InTransientScope();

            //Servizi LDP
            Kernel.Bind<ILdpAnnullamentoServiceWrapper>().To<LdpProxyServiceWrapper>().InTransientScope();
            Kernel.Bind<IVerticalizzazioneAttiva<VerticalizzazioneSitLdp>>().To<VerticalizzazioneSitLdpAttiva>().InTransientScope();
            Kernel.Bind<GestioneIntegrazioneLDPDomandaFOEventHandler>().ToSelf().InTransientScope();
            Kernel.Bind<GestioneIntegrazioneLDPService>().ToSelf().InTransientScope();
            Kernel.Bind<ILdpProxyServiceWrapperFactory>().To<LdpProxyServiceWrapperFactory>().InTransientScope();

            Kernel.Bind<EventiSchedeDinamicheAttivitaService>().ToSelf().InTransientScope();
            Kernel.Bind<ISchedeDinamicheAttivitaService>().To<SchedeDinamicheAttivitaService>().InTransientScope();

            // Servizi di sistema
            Kernel.Bind<IAuthenticationInfoResolver>().To<HttpContextAuthenticationInfoResolver>().InTransientScope();
            Kernel.Bind<IDatabaseCreator>().To<DatabaseCreator>().InTransientScope();
            Kernel.Bind<IIdComuneResolver>().To<IdComuneResolver>().InTransientScope();
            Kernel.Bind<AuthenticationInfo>().ToMethod(context =>
            {
                var svc = (IAuthenticationInfoResolver)context.Kernel.GetService(typeof(IAuthenticationInfoResolver));
                return svc.Resolve();
            }).InTransientScope();
        }
    }
}
