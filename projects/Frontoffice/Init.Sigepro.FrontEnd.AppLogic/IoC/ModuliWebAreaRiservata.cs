using Ninject.Modules;

namespace Init.Sigepro.FrontEnd.AppLogic.IoC
{
    public static class ModuliWebAreaRiservata
    {
        public static NinjectModule[] Get()
        {
            return new NinjectModule[]{
                new IoCConfigurationModule()	// Deve sempre essere l'ultimo 				
			};
        }
    }
}
