using PersonalLib2.Data;

namespace Init.SIGePro.Manager.Logic.Visura.QueryRicerca
{
    public class DataIstanzaCondition : QueryConditionBase
    {
        public DataIstanzaCondition(DataBase database, FiltroPeriodoPresentazione periodoPresentazione)
            : base(database, "DataIstanza")
        {
            if (periodoPresentazione == null)
            {
                return;
            }

            var range = periodoPresentazione.ToDateRange();
            this.Query = $"istanze.DATA BETWEEN {QueryParameterName("dallaData")} AND {QueryParameterName("allaData")}";

            AddParameter("dallaData", range.Min);
            AddParameter("allaData", range.Max);
        }
    }
}
