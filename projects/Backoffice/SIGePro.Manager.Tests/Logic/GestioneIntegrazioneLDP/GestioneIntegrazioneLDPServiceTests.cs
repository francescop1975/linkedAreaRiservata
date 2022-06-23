using Init.SIGePro.Authentication;
using Init.SIGePro.Data;
using Init.SIGePro.Manager.IOC;
using Init.SIGePro.Manager.Logic.GestioneDomandaOnLine;
using Init.SIGePro.Manager.Logic.GestioneIntegrazioneLDP;
using Init.SIGePro.Manager.Verticalizzazioni;
using Init.SIGePro.Verticalizzazioni;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vbg.EventBus.IOC;

namespace SIGePro.Manager.Tests.Logic.GestioneIntegrazioneLDP
{
    [TestClass]
    public class GestioneIntegrazioneLDPServiceTests
    {
        public class FakeLdpServiceWrapper : ILdpAnnullamentoServiceWrapper
        {
            public bool DeleteOccupazioneChiamato { get; private set; } = false;
            public bool ExistOccupazionePrenotataChiamato { get; private set; } = false;

            public bool RisultatoExistOccupazionePrenotata;

            public FakeLdpServiceWrapper(bool risultatoExistOccupazionePrenotata)
            {
                this.RisultatoExistOccupazionePrenotata = risultatoExistOccupazionePrenotata;
            }

            public void EliminaOccupazione(string idPratica)
            {
                this.DeleteOccupazioneChiamato = true;
            }

            public bool EsisteUnOccupazionePrenotata(string identificativo_temporaneo)
            {
                this.ExistOccupazionePrenotataChiamato = true;

                return RisultatoExistOccupazionePrenotata;
            }
        }

        public class FakeCheckVerticalizzazioneSitLdpAttiva : IVerticalizzazioneAttiva<VerticalizzazioneSitLdp>
        {
            public bool RisultatoIsAttiva = false;

            public FakeCheckVerticalizzazioneSitLdpAttiva(bool risultatoIsAttiva)
            {
                this.RisultatoIsAttiva = risultatoIsAttiva;
            }

            public bool IsAttiva(string software)
            {
                return this.RisultatoIsAttiva;
            }
        }

        public class FakeDomandaOnlineService : IDomandaOnlineService
        {
            public FoDomande RisultatoGetById;

            public FakeDomandaOnlineService(FoDomande risultatoGetById)
            {
                this.RisultatoGetById = risultatoGetById;
            }

            public void EliminaDomanda(int idDomanda)
            {
                throw new NotImplementedException();
            }

            public FoDomande GetById(int idDomanda)
            {
                return this.RisultatoGetById;
            }

            public void RecuperaDocumentiIstanzaCollegata(int codiceIstanzaOrigine, int idDomandaDestinazione)
            {
                throw new NotImplementedException();
            }
        }

        public class FakeLdpProxyServiceWrapperFactory : ILdpProxyServiceWrapperFactory
        {
            public readonly FakeLdpServiceWrapper Service;

            public FakeLdpProxyServiceWrapperFactory(bool risultatoExistOccupazionePrenotata)
            {
                this.Service = new FakeLdpServiceWrapper(risultatoExistOccupazionePrenotata);
            }

            public ILdpAnnullamentoServiceWrapper GetAnnullamentoService(string software)
            {
                return this.Service;
            }
        }

        [TestMethod]
        public void Verifica_che_la_configurazione_di_ninject_sia_valida()
        {
            var kernel = new StandardKernel();

            kernel.Load(new SigeproManagerNinjectModule());
            kernel.Load(new EventBusIOCModule());

            Assert.IsInstanceOfType(kernel.Get<GestioneIntegrazioneLDPService>(), typeof(GestioneIntegrazioneLDPService));
        }

        [TestMethod]
        public void AnnullaPrenotazione_non_fa_niente_se_verticalizzazioneSitLdp_non_attiva()
        {
            var serviceFatory = new FakeLdpProxyServiceWrapperFactory(false);
            var checkVerticalizzazione = new FakeCheckVerticalizzazioneSitLdpAttiva(false);

            var domandeService = new FakeDomandaOnlineService(new FoDomande { Software = "SS" });

            var cls = new GestioneIntegrazioneLDPService(serviceFatory, domandeService, checkVerticalizzazione);
            var idDomanda = 123;

            cls.AnnullaPrenotazione(idDomanda);

            Assert.IsFalse(serviceFatory.Service.ExistOccupazionePrenotataChiamato);
            Assert.IsFalse(serviceFatory.Service.DeleteOccupazioneChiamato);
        }

        [TestMethod]
        public void AnnullaPrenotazione_invoca_ExistOccupazionePrenotata_e_DeleteOccupazione_se_verticalizzazione_attiva()
        {
            var serviceFatory = new FakeLdpProxyServiceWrapperFactory(true);
            var checkVerticalizzazione = new FakeCheckVerticalizzazioneSitLdpAttiva(true);

            var domandeService = new FakeDomandaOnlineService(new FoDomande{Software = "SS"});
            
            var cls = new GestioneIntegrazioneLDPService(serviceFatory, domandeService, checkVerticalizzazione);
            var idDomanda = 123;

            cls.AnnullaPrenotazione(idDomanda);

            Assert.IsTrue(serviceFatory.Service.ExistOccupazionePrenotataChiamato);
            Assert.IsTrue(serviceFatory.Service.DeleteOccupazioneChiamato);
        }

        [TestMethod]
        public void AnnullaPrenotazione_non_invoca_DeleteOccupazione_se_ExistOccupazionePrenotata_restituisce_false()
        {
            var serviceFatory = new FakeLdpProxyServiceWrapperFactory(false);
            var checkVerticalizzazione = new FakeCheckVerticalizzazioneSitLdpAttiva(true);

            var domandeService = new FakeDomandaOnlineService(new FoDomande { Software = "SS" });

            var cls = new GestioneIntegrazioneLDPService(serviceFatory, domandeService, checkVerticalizzazione);
            var idDomanda = 123;

            cls.AnnullaPrenotazione(idDomanda);

            Assert.IsTrue(serviceFatory.Service.ExistOccupazionePrenotataChiamato);
            Assert.IsFalse(serviceFatory.Service.DeleteOccupazioneChiamato);
        }
    }
}
