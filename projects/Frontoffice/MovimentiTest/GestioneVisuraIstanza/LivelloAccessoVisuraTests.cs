using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogicTests.GestioneVisuraIstanza
{
    [TestClass]
    public class LivelloAccessoVisuraTests
    {
        private static class Constants
        {
            public const int DatiGenerali = 1 << 0;
            public const int Schede = 1 << 1;
            public const int Documenti = 1 << 2;
            public const int Endoprocedimenti = 1 << 3;
            public const int Oneri = 1 << 4;
            public const int MovimentiEffettuati = 1 << 5;
            public const int Autorizzazioni = 1 << 6;
        }

        [TestMethod]
        public void AccessoDatiGenerali()
        {
            var livello = LivelloAccessoVisura.DaValoreFlag(Constants.DatiGenerali);

            Assert.IsTrue(livello.DatiGenerali);
        }

        [TestMethod]
        public void AccessoSchede()
        {
            var livello = LivelloAccessoVisura.DaValoreFlag(Constants.Schede);

            Assert.IsTrue(livello.Schede);
        }

        [TestMethod]
        public void AccessoDocumenti()
        {
            var livello = LivelloAccessoVisura.DaValoreFlag(Constants.Documenti);

            Assert.IsTrue(livello.Documenti);
        }

        [TestMethod]
        public void AccessoEndoprocedimenti()
        {
            var livello = LivelloAccessoVisura.DaValoreFlag(Constants.Endoprocedimenti);

            Assert.IsTrue(livello.Endoprocedimenti);
        }

        [TestMethod]
        public void AccessoOneri()
        {
            var livello = LivelloAccessoVisura.DaValoreFlag(Constants.Oneri);

            Assert.IsTrue(livello.Oneri);
        }

        [TestMethod]
        public void AccessoMovimentiEffettuati()
        {
            var livello = LivelloAccessoVisura.DaValoreFlag(Constants.MovimentiEffettuati);

            Assert.IsTrue(livello.MovimentiEffettuati);
        }

        [TestMethod]
        public void AccessoAutorizzazioni()
        {
            var livello = LivelloAccessoVisura.DaValoreFlag(Constants.Autorizzazioni);

            Assert.IsTrue(livello.Autorizzazioni);
        }

        [TestMethod]
        public void AccessoCompleto()
        {
            var livello = LivelloAccessoVisura.Completo;
            
            Assert.IsTrue(livello.DatiGenerali);
            Assert.IsTrue(livello.Schede);
            Assert.IsTrue(livello.Documenti);
            Assert.IsTrue(livello.Endoprocedimenti);
            Assert.IsTrue(livello.Oneri);
            Assert.IsTrue(livello.MovimentiEffettuati);
            Assert.IsTrue(livello.Autorizzazioni);
        }

        [TestMethod]
        public void Verifica_operatore_di_uguaglianza()
        {
            var v1 = LivelloAccessoVisura.DaValoreFlag(Constants.Autorizzazioni + Constants.Documenti);
            var v2 = LivelloAccessoVisura.DaValoreFlag(Constants.Documenti + Constants.Autorizzazioni);

            var result = v1 == v2;

            Assert.IsTrue(result);

            v2 = LivelloAccessoVisura.DaValoreFlag(Constants.Autorizzazioni);
            result = v1 == v2;

            Assert.IsFalse(result);

            result = v1 != v2;

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestSerializzazione_e_deserializzazione()
        {
            var v1 = LivelloAccessoVisura.AccessoAnonimo;
            var code = v1.ToSerializationCode();
            var v2 = LivelloAccessoVisura.FromSerializationCode(code);
            var result = v1 == v2;

            Assert.IsTrue(result);
        }
    }
}
