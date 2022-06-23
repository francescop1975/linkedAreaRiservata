using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.LetturaXmlDomandaBackend
{
    internal class XmlDomandaBackendDaVbg : IXmlDomandaDaVisura
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(XmlDomandaBackendDaVbg));

        private readonly IVisuraService _visuraService;

        internal XmlDomandaBackendDaVbg(IVisuraService visuraService)
        { 
            this._visuraService = visuraService;
        }

        public string GetXml(int idDomandaBackend, bool leggiDatiConfigurazione = false)
        {
            var istanza = this._visuraService.GetById(idDomandaBackend, new VisuraIstanzaFlags { LeggiDatiConfigurazione = leggiDatiConfigurazione });

            return (istanza != null) ? istanza.ToXmlModelloRiepilogo() : String.Empty;
        }
    }
}
