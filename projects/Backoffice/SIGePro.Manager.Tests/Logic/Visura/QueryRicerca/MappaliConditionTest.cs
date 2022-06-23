using Init.SIGePro.Manager.Logic.Visura;
using Init.SIGePro.Manager.Logic.Visura.QueryRicerca;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGePro.Manager.Tests.Fakes.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Visura.QueryRicerca
{
    [TestClass]
    public class MappaliConditionTest
    {
        [TestMethod]
        public void MappaliCondition_imposta_correttamente_nome_e_numero_di_parametri()
        {
            var filtro = new FiltriDatiCatastali
            {
                TipoCatasto = "F",
                Foglio = "1",
                Particella = "2",
                Subalterno = "3"
            };
            var cls = new MappaliCondition(new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient), filtro);

            cls.VerifyParameters();

            Assert.AreEqual(4, cls.GetParameters().Count());
        }
    }
}
