using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Mercati;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonalLib2.Data;
using SIGePro.Manager.Tests.Fakes.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.DatiDinamici.DataAccess.Mercati
{
    [TestClass]
    public class MercatiDyn2DatiRepositoryTests
    {
        public class FakeCampoDinamico: CampoDinamicoBase
        {
            public FakeCampoDinamico(int id, string valore, string valoreDecodificato)
                :base(null, null)
            {
                this.Id = id;
                this.ListaValori[0].Valore = valore;
                this.ListaValori[0].ValoreDecodificato = valoreDecodificato;
            }
        }

        [TestMethod]
        public void EliminaValoriCampi_esegue_una_query_di_eliminazione_su_db()
        {
            var database = new FakeDatabase(ProviderType.OracleClient);

            var repository = new MercatiDyn2DatiRepository(database, 1, "TEST");

            var idModello = new DatiIdentificativiModello(1,0);

            repository.EliminaValoriCampi(idModello, new List<DatiIdentificativiCampo> {
                new DatiIdentificativiCampo(3)
            });

            var expectedQuery = @"delete from mercatidyn2dati where 
                                        idcomune=:idcomune and 
                                        codicemercato=:codicemercato and
                                        fk_d2c_id = :idCampo and
                                        indice = :indice";

            Assert.AreEqual<string>(expectedQuery, database.UltimaQueryEseguita.Sql);

            Assert.AreEqual("TEST", database.UltimaQueryEseguita.Parametri["idcomune"].ToString());
            Assert.AreEqual<int>(1, (int)database.UltimaQueryEseguita.Parametri["codicemercato"]);
            Assert.AreEqual<int>(3, (int)database.UltimaQueryEseguita.Parametri["idCampo"]);
            Assert.AreEqual<int>(0, (int)database.UltimaQueryEseguita.Parametri["indice"]);
        }

        [TestMethod]
        public void GetValoriCampiDaIdModello_esegue_una_query_su_db()
        {
            var database = new FakeDatabase(ProviderType.OracleClient);
            
            var repository = new MercatiDyn2DatiRepository(database, 1, "TEST");

            var valoriCampi = repository.GetValoriCampiDaIdModello(1, 0);

            Assert.AreEqual<int>(1, database.QueryEseguite.Count);
        }


        [TestMethod]
        public void GetValoriCampiDaIdModello_legge_i_valori_da_un_datareader_e_ci_popola_una_classe_IValoreCampo()
        {
            var database = new FakeDatabase(ProviderType.OracleClient);

            database.DataSource = new FakeDatabaseDataSource();

            database.DataSource.Add(new Dictionary<string, object>
            {
                { "fk_d2c_id", 1},
                { "indice", 0},
                { "indice_molteplicita", 0},
                { "Valore", "Valore 1.0"},
                { "valoredecodificato", "Valore decodificato 1.0"},
            });

            database.DataSource.Add(new Dictionary<string, object>
            {
                { "fk_d2c_id", 1},
                { "indice", 0},
                { "indice_molteplicita", 1},
                { "Valore", "Valore 1.1"},
                { "valoredecodificato", "Valore decodificato 1.1"},
            });

            database.DataSource.Add(new Dictionary<string, object>
            {
                { "fk_d2c_id", 2},
                { "indice", 0},
                { "indice_molteplicita", 0},
                { "Valore", "Valore 2.0"},
                { "valoredecodificato", "Valore decodificato 2.0"},
            });

            var repository = new MercatiDyn2DatiRepository(database, 1, "TEST");

            var valoriCampi = repository.GetValoriCampiDaIdModello(1, 0);

            Assert.AreEqual<int>(2, valoriCampi.Count);

            Assert.AreEqual<int>(0, valoriCampi[1].ElementAt(0).IndiceMolteplicita.Value);
            Assert.AreEqual<string>("Valore 1.0", valoriCampi[1].ElementAt(0).Valore);
            Assert.AreEqual<string>("Valore decodificato 1.0", valoriCampi[1].ElementAt(0).Valoredecodificato);

            Assert.AreEqual<int>(1, valoriCampi[1].ElementAt(1).IndiceMolteplicita.Value);
            Assert.AreEqual<string>("Valore 1.1", valoriCampi[1].ElementAt(1).Valore);
            Assert.AreEqual<string>("Valore decodificato 1.1", valoriCampi[1].ElementAt(1).Valoredecodificato);

            Assert.AreEqual<int>(0, valoriCampi[2].ElementAt(0).IndiceMolteplicita.Value);
            Assert.AreEqual<string>("Valore 2.0", valoriCampi[2].ElementAt(0).Valore);
            Assert.AreEqual<string>("Valore decodificato 2.0", valoriCampi[2].ElementAt(0).Valoredecodificato);
        }

        [TestMethod]
        public void SalvaValoriCampi_prima_elimina_i_vecchi_valori_da_db_e_poi_inserisce_i_nuovi()
        {
            var database = new FakeDatabase(ProviderType.OracleClient);

            var repository = new MercatiDyn2DatiRepository(database, 1, "TEST");

            //throw new NotImplementedException();
            var campoDaSalvare = new CampoDaSalvare(10, "Test", false);
            campoDaSalvare.AggiungiValore("Valore", "ValoreDecodificato");

            repository.SalvaValoriCampi(new DatiIdentificativiModello(1, 0), new[] { campoDaSalvare });

            Assert.AreEqual(2, database.QueryEseguite.Count);

            Assert.IsTrue(database.QueryEseguite[0].Sql.StartsWith("delete from mercatidyn2dati"));
            Assert.IsTrue(database.QueryEseguite[1].Sql.ToLower().StartsWith("insert into mercatidyn2dati"));

            Assert.AreEqual<string>("TEST", database.QueryEseguite[1].Parametri["idComune"].ToString());
            Assert.AreEqual<int>(1, (int)database.QueryEseguite[1].Parametri["codiceMercato"]);
            Assert.AreEqual<int>(10, (int)database.QueryEseguite[1].Parametri["idCampo"]);
            Assert.AreEqual<string>("Valore", database.QueryEseguite[1].Parametri["valore"].ToString());
            Assert.AreEqual<string>("ValoreDecodificato", database.QueryEseguite[1].Parametri["valoreDecodificato"].ToString());
            Assert.AreEqual<int>(0, (int)database.QueryEseguite[1].Parametri["indiceMolteplicita"]);
        }
    }
}
