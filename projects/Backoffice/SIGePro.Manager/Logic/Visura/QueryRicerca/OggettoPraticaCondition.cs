using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Visura.QueryRicerca
{
    public class OggettoPraticaCondition: QueryConditionBase
    {
        public OggettoPraticaCondition(DataBase db, string partial)
            :base(db, "OggettoPratica")
        {
            if(String.IsNullOrEmpty(partial))
            {
                return;
            }

            this.Query = $"{db.Specifics.UCaseFunction("istanze.LAVORI")} LIKE {QueryParameterName("partial")}";

            this.AddParameter("partial", partial.ToUpper() + "%");
        }
    }
}
