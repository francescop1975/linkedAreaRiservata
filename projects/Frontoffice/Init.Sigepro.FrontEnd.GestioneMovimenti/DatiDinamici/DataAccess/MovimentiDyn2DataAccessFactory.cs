using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Interfaces.WebControls;
using Init.Sigepro.FrontEnd.GestioneMovimenti.GestioneMovimento.GestioneMovimentoDiOrigine;
using Init.Sigepro.FrontEnd.GestioneMovimenti.GestioneMovimento.GestioneMovimentoDaEffettuare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.Infrastructure.Dispatching;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;

namespace Init.Sigepro.FrontEnd.GestioneMovimenti.DatiDinamici.DataAccess
{
    public class MovimentiDyn2DataAccessFactory : IDyn2DataAccessFactory
    {

        public class ClassLoader : IClasseContestoLoader
        {
            private readonly int _codiceIstanza;
            private readonly MovimentiIstanzeManager _istanzeManager;

            public ClassLoader(MovimentiIstanzeManager istanzeManager, int codiceIstanza)
            {
                _codiceIstanza = codiceIstanza;
                _istanzeManager = istanzeManager;
            }

            public IClasseContestoModelloDinamico LoadClass()
            {
                return this._istanzeManager.LeggiIstanza(this._codiceIstanza);
            }
        }

        private readonly ModelloDinamicoCache _cacheModelloDinamico;
        private readonly MovimentoDiOrigine _movimentoDiOrigine;
        private readonly MovimentoDaEffettuare _movimentoDaEffettuare;
        private readonly ICommandSender _bus;
        private readonly ITokenApplicazioneService _tokenService;
        private readonly MovimentiIstanzeManager _istanzeManager;

        public MovimentiDyn2DataAccessFactory(ModelloDinamicoCache cacheModelloDinamico, MovimentoDiOrigine movimentoDiOrigine,
                                        MovimentoDaEffettuare movimentoDaEffettuare, ICommandSender bus, 
                                        ITokenApplicazioneService tokenService, MovimentiIstanzeManager istanzeManager)
        {
            _cacheModelloDinamico = cacheModelloDinamico;
            _movimentoDiOrigine = movimentoDiOrigine;
            _movimentoDaEffettuare = movimentoDaEffettuare;
            _bus = bus;
            _tokenService = tokenService;
            _istanzeManager = istanzeManager;
        }

        public IDyn2CampiManager GetCampiManager()
        {
            return new Dyn2CampiManager(this._cacheModelloDinamico);
        }

        public IClasseContestoLoader GetClassLoader()
        {
            return new ClassLoader(this._istanzeManager, this._movimentoDaEffettuare.CodiceIstanza);
        }

        public IDyn2DettagliModelloManager GetDettagliModelloManager()
        {
            return new Dyn2DettagliModelloManager(this._cacheModelloDinamico);
        }

        public IDyn2QueryDatiDinamiciManager GetDyn2QueryDatiDinamiciManager()
        {
            throw new NotImplementedException();
        }

        public IDyn2ModelliManager GetModelliManager()
        {
            return new Dyn2ModelliManager(this._cacheModelloDinamico);
        }

        public IDyn2ProprietaCampiManager GetProprietaCampiManager()
        {
            return new Dyn2ProprietaCampiManager(this._cacheModelloDinamico);
        }

        public IQueryLocalizzazioni GetQueryLocalizzazioni()
        {
            var istanza = (Istanze)this._istanzeManager.LeggiIstanza(this._movimentoDaEffettuare.CodiceIstanza);

            return new QueryLocalizzazioni(istanza);
        }

        public IDyn2DatiRepository GetRepository()
        {
            return new IstanzeDyn2DatiManager(this._movimentoDaEffettuare, this._movimentoDiOrigine.SchedeDinamiche, this._bus);
        }

        public IDyn2ScriptCampiManager GetScriptCampiManager()
        {
            return new Dyn2ScriptCampiManager(this._cacheModelloDinamico);
        }

        public IDyn2ScriptModelloManager GetScriptModelliManager()
        {
            return new Dyn2ScriptModelloManager(this._cacheModelloDinamico);
        }

        public IDyn2DatiStoricoRepository GetStoricoRepository(int idVersioneStorico)
        {
            throw new NotImplementedException();
        }

        public IDyn2TestoModelloManager GetTestoModelloManager()
        {
            return new Dyn2TestoModelloManager(this._cacheModelloDinamico);
        }

        public string GetToken()
        {
            return this._tokenService.GetToken();
        }
    }
}
