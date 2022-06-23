using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Visura.QueryRicerca
{
    public class SoftwarePraticaCondition: QueryConditionBase
    {
        public SoftwarePraticaCondition(DataBase db, string software)
            :base(db, "Software")
        {
            if (String.IsNullOrEmpty(software))
            {
                return;
            }

            this.Query = $"istanze.software = {QueryParameterName("software")}";

            AddParameter("software", software);
        }
    }
}
