using Init.SIGePro.Manager.Logic.GestioneStradario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.GestioneStradario
{
    [TestClass]
    public class TrovaAreaDaKmAKmRequestTests
    {
        [TestMethod]
        public void costruttore_passando_i_dati_corretti_torna_i_kmDa_corretti()
        {
            int codiceTipoarea = 1;
            int codiceStradario = 2;
            double kmDa = 1.0d;
            int metriDa = 100;
            double kmA = 2.0d;
            int metriA = 100;

            var request = new TrovaAreaDaKmAKmRequest(codiceTipoarea, codiceStradario, kmDa, metriDa, kmA, metriA);

            Assert.AreEqual<double>(1.1d, request.KmDa);
        }

        [TestMethod]
        public void costruttore_passando_i_dati_corretti_torna_i_kmA_corretti()
        {
            int codiceTipoarea = 1;
            int codiceStradario = 2;
            double kmDa = 1.0d;
            int metriDa = 100;
            double kmA = 2.0d;
            int metriA = 100;

            var request = new TrovaAreaDaKmAKmRequest(codiceTipoarea, codiceStradario, kmDa, metriDa, kmA, metriA);

            Assert.AreEqual<double>(2.1d, request.KmA);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void costruttore_passando_i_kmDa_inferiore_a_zero_genera_argumentexception()
        {
            int codiceTipoarea = 1;
            int codiceStradario = 2;
            double kmDa = -1.0d;
            int metriDa = 100;
            double kmA = 2.0d;
            int metriA = 100;

            new TrovaAreaDaKmAKmRequest(codiceTipoarea, codiceStradario, kmDa, metriDa, kmA, metriA);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void costruttore_passando_i_metriDa_inferiore_a_zero_genera_argumentexception()
        {
            int codiceTipoarea = 1;
            int codiceStradario = 2;
            double kmDa = 1.0d;
            int metriDa = -100;
            double kmA = 2.0d;
            int metriA = 100;

            new TrovaAreaDaKmAKmRequest(codiceTipoarea, codiceStradario, kmDa, metriDa, kmA, metriA);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void costruttore_passando_i_kmA_inferiore_a_zero_genera_argumentexception()
        {
            int codiceTipoarea = 1;
            int codiceStradario = 2;
            double kmDa = 1.0d;
            int metriDa = 100;
            double kmA = -2.0d;
            int metriA = 100;

            new TrovaAreaDaKmAKmRequest(codiceTipoarea, codiceStradario, kmDa, metriDa, kmA, metriA);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void costruttore_passando_i_metriA_inferiore_a_zero_genera_argumentexception()
        {
            int codiceTipoarea = 1;
            int codiceStradario = 2;
            double kmDa = 1.0d;
            int metriDa = 100;
            double kmA = 2.0d;
            int metriA = -100;

            new TrovaAreaDaKmAKmRequest(codiceTipoarea, codiceStradario, kmDa, metriDa, kmA, metriA);
        }
    }
}
