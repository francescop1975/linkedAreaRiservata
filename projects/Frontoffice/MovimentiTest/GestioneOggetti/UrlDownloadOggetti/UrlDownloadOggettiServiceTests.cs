using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.UrlDownloadOggetti;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogicTests.GestioneOggetti.UrlDownloadOggetti
{
    [TestClass]
    public class UrlDownloadOggettiServiceTests
    {
        private IAliasSoftwareResolver _aliasResolver = new StaticAliasSoftwareResolver("DEF", "SS");

        [TestMethod]
        public void genera_url_con_checksum()
        {
            var cdp = new StaticCurrentDateTimeProvider(new DateTime(2020, 5, 19, 12, 00, 00));
            var str = new StaticTokenResolver("token");
            var sks = new StaticDownloadOggettiSecretKeyService("secret");

            var urlService = new UrlDownloadOggettiService(cdp, str, sks, _aliasResolver);

            var actual = urlService.GetUrlDownload(123);
            var expected = "~/oggetti/download.ashx?id=DEF.123.202005191200.1a4b411793694a0e4022e595324458a080058444696b3f4e15fd347ff483f787";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(FileChecksumValidationException))]
        public void se_checksum_non_valido_solleva_eccezione()
        {
            var cdp = new StaticCurrentDateTimeProvider(new DateTime(2020, 5, 19, 12, 00, 00));
            var str = new StaticTokenResolver("token");
            var sks = new StaticDownloadOggettiSecretKeyService("secret");

            var urlService = new UrlDownloadOggettiService(cdp, str, sks, _aliasResolver);

            var encrypted = "DEF.123.202005191200.asdasdasd";
            
            urlService.GetCodiceOggettoDaEncryptedString(encrypted);
        }

        [TestMethod]
        [ExpectedException(typeof(FileChecksumValidationException))]
        public void se_data_fuori_da_finestra_sicurezza_valida_solleva_eccezione()
        {
            // Checksum creato il 19/05/2020 alle 12.00
            var encrypted = "DEF.123.202005191200.1a4b411793694a0e4022e595324458a080058444696b3f4e15fd347ff483f787";
            // La finestra di validità di default è di 20 minuti
            var cdp = new StaticCurrentDateTimeProvider(new DateTime(2020, 5, 19, 12, 20, 01));
            var str = new StaticTokenResolver("token");
            var sks = new StaticDownloadOggettiSecretKeyService("secret");

            var urlService = new UrlDownloadOggettiService(cdp, str, sks, _aliasResolver);

            urlService.GetCodiceOggettoDaEncryptedString(encrypted);
        }

        [TestMethod]
        public void se_checksum_valido_restituisce_codice_oggetto()
        {
            var cdp = new StaticCurrentDateTimeProvider(new DateTime(2020, 5, 19, 12, 00, 00));
            var str = new StaticTokenResolver("token");
            var sks = new StaticDownloadOggettiSecretKeyService("secret");

            var urlService = new UrlDownloadOggettiService(cdp, str, sks, _aliasResolver);

            var encrypted = "DEF.123.202005191200.1a4b411793694a0e4022e595324458a080058444696b3f4e15fd347ff483f787";
            var actual = urlService.GetCodiceOggettoDaEncryptedString(encrypted);
            var expected = new CodiceOggettoDownload("DEF", 123);

            Assert.AreEqual(expected, actual);
        }


    }
}
