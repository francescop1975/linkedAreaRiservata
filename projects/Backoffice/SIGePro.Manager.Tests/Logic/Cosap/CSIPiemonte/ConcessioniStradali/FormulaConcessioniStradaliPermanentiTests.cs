using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte;
using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Cosap.CSIPiemonte.ConcessioniStradali
{
    [TestClass]
    public class FormulaConcessioniStradaliPermanentiTests
    {
        public class FakeTariffeConcessioniStradaliConElencoFasceVuoto : ITariffeConcessioniStradali
        {
            public Importo Tariffa => new Importo(10.82);

            public IEnumerable<FasciaSuperficie> FasceDiSuperfici => new List<FasciaSuperficie>();
        }

        public class FakeTariffeConcessioniStradaliSenzaFasce : ITariffeConcessioniStradali
        {
            public Importo Tariffa => new Importo(10.82);

            public IEnumerable<FasciaSuperficie> FasceDiSuperfici => null;
        }

        public class FakeTariffeConcessioniStradaliConFasce : ITariffeConcessioniStradali
        {
            public Importo Tariffa => new Importo(10.82);

            public IEnumerable<FasciaSuperficie> FasceDiSuperfici => new List<FasciaSuperficie> { 
                new FasciaSuperficie( "FS01", 1000, 0.1 ),
                new FasciaSuperficie( "FS02", 100, 0.25 ),
                new FasciaSuperficie( "FS03", 0, 0.5 )
            };
        }

        [TestMethod]
        public void SpiegazioneFormula_senzaFasce_tornaSpiegazioneFormulaCorretta() 
        {
            var request = new CalcoloConcessioniStradaliPermanentiRequest("T1", "C1", 1500);
            var tariffe = new FakeTariffeConcessioniStradaliSenzaFasce();

            var formula = new FormulaConcessioniStradaliPermanenti(request, tariffe);
            var expected = @"Tariffa * Quantità
10,82 * 1500";

            Assert.AreEqual<string>(expected, formula.Calcola().SpiegazioneFormula, $"Valore atteso: {expected}");
        }

        [TestMethod]
        public void Importo_senzaFasce_tornaImportoCorretto()
        {
            var request = new CalcoloConcessioniStradaliPermanentiRequest("T1", "C1", 1500);
            var tariffe = new FakeTariffeConcessioniStradaliSenzaFasce();

            var formula = new FormulaConcessioniStradaliPermanenti(request, tariffe);
            var expected = 10.82 * 1500.0d;

            Assert.AreEqual<double>(expected, formula.Calcola().Importo.Valore, $"Valore atteso: {expected}");
        }


        [TestMethod]
        public void SpiegazioneFormula_conElencoFasceVuoto_tornaSpiegazioneFormulaCorretta()
        {
            var request = new CalcoloConcessioniStradaliPermanentiRequest("T1", "C1", 1500);
            var tariffe = new FakeTariffeConcessioniStradaliConElencoFasceVuoto();

            var formula = new FormulaConcessioniStradaliPermanenti(request, tariffe);
            var expected = @"Tariffa * Quantità
10,82 * 1500";

            Assert.AreEqual<string>(expected, formula.Calcola().SpiegazioneFormula, $"Valore atteso: {expected}");
        }

        [TestMethod]
        public void Importo_conElencoFasceVuoto_tornaImportoCorretto()
        {
            var request = new CalcoloConcessioniStradaliPermanentiRequest("T1", "C1", 1500);
            var tariffe = new FakeTariffeConcessioniStradaliConElencoFasceVuoto();

            var formula = new FormulaConcessioniStradaliPermanenti(request, tariffe);
            var expected = 10.82 * 1500.0d;

            Assert.AreEqual<double>(expected, formula.Calcola().Importo.Valore, $"Valore atteso: {expected}");
        }


        [TestMethod]
        public void SpiegazioneFormula_conFasce_tornaSpiegazioneFormulaCorretta()
        {
            var request = new CalcoloConcessioniStradaliPermanentiRequest("T1", "C1", 1500);
            var tariffe = new FakeTariffeConcessioniStradaliConFasce();

            var formula = new FormulaConcessioniStradaliPermanenti(request, tariffe);
            var expected = @"[Tariffa * (Fascia * Percentuale)]
[10,82 * (500 * 0,1)] + [10,82 * (900 * 0,25)] + [10,82 * (100 * 0,5)]";

            Assert.AreEqual<string>(expected, formula.Calcola().SpiegazioneFormula, $"Valore atteso: {expected}");
        }

        [TestMethod]
        public void Importo_conFasce_tornaImportoCorretto() 
        {

            var request = new CalcoloConcessioniStradaliPermanentiRequest("T1", "C1", 1500);
            var tariffe = new FakeTariffeConcessioniStradaliConFasce();

            var formula = new FormulaConcessioniStradaliPermanenti(request, tariffe);
            var expected = (10.82 * (500.0d * 0.1)) + (10.82 * (900.0d * 0.25)) + (10.82 * (100.0d * 0.5));

            Assert.AreEqual<double>(expected, formula.Calcola().Importo.Valore, $"Valore atteso: {expected}");
        }
    }
}
