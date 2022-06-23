using Init.SIGePro.Data;
using Init.SIGePro.Manager.Logic.PagamentiParma;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.PagamentiParma
{
    [TestClass]
    public class TestRateizzazioniImpiantiPubblicitari
    {
        static Istanze ISTANZA_TEST;

        [TestInitialize]
        public void InizializzaIstanza()
        {
            ISTANZA_TEST = new Istanze
            {
                NUMEROISTANZA = "123",
                DATA = new DateTime(2001, 01, 01),
                CODICEISTANZA = "123",
                Richiedente = new Anagrafe
                {
                    NOMINATIVO = "ROSSI",
                    NOME = "MARIO",
                    CODICEFISCALE = "XXXXXXXX",
                    TIPOANAGRAFE = "F"
                },
                AziendaRichiedente = new Anagrafe
                {
                    NOMINATIVO = "AZIENDA",
                    PARTITAIVA = "12345678901",
                    TIPOANAGRAFE = "G"
                },
                Stradario = new List<IstanzeStradario>
                {
                    new IstanzeStradario
                    {
                        CIVICO = "1",
                        ESPONENTE = "A",
                        PRIMARIO = "1",
                        Stradario = new Stradario
                        {
                            PREFISSO = "VIA",
                            DESCRIZIONE = "NOMEVIA"
                        }
                    }
                }
            };
        }

        [TestMethod]
        public void DescrizioneEmissione_viene_popolata_con_dati_richiedente_azienda_e_localizzazione()
        {
            var rateizzazione = new RateizzazioneEmissioniParmaImpiantiPubblicitari(TipologiaCosapParmaPagoPA.CosapGenerica, ISTANZA_TEST, "Note", Enumerable.Empty<RataCosap>(), false);
            var emissione = rateizzazione.GetEmissioni()[0];
            var expected = "Imposta di Pubblicità sita in VIA NOMEVIA 1/A per ROSSI MARIO (PF) - C.F. XXXXXXXX (richiedente), AZIENDA (PG) - P.I. 12345678901 Note";

            Assert.AreEqual<string>(expected, emissione.Descrizione);
        }
    }
}
