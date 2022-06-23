using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Visura.QueryRicerca
{
    public class NomeOCfRichiedenteCondition: QueryConditionBase
    {
        public NomeOCfRichiedenteCondition(DataBase db, string nomeOCfRichiedente)
            :base(db, "NomeCfRichiedente")
        {
            if (String.IsNullOrEmpty(nomeOCfRichiedente))
            {
                return;
            }

            var sb = new StringBuilder();
            var cf = "%" + nomeOCfRichiedente.ToUpper() + "%";

            sb.Append("(");
            sb.Append($" ANAGRAFE.codicefiscale = {QueryParameterName("cf1")} OR ANAGRAFE.partitaiva = {QueryParameterName("cf2")} ");
            sb.Append($" OR {db.Specifics.UCaseFunction( db.Specifics.ConcatFunction("ANAGRAFE.NOMINATIVO", "ANAGRAFE.NOME"))} like {QueryParameterName("cf3")} ");
            sb.Append($" OR AZIENDA.codicefiscale = {QueryParameterName("cf4")} OR AZIENDA.partitaiva = {QueryParameterName("cf5")} ");
            sb.Append($" OR {db.Specifics.UCaseFunction(db.Specifics.ConcatFunction("AZIENDA.NOMINATIVO", "AZIENDA.NOME"))} like {QueryParameterName("cf6")} ");
            sb.Append(")");


            Query = sb.ToString();

            AddParameter("cf1", cf);
            AddParameter("cf2", cf);
            AddParameter("cf3", cf);
            AddParameter("cf4", cf);
            AddParameter("cf5", cf);
            AddParameter("cf6", cf);
        }
    }
}
