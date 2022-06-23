[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Init.Sigepro.FrontEnd.App_Start.DomandaOnlineEventBus), "Start")]

namespace Init.Sigepro.FrontEnd.App_Start
{
    using Init.Sigepro.FrontEnd.AppLogic.AutomapperBootstrapper;
    using Init.Sigepro.FrontEnd.AppLogic.IoC;
    using Init.Sigepro.FrontEnd.GestioneMovimenti.Bootstrap;

    public static class DomandaOnlineEventBus
    {
        public static void Start()
        {
            DomandaOnlineEventBusBootstrapper.Bootstrap();

            //var config = (GestioneMovimentiBootstrapper.GestioneMovimentiBootstrapperSettings)FoKernelContainer.Kernel.GetService(typeof(GestioneMovimentiBootstrapper.GestioneMovimentiBootstrapperSettings));

            //GestioneMovimentiBootstrapper.Bootstrap(config);

            AutomapperApplogicBootstrapper.Bootstrap();
            MovimentiAutomapperBootstrapper.Bootstrap();
        }
    }
}