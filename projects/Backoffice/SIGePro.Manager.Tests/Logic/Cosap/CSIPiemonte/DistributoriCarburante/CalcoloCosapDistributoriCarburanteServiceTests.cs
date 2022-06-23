using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    [TestClass]
    public class CalcoloCosapDistributoriCarburanteServiceTests
    {
        public class FakeTariffeDistributoriReader : ITariffeDistributoriReader
        {
            public TariffaPassiCarrabili GetTariffaPassiCarrabili()
            {
                return new TariffaPassiCarrabili(10.0d);
            }

            public TariffeDistributoriCarburante GetTariffeCanoneDistributori()
            {
                return new TariffeDistributoriCarburante(10.0d, 20.0d, 30.0d, 40.0d, 50.0d);
            }
        }


        [TestMethod]
        public void CalcolaImporti_MostraFormulaCalcoloCanone()
        {
            var service = new CalcoloCosapDistributoriCarburanteService();
            var request = new CalcolaImportiRequest("T1", "C1", "CC1",1, 2, 3, 4.0d);
            var reader = new FakeTariffeDistributoriReader();
            var expected = "[10,00 + (1 * 20,00) + (2 * 30,00) + (3 * 40,00)] * 50,00";

            var esito = service.CalcolaImporti(request, reader);

            Assert.AreEqual<string>(expected, esito.Canone.SpiegazioneFormula);
        }

        [TestMethod]
        public void CalcolaImporti_CalcoloCanone_Corretto()
        {
            var service = new CalcoloCosapDistributoriCarburanteService();
            var request = new CalcolaImportiRequest("T1", "C1","CC1", 1, 2, 3, 4.0d);
            var reader = new FakeTariffeDistributoriReader();
            var expected = (10.0d + (1.0d * 20.0d) + (2.0d * 30.0d) + (3.0d * 40.0d)) * 50.0d;

            var esito = service.CalcolaImporti(request, reader);

            Assert.AreEqual<double>(expected, esito.Canone.Importo.Valore);
        }

        [TestMethod]
        public void CalcolaImporti_MostraFormulaPassiCarrabili()
        {
            var service = new CalcoloCosapDistributoriCarburanteService();
            var request = new CalcolaImportiRequest("T1", "C1", "CC1", 1, 2, 3, 4.0d);
            var reader = new FakeTariffeDistributoriReader();
            var expected = "4,00 * 10,00";

            var esito = service.CalcolaImporti(request, reader);

            Assert.AreEqual<string>(expected, esito.PassiCarrabili.SpiegazioneFormula);
        }

        [TestMethod]
        public void CalcolaImporti_CalcoloPassiCarrabili_Corretto()
        {
            var service = new CalcoloCosapDistributoriCarburanteService();
            var request = new CalcolaImportiRequest("T1", "C1", "CC1", 1, 2, 3, 4.0d);
            var reader = new FakeTariffeDistributoriReader();
            var expected = 4.0d * 10.0d;

            var esito = service.CalcolaImporti(request, reader);

            Assert.AreEqual<double>(expected, esito.PassiCarrabili.Importo.Valore);
        }

        [TestMethod]
        public void CalcolaImporti_MostraFormulaEccedenza()
        {
            var service = new CalcoloCosapDistributoriCarburanteService();
            var request = new CalcolaImportiRequest("T1", "C1", "CC1", 1, 2, 3, 4.0d);
            var reader = new FakeTariffeDistributoriReader();
            var expected = "40,00 - 10500,00";

            var esito = service.CalcolaImporti(request, reader);

            Assert.AreEqual<string>(expected, esito.Eccedenza.SpiegazioneFormula);
        }

        [TestMethod]
        public void CalcolaImporti_CalcoloEccedenza_Corretto()
        {
            var service = new CalcoloCosapDistributoriCarburanteService();
            var request = new CalcolaImportiRequest("T1", "C1", "CC1", 1, 2, 3, 4.0d);
            var reader = new FakeTariffeDistributoriReader();
            var expected = 40.0d - 10500.0d;

            var esito = service.CalcolaImporti(request, reader);

            Assert.AreEqual<double>(expected, esito.Eccedenza.Importo.Valore);
        }
    }
}
