using Init.SIGePro.Manager.Logic.GestioneAntenneLucca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sigepro.net.Antenne
{
    public partial class elemento_lista_antenne : System.Web.UI.UserControl
    {
        public PraticaPerAntenna Pratica { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override void DataBind()
        {
            if (this.Pratica == null)
            {
                this.Visible = false;
                return;
            }

            this.Visible = true;

            this.ltrNumeroPratica.Text = $"Pratica {this.Pratica.NumeroIstanza} del {this.Pratica.DataIstanza.ToString("dd/MM/yyyy")}";
            this.ltrNumeroProtocollo.Text = $"Protocollo {this.Pratica.NumeroProtocollo} del {this.Pratica.DataProtocollo?.ToString("dd/MM/yyyy")}";

            if (String.IsNullOrEmpty(this.Pratica.NumeroProtocollo))
            {
                this.ltrNumeroProtocollo.Visible = false;
            }

            this.txtRichiedente.Text = this.Pratica.Richiedente;
            this.txtIntervento.Text = this.Pratica.Intervento;

            this.allegatiIntervento.Allegati = this.Pratica.AllegatiIntervento;
            this.allegatiIntervento.DataBind();

            this.allegatiEndoprocedimento.Allegati = this.Pratica.AllegatiEndoprocedimenti;
            this.allegatiEndoprocedimento.DataBind();

            this.allegatiMovimenti.Allegati = this.Pratica.AllegatiMovimenti;
            this.allegatiMovimenti.DataBind();
        }
    }
}