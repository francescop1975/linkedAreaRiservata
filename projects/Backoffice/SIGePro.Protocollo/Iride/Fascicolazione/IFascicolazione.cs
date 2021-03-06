using Init.SIGePro.Protocollo.Iride.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.Data;

namespace Init.SIGePro.Protocollo.Iride.Fascicolazione
{
    public interface IFascicolazione
    {
        FascicoloOut LeggiFascicolo(string numero, string anno, string classifica, int id, string utente, string ruolo, string codiceAmministrazione, string codiceAoo);
        FascicolazioneProxy Proxy { get; }
    }
}
