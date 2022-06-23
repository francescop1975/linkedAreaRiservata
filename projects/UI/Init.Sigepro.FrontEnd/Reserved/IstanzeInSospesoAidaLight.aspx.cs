﻿using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace Init.Sigepro.FrontEnd.Reserved
{
    public partial class IstanzeInSospesoAidaLight : ReservedBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var qs = GetAuthenticatedQuerystring();
                var url = GetUrlPresentazioneDomanda();

                Response.Redirect(String.Format("{0}?{1}", url, qs));
            }
        }


        private string GetUrlPresentazioneDomanda()
        {
            var url = ConfigurationManager.AppSettings["aidaLight.inCompilazione"];

            if (String.IsNullOrEmpty(url))
            {
                throw new Exception("Url di AIDA light non definito");
            }

            return url;

        }

        private string GetAuthenticatedQuerystring()
        {
            var tokenServiceUrl = ConfigurationManager.AppSettings["aidaLight.tokenServiceUrl"];

            if (String.IsNullOrEmpty(tokenServiceUrl))
            {
                throw new Exception("Url del token service non definito");
            }

            var querystringFmtString = "?cf={0}&nome={1}&cognome={2}&sesso={3}&time={4}";
            var uar = UserAuthenticationResult.DatiUtente;
            var qs = String.Format(querystringFmtString, uar.Codicefiscale, uar.Nome, uar.Nominativo, uar.Sesso, DateTime.Now.ToString("yyyyMMddHHmmssffff"));

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var request = (HttpWebRequest)WebRequest.Create(tokenServiceUrl + qs);
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse(); ;
            var stream = response.GetResponseStream();
            var readStream = new StreamReader(stream, Encoding.UTF8);

            return readStream.ReadToEnd();
        }

    }
}