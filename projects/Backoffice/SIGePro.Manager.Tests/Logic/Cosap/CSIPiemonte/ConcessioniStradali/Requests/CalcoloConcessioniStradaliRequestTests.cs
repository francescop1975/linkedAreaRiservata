using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Cosap.CSIPiemonte.ConcessioniStradali.Requests
{
    [TestClass]
    public class CalcoloConcessioniStradaliRequestTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_tipoOccupazione__NonPuoEssereStringaVuota()
        {
            var cls = new CalcoloConcessioniStradaliRequest("","B");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_tipoOccupazione__NonPuoEssereNull()
        {
            var cls = new CalcoloConcessioniStradaliRequest(null, "B");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_categoriaStrada__NonPuoEssereStringaVuota()
        {
            var cls = new CalcoloConcessioniStradaliRequest("A", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_categoriaStrada__NonPuoEssereNull()
        {
            var cls = new CalcoloConcessioniStradaliRequest("A",null);
        }
    }
}
