using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestioneInterventi;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.AmbitoRicercaIntervento;
using Init.Sigepro.FrontEnd.AppLogic.WsInterventi;
using Init.Sigepro.FrontEnd.Infrastructure.IOC;
using Init.SIGePro.Manager.DTO.Interventi;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Init.Sigepro.FrontEnd.Public.WebServices
{
    /// <summary>
    /// Summary description for InterventiJsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class InterventiJsService : System.Web.Services.WebService
    {
        [Inject]
        public IInterventiRepository _alberoProcRepository { get; set; }

        public InterventiJsService()
        {
            FoKernelContainer.Inject(this);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod()]
        public InterventoDto[] GetNodiFiglio(string aliasComune, string software, int idPadre, int idAteco, bool areaRiservata, bool utenteTester)
        {
            ContextAliasSoftwareSetter.Set(HttpContext.Current, aliasComune, software);

            IAmbitoRicercaIntervento ambitoRicerca = areaRiservata ? (IAmbitoRicercaIntervento)new AmbitoRicercaAreaRiservata(utenteTester) :
                                                                     (IAmbitoRicercaIntervento)new AmbitoRicercaFrontofficePubblico();

            if (idAteco != -1)
                return _alberoProcRepository.GetSottonodiDaIdAteco(aliasComune, software, idPadre, idAteco, ambitoRicerca).ToArray();

            var tmp = _alberoProcRepository.GetSottonodi(aliasComune, software, idPadre, ambitoRicerca);

            return tmp.ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod()]
        public InterventoBreveDto[] RicercaTestuale(string aliasComune, string software, string matchParziale, int matchCount, string modoRicerca, string tipoRicerca, bool areaRiservata, bool utenteTester)
        {
            ContextAliasSoftwareSetter.Set(HttpContext.Current, aliasComune, software);

            IAmbitoRicercaIntervento ambitoRicerca = areaRiservata ? (IAmbitoRicercaIntervento)new AmbitoRicercaAreaRiservata(utenteTester) :
                                                                     (IAmbitoRicercaIntervento)new AmbitoRicercaFrontofficePubblico();


            if (matchParziale.Length < 2)
                return new InterventoBreveDto[0];

            return _alberoProcRepository.RicercaTestuale(aliasComune, software, matchParziale, matchCount, modoRicerca, tipoRicerca, ambitoRicerca).ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public List<int> CaricaGerarchia(string aliasComune, int id)
        {
            ContextAliasSoftwareSetter.Set(HttpContext.Current, aliasComune, "");

            return _alberoProcRepository.GetIdNodiPadre(aliasComune, id);
        }
    }
}
