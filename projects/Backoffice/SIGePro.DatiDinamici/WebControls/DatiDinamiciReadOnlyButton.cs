using Init.SIGePro.DatiDinamici.Interfaces.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Init.SIGePro.DatiDinamici.WebControls
{
    public class DatiDinamiciReadOnlyButton : WebControl, IDatiDinamiciControl
    {
        private Literal _testoDescrizione { get; set; }
        private HiddenField _hidden;

        public int IdCampoCollegato
        {
            get { object o = ViewState["IdCampoCollegato"]; return o == null ? -1 : (int)o; }
            private set { ViewState["IdCampoCollegato"] = value; }
        }

        public string Descrizione
        {
            get { return _testoDescrizione.Text; }
            private set { _testoDescrizione.Text = value; }
        }
        public int Indice { get; set; }
        public int NumeroRiga { get; set; }

        public string IdComune
        {
            get { return HttpContext.Current.Items["IdComune"].ToString(); }
        }
        public string Software
        {
            get { return HttpContext.Current.Items["Software"].ToString(); }
        }

        public string Valore { get => _hidden.Value; set => _hidden.Value = value; }

        public string Note { get; set; }

        public DatiDinamiciReadOnlyButton(CampoDinamicoBase campo) : this()
        {
            IdCampoCollegato = campo.Id;
            Descrizione = campo.Descrizione;
        }

        protected DatiDinamiciReadOnlyButton()
        {
            this._hidden = new HiddenField();
            this._testoDescrizione = new Literal();

            this.Controls.Add(this._hidden);
            this.Controls.Add(this._testoDescrizione);
            
        }
    }
}
