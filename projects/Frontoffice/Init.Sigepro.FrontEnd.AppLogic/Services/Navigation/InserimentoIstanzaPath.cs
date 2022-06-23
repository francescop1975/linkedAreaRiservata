namespace Init.Sigepro.FrontEnd.AppLogic.Services.Navigation
{
    using Init.Sigepro.FrontEnd.AppLogic.Common;
    using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class InserimentoIstanzaPath
    {
        public EditOggettiPath EditOggetti { get; private set; }
        public FirmaGrafometricaPath FirmaGrafometrica { get; private set; }


        public InserimentoIstanzaPath(IAliasSoftwareResolver aliasSoftwareResolver)
        {
            this.EditOggetti = new EditOggettiPath(aliasSoftwareResolver);
            this.FirmaGrafometrica = new FirmaGrafometricaPath(aliasSoftwareResolver);

        }

        
        
    }
}
