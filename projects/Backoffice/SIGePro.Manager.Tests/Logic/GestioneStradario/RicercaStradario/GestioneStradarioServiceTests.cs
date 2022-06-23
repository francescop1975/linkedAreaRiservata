using Init.SIGePro.Manager.Logic.GestioneStradario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.GestioneStradario.RicercaStradario
{
    [TestClass]
    public partial class GestioneStradarioServiceTests
    {
        [TestMethod]
        public void Se_non_trova_risultati_restituisce_enumerable_vuoto()
        {
            var svc = new GestioneStradarioService(new FakeStradarioRepository());
            var result = svc.FindByMatchParziale("asd", "asd", "via le mani dal naso");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

        }
    }
}
