using Init.SIGePro.Manager.Logic.GestioneRisorseTestuali;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Sigepro.net.WebServices.WsAreaRiservata.Classes
{
	public partial class AreaRiservataServiceBase
	{
        [WebMethod]
        public RisorseTestualiService.Risorsa[] GetRisorseTestuali(string token, string software)
        {
            var authInfo = CheckToken(token);

            using (var db = authInfo.CreateDatabase())
            {
                var mgr = new RisorseTestualiService(db, authInfo.IdComune);

                return mgr.GetList(software, RisorseTestualiService.PrefissiRisorse.RisorsaAreaRiservata).ToArray();
            }
        }

        [WebMethod]
        public void AggiornaRisorsaTestuale(string token, string software, string chiave, string valore, int codiceAnagrafe)
        {
            var authInfo = CheckToken(token);

            using (var db = authInfo.CreateDatabase())
            {
                var mgr = new RisorseTestualiService(db, authInfo.IdComune);
                var auditService = new LayoutTestiAuditService(db, authInfo.IdComune, codiceAnagrafe);

                mgr.AggiornaRisorsa(software, chiave, valore, auditService);
            }
        }
    }
}