using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Sit.Ravenna2.SQLServer
{
    public class SQLServerTableNameProvider : IDatabaseTableNameProvider
    {
        public string Ra012 => "vwVBGRA012";

        public string Ra147 => "vwVBGRA147";

        public string ParticelleCatastali => "vwVBGPARTICELLE_CATASTALI";
    }
}
