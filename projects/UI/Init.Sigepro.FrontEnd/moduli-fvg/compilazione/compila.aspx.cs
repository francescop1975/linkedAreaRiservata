using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Init.Sigepro.FrontEnd.QsParameters.Fvg;
using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.WebControls.MaschereSolaLettura;
using Ninject;
using System;
using System.Web;
using System.Web.UI;

namespace Init.Sigepro.FrontEnd.moduli_fvg.compilazione
{
    public partial class compila : BasePage
    {
        private static class Constants
        {
            public const string UrlPaginaLista = "~/moduli-fvg/compilazione/lista.aspx";
        }


        [Inject]
        public IModelliDinamiciService _modelliService { get; set; }
        [Inject]
        public ServiziFVGService _fvgService { get; set; }


        protected QsFvgCodiceIstanza CodiceIstanza { get => new QsFvgCodiceIstanza(Request.QueryString); }
        protected QsFvgIdModulo IdModulo { get => new QsFvgIdModulo(Request.QueryString); }
        protected QsFvgIdScheda IdScheda { get => new QsFvgIdScheda(Request.QueryString); }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.MostraTitoloPagina = false;

            HttpContext.Current.Items["UserAuthenticationResult"] = UserAuthenticationResult.CreateFake(IdComune);

            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            CaricaSchedaDinamica(this.IdScheda.Value);
        }

        private void CaricaSchedaDinamica(int idScheda)
        {
            var indiceScheda = 0; // per ora FVG non supporta la molteplicità a livello di scheda
            var scheda = this._fvgService.GetModelloDinamico(CodiceIstanza.Value, IdModulo.Value, idScheda, indiceScheda);

            this.Page.Title = scheda.NomeModello;


            scheda.EseguiScriptCaricamento();

            renderer.ImpostaMascheraSolaLettura(new MascheraSolaLetturaVuota());
            //renderer.RicaricaModelloDinamico += new SIGePro.DatiDinamici.WebControls.ModelloDinamicoRenderer.RicaricaModelloDinamicoDelegate(renderer_RicaricaModelloDinamico);
            renderer.DataSource = scheda;
            renderer.DataBind();

            /*
            paginatoreSchedeDinamiche.Visible = scheda.ModelloMultiplo;
            paginatoreSchedeDinamiche.IndiciSchede = ModelliDinamiciService.GetIndiciScheda(idDomanda, idScheda);
            paginatoreSchedeDinamiche.IndiceCorrente = indiceScheda;
            paginatoreSchedeDinamiche.DataBind();
            */
            //cmdSalvaEResta.Visible = scheda.ModelloMultiplo;
        }

        protected void cmdSalvaEContinua_Click(object sender, EventArgs e)
        {
            if (SalvaSchedaCorrente())
            {
                VaiAllaSchedaSuccessiva();
            }
        }

        private bool SalvaSchedaCorrente()
        {
            try
            {
                if (renderer.DataSource == null)
                {
                    this.Errori.Add("Si è verificato un errore interno. Ricaricare la scheda corrente e riprovare.");

                    return false;
                }

                try
                {
                    renderer.DataSource.ValidaModello();
                }
                catch (ValidazioneModelloDinamicoException /*ex*/)
                {
                    MostraErroreSalvataggio();
                    return false;
                }

                // renderer.DataSource.Salva();
                // var campiNOnVisibili = renderer.DataSource.GetStatoVisibilitaCampi(false);

                this._fvgService.SalvaScheda(renderer.DataSource);

                return true;
            }
            catch (SalvataggioModelloDinamicoException)
            {
                MostraErroreSalvataggio();
            }
            catch (Exception ex)
            {
                this.Errori.Add(ex.Message);
            }

            return false;
        }

        protected void cmdSalva_Click(object sender, EventArgs e)
        {
            if (SalvaSchedaCorrente())
            {
                TornaAllaLista();
            }
        }

        private void MostraErroreSalvataggio()
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "notifica", "alert('Si sono verificati errori durante il salvataggio');", true);
        }

        protected void lnkChiudi_Click(object sender, EventArgs e)
        {
            TornaAllaLista();
        }

        private void VaiAllaSchedaSuccessiva()
        {
            var url = UrlBuilder.Url(Constants.UrlPaginaLista, x =>
            {
                x.Add(new QsAliasComune(this.IdComune));
                x.Add(new QsSoftware(this.Software));
                x.Add(this.CodiceIstanza);
                x.Add(this.IdModulo);
                x.Add(new QsFvgPassaASuccessiva(IdScheda.Value));
            });

            Response.Redirect(url);
        }

        private void TornaAllaLista()
        {
            var url = UrlBuilder.Url(Constants.UrlPaginaLista, x =>
            {
                x.Add(new QsAliasComune(this.IdComune));
                x.Add(new QsSoftware(this.Software));
                x.Add(this.CodiceIstanza);
                x.Add(this.IdModulo);
            });

            Response.Redirect(url);
        }
    }
}