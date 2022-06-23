using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Cosap.CSIPiemonte
{
    [TestClass]
    public class ImportoTests
    {
        [TestMethod]
        public void ctor_senzaDecimali_tornaToStringCon2DecimaliArrotondatoPerEccesso() 
        {
            var importo = new Importo(10.2563548);
            var expected = "10,26";

            Assert.AreEqual<string>(expected, importo.ToString(), $"Torna il valore {expected}");
        }

        [TestMethod]
        public void ctor_senzaDecimali_tornaToStringCon2DecimaliArrotondatoPerDifetto()
        {
            var importo = new Importo(10.2543548);
            var expected = "10,25";

            Assert.AreEqual<string>(expected, importo.ToString(), $"Torna il valore {expected}");
        }

        [TestMethod]
        public void ctor_senzaDecimali_tornaToStringCon2DecimaliArrotondatoPerEccessoSeCifraUtile5()
        {
            var importo = new Importo(10.2553548);
            var expected = "10,26";

            Assert.AreEqual<string>(expected, importo.ToString(), $"Torna il valore {expected}");
        }

        [TestMethod]
        public void ctor_con5Decimali_tornaToStringCon5DecimaliArrotondatoPerEccesso()
        {
            var importo = new Importo(10.256358,5);
            var expected = "10,25636";

            Assert.AreEqual<string>(expected, importo.ToString(), $"Torna il valore {expected}");
        }

        [TestMethod]
        public void ctor_con5Decimali_tornaToStringCon5DecimaliArrotondatoPerDifetto()
        {
            var importo = new Importo(10.256354, 5);
            var expected = "10,25635";

            Assert.AreEqual<string>(expected, importo.ToString(), $"Torna il valore {expected}");
        }

        [TestMethod]
        public void ctor_con5Decimali_tornaToStringCon5DecimaliArrotondatoPerEccessoSeCifraUtile5()
        {
            var importo = new Importo(10.256355, 5);
            var expected = "10,25636";

            Assert.AreEqual<string>(expected, importo.ToString(), $"Torna il valore {expected}");
        }
    }
}
