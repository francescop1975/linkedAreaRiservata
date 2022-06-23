using System;
using Init.Sigepro.FrontEnd.AppLogic.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Init.Sigepro.FrontEnd.AppLogicTests.Utils
{
    [TestClass]
    public class DataDaStringParserTests
    {
        [TestMethod]
        public void restituisce_null_se_data_stringa_vuota()
        {
            var dp = new DataDaStringParser("");

            var result = dp.Parse();

            Assert.IsFalse(result.HasValue);
        }

        [TestMethod]
        public void restituisce_data_se_separatore_barra()
        {
            var dp = new DataDaStringParser("19/03/1979");

            var result = dp.Parse();

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual<DateTime>(new DateTime(1979, 03, 19), result.Value);
        }

        [TestMethod]
        public void restituisce_data_se_separatore_tratto()
        {
            var dp = new DataDaStringParser("19-03-1979");

            var result = dp.Parse();

            Assert.IsTrue(result.HasValue);
            Assert.AreEqual<DateTime>(new DateTime(1979, 03, 19), result.Value);
        }

        [TestMethod]
        public void restituisce_null_se_data_non_valida()
        {
            var dp = new DataDaStringParser("asdasdasd");

            var result = dp.Parse();

            Assert.IsFalse(result.HasValue);
        }
    }
}
