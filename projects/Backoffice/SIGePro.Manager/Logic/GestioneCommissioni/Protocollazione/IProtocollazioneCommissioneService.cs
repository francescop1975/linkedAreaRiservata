using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneCommissioni.Protocollazione
{
    public interface IProtocollazioneCommissioneService
    {
        EsitoProtocollazioneCommissione ProtocollaParereEsterno(int codiceIstanza, IRisolviMittenteProtocolloCommissione mittente, IRisolviOggettoProtocolloCommissione oggetto, IRisolviAllegatiProtocolloCommissione allegati);
    }
}
