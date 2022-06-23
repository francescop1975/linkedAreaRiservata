using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;

namespace Init.Sigepro.FrontEnd.AppLogic.Common
{
    public class StaticAuthenticationDataResolver : IAuthenticationDataResolver
    {
        public UserAuthenticationResult DatiAutenticazione { get; set; }

        public bool IsAuthenticated { get; set; }

        public StaticAuthenticationDataResolver(string token = "123-456-789", string idComune = "XXX")
        {
            this.DatiAutenticazione = new UserAuthenticationResult(token, idComune, idComune, new GestioneAnagrafiche.AnagraficaUtente
            {
            }, LivelloAutenticazioneEnum.Identificato);
        }
    }
}
