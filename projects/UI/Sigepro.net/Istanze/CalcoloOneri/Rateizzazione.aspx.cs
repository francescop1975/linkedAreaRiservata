using System;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SIGePro.Net;
using Init.SIGePro.Manager;
using Init.SIGePro.Data;
using System.Collections.Generic;
using SIGePro.Net.Navigation;
using Init.Utils.Web.UI;
using System.Reflection;
using Init.SIGePro.Manager.Logic.CalcoloOneri.Rateizzazioni;
using Init.SIGePro.Manager.Configuration;
using Init.SIGePro.Manager.Logic.GestioneOneri;
using log4net;

namespace Sigepro.net.Istanze.CalcoloOneri
{
    public partial class Istanze_CalcoloOneri_Rateizzazione : BasePage
    {
        protected int? CodiceRaggruppamento => string.IsNullOrEmpty(Request.QueryString["codiceraggruppamento"]) ? (int?)null : Convert.ToInt32(Request.QueryString["codiceraggruppamento"]);
        protected int? CodiceOnere  => string.IsNullOrEmpty(Request.QueryString["id"]) ? (int?)null : Convert.ToInt32(Request.QueryString["id"]);
        protected int CodiceIstanza  => Convert.ToInt32(Request.QueryString["codiceistanza"]);

        ILog _log = LogManager.GetLogger(typeof(Istanze_CalcoloOneri_Rateizzazione));

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.TabSelezionato = IntestazionePaginaTipiTabEnum.Scheda;

            if (!Page.IsPostBack)
            {
                SetEtichetta();
                BindComboTipiRateizzazioni();
                BindComboFrequenzaRate();
                BindComboScadenza();
                SetRateizzazione();

                IstanzeOneriMgr mgr = new IstanzeOneriMgr(Database, AuthenticationInfo);

                cmdRateizza1.Visible = (this.CodiceOnere.HasValue) ?
                                        !mgr.IsOnereRateizzato(this.IdComune, this.CodiceIstanza, this.CodiceOnere.Value) :
                                        !mgr.IsRaggruppamentoRateizzato(this.IdComune, this.CodiceIstanza, this.CodiceRaggruppamento.Value);

                cmdDerateizza1.Visible = !cmdRateizza1.Visible;
            }
        }

        #region Metodi usati per settare la pagina nel momento in cui viene invocata
        private void BindComboScadenza()
        {
            this.ddlComportamentoScadenza.Item.DataSource = (new OneriTipiRateizzazioneMgr(Database)).GetTipiScadenze();
            this.ddlComportamentoScadenza.Item.DataBind();
        }

        private void BindComboTipiRateizzazioni()
        {
            OneriTipiRateizzazioneMgr mgrotr = new OneriTipiRateizzazioneMgr(Database);
            OneriTipiRateizzazione otr = new OneriTipiRateizzazione();
            otr.Idcomune = IdComune;
            otr.Software = Software;

            List<OneriTipiRateizzazione> list = mgrotr.GetList(otr);
            this.ddlTipiRateizzazione.Item.DataSource = list;
            this.ddlTipiRateizzazione.Item.DataBind();

            //Leggi configurazione per l'utente dalla tabella CONFIGURAZIONEUTENTE
            if ((list != null) && (list.Count != 0))
            {
                ddlTipiRateizzazione.Value = LeggiConfUtente(list[0].Tiporateizzazione.ToString());
            }
        }

        private void BindComboFrequenzaRate()
        {
            this.ddlFrequenzaRateAmmortamentoFrancese.Item.DataSource = new OneriTipiRateizzazioneMgr(Database).GetFrequenzaRate();
            this.ddlFrequenzaRateAmmortamentoFrancese.Item.DataBind();
        }

