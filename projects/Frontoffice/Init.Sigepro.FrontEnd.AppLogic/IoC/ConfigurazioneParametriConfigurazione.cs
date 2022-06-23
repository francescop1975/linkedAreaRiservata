using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2.Builders;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.Configurazione;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.IoC
{
    internal static class ConfigurazioneParametriConfigurazione
    {
        public static IKernel RegistraParametriConfigurazione(this IKernel kernel)
        {
            // Aspetto
            kernel.Bind<IConfigurazione<ParametriAspetto>>().To<ConfigurazioneImpl<ParametriAspetto>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriAspetto>>().To<ParametriAspettoBuilder>().InRequestScope();

            // Dati catastali
            kernel.Bind<IConfigurazione<ParametriDatiCatastali>>().To<ConfigurazioneImpl<ParametriDatiCatastali>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriDatiCatastali>>().To<ParametriDatiCatastaliBuilder>().InRequestScope();

            // Parametri invio domanda
            kernel.Bind<IConfigurazione<ParametriInvio>>().To<ConfigurazioneImpl<ParametriInvio>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriInvio>>().To<ParametriInvioBuilder>().InRequestScope();

            // Login
            kernel.Bind<IConfigurazione<ParametriLogin>>().To<ConfigurazioneImpl<ParametriLogin>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriLogin>>().To<ParametriLoginBuilder>().InRequestScope();

            // Menu
            kernel.Bind<IConfigurazione<ParametriMenuV2>>().To<ConfigurazioneImpl<ParametriMenuV2>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriMenuV2>>().To<ParametriMenuBuilder>().InRequestScope();

            // STC
            kernel.Bind<IConfigurazione<ParametriStc>>().To<ConfigurazioneImpl<ParametriStc>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriStc>>().To<ParametriStcBuilder>().InRequestScope();

            // Visura
            kernel.Bind<IConfigurazione<ParametriVisura>>().To<ConfigurazioneImpl<ParametriVisura>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriVisura>>().To<ParametriVisuraBuilder>().InRequestScope();

            // Workflow
            kernel.Bind<IConfigurazione<ParametriWorkflow>>().To<ConfigurazioneImpl<ParametriWorkflow>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriWorkflow>>().To<ParametriWorkflowBuilder>().InRequestScope();

            // Scadenzario
            kernel.Bind<IConfigurazione<ParametriScadenzario>>().To<ConfigurazioneImpl<ParametriScadenzario>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriScadenzario>>().To<ParametriScadenzarioBuilder>().InRequestScope();

            // Registrazione
            kernel.Bind<IConfigurazione<ParametriRegistrazione>>().To<ConfigurazioneImpl<ParametriRegistrazione>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriRegistrazione>>().To<ParametriRegistrazioneBuilder>().InRequestScope();

            // SigeproSecurity
            kernel.Bind<IConfigurazione<ParametriSigeproSecurity>>().To<ConfigurazioneImpl<ParametriSigeproSecurity>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriSigeproSecurity>>().To<ParametriSigeproSecurityBuilder>().InRequestScope();

            // Scheda dinamica cittadini extracomunitari
            kernel.Bind<IConfigurazione<ParametriSchedaCittadiniExtracomunitari>>().To<ConfigurazioneImpl<ParametriSchedaCittadiniExtracomunitari>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriSchedaCittadiniExtracomunitari>>().To<ParametriSchedaCittadiniExtracomunitariBuilder>().InRequestScope();

            // CART
            kernel.Bind<IConfigurazione<ParametriCart>>().To<ConfigurazioneImpl<ParametriCart>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriCart>>().To<ParametriCartBuilder>().InRequestScope();

            // Firma digitale
            kernel.Bind<IConfigurazione<ParametriFirmaDigitale>>().To<ConfigurazioneImpl<ParametriFirmaDigitale>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriFirmaDigitale>>().To<ParametriFirmaDigitaleBuilder>().InRequestScope();

            // Ricerche anagrafiche
            kernel.Bind<IConfigurazione<ParametriRicercaAnagrafiche>>().To<ConfigurazioneImpl<ParametriRicercaAnagrafiche>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriRicercaAnagrafiche>>().To<ParametriRicercaAnagraficheBuilder>().InRequestScope();

            // Allegati
            kernel.Bind<IConfigurazione<ParametriAllegati>>().To<ConfigurazioneImpl<ParametriAllegati>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriAllegati>>().To<ParametriAllegatiBuilder>().InRequestScope();


            // Localizzazioni
            kernel.Bind<IConfigurazione<ParametriLocalizzazioni>>().To<ConfigurazioneImpl<ParametriLocalizzazioni>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriLocalizzazioni>>().To<ParametriLocalizzazioniBuilder>().InRequestScope();

            // Integrazione LDP
            kernel.Bind<IConfigurazione<ParametriIntegrazioneLDP>>().To<ConfigurazioneImpl<ParametriIntegrazioneLDP>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriIntegrazioneLDP>>().To<ParametriIntegrazioneLDPBuilder>().InRequestScope();

            // Integrazioni documentali
            kernel.Bind<IConfigurazione<ParametriIntegrazioniDocumentali>>().To<ConfigurazioneImpl<ParametriIntegrazioniDocumentali>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriIntegrazioniDocumentali>>().To<ParametriIntegrazioniDocumentaliBuilder>().InRequestScope();
 
            // COnfigurazione phantomjs
            kernel.Bind<IConfigurazione<ParametriPhantomjs>>().To<ConfigurazioneImpl<ParametriPhantomjs>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriPhantomjs>>().To<ParametriPhantomjsBuilder>().InRequestScope();

            // COnfigurazione Loghi
            kernel.Bind<IConfigurazione<ParametriLoghi>>().To<ConfigurazioneImpl<ParametriLoghi>>().InRequestScope(); ;
            kernel.Bind<IConfigurazioneBuilder<ParametriLoghi>>().To<ParametriLoghiBuilder>().InRequestScope();

            // Configurazione SAC con Drupal Livorno
            kernel.Bind<IConfigurazione<ParametriLivornoConfigurazioneDrupal>>().To<ConfigurazioneImpl<ParametriLivornoConfigurazioneDrupal>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriLivornoConfigurazioneDrupal>>().To<ParametriLivornoConfigurazioneDrupalBuilder>().InRequestScope();

            // Url area riservata
            kernel.Bind<IConfigurazione<ParametriUrlAreaRiservata>>().To<ConfigurazioneImpl<ParametriUrlAreaRiservata>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriUrlAreaRiservata>>().To<ParametriUrlAreaRiservataBuilder>().InRequestScope();

            // Generazione riepilogo domanda
            kernel.Bind<IConfigurazione<ParametriGenerazioneRiepilogoDomanda>>().To<ConfigurazioneImpl<ParametriGenerazioneRiepilogoDomanda>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriGenerazioneRiepilogoDomanda>>().To<ParametriGenerazioneRiepilogoDomandaBuilder>().InRequestScope();

            // AR REdirect
            kernel.Bind<IConfigurazione<ParametriARRedirect>>().To<ConfigurazioneImpl<ParametriARRedirect>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriARRedirect>>().To<ParametriARRedirectBuilder>().InRequestScope();

            // Aida SMART
            // kernel.Bind<IConfigurazione<ParametriAidaSmart>>().To<ConfigurazioneImpl<ParametriAidaSmart>>().InRequestScope();
            // kernel.Bind<IConfigurazioneBuilder<ParametriAidaSmart>>().To<ParametriAidaSmartBuilder>().InRequestScope();

            // FVG SOL
            kernel.Bind<IConfigurazione<ParametriFvgSol>>().To<ConfigurazioneImpl<ParametriFvgSol>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriFvgSol>>().To<ParametriFvgSolBuilder>().InRequestScope();

            // Dimensione allegati liberi
            kernel.Bind<IConfigurazioneBuilder<ParametriDimensioneAllegatiLiberi>>().To<ParametriDimensioneAllegatiLiberiBuilder>().InRequestScope();
            kernel.Bind<IConfigurazione<ParametriDimensioneAllegatiLiberi>>().To<ConfigurazioneImpl<ParametriDimensioneAllegatiLiberi>>().InRequestScope();

            // Scrivania enti terzi
            kernel.Bind<IConfigurazioneBuilder<ParametriScrivaniaEntiTerzi>>().To<ParametriScrivaniaEntiTerziBuilder>().InRequestScope();
            kernel.Bind<IConfigurazione<ParametriScrivaniaEntiTerzi>>().To<ConfigurazioneImpl<ParametriScrivaniaEntiTerzi>>().InRequestScope();

            // Trieste accesso atti
            kernel.Bind<IConfigurazioneBuilder<ParametriTriesteAccessoAtti>>().To<TriesteAccessoAttiBuilder>().InRequestScope();
            kernel.Bind<IConfigurazione<ParametriTriesteAccessoAtti>>().To<ConfigurazioneImpl<ParametriTriesteAccessoAtti>>().InRequestScope();

            // Google maps
            kernel.Bind<IConfigurazioneBuilder<ParametriGoogleMaps>>().To<ParametriGoogleMapsBuilder>().InRequestScope();
            kernel.Bind<IConfigurazione<ParametriGoogleMaps>>().To<ConfigurazioneImpl<ParametriGoogleMaps>>().InRequestScope();

            // Certificato di invio
            //IConfigurazioneBuilder<ParametriGenerazioneCertificatoInvio>
            kernel.Bind<IConfigurazioneBuilder<ParametriGenerazioneCertificatoInvio>>().To<ParametriGenerazioneCertificatoDiInvioBuilder>().InRequestScope();
            kernel.Bind<IConfigurazione<ParametriGenerazioneCertificatoInvio>>().To<ConfigurazioneImpl<ParametriGenerazioneCertificatoInvio>>().InRequestScope();

            //Parametri di presentazione domanda
            kernel.Bind<IConfigurazione<ParametriPresentazioneDomanda>>().To<ConfigurazioneImpl<ParametriPresentazioneDomanda>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriPresentazioneDomanda>>().To<ParametriPresentazioneDomandaBuilder>().InRequestScope();

            kernel.Bind<IConfigurazione<ParametriQuestionarioSoddisfazione>>().To<ConfigurazioneImpl<ParametriQuestionarioSoddisfazione>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriQuestionarioSoddisfazione>>().To<ParametriQuestionarioBuilder>().InRequestScope();

            // SIT
            kernel.Bind<IConfigurazione<ParametriSIT>>().To<ConfigurazioneImpl<ParametriSIT>>().InRequestScope();
            kernel.Bind<IConfigurazioneBuilder<ParametriSIT>>().To<ParametriSITBuilder>().InRequestScope();

            return kernel;
        }
    }
}
