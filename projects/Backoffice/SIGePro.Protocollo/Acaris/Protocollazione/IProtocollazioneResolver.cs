using Init.SIGePro.Protocollo.Acaris.Entity;
using Init.SIGePro.Protocollo.AcarisOfficialBookServicePort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Protocollazione
{
    public interface IProtocollazioneResolver : IRepositoryIdResolver, IPrincipalIdResolver
    {
        enumTipoRegistrazioneDaCreare TipologiaCreazione { get; }
        RegistrazioneRequest InfoRichiestaCreazione { get; }
        string OfficialBookPortUrl { get; }
        string ObjectPortUrl { get; }

        IdAoo IdAoo { get; }
        IdStruttura IdStruttura { get; }
        IdNodo IdNodo { get; }
    }
}
