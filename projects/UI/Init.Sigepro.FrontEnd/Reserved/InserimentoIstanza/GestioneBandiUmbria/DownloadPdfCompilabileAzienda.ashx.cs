﻿using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.GestioneBandiUmbria.CustomIstanzaStcAdapters;
using Init.Sigepro.FrontEnd.AppLogic.GestioneBandiUmbria.Paths;
using Init.Sigepro.FrontEnd.AppLogic.PrecompilazionePDF;
using Init.Sigepro.FrontEnd.AppLogic.Services.Navigation;
using log4net;
using Ninject;
using Ninject.Web;
using System;
using System.Web;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.GestioneBandiUmbria
{
    /// <summary>
    /// Summary description for DownloadPdfCompilabileAzienda
    /// </summary>
    public class DownloadPdfCompilabileAzienda : HttpHandlerBase, IHttpHandler
    {
        private static class QuerystringConstants
        {
            public const string UserAuthenticationResult = "UserAuthenticationResult";
            public const string CodiceOggetto = "codiceOggetto";
            public const string IdDomanda = "idDomanda";
        }

        [Inject]
        protected IPdfUtilsService _pdfUtilsService { get; set; }

        [Inject]
        protected IIstanzaStcAdapter _istanzaStcAdapter { get; set; }

        ILog _log = LogManager.GetLogger(typeof(DownloadPdfCompilabile));
        HttpContext _context;

        public UserAuthenticationResult UserAuthenticationResult
        {
            get { return HttpContext.Current.Items[QuerystringConstants.UserAuthenticationResult] as UserAuthenticationResult; }
        }

        protected int CodiceOggetto
        {
            get { return Convert.ToInt32(this._context.Request.QueryString[PathUtils.UrlParameters.CodiceOggetto]); }
        }

        protected int IdDomanda
        {
            get { return Convert.ToInt32(this._context.Request.QueryString[PathUtils.UrlParameters.IdPresentazione]); }
        }

        protected string CodiceFiscaleAzienda
        {
            get { return this._context.Request.QueryString[GestioneBandiPath.Constants.CodiceFiscaleAzienda]; }
        }

        protected string PartitaIvaAzienda
        {
            get { return this._context.Request.QueryString[GestioneBandiPath.Constants.PartitaIvaAzienda]; }
        }

        protected override void DoProcessRequest(HttpContext context)
        {
            try
            {
                this._context = context;

                var adapter = new GestioneBandiIstanzaStcAdapter(this.PartitaIvaAzienda, this.CodiceFiscaleAzienda, this._istanzaStcAdapter);

                var file = _pdfUtilsService.PrecompilaPdf(CodiceOggetto, IdDomanda, adapter);

                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + file.FileName + "\"");
                context.Response.ContentType = file.MimeType;
                context.Response.BinaryWrite(file.FileContent);
            }
            catch (Exception ex)
            {
                _log.Error($"Errore durante la precompilazione di un modello pdf: codiceoggetto={CodiceOggetto}, iddomanda={IdDomanda}, errore={ex.ToString()}");

                context.Response.ContentType = "text/plain";
                context.Response.Write("Si è verificato un errore durante la preparazione del file. Contattare l'assistenza");
            }
        }

        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}