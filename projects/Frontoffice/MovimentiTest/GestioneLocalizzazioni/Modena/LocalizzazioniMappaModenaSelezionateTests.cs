using Init.Sigepro.FrontEnd.AppLogic.GestioneLocalizzazioni.Modena;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogicTests.GestioneLocalizzazioni.Modena
{
    [TestClass]
    public class LocalizzazioniMappaModenaSelezionateTests
    {
        [TestMethod]
        public void Deserializzazione_da_stringa_json()
        {
            var stringaJson = @"{ ""particelleGrafiche"":
                                    [
                                        {
                                            ""codCatastale"":""F257"",
                                            ""foglio"":""7"",
                                            ""particella"":""4""
                                        },
                                        {
                                            ""codCatastale"":""F257"",
                                            ""foglio"":""7"",
                                            ""particella"":""16""
                                        }
                                    ],
                                    ""particelleManuali"":[
                                        {
                                            ""codCatastale"":""F257"",
                                            ""foglio"":""7"",
                                            ""particella"":""ZZ""
                                        },
                                    ]}";

            var result = LocalizzazioniMappaModenaSelezionate.FromJsonString(stringaJson);

            Assert.AreEqual<int>(2, result.ParticelleGrafiche.Length);
            Assert.AreEqual<int>(1, result.ParticelleManuali.Length);

            Assert.AreEqual("F257", result.ParticelleGrafiche[0].CodCatastale);
            Assert.AreEqual("7", result.ParticelleGrafiche[0].Foglio);
            Assert.AreEqual("4", result.ParticelleGrafiche[0].Particella);

            Assert.AreEqual("F257", result.ParticelleGrafiche[1].CodCatastale);
            Assert.AreEqual("7", result.ParticelleGrafiche[1].Foglio);
            Assert.AreEqual("16", result.ParticelleGrafiche[1].Particella);

            Assert.AreEqual("F257", result.ParticelleManuali[0].CodCatastale);
            Assert.AreEqual("7", result.ParticelleManuali[0].Foglio);
            Assert.AreEqual("ZZ", result.ParticelleManuali[0].Particella);
        }

        [TestMethod]
        public void No_npe_su_ParticelleTotali_se_non_sono_presenti_particelle()
        {
            var stringaJson = @"{ ""particelleGrafiche"": [], ""particelleManuali"":[]}";

            var result = LocalizzazioniMappaModenaSelezionate.FromJsonString(stringaJson);

            Assert.AreEqual(0, result.ParticelleTotali.Count());
        }

        [TestMethod]
        public void No_npe_su_ParticelleTotali_se_non_e_presente_un_elemento()
        {
            var stringaJson = @"{ ""particelleGrafiche"": [{
                                            ""codCatastale"":""F257"",
                                            ""foglio"":""7"",
                                            ""particella"":""4""
                                        }]}";

            var result = LocalizzazioniMappaModenaSelezionate.FromJsonString(stringaJson);

            Assert.AreEqual(1, result.ParticelleTotali.Count());
        }
    }
}
