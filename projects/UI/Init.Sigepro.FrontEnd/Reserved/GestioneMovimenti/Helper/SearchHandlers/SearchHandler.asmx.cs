﻿using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Ricerche;
using Init.SIGePro.Manager.DTO.DatiDinamici;
using Ninject;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;

namespace Init.Sigepro.FrontEnd.Reserved.GestioneMovimenti.Helper.SearchHandlers
{
    /// <summary>
    /// Summary description for SearchHandler
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SearchHandler : Ninject.Web.WebServiceBase
    {

        [Inject]
        public IRicercheDatiDinamiciService _ricercheService { get; set; }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object initializeControl(int idCampo, string valore)
        {
            var val = _ricercheService.InitializeControl(idCampo, valore);

            return new { value = val.Value, label = val.Label };
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object getCompletionList(int idCampo, string partial, ValoreFiltroRicercaDto[] filtri)
        {
            return this._ricercheService
                        .GetCompletionList(idCampo, partial, filtri)
                        .Select(x => new { value = x.Value, label = x.Label });
        }

    }
}
