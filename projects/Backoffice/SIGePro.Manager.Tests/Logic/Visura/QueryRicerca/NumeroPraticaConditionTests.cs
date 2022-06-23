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
    public class NumeroPraticaConditionTests
    {
        [
           TestMethod ]
        public void NumeroPraticaCondition_imposta_correttamente_nome_e_numero_parametri()
        {
            var cls = new NumeroPraticaCondition(new FakeDatabase(), "123");

            cls.VerifyParameters();

            // I paramteri devono essere 2, uno per la ricerca esatta (Es. 1/1900) e uno per la richierca parziale (Es. 1/*)
            Assert.AreEqual(2, cls.GetParameters().Count());
        }
    }
}
