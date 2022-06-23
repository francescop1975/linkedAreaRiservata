using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Init.Sigepro.FrontEnd.AppLogic.Utils.SerializationExtensions;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Init.Sigepro.FrontEnd.GestioneMovimenti.ExternalServices;
using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
using Init.Sigepro.FrontEnd.QsParameters;
using Init.Sigepro.FrontEnd.Reserved.Visura;
using log4net;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using static Init.Sigepro.FrontEnd.Reserved.Visura.dati_generali;
using static Init.Sigepro.FrontEnd.Reserved.Visura.visura_autorizzazioni;
using static Init.Sigepro.FrontEnd.Reserved.Visura.visura_documenti;
using static Init.Sigepro.FrontEnd.Reserved.Visura.visura_endoprocedimenti;
using static Init.Sigepro.FrontEnd.Reserved.Visura.visura_localizzazioni;
using static Init.Sigepro.FrontEnd.Reserved.Visura.visura_movimenti;
using static Init.Sigepro.FrontEnd.Reserved.Visura.visura_oneri;
using static Init.Sigepro.FrontEnd.Reserved.Visura.visura_soggetti;

namespace Init.Sigepro.FrontEnd.Reserved
{
    public partial class VisuraExCtrl : System.Web.UI.UserControl
    {
        [Inject]
        public IVisuraService _visuraService { get; set; }
        [Inject]
        public IScadenzeService _scadenzeService { get; set; }
        [Inject]
        public IAuthenticationDataResolver _authDataResolver { get; set; }

        [Inject]
        public IConfigurazione<ParametriVisura> _configurazione { get; set; }

        [Inject]
        public IUrlDownloadOggettiService _urlDownloadOggettiService { get; set; }

        public delegate void ScadenzaSelezionataDelegate(object sender, string idScadenza);
        public event ScadenzaSelezionataDelegate ScadenzaSelezionata;

        ILog _log = LogManager.GetLogger(typeof(VisuraExCtrl));

        /*
        public bool DaArchivio
        {
            get { object o = ViewState["DaArchivio"]; return o == null ? false : (bool)o; }
            set { ViewState["DaArchivio"] = value; }
        }
        */

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

        public bool MostraDocumentiNonValidi
        {
            get { object o = ViewState["MostraDocumentiNonValidi"]; return o == null ? true : (bool)o; }
            set { ViewState["MostraDocumentiNonValidi"] = value; visuraEndoprocedimenti.MostraDocumentiNonValidi = value; }
        }

        public bool MostraScadenze
        {
            get { object o = ViewState["MostraScadenze"]; return o == null ? true : (bool)o; }
            set { ViewState["MostraScadenze"] = value; }
        }

        public bool MostraPraticheCollegate
        {
            get { return visuraMovimenti.MostraPraticheCollegate; }
            set { visuraMovimenti.MostraPraticheCollegate = value; }
        }

        public LivelloAccessoVisura LivelloAccesso
        {
            get { return LivelloAccessoVisura.FromSerializationCode(this.ViewState["LivelloAccesso"]?.ToString()); }
            set { this.ViewState["LivelloAccesso"] = value.ToSerializationCode(); }
        }

        public Istanze DataSource { get; set; }

        public VisuraTabList TabsPagina = VisuraTabList.Default;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void EffettuaVisuraIstanza(string codiceIstanza)
        {
            var istanza = _visuraService.GetByUuid(codiceIstanza, true);

            EffettuaVisuraIstanza(istanza);
        }

        public void EffettuaVisuraIstanza(Istanze istanza)
        {
            DataSource = istanza;
            DataBind();

            try
            {
                if (!IsPostBack)
                    ltrIntestazioneDettaglio.Text = _configurazione.Parametri.MessaggioIntestazioneVisura;
            }
            catch (Exception ex1)
            {
                _log.Error(ex1.ToString());
            }
        }

