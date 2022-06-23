using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Init.Utils;
//using Init.Sigepro.FrontEnd.AppLogic.Visura.Collections;
using log4net;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved
{
    public partial class IstanzePresentate : ReservedBasePage
    {
        [Inject]
        public IVisuraService _visuraService { get; set; }

        bool RestoreResults
        {
            get
            {
                return Request.QueryString["restore"] == "1";
            }
        }


        ILog m_logger = LogManager.GetLogger(typeof(IstanzePresentate));


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FiltriVisura.IdComune = IdComune;
                FiltriVisura.Software = Software;
                dglistaPratiche.IdComune = IdComune;
                dglistaPratiche.Software = Software;

                if (RestoreResults)
                {
                    RebindFromCache();
                }
            }
        }

        private void RebindFromCache()
        {
            multiView.ActiveViewIndex = 1;

            dglistaPratiche.RebindFromCache();
        }

        protected void cmdSearch_Click(object sender, EventArgs e)
        {
            var richiesta = FiltriVisura.GetRichiestaListaPratiche(base.UserAuthenticationResult.DatiUtente);

            try
            {
                multiView.ActiveViewIndex = 1;

                dglistaPratiche.PageIndex = 0;
                dglistaPratiche.DataSource = _visuraService.GetListaPratiche(richiesta);
                dglistaPratiche.DataBind();
            }
            catch (Exception ex)
            {
                m_logger.ErrorFormat("Errore durante la ricerca delle istanze presentate: {0} \r\n\r\n Richiesta: {1}", ex.ToString(), StreamUtils.SerializeClass(richiesta));

                multiView.ActiveViewIndex = 0;

                Errori.Add("La ricerca ha restituito un numero troppo elevato di risultati. Utilizzare uno o più filtri per ridurre il numero di risultati restituiti.");
            }
        }

        public string GetNavigateUrlFormatString()
        {
            var returnTo = UrlBuilder.Url("~/Reserved/IstanzePresentate.aspx", qs =>
            {
                qs.Add(new QsAliasComune(this.IdComune));
                qs.Add(new QsSoftware(this.Software));
                qs.Add("restore", "1");
            });

            var url = UrlBuilder.Url("~/Reserved/DettaglioIstanzaEx.aspx", qs =>
            {
                qs.Add(new QsAliasComune(this.IdComune));
                qs.Add(new QsSoftware(this.Software));
                qs.Add(new QsReturnTo(returnTo));
            });
            return url + "&uuid-pratica={0}";
        }

        protected void cmdNewSearch_Click(object sender, EventArgs e)
        {
            multiView.ActiveViewIndex = 0;
        }
    }
}
