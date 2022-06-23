using Init.SIGePro.Manager.Verticalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Halley2
{
    public class Parametri
    {
        public string Url { get; internal set; }
        public string CasellaEmail { get; internal set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public static Parametri FromVerticalizzazione(VerticalizzazioneProtocolloHalley2 verticalizzazione) 
        {
            if (verticalizzazione is null)
            {
                throw new ArgumentNullException(nameof(verticalizzazione));
            }

            return new Parametri
            {
                Url = verticalizzazione.Url,
                CasellaEmail = verticalizzazione.CasellaEmail,
                UserName = verticalizzazione.UserName,
                Password = verticalizzazione.Password
            };
        }

 
    }
}
