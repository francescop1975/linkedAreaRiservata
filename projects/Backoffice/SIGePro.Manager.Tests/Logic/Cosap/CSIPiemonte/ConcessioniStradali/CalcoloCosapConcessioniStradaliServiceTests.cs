using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Cosap.CSIPiemonte.ConcessioniStradali
{
    [TestClass]
    public class CalcoloCosapConcessioniStradaliServiceTests
    {
        public class FakeTariffeConcessioniStradaliReader : ITariffeConcessioniStradaliReader
        {
            public ITariffeConcessioniStradali GetTariffe()
            {
                return new Tariffario(10.2d, 2);
            }
        }

        public class FakeTariffeConcessioniStradaliReaderRidotte50 : ITariffeConcessioniStradaliReader
        {
            public ITariffeConcessioniStradali GetTariffe()
            {
                return new Tariffario(10.2d, 50.0d, 2);
            }
        }

        [TestMethod]
        public void CalcolaImportiPermanenti_MostraFormulaCalcoloCanone()
        {
            var service = new CalcoloCosapConcessioniStradaliService();
            var request = new CalcoloConcessioniStradaliPermanentiRequest("T1", "C1", 4.0d);
            var reader = new FakeTariffeConcessioniStradaliReader();
            var expected = @"Tariffa * Quantità
10,20 * 4";

            var esito = service.CalcolaImportiPermanenti(request, reader);

            Assert.AreEqual<string>(expected, esito.Canone.SpiegazioneFormula);
        }

        [TestMethod]
        public void CalcolaImportiPermanenti_CalcoloCanone_Corretto()
        {
            var service = new CalcoloCosapConcessioniStradaliService();
            var request = new CalcoloConcessioniStradaliPermanentiRequest("T1", "C1", 4.0d);
            var reader = new FakeTariffeConcessioniStradaliReader();
            var expected = 10.20 * 4.0d;

            var esito = service.CalcolaImportiPermanenti(request, reader);

            Assert.AreEqual<double>(expected, esito.Canone.Importo.Valore);
        }

        [TestMethod]
        public void CalcolaImportiPermanenti_TariffaRidotta50_MostraFormulaCalcoloCanone()
        {
            var service = new CalcoloCosapConcessioniStradaliService();
            var request = new CalcoloConcessioniStradaliPermanentiRequest("T1", "C1", 4.0d);
            var reader = new FakeTariffeConcessioniStradaliReaderRidotte50();
            var expected = @"Tariffa * Quantità
5,10 * 4";

            var esito = service.CalcolaImportiPermanenti(request, reader);

            Assert.AreEqual<string>(expected, esito.Canone.SpiegazioneFormula);
        }

        [TestMethod]
        public void CalcolaImportiPermanenti_TariffaRidotta50_CalcoloCanoneCorretto()
        {
            var service = new CalcoloCosapConcessioniStradaliService();
            var request = new CalcoloConcessioniStradaliPermanentiRequest("T1", "C1", 4.0d);
            var reader = new FakeTariffeConcessioniStradaliReaderRidotte50();
            var expected = 5.10 * 4.0d;

            var esito = service.CalcolaImportiPermanenti(request, reader);

            Assert.AreEqual<double>(expected, esito.Canone.Importo.Valore);
        }

      
    }
}