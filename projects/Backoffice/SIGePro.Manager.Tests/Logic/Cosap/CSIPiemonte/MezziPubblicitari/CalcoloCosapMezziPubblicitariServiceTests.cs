using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte;
using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante;
using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari;
using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Permanenti;
using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Temporanei;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Cosap.CSIPiemonte.MezziPubblicitari
{
    [TestClass]
    public class CalcoloCosapMezziPubblicitariServiceTests
    {

        public class FakeTariffeMezziPermanentiReader : ITariffeMezziPubblicitariReader
        {


            public ITariffaMezziPubblicitari GetTariffe()
            {
                return new TariffaMezziPubblicitariPermamenti(10.0d, 1.3d, 1.2d);
            }

        }

        public class FakeTariffeMezziTemporaneiReader : ITariffeMezziPubblicitariReader
        {


            public ITariffaMezziPubblicitari GetTariffe()
            {
                return new TariffaMezziPubblicitariTemporanei(20.0d, 2.6d, 2.4d);
            }

        }

        [TestMethod]
        public void CalcolaImporti_MezziPermanenti_MostraFormulaCalcoloCanone()
        {
            var service = new CalcoloCosapMezziPubblicitariService();
            var request = new CalcoloMezziPermanentiRequest(5, "A", "B", "C", "D", true, true);
            var reader = new FakeTariffeMezziPermanentiReader();
            var expected = "10,00 * 5 * 1,30 * 1,20";

            var esito = service.CalcolaImporti(request, reader);

            Assert.AreEqual<string>(expected, esito.Canone.SpiegazioneFormula);
        }

        [TestMethod]
        public void CalcolaImporti_MezziPermanenti_Corretto()
        {
            var service = new CalcoloCosapMezziPubblicitariService();
            var request = new CalcoloMezziPermanentiRequest(5, "A", "B", "C", "D", true, true);
            var reader = new FakeTariffeMezziPermanentiReader();
            var expected = 10.0d * 5.0d * 1.3d * 1.2d;

            var esito = service.CalcolaImporti(request, reader);

            Assert.AreEqual<double>(expected, esito.Canone.Importo.Valore);
        }



        [TestMethod]
        public void CalcolaImporti_MezziTemporanei_MostraFormulaCalcoloCanone()
        {
            var service = new CalcoloCosapMezziPubblicitariService();
            var request = new CalcoloMezziTemporaneiRequest(15, "A", "B", "C", "D", true, true,25);
            var reader = new FakeTariffeMezziTemporaneiReader();
            var expected = "20,00 * 15 * 2,60 * 2,40 * 25";

            var esito = service.CalcolaImporti(request, reader);

            Assert.AreEqual<string>(expected, esito.Canone.SpiegazioneFormula);
        }

        [TestMethod]
        public void CalcolaImporti_MezziTemporanei_Corretto()
        {
            var service = new CalcoloCosapMezziPubblicitariService();
            var request = new CalcoloMezziTemporaneiRequest(15, "A", "B", "C", "D", true, true, 25);
            var reader = new FakeTariffeMezziTemporaneiReader();
            var expected = 20.0d * 15.0d * 2.6d * 2.4d * 25.0d;

            var esito = service.CalcolaImporti(request, reader);

            Assert.AreEqual<double>(expected, esito.Canone.Importo.Valore);
        }
    }
}
