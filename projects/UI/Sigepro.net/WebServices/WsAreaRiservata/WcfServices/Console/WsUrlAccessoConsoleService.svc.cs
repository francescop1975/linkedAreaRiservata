using Init.SIGePro.Manager.Logic.GestioneConsole.UrlAccesso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace Sigepro.net.WebServices.WsAreaRiservata.WcfServices.Console
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WsUrlAccessoConsoleService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WsUrlAccessoConsoleService.svc or WsUrlAccessoConsoleService.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WsUrlAccessoConsoleService : WcfServiceBase, IWsUrlAccessoConsoleService
    {

        public ConfigurazioneUrlConsole GetUrlAccessoConsole(string token, string software)
        {
            var authInfo = CheckToken(token);

            return new UrlAccessoConsoleService(authInfo).GetUrlAccessoConsole(software);
        }
    }
}
