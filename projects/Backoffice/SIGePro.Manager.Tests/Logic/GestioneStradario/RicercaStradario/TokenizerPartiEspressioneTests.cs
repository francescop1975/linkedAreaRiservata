using Init.SIGePro.Manager.Logic.GestioneStradario.RicercaStradario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.GestioneStradario.RicercaStradario
{
    [TestClass]
    public class TokenizerPartiEspressioneTests
    {
        [TestMethod]
        public void Separa_indirizzo_in_token()
        {
            var indirizzo = "uno due tre";
            var tokens = new TokenizerPartiEspressione(indirizzo).GetTokens();

            Assert.AreEqual(3, tokens.Count());
            Assert.AreEqual("uno", tokens.ElementAt(0));
            Assert.AreEqual("due", tokens.ElementAt(1));
            Assert.AreEqual("tre", tokens.ElementAt(2));
        }

        [TestMethod]
        public void Esclude_principali_toponimi()
        {
            var indirizzo = String.Join(" ", TokenizerPartiEspressione.PAROLE_RISERVATE) + " uno";

            var tokens = new TokenizerPartiEspressione(indirizzo).GetTokens();

            Assert.AreEqual(1, tokens.Count());
            Assert.AreEqual("uno", tokens.ElementAt(0));
        }

        [TestMethod]
        public void Se_contiene_puntato_viene_trattato_come_un_token_a_se_stante()
        {
            var indirizzo = "via S.Uno";
            var tokens = new TokenizerPartiEspressione(indirizzo).GetTokens();

            Assert.AreEqual(2, tokens.Count());
            Assert.AreEqual("S.", tokens.ElementAt(0));
            Assert.AreEqual("Uno", tokens.ElementAt(1));
        }
    }
}
