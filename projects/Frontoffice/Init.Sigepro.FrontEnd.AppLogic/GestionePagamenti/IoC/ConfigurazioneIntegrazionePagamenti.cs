using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2.Builders;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.EntraNext;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.ESED;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.MIP;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.NODOPAGAMENTI;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.NODOPAGAMENTI.Anagrafiche;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.NODOPAGAMENTI.Configurazione;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.NODOPAGAMENTI.Conti;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.Pagamenti;
using Init.Sigepro.FrontEnd.Pagamenti.ENTRANEXT;
using Init.Sigepro.FrontEnd.Pagamenti.ESED;
using Init.Sigepro.FrontEnd.Pagamenti.MIP;
using Init.Sigepro.FrontEnd.Pagamenti.NODOPAGAMENTI;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.IoC
{
    public static class ConfigurazioneIntegrazionePagamenti
    {
        public static IKernel RegistraIntegrazionePagamenti(this IKernel k)
        {
            // NODO_PAGAMENTI
            k.Bind<INodoPagamentiSettingsReader>().To<NodoPagamentiSettingsReader>().InRequestScope();
            k.Bind<IPagamentiNodoPagamentiService>().To<PagamentiNodoPagamentiService>().InRequestScope();
            k.Bind<IConfigurazioneNodoPagamentiRepository>().To<ConfigurazioneNodoPagamentiRepository>().InRequestScope();
            k.Bind<IVerificaConfigurazioneNodoPagamentiService>().To<VerificaConfigurazioneNodoPagamentiService>().InRequestScope();
            // k.Bind<IConfigurazioneBuilder<ParametriConfigurazionePagamentiNodoPagamenti>>().To<ParametriPagamentiNodoPagamentiServiceBuilder>().InRequestScope();
            k.Bind<INodoPagamentiPaymentService>().To<NodoPagamentiPaymentService>().InRequestScope();
            k.Bind<IContiRepository>().To<ContiRepository>();
            k.Bind<IEstremiDomandaNodoPagamentiReader>().To<EstremiDomandaNodoPagamentiReader>().InRequestScope();

            // Entranext
            k.Bind<IConfigurazione<ParametriConfigurazionePagamentiEntraNext>>().To<ConfigurazioneImpl<ParametriConfigurazionePagamentiEntraNext>>().InRequestScope();
            k.Bind<IConfigurazioneBuilder<ParametriConfigurazionePagamentiEntraNext>>().To<ParametriPagamentiEntraNextServiceBuilder>().InRequestScope();
            k.Bind<IPagamentiEntraNextSettingsReader>().To<PagamentiEntraNextSettingsReader>().InRequestScope();

            // ESED
            k.Bind<IGetStatoPagamento>().To<GetStatoPagamentoESED>().InRequestScope();
            k.Bind<PayServerClientWrapperESED>().ToMethod(x =>
            {

                var settingsReader = x.Kernel.Get<IPagamentiSettingsReader>();
                var urlEncoder = x.Kernel.Get<IUrlEncoder>();
                var getStatoPagamento = x.Kernel.Get<IGetStatoPagamento>();

                return new PayServerClientWrapperESED(new PayServerClientSettings(settingsReader.GetSettings()), urlEncoder, getStatoPagamento);
            }).InRequestScope();

            // MIP
            k.Bind<IConfigurazione<ParametriConfigurazionePagamentiMIP>>().To<ConfigurazioneImpl<ParametriConfigurazionePagamentiMIP>>().InRequestScope();
            k.Bind<IConfigurazioneBuilder<ParametriConfigurazionePagamentiMIP>>().To<ParametriPagamentiMipServiceBuilder>().InRequestScope();
            k.Bind<IPagamentiSettingsReader>().To<PagamentiMipSettingsReader>().InRequestScope();
            k.Bind<IMIPPaymentRequestFactory>().To<MIPPaymentRequestFactory>().InRequestScope();

            return k;
        }
    }
}
