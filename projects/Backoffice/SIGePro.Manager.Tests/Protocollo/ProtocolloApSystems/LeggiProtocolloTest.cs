using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Protocollo.ProtocolloApSystems
{
    [TestClass]
    public class LeggiProtocolloTest
    {

        [TestMethod]
        public void FormattaData()
        {
            string data = "02/19/2019 00:00:00";

            var dataProtocollo = DateTime.ParseExact(data, "MM/dd/yyyy hh:mm:ss", null).ToString("dd/MM/yyyy");
            var annoProtocollo = DateTime.ParseExact(data, "MM/dd/yyyy hh:mm:ss", null).ToString("yyyy");

            Assert.AreEqual("19/02/2019", dataProtocollo);
            Assert.AreEqual("2019", annoProtocollo);

        }
    }
}