        public override void DataBind()
        {
            //try
            //{
            if (DataSource == null)
                return;

            var visuraSuStessoComune = DataSource.IDCOMUNE == ((ReservedBasePage)this.Page).UserAuthenticationResult.IdComuneDb;

            var praticheCollegate = DataSource.Movimenti.Where(x => !String.IsNullOrEmpty(x.UuidPraticaCollegata));

            if (MostraPraticheCollegate && _configurazione.Parametri.VerticalizzazioneArpaCalabriaAttiva && praticheCollegate.Any())
            {
                var url = UrlBuilder.Url(Request.Url.ToString().Split('?')[0], qb =>
               {

                   foreach (var key in Request.QueryString.Keys)
                   {
                       var val = Request.QueryString[key.ToString()];

                       if (key.ToString() == QsUuidIstanza.QuerystringParameterName)
                       {
                           val = praticheCollegate.First().UuidPraticaCollegata;
                       }

                       qb.Add(key.ToString(), val);
                   }
               });

                Response.Redirect(url);
            }

            datiGenerali.MostraDatiProtocollo = this.LivelloAccesso != LivelloAccessoVisura.AccessoAnonimo;
            datiGenerali.DataSource = new VisuraDatiGeneraliDataSource
            {
                ComunePratica = DataSource.ComuneIstanza.COMUNE,
                DataPratica = DataSource.DATA,
                DataProtocollo = DataSource.DATAPROTOCOLLO,
                Intervento = DataSource.Intervento.SC_DESCRIZIONE,
                Istruttore = DataSource.Istruttore?.RESPONSABILE,
                NumeroPratica = DataSource.NUMEROISTANZA,
                NumeroProtocollo = DataSource.NUMEROPROTOCOLLO,
                Oggetto = DataSource.LAVORI,
                Operatore = DataSource.Operatore?.RESPONSABILE,
                PosizioneArchivio = DataSource.POSIZIONEARCHIVIO,
                ResponsabileProcedimento = DataSource.ResponsabileProc?.RESPONSABILE,
                Stato = DataSource.Stato.Stato
            };
            datiGenerali.DataBind();

            if (!LivelloAccesso.DatiGenerali)
            {
                datiGenerali.Visible = false;
                TabsPagina.RimuoviTab(VisuraTabListNames.DatiGenerali);
            }

            // Soggetti dell'istanza
            var soggetti = new List<VisuraSoggettiListItem>();

            soggetti.Add(new VisuraSoggettiListItem(DataSource.Richiedente, DataSource.TipoSoggetto, DataSource.AziendaRichiedente));

            if (DataSource.Professionista != null)
            {
                TipiSoggetto ts = new TipiSoggetto();
                ts.TIPOSOGGETTO = "Intermediario";
                soggetti.Add(new VisuraSoggettiListItem(DataSource.Professionista, ts));
            }

            if (DataSource.Richiedenti != null)
            {
                var richiedenti = DataSource.Richiedenti.Select(x => new VisuraSoggettiListItem(x.Richiedente, x.TipoSoggetto, x.AnagrafeCollegata, x.Procuratore));

                soggetti.AddRange(richiedenti);
            }

            if (!LivelloAccesso.DatiGenerali || !LivelloAccesso.SogettiPratica)
            {
                soggetti = new List<VisuraSoggettiListItem>();
            }

            visuraSoggetti.DataSource = soggetti;
            visuraSoggetti.DataBind();



            // Localizzazioni
            visuraLocalizzazioni.MostraDatiCatastaliEstesi = MostraDatiCatastaliEstesi;
            visuraLocalizzazioni.DataSource = new VisuraLocalizzazioniDataSource
            {
                Stradario = DataSource.Stradario,
                Mappali = DataSource.Mappali
            };
            visuraLocalizzazioni.DataBind();

            var numeroLocalizzazioni = visuraLocalizzazioni.NumeroRecords;

            TabsPagina.SetBadgeValue(VisuraTabListNames.Localizzazioni, numeroLocalizzazioni.ToString());

            if (numeroLocalizzazioni == 0)
            {
                TabsPagina.RimuoviTab(VisuraTabListNames.Localizzazioni);
                visuraLocalizzazioni.Visible = false;
            }


            if (visuraSuStessoComune && LivelloAccesso.Schede )
            {
                // Schede dinamiche
                schedeDinamicheReadonly.Istanza = DataSource;
                schedeDinamicheReadonly.DataBind();

                TabsPagina.SetBadgeValue(VisuraTabListNames.Schede, schedeDinamicheReadonly.ConteggioSchede.ToString());

                if (schedeDinamicheReadonly.ConteggioSchede == 0)
                {
                    schedeDinamicheReadonly.Visible = false;
                    TabsPagina.RimuoviTab(VisuraTabListNames.Schede);
                }
            }
            else
            {
                schedeDinamicheReadonly.Visible = false;
                TabsPagina.RimuoviTab(VisuraTabListNames.Schede);
            }

            // Procedimenti
            var endo = DataSource.EndoProcedimenti.Select(x => new VisuraEndoListItem
            {
                Id = Convert.ToInt32(x.CODICEINVENTARIO),
                CodiceIstanza = Convert.ToInt32(DataSource.CODICEISTANZA),
                Endoprocedimento = x.Endoprocedimento.Procedimento,
                NumeroAllegati = x.IstanzeAllegati?.Where(y =>
                    !String.IsNullOrEmpty(y.CODICEOGGETTO) && (y.CONTROLLOOK == "1" || MostraDocumentiNonValidi)).Count() ?? 0
            });

            visuraEndoprocedimenti.PermettiDownload = visuraSuStessoComune;
            visuraEndoprocedimenti.DataSource = endo;
            visuraEndoprocedimenti.DataBind();

            var numeroEndo = endo?.Count() ?? 0;

            TabsPagina.SetBadgeValue(VisuraTabListNames.Endoprocedimenti, numeroEndo.ToString());

            if (numeroEndo == 0 || LivelloAccesso.Endoprocedimenti == false)
            {
                TabsPagina.RimuoviTab(VisuraTabListNames.Endoprocedimenti);
                visuraEndoprocedimenti.Visible = false;
            }

            // Documenti
            var documenti = DataSource.DocumentiIstanza
                                        .Where(x => !String.IsNullOrEmpty(x.CODICEOGGETTO));

            if (!MostraDocumentiNonValidi)
            {
                documenti = documenti.Where(x => x.ControlloOk.GetValueOrDefault(0) == 1);
            }

            var documentiVisura = documenti.Select(x => new VisuraDocumentiListItem
            {
                CodiceOggetto = Convert.ToInt32(x.CODICEOGGETTO),
                Data = x.DATA,
                Descrizione = x.DOCUMENTO,
                Md5 = x.Oggetto.Md5,
                NomeFile = x.Oggetto.NOMEFILE,
                UrlDownload = _urlDownloadOggettiService.GetUrlDownload(Convert.ToInt32(x.CODICEOGGETTO))
            });

            visuraDocumenti.PermettiDownload = visuraSuStessoComune;
            visuraDocumenti.DataSource = documentiVisura;
            visuraDocumenti.DataBind();

            var numeroDocumenti = documentiVisura?.Count() ?? 0;

            TabsPagina.SetBadgeValue(VisuraTabListNames.Documenti, numeroDocumenti.ToString());

            if (numeroDocumenti == 0 || LivelloAccesso.Documenti == false)
            {
                TabsPagina.RimuoviTab(VisuraTabListNames.Documenti);
                visuraDocumenti.Visible = false;
            }

            // Oneri
            var oneri = DataSource.Oneri
                                    .Where(x => x.DATAPAGAMENTO.HasValue && x.ImportoPagato.GetValueOrDefault(0) > 0)
                                    .Select(x => new VisuraOneriListItem
                                    {
                                        Causale = x.CausaleOnere.CoDescrizione,
                                        Importo = (float)x.ImportoPagato.GetValueOrDefault(0),
                                        DataPagamento = x.DATAPAGAMENTO,
                                        DataScadenza = x.DATASCADENZA
                                    });

            visuraOneri.DataSource = oneri;
            visuraOneri.DataBind();

            var numeroOneri = oneri?.Count() ?? 0;

            TabsPagina.SetBadgeValue(VisuraTabListNames.Oneri, numeroOneri.ToString());

            if (numeroOneri == 0 || LivelloAccesso.Oneri == false)
            {
                TabsPagina.RimuoviTab(VisuraTabListNames.Oneri);
                visuraOneri.Visible = false;
            }


            // movimenti
            var movimenti = DataSource.Movimenti
                                      .Where(x => x.PUBBLICA == "1" && x.DATA.HasValue)
                                      .Select(x => new VisuraMovimentiListItem
                                      {
                                          Id = Convert.ToInt32(x.CODICEMOVIMENTO),
                                          CodiceIstanza = Convert.ToInt32(DataSource.CODICEISTANZA),
                                          Descrizione = x.MOVIMENTO,
                                          Data = x.DATA,
                                          Parere = x.PUBBLICAPARERE == "1" ? x.PARERE : String.Empty,
                                          NumeroProtocollo = x.NUMEROPROTOCOLLO,
                                          DataProtocollo = x.DATAPROTOCOLLO,
                                          UuidPraticaCollegata = x.UuidPraticaCollegata,
                                          Allegati = x.MovimentiAllegati?.Where(y => y.FlagPubblica.GetValueOrDefault(0) == 1 &&
                                                                                          !String.IsNullOrEmpty(y.CODICEOGGETTO))
                                                                                             .Select(y => new VisuraMovimentiAllegato
                                                                                             {
                                                                                                 Descrizione = y.DESCRIZIONE,
                                                                                                 NomeFile = y.Oggetto.NOMEFILE,
                                                                                                 UrlDownload = this._urlDownloadOggettiService
                                                                                                                    .GetUrlDownload(Convert.ToInt32(y.CODICEOGGETTO))
                                                                                             })
                                      });


            visuraMovimenti.PermettiDownload = visuraSuStessoComune;
            visuraMovimenti.DataSource = movimenti;
            visuraMovimenti.DataBind();

            var numeroMovimenti = movimenti?.Count() ?? 0;

            if (numeroMovimenti == 0 || LivelloAccesso.MovimentiEffettuati == false)
            {
                titoloMovimenti.Visible = false;
                visuraMovimenti.Visible = false;
            }

            // Autorizzazioni
            var autorizzazioni = DataSource.Autorizzazioni.Select(x => new VisuraAutorizzazioniListItem
            {
                Data = x.AUTORIZDATA,
                Descrizione = x.Registro.TR_DESCRIZIONE,
                Note = x.AUTORIZRESPONSABILE,
                Numero = x.AUTORIZNUMERO
            });

            visuraAutorizzazioni.DataSource = autorizzazioni;
            visuraAutorizzazioni.DataBind();

            var numeroAutorizzazioni = autorizzazioni?.Count() ?? 0;

            TabsPagina.SetBadgeValue(VisuraTabListNames.Autorizzazioni, numeroAutorizzazioni.ToString());

            if (numeroAutorizzazioni == 0 || LivelloAccesso.Autorizzazioni == false)
            {
                TabsPagina.RimuoviTab(VisuraTabListNames.Autorizzazioni);
                visuraAutorizzazioni.Visible = false;
            }

            // scadenze
            string codiceUtente = _authDataResolver.IsAuthenticated ? _authDataResolver.DatiAutenticazione.DatiUtente.Codicefiscale : null;


            var listaScadenze = _scadenzeService.GetListaScadenzeByCodiceIstanza(DataSource.SOFTWARE, Convert.ToInt32(DataSource.CODICEISTANZA), codiceUtente);

            dgScadenze.DataSource = listaScadenze;
            dgScadenze.DataBind();

            var numeroScadenze = listaScadenze?.Count() ?? 0;

            TabsPagina.SetBadgeValue(VisuraTabListNames.Scadenze, numeroScadenze.ToString());

            if (!MostraScadenze || numeroScadenze == 0)
            {
                TabsPagina.RimuoviTab(VisuraTabListNames.Scadenze);
                dgScadenze.Visible = false;
            }

            if (!MostraPraticheCollegate)
            {
                // ???
            }

            // Tabs
            var tabs = TabsPagina.Tabs.AsEnumerable();

            if (LivelloAccesso == LivelloAccessoVisura.AccessoAnonimo)
            {
                tabs = tabs.Where(x => x.VisibileDaArchivio);
            }

            rptTabs.DataSource = tabs;
            rptTabs.DataBind();
        }

        protected string GetUrlMovimento(object idMovimento)
        {
            var page = Page as ReservedBasePage;

            return ToAbsoluteUrl(UrlBuilder.Url("~/Reserved/Gestionemovimenti/EffettuaMovimento.aspx", qs =>
            {
                qs.Add(new QsAliasComune(page.IdComune));
                qs.Add(new QsSoftware(page.Software));
                qs.Add("IdMovimento", idMovimento.ToString());
            }));

        }
        private string ToAbsoluteUrl(string relative)
        {
            //return relative;
            var pre = "//"
                        + HttpContext.Current.Request.Url.Authority
                        + HttpContext.Current.Request.ApplicationPath;

            return pre + relative.Replace("~", String.Empty);
            
        }

        public string EsitoMovimento(object val)
        {
            if (val == null || String.IsNullOrEmpty(val.ToString())) return String.Empty;

            if (val.ToString() == "0") return "Negativo";

            return "Positivo";
        }

        public void dgScadenze_SelectedIndexChanged(object sender, EventArgs e)
        {
            string scadenza = dgScadenze.DataKeys[dgScadenze.SelectedIndex].Value.ToString();

            if (ScadenzaSelezionata != null)
                ScadenzaSelezionata(this, scadenza);
        }

    }
}