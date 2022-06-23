using Init.SIGePro.Manager.Logic.GestioneElaborazioneMassiva.SchedeIstanza;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace SIGePro.Manager.Tests.Logic.GestioneElaborazioneMassiva.SchedeIstanza
{
    [TestClass]
    public partial class ElaborazioneMassivaSchedeIstanzaServiceTests
    {

        public class ElaborazioneModelloScriptNullRunner : IElaborazioneModelloScriptRunner
        {
            private int _codiceIstanzaConEccezione = -1;
            private int _codiceSchedaConEccezione = -1;

            public void SollevaEccezioneSuCodiceIstanza(int codiceIstanza, int codiceScheda)
            {
                this._codiceIstanzaConEccezione = codiceIstanza;
                this._codiceSchedaConEccezione = codiceScheda;
            }

            public void EseguiScriptElaborazioneMassiva(int codiceIstanza, int idScheda)
            {
                if(codiceIstanza == _codiceIstanzaConEccezione && idScheda == _codiceSchedaConEccezione)
                {
                    throw new Exception("Eccezione sollevata dal runner di test");
                }

                return;
            }
        }


        ElaborazioneMassivaSchedeFakeDao _fakeDao;
        ElaborazioneModelloScriptNullRunner _runner;

        [TestInitialize]
        public void TestInitialize()
        {
            _fakeDao = new ElaborazioneMassivaSchedeFakeDao();
            _runner = new ElaborazioneModelloScriptNullRunner();
        }

        [TestMethod]
        public void Verifica_esecuzione()
        {
            var svc = new ElaborazioneMassivaSchedeIstanzaService(_fakeDao, _runner);

            var esito = svc.Elabora(1);

            Assert.AreEqual(EsitoElaborazioneMassivaSchedeEnum.ElaborazioneCompletataConSuccesso, esito.Esito);
            Assert.AreEqual(2, esito.ElementiElaborati);
            Assert.AreEqual(0, esito.ElementiConErrore);
        }

        [TestMethod]
        public void Verifica_scheda_con_errore()
        {
            var svc = new ElaborazioneMassivaSchedeIstanzaService(_fakeDao, _runner);

            this._runner.SollevaEccezioneSuCodiceIstanza(1, 1);

            var esito = svc.Elabora(1);

            Assert.AreEqual(EsitoElaborazioneMassivaSchedeEnum.ElaborazioneCompletataConErrori, esito.Esito);
            Assert.AreEqual(2, esito.ElementiElaborati);
            Assert.AreEqual(1, esito.ElementiConErrore);
        }
    }
}
