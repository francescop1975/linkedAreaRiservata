using Init.SIGePro.Data;
using Init.SIGePro.Manager.Logic.PagamentiParma;
using Init.SIGePro.Manager.WsParmaPagamenti;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.PagamentiParma
{
    [TestClass]
    public class TestsRateizzazioneEmissioniParma
    {
        public class PagamentiStubCodiceIdentificativo : IPagamentiParmaProxy
        {
            public string CodiceIdentificativo;

            public DtoEsitoPagameti InserisciEmissioniAvvisoPagoPA(int codiceIdentificativo, RateizzazioneEmissioniParmaBase emissioniFactory)
            {
                this.CodiceIdentificativo = codiceIdentificativo.ToString();

                return null;
            }

            public DtoOutInserimentoEmissioni InserisciEmissioniByCodiceAnno(string codiceIdentificativo, RateizzazioneEmissioniParmaBase emissioniFactory)
            {
                this.CodiceIdentificativo = codiceIdentificativo;

                return null;
            }
        }

        [TestMethod]
        public void Codice_identificativo_CosapDehors_viene_decodificato_con_6()
        {
            var rateizzazione = new RateizzazioneEmissioniParma(TipologiaCosapParma.CosapDehors, DateTime.Now, null, new PeriodoCosap(DateTime.Now, DateTime.Now), 1, null, null);
            var proxyStub = new PagamentiStubCodiceIdentificativo();

            rateizzazione.Notifica(proxyStub);

            Assert.AreEqual<string>("6", proxyStub.CodiceIdentificativo);
        }

        [TestMethod]
        public void Codice_identificativo_Cosap_viene_decodificato_con_5()
        {
            var rateizzazione = new RateizzazioneEmissioniParma(TipologiaCosapParma.Cosap, DateTime.Now, null, new PeriodoCosap(DateTime.Now, DateTime.Now), 1, null, null);
            var proxyStub = new PagamentiStubCodiceIdentificativo();

            rateizzazione.Notifica(proxyStub);

            Assert.AreEqual<string>("5", proxyStub.CodiceIdentificativo);
        }

        [TestMethod]
        public void Codice_identificativo_CosapGenerica_viene_decodificato_con_30()
        {
            var rateizzazione = new RateizzazioneEmissioniParma(TipologiaCosapParma.CosapGenerica, DateTime.Now, null, new PeriodoCosap(DateTime.Now, DateTime.Now), 1, null, null);
            var proxyStub = new PagamentiStubCodiceIdentificativo();

            rateizzazione.Notifica(proxyStub);

            Assert.AreEqual<string>("30", proxyStub.CodiceIdentificativo);
        }

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
            var periodo = new PeriodoCosap(new DateTime(2019,12,18), new DateTime(2019, 12, 19));
            var totaleGiorni = 1;

            var rateizzazione = new RateizzazioneEmissioniParma(TipologiaCosapParma.CosapGenerica, DateTime.Now, ISTANZA_TEST, periodo, totaleGiorni, "", Enumerable.Empty<RataCosap>());
            var emissione = rateizzazione.GetEmissioni()[0];
            var expected = "Cosap Generica sita in VIA NOMEVIA 1/A periodo dal 18/12/2019 al 19/12/2019 (1 giorni) per ROSSI MARIO (PF) - C.F. XXXXXXXX (richiedente), AZIENDA (PG) - P.I. 12345678901";

            Assert.AreEqual<string>(expected, emissione.Descrizione);
        }

        [TestMethod]
        public void DescrizioneEmissione_contiene_dati_occupazione()
        {
            var periodo = new PeriodoCosap(new DateTime(2019, 12, 18), new DateTime(2019, 12, 19));
            var totaleGiorni = 1;
            var datiOccupazione = "dati occupazione";
            var rateizzazione = new RateizzazioneEmissioniParma(TipologiaCosapParma.CosapGenerica, DateTime.Now, ISTANZA_TEST, periodo, totaleGiorni, datiOccupazione, Enumerable.Empty<RataCosap>());
            var emissione = rateizzazione.GetEmissioni()[0];
            var expected = "Cosap Generica sita in VIA NOMEVIA 1/A periodo dal 18/12/2019 al 19/12/2019 (1 giorni) dati occupazione per ROSSI MARIO (PF) - C.F. XXXXXXXX (richiedente), AZIENDA (PG) - P.I. 12345678901";

            Assert.AreEqual<string>(expected, emissione.Descrizione);
        }


        [TestMethod]
        public void DescrizioneEmissione_viene_popolata_con_dati_richiedente_e_localizzazione()
        {
            var periodo = new PeriodoCosap(new DateTime(2019, 12, 18), new DateTime(2019, 12, 19));
            var totaleGiorni = 1;

            ISTANZA_TEST.AziendaRichiedente = null;

            var rateizzazione = new RateizzazioneEmissioniParma(TipologiaCosapParma.CosapGenerica, DateTime.Now, ISTANZA_TEST, periodo, totaleGiorni, "", Enumerable.Empty<RataCosap>());
            var emissione = rateizzazione.GetEmissioni()[0];
            var expected = "Cosap Generica sita in VIA NOMEVIA 1/A periodo dal 18/12/2019 al 19/12/2019 (1 giorni) per ROSSI MARIO (PF) - C.F. XXXXXXXX (richiedente)";

            Assert.AreEqual<string>(expected, emissione.Descrizione);
        }

        [TestMethod]
        public void Genera_un_emissione_con_2_righe_e_3_rate()
        {
            var periodo = new PeriodoCosap(DateTime.Now, DateTime.Now);
            var totaleGiorni = 1;

            var rate = new[] {
                new RataCosap(new DateTime(2019, 01, 01),100),
                new RataCosap(new DateTime(2019, 01, 02),50),
                new RataCosap(new DateTime(2019, 01, 03),25),
            };

            ISTANZA_TEST.AziendaRichiedente = null;

            var rateizzazione = new RateizzazioneEmissioniParma(TipologiaCosapParma.CosapGenerica, DateTime.Now, ISTANZA_TEST, periodo, totaleGiorni, "", rate);
            var emissione = rateizzazione.GetEmissioni()[0];

            Assert.AreEqual<int>(3, emissione.Rate.Count());
            Assert.AreEqual<int>(2, emissione.Righe.Count());

            Assert.AreEqual<int>(ISTANZA_TEST.DATA.Value.Year, emissione.Rate[0].AnnoRiferimento);
            Assert.AreEqual<decimal>(100, emissione.Rate[0].Importo);
            Assert.AreEqual<DateTime>(new DateTime(2019, 01, 01), emissione.Rate[0].DataScadenza.Value);

            Assert.AreEqual<int>(ISTANZA_TEST.DATA.Value.Year, emissione.Rate[1].AnnoRiferimento);
            Assert.AreEqual<decimal>(50, emissione.Rate[1].Importo);
            Assert.AreEqual<DateTime>(new DateTime(2019, 01, 02), emissione.Rate[1].DataScadenza.Value);

            Assert.AreEqual<int>(ISTANZA_TEST.DATA.Value.Year, emissione.Rate[2].AnnoRiferimento);
            Assert.AreEqual<decimal>(25, emissione.Rate[2].Importo);
            Assert.AreEqual<DateTime>(new DateTime(2019, 01, 03), emissione.Rate[2].DataScadenza.Value);

            Assert.AreEqual<string>("TXT", emissione.Righe[0].CausaleRiga);
            Assert.AreEqual<string>(emissione.Descrizione, emissione.Righe[0].Descrizione);

            Assert.AreEqual<string>("IMP", emissione.Righe[1].CausaleRiga);
            Assert.AreEqual<decimal>(175, emissione.Righe[1].Imponibile);
            Assert.AreEqual<decimal>(175, emissione.Righe[1].Imposta);
        }
    }
}
