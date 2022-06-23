using Init.SIGePro.Manager.Logic.GestioneSchedeAttivita.Eventi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vbg.EventBus;

namespace Init.SIGePro.Manager.Logic.GestioneSchedeAttivita
{
    public class SchedeAttivitaEventBusModule : EventBusModule
    {
        public override void Load()
        {
            this.EventPublisher.Add<SchedaDinamicaAggiuntaAdAttivita, EventiSchedeDinamicheAttivitaService>();
            this.EventPublisher.Add<SchedaDinamicaAttivitaSalvata, EventiSchedeDinamicheAttivitaService>();
            this.EventPublisher.Add<SchedaDinamicaIstanzaEliminata, EventiSchedeDinamicheAttivitaService>();
            this.EventPublisher.Add<SchedaDinamicaIstanzaSalvata, EventiSchedeDinamicheAttivitaService>();
            this.EventPublisher.Add<SchedaDinamicaRimossaDaAttivita, EventiSchedeDinamicheAttivitaService>();
        }
    }
}
