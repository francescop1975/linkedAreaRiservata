using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Interfaces.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common
{
    public abstract class ARDataAccessFactoryBase : IDyn2DataAccessFactory
    {
        protected readonly ModelloDinamicoCache _cache;
        protected readonly ITokenApplicazioneService _tokenApplicazioneService;

        public ARDataAccessFactoryBase(ModelloDinamicoCache cache, ITokenApplicazioneService tokenApplicazioneService)
        {
            _cache = cache;
            _tokenApplicazioneService = tokenApplicazioneService;
        }


        public virtual IDyn2CampiManager GetCampiManager()
        {
            return new IstanzaDyn2CampiManager(this._cache);
        }

        public virtual IDyn2DettagliModelloManager GetDettagliModelloManager()
        {
            return new DettagliModelloManager(this._cache);
        }

        public virtual IDyn2QueryDatiDinamiciManager GetDyn2QueryDatiDinamiciManager()
        {
            throw new NotImplementedException();
        }

        public virtual IDyn2ModelliManager GetModelliManager()
        {
            return new ModelliManager(this._cache);
        }

        public virtual IDyn2ProprietaCampiManager GetProprietaCampiManager()
        {
            return new ProprietaCampiManager(this._cache);
        }

        public virtual IDyn2ScriptCampiManager GetScriptCampiManager()
        {
            return new ScriptCampiManager(this._cache);
        }

        public virtual IDyn2ScriptModelloManager GetScriptModelliManager()
        {
            return new ScriptModelloManager(this._cache);
        }
        public virtual IDyn2TestoModelloManager GetTestoModelloManager()
        {
            return new TestoModelloManager(this._cache);
        }

        public virtual string GetToken()
        {
            return this._tokenApplicazioneService.GetToken();
        }

        public abstract IClasseContestoLoader GetClassLoader();
        public abstract IDyn2DatiRepository GetRepository();
        public abstract IQueryLocalizzazioni GetQueryLocalizzazioni();

        #region non usati
        public virtual IDyn2DatiStoricoRepository GetStoricoRepository(int idVersioneStorico)
        {
            return new DummyDatiStoricoRepository();
        }


        #endregion
    }
}
