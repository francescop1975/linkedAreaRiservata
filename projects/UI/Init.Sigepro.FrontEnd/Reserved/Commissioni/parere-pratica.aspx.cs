using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.Votazioni;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.PostedFileSpecifications;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Init.Sigepro.FrontEnd.QsParameters.Commissioni;
using Init.SIGePro.Manager.DTO.Commissioni.Votazioni;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved.Commissioni
{
    public partial class parere_pratica : CommissioniBasePage
    {
        public QsIdCommissione IdCommissione => new QsIdCommissione(Request.QueryString);
        public QsUuidIstanza UuidIstanza => new QsUuidIstanza(Request.QueryString);

        [Inject]
        public IVotazioniCommissioniService _votazioniCommissioniService { get; set; }

        [Inject]
        public IOggettiService _oggettiService { get; set; }

        [Inject]
        public IPostedFileSpecificationFactory _postedFileSpecificationFactory { get; set; }


        private bool RichiedeFirmaDigitale
        {
            get => Boolean.Parse(this.ViewState["RichiedeFirmaDigitale"]?.ToString() ?? "false");
            set => this.ViewState["RichiedeFirmaDigitale"] = value;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this._votazioniCommissioniService.UtenteLoggatoPuoEsprimereVoto(this.IdCommissione.Value, this.UuidIstanza.Value))
            {
                this.Errori.Add("Non è possibile esprimere un voto per questa pratica");
                this.cmdSalvaVoto.Visible = false;
            }

            if (!IsPostBack)
            {
                this.ddlParere.Inner.DataTextField = "Text";
                this.ddlParere.Inner.DataValueField = "Value";

                this.ddlParere.DataSource = this._votazioniCommissioniService
                                                .GetListaPareri()
                                                .Select(x => new { 
                                                    Text = x.Descrizione,
                                                    Value = x.Id.ToString()
                                                });
                this.ddlParere.DataBind();
                this.ddlParere.Inner.Items.Insert(0, new ListItem("", ""));

                DataBind();
            }
        }

        public override void DataBind()
        {
            // resettare i campi
            // ...
            this.ddlParere.SelectedValue = "";
            this.txtNote.Text = "";
            this.fileParere.ResetValues();

            var voto =  this._votazioniCommissioniService.GetVotoUtenteLoggato(this.IdCommissione.Value, this.UuidIstanza.Value);

            this.RichiedeFirmaDigitale = voto.RichiedeFirmaDigitale;

            if (voto.Voto != null)
            {
                // todo: bindare i campi
                this.ddlParere.Value = voto.Voto.CodiceParere.ToString();
                this.txtNote.Value = voto.Voto.Note;
                this.fileParere.Required = this.RichiedeFirmaDigitale;

                if (voto.Voto.CodiceOggetto.HasValue)
                {
                    this.fileParere.SetOggetto(voto.Voto.CodiceOggetto.Value, voto.Voto.NomeFile);
                }

                this.cmdSalvaVoto.Visible = false;
                this.MessaggiInformativi.Add("Hai già espresso un parere per questa pratica. Il parere può essere espresso una sola volta, contatta l'ufficio di competenza per poter modificare il parere.");
            }

            this.fileParere.DataBind();
        }

        protected void cmdSalvaVoto_Click(object sender, EventArgs e)
        {
            try
            {
                var codiceParere = ddlParere.Value;
                var note = txtNote.Value;
                var codiceOggetto = this.fileParere.CodiceOggetto;

                if (String.IsNullOrEmpty(codiceParere))
                {
                    this.Errori.Add("Selezionare un parere dalla lista"); //TODO: spostare sul controllo base
                    return;
                }

                if (this.fileParere.FileModificato)
                {
                    var esitoValidazione = this.fileParere.ValidaFile(_postedFileSpecificationFactory.Get(new FileValidationFlags
                    {
                        FirmatoDigitalmente = this.RichiedeFirmaDigitale,
                        Obbligatorio = this.RichiedeFirmaDigitale
                    }));

                    if (!esitoValidazione) // Il messaggio di validazione viene gestito direttamente nel controllo
                    {
                        return;
                    }

                    var file = this.fileParere.GetFile();
                    codiceOggetto = _oggettiService.InserisciOggetto(file);
                }

                if (this.RichiedeFirmaDigitale && codiceOggetto == null)
                {
                    this.Errori.Add("Per esprimere un voto è necessario caricare un file contenente le motivazioni del parere"); //TODO: spostare sul controllo base
                    return;
                }

                var voto = new VotoPraticaCommissioneDto
                {
                    CodiceParere = Convert.ToInt32(codiceParere),
                    DescrizioneParere = ddlParere.SelectedItem.Text,
                    CodiceOggetto = codiceOggetto,
                    Note = note
                };

                this._votazioniCommissioniService.EsprimiVotoPerUtenteLoggato(this.IdCommissione.Value, this.UuidIstanza.Value, voto);

                SalvataggioRiuscito();
            }
            catch(Exception ex)
            {
                this.Errori.Add(ex.Message);    
            }
        }

        public override void SalvataggioRiuscito()
        {
            this.multiView.ActiveViewIndex = 1; // parere salvato con successo
        }

        protected void cmdChiudi_Click(object sender, EventArgs e)
        {
            var url = UrlBuilder.Url("~/reserved/commissioni/dettaglio-pratica.aspx", mp =>
            {
                mp.Add(new QsAliasComune(IdComune));
                mp.Add(new QsSoftware(Software));
                mp.Add(this.IdCommissione);
                mp.Add(this.UuidIstanza);
            });

            Response.Redirect(url);
        }
    }
}