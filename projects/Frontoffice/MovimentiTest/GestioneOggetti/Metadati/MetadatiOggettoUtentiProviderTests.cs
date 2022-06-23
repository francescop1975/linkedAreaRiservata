using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestioneAnagrafiche;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti.Metadati;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogicTests.GestioneOggetti.Metadati
{

    public class FakeAuthenticationDataResolver : IAuthenticationDataResolver
    {
        private readonly string _token = "token123";
        private readonly string _idComune = "idcomuneE256";
        private readonly AnagraficaUtente _utente;
        private readonly LivelloAutenticazioneEnum _livello = LivelloAutenticazioneEnum.Anonimo;

        public FakeAuthenticationDataResolver(AnagraficaUtente utente)
        {
            this._utente = utente;
        }

        public UserAuthenticationResult DatiAutenticazione => new UserAuthenticationResult(_token, _idComune, _idComune, _utente, _livello);

        public bool IsAuthenticated => true;
    }

    [TestClass]
    public class MetadatiOggettoUtentiProviderTests
    {
        private static class Constants
        {
            public const string Nominativo = "MITTENTE_NOMINATIVO";
            public const string Nome = "MITTENTE_NOME";
            public const string CodiceFiscale = "MITTENTE_CODICEFISCALE";
            public const string Partitaiva = "MITTENTE_PARTITAIVA";
        }

        [TestMethod]
        public void SeMetadatiNonPresentiTornaIEnumerableVuoto() 
        {
            var utente = new AnagraficaUtente();
            var m = new MetadatiOggettoUtenteProvider(new FakeAuthenticationDataResolver(utente)).Metadati;
            Assert.IsTrue(m.Count() == 0);
        }

        [TestMethod]
        public void SeNominativoUtentePresenteTornaMetadatoNominativo() 
        {
            var utente = new AnagraficaUtente
            {
                Nominativo = "MENDICHI",
            };

            var m = new MetadatiOggettoUtenteProvider(new FakeAuthenticationDataResolver(utente)).Metadati;
            var nominativo = m.Where(x => x.Chiave == Constants.Nominativo).Select(x => x.Valore).FirstOrDefault();
            Assert.AreEqual("MENDICHI", nominativo);
        }

        [TestMethod]
        public void SeNomeUtentePresenteTornaMetadatoNome()
        {
            var utente = new AnagraficaUtente
            {
                Nome = "STEFANO",
            };

            var m = new MetadatiOggettoUtenteProvider(new FakeAuthenticationDataResolver(utente)).Metadati;
            var nome = m.Where(x => x.Chiave == Constants.Nome).Select(x => x.Valore).FirstOrDefault();
            Assert.AreEqual("STEFANO", nome);
        }

        [TestMethod]
        public void SeCFUtentePresenteTornaMetadatoCF()
        {
            var utente = new AnagraficaUtente
            {
                Codicefiscale = "MNDSFN83L26E230I",
            };

            var m = new MetadatiOggettoUtenteProvider(new FakeAuthenticationDataResolver(utente)).Metadati;
            var codiceFiscale = m.Where(x => x.Chiave == Constants.CodiceFiscale).Select(x => x.Valore).FirstOrDefault();
            Assert.AreEqual("MNDSFN83L26E230I", codiceFiscale);
        }

        [TestMethod]
        public void SePIvaUtentePresenteTornaMetadatoPIva()
        {
            var utente = new AnagraficaUtente
            {
                Partitaiva = "12345678901",
            };

            var m = new MetadatiOggettoUtenteProvider(new FakeAuthenticationDataResolver(utente)).Metadati;
            var partitaIva = m.Where(x => x.Chiave == Constants.Partitaiva).Select(x => x.Valore).FirstOrDefault();
            Assert.AreEqual("12345678901", partitaIva);
        }

        [TestMethod]
        public void SePartitaIvaUtenteAssenteNonCompareNeiMetadati()
        {
            var utente = new AnagraficaUtente
            {
                Nominativo = "MENDICHI",
                Nome = "STEFANO",
                Codicefiscale = "MNDSFN83L26E230I",
            };

            var m = new MetadatiOggettoUtenteProvider(new FakeAuthenticationDataResolver(utente)).Metadati;
            var partitaiva = m.Where(x => x.Chiave == Constants.Partitaiva).Select(x => x.Valore).FirstOrDefault();
            Assert.IsNull(partitaiva);
        }
    }
}
