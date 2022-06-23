using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.AreaRiservata;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GestioneMenu;
using Init.Sigepro.FrontEnd.AppLogic.GestioneRisorseTestuali;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using Init.Sigepro.FrontEnd.AppLogic.Services.Navigation;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Init.Sigepro.FrontEnd.Reserved;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
//using Init.Sigepro.FrontEnd.AppLogic.Validation;

namespace Init.Sigepro.FrontEnd
{
    public partial class AreaRiservataMaster : BaseAreaRiservataMaster
    {
        [Inject]
        public IConfigurazioneVbgRepository _configurazioneVbgRepository { get; set; }
        [Inject]
        public IConfigurazione<ParametriAspetto> _configurazioneAspetto { get; set; }
        [Inject]
        public MenuService _menuService { get; set; }
        [Inject]
        public IsUtenteAnonimoSpecification IsUtenteAnonimo { get; set; }
        [Inject]
        public IConfigurazione<ParametriUrlAreaRiservata> _urlAreaRiservata { get; set; }
        [Inject]
        public IRisorseTestualiService _risorseService { get; set; }
        [Inject]
        public RedirectService _navigationService { get; set; }
        [Inject]
        public ICheckBrowserService _checkBrowserService { get; set; }

        [Inject]
        protected IAreaRiservataAuthenticationService _areaRiservataAuthenticationService { get; set; }

        public bool MostraIntestazione
        {
            get { object o = this.ViewState["MostraIntestazione"]; return o == null ? true : (bool)o; }
            set { this.ViewState["MostraIntestazione"] = value; }
        }

        public bool MostraFooter
        {
            get { object o = this.ViewState["MostraFooter"]; return o == null ? true : (bool)o; }
            set { this.ViewState["MostraFooter"] = value; }
        }

        public bool NascondiTitoloPagina
        {
            get { object o = this.ViewState["NascondiTitoloPagina"]; return o == null ? false : (bool)o; }
            set { this.ViewState["NascondiTitoloPagina"] = value; }
        }

        public bool ResetValidatorsOnLoad
        {
            get { object o = this.ViewState["ResetValidatorsOnLoad"]; return o == null ? true : (bool)o; }
            set { this.ViewState["ResetValidatorsOnLoad"] = value; }
        }

        public bool UtenteAnonimo
        {
            get
            {
                if (this._authenticationDataResolver.IsAuthenticated)
                {
                    return IsUtenteAnonimo.IsSatisfiedBy(UserAuthenticationResult);
                }

                return false;
            }
        }

        protected string AnalyticsId
        {
            get { return ConfigurationManager.AppSettings["AnalyticsId"]; }
        }

        protected bool UtenteTester
        {
            get
            {
                if (this._authenticationDataResolver.IsAuthenticated)
                {
                    return this.UserAuthenticationResult.DatiUtente.UtenteTester;
                }

                return false;
            }
        }

        public string SottotitoloPagina
        {
            get { var obj = this.ViewState["SottotitoloPagina"]; return obj == null ? "" : obj.ToString(); }
            set { this.ViewState["SottotitoloPagina"] = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OutputErrori = rptErrori;
            base.OutputMessaggiInformativi = rptMessaggi;
            base.OutputMessaggiSuccesso = rptMessaggiSuccesso;

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckBrowserVersion();

            InizializzaMenu();

            if (!IsPostBack)
            {
                InizializzaDatiUtente();

                this.SottotitoloPagina = _configurazioneVbgRepository.LeggiConfigurazioneComune(Software).DENOMINAZIONE;

            }
        }

        private void CheckBrowserVersion()
        {
            var userAgent = Request.Headers["User-Agent"];
            if (this._checkBrowserService.IsInternetExplorer(userAgent))
            {
                this._navigationService.RedirectToAvvisoInternetExplorer();
            }
        }

        private void InizializzaDatiUtente()
        {
            if (this._authenticationDataResolver.IsAuthenticated)
            {
                lblNomeUtente2.Text = UserAuthenticationResult.DatiUtente.Nominativo + " " + UserAuthenticationResult.DatiUtente.Nome;
            }
        }

        private void InizializzaMenu()
        {
            if (!(this.Page is ReservedBasePage))
            {
                rptMenu.Visible = false;
                return;
            }
            var menu = this._menuService.LoadMenu();

            rptMenu.DataSource = menu.Sezioni;
            rptMenu.DataBind();

            rptMenuUtente.DataSource = menu.MenuUtente;
            rptMenuUtente.DataBind();

            rptMenuDestra.DataSource = menu.MenuDestra;
            rptMenuDestra.DataBind();

            rptMenuDestra.Visible = menu.MenuDestra.Any();
        }

        protected void rptMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var url = UrlBuilder.Url(e.CommandArgument.ToString(), x =>
            {
                x.Add(new QsAliasComune(IdComune));
                x.Add(new QsSoftware(Software));
            });

            Response.Redirect(url);
        }

        protected override void OnPreRender(EventArgs e)
        {
            lblTitoloPagina.Text = this.Page.Title;

            base.OnPreRender(e);
        }

        protected void lnkTornaAllaHome_Click(object sender, EventArgs e)
        {
            _navigationService.RedirectToHomeAreaRiservata();
        }

        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rptSubMenu = (Repeater)e.Item.FindControl("rptSubMenu");
                var dataItem = (SezioneMenu)e.Item.DataItem;

                rptSubMenu.DataSource = dataItem.Items;
                rptSubMenu.DataBind();
            }
        }

        protected void bmModificaRisorse_OkClicked(object sender, EventArgs e)
        {
            try
            {
                var testoRisorsa = txtNuovoTesto.Text.Replace("&lt;", "<").Replace("&gt;", ">");
                this._risorseService.AggiornaRisorsa(txtIdRisorsa.Text, testoRisorsa);
            }
            catch (Exception ex)
            {
                this.MostraMessaggi(this.rptErrori, new[] { ex.Message });
                throw;
            }
        }

        protected void lnkAccedi_Click(object sender, EventArgs e)
        {
            this._areaRiservataAuthenticationService.SignOut();
            Response.Redirect(Request.Url.ToString().Replace(UserToken, String.Empty));
        }


    }
}
