using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Init.SIGePro.Data;
using System.ComponentModel;
using SIGePro.Net;
using Init.SIGePro.Manager;
using System.Collections.Generic;

namespace Sigepro.net.Archivi.Mercati
{
    public partial class Posteggio : System.Web.UI.UserControl
    {

        protected BasePage BasePage
        {
            get { return (BasePage)this.Page; }
        }

        private Mercati_D mercatid = null;
        private int? codiceuso = null;

        /// <summary>
        /// Classe Init.Sigepro.Data.Mercati_D
        /// </summary>
        [Bindable(true)]
        public Mercati_D MercatiD
        {
            get { return mercatid; }
            set { mercatid = value; }
        }

        /// <summary>
        /// MERCATI_USO.ID
        /// </summary>
        public int? CodiceUso
        {
            get { return codiceuso; }
            set { codiceuso = value; }
        }

        public override void DataBind()
        {
            base.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (MercatiD == null) return;


            this.lblPosteggio.Text = "<font class='output'>" + MercatiD.CodicePosteggio + "</font>";
            if (!string.IsNullOrEmpty(MercatiD.FkCodiceTipoSpazio))
            {
                PosteggiTipoSpazio pts = new PosteggiTipoSpazioMgr(BasePage.Database).GetById(MercatiD.FkCodiceTipoSpazio, MercatiD.IdComune);
                this.lblPosteggio.Text += " - " + pts.TIPOSPAZIO;
            }

            if (MercatiD.Superficie.HasValue && MercatiD.Superficie.Value > double.MinValue)
            {
                this.lblPosteggio.Text += " (";

                if ((MercatiD.Larghezza.HasValue && MercatiD.Larghezza.Value > double.MinValue) && (MercatiD.Lunghezza.HasValue && MercatiD.Lunghezza.Value > double.MinValue))
                {
                    this.lblPosteggio.Text += MercatiD.Larghezza.Value.ToString("N2") + "x" + MercatiD.Lunghezza.Value.ToString("N2") + "=";
                }

                this.lblPosteggio.Text += MercatiD.Superficie.Value.ToString("N2") + ")";
            }

            this.imNote.AlternateText = MercatiD.Note;
            this.imNote.Visible = (!String.IsNullOrEmpty(MercatiD.Note));

            BindMerceologie();

            BindGrid();

            base.OnPreRender(e);
        }

        private void BindMerceologie()
        {
            Mercati_DAttivitaIstat m = new Mercati_DAttivitaIstat();
            m.FKCODICEMERCATO = MercatiD.FkCodiceMercato;
            m.FKIDPOSTEGGIO = MercatiD.IdPosteggio;
            m.IDCOMUNE = MercatiD.IdComune;

            List<Mercati_DAttivitaIstat> merceologie = new Mercati_DAttivitaIstatMgr(BasePage.Database).GetList(m);

            foreach (Mercati_DAttivitaIstat att in merceologie)
            {
                this.imMerceologie.AlternateText += att.Attivita.CodiceIstat + " - " + att.Attivita.ISTAT;
                this.imMerceologie.AlternateText += (att.Flag_Consentito == "0") ? " non consentito" : " consentito";
                this.imMerceologie.AlternateText += "\n";
            }

            this.imMerceologie.Visible = (!String.IsNullOrEmpty(this.imMerceologie.AlternateText));
        }

        private void BindGrid()
        {
            Mercati_DMgr mgr = new Mercati_DMgr(BasePage.Database);

            this.gvOccupanti.DataSource = mgr.GetOccupanti(BasePage.IdComune, MercatiD.FkCodiceMercato.GetValueOrDefault(int.MinValue), MercatiD.IdPosteggio.GetValueOrDefault(int.MinValue), CodiceUso.GetValueOrDefault(int.MinValue));
            this.gvOccupanti.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}