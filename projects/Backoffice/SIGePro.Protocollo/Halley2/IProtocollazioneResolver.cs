using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Halley2
{
    public interface IProtocollazioneResolver
    {
        string ProtocollazioneUrl { get; }
        string CasellaEmail { get; }
        string UserName { get; }
        string Password { get; }
    }
}
