using Init.SIGePro.Manager.Logic.Visura;
using Init.SIGePro.Manager.Logic.Visura.QueryRicerca;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGePro.Manager.Tests.Fakes.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.Visura.QueryRicerca
{
    [TestClass]
    public class IndirizzoConditionTests
    {
        [TestMethod]
        public void IndirizzoCondition_imposta_correttamente_il_numero_e_il_nome_dei_parametri()
        {
            var filtroIndirizzo = new FiltroIndirizzo
            {
                Civico = "12",
                CodiceStradario = 123
            };
            var cls = new IndirizzoCondition(new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient), filtroIndirizzo);

            cls.VerifyParameters();
        }

        [TestMethod]
        public void IndirizzoCondition_se_civico_non_impostato_cerca_solo_per_codice_stradario()
        {
            var filtroIndirizzo = new FiltroIndirizzo
            {
                CodiceStradario = 123
            };
            var cls = new IndirizzoCondition(new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient), filtroIndirizzo);

            cls.VerifyParameters();

            Assert.AreEqual(1, cls.GetParameters().Count());
            Assert.AreEqual<int>(filtroIndirizzo.CodiceStradario, (int)cls.GetParameters().ElementAt(0).Value);
        }
    }
}
