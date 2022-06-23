using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    [TestClass]
    public class CalcolaImportiRequestTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_DistributoriSingoli_NonPuoEssereMinoreDi0()
        {
            var cls = new CalcolaImportiRequest("A", "B","C", -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_DistributoriDoppi_NonPuoEssereMinoreDi0()
        {
            var cls = new CalcolaImportiRequest("A", "B", "C", 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_DistributoriMultipli_NonPuoEssereMinoreDi0()
        {
            var cls = new CalcolaImportiRequest("A", "B", "C", 0, 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_MQPassiCarrabili_NonPuoEssereMinoreDi0()
        {
            var cls = new CalcolaImportiRequest("A", "B", "C", 0, 0, 0, -1.0d);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_TipologiaImpianto_NonPuoEssereStringaVuota()
        {
            var cls = new CalcolaImportiRequest("", "B", "C", 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_TipologiaImpianto_NonPuoEssereNull()
        {
            var cls = new CalcolaImportiRequest(null, "B", "C", 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_CategoriaStradaDistributore_NonPuoEssereNull()
        {
            var cls = new CalcolaImportiRequest("A", null, "C", 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_CategoriaStradaDistributore_NonPuoEssereVuoto()
        {
            var cls = new CalcolaImportiRequest("A", "", "C", 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_CategoriaStradaCOSAP_NonPuoEssereNull()
        {
            var cls = new CalcolaImportiRequest("A", "B", null, 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_CategoriaStradaCOSAP_NonPuoEssereVuoto()
        {
            var cls = new CalcolaImportiRequest("A", "B", null, 0, 0, 0);
        }

        [TestMethod]
        public void Ctor_ServiziAccessori_SeInizializzatoConNull_NonEMaiNull()
        {
            var cls = new CalcolaImportiRequest("A", "B", "C", 0, 0, 0, 0, null);

            Assert.IsNotNull(cls.ServiziAccessoriAttivi);
        }
    }
}
