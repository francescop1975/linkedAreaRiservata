using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari;
using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Temporanei;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Temporanei
{
    [TestClass]
    public class CalcoloMezziTemporaneiRequestTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_NumeroMezziTemporanei_NonPuoEssereMinoreDi0()
        {
            new CalcoloMezziTemporaneiRequest(-1, "A", "B", "C", "D", false, true, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_TipoImpiantoMezziTemporanei_NonPuoEssereNull()
        {
            new CalcoloMezziTemporaneiRequest(1, null, "B", "C", "D", false, true, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_TipoImpiantoMezziTemporanei_NonPuoEssereVuoto()
        {
            new CalcoloMezziTemporaneiRequest(1, "", "B", "C", "D", false, true, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_ClasseDimensioneTemporanei_NonPuoEssereNull()
        {
            new CalcoloMezziTemporaneiRequest(1, "A", null, "C", "D", false, true, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_ClasseDimensioneTemporanei_NonPuoEssereVuoto()
        {
            new CalcoloMezziTemporaneiRequest(1, "A", "", "C", "D", false, true, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_CategoriaStradaTemporanei_NonPuoEssereNull()
        {
            new CalcoloMezziTemporaneiRequest(1, "A", "B", null, "D", false, true, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_CategoriaStradaTemporanei_NonPuoEssereVuoto()
        {
            new CalcoloMezziTemporaneiRequest(1, "A", "B", "", "D", false, true, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_OccupazioneStradaTemporanei_NonPuoEssereNull()
        {
            new CalcoloMezziTemporaneiRequest(1, "A", "B", "C", null, false, true, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_OccupazioneStradaTemporanei_NonPuoEssereVuoto()
        {
            new CalcoloMezziTemporaneiRequest(1, "A", "B", "C", "", false, true, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_NumeroGiorniTemporanei_NonPuoEssereMinoreDi0()
        {
            new CalcoloMezziTemporaneiRequest(1, "A", "B", "C", "D", false, true, -1);
        }
    }
}
