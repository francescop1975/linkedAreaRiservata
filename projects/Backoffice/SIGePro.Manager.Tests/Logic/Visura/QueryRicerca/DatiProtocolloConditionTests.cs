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
    public class DatiProtocolloConditionTests
    {
        [TestMethod]
        public void DatiProtocolloCondition_imposta_correttamente_il_numero_e_il_nome_dei_parametri()
        {
            var filtro = new FiltroDatiProtocollo
            {
                Data = DateTime.Now,
                Numero = "123"
            };
            var cls = new DatiProtocolloCondition(new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient), filtro);

            cls.VerifyParameters();
        }

        [TestMethod]
        public void DatiProtocolloCondition_se_data_protocollo_non_impostata_cerca_solo_numero()
        {
            var filtro = new FiltroDatiProtocollo
            {
                Numero = "123"
            };
            var cls = new DatiProtocolloCondition(new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient), filtro);

            Assert.AreEqual(1, cls.GetParameters().Count());
            Assert.AreEqual<string>(filtro.Numero, (string)cls.GetParameters().ElementAt(0).Value);
        }

        [TestMethod]
        public void DatiProtocolloCondition_se_numero_protocollo_non_impostata_cerca_solo_data()
        {
            var filtro = new FiltroDatiProtocollo
            {
                Data = new DateTime(2020, 01, 01)
            };
            var cls = new DatiProtocolloCondition(new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient), filtro);

            Assert.AreEqual(1, cls.GetParameters().Count());
            Assert.AreEqual<string>(filtro.Data.Value.ToString("yyyyMMdd"), cls.GetParameters().ElementAt(0).Value.ToString());

        }
    }
}
