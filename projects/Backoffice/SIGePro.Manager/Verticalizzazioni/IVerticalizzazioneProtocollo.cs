using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Verticalizzazioni
{
    //utilizzata per identificare le verticalizzazioni PROTOCOLLO_ATTIVO e PROTOCOLLO_STORICO 
    public interface IVerticalizzazioneProtocollo
    {
        string Nome { get; }
        bool Attiva { get; }
        string Tipoprotocollo { get; }
        string ProxyAddress { get; }
    }
}
