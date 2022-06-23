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
    public class OggettoPraticaConditionTests
    {
        [TestMethod]
        public void OggettoPraticaCondition_imposta_correttamente_nome_e_numero_di_parametri()
        {
            var cls = new OggettoPraticaCondition(new FakeDatabase(), "123");

            cls.VerifyParameters();

            Assert.AreEqual(1, cls.GetParameters().Count());
        }
    }
}
