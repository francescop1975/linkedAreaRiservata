using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.DebugConfiguration
{
    public class FVGDebugConfiguration : IFVGDebugConfiguration
    {
        public bool IsDebugEnabled => !string.IsNullOrEmpty(ConfigurationManager.AppSettings["FvgDatabasePersistenceMediumFactory.debugMode"]);

        public string ManagedDataMappingsDevFile => "~/moduli-fvg/compilazione/managed-data-mappings.nocopy.xml";
    }
}
