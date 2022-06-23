using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.Adapters.IOC;
using Init.Sigepro.FrontEnd.AppLogic.Adapters.StcPartialAdapters;
using Init.Sigepro.FrontEnd.AppLogic.AllegatiDomanda;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.AreaRiservata;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Stc;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.AutenticazioneUtente;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TraduzioneIdComune;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.WebConfig;
using Init.Sigepro.FrontEnd.AppLogic.CopiaDomanda;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.IoC;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.AllegaCertificatoDiInvio;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.StrategiaLetturaRiepilogo;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo;
using Init.Sigepro.FrontEnd.AppLogic.GestioneAnagrafiche;
using Init.Sigepro.FrontEnd.AppLogic.GestioneBookmarks;
using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.IoC;
using Init.Sigepro.FrontEnd.AppLogic.GestioneComuni;
using Init.Sigepro.FrontEnd.AppLogic.GestioneConversioneFiles.IoC;
using Init.Sigepro.FrontEnd.AppLogic.GestioneDatiExtra;
using Init.Sigepro.FrontEnd.AppLogic.GestioneEndoprocedimenti.IoC;
using Init.Sigepro.FrontEnd.AppLogic.GestioneInpsInail;
using Init.Sigepro.FrontEnd.AppLogic.GestioneInterventi;
using Init.Sigepro.FrontEnd.AppLogic.GestioneLocalizzazioni;
using Init.Sigepro.FrontEnd.AppLogic.GestioneLocalizzazioni.Modena;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.IoC;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOneri;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.IoC;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche.LogicaRisoluzioneSoggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche.Sincronizzazione;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneDocumenti.LogicaSincronizzazione;
using Init.Sigepro.FrontEnd.AppLogic.GestioneQuestionario.IOC;
using Init.Sigepro.FrontEnd.AppLogic.GestioneRisorseTestuali;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.IoC;
using Init.Sigepro.FrontEnd.AppLogic.GestioneTipiSoggetto;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVbgConsole.IoC;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Init.Sigepro.FrontEnd.AppLogic.InvioDomanda;
using Init.Sigepro.FrontEnd.AppLogic.InvioDomanda.MessaggiErroreInvio;
using Init.Sigepro.FrontEnd.AppLogic.Livorno.PortaleCittadino;
using Init.Sigepro.FrontEnd.AppLogic.ReadInterface;
using Init.Sigepro.FrontEnd.AppLogic.ReadInterface.Imp;
using Init.Sigepro.FrontEnd.AppLogic.RedirectFineDomanda;
using Init.Sigepro.FrontEnd.AppLogic.RedirectFineDomanda.CopiaDomanda;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.WebServices;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.Services.Cart;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using Init.Sigepro.FrontEnd.AppLogic.Services.Navigation;
using Init.Sigepro.FrontEnd.AppLogic.STC;
using Init.Sigepro.FrontEnd.AppLogic.Utils;
using Init.Sigepro.FrontEnd.AppLogic.VerificaFirmaDigitale;
using Init.Sigepro.FrontEnd.AppLogic.Wrappers;
using Init.Sigepro.FrontEnd.Infrastructure.DatesAndTimes;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.Infrastructure.Web;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Configuration;



namespace Init.Sigepro.FrontEnd.AppLogic.IoC
{
    public class IoCConfigurationModule : NinjectModule
	{

