using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.EntraNext;
using Init.Sigepro.FrontEnd.AppLogic.RicercheAnagraficheWebService;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using Init.Sigepro.FrontEnd.AppLogic.Utils.SerializationExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.EntraNext.PagamentiEntraNextService;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneFixDatiModena
{
    public class FixDatiModenaService
    {
        private readonly AreaRiservataServiceCreator _areaRiservataServiceCreator;
        private readonly SigeproSecurityProxy _sigeproSecurityProxy;
        private readonly RicercaAnagraficheServiceCreator _anagraficheServiceCreator;
        private readonly IAliasSoftwareResolver _aliasSoftwareResolver;
        private readonly DomandeOnlineService _domandeOnlineService;
        private readonly ISalvataggioDomandaStrategy _salvataggioDomandaStrategy;
        private readonly ITokenApplicazioneService _tokenApplicazioneService;

        internal FixDatiModenaService(IAliasSoftwareResolver aliasSoftwareResolver, AreaRiservataServiceCreator areaRiservataServiceCreator, SigeproSecurityProxy sigeproSecurityProxy,
            RicercaAnagraficheServiceCreator anagraficheServiceCreator, DomandeOnlineService domandeOnlineService, ISalvataggioDomandaStrategy salvataggioDomandaStrategy,
            ITokenApplicazioneService tokenApplicazioneService)
        {
            _areaRiservataServiceCreator = areaRiservataServiceCreator;
            _sigeproSecurityProxy = sigeproSecurityProxy;
            _anagraficheServiceCreator = anagraficheServiceCreator;
            _aliasSoftwareResolver = aliasSoftwareResolver;
            _domandeOnlineService = domandeOnlineService;
            _salvataggioDomandaStrategy = salvataggioDomandaStrategy;
            _tokenApplicazioneService = tokenApplicazioneService;
        }


        public string LoginAs(string username, string password)
        {
            return _sigeproSecurityProxy.GetToken(_aliasSoftwareResolver.AliasComune, username, password, SigeproSecurityService.ContestoType.UTE);
        }


        public void SetContextIdentity(int codiceAnagrafe)
        {
            using (var ws = this._areaRiservataServiceCreator.CreateClient())
            {
                var a = ws.Service.GetAnagraficaById(ws.Token, codiceAnagrafe);

                var token = _tokenApplicazioneService.GetToken();
                var adapter = new AnagrafeAdapter(a);

                var uar = new UserAuthenticationResult(token, _aliasSoftwareResolver.AliasComune, _aliasSoftwareResolver.AliasComune, adapter.ToAnagraficaUtente(), LivelloAutenticazioneEnum.Identificato);

                HttpContext.Current.Items["UserAuthenticationResult"] = uar;
            }
        }

        public RicercheAnagraficheWebService.Anagrafe CercaAnagrafe(string codiceFiscale)
        {
            using (var ws = _anagraficheServiceCreator.CreateClient(GestionePresentazioneDomanda.GestioneAnagrafiche.TipoPersonaEnum.Fisica))
            {
                var anagrafe = ws.Service.getPersonaFisica(ws.Token, codiceFiscale);

                if (anagrafe != null)
                {
                    return anagrafe;
                }

            }

            return null;
        }

        public string ModificaPassword(string username, string password, string nuovaPassword)
        {

            using (var ws = _areaRiservataServiceCreator.CreateClient())
            {
                var anagrafe = CercaAnagrafe(username);

                ws.Service.ModificaPasswordAnagrafica(ws.Token, Convert.ToInt32(anagrafe.CODICEANAGRAFE), password, nuovaPassword);

                return _sigeproSecurityProxy.GetToken(_aliasSoftwareResolver.AliasComune, username, nuovaPassword, SigeproSecurityService.ContestoType.UTE);
            }
        }

        public DatiDomandaModena GetDomandaById(int idDomanda)
        {
            var ds = _salvataggioDomandaStrategy.GetById(idDomanda);

            return new DatiDomandaModena
            {
                Domanda = null,
                OneriInSospeso = ds.ReadInterface.Oneri.GetOneriOnlineConPagamentoAvviato(),
                OneriTotali = ds.ReadInterface.Oneri.Oneri,
                DatiExtra = ds.ReadInterface.DatiExtra.GetValoreDato(PagamentiEntraNextService.Constants.DatiPagamentiExtra).DeserializeXML<PagamentiDatiExtra>(),
                Intervento = ds.ReadInterface.AltriDati.Intervento?.Descrizione
            };
        }

        public IEnumerable<DatiDomandaModena> GetDomandeInSospeso(int codiceAnagrafe)
        {
            var domande = _domandeOnlineService.GetDomandeInSospeso(codiceAnagrafe);

            return domande.Select(x =>
            {

                var ds = _salvataggioDomandaStrategy.GetById(x.Id.Value);

                return new DatiDomandaModena
                {
                    Domanda = x,
                    OneriTotali = ds.ReadInterface.Oneri.Oneri,
                    OneriInSospeso = ds.ReadInterface.Oneri.GetOneriOnlineConPagamentoAvviato(),
                    DatiExtra = ds.ReadInterface.DatiExtra.GetValoreDato(PagamentiEntraNextService.Constants.DatiPagamentiExtra).DeserializeXML<PagamentiDatiExtra>(),
                    Intervento = ds.ReadInterface.AltriDati.Intervento?.Descrizione
                };
            });
        }

        public void AggiornaDatiPagamento(int idDomanda, string idTransazione, string iuv, string codicePagamento)
        {
            var ds = _salvataggioDomandaStrategy.GetById(idDomanda);

            ds.WriteInterface.DatiExtra.SetValoreDato(PagamentiEntraNextService.Constants.DatiPagamentiExtra, new PagamentiDatiExtra
            {
                IdTransazione = idTransazione,
                CodicePagamento = codicePagamento,
                Iuv = iuv
            }.ToXmlString());

            ds.WriteInterface.Oneri.ForzaModificaIdPagamentoOnline(codicePagamento);

            this._salvataggioDomandaStrategy.Salva(ds);
        }

        public void EliminaDomandaInSospeso(int idDomanda, string cfUtente)
        {
            this._domandeOnlineService.Elimina(idDomanda, cfUtente);
        }
    }
}
