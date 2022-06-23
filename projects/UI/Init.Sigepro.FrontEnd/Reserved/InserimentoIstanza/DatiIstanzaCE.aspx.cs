using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    public partial class DatiIstanzaCE : IstanzeStepPage
    {
        [Inject]
        public DatiDomandaService DatiDomandaService { get; set; }

        public bool OggettoEditabile
        {
            get { object o = this.ViewState["OggettoEditabile"]; return o == null ? true : (bool)o; }
            set
            {
                this.ViewState["OggettoEditabile"] = value;

                this.Oggetto.ReadOnly = !value;
                this.Oggetto.Required = value;
            }
        }

        public int LimiteCaratteri
        {
            get { object o = this.ViewState["LimiteCaratteri"]; return o == null ? 2000 : (int)o; }
            set { this.ViewState["LimiteCaratteri"] = value; }
        }

        public bool NascondiNote
        {
            get { return !this.Note.Visible; }
            set { this.Note.Visible = !value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            Master.IgnoraSalvataggioDati = true;

            if (!IsPostBack)
                DataBind();
        }

        public override void OnBeforeExitStep()
        {
            var note = this.NascondiNote ? String.Empty : Note.Text;

            DatiDomandaService.ImpostaDatiIstanza(IdDomanda, note, Oggetto.Text, String.Empty);
        }

        public override bool CanExitStep()
        {
            if (String.IsNullOrEmpty(ReadFacade.Domanda.AltriDati.DescrizioneLavori))
            {
                Errori.Add("Compilare tutti i campi obbligatori");
                return false;
            }

            if (LimiteCaratteri > 0 && ReadFacade.Domanda.AltriDati.DescrizioneLavori.Length > LimiteCaratteri)
            {
                Errori.Add("La lunghezza del testo del campo Oggetto non può superare " + LimiteCaratteri + " caratteri");
                return false;
            }

            return true;
        }

        public override void DataBind()
        {
            Note.Text = ReadFacade.Domanda.AltriDati.Note;
            Oggetto.Text = ReadFacade.Domanda.AltriDati.DescrizioneLavori;

            Oggetto.HelpText = String.Empty;

            if (LimiteCaratteri > 0)
            {
                Oggetto.HelpText = $"<div id='caratteri-rimanenti'><span></span> / {LimiteCaratteri}</div>";
            }
        }
    }
}
