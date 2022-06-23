using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
using Init.Sigepro.FrontEnd.AppLogic.VerificaFirmaDigitale;
using log4net;
using Ninject;
using System;
using System.Net;
using System.Threading;
using System.Web;

namespace Init.Sigepro.FrontEnd.oggetti
{
    public partial class download_firmato : Ninject.Web.PageBase
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(download_firmato));

        [Inject]
        public IFirmaDigitaleMetadataService _firmaDigitaleService { get; set; }

        [Inject]
        public IUrlDownloadOggettiService _urlDownloadService { get; set; }

        [Inject]
        public IOggettiService _oggettiService { get; set; }

        public string Referrer
        {
            get { object o = this.Session["firmadigitale-referrer"]; return o == null ? String.Empty : (string)o; }
            set { this.Session["firmadigitale-referrer"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    Referrer = Request.UrlReferrer.ToString();
                }
                DataBind();
            }
        }

        private BinaryFile GetOggettoDaQuerystring()
        {
            var id = Request.QueryString["id"];

            if (String.IsNullOrEmpty(id))
            {
                _log.Error("Richiesta all'handler di download con id nullo");

                Response.StatusCode = (int)HttpStatusCode.NotFound;
                Response.ContentType = "text/plain";
                Response.Write("id non valido");
                Response.End();
            }

            var codiceOggetto = this._urlDownloadService.GetCodiceOggettoDaEncryptedString(id);

            // Imposto l'alias dell'utente loggato nel contesto http
            ContextAliasSoftwareSetter.Set(HttpContext.Current, codiceOggetto.Alias);

            return _oggettiService.GetById(codiceOggetto.Id);
        }

        public override void DataBind()
        {
            try
            {
                var oggetto = GetOggettoDaQuerystring();

                var datiCert = _firmaDigitaleService.EstraiDatiFirma(oggetto);

                this.rptEsitiVerificaFirma.DataSource = datiCert.DettagliFirme;
                this.rptEsitiVerificaFirma.DataBind();
            }
            catch (FileChecksumValidationException ex)
            {
                _log.Error($"Invocato handler di download con un id non valido: {Request.QueryString["id"]}, ex={ex}");

                Response.StatusCode = (int)HttpStatusCode.NotFound;
                Response.ContentType = "text/plain";
                Response.Write("id non valido");
                Response.End();
            }
            catch (ThreadAbortException)
            {
                // Ho usato una response.end;
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString();

                this._log.Error($"Si è verificato un errore durante il recupero del file per l'id {Request.QueryString["id"]}. Riferimento errore: {errorId}, Errore: {ex}");

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Response.ContentType = "text/plain";
                Response.Write($"Si è verificato un errore durante il recupero del file. Riferimento dell'errore: {errorId}");
                Response.End();
            }
        }



        // Scarica il file firmato
        protected void lnkDownloadFile_Click(object sender, EventArgs e)
        {
            var oggetto = GetOggettoDaQuerystring();

            oggetto.WriteTo(Response);

            Response.End();
        }

        // Scarica il file non firmato
        protected void lnkDownloadFileNoFirma_Click(object sender, EventArgs e)
        {
            var oggetto = GetOggettoDaQuerystring();
            var fileInchiaro = _firmaDigitaleService.GetFileInChiaro(oggetto);

            try
            {
                fileInchiaro.WriteTo(Response);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            Response.End();

        }

        protected void cmdClose_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Referrer))
            {
                Response.Redirect(Referrer);
                this.Referrer = String.Empty;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "startup", "self.close();", true);
            }
        }
    }
}