using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Cosap.CSIPiemonte.ConcessioniStradali.Requests
{
    [TestClass]
    public class CalcoloConcessioniStradaliMQRequestTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_tipoOccupazione__NonPuoEssereMinoreDi0()
        {
            var cls = new CalcoloConcessioniStradaliPermanentiRequest("", "B",-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_tipoOccupazione__NonPuoEssereStringaVuota()
        {
            var cls = new CalcoloConcessioniStradaliPermanentiRequest("", "B", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_tipoOccupazione__NonPuoEssereNull()
        {
            var cls = new CalcoloConcessioniStradaliPermanentiRequest(null, "B", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_categoriaStrada__NonPuoEssereStringaVuota()
        {
            var cls = new CalcoloConcessioniStradaliPermanentiRequest("A", "", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_categoriaStrada__NonPuoEssereNull()
        {
            var cls = new CalcoloConcessioniStradaliPermanentiRequest("A", null, 0);
        }
    }
}
