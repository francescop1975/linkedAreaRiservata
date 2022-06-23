using Init.SIGePro.Manager.Events;
using Init.SIGePro.Manager.Logic.GestioneIntegrazioneLDP.EventHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vbg.EventBus;

namespace Init.SIGePro.Manager.IOC
{
    public class SigeproManagaerEventBusModule : EventBusModule
    {
        public override void Load()
        {
            this.EventPublisher.Add<DomandaFOInCancellazioneEvent, GestioneIntegrazioneLDPDomandaFOEventHandler>();
        }
    }
}