        protected void SetEtichetta()
        {
            //Rateizzazione per causale
            if (!CodiceRaggruppamento.HasValue)
            {
                this.lblTitolo1.Visible = false;
                this.lblRaggruppamento.Visible = false;

                IstanzeOneriMgr mgrio = new IstanzeOneriMgr(Database, AuthenticationInfo);
                IstanzeOneri io = mgrio.GetById(IdComune, CodiceOnere.ToString());
                this.lblImporto.Text = io.PREZZO.ToString();

                if (io.PREZZOISTRUTTORIA.GetValueOrDefault(0.0d) == 0.0d)
                {
                    this.lblImportoIstrEtichetta.Visible = false;
                    this.lblImportoIstr.Visible = false;
                }
                else
                {
                    this.lblImportoIstr.Text = io.PREZZOISTRUTTORIA.ToString();
                }

                TipiCausaliOneriMgr mgrtco = new TipiCausaliOneriMgr(Database);
                TipiCausaliOneri tco = mgrtco.GetById(IdComune, Convert.ToInt32(io.FKIDTIPOCAUSALE));
                this.lblCausale.Text = tco.CoDescrizione;
            }
            //Rateizzazione per raggruppamento
            if (CodiceRaggruppamento.HasValue)
            {
                this.lblCausale.Visible = false;

                RaggruppamentoCausaliOneriMgr mgr = new RaggruppamentoCausaliOneriMgr(Database);
                RaggruppamentoCausaliOneri rco = mgr.GetById(IdComune, CodiceRaggruppamento.Value);

                this.lblRaggruppamento.Text = rco.RcoDescr;
                this.lblImporto.Text = mgr.GetImportoTotale(IdComune, CodiceIstanza, CodiceRaggruppamento.Value).ToString();

                double dImportoTotaleIstruttoria = mgr.GetImportoTotale(IdComune, CodiceIstanza, CodiceRaggruppamento.Value, true);
                this.lblImportoIstr.Text = dImportoTotaleIstruttoria.ToString();

                if (dImportoTotaleIstruttoria == 0.0)
                {
                    this.lblImportoIstrEtichetta.Visible = false;
                    this.lblImportoIstr.Visible = false;
                }
            }
        }

        //Questo metodo viene usato anche in seguito alla modifica del valore della combo box adibita a modifcare il tipo di rateizzazione
        protected void SetRateizzazione()
        {
            if (!string.IsNullOrEmpty(ddlTipiRateizzazione.Value))
            {
                txGiorniScadenze.Visible = true;
                ddlFrequenzaRateAmmortamentoFrancese.Visible = false;

                var mgrotr = new OneriTipiRateizzazioneMgr(Database);
                var otr = mgrotr.GetById(IdComune, Convert.ToInt32(ddlTipiRateizzazione.Value));

                var dataInizioRateizzazione = (OneriTipiRateizzazioneMgr.DataInizioRateizzazione)otr.Determdatainiziorate.GetValueOrDefault(0);

                var dataInizioRate = mgrotr.GetDataInizioRate(IdComune, CodiceIstanza, dataInizioRateizzazione, otr);

                txNumeroRate.Value = otr.Nrorate;
                txRipartizioneRate.Value = otr.Ripartizionerate;
                txGiorniScadenze.Value = otr.Frequenzarate;
                ddlComportamentoScadenza.Value = otr.Scadenzarate.ToString();
                txPercInteressi.Value = otr.Interessirate;
                txDataInizio.Value = dataInizioRate;
                chkInteressiLegali.Item.Checked = otr.FlagInteressiLegali == 1 ? true : false;
                ddlInteressiLegali.SelectedValue = otr.TipoAnatocismo.GetValueOrDefault(0).ToString();
                TxDataInizioIntLegali.Item.Text = txDataInizio.Value;

                ddlInteressiLegali.Visible = chkInteressiLegali.Item.Checked;
                TxDataInizioIntLegali.Visible = chkInteressiLegali.Item.Checked;
                txPercInteressi.Visible = !chkInteressiLegali.Item.Checked;
                chkInteressiLegali.Visible = true;

                if (otr.TipologiaRateizzazione == OneriTipiRateizzazioneMgr.TipoRateizzazione.AMMORTAMENTO_FRANCESE.ToString())
                {
                    txGiorniScadenze.Visible = false;
                    ddlFrequenzaRateAmmortamentoFrancese.Visible = true;
                    ddlFrequenzaRateAmmortamentoFrancese.Value = otr.Frequenzarate;
                    chkInteressiLegali.Item.Checked = false;
                    chkInteressiLegali.Visible = false;
                }

            }
        }
        #endregion

        #region Metodi usati per verificare la validità dei valori inseriti
        private bool VerificaValiditaCampi()
        {
            if (!VerificaCampoVuoto("txNumeroRate"))
                return false;
            if (!VerificaCampoVuoto("txRipartizioneRate"))
                return false;
            else if (!VerificaValiditaCampo("txRipartizioneRate"))
                return false;
            if (!VerificaCampoVuoto("txDataInizio"))
                return false;
            if (!VerificaCampoVuoto("txGiorniScadenze"))
                return false;
            else if (!VerificaValiditaCampo("txGiorniScadenze"))
                return false;

            return true;
        }

        private bool VerificaCampoVuoto(string sId)
        {
            WebControl ctrl = (WebControl)this.UpdatePanel1.FindControl(sId);
            if (string.IsNullOrEmpty(GetValue(ctrl)))
            {
                MostraErrore("Attenzione, i campi contrassegnati con un asterisco sono obbligatori.", null);
                return false;
            }
            return true;
        }

