﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2
{
    public class ParametriSIT : IParametriConfigurazione
    {
        private static class Constants
        {
            public const string WS_SIT = "/WebServices/WsSIGePro/wssit.asmx";
        }

        public readonly string UrlWsSit = "";
        public readonly bool Attivo = false;
        public readonly bool ForzaStepLocalizzazioniSit = false;


        public ParametriSIT(bool attivo, string urlWsSit, bool forzaStepLocalizzazioniSit, string aspNetBaseUrl)
        {
            Attivo = attivo;

            if (!Attivo)
            {
                return;
            }

            UrlWsSit = String.IsNullOrEmpty(urlWsSit) ?
                        (aspNetBaseUrl + Constants.WS_SIT):
                        urlWsSit;

            this.ForzaStepLocalizzazioniSit = forzaStepLocalizzazioniSit;
        }
    }
}