        public override void Load()
        {
            Bind<AreaRiservataServiceCreator>().ToSelf();
            Bind<IDateTimeServiceWrapper>().To<DateTimeServiceWrapper>().InRequestScope();

            // Modalità di invio
            Bind<IInvioDomandaStrategy>().To<InvioDomandaSTCStrategy>();

            // Logica di salvataggio xml
            var tipoLogicaSalvataggioXmlEnum = ConfigurationManager.AppSettings["logicaSalvataggioXml"];

            var logicaSalvataggio = TipoLogicaSalvataggioXmlEnum.CachedThreaded;

            if (!String.IsNullOrEmpty(tipoLogicaSalvataggioXmlEnum))
                logicaSalvataggio = (TipoLogicaSalvataggioXmlEnum)Enum.Parse(typeof(TipoLogicaSalvataggioXmlEnum), tipoLogicaSalvataggioXmlEnum);

            switch (logicaSalvataggio)
            {
                case TipoLogicaSalvataggioXmlEnum.Default:
                    Bind<ISalvataggioDomandaStrategy>().To<SalvataggioDirettoStrategy>();
                    break;
                case TipoLogicaSalvataggioXmlEnum.Cached:
                    Bind<ISalvataggioDomandaStrategy>().To<SalvataggioCachedStrategy>();
                    break;
                default:
                    //Bind<ISalvataggioDomandaStrategy>().To<SalvataggioCachedThreadedStrategy>();
                    Bind<ISalvataggioDomandaStrategy>().To<SalvataggioCachedStrategy>();

                    break;
            }

            // Gestione interventi
            Bind<IInterventiRepository>().To<WsInterventiRepository>();
            Bind<IResolveDescrizioneIntervento>().To<ResolveDescrizioneIntervento>();
            Bind<InterventiServiceCreator>().ToSelf().InRequestScope();

            Bind<IAlboPretorioRepository>().To<WsAlboPretorioRepository>();
            Bind<IAllegatiDomandaFoRepository>().To<WsAllegatiDomandaFoRepository>();
            Bind<IAnagraficheRepository>().To<WsAnagraficheRepository>();
            Bind<IAtecoRepository>().To<WsAtecoRepository>();
            Bind<ICampiRicercaVisuraRepository>().To<WsCampiRicercaVisuraRepository>();
            Bind<ICartRepository>().To<WsCartRepository>();
            Bind<IComuniRepository>().To<WsCachedComuniRepository>().InRequestScope();
            Bind<IConfigurazioneContenutiRepository>().To<WsConfigurazioneContenutiRepository>();
            Bind<IConfigurazioneAreaRiservataRepository>().To<WsConfigurazioneAreaRiservataRepository>();
            Bind<IConfigurazioneVbgRepository>().To<WsConfigurazioneVbgRepository>();



            Bind<IDatiDomandaFoRepository>().To<WsDatiDomandaFoRepository>();
            Bind<IDomandeEndoRepository>().To<WsDomandeEndoRepository>();
            Bind<IElenchiProfessionaliRepository>().To<WsElenchiProfessionaliRepository>();
            Bind<IFormeGiuridicheRepository>().To<WsFormeGiuridicheRepository>();
            Bind<IInterventiAllegatiRepository>().To<WsInterventiAllegatiRepository>();
            Bind<IIstanzePresentateRepository>().To<WsIstanzePresentateRepository>();
            //			Bind<IMovimentiRepository>().To<WsMovimentiRepository>();
            Bind<IMessaggiFrontofficeRepository>().To<WsMessaggiFrontofficeRepository>();

            Bind<IOneriRepository>().To<OneriRepository>();
            //Bind<IScadenzeRepository>().To<ScadenzeRepository>();
            Bind<ISoftwareRepository>().To<WsSoftwareRepository>();
            Bind<ISottoscrizioniRepository>().To<WsSottoscrizioniRepository>();
            Bind<IStatiIstanzaRepository>().To<WsStatiIstanzaRepository>();
            Bind<IStradarioRepository>().To<WsStradarioRepository>();
            Bind<ITipiSoggettoRepository>().To<TipiSoggettoRepository>();
            Bind<ITitoliRepository>().To<WsTitoliRepository>();
            //Bind<IVisuraRepository>().To<WsVisuraRepository>().InRequestScope();

            Bind<IComuniService>().To<ComuniService>();
            Bind<IComuniAssociatiService>().To<ComuniAssociatiService>().InRequestScope();
            Bind<ICittadinanzeService>().To<CittadinanzeService>();
            Bind<ITipiSoggettoService>().To<TipiSoggettoService>();

            Bind<ILogicaSincronizzazioneTipiSoggetto>().To<LogicaSincronizzazioneTipiSoggetto>();
            Bind<IAnagraficheService>().To<AnagraficheService>();
            //Bind<IMovimentiService>().To<MovimentiService>();
            // File converter

            // Check Browser
            Bind<ICheckBrowserService>().To<CheckBrowserService>();

            //WrapperService
            Bind<IGuidWrapperService>().To<GuidServiceWrapper>();


            Bind<IIdPresentazioneResolver>().To<IdPresentazioneFromQuerystringResolver>().InRequestScope();
            Bind<IAliasResolver>().To<QuerystringAliasResolver>().InRequestScope();
            Bind<IAliasSoftwareResolver>().To<QuerystringAliasSoftwareResolver>().InRequestScope();
            Bind<ISoftwareResolver>().To<QuerystringAliasSoftwareResolver>().InRequestScope();
            Bind<IAuthenticationDataResolver>().To<ContextAuthenticationDataResolver>().InRequestScope();
            Bind<IIdDomandaResolver>().To<QuerystringIdDomandaResolver>().InRequestScope();
            Bind<ITokenResolver>().To<FormsAuthenticationService>().InRequestScope();
            Bind<IAreaRiservataAuthenticationService>().To<FormsAuthenticationService>().InRequestScope();

            // Services
            Bind<LocalizzazioniService>().ToSelf();
            Bind<DomandeOnlineService>().ToSelf();
            Bind<EventiDomandaService>().ToSelf();
            Bind<InvioDomandaService>().ToSelf();
            Bind<ProcureService>().ToSelf();



            Bind<ITokenApplicazioneService>().To<TokenApplicazioneService>();
            Bind<IAliasToIdComuneTranslator>().To<AliasToIdComuneTranslator>();

            // Generazione certificato di invio
            Bind<IReadFacade>().To<ReadFacadeImp>().InRequestScope();
            Bind<IReadDatiDomanda>().To<ReadFacadeImp>().InRequestScope();
            Bind<GeneratoreCertificatoDiInvio>().ToSelf().InRequestScope();
            Bind<CertificatoDiInvioFinderDaMetadati>().ToSelf().InRequestScope();
            Bind<CertificatoDiInvioFinderDaVisura>().ToSelf().InRequestScope();
            Bind<ICertificatoDiInvioFinder>().To<CompositeCertificatoInvioFinder>().InRequestScope();
            Bind<IAllegaCertificatoDiInvioService>().To<AllegaCertificatoDiInvioService>().InRequestScope();

            // Ricerca del riepilogo della domanda online
            Bind<IndividuazioneCertificatoInvioDaConfigurazione>().ToSelf();
            Bind<IndividuazioneCertificatoInvioDaProcedura>().ToSelf();
            Bind<IstanzaStcAdapter>().ToSelf();
            Bind<IStrategiaIndividuazioneCertificatoInvio>().To<IndividuazioneCertificatoInvioDaProceduraOConfigurazione>();

            // Messaggi notifica
            Bind<IMessaggiNotificaInvioService>().To<MessaggiNotificaInvioService>();
            Bind<IMessaggioErroreInvioService>().To<MessaggioErroreInvioService>();

            // stc
            Bind<IStcService>().To<StcServiceImpl>();
            Bind<IIstanzaStcAdapter>().To<IstanzaStcAdapter>();
            Bind<StcToken>().ToSelf();
            Bind<IStcServiceCreator>().To<StcServiceCreator>().InRequestScope();

            // CART
            Bind<IFacctRedirectService>().To<FacctRedirectService>();

            // firma digitale
            Bind<IFirmaDigitaleMetadataService>().To<VerificaFirmaDigitaleService>();
            Bind<IVerificaFirmaDigitaleService>().To<VerificaFirmaDigitaleService>();

            // Dati dinamici


            // Segnaposto schede dinamiche
            Bind<ISostituzioneSegnapostoRiepilogoService>().To<SostituzioneSegnapostoRiepilogoService>().InRequestScope();
            Bind<IGeneratoreHtmlSchedeDinamiche>().To<GeneratoreHtmlSchedeDinamiche>().InRequestScope();

            // Dati dinamici della visura

            //Bind<VisuraDyn2ModelliManager>().ToSelf().InRequestScope();
            //Bind<VisuraIstanzeDyn2DatiManager>().ToSelf().InRequestScope();


            // Redirect a fine presentazione  e copia dati domanda
            Bind<IRedirectFineDomandaService>().To<RedirectFineDomandaService>().InRequestScope();
            Bind<ICopiaDatiDomandaService>().To<CopiaDatiDomandaService>().InRequestScope();

            // Gestione FVG-SOL
            Bind<FVGWebServiceProxyFactory>().ToSelf().InRequestScope();
            Bind<IFVGWebServiceProxy>().ToMethod((ctxt) => {
                var svc = (FVGWebServiceProxyFactory)ctxt.Kernel.GetService(typeof(FVGWebServiceProxyFactory));

                return svc.CreateService();
            }).InRequestScope();

            // Autenticazione
            Bind<VbgAuthenticationService>().ToSelf().InRequestScope();
            Bind<IUserCredentialsStorage>().To<UserCredentialsStorage>();

            // STC
            Bind<ICodiceAccreditamentoHelper>().To<CodiceAccreditamentoHelper>();

            // infrastruttura
            Bind<IResolveHttpContext>().To<ResolveHttpContext>();
            Bind<IRisorseTestualiService>().To<CachedRisorseTestualiService>().InRequestScope();
            Bind<IUrlEncoder>().To<HttpUtilityUrlEncoder>().InRequestScope();
            Bind<SigeproSecurityProxy>().ToSelf();

            // Presentazione domanda
            Bind<ILogicaSincronizzazioneAllegatiIntervento>().To<LogicaSincronizzazioneAllegatiIntervento>();
            Bind<ILogicaRisoluzioneTecnico>().To<LogicaRisoluzioneTecnico>();
            Bind<ILogicaSincronizzazioneOneri>().To<LogicaSincronizzazioneOneri>().InTransientScope();
            Bind<IInpsInailService>().To<InpsInailService>();
            
            Bind<IBookmarksService>().To<BookmarksService>();
            Bind<ILocalizzazioniService>().To<LocalizzazioniService>();
            Bind<IPortaleCittadinoService>().To<PortaleCittadinoService>().InRequestScope();

            Bind<IDatiExtraService>().To<DatiExtraService>().InRequestScope();

            //Copia domanda da template istanza di backoffice
            Bind<CopiaDomandaService>().ToSelf();
            Bind<CopiaDomandaAltriDatiAdapter>().ToSelf();
            Bind<CopiaDomandaAnagraficheAdapter>().ToSelf();
            Bind<CopiaDomandaLocalizzazioniAdapter>().ToSelf();
            Bind<CopiaDomandaDatiDinamiciAdapter>().ToSelf();
            Bind<CopiaDomandaEndoprocedimentiAdapter>().ToSelf();
            Bind<CopiaDomandaProcureAdapter>().ToSelf();
            Bind<CopiaDomandaAllegatiAdapter>().ToSelf();

            Bind<ICurrentDateTimeProvider>().To<CurrentDateTimeProvider>().InTransientScope();


            Bind<ILocalizzazioniModenaService>().To<LocalizzazioniModenaService>();

            //Download ZIP della domanda
            Bind<DownloadDomandaZIPService>().ToSelf();

            // Kernel.RegistraClassiAidaSmart();
            Kernel.RegistraBandiUmbria()
                    .RegistraClassiConversionePDF()
                    .RegistraGestioneFilesExcel()
                    .RegistraGestioneOggetti()
                    .RegistraIntegrazionePagamenti()
                    .RegistraParametriConfigurazione()
                    .RegistraSIT()
                    .RegistraWorkflow()
                    .RegistraClassiScrivaniaEntiTerzi()
                    .RegistraParametriAccessoAtti()
                    .RegistraGestioneTransiti()
                    .RegistraServiziFVG()
                    .RegistraClassiVbgConsole()
                    .RegistraGestioneEndo()
                    .RegistraDatiDinamici()
                    .RegistraConversioneFiles()
                    .RegistraClassiCommissioni()
                    .RegistraSigeproAdapters()
                    .RegistraQuestionarioSoddisfazione();

            Kernel.Bind<Tls12Utils>().ToSelf().InRequestScope();
        }
    }
}
