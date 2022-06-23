using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GestionePercorsiGoogleMaps;
using Ninject;
using System;
using System.Linq;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    public partial class gestione_percorsi_google_maps : IstanzeStepPage
    {


        [Inject]
        protected IConfigurazione<ParametriGoogleMaps> _config { get; set; }


        [Inject]
        public PercorsiGoogleMapsService _googleMapsService { get; set; }


        // TODO: Leggere i parametri da una verticalizzazione
        protected string MapsApiKey => this._config.Parametri.ApiKey;
        protected string LimitiMappa => String.IsNullOrEmpty(this._config.Parametri.MapBounds) ? String.Empty :
                            $"mapBounds: {this._config.Parametri.MapBounds},";
        protected bool PercorsoCalcolato => !String.IsNullOrEmpty(this.hidContenitorePercorso.Value);

        public int IdCampoNomeVia
        {
            get { object o = this.ViewState["IdCampoNomeVia"]; return o == null ? -1 : (int)o; }
            set { this.ViewState["IdCampoNomeVia"] = value; }
        }

        public int IdCampoMetriPercorsi
        {
            get { object o = this.ViewState["IdCampoMetriPercorsi"]; return o == null ? -1 : (int)o; }
            set { this.ViewState["IdCampoMetriPercorsi"] = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public override bool CanExitStep()
        {
            if (!this.PercorsoCalcolato)
            {
                this.Errori.Add("Utilizzare la mappa per individuare un percorso");

                return false;
            }


            DatoStepGoogleMaps[] steps = new DatoStepGoogleMaps[0];

            if (!String.IsNullOrEmpty(this.hidContenitoreSteps.Value))
            {
                var parts = this.hidContenitoreSteps.Value.Split('\n');
                steps = parts.Where(x => !String.IsNullOrEmpty(x)).Select(x => new DatoStepGoogleMaps(double.Parse(x.Split('@')[0]), Server.UrlDecode(x.Split('@')[1]))).ToArray();
            }

            var mappaturaCampi = new MappaturaCampiDinamiciGoogleMaps
            {
                IdCampoMetriPercorsi = this.IdCampoMetriPercorsi,
                IdCampoNomeVia = this.IdCampoNomeVia
            };

            this._googleMapsService.SalvaDatiDomanda(this.IdDomanda, this.hidContenitorePercorso.Value, steps, mappaturaCampi);

            return true;
        }

        public override void OnInitializeStep()
        {
            this.hidContenitorePercorso.Value = this._googleMapsService.GetBase64Percorso(this.IdDomanda);
        }
    }
}