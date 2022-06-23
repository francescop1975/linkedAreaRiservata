using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni;
using Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.Votazioni;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Init.Sigepro.FrontEnd.QsParameters.Commissioni;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved.Commissioni
{
    public partial class dettaglio_pratica : CommissioniBasePage
    {
        public QsIdCommissione IdCommissione => new QsIdCommissione(Request.QueryString);
        public QsUuidIstanza UuidIstanza=> new QsUuidIstanza(Request.QueryString);

        [Inject]
        protected ICommissioniService _commissioniService { get; set; }

        [Inject]
        protected IUrlDownloadOggettiService _urlDownloadOggettiService { get; set; }

        [Inject]
        public IVotazioniCommissioniService _votazioniCommissioniService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public override void DataBind()
        {
            var pratica = this._commissioniService.GetDettaglioPraticaPerUtenteCorrente(IdCommissione.Value, UuidIstanza.Value);

            if(pratica == null)
            {
                ErroreAccesso();
                return;
            }

            lblComune.Value = pratica.DatiPratica.Comune;
            lblNumero.Value = pratica.DatiPratica.NumeroData;
            lblProtocollo.Value = pratica.DatiPratica.DatiProtocollo;
            lblRichiedente.Value = pratica.DatiPratica.Richiedente;
            lblIntervento.Value = pratica.DatiPratica.Intervento;
            lblLavori.Value = pratica.DatiPratica.DescrizioneLavori;
            lblDescrizioneParere.Value = pratica.DatiPratica.DescrizioneParere;
            lblParereEsteso.Value = pratica.DatiPratica.ParereEsteso;

            gvLocalizzazioni.DataSource = pratica.Localizzazioni;
            gvLocalizzazioni.DataBind();

            fsLocalizzazioni.Visible = pratica.Localizzazioni.Any();
            fsParere.Visible = !String.IsNullOrEmpty(pratica.DatiPratica.DescrizioneParere);

            // Visualizzazione del div del voto espresso
            fsParereEspresso.Visible = false;


            var parere = this._votazioniCommissioniService.GetVotoUtenteLoggato(this.IdCommissione.Value, this.UuidIstanza.Value);

            if (parere != null && parere.Voto != null)
            {
                lblVotoEspresso.Value = parere.Voto.DescrizioneParere;
                lblNoteVoto.Value = parere.Voto.Note;
                lblProtocolloVoto.Value = String.IsNullOrEmpty(parere.Voto.NumeroProtocollo) ? 
                                            "Non protocollato" :
                                            $"N.{parere.Voto.NumeroProtocollo} del {parere.Voto.DataProtocollo}";

                fsParereEspresso.Visible = true;
                cmdParere.Visible = false;

                this.MessaggiInformativi.Add("Hai già espresso un parere per questa pratica");
            }
            else
            {
                this.cmdParere.Visible = this._votazioniCommissioniService.UtenteLoggatoPuoEsprimereVoto(this.IdCommissione.Value, this.UuidIstanza.Value);
            }

            var documenti = pratica.Documenti.Istanza
                                                    .Union(pratica.Documenti.Endoprocedimenti)
                                                    .Union(pratica.Documenti.Movimenti)
                                                    .OrderBy(x => x.Categoria)
                                                    .ThenBy(x => x.Descrizione);

            gvDocumenti.DataSource = documenti;
            gvDocumenti.DataBind();

            fsDocumenti.Visible = documenti.Any();
        }

        protected void cmdChiudi_Click(object sender, EventArgs e)
        {

            var url = UrlBuilder.Url("~/reserved/commissioni/dettaglio-commissione.aspx", mp =>
            {
                mp.Add(new QsAliasComune(this.IdComune));
                mp.Add(new QsSoftware(this.Software));
                mp.Add(this.IdCommissione);
            });

            Response.Redirect(url);
        }

        protected void gvDocumenti_SelectedIndexChanged(object sender, EventArgs e)
        {
            var codiceOggetto = (int)gvDocumenti.DataKeys[gvDocumenti.SelectedIndex].Values[0];
            var firmato = (bool)gvDocumenti.DataKeys[gvDocumenti.SelectedIndex].Values[1];

            var verificaAccesso = this._commissioniService.VerificaAccessoAFilePerUtenteCorrente(IdCommissione.Value, UuidIstanza.Value, codiceOggetto);

            if (!verificaAccesso)
            {
                ErroreAccesso();
                return;
            }

            if (firmato)
            {
                var url = this._urlDownloadOggettiService.GetUrlDownloadFirmato(codiceOggetto);
                Response.Redirect(url);
                return;
            }

            var urlDownload = this._urlDownloadOggettiService.GetUrlDownload(codiceOggetto);

            var script = $"window.open('{ResolveClientUrl(urlDownload)}');";

            this.Page.ClientScript.RegisterStartupScript(GetType(), "apriDownload", script, true);
        }

        protected void lnkParere_Click(object sender, EventArgs e)
        {

            var url = UrlBuilder.Url("~/reserved/commissioni/parere-pratica.aspx", mp =>
            {
                mp.Add(new QsAliasComune(this.IdComune));
                mp.Add(new QsSoftware(this.Software));
                mp.Add(this.IdCommissione);
                mp.Add(this.UuidIstanza);
            });

            Response.Redirect(url);
        }

    }
}