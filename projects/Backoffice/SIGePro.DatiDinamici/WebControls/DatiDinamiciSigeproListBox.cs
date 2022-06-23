using Init.SIGePro.DatiDinamici.Statistiche;
using System;
using System.Data;
using System.Web.UI;

namespace Init.SIGePro.DatiDinamici.WebControls
{
    [ControlValueProperty("Valore")]
    public partial class DatiDinamiciSigeproListBox : DatiDinamiciListBox
    {
        public static new TipoConfrontoFiltroEnum[] GetTipiConfrontoSupportati()
        {
            return new TipoConfrontoFiltroEnum[] {
                            TipoConfrontoFiltroEnum.Equal,
                            TipoConfrontoFiltroEnum.NotEqual,
                            TipoConfrontoFiltroEnum.Null,
                            TipoConfrontoFiltroEnum.NotNull };
        }

        /// <summary>
        /// Ritorna la lista di proprieta valorizzabili tramite la pagina di editing dei campi
        /// </summary>
        /// <returns>lista di proprieta valorizzabili tramite la pagina di editing dei campi</returns>
        public static new ProprietaDesigner[] GetProprietaDesigner()
        {
            return new ProprietaDesigner[]{
                        new ProprietaDesigner("Obbligatorio","Obbligatorio",TipoControlloEditEnum.ListBox,"No=false,Si=true","false"),
                        new ProprietaDesigner("IgnoraObbligatorietaSuAttivita","Ignora obbligatorietà su schede attività",TipoControlloEditEnum.ListBox,"No=false,Si=true","false"),
                        new ProprietaDesigner("CampiSelect", "Campi della select",""),
                        new ProprietaDesigner("TabelleSelect", "Tabelle della select",""),
                        new ProprietaDesigner("CondizioneJoin", "Condizioni di join",""),
                        new ProprietaDesigner("CondizioniWhere", "Condizioni where",""),
                        new ProprietaDesigner("NomeCampoValore", "Nome campo valore",""),
                        new ProprietaDesigner("NomeCampoTesto", "Nome campo testo","")};
        }

        #region gestione della query di selezione
        public string CampiSelect
        {
            get { object o = ViewState["CampiSelect"]; return o == null ? String.Empty : o.ToString(); }
            set { ViewState["CampiSelect"] = value; }
        }

        public string TabelleSelect
        {
            get { object o = ViewState["TabelleSelect"]; return o == null ? String.Empty : (string)o; }
            set { ViewState["TabelleSelect"] = value; }
        }

        public string CondizioneJoin
        {
            get { object o = ViewState["CondizioneJoin"]; return o == null ? String.Empty : (string)o; }
            set { ViewState["CondizioneJoin"] = value; }
        }

        public string CondizioniWhere
        {
            get { object o = ViewState["CondizioniWhere"]; return o == null ? String.Empty : (string)o; }
            set { ViewState["CondizioniWhere"] = value; }
        }

        public string NomeCampoValore
        {
            get { return InnerControl.DataValueField; }
            set { InnerControl.DataValueField = value; }
        }

        public string NomeCampoTesto
        {
            get { return InnerControl.DataTextField; }
            set { InnerControl.DataTextField = value; }
        }
        #endregion

        // Override solo per evitare che compaia nella lista delle proprietà
        public override string ElementiLista
        {
            get
            {
                return base.ElementiLista;
            }
            set
            {
                base.ElementiLista = value;
            }
        }

        public override string Valore
        {/*
			get { object o = this.ViewState["Valore"]; return o == null ? String.Empty : (string)o; }
			set { this.ViewState["Valore"] = value; }*/
            get { return InnerControl.SelectedValue; }
            set { InnerControl.SelectedValue = value; }
        }


        internal DatiDinamiciSigeproListBox()
        {
            InnerControl.SelectedIndexChanged += new EventHandler(InnerControl_SelectedIndexChanged);
            RichiedeNotificaSuModificaValoreDecodificato = true;
        }

        public DatiDinamiciSigeproListBox(CampoDinamicoBase campo)
            : base(campo)
        {
            InnerControl.SelectedIndexChanged += new EventHandler(InnerControl_SelectedIndexChanged);
            RichiedeNotificaSuModificaValoreDecodificato = true;
        }

        void InnerControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Valore = InnerControl.SelectedValue;
        }

        public override void DataBind()
        {
            var mgr = DataAccessFactory.GetDyn2QueryDatiDinamiciManager();
            DataSet ds = mgr.EseguiQuery(IdComune, CampiSelect, TabelleSelect, CondizioneJoin, CondizioniWhere, NomeCampoTesto, NomeCampoValore);

            DataRow dr = ds.Tables[0].NewRow();
            dr[NomeCampoTesto] = String.Empty;
            dr[NomeCampoValore] = String.Empty;

            ds.Tables[0].Rows.InsertAt(dr, 0);

            InnerControl.DataSource = ds.Tables[0];
            InnerControl.DataBind();
        }

    }
}
