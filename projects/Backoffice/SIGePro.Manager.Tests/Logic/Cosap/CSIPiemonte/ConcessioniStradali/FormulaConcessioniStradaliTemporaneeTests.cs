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
    public class FormulaConcessioniStradaliTemporaneeTests
    {
        public class FakeTariffeConcessioniStradaliConElencoFasceVuoto : ITariffeConcessioniStradali
        {
            public Importo Tariffa => new Importo(10.82);

            public IEnumerable<FasciaSuperficie> FasceDiSuperfici => new List<FasciaSuperficie>();
        }

        public class FakeTariffeConcessioniStradaliSenzaFasce : ITariffeConcessioniStradali
        {
            private double _tariffa;
            public FakeTariffeConcessioniStradaliSenzaFasce(double tariffa)
            {
                this._tariffa = tariffa;
            }
            public Importo Tariffa => new Importo(this._tariffa);

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
            int oreOccupazione = 144;
            int quantita = 98;
            Importo tariffa = new Importo(0.07747);

            var tariffe = new FakeTariffeConcessioniStradaliSenzaFasce(tariffa.Valore);
            var request = new CalcoloConcessioniStradaliTemporaneeRequest("T1", "C1", quantita, oreOccupazione);
            

            var formula = new FormulaConcessioniStradaliTemporanee(request, tariffe);
            var expected = $@"Tariffa * Ore occupazione * Quantità
{tariffa} * {oreOccupazione} * {quantita}";

            Assert.AreEqual<string>(expected, formula.Calcola().SpiegazioneFormula, $"Valore atteso: {expected}");
        }

        [TestMethod]
        public void Importo_senzaFasce_tornaImportoCorretto()
        {
            int oreOccupazione = 144;
            int quantita = 98;
            Importo tariffa = new Importo(0.07747);

            var tariffe = new FakeTariffeConcessioniStradaliSenzaFasce(tariffa.Valore);
            var request = new CalcoloConcessioniStradaliTemporaneeRequest("T1", "C1", quantita, oreOccupazione);
            

            var formula = new FormulaConcessioniStradaliTemporanee(request, tariffe);
            var expected = tariffa.Valore * (double)oreOccupazione * (double)quantita;

            Assert.AreEqual<double>(expected, formula.Calcola().Importo.Valore, $"Valore atteso: {expected}");
        }

        [TestMethod]
        public void SpiegazioneFormula_conElencoFasceVuoto_tornaSpiegazioneFormulaCorretta()
        {
            var request = new CalcoloConcessioniStradaliTemporaneeRequest("T1", "C1", 1500, 6);
            var tariffe = new FakeTariffeConcessioniStradaliConElencoFasceVuoto();

            var formula = new FormulaConcessioniStradaliTemporanee(request, tariffe);
            var expected = @"Tariffa * Ore occupazione * Quantità
10,82 * 6 * 1500";

            Assert.AreEqual<string>(expected, formula.Calcola().SpiegazioneFormula, $"Valore atteso: {expected}");
        }


        [TestMethod]
        public void Importo_conElencoFasceVuoto_tornaImportoCorretto()
        {
            var request = new CalcoloConcessioniStradaliTemporaneeRequest("T1", "C1", 1500, 6);
            var tariffe = new FakeTariffeConcessioniStradaliConElencoFasceVuoto();

            var formula = new FormulaConcessioniStradaliTemporanee(request, tariffe);
            var expected = 10.82 * 6.0d * 1500.0d;

            Assert.AreEqual<double>(expected, formula.Calcola().Importo.Valore, $"Valore atteso: {expected}");
        }

        [TestMethod]
        public void SpiegazioneFormula_conFasce_tornaSpiegazioneFormulaCorretta()
        {
            var request = new CalcoloConcessioniStradaliTemporaneeRequest("T1", "C1", 1500, 6);
            var tariffe = new FakeTariffeConcessioniStradaliConFasce();

            var formula = new FormulaConcessioniStradaliTemporanee(request, tariffe);
            var expected = @"[Tariffa * Ore occupazione * (Fascia * Percentuale)]
[10,82 * 6 * (500 * 0,1)] + [10,82 * 6 * (900 * 0,25)] + [10,82 * 6 * (100 * 0,5)]";

            Assert.AreEqual<string>(expected, formula.Calcola().SpiegazioneFormula, $"Valore atteso: {expected}");
        }

        [TestMethod]
        public void Importo_conFasce_tornaImportoCorretto()
        {

            var request = new CalcoloConcessioniStradaliTemporaneeRequest("T1", "C1", 1500, 6);
            var tariffe = new FakeTariffeConcessioniStradaliConFasce();

            var formula = new FormulaConcessioniStradaliTemporanee(request, tariffe);
            var expected = (10.82 * 6.0d * (500.0d * 0.1)) + (10.82 * 6.0d * (900.0d * 0.25)) + (10.82 * 6.0d * (100.0d * 0.5));

            Assert.AreEqual<double>(expected, formula.Calcola().Importo.Valore, $"Valore atteso: {expected}");
        }
    }
}
