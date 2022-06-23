﻿using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestioneEntiTerzi;
using Init.Sigepro.FrontEnd.Infrastructure.IOC;
using log4net;
using System;
using System.Web;

namespace Init.Sigepro.FrontEnd.HttpModules
{
    public class ScrivaniaEntiTerziModule : IHttpModule
    {
        HttpApplication _context;
        ILog _log = LogManager.GetLogger(typeof(ScrivaniaEntiTerziModule));

        public ScrivaniaEntiTerziModule()
        {

        }

        public void Init(HttpApplication context)
        {
            _context = context;

            // FoKernelContainer.Inject(this);

            context.PostAuthorizeRequest += Context_PostAuthorizeRequest; ;
        }

        private void Context_PostAuthorizeRequest(object sender, EventArgs e)
        {
            try
            {
                var service = FoKernelContainer.GetService<IScrivaniaEntiTerziService>();
                var resolveSoftware = FoKernelContainer.GetService<ISoftwareResolver>();
                var authDataResolver = FoKernelContainer.GetService<IAuthenticationDataResolver>();

                var localPath = new ApplicationPath(_context.Request.Url.LocalPath);

                if (!localPath.IsReserved)
                {
                    return;
                }

                if (service.ModuloAttivo(resolveSoftware.Software) && !service.UtentePuoAccedere(new ETCodiceAnagrafe(authDataResolver.DatiAutenticazione.DatiUtente.Codiceanagrafe.Value)))
                {
                    _context.Response.StatusCode = 404;
                    _context.Response.End();

                }
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid().ToString();
                this._log.Error($"[{guid}]Context_PostAuthorizeRequest->{ex}");
                throw new Exception($"Si è verificato un errore inatteso, verificare i log per ulteriori informazioni (rif.errore: {guid})");
            }
        }


        public void Dispose()
        {

        }
    }
}