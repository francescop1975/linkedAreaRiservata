using Init.SIGePro.Manager.Logic.GestioneStradario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.GestioneStradario
{
    [TestClass]
    public class TrovaAreaDaKmRequestTests
    {
        [TestMethod]
        public void costruttore_passando_i_metri_torna_il_km_corretto() 
        {
            int codiceTipoarea = 1;
            int codiceStradario = 2;
            double km = 1.0d;
            int metri = 100;

            var request = new TrovaAreaDaKmRequest(codiceTipoarea, codiceStradario, km, metri);

            Assert.AreEqual<double>(1.1d, request.Km);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void costruttore_passando_i_km_inferiore_a_zero_genera_argumentexception()
        {
            int codiceTipoarea = 1;
            int codiceStradario = 2;
            double km = -1.0d;
            int metri = 1;

            new TrovaAreaDaKmRequest(codiceTipoarea, codiceStradario, km, metri);

        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException )) ]
        public void costruttore_passando_i_metri_inferiore_a_zero_genera_argumentexception()
        {
            int codiceTipoarea = 1;
            int codiceStradario = 2;
            double km = 1.0d;
            int metri = -1;

            new TrovaAreaDaKmRequest(codiceTipoarea, codiceStradario, km, metri);

        }


    }
}
