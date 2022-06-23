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
    public class CodiceFiscalePersonaAventeTitoloConditionTests
    {
        [TestMethod]
        public void CodiceFiscaleRichiedenteCondition_imposta_correttamente_il_nome_e_il_numero_dei_parametri()
        {
            var filtro = new FiltroPersonaAventeTitoloDiVisura
            {
                // CercaNeiSoggettiCollegati = true,
                CodiceFiscale = "CODICE_FISCALE"
            };
            var cond = new CodiceFiscalePersonaAventeTitoloCondition(new FakeDatabase(PersonalLib2.Data.ProviderType.OracleClient), filtro);

            // Solleva un'eccezione se almeno uno dei parametri impostati non è presente nella query
            cond.VerifyParameters();
        }
    }
}
