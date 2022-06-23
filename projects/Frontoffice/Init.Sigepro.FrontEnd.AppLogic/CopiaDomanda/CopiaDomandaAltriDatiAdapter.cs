using Init.Sigepro.FrontEnd.AppLogic.GestioneInterventi;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.PresentazioneIstanze.Workflow;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.CopiaDomanda
{
    public class CopiaDomandaAltriDatiAdapter: ICopiaDomandaDatiAdapter
    {
        public IResolveDescrizioneIntervento _resolveDescrizioneIntervento { get; set; }

        public CopiaDomandaAltriDatiAdapter(IResolveDescrizioneIntervento resolveDescrizioneIntervento)
        {
            this._resolveDescrizioneIntervento = resolveDescrizioneIntervento;
        }


        public void Adatta( Istanze istanzaTemplate, DomandaOnline domanda )
        {
            // CodiceComune
            domanda.WriteInterface.AltriDati.ImpostaCodiceComune(istanzaTemplate.CODICECOMUNE);
            domanda.WriteInterface.AltriDati.ImpostaDomicilioElettronico(istanzaTemplate.DOMICILIO_ELETTRONICO);

            var idIntervento = Convert.ToInt32(istanzaTemplate.CODICEINTERVENTOPROC);
            domanda.WriteInterface.AltriDati.ImpostaIntervento(idIntervento, (int?)null, new NullWorkflowService(), this._resolveDescrizioneIntervento);
        }
    }
}
