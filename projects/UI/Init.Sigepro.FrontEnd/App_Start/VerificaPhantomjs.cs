[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Init.Sigepro.FrontEnd.App_Start.VerificaPhantomjs), "Test")]


namespace Init.Sigepro.FrontEnd.App_Start
{

    using Init.Sigepro.FrontEnd.AppLogic.ConversionePDF;

    public class VerificaPhantomjs
    {
        public static void Test()
        {
            new PhantomjsBootstrapper().Bootstrap();
        }
    }
}