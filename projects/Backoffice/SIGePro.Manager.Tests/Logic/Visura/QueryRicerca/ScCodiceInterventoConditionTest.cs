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
    public class ScCodiceInterventoConditionTest
    {
        [TestMethod]
        public void ScCodiceInterventoCondition_imposta_correttamente_il_numero_e_il_nome_dei_parametri()
        {
            var cls = new ScCodiceInterventoCondition(new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient), "01");

            cls.VerifyParameters();
        }
    }
}
