using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda;
using Init.Sigepro.FrontEnd.AppLogic.GestioneQuestionario;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved
{
    public partial class DettaglioIstanzaEx : ReservedBasePage
    {
        [Inject]
        protected RiepilogoDomandaDaVisuraService _riepilogoDomandaService { get; set; }
        [Inject]
        protected IVisuraService _visuraService { get; set; }
        [Inject]
        protected IConfigurazione<ParametriUrlAreaRiservata> _parametriUrl { get; set; }
        [Inject]
        protected IConfigurazione<ParametriVisura> _parametriVisura { get; set; }
        [Inject]
        protected AllegatiInterventoService _allegatiInterventoService { get; set; }
        [Inject]
        protected DownloadDomandaZIPService _downloadDomandaZIPService { get; set; }

        [Inject]
        protected IQuestionarioSoddisfazioneService _questionarioService { get; set; }

        protected QsUuidIstanza IdIstanza
        {
            get { return new QsUuidIstanza(Request.QueryString); }
        }

        protected QsReturnTo ReturnTo
        {
            get
            {
                return new QsReturnTo(Request.QueryString);

                //if (String.IsNullOrEmpty(str))
                //	return "~/Reserved/IstanzePresentate.aspx";
                //
                //return str;
            }
        }

        protected string ReturnToArgs
        {
            get
            {
                return Request.QueryString["ReturnToArgs"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.VisuraExCtrl1.ScadenzaSelezionata += new VisuraExCtrl.ScadenzaSelezionataDelegate(visuraCtrl_ScadenzaSelezionata);

            if (!IsPostBack)
            {
                var istanza = this._visuraService.GetByUuid(this.IdIstanza.Value, true);

                var livelloAccesso = VerificaAccesso(istanza);

                VisuraExCtrl1.LivelloAccesso = livelloAccesso;
                VisuraExCtrl1.EffettuaVisuraIstanza(istanza);

                this.cmdGeneraRiepilogo.Visible = false;
                this.cmdScaricaZIP.Visible = false;

                if (livelloAccesso == LivelloAccessoVisura.Completo)
                {
                    var codiceriepilogo = this._allegatiInterventoService.GetCodiceOggettoDelModelloDiRiepilogo(Convert.ToInt32(istanza.CODICEINTERVENTOPROC));
                    this.cmdGeneraRiepilogo.Visible = codiceriepilogo.HasValue;

                    if (this._parametriVisura.Parametri.NascondiRigeneraRiepilogo)
                    {
                        this.cmdGeneraRiepilogo.Visible = false;
                    }

                    this.cmdScaricaZIP.Visible = this._downloadDomandaZIPService.ServizioConfigurato();
                }

                this.cmdAccedi.Visible = TokenAnonimo();
                this.cmdClose.Visible = !TokenAnonimo();


                this.cmdQuestionario.Visible = false;
                // this.cmdQuestionario.Visible = livelloAccesso == LivelloAccessoVisura.Completo &&
                //                                this._questionarioService.CompilazioneQuestionarioAttiva &&
                //                                !this._questionarioService.QuestionarioCompilato(this.IdIstanza.Value);
            }
        }

        private LivelloAccessoVisura VerificaAccesso(Istanze istanza)
        {
            if (TokenAnonimo())
            {
                return LivelloAccessoVisura.AccessoAnonimo;
            }

            return istanza.GetLivelloAccesso(this.UserAuthenticationResult.DatiUtente.Codicefiscale);
        }

        private bool TokenAnonimo()
        {
            return this.UserAuthenticationResult.LivelloAutenticazione == LivelloAutenticazioneEnum.Anonimo;
        }

        void visuraCtrl_ScadenzaSelezionata(object sender, string idScadenza)
        {
            Redirect("~/Reserved/Gestionemovimenti/EffettuaMovimento.aspx", qs => qs.Add("IdMovimento", idScadenza));
        }


        protected void cmdClose_Click(object sender, EventArgs e)
        {
            var url = UrlBuilder.Url("~/Reserved/IstanzePresentate.aspx", qs =>
            {
                qs.Add(new QsAliasComune(this.IdComune));
                qs.Add(new QsSoftware(this.Software));
            }); 

            if (this.ReturnTo.HasValue)
            {
                url = this.ReturnTo.Value;
            }

            Response.Redirect(url);
        }

        protected void cmdGeneraRiepilogo_Click(object sender, EventArgs e)
        {
            var riepilogo = this._riepilogoDomandaService.GeneraRiepilogoDomanda(this.IdIstanza.Value);

            Response.Clear();
            Response.ContentType = riepilogo.MimeType;
            Response.AddHeader("content-disposition", $"attachment;filename=\"{riepilogo.FileName}\"");
            Response.BinaryWrite(riepilogo.FileContent);
            Response.End();
        }

        protected void cmdAccedi_Click(object sender, EventArgs e)
        {
            var url = UrlBuilder.Url(_parametriUrl.Parametri.VisuraAutenticata, qs =>
            {
                qs.Add(new QsAliasComune(this.IdComune));
                qs.Add(new QsSoftware(this.Software));
                qs.Add(IdIstanza);
            });

            Response.Redirect(url);
        }

        protected void cmdScaricaZIP_Click(object sender, EventArgs e)
        {

            var zipFile = this._downloadDomandaZIPService.RecuperaZIPDaUUIDDomanda(this.IdIstanza.Value);

            Response.Clear();
            Response.ContentType = zipFile.MimeType;
            Response.AddHeader("content-disposition", $"attachment;filename=\"{zipFile.FileName}\"");
            Response.BinaryWrite(zipFile.FileContent);
            Response.End();
        }

        protected void cmdQuestionario_Click(object sender, EventArgs e)
        {
            var url = UrlBuilder.Url("~/reserved/questionario/compila.aspx", qs => {
                qs.Add(new QsAliasComune(IdComune));
                qs.Add(new QsSoftware(Software));
                qs.Add(IdIstanza);
            });

            Response.Redirect(url);
        }
    }
}
