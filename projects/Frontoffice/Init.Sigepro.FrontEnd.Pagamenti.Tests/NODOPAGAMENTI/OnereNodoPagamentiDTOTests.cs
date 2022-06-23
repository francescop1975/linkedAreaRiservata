using Init.Sigepro.FrontEnd.Pagamenti.NODOPAGAMENTI;
using Init.Sigepro.FrontEnd.Pagamenti.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Init.Sigepro.FrontEnd.Pagamenti.Tests.NODOPAGAMENTI
{
    [TestClass]
    public class OnereNodoPagamentiDTOTests
    {
        // public OnereNodoPagamentiDTO(string uniqueId, string codiceCausale, string descrizione, decimal importo)
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_uniqueId_non_puo_essere_null()
        {
            new OnereNodoPagamentiDTO(null, "a", "a", 0, new MockConto());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_uniqueId_non_puo_essere_vuoto()
        {
            new OnereNodoPagamentiDTO("", "a", "a", 0, new MockConto());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_codiceCausale_non_puo_essere_null()
        {
            new OnereNodoPagamentiDTO("a", null, "a", 0, new MockConto());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_codiceCausale_non_puo_essere_vuoto()
        {
            new OnereNodoPagamentiDTO("a", "", "a", 0, new MockConto());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_descrizione_non_puo_essere_null()
        {
            new OnereNodoPagamentiDTO("a", "a", null, 0, new MockConto());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_descrizione_non_puo_essere_vuoto()
        {
            new OnereNodoPagamentiDTO("a", "a", "", 0, new MockConto());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_importo_non_puo_essere_minore_di_0()
        {
            new OnereNodoPagamentiDTO("a", "a", "a", -1, new MockConto());
        }

        [TestMethod]
        public void Ctor_importo_puo_essere_0()
        {
            new OnereNodoPagamentiDTO("a", "a", "a", 0, new MockConto());
        }

        [TestMethod]
        public void Ctor_inizializzazione_argomenti_corretta()
        {
            const string UniqueId = "uniqueId";
            const string CodiceCausale = "codiceCausale";
            const string Descrizione = "descrizione";
            const int Importo = 123;

            var onere = new OnereNodoPagamentiDTO(UniqueId, CodiceCausale, Descrizione, Importo, new MockConto());

            Assert.AreEqual(UniqueId, onere.UniqueId);
            Assert.AreEqual(CodiceCausale, onere.CodiceCausale);
            Assert.AreEqual(Descrizione, onere.Descrizione);
            Assert.AreEqual<decimal>(Importo, onere.Importo);
        }
    }
}
