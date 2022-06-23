using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Halley2
{
    public class Halley2ProtocollazioneResolver : IProtocollazioneResolver
    {
        public string ProtocollazioneUrl { get; }
        public string CasellaEmail { get; }
        public string UserName { get; }
        public string Password { get; }

        public Halley2ProtocollazioneResolver( Parametri parametri )
        {
            this.ProtocollazioneUrl = parametri.Url;
            this.CasellaEmail = parametri.CasellaEmail;
            this.UserName = parametri.UserName;
            this.Password = parametri.Password;
        }
    }
}
