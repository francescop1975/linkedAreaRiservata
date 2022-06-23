using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.GestioneInterventi;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.GestioneSTAR;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Init.SIGePro.Manager.DTO.Interventi;
using Init.Utils;
using log4net;
using Ninject;
using System;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;

namespace Init.Sigepro.FrontEnd.Reserved
{
    public partial class ListaIstanzePresentate : ReservedBasePage
    {
        private static class Constants
        {
            public const string IstanzaConBookmark = "1";
            public const string IstanzaSenzaBookmark = "0";
            public const string UrlIstanzeInSospesoStar = "~/reserved/istanzeInSospesoSTAR.aspx";
        }

        ILog _log = LogManager.GetLogger(typeof(ListaIstanzePresentate));

        [Inject]
        public IInterventiRepository _alberoProcRepository { get; set; }
        [Inject]
        public DomandeOnlineService _datiDomandaService { get; set; }
        [Inject]
        public STARUrlService _starUrlService { get; set; }

        public bool MostraDatiCatastaliEstesi
        {
            get
            {
                var obj = ConfigurationManager.AppSettings["MostraDatiCatastaliEstesi"];

                if (String.IsNullOrEmpty(obj))
                    return false;

                try
                {
                    return Convert.ToBoolean(obj);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        protected bool NoStar
        {
            get
            {
                return Request.QueryString["star"] == "0";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificaRedirectStar();

                DataBind();
            }

        }

        private void VerificaRedirectStar()
        {
            if (!NoStar && this._starUrlService.StarAttivo())
            {
                var qs = Request.QueryString.ToString();
                Response.Redirect(String.Format("{0}?{1}", Constants.UrlIstanzeInSospesoStar, qs));
                Response.End();
            }
        }

        protected override void DataBind(bool raiseOnDataBinding)
        {
            var codiceAnagrafe = UserAuthenticationResult.DatiUtente.Codiceanagrafe.Value;

            var ds = _datiDomandaService.GetDomandeInSospeso(codiceAnagrafe);

            dgIstanzePresentate.DataSource = ds;
            dgIstanzePresentate.DataBind();
        }

        public void dgIstanzePresentate_SelectedIndexChanged(object sender, EventArgs e)
        {
            var key = dgIstanzePresentate.DataKeys[dgIstanzePresentate.SelectedIndex].Value.ToString();
            var bookmark = (HiddenField)dgIstanzePresentate.Rows[dgIstanzePresentate.SelectedIndex].FindControl("hidBookmark");
            var redirPage = "~/Reserved/InserimentoIstanza/Benvenuto.aspx";

            if (bookmark != null && bookmark.Value == Constants.IstanzaConBookmark)
            {
                redirPage = "~/Reserved/InserimentoIstanza/BenvenutoBookmark.aspx";
            }

            Redirect(redirPage, qs =>
            {
                qs.Add("StepId", "0");
                qs.Add("IdPresentazione", key);

                if (NoStar)
                {
                    qs.Add("star", "0");
                }
            });
        }

        public void dgIstanzePresentate_ItemDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblRichiedente = (Label)e.Row.FindControl("lblRichiedente");
                var lblTipoIntervento = (Label)e.Row.FindControl("lblTipoIntervento");
                var lblIdDomanda = (Label)e.Row.FindControl("lblIdDomanda");
                var hidBookmark = (HiddenField)e.Row.FindControl("hidBookmark");
                var lblOggetto = (Label)e.Row.FindControl("lblOggetto");

                var datiDomanda = (FoDomande)e.Row.DataItem;

                try
                {
                    var domanda = _datiDomandaService.GetById(datiDomanda.Id.Value);

                    // Bookmarks
                    hidBookmark.Value = Constants.IstanzaSenzaBookmark;
                    if (!String.IsNullOrEmpty(domanda.ReadInterface.Bookmarks.Bookmark))
                    {
                        hidBookmark.Value = Constants.IstanzaConBookmark;
                    }

                    // Nominativo
                    var richiedente = domanda.ReadInterface.Anagrafiche.GetRichiedente();


                    if (richiedente != null)
                        lblRichiedente.Text = richiedente.ToString();

                    // Oggetto
                    if (domanda != null && domanda.ReadInterface.AltriDati != null)
                    {
                        lblOggetto.Text = domanda.ReadInterface.AltriDati.DescrizioneLavori;
                    }

                    // Intervento
                    try
                    {

                        string strIntervento = domanda.ReadInterface.AltriDati.Intervento == null ? "Non definito" : domanda.ReadInterface.AltriDati.Intervento.Descrizione;

                        if (String.IsNullOrEmpty(strIntervento))
                            strIntervento = StringaIntervento(domanda);

                        lblTipoIntervento.Text = strIntervento.Replace(Environment.NewLine, "<br />");
                    }
                    catch (Exception)
                    {
                        lblTipoIntervento.Text = "<div style='color:red'>Intervento non valido. La domanda non potrà essere ripresa</div>";
                        LinkButton lb = (LinkButton)e.Row.FindControl("Linkbutton1");

                        lb.Visible = false;
                    }

                    // oneri in sospeso (se esistono non si può eliminare la domanda)
                    //var oneriInSospeso = domanda.ReadInterface.Oneri.GetOperazioniConPagamentoInSospeso();
                    var chkChecked = e.Row.FindControl("chkChecked");

                    var literalOneriInSospeso = new Literal
                    {
                        Text = "<div class='alert alert-warning alert-pagamenti'>Operazione di pagamento in sospeso</div>"
                    };
                    var literalOneriPagatiOnLine = new Literal
                    {
                        Text = "<div class='alert alert-warning alert-pagamenti'>La domanda contiene oneri pagati</div>"
                    };

                    chkChecked.Visible = !domanda.ReadInterface.Oneri.GetOneriOnlineConPagamentoAvviato().Any() &&
                                         !domanda.ReadInterface.Oneri.GetOneriOnlineConPagamentoRiuscito().Any();

                    if (domanda.ReadInterface.Oneri.GetOneriOnlineConPagamentoAvviato().Any())
                    {
                        e.Row.Cells[1].Controls.Add(literalOneriInSospeso);
                    }

                    if (domanda.ReadInterface.Oneri.GetOneriOnlineConPagamentoRiuscito().Any())
                    {
                        e.Row.Cells[1].Controls.Add(literalOneriPagatiOnLine);

                    }
                }
                catch (Exception ex)
                {
                    lblTipoIntervento.Text = "Errore nella lettura dei dati della domanda: " + ex.Message;
                }
            }
        }

        public string StringaIntervento(DomandaOnline domanda)
        {
            var strIntervento = "Non specificato";

            if (domanda.ReadInterface.AltriDati.Intervento == null)
                return strIntervento;

            var idcomune = domanda.DataKey.IdComune;
            var codIntervento = domanda.ReadInterface.AltriDati.Intervento.Codice;

            var albero = _alberoProcRepository.GetAlberaturaNodoDaId(idcomune, codIntervento);

            return AttraversaAlbero(albero);

        }

        private string AttraversaAlbero(ClassTree<InterventoDto> albero)
        {
            var str = albero.Elemento.Descrizione;

            str += Environment.NewLine;

            if (albero.NodiFiglio.Any())
                str += AttraversaAlbero(albero.NodiFiglio[0]);

            return str;

        }

        protected void cmdDeleteRows_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow it in dgIstanzePresentate.Rows)
            {
                var chkChecked = (CheckBox)it.FindControl("chkChecked");

                if (chkChecked.Checked)
                {
                    try
                    {
                        int key = (int)dgIstanzePresentate.DataKeys[it.RowIndex].Value;
                        _datiDomandaService.Elimina(key, UserAuthenticationResult.DatiUtente.Codicefiscale);
                    }
                    catch (Exception ex)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var id = dgIstanzePresentate.DataKeys[it.RowIndex].Value;
                        this._log.Error($"{guid} Errore durante la cancellazione della pratica in sospeso, id={id}, ex={ex.ToString()}");
                        Errori.Add($"Si è verificato un errore inaspettato durante la cancellazione della domanda (dati tecnici: errore={guid}, id={id})");
                    }
                }
            }

            DataBind();
        }

        protected void cmdClose_Click(object sender, EventArgs e)
        {
            var url = UrlBuilder.Url("~/reserved/benvenuto.aspx", pb =>
            {
                pb.Add(new QsAliasComune(IdComune));
                pb.Add(new QsSoftware(Software));
            });

            Response.Redirect(url);
        }
    }
}
