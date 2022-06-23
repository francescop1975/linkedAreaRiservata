using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PersonalLib2.Data;

namespace Init.SIGePro.Manager
{
    public partial class RaggruppamentoCausaliOneriMgr
    {
        public double GetImportoTotale(string idComune, int codiceIstanza, int codiceRaggruppamento)
        {
            return GetImportoTotale(idComune, codiceIstanza, codiceRaggruppamento, false);
        }

        public double GetImportoTotale(string idComune, int codiceIstanza, int codiceRaggruppamento, bool bIstruttoria)
        {
            var sql = $@"SELECT 
							  sum( istanzeoneri.{(bIstruttoria ? "prezzoistruttoria" : "prezzo")} ) as prezzoTot
							FROM
							  istanzeoneri,
                              tipicausalioneri
							WHERE
							   istanzeoneri.idcomune = tipicausalioneri.idcomune  AND
							   istanzeoneri.fkidtipocausale = tipicausalioneri.co_id AND
                               istanzeoneri.codiceistanza = {db.Specifics.QueryParameterName("codiceistanza")} AND
							   tipicausalioneri.idcomune = {db.Specifics.QueryParameterName("idcomune")} AND
							   tipicausalioneri.fk_rco_id = {db.Specifics.QueryParameterName("fk_rco_id")}";

            return this.db.ExecuteScalar(sql, 0.0d, mp =>
            {
                mp.AddParameter("codiceistanza", codiceIstanza);
                mp.AddParameter("idcomune", idComune);
                mp.AddParameter("fk_rco_id", codiceRaggruppamento);
            });

        }

        public DataSet GetImporti(string idComune, int codiceIstanza, int codiceRaggruppamento)
        {
            DataSet ds = new DataSet();
            string sql;


            sql = @"SELECT 
						  istanzeoneri.prezzo,istanzeoneri.prezzoistruttoria,istanzeoneri.fkidtipocausale,istanzeoneri.flentratauscita,istanzeoneri.data,istanzeoneri.id,istanzeoneri.numerorata,istanzeoneri.nr_documento
						FROM
						  istanzeoneri,
                          tipicausalioneri
						WHERE
						   istanzeoneri.idcomune = tipicausalioneri.idcomune  AND
						   istanzeoneri.fkidtipocausale = tipicausalioneri.co_id AND
                           istanzeoneri.datapagamento is null AND 
                           istanzeoneri.codiceistanza = {0} AND
						   tipicausalioneri.idcomune = {1} AND
						   tipicausalioneri.fk_rco_id = {2} order by istanzeoneri.fkidtipocausale";
            

            sql = String.Format(sql, db.Specifics.QueryParameterName("codiceistanza"),
                                     db.Specifics.QueryParameterName("idcomune"),
                                     db.Specifics.QueryParameterName("fk_rco_id"));


            using (IDbCommand cmd = db.CreateCommand(sql))
            {
                cmd.Parameters.Add(db.CreateParameter("codiceistanza", codiceIstanza));
                cmd.Parameters.Add(db.CreateParameter("idcomune", idComune));
                cmd.Parameters.Add(db.CreateParameter("fk_rco_id", codiceRaggruppamento));

                IDataAdapter da = db.CreateDataAdapter(cmd);
                da.Fill(ds);
            }

            return ds;
        }

        private string GetDate(string dateDB)
        {
            string date = string.Empty;
            switch (db.ConnectionDetails.ProviderType)
            {
                case ProviderType.OracleClient:
                    date = "TO_CHAR(" + dateDB + ",'DD/MM/YYYY')";
                    break;
                case ProviderType.SqlClient:
                    date = "CONVERT(VARCHAR," + dateDB + ",103)";
                    break;
                case ProviderType.MySqlClient:
                    date = "date_format(" + dateDB + ",'%d/%m/%Y')";
                    break;
            }

            return date;
        }

        public DataSet GetListaOneri(string idComune, int codiceIstanza, int codiceRaggruppamento)
        {
            DataSet ds = new DataSet();
            string sql;


            sql = "SELECT istanzeoneri.prezzo,istanzeoneri.prezzoistruttoria," + GetDate("istanzeoneri.datascadenza") + " as datascadenza," + GetDate("istanzeoneri.data") + " as data,istanzeoneri.fkidtipocausale,tipicausalioneri.co_descrizione,istanzeoneri.nr_documento " +
                        "FROM " +
						  "istanzeoneri, "+
                          "tipicausalioneri "+
						"WHERE "+
						   "istanzeoneri.idcomune = tipicausalioneri.idcomune  AND "+
						   "istanzeoneri.fkidtipocausale = tipicausalioneri.co_id AND "+
                           "istanzeoneri.numerorata is not null AND "+
                           "istanzeoneri.datapagamento is null AND "+
                           "istanzeoneri.codiceistanza = {0} AND "+
						   "tipicausalioneri.idcomune = {1} AND "+
						   "tipicausalioneri.fk_rco_id = {2} "+
                       "ORDER BY istanzeoneri.fkidtipocausale,istanzeoneri.datascadenza,istanzeoneri.id";


            sql = String.Format(sql, db.Specifics.QueryParameterName("codiceistanza"),
                                     db.Specifics.QueryParameterName("idcomune"),
                                     db.Specifics.QueryParameterName("fk_rco_id"));


            using (IDbCommand cmd = db.CreateCommand(sql))
            {
                cmd.Parameters.Add(db.CreateParameter("codiceistanza", codiceIstanza));
                cmd.Parameters.Add(db.CreateParameter("idcomune", idComune));
                cmd.Parameters.Add(db.CreateParameter("fk_rco_id", codiceRaggruppamento));

                IDataAdapter da = db.CreateDataAdapter(cmd);
                da.Fill(ds);
            }

            return ds;
        }
    }
}
