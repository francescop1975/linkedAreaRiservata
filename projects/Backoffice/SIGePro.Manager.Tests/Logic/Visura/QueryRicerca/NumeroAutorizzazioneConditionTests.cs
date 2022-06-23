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
    public class NumeroAutorizzazioneConditionTests
    {
        [TestMethod]
        public void NumeroAutorizzazioneCondition_imposta_correttamente_nome_e_numero_parametri()
        {
            var cls = new NumeroAutorizzazioneCondition(new FakeDatabase(), "123");

            cls.VerifyParameters();

            Assert.AreEqual(1, cls.GetParameters().Count());
        }
    }
}
