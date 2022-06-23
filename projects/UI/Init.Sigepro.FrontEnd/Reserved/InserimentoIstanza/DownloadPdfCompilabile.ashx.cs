﻿using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.PrecompilazionePDF;
using Init.Sigepro.FrontEnd.AppLogic.Services.Navigation;
using log4net;
using Ninject;
using System;
using System.Web;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza
{
    /// <summary>
    /// Summary description for DownloadPdfCompilabile
    /// </summary>
    public class DownloadPdfCompilabile : Ninject.Web.HttpHandlerBase, IHttpHandler
    {
        private static class QuerystringConstants
        {
            public const string UserAuthenticationResult = "UserAuthenticationResult";
            public const string CodiceOggetto = "codiceOggetto";
            public const string IdDomanda = "idDomanda";
        }

        [Inject]
        protected IPdfUtilsService _pdfUtilsService { get; set; }
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

        protected override void DoProcessRequest(HttpContext context)
        {
            try
            {
                this._context = context;

                var file = _pdfUtilsService.PrecompilaPdf(CodiceOggetto, IdDomanda);

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