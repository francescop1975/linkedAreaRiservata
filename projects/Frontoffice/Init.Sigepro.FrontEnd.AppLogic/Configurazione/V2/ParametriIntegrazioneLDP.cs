﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2
{
    public class ParametriIntegrazioneLDP : IParametriConfigurazione
    {
        public readonly string UrlCompilazioneDomanda;
        public readonly string UrlDownloadPdf;
        public readonly string UrlServizioDomanda;
        public readonly string UrlRitornoPraticaGIS;
        public readonly string ServiceUsername;
        public readonly string ServicePassword;
        public readonly string UrlPresentazioneIntegrazione;
        

        internal ParametriIntegrazioneLDP(string urlCompilazioneDomanda, string urlServizioDomanda, string serviceUsername, string servicePassword, string urlDownloadPdf, string urlRitornoPraticaGIS, string urlPresentazioneIntegrazione)
        {
            this.UrlDownloadPdf = urlDownloadPdf;
            this.UrlCompilazioneDomanda = urlCompilazioneDomanda;
            this.UrlServizioDomanda = urlServizioDomanda;
            this.UrlRitornoPraticaGIS = urlRitornoPraticaGIS;
            this.UrlPresentazioneIntegrazione = urlPresentazioneIntegrazione;
            this.ServiceUsername = serviceUsername;
            this.ServicePassword = servicePassword;
        }
    }
}
