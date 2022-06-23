using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Permanenti;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Permanenti
{
    [TestClass]
    public class CalcoloMezziPermanentiRequestTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_MezziPermanenti_NonPuoEssereMinoreDi0()
        {
            new CalcoloMezziPermanentiRequest(-1, "A", "B", "C", "D", false, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_TipoImpiantoMezziPermanenti_NonPuoEssereNull()
        {
            new CalcoloMezziPermanentiRequest(1, null, "B", "C", "D", false, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_TipoImpiantoMezziPermanenti_NonPuoEssereVuoto()
        {
            new CalcoloMezziPermanentiRequest(1, "", "B", "C", "D", false, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_ClasseDimensionePermanenti_NonPuoEssereNull()
        {
            new CalcoloMezziPermanentiRequest(1, "A", null, "C", "D", false, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_ClasseDimensionePermanenti_NonPuoEssereVuoto()
        {
            new CalcoloMezziPermanentiRequest(1, "A", "", "C", "D", false, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_CategoriaStradaPermanenti_NonPuoEssereNull()
        {
            new CalcoloMezziPermanentiRequest(1, "A", "B", null, "D", false, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_CategoriaStradaPermanenti_NonPuoEssereVuoto()
        {
            new CalcoloMezziPermanentiRequest(1, "A", "B", "", "D", false, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_OccupazioneStradaPermanenti_NonPuoEssereNull()
        {
            new CalcoloMezziPermanentiRequest(1, "A", "B", "C", null, false, true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_OccupazioneStradaPermanenti_NonPuoEssereVuoto()
        {
            new CalcoloMezziPermanentiRequest(1, "A", "B", "C", "", false, true);
        }

    }
}
