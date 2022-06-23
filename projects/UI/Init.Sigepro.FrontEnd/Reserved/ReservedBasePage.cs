using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved
{

    public class ReservedBasePage : BasePage
    {
        [Inject]
        protected IAuthenticationDataResolver _authenticationDataResolver { get; set; }
        [Inject]
        protected IUrlDownloadOggettiService _urlService { get; set; }

        public UserAuthenticationResult UserAuthenticationResult
        {
            get { return _authenticationDataResolver.DatiAutenticazione; }
        }

        protected string CodiceUtente
        {
            get { return UserAuthenticationResult.DatiUtente.Codicefiscale; }
        }


        public ReservedBasePage()
        {

        }

        public virtual void SalvataggioRiuscito()
        {
            this.MessaggiSuccesso.Add("Dati salvati con successo");
        }


        #region gestione della navigazione

        public string ResolvePlaceholders(string url)
        {
            if (url.IndexOf("{software}") >= 0)
            {
                url = url.Replace("{software}", Software);
            }

            if (url.IndexOf("{idcomune}") >= 0)
            {
                url = url.Replace("{idcomune}", IdComune);
            }

            return url;
        }

        protected void Redirect(string url, Action<QuerystringArgumentsList> parametersBuilder)
        {
            var ub = new UrlBuilder();

            ub.AddDefaultParameter("Software", Software);
            ub.AddDefaultParameter("IdComune", IdComune);

            var newUrl = ub.Build(url, parametersBuilder);

            Response.Redirect(newUrl);
        }

        #endregion


        public string UrlDownload(object codiceOggetto)
        {
            if (codiceOggetto == null)
            {
                return null;
            }
            return this._urlService.GetUrlDownload(Convert.ToInt32(codiceOggetto));
        }
    }
}
