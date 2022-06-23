using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.Services.Cart;
using Ninject;
using System;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    public partial class DomandaCART : IstanzeStepPage
    {

        [Inject]
        public IFacctRedirectService _facctRedirectService { get; set; }
        [Inject]
        public IConfigurazione<ParametriCart> ConfigurazioneCart { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            Master.IgnoraSalvataggioDati = true;
        }

        public override bool CanEnterStep()
        {
            try
            {
                var codiceComune = ReadFacade.Domanda.AltriDati.CodiceComune;
                var idIntervento = ReadFacade.Domanda.AltriDati.Intervento.Codice;

                var urlCompletoApplicazioneBdr = this._facctRedirectService.GetUrlApplicazioneBdr(IdDomanda, codiceComune, idIntervento);

                if (String.IsNullOrEmpty(urlCompletoApplicazioneBdr))
                    return false;

                Response.Redirect(urlCompletoApplicazioneBdr);

                return true;
            }
            catch (Exception ex)
            {
                Errori.Add(ex.Message);

                Master.MostraBottoneAvanti = false;

                return true;
            }

        }
    }
}