        private bool VerificaValiditaCampo(string sId)
        {
            WebControl ctrl = (WebControl)this.UpdatePanel1.FindControl(sId);
            string[] aElem = GetValue(ctrl).Split(';');
            foreach (string elem in aElem)
            {
                try
                {
                    Convert.ToDouble(elem);
                }
                catch (Exception)
                {
                    MostraErrore("Attenzione, uno dei campi non contiene un valore valido.", null);
                    return false;
                }
            }

            return true;
        }

        private string GetValue(WebControl m_control)
        {
            Type type = m_control.GetType();
            ControlValuePropertyAttribute[] attrb = (ControlValuePropertyAttribute[])type.GetCustomAttributes(typeof(ControlValuePropertyAttribute), true);
            if (attrb != null && attrb.Length > 0)
            {
                PropertyInfo pi = type.GetProperty(attrb[0].Name);
                if (pi == null) return String.Empty;
                object obj = pi.GetValue(m_control, null);
                return obj == null ? String.Empty : obj.ToString();
            }

            return String.Empty;
        }
        #endregion



        private void ScriviConfUtente(string valore)
        {
            string nomeParametro = GetNomeParametro();

            ConfigurazioneUtenteMgr confUtMgr = new ConfigurazioneUtenteMgr(Database);
            confUtMgr.SetValoreParametro(IdComune, AuthenticationInfo.CodiceResponsabile.Value, nomeParametro, valore);
        }

        private string GetNomeParametro()
        {
            string nomeParametro = "ROInt" + (new IstanzeMgr(Database).GetById(IdComune, CodiceIstanza).CODICEINTERVENTOPROC);
            if (!CodiceRaggruppamento.HasValue)
                nomeParametro += "#Caus" + new IstanzeOneriMgr(Database, AuthenticationInfo).GetById(IdComune, CodiceOnere.ToString()).FKIDTIPOCAUSALE;
            else
                nomeParametro += "#Rag" + CodiceRaggruppamento;

            return nomeParametro;
        }

        private string LeggiConfUtente(string valoreDefault)
        {
            string nomeParametro = GetNomeParametro();

            ConfigurazioneUtenteMgr confUtMgr = new ConfigurazioneUtenteMgr(Database);
            return confUtMgr.GetValoreParametro(IdComune, AuthenticationInfo.CodiceResponsabile.Value, nomeParametro, valoreDefault);
        }

        protected void cmdRateizza_Click(object sender, EventArgs e)
        {
            //Verifico validità campi
            if (!VerificaValiditaCampi())
                return;

            //Scrivi confiurazione nella tabella CONFIGURAZIONEUTENTE
            ScriviConfUtente(ddlTipiRateizzazione.Value);

            try
            {
                var rateizzazioniService = new RateizzazioniService(this.Token);

                //Rateizzazione per causale
                if (CodiceOnere.HasValue)
                {
                    var importo = Convert.ToDecimal(this.lblImporto.Text);
                    decimal importoIstruttoria = 0;

                    if (!string.IsNullOrEmpty(this.lblImportoIstr.Text))
                        importoIstruttoria = Convert.ToDecimal(this.lblImportoIstr.Text);

                    IstanzeOneriMgr mgr = new IstanzeOneriMgr(Database, AuthenticationInfo);
                    IstanzeOneri io = mgr.GetById(IdComune, CodiceOnere.ToString());

                    //Verifico che la l'onere non sia stato già pagato
                    if (io.DATAPAGAMENTO.HasValue)
                        base.CloseCurrentPage();

                    var parametri = CreaParametriRateizzazione(CodiceOnere.Value);

                    if (!rateizzazioniService.RateizzaOnere(parametri, importo, importoIstruttoria))
                        return;
                }

                //Rateizzazione per raggruppamento
                if (CodiceRaggruppamento.HasValue)
                {
                    var mgr = new RaggruppamentoCausaliOneriMgr(Database);

                    foreach (DataRow row in mgr.GetImporti(IdComune, CodiceIstanza, CodiceRaggruppamento.Value).Tables[0].Rows)
                    {
                        var idIstanzeOneri = Convert.ToInt32(row["id"]);
                        var importo = Convert.ToDecimal(row["prezzo"]);
                        decimal importoIstruttoria = 0;

                        if (row["prezzoistruttoria"] != DBNull.Value)
                            importoIstruttoria = Convert.ToDecimal(row["prezzoistruttoria"]);

                        var parametri = CreaParametriRateizzazione(idIstanzeOneri);

                        if (!rateizzazioniService.RateizzaOnere(parametri, importo, importoIstruttoria))
                            return;
                    }
                }

                base.CloseCurrentPage();
            }
            catch (Exception ex)
            {
                MostraErrore("Attenzione, non è possibile effettuare la rateizzazione per il seguente motivo: " + ex.Message, ex);
            }
        }

