using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneQuestionario.IOC
{
    public static class QuestionarioFoNinjectModule
    {
        public static IKernel RegistraQuestionarioSoddisfazione(this IKernel k)
        {
            k.Bind<IQuestionarioSoddisfazioneDao>().To<QuestionarioWebServiceDao>().InRequestScope();
            k.Bind<IQuestionarioSoddisfazioneService>().To<QuestionarioSoddisfazioneService>().InRequestScope();

            return k;
        }
    }
}
