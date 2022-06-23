using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Manager.Logic.GestioneOneri;
using Init.Utils.Web.UI;
using Sigepro.net.Istanze.CalcoloOneri;
using SIGePro.WebControls.UI;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Istanze_CalcoloOneri_Urbanizzazione_OICalcoloTot : PaginaTotaleOneriBase
{
    private int CodiceIstanza
    {
        get
        {
            string codiceIstanza = this.Request.QueryString["CodiceIstanza"];

            if (String.IsNullOrEmpty(codiceIstanza))
            {
                if (String.IsNullOrEmpty(this.IdCalcoloTot))
                    throw new ArgumentException("Codice istanza e id calcolo tot non passati");

                OICalcoloTot oict = new OICalcoloTotMgr(this.Database).GetById(this.AuthenticationInfo.IdComune, Convert.ToInt32(this.IdCalcoloTot));

                return oict.Codiceistanza.GetValueOrDefault(int.MinValue);
            }

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

    protected string ReturnTo
    {
        get { return this.Request.QueryString["ReturnTo"]; }
    }

    protected string IdCalcoloTot
    {
        get { return this.Request.QueryString["IdCalcoloTot"]; }
    }

    #region gestione della griglia con il riepilogo calcoli

    protected DataSet GridDataSource
    {
        set { this.Session["GridDataSource"] = value; }
        get { return (DataSet)this.Session["GridDataSource"]; }
    }

    protected void BindGrigliaDettaglioCalcoli()
    {
        this.dgDettagliCalcolo.DataSource = this.GridDataSource;
        this.dgDettagliCalcolo.DataBind();
    }

    protected void dgDettagliCalcolo_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        #region Binding dell'intestazione
        if (e.Item.ItemType == ListItemType.Header)
        {
            e.Item.Cells[e.Item.Cells.Count - 1].Text = "&nbsp;";

            OBaseTipiOnereMgr mgrBaseTo = new OBaseTipiOnereMgr(this.Database);

            for (int i = 0; i < e.Item.Cells.Count; i++)
            {
                if (i > 0 && i < e.Item.Cells.Count - 2)
                {
                    string id = e.Item.Cells[i].Text;

                    OBaseTipiOnere bto = mgrBaseTo.GetById(id);

                    if (bto != null)
                        e.Item.Cells[i].Attributes.Add("title", bto.Descrizione);
                }
            }
        }
        #endregion

        #region Binding degli elementi
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;

            if (dr == null) return;

            DataTable table = dr.Row.Table;

            // Aggiungo il bottone Dettagli
            TableCell cell = e.Item.Cells[e.Item.Cells.Count - 1];
            cell.Controls.Clear();

            ImageButton ib = new ImageButton();
            ib.ID = "ibDettagli";
            ib.CommandArgument = dr["Comandi"].ToString();
            ib.CommandName = "Select";
            ib.ImageUrl = "~/images/edit.gif";
            ib.AlternateText = "Visualizza i parametri del calcolo degli oneri di urbanizzazione per la destinazione corrente";

            cell.Controls.Add(ib);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                DataColumn dc = table.Columns[i];

                if (dc.ColumnName != "Destinazione" && dc.ColumnName != "Comandi")
                {
                    e.Item.Cells[i].Text = Convert.ToDouble(e.Item.Cells[i].Text).ToString("N2");
                    e.Item.Cells[i].Style.Add("text-align", "right");
                }
            }
        }
        #endregion

        #region Binding del footer
        if (e.Item.ItemType == ListItemType.Footer)
        {
            if (this.GridDataSource == null || string.IsNullOrEmpty(this.IdCalcoloTot)) return;

            DataTable dt = this.GridDataSource.Tables[0];

            OICalcoloTotMgr ctotMgr = new OICalcoloTotMgr(this.Database);
            OConfigurazioneTipiOnereMgr cfgToMgr = new OConfigurazioneTipiOnereMgr(this.Database);

            string software = ctotMgr.GetSoftwareDaCalcoloTot(this.AuthenticationInfo.IdComune, Convert.ToInt32(this.IdCalcoloTot));

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string columnName = dt.Columns[i].ColumnName;

                int? causale = cfgToMgr.GetCausaleDaTipoOnereBase(this.AuthenticationInfo.IdComune, columnName, software);

                e.Item.Cells[i].Controls.Clear();

                if (columnName != "Destinazione" && columnName != "Comandi")
                {
                    Literal lt = new Literal();
                    lt.ID = "lt" + i;
                    lt.Text = "<span style=\"float:right\"><b>" + this.TotaleColonna(dt, columnName).ToString("N2") + "</b></span>";
                    e.Item.Cells[i].Controls.Add(lt);
                }

                if (causale.HasValue)
                {
                    SigeproButton sbAggiungi = new SigeproButton();
                    sbAggiungi.ID = "ibAggiungi" + i;
                    sbAggiungi.IdRisorsa = "COPIAONERI";
                    sbAggiungi.CommandArgument = causale.Value.ToString() + "$" + this.TotaleColonna(dt, columnName).ToString("N2");
                    sbAggiungi.Text = "Copia oneri";
                    sbAggiungi.Style.Add("float", "left");
                    sbAggiungi.Click += new EventHandler(this.ibAggiungi_Click);

                    Div div = new Div();
                    div.CssClass = "Bottoni";
                    div.Style.Add("padding-top", "0px");
                    div.Controls.Add(sbAggiungi);

                    this.ImpostaScriptCopia(sbAggiungi, this.CodiceIstanza, causale.Value);

                    e.Item.Cells[i].Controls.Add(div);
                }

                e.Item.Cells[i].Style.Add("vertical-align", "top");
                e.Item.Cells[i].Style.Add("border", "0px");
            }
        }
        #endregion
    }

    private void ibAggiungi_Click(object sender, EventArgs e)
    {
        var ibAggiungi = (SigeproButton)sender;

        var parts = ibAggiungi.CommandArgument.Split('$');

        var idCausale = Convert.ToInt32(parts[0]);
        var totaleOnere = Convert.ToDouble(parts[1]);

        var mgrIstanzeOneri = new IstanzeOneriMgr(this.Database, this.AuthenticationInfo);
        var al = this.GetOneriFromIstanzaCausale(this.CodiceIstanza, idCausale);

        //Verifico se è stato trovato un onere nell'istanza con la stessa causale
        if (al.Count == 1)
        {
            //E' stato trovato un onere 
            var onere = al[0];
            var importo = onere.PREZZO.GetValueOrDefault(0.0d) + totaleOnere;
            var idComune = onere.IDCOMUNE;
            var idOnere = Convert.ToInt32(onere.ID);

            try
            {
                mgrIstanzeOneri.UpdateImporto(idComune, idOnere, importo);
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

                oneriService.Inserisci(this.CodiceIstanza, idCausale, totaleOnere);
            }
            catch (Exception ex)
            {
                this.MostraErrore(AmbitoErroreEnum.Inserimento, ex);
            }
        }

        this.MostraConfermaCopiaOneri();
        //ImpostaScriptCopia(ibAggiungi, CodiceIstanza, idCausale);
    }

    private double TotaleColonna(DataTable dt, string columnName)
    {
        double tot = 0.0d;

        foreach (DataRow dr in dt.Rows)
        {
            tot += Convert.ToDouble(dr[columnName]);
        }

        return tot;
    }

    #endregion


    public override string Software
    {
        get
        {
            return this.Istanza.SOFTWARE;
        }
    }

    public Istanze_CalcoloOneri_Urbanizzazione_OICalcoloTot()
    {
        //VerificaSoftware = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ImpostaScriptEliminazione(this.cmdEliminaCalcolo);

        if (!this.IsPostBack)
        {
            if (!String.IsNullOrEmpty(this.IdCalcoloTot))
            {
                int id = Convert.ToInt32(this.IdCalcoloTot);
                this.BindDettaglio(new OICalcoloTotMgr(this.Database).GetById(this.AuthenticationInfo.IdComune, id));
            }
            else
            {
                this.DataBind();
            }
        }
    }

    private void BindDettaglio(OICalcoloTot oICalcoloTot)
    {
        this.IsInserting = oICalcoloTot.Id.GetValueOrDefault(int.MinValue) == int.MinValue;

        if (this.IsInserting)
            this.BindInserimento();
        else
            this.BindModifica(oICalcoloTot);
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.BindGrigliaDettaglioCalcoli();
    }

    private void BindModifica(OICalcoloTot cls)
    {
        this.multiView.ActiveViewIndex = 2;

        this.lblId.Text = cls.Id.ToString();
        this.lblData.Text = cls.Data.GetValueOrDefault(DateTime.MinValue).ToString("dd/MM/yyyy");

        OValiditaCoefficienti ovc = new OValiditaCoefficientiMgr(this.Database).GetById(this.AuthenticationInfo.IdComune, cls.FkOvcId.GetValueOrDefault(int.MinValue));

        this.lblListino.Text = ovc == null ? string.Empty : ovc.Descrizione;

        this.txtEditDescrizione.Text = cls.Descrizione;

        this.GridDataSource = new OICalcoloTotMgr(this.Database).GeneraDataSetOneriUrbanizzazione(this.AuthenticationInfo.IdComune, cls.Id.GetValueOrDefault(int.MinValue));
        this.BindGrigliaDettaglioCalcoli();
    }

    private void BindInserimento()
    {
        this.multiView.ActiveViewIndex = 1;

        this.txtData.DateValue = DateTime.Now;

        this.SetCoefficienti();
    }

    public override void DataBind()
    {
        this.ddlCoefficienti.DataSource = new OValiditaCoefficientiMgr(this.Database).GetList(this.AuthenticationInfo.IdComune, this.Istanza.SOFTWARE, null, null);
        this.ddlCoefficienti.DataBind();

        this.gvLista.DataSource = OICalcoloTotMgr.Find(this.Token, this.CodiceIstanza);
        this.gvLista.DataBind();

    }

    protected void cmdNuovo_Click(object sender, EventArgs e)
    {
        this.BindDettaglio(new OICalcoloTot());
    }
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(this.gvLista.DataKeys[this.gvLista.SelectedIndex][0]);

        string url = "~/Istanze/CalcoloOneri/Urbanizzazione/OICalcoloTot.aspx?Software={0}&Token={1}&CodiceIstanza={2}&IdCalcoloTot={3}";

        this.Response.Redirect(String.Format(url, this.Software, this.AuthenticationInfo.Token, this.CodiceIstanza, id));
    }
    protected void txtData_TextChanged(object sender, EventArgs e)
    {
        if (this.txtData.DateValue.HasValue && this.txtData.DateValue.Value.Date != DateTime.MinValue)
        {
            this.SetCoefficienti();
        }
    }
    protected void SetCoefficienti()
    {
        OValiditaCoefficienti coeff = new OValiditaCoefficientiMgr(this.Database).GetCoefficienteAllaData(this.Istanza.IDCOMUNE, this.Istanza.SOFTWARE, this.txtData.DateValue.Value);
        if (coeff != null)
            this.ddlCoefficienti.SelectedValue = coeff.Id.ToString();
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
    protected void cmdInserisci_Click(object sender, EventArgs e)
    {
        try
        {
            OICalcoloTot cls = new OICalcoloTot();
            cls.Idcomune = this.Istanza.IDCOMUNE;
            cls.Codiceistanza = Convert.ToInt32(this.Istanza.CODICEISTANZA);
            cls.Data = this.txtData.DateValue.HasValue ? this.txtData.DateValue.Value : DateTime.MinValue;
            cls.Descrizione = string.IsNullOrEmpty(this.txtDescrizione.Value) ? "Calcolo degli oneri di urbanizzazione" : this.txtDescrizione.Value;
            cls.FkOvcId = Convert.ToInt32(this.ddlCoefficienti.SelectedValue);

            cls = new OICalcoloTotMgr(this.Database).Insert(cls);

            this.BindDettaglio(cls);
        }
        catch (Exception ex)
        {
            this.MostraErrore(AmbitoErroreEnum.Inserimento, ex);
        }

    }
    protected void cmdChiudiInserimento_Click(object sender, EventArgs e)
    {
        this.multiView.ActiveViewIndex = 0;
    }
    protected void cmdAggiornaDescrizione_Click(object sender, EventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(this.lblId.Text);

            OICalcoloTot cls = new OICalcoloTotMgr(this.Database).GetById(this.AuthenticationInfo.IdComune, id);
            cls.Descrizione = this.txtEditDescrizione.Text;

            cls = new OICalcoloTotMgr(this.Database).Update(cls);

            this.BindDettaglio(cls);
        }
        catch (Exception ex)
        {
            this.MostraErrore(AmbitoErroreEnum.Aggiornamento, ex);
        }
    }
    protected void cmdChiudiDettaglio_Click(object sender, EventArgs e)
    {
        string url = "~/Istanze/CalcoloOneri/Urbanizzazione/OICalcoloTot.aspx?Token={0}&CodiceIstanza={1}";

        int id = Convert.ToInt32(this.lblId.Text);

        OICalcoloTot cTot = new OICalcoloTotMgr(this.Database).GetById(this.AuthenticationInfo.IdComune, id);

        this.Response.Redirect(string.Format(url, this.AuthenticationInfo.Token, cTot.Codiceistanza));
    }
    protected void cmdRedirDettagli_Click(object sender, EventArgs e)
    {
        string url = "~/Istanze/CalcoloOneri/Urbanizzazione/OIDettaglio.aspx?Token={0}&IdCalcolo={1}";

        int id = Convert.ToInt32(this.lblId.Text);

        this.Response.Redirect(string.Format(url, this.AuthenticationInfo.Token, id));
    }

    protected void dgDettagliCalcolo_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            string url = "~/Istanze/CalcoloOneri/Urbanizzazione/OICalcoloContribT.aspx?Token={0}&IdContribT={1}";

            this.Response.Redirect(string.Format(url, this.AuthenticationInfo.Token, id));
        }
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

        OICalcoloTotMgr mgr = new OICalcoloTotMgr(this.Database);
        OICalcoloTot cls = mgr.GetById(this.AuthenticationInfo.IdComune, selId);

        mgr.Delete(cls);

        this.DataBind();
    }
    protected void cmdEliminaCalcolo_Click(object sender, EventArgs e)
    {
        int selId = Convert.ToInt32(this.lblId.Text);

        OICalcoloTotMgr mgr = new OICalcoloTotMgr(this.Database);
        OICalcoloTot cls = mgr.GetById(this.AuthenticationInfo.IdComune, selId);

        string url = "OICalcoloTot.aspx?Software=" + this.Software + "&Token=" + this.Token + "&CodiceIstanza=" + cls.Codiceistanza + "&ReturnTo=" + this.ReturnTo;

        mgr.Delete(cls);

        this.Response.Redirect(url);
    }
}
