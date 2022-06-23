using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.DebugConfiguration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG
{
    public class FVGWebServiceProxyFactory
    {
        private readonly IConfigurazione<ParametriFvgSol> _config;
        private readonly IFVGDebugConfiguration _debugConfiguration;

        public FVGWebServiceProxyFactory(IConfigurazione<ParametriFvgSol> config, IFVGDebugConfiguration debugConfiguration)
        {
            this._config = config;
            _debugConfiguration = debugConfiguration;
        }

        public IFVGWebServiceProxy CreateService()
        {
            if (this._debugConfiguration.IsDebugEnabled)
            {
                return new FVGFileSystemWebServiceProxy();
            }

            return new FVGWebServiceProxy(this._config);
        }
    }
}