        private ParametriRateizzazione CreaParametriRateizzazione(int idIstanzeOneri)
        {
            var tipoRateizzazione = Convert.ToInt32(ddlTipiRateizzazione.Value);
            var dataInizioRateizzazione = Convert.ToDateTime(this.txDataInizio.Value);
            var dataInizioInteressiLegali = (chkInteressiLegali.Item.Checked) ? Convert.ToDateTime(this.TxDataInizioIntLegali.Value) : (DateTime?)null;
            var speseRateizzazioni = String.IsNullOrEmpty(txSpeseRateizzazioni.Value) ? 0 : Convert.ToDecimal(txSpeseRateizzazioni.Value);
            var codiceIstanza = this.CodiceIstanza;

            return new ParametriRateizzazione(tipoRateizzazione, dataInizioRateizzazione, dataInizioInteressiLegali, speseRateizzazioni, codiceIstanza, idIstanzeOneri);
        }

        protected void cmdChiudi_Click(object sender, EventArgs e)
        {
            base.CloseCurrentPage();
        }

        protected void txNumeroRate_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txNumeroRate.Value))
            {
                //Modifica il campo Ripartizione rate
                var numeroRate = Convert.ToInt32(this.txNumeroRate.Value);
                Rateizzazione calcoloRate = new Rateizzazione();
                calcoloRate.NumeroRate = numeroRate;

                var ripartizioneRate = "";
                for (int i = 0; i < numeroRate; i++)
                    ripartizioneRate += calcoloRate.CalcolaRipartizioneRata(i) + ";";

                ripartizioneRate = ripartizioneRate.Remove(ripartizioneRate.Length - 1);

                this.txRipartizioneRate.Value = ripartizioneRate;

                //Modifica il campo Frequenza rate
                if (string.IsNullOrEmpty(this.txGiorniScadenze.Value))
                {
                    string frequenzaRate = "";

                    for (int i = 0; i < numeroRate; i++)
                        frequenzaRate += "30;";

                    frequenzaRate = frequenzaRate.Remove(frequenzaRate.Length - 1);
                    this.txGiorniScadenze.Value = frequenzaRate;
                }
                else
                {
                    string[] listaFrequenzaRate = txGiorniScadenze.Value.Split(new Char[] { ';' });

                    if (numeroRate < listaFrequenzaRate.Length)
                    {
                        txGiorniScadenze.Value = "";
                        
                        string frequenzaRate = "";
                        
                        for (int iCount = 0; iCount < numeroRate; iCount++)
                            frequenzaRate += listaFrequenzaRate[iCount] + ";";

                        frequenzaRate = frequenzaRate.Remove(frequenzaRate.Length - 1);
                        
                        this.txGiorniScadenze.Value = frequenzaRate;
                    }
                }

                //Modifica il campo Interessi
                this.txPercInteressi.Value = "";
            }
        }

        protected void cmdDerateizza_Click(object sender, EventArgs e)
        {
            string returnTo = Server.UrlEncode(Request.QueryString["returnTo"]);
            
            //Rateizzazione per causale
            if (CodiceOnere.HasValue)
            {
                IstanzeOneriMgr mgr = new IstanzeOneriMgr(Database, AuthenticationInfo);
                IstanzeOneri io = mgr.GetById(IdComune, CodiceOnere.ToString());

                var tipoCausale = Convert.ToInt32(io.FKIDTIPOCAUSALE);
                var url = $"~/Istanze/CalcoloOneri/Derateizzazione.aspx?Software={Software}&Token={AuthenticationInfo.Token}&CodiceIstanza={CodiceIstanza}&tipocausale={tipoCausale}&ReturnTo={returnTo}";
                Response.Redirect(url);

                return;
            }

            //Rateizzazione per raggruppamento
            if (CodiceRaggruppamento.HasValue)
            {
                var url = $"~/Istanze/CalcoloOneri/Derateizzazione.aspx?Software={Software}&Token={AuthenticationInfo.Token}&CodiceIstanza={CodiceIstanza}&codiceraggruppamento={CodiceRaggruppamento}&ReturnTo={returnTo}";
                Response.Redirect(url);

                return;
            }
        }

        protected void ddlTipiRateizzazione_ValueChanged(object sender, EventArgs e)
        {
            SetRateizzazione();
        }
    }
}
