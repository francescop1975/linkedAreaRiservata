using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Fakes.DataAccess
{
    [TestClass]
    public class FakeDatabaseTests
    {
        [TestMethod]
        public void ExecuteNonQuery_logga_la_query_e_i_parametri_utilizzati()
        {
            var sql = "select * from miatabella";
            var db = new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient);

            db.ExecuteNonQuery(sql, mp =>
            {
                mp.AddParameter("par1", 1);
                mp.AddParameter("par2", "due");
                mp.AddParameter("par3", 3);
            });

            Assert.AreEqual(sql, db.UltimaQueryEseguita.Sql);

            Assert.AreEqual("par1", db.UltimaQueryEseguita.Parametri.Keys.ElementAt(0));
            Assert.AreEqual<int>(1, (int)db.UltimaQueryEseguita.Parametri["par1"]);

            Assert.AreEqual("par2", db.UltimaQueryEseguita.Parametri.Keys.ElementAt(1));
            Assert.AreEqual<string>("due", db.UltimaQueryEseguita.Parametri["par2"].ToString());

            Assert.AreEqual("par3", db.UltimaQueryEseguita.Parametri.Keys.ElementAt(2));
            Assert.AreEqual<int>(3, (int)db.UltimaQueryEseguita.Parametri["par3"]);
        }

        [TestMethod]
        public void ExecuteScalar_logga_la_query_e_i_parametri_utilizzati()
        {
            var sql = "select * from miatabella";
            var db = new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient);

            db.ExecuteScalar(sql, 0, mp =>
            {
                mp.AddParameter("par1", 1);
                mp.AddParameter("par2", "due");
                mp.AddParameter("par3", 3);
            });

            Assert.AreEqual<string>(sql, db.UltimaQueryEseguita.Sql);

            Assert.AreEqual("par1", db.UltimaQueryEseguita.Parametri.Keys.ElementAt(0));
            Assert.AreEqual<int>(1, (int)db.UltimaQueryEseguita.Parametri["par1"]);

            Assert.AreEqual("par2", db.UltimaQueryEseguita.Parametri.Keys.ElementAt(1));
            Assert.AreEqual<string>("due", db.UltimaQueryEseguita.Parametri["par2"].ToString());

            Assert.AreEqual("par3", db.UltimaQueryEseguita.Parametri.Keys.ElementAt(2));
            Assert.AreEqual<int>(3, (int)db.UltimaQueryEseguita.Parametri["par3"]);
        }

        [TestMethod]
        public void ExecuteScalar_restituisce_il_valore_di_ExecuteScalarResult()
        {
            var db = new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient);
            var expected = 1;

            db.ExecuteScalarResult = expected;

            var result = db.ExecuteScalar("mia_query", 0, mp =>
            {
                mp.AddParameter("par1", 1);
            });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExecuteScalar_restituisce_il_valore_di_default_se_ExecuteScalarResult_è_null()
        {
            var db = new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient);
            var expected = 0;

            db.ExecuteScalarResult = null;

            var result = db.ExecuteScalar("mia_query", expected, mp =>
            {
                mp.AddParameter("par1", 1);
            });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExecuteReader_logga_la_query_e_i_parametri_utilizzati()
        {
            var sql = "select * from miatabella";
            var db = new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient);

            db.ExecuteReader(sql, mp =>
            {
                mp.AddParameter("par1", 1);
                mp.AddParameter("par2", "due");
                mp.AddParameter("par3", 3);
            }, dr => new { });

            Assert.AreEqual<string>(sql, db.UltimaQueryEseguita.Sql);

            Assert.AreEqual("par1", db.UltimaQueryEseguita.Parametri.Keys.ElementAt(0));
            Assert.AreEqual<int>(1, (int)db.UltimaQueryEseguita.Parametri["par1"]);

            Assert.AreEqual("par2", db.UltimaQueryEseguita.Parametri.Keys.ElementAt(1));
            Assert.AreEqual<string>("due", db.UltimaQueryEseguita.Parametri["par2"].ToString());

            Assert.AreEqual("par3", db.UltimaQueryEseguita.Parametri.Keys.ElementAt(2));
            Assert.AreEqual<int>(3, (int)db.UltimaQueryEseguita.Parametri["par3"]);
        }

        [TestMethod]
        public void ExecuteReader_invoca_la_callback_per_ogni_record_della_data_source()
        {
            var sql = "select * from miatabella";
            var db = new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient);

            db.DataSource = new FakeDatabaseDataSource();

            db.DataSource.Add(new Dictionary<string, object>
            {
                {"par1", 1},
                {"par2", 2}
            });

            db.DataSource.Add(new Dictionary<string, object>
            {
                {"par1", 3},
                {"par2", 4}
            });

            var result = db.ExecuteReader(sql, mp => { }, 
                dr => new {
                    par1 = (int)dr["par1"],
                    par2 = (int)dr["par2"],
                }).ToList();

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result[0].par1);
            Assert.AreEqual(2, result[0].par2);
            Assert.AreEqual(3, result[1].par1);
            Assert.AreEqual(4, result[1].par2);
            
        }

        [TestMethod]
        public void BeginTransaction_imposta_IsInTransaction_a_true()
        {
            var db = new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient);

            db.BeginTransaction();

            Assert.AreEqual(true, db.IsInTransaction);
        }

        [TestMethod]
        public void RollbackTransaction_imposta_IsInTransaction_a_false()
        {
            var db = new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient);

            db.BeginTransaction();
            db.RollbackTransaction();

            Assert.AreEqual(false, db.IsInTransaction);
        }

        [TestMethod]
        public void CommitTransaction_imposta_IsInTransaction_a_false()
        {
            var db = new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient);

            db.BeginTransaction();
            db.CommitTransaction();

            Assert.AreEqual(false, db.IsInTransaction);
        }
    }
}
