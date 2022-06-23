using Init.SIGePro.Manager.Logic.Visura.QueryRicerca;
using Init.SIGePro.Manager.Logic.Visura;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGePro.Manager.Tests.Fakes.DataAccess;

namespace SIGePro.Manager.Tests.Logic.Visura.QueryRicerca
{
    [TestClass]
    public class NomeOCfRichiedenteConditionTests
    {
        [TestMethod]
        public void NomeOCfRichiedenteCondition_imposta_correttamente_nome_e_numero_di_parametri()
        {
            var cls = new NomeOCfRichiedenteCondition(new FakeDatabase(), "CF" );

            cls.VerifyParameters();

            // Cerca per:
            // CF Richiedente
            // PI Richiedente
            // Nome Richiedente
            // CF Azienda
            // PI Azienda
            // Nome Azienda
            Assert.AreEqual(6, cls.GetParameters().Count());
        }
    }
}
