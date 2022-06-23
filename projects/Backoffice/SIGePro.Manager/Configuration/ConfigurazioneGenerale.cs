using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Manager.WsSigeproSecurity;
using System.Configuration;

namespace Init.SIGePro.Manager.Configuration
{
    public class ConfigurazioneGenerale : HandlerConfigurazione
    {
        private static class Constants
        {
            public const string AnagrafeServiceUrlPart = "services/anagrafe";
            public const string OggettiServiceUrlPart = "services/oggetti";
            public const string RateizzazioneServiceUrlPart = "services/rateizzazioni";
            public const string MercatiServiceUrlPart = "services/mercati";
            public const string EventiAttivitaServiceUrlPart = "services/attivita";
            public const string IstanzeOneriServiceUrlPart = "services/istanzeoneri";
            public const string IstanzeServiceUrlPart = "services/istanze";
            public const string IstanzeStradarioJsonUrlPart = "/stradario";
            public const string IstanzeJsonUrlPart = "services/rest-private/istanze";
            public const string PosizioniDebitorieServiceUrlPart = "services/posizionidebitorie";
            // public const string ContiServiceUrlPart = "";
        }

        public string WsHostUrlJava { get; protected set; }
        public string WsHostUrlAspNet { get; protected set; }
        public string WsHostUrlAspExport { get; protected set; }
        public string WsHostUrlFirmaDigitale { get; protected set; }
        public string WsHostUrlFileConverter { get; protected set; }
        public string WsHostUrlRender { get; protected set; }
        public string WsHostUrlNlaPec { get; protected set; }
        public string WsHostUrlApiBackend { get; protected set; }
        public string WsAnagrafeServiceUrl => this.WsHostUrlJava + Constants.AnagrafeServiceUrlPart;
        public string WsOggettiServiceUrl => this.WsHostUrlJava + Constants.OggettiServiceUrlPart;
        public string WsRateizzazioniServiceUrl  => this.WsHostUrlJava + Constants.RateizzazioneServiceUrlPart;
        public string WsMercatiServiceUrl  => this.WsHostUrlJava + Constants.MercatiServiceUrlPart;
        public string WsEventiAttivitaServiceUrl  => this.WsHostUrlJava + Constants.EventiAttivitaServiceUrlPart;
        public string WsIstanzeOneriServiceUrl  => this.WsHostUrlJava + Constants.IstanzeOneriServiceUrlPart;
        public string WsIstanzeServiceUrl  => this.WsHostUrlJava + Constants.IstanzeServiceUrlPart;
        public string RestIstanzeJsonUrl => this.WsHostUrlJava + Constants.IstanzeJsonUrlPart;
        public string RestIstanzeStradarioJsonUrl => this.RestIstanzeJsonUrl + Constants.IstanzeStradarioJsonUrlPart;

        public string WsPosizioniDebitorieServiceUrl => this.WsHostUrlJava + Constants.PosizioniDebitorieServiceUrlPart;
        //public string WsContiServiceUrl => this.WsHostUrlJava + Constants.ContiServiceUrlPart;


        public string BaseUrl { get; protected set; }
        public string AppJava { get; protected set; }
        public string AppAsp { get; protected set; }
        public string AppAspNet { get; protected set; }
        public string AuthenticationGatewayUrl { get; protected set; }
        public string AuditServiceUrl { get; protected set; }
        public int TokenTimeout { get; protected set; }

        internal ConfigurazioneGenerale(ApplicationInfoType[] parametri) : base(parametri)
        {
            WsHostUrlAspExport = GetParam("WSHOSTURL_EXPORT");
            WsHostUrlAspNet = GetParam("WSHOSTURL_ASPNET");
            WsHostUrlFileConverter = GetParam("WSHOSTURL_FILECONVERTER");
            WsHostUrlFirmaDigitale = GetParam("WSHOSTURL_FIRMADIGITALE");
            WsHostUrlJava = GetParam("WSHOSTURL_JAVA");
            WsHostUrlRender = GetParam("WSHOSTURL_RENDER");
            WsHostUrlNlaPec = GetParam("WSHOSTURL_NLAPEC");
            WsHostUrlApiBackend = GetParam("WSHOSTURL_APIBACKEND");

            BaseUrl = GetParam("BASE_URL");

            AppAsp = GetParam("APP_ASP");
            AppAspNet = GetParam("APP_ASPNET");
            AppJava = GetParam("APP_JAVA");

            AuthenticationGatewayUrl = GetParam("AUTHENTICATION_GATEWAY_URL");
            AuditServiceUrl = GetParam("AUDIT_SERVICE_URL");

            if (!WsHostUrlJava.EndsWith("/"))
                WsHostUrlJava += "/";

            //JavaServiceUrl = WsHostUrlJava + "services/";

            var overrideJavaBaseUrl = ConfigurationManager.AppSettings["override-java-base-url"];

            if (!String.IsNullOrEmpty(overrideJavaBaseUrl))
                this.WsHostUrlJava = overrideJavaBaseUrl;

            var tokenTimeout = GetParam("CHECK_TOKEN_TIMEOUT");

            this.TokenTimeout = String.IsNullOrEmpty(tokenTimeout) ? 0 : Convert.ToInt32(tokenTimeout);
        }




    }
}
