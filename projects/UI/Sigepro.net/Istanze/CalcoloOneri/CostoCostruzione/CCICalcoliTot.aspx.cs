using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Manager.Logic.GestioneOneri;
using Init.Utils;
using Sigepro.net.Istanze.CalcoloOneri;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Istanze_CalcoloOneri_CostoCostruzione_CCICalcoliTot : PaginaTotaleOneriBase
{
    public Istanze_CalcoloOneri_CostoCostruzione_CCICalcoliTot()
    {
        //VerificaSoftware = false;
    }

    public override string Software
    {
        get
        {
            return this.Istanza.SOFTWARE;
        }
    }

    private int CodiceIstanza
    {
        get
        {
            string codiceIstanza = this.Request.QueryString["CodiceIstanza"];

            if (String.IsNullOrEmpty(codiceIstanza))
                throw new ArgumentException("Codice istanza non passato");

            return int.Parse(codiceIstanza);
        }
    }

    private Istanze m_istanza = null;
    private Istanze Istanza
    {
        get
        {
            if (this.m_istanza == null)
                this.m_istanza = new IstanzeMgr(this.AuthenticationInfo.CreateDatabase()).GetById(this.AuthenticationInfo.IdComune, Convert.ToInt32(this.CodiceIstanza));

            return this.m_istanza;
        }
    }

    #region visibilità delle righe della tabella del contributo
    public bool MostraRigaContributoProgetto
    {
        get { object o = this.ViewState["MostraRigaContributoProgetto"]; return o == null ? true : (bool)o; }
        set { this.ViewState["MostraRigaContributoProgetto"] = value; }
    }

    public bool MostraRigaContributoAttuale
    {
        get { object o = this.ViewState["MostraRigaContributoAttuale"]; return o == null ? true : (bool)o; }
        set { this.ViewState["MostraRigaContributoAttuale"] = value; }
    }
    #endregion

    public bool MostraTabellaContributo
    {
        get { return this.MostraRigaContributoProgetto || this.MostraRigaContributoAttuale; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ImpostaScriptEliminazione(this.cmdEliminaCalcolo);

        if (!this.IsPostBack)
        {
            if (!String.IsNullOrEmpty(this.Request.QueryString["IdCalcoloTot"]))
            {
                int id = Convert.ToInt32(this.Request.QueryString["IdCalcoloTot"]);
                this.BindDettaglio(new CCICalcoloTotMgr(this.Database).GetById(this.AuthenticationInfo.IdComune, id));
            }
            else
            {
                this.DataBind();
            }
        }
    }



    public override void DataBind()
    {
        this.gvLista.DataSource = new CCICalcoloTotMgr(this.AuthenticationInfo.CreateDatabase()).GetList(this.AuthenticationInfo.IdComune, this.CodiceIstanza);
        this.gvLista.DataBind();

        this.multiView.ActiveViewIndex = 0;
    }

    protected void cmdNuovo_Click(object sender, EventArgs e)
    {
        this.BindDettaglio(new CCICalcoloTot());
    }

    private void BindDettaglio(CCICalcoloTot cls)
    {
        this.IsInserting = cls.Id.GetValueOrDefault(int.MinValue) == int.MinValue;
        this.cmdEliminaCalcolo.Visible = !this.IsInserting;
        if (this.IsInserting)
        {
            this.BindInserimento(cls);
        }
        else
        {
            this.BindAggiornamento(cls);
        }
    }

    private void BindAggiornamento(CCICalcoloTot cls)
    {
        new CCICalcoloTotMgr(this.Database).IntegraForeignKey(cls);

        this.lblId.Text = cls.Id.ToString();
        this.lblData.Text = cls.Data.GetValueOrDefault(DateTime.MinValue).ToString("dd/MM/yyyy");
        this.lblListino.Text = new CCValiditaCoefficientiMgr(this.Database).GetById(this.AuthenticationInfo.IdComune, cls.FkCcvcId.GetValueOrDefault(int.MinValue)).Descrizione;
        this.lblTipoCalcolo.Text = new CCBaseTipoCalcoloMgr(this.Database).GetById(cls.FkBcctcId).Tipocalcolo;
        this.lblTipoIntervento.Text = new OCCBaseTipoInterventoMgr(this.Database).GetById(cls.FkOccbtiId).Intervento;
        this.lblDestinazione.Text = new OCCBaseDestinazioniMgr(this.Database).GetById(cls.FkOccbdeId).Destinazione;

        this.txtEditDescrizione.Text = cls.Descrizione;

        this.MostraRigaContributoAttuale =
        this.MostraRigaContributoProgetto = false;

        double totaleContributo = 0.0d;
        double contrAttuale = 0.0d;
        double contrProgetto = 0.0d;

        if (cls.StatoAttuale != null)
        {
            this.MostraRigaContributoAttuale = true;
            this.txtCostoEdificioAttuale.ValoreDouble = cls.StatoAttuale.CostocEdificio.GetValueOrDefault(double.MinValue);
            this.txtCoefficenteAttuale.ValoreDouble = cls.StatoAttuale.Coefficiente.GetValueOrDefault(double.MinValue);

            if (DoubleChecker.IsDoubleEmpty(cls.StatoAttuale.Riduzioneperc))
            {
                cls.StatoAttuale.Riduzioneperc = 0.0d;
                new CCICalcoloTContributoMgr(this.Database).Update(cls.StatoAttuale);
            }

            double quotaNoRiduz = cls.StatoAttuale.GetQuotaSenzaRiduzioni();
            double quotaConRiduz = cls.StatoAttuale.GetQuotaConRiduzioni();

            this.lblQuotaAttuale.Text = quotaNoRiduz.ToString("N2");
            this.txtQuotaContributoAttuale.ValoreDouble = quotaConRiduz;

            contrAttuale = quotaConRiduz;

            this.cmdDettagliCostoEdificioAttuale.Visible = cls.StatoAttuale.FkCcicId.GetValueOrDefault(int.MinValue) != int.MinValue;

            if (cls.StatoAttuale.FkCcicId.GetValueOrDefault(int.MinValue) != int.MinValue)
                this.cmdDettagliCostoEdificioAttuale.CommandArgument = cls.StatoAttuale.FkCcicId.ToString();

            this.cmdDettagliContributoAttuale.CommandArgument = cls.Id.ToString() + "$A";

            bool haRiduzioni = new CcICalcoloTContributoRiduzMgr(this.Database).GetListByIdTContributo(this.IdComune, cls.StatoAttuale.Id.GetValueOrDefault(int.MinValue)).Count > 0;

            this.txtVariazioneAttuale.ReadOnly = haRiduzioni;
            this.txtVariazioneAttuale.ValoreDouble = cls.StatoAttuale.Riduzioneperc.GetValueOrDefault(double.MinValue);

            this.lnkEditVariazioneAttuale.OnClientClick = "return " + (haRiduzioni || cls.StatoAttuale.Riduzioneperc == 0.0d ? "ModificaRiduzioni" : "ModificaNote") + "('" + this.Token + "'," + cls.StatoAttuale.Id + ");";

            this.hlpVariazioneAttuale.Text = this.GeneraTestoRiduzioni(cls.StatoAttuale);
        }

        if (cls.StatoDiProgetto != null)
        {
            this.MostraRigaContributoProgetto = true;
            this.txtCostoEdificioProgetto.ValoreDouble = cls.StatoDiProgetto.CostocEdificio.GetValueOrDefault(double.MinValue);
            this.txtCoefficenteProgetto.ValoreDouble = cls.StatoDiProgetto.Coefficiente.GetValueOrDefault(double.MinValue);

            if (DoubleChecker.IsDoubleEmpty(cls.StatoDiProgetto.Riduzioneperc))
            {
                cls.StatoDiProgetto.Riduzioneperc = 0.0d;
                new CCICalcoloTContributoMgr(this.Database).Update(cls.StatoDiProgetto);
            }

            double quotaNoRiduz = cls.StatoDiProgetto.GetQuotaSenzaRiduzioni();
            double quotaConRiduz = cls.StatoDiProgetto.GetQuotaConRiduzioni();

            this.lblQuotaProgetto.Text = quotaNoRiduz.ToString("N2");
            this.txtQuotaContributoProgetto.ValoreDouble = quotaConRiduz;

            contrProgetto = quotaConRiduz;

            this.cmdDettagliCostoEdificioProgetto.Visible = cls.StatoDiProgetto.FkCcicId.GetValueOrDefault(int.MinValue) != int.MinValue;

            if (cls.StatoDiProgetto.FkCcicId.GetValueOrDefault(int.MinValue) != int.MinValue)
                this.cmdDettagliCostoEdificioProgetto.CommandArgument = cls.StatoDiProgetto.FkCcicId.ToString();

            this.cmdDettagliContributoProgetto.CommandArgument = cls.Id.ToString() + "$P";

            this.txtVariazioneProgetto.ValoreDouble = cls.StatoDiProgetto.Riduzioneperc.GetValueOrDefault(double.MinValue);

            bool haRiduzioni = new CcICalcoloTContributoRiduzMgr(this.Database).GetListByIdTContributo(this.IdComune, cls.StatoDiProgetto.Id.GetValueOrDefault(int.MinValue)).Count > 0;
            this.txtVariazioneProgetto.ReadOnly = haRiduzioni;

            this.lnkEditVariazioneProgetto.OnClientClick = "return " + (haRiduzioni || cls.StatoDiProgetto.Riduzioneperc == 0.0d ? "ModificaRiduzioni" : "ModificaNote") + "('" + this.Token + "'," + cls.StatoDiProgetto.Id + ");";

            this.hlpVariazioneProgetto.Text = this.GeneraTestoRiduzioni(cls.StatoDiProgetto);
        }

        totaleContributo = contrProgetto - contrAttuale;

        this.txtQuotaContributoTotale.ValoreDouble = totaleContributo;

        this.multiView.ActiveViewIndex = 2;

        this.ImpostaScriptCopia(this.cmdCopiaOneri, this.CodiceIstanza, new CCConfigurazioneMgr(this.Database).GetById(this.IdComune, this.Software).FkCoId.GetValueOrDefault(int.MinValue));

        this.cmdCopiaOneri.Visible = this.VerificaVisibilitaBottoneRiporta();
    }

    private string GeneraTestoRiduzioni(CCICalcoloTContributo cCICalcoloTContributo)
    {
        List<CcICalcoloTContributoRiduz> riduzioni = new CcICalcoloTContributoRiduzMgr(this.Database).GetListByIdTContributo(this.IdComune, cCICalcoloTContributo.Id.GetValueOrDefault(int.MinValue));

        if (riduzioni.Count == 0) return cCICalcoloTContributo.Noteriduzione;   // TODO: Verificare che non sia stato immesso un valore manualmente in tal caso ritornare le note del calcolo

        StringBuilder sb = new StringBuilder();

        sb.Append("<table width='100%'>");

        double totale = 0.0d;

        foreach (CcICalcoloTContributoRiduz r in riduzioni)
        {
            sb.Append("<tr style='color:#000'><td>");
            sb.Append(r.CausaleRiduzione.Descrizione);
            sb.Append("</td><td style='text-align:right;'>");
            sb.Append(r.Riduzioneperc.GetValueOrDefault(double.MinValue).ToString("N2") + "%");
            sb.Append("</td></tr>");

            if (!String.IsNullOrEmpty(r.Note))
            {
                sb.Append("<tr><td>&nbsp;&nbsp;<i>");
                sb.Append(r.Note);
                sb.Append("</i></td><td>&nbsp;</td></tr>");
            }

            totale += r.Riduzioneperc.GetValueOrDefault(0);

        }

        sb.Append("<tr style='color:#000'><td>&nbsp;</td><td style='text-align:right;'>-------------</td></tr>");

        sb.Append("<tr style='color:#000'><td>Totale</td><td style='text-align:right;'>");
        sb.Append(totale.ToString("N2"));
        sb.Append("%");
        sb.Append("</td></tr>");

        sb.Append("</table>");

        return sb.ToString();

    }

    private bool VerificaVisibilitaBottoneRiporta()
    {
        CCConfigurazioneMgr mgrConfigurazione = new CCConfigurazioneMgr(this.Database);
        CCConfigurazione configurazione = mgrConfigurazione.GetById(this.AuthenticationInfo.IdComune, this.Istanza.SOFTWARE);

        if (configurazione == null || configurazione.FkCoId.GetValueOrDefault(int.MinValue) == int.MinValue)
            return false;

        return true;
    }

    private void BindInserimento(CCICalcoloTot cls)
    {
        this.txtData.DateValue = DateTime.Now.Date;
        this.txtDescrizione.Text = "";

        this.ddlFkCCVCID.DataSource = new CCValiditaCoefficientiMgr(this.Database).GetList(this.Istanza.IDCOMUNE, this.Istanza.SOFTWARE);
        this.ddlFkCCVCID.DataBind();

        this.SetCoefficienti();

        this.ddlFkCCVCID.SelectedIndex = 0;

        this.ddlFfOCCBTIID.DataSource = new OCCBaseTipoInterventoMgr(this.Database).GetList(null, null);
        this.ddlFfOCCBTIID.DataBind();

        this.ddlFfOCCBTIID.SelectedIndex = 0;

        //ddlFkOCCBDEID.DataSource = new OCCBaseDestinazioniMgr(Database).GetList(null, null);
        this.ddlFkOCCBDEID.DataSource = new CCDestinazioniMgr(this.Database).GetBaseDestinazioniList(this.Istanza.IDCOMUNE);
        this.ddlFkOCCBDEID.DataBind();

        this.ddlFkOCCBDEID.SelectedIndex = 0;

        this.RicalcolaTipiCalcolo(this, EventArgs.Empty);

        this.multiView.ActiveViewIndex = 1;
    }

    protected void RicalcolaTipiCalcolo(object sender, EventArgs e)
    {
        this.ddlFkBCCTCID.DataSource = new CCICalcoloTotMgr(this.Database).GetTipiCalcolo(this.AuthenticationInfo.IdComune, this.Istanza.SOFTWARE, this.ddlFfOCCBTIID.SelectedValue, this.ddlFkOCCBDEID.SelectedValue);
        this.ddlFkBCCTCID.DataBind();
    }

    protected void cmdInserisci_Click(object sender, EventArgs e)
    {
        CCICalcoloTot cls = new CCICalcoloTot();
        cls.Idcomune = this.AuthenticationInfo.IdComune;
        cls.Codiceistanza = this.CodiceIstanza;
        cls.Data = this.txtData.DateValue;
        cls.Descrizione = string.IsNullOrEmpty(this.txtDescrizione.Text) ? this.ddlFkOCCBDEID.SelectedItem.Text + " - " + this.ddlFfOCCBTIID.SelectedItem.Text : this.txtDescrizione.Text;
        cls.FkBcctcId = this.ddlFkBCCTCID.SelectedValue;
        cls.FkCcvcId = int.Parse(this.ddlFkCCVCID.SelectedValue);
        cls.FkOccbdeId = this.ddlFkOCCBDEID.SelectedValue;
        cls.FkOccbtiId = this.ddlFfOCCBTIID.SelectedValue;

        CCICalcoloTotMgr mgr = new CCICalcoloTotMgr(this.Database);

        try
        {
            cls = mgr.Insert(cls);

            // TODO: redirect alla pagina di riepilogo
            this.BindDettaglio(cls);
        }
        catch (Exception ex)
        {
            this.MostraErrore("Errore durante l'inserimento: " + ex.Message, ex);
        }
    }

    protected void multiView_ActiveViewChanged(object sender, EventArgs e)
    {
        switch (this.multiView.ActiveViewIndex)
        {
            case (0):
                this.Master.TabSelezionato = IntestazionePaginaTipiTabEnum.Risultato;
                return;
            case (1):
                this.Master.TabSelezionato = IntestazionePaginaTipiTabEnum.Scheda;
                return;
            case (2):
                this.Master.TabSelezionato = IntestazionePaginaTipiTabEnum.Scheda;
                return;
        }
    }

    protected void cmdChiudiInserimento_Click(object sender, EventArgs e)
    {
        this.DataBind();
    }

    protected void cmdAggiornaDescrizione_Click(object sender, EventArgs e)
    {
        CCICalcoloTotMgr mgr = new CCICalcoloTotMgr(this.Database);
        CCICalcoloTot cls = mgr.GetById(this.AuthenticationInfo.IdComune, Convert.ToInt32(this.lblId.Text));

        cls.Descrizione = this.txtEditDescrizione.Text;

        try
        {
            mgr.Update(cls);
        }
        catch (Exception ex)
        {
            this.MostraErrore("Errore durante l'aggiornamento: " + ex.Message, ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selId = Convert.ToInt32(this.gvLista.DataKeys[this.gvLista.SelectedIndex][0]);

        //CCICalcoloTotMgr mgr = new CCICalcoloTotMgr(Database);

        //CCICalcoloTot cls = mgr.GetById(AuthenticationInfo.IdComune, selId);

        string url = "~/Istanze/CalcoloOneri/CostoCostruzione/CCICalcoliTot.aspx?Token={0}&CodiceIstanza={1}&IdCalcoloTot={2}";

        url = String.Format(url, this.Token, this.CodiceIstanza, selId);

        //BindDettaglio(cls);
        this.Response.Redirect(url);
    }

    protected void cmdSalvaContributo_Click(object sender, EventArgs e)
    {
        int id = int.Parse(this.lblId.Text);
        CCICalcoloTotMgr mgrTot = new CCICalcoloTotMgr(this.Database);
        CCICalcoloTContributoMgr mgrICalc = new CCICalcoloTContributoMgr(this.Database);

        CCICalcoloTot cTot = mgrTot.GetById(this.AuthenticationInfo.IdComune, id);
        mgrTot.IntegraForeignKey(cTot);

        if (cTot.StatoDiProgetto != null)
        {
            cTot.StatoDiProgetto.Coefficiente = this.txtCoefficenteProgetto.ValoreDouble;
            cTot.StatoDiProgetto.CostocEdificio = this.txtCostoEdificioProgetto.ValoreDouble;
            cTot.StatoDiProgetto.Riduzioneperc = String.IsNullOrEmpty(this.txtVariazioneProgetto.Text) ? 0.0d : this.txtVariazioneProgetto.ValoreDouble;
            cTot.StatoDiProgetto = mgrICalc.Update(cTot.StatoDiProgetto);
        }

        if (cTot.StatoAttuale != null)
        {
            cTot.StatoAttuale.Coefficiente = this.txtCoefficenteAttuale.ValoreDouble;
            cTot.StatoAttuale.CostocEdificio = this.txtCostoEdificioAttuale.ValoreDouble;
            cTot.StatoAttuale.Riduzioneperc = String.IsNullOrEmpty(this.txtVariazioneAttuale.Text) ? 0.0d : this.txtVariazioneAttuale.ValoreDouble;
            cTot.StatoAttuale = mgrICalc.Update(cTot.StatoAttuale);
        }

        cTot = mgrTot.GetById(this.AuthenticationInfo.IdComune, id);

        this.BindDettaglio(cTot);

    }

    protected void cmdChiudiDettaglio_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(this.Request.QueryString["IdCalcoloTot"]))
        {
            this.DataBind();
        }

        this.multiView.ActiveViewIndex = 0;

    }

    protected void ApriDettagli(object sender, EventArgs e)
    {
        ImageButton lbSender = (ImageButton)sender;

        int idCalcolo = int.Parse(lbSender.CommandArgument);

        CCConfigurazione cfg = new CCConfigurazioneMgr(this.Database).GetById(this.IdComune, this.Istanza.SOFTWARE);

        string fmtUrl = "";

        // Utilizza l'immissione dei dati nel modello?
        if (cfg.Usadettagliosup == CCConfigurazione.CALCSUP_MODELLO)
            fmtUrl = "~/Istanze/CalcoloOneri/CostoCostruzione/CCITabella1.aspx?Token={0}&IdCalcolo={1}";
        else
            fmtUrl = "~/Istanze/CalcoloOneri/CostoCostruzione/CCICalcoliDettaglio.aspx?Token={0}&IdCalcolo={1}";

        this.Response.Redirect(String.Format(fmtUrl, this.AuthenticationInfo.Token, idCalcolo), true);
    }

    protected void ApriDettagliContributo(object sender, EventArgs e)
    {
        ImageButton lbSender = (ImageButton)sender;

        string[] parts = lbSender.CommandArgument.Split('$');

        int idCalcoloTot = int.Parse(parts[0]);
        string tipoDettaglio = parts[1];

        string fmtUrl = "~/Istanze/CalcoloOneri/CostoCostruzione/CCICalcoloCoeffPercentuale.aspx?Token={0}&IdCalcoloTot={1}&TipoDettaglio={2}";

        this.Response.Redirect(String.Format(fmtUrl, this.AuthenticationInfo.Token, idCalcoloTot, tipoDettaglio), true);


    }

    protected void cmdRiportaValore_Click(object sender, EventArgs e)
    {
        var istanzeOneriMgr = new IstanzeOneriMgr(this.Database, this.AuthenticationInfo);
        var codiceCausale = new CCConfigurazioneMgr(this.Database).GetById(this.IdComune, this.Software).FkCoId;
        var oneriEsistenti = this.GetOneriFromIstanzaCausale(this.CodiceIstanza, codiceCausale.GetValueOrDefault(int.MinValue));
        var importoOnere = this.txtQuotaContributoTotale.ValoreDouble;

        //Verifico se è stato trovato un onere nell'istanza con la stessa causale
        if (oneriEsistenti.Count == 1)
        {
            //E' stato trovato un onere 
            var onere = oneriEsistenti[0];

            var importo = onere.PREZZO.GetValueOrDefault(0.0d) + importoOnere;
            var idComune = onere.IDCOMUNE;
            var idOnere = Convert.ToInt32(onere.ID);

            try
            {
                istanzeOneriMgr.UpdateImporto(idComune, idOnere, importo);
            }
            catch (Exception ex)
            {
                this.MostraErrore(AmbitoErroreEnum.Aggiornamento, ex);
            }
        }
        else
        {
            //Non è stato trovato nessun onere o più di uno
            try
            {
                var oneriService = new OneriService(this.AuthenticationInfo);

                oneriService.Inserisci(this.CodiceIstanza, codiceCausale.Value, importoOnere);
            }
            catch (Exception ex)
            {
                this.MostraErrore(AmbitoErroreEnum.Inserimento, ex);
            }
        }

        this.MostraConfermaCopiaOneri();
        //ImpostaScriptCopia(cmdCopiaOneri, CodiceIstanza, codiceCausale.GetValueOrDefault(int.MinValue));
    }


    protected void txtData_TextChanged(object sender, EventArgs e)
    {
        if (this.txtData.DateValue.HasValue && this.txtData.DateValue.GetValueOrDefault(DateTime.MinValue) != DateTime.MinValue)
        {
            this.SetCoefficienti();
        }
    }

    protected void SetCoefficienti()
    {
        CCValiditaCoefficienti ccvc = new CCValiditaCoefficientiMgr(this.Database).GetCoefficienteAllaData(this.Istanza.IDCOMUNE, this.Istanza.SOFTWARE, this.txtData.DateValue.Value);

        if (ccvc == null) return;

        this.ddlFkCCVCID.SelectedValue = ccvc.Id.ToString();
    }

    protected void cmdChiudi_Click(object sender, EventArgs e)
    {
        base.CloseCurrentPage();
    }
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton cmdElimina = e.Row.FindControl("cmdElimina") as ImageButton;

        if (cmdElimina != null)
            this.ImpostaScriptEliminazione(cmdElimina);
    }
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int selId = Convert.ToInt32(this.gvLista.DataKeys[e.RowIndex][0]);

        CCICalcoloTotMgr mgr = new CCICalcoloTotMgr(this.Database);
        CCICalcoloTot cls = mgr.GetById(this.AuthenticationInfo.IdComune, selId);

        try
        {
            mgr.Delete(cls);

            this.DataBind();
        }
        catch (Exception ex)
        {
            this.MostraErrore(ex);
        }
    }
    protected void cmdEliminaCalcolo_Click(object sender, EventArgs e)
    {
        int selId = Convert.ToInt32(this.lblId.Text);

        CCICalcoloTotMgr mgr = new CCICalcoloTotMgr(this.Database);
        CCICalcoloTot cls = mgr.GetById(this.AuthenticationInfo.IdComune, selId);

        try
        {
            mgr.Delete(cls);
        }
        catch (Exception ex)
        {
            this.MostraErrore(ex);

            return;
        }

        this.DataBind();

        this.multiView.ActiveViewIndex = 0;
    }

    protected void lnkEditVariazione_Click(object sender, ImageClickEventArgs e)
    {
        int id = Convert.ToInt32(this.Request.QueryString["IdCalcoloTot"]);
        this.BindDettaglio(new CCICalcoloTotMgr(this.Database).GetById(this.AuthenticationInfo.IdComune, id));
    }


}
