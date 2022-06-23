using Init.Sigepro.FrontEnd.AppLogic.GestioneLocalizzazioni.Modena;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.localizzazioni_modena
{
    public partial class gestione_localizzazioni_modena : IstanzeStepPage
    {
        [Inject]
        public ILocalizzazioniModenaService _localizzazioniService { get; set; }


        public int IdStradarioDefault
        {
            get { object o = this.ViewState["IdStradarioDefault"]; return o == null ? 0 : (int)o; }
            set { this.ViewState["IdStradarioDefault"] = value; }
        }

        public string CodiceCatastoDefault
        {
            get { object o = this.ViewState["CodiceCatastoDefault"]; return o == null ? String.Empty : (string)o; }
            set { this.ViewState["CodiceCatastoDefault"] = value; }
        }

        public string IdCatastoDefault
        {
            get { object o = this.ViewState["IdCatastoDefault"]; return o == null ? "T" : (string)o; }
            set { this.ViewState["IdCatastoDefault"] = value; }
        }

        public string NomeCatastoDefault
        {
            get { object o = this.ViewState["NomeCatastoDefault"]; return o == null ? "Terreni" : (string)o; }
            set { this.ViewState["NomeCatastoDefault"] = value; }
        }

        public int IdCampoParticelleManualiPresenti
        {
            get { object o = this.ViewState["IdCampoParticelleManualiPresenti"]; return o == null ? -1 : (int)o; }
            set { this.ViewState["IdCampoParticelleManualiPresenti"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.MostraBottoneAvanti = false;

            if (!IsPostBack)
            {
                DataBind();
            }
        }

        public string GetArrayJsPerInizializzazione()
        {
            return this._localizzazioniService.GetStringaArrayParticelleSelezionate(this.IdDomanda, this.CodiceCatastoDefault);
        }

        public override void DataBind()
        {
            //if (ReadFacade.Domanda.Localizzazioni.Indirizzi.Any())
            //{
            //    MostraLista();
            //}
            //else
            //{
            //    MostraSelezione();
            //}

        }

        protected void cmdConferma_Click(object sender, EventArgs e)
        {
            try
            {
                var localizzazioni = LocalizzazioniMappaModenaSelezionate.FromJsonString(hidValoriSelezionati.Value);

                var opzioni = new OpzioniSalvataggioLocalizzazioniModena(this.IdStradarioDefault, this.IdCatastoDefault, this.NomeCatastoDefault, this.IdCampoParticelleManualiPresenti);

                this._localizzazioniService.AggiornaLocalizzazioniModenaDaMappa(this.IdDomanda, localizzazioni, opzioni);
            }
            catch (Exception ex)
            {
                this.Errori.Add(ex.Message);

                return;
            }

            this.Master.cmdNextStep_Click(this, EventArgs.Empty);
        }
    }
}