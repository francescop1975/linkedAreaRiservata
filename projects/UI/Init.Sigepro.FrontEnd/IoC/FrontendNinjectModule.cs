using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.CondizioniIngressoSteps;
using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.CondizioniUscitaSteps;
using Ninject.Modules;

namespace Init.Sigepro.FrontEnd.IoC
{
    public class FrontendNinjectModule : NinjectModule
    {
        public override void Load()
        {
            // Condizioni di ingresso
            Bind<CondizioneIngressoGestioneSottoscrittori>().ToSelf();
            Bind<CondizioneIngressoStepSempreVera>().ToSelf();

            // Condizioni di uscita
            Bind<CondizioneUscitaStepSempreVera>().ToSelf();
            Bind<CondizioneUscitaGestioneAnagraficheBase>().ToSelf();
            Bind<CondizioniUscitaGestioneAnagrafiche>().ToSelf();
            Bind<CondizioniUscitaGestioneAnagraficheSemplificata>().ToSelf();
            Bind<CondizioneUscitaPrivacyAccettata>().ToSelf();
        }
    }
}