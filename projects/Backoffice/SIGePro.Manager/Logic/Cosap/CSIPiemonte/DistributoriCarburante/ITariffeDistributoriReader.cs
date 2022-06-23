using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    public interface ITariffeDistributoriReader
    {
        TariffaPassiCarrabili GetTariffaPassiCarrabili();
        TariffeDistributoriCarburante GetTariffeCanoneDistributori();
    }
}
