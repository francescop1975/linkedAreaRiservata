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
    public class StatoPraticaConditionTests
    {
        [TestMethod]
        public void StatoPraticaCondition_imposta_nome_e_numero_di_parametri()
        {

            var cls = new StatoPraticaCondition(new FakeDatabase(), "asd");

            cls.VerifyParameters();

            Assert.AreEqual(1, cls.GetParameters().Count());
        }
    }
}
