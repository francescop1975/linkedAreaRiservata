using Init.SIGePro.Protocollo.AcarisOfficialBookServicePort;
using Init.SIGePro.Protocollo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Protocollazione.Registrazioni
{
    public interface IRegistrazioneAPIResolver
    {
        RegistrazioneAPI ResolveRegistrazione();
        string ClassificazioneID { get; }
    }
}
