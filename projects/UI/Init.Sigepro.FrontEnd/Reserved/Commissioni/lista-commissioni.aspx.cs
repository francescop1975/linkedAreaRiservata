using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni;
using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.AccessoPIN;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Init.Sigepro.FrontEnd.QsParameters.Commissioni;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved.Commissioni
{
    public partial class lista_commissioni : CommissioniBasePage
    {
        [Inject]
        protected ICommissioniService _commissioniService { get; set; }

        [Inject]
        protected IAccessoPINService _accessoPINService { get; set; }

        private readonly ILog _log = LogManager.GetLogger(typeof(lista_commissioni));

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            var listaCommissioni = this._commissioniService.GetCommissioniApertePerUtenteCorrente();

            gvCommissioni.DataSource = listaCommissioni;
            gvCommissioni.DataBind();
        }

        protected void gvCommissioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = gvCommissioni.DataKeys[gvCommissioni.SelectedIndex].Value.ToString();

            RedirectUtenteACommissione(Convert.ToInt32(id));
        }

        private void RedirectUtenteACommissione(int idCommissione)
        {
            var url = UrlBuilder.Url("~/reserved/commissioni/dettaglio-commissione.aspx", mp =>
            {
                mp.Add(new QsAliasComune(this.IdComune));
                mp.Add(new QsSoftware(this.Software));
                mp.Add(new QsIdCommissione(idCommissione.ToString()));
            });

            Response.Redirect(url);
        }

        protected void cmdChiudi_Click(object sender, EventArgs e)
        {
            var url = UrlBuilder.Url("~/reserved/default.aspx", mp =>
            {
                mp.Add(new QsAliasComune(this.IdComune));
                mp.Add(new QsSoftware(this.Software));
            });

            Response.Redirect(url);
        }

        protected void bmAccediConPin_OkClicked(object sender, EventArgs e)
        {
            var pin = this.txtPin.Text;

            if (String.IsNullOrEmpty(pin) || !this._accessoPINService.VerificaValiditaPIN(pin))
            {
                this._log.Error($"L'utente {this._authenticationDataResolver.DatiAutenticazione.DatiUtente.ToString()} " +
                                $"(codice anagrafe {this._authenticationDataResolver.DatiAutenticazione.DatiUtente.Codiceanagrafe}) " +
                                $"ha cercato di accedere ad una commissione con un PIN non valido: {pin}");
                this.Errori.Add("Il pin immesso non è valido o un altro utente è già stato associato alla commissione");
                this.txtPin.Text = "";

                return;
            }

            try
            {
                var idCommissione = this._accessoPINService.AssociaUtenteCorrenteACommissioneByPIN(pin.ToUpper());

                RedirectUtenteACommissione(idCommissione);
            }
            catch(Exception ex)
            {
                var id = Guid.NewGuid().ToString();
                this._log.Error($"Errore durante l'associazione di un utente ad una commissione tramite pin. Riferimento errore => {id}: {ex}");
                var msg = "Si è verificato un problema durante l'accesso tramite pin.<br/> " +
                          "Riprovare tra qualche minuto, se il problema persiste contattare l'assistenza " +
                         $"fornendo il seguente riferimento errore:<br/>" +
                         $"<b>{id}</b>";

                this.Errori.Add(msg);
            }
        }
    }
}