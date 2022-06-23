using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Interfaces.WebControls;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess
{
    public abstract class Dyn2DataAccessFactoryBase : IDyn2DataAccessFactory
    {
        private readonly DataBase _database;

        public Dyn2DataAccessFactoryBase(DataBase database)
        {
            _database = database;
        }

        public IDyn2CampiManager GetCampiManager()
        {
            return new Dyn2CampiMgr(this._database);
        }

        public abstract IClasseContestoLoader GetClassLoader();

        public IDyn2DettagliModelloManager GetDettagliModelloManager()
        {
            return new Dyn2ModelliDMgr(this._database);
        }

        public IDyn2QueryDatiDinamiciManager GetDyn2QueryDatiDinamiciManager()
        {
            return new QuerySigepro(this._database);
        }

        public IDyn2ModelliManager GetModelliManager()
        {
            return new Dyn2ModelliTMgr(this._database);
        }

        public IDyn2ProprietaCampiManager GetProprietaCampiManager()
        {
            return new Dyn2CampiProprietaMgr(this._database);
        }

        public IDyn2ScriptCampiManager GetScriptCampiManager()
        {
            return new Dyn2CampiScriptMgr(this._database);
        }

        public IDyn2ScriptModelloManager GetScriptModelliManager()
        {
            return new Dyn2ModelliScriptMgr(this._database);
        }

        public IDyn2TestoModelloManager GetTestoModelloManager()
        {
            return new Dyn2ModelliDTestiMgr(this._database);
        }

        public string GetToken()
        {
            return this._database.ConnectionDetails.Token;
        }

        public abstract IDyn2DatiRepository GetRepository();
        public abstract IDyn2DatiStoricoRepository GetStoricoRepository(int idVersioneStorico);
        public abstract IQueryLocalizzazioni GetQueryLocalizzazioni();

    }
}
