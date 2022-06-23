using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni;
using Init.SIGePro.DatiDinamici.Interfaces;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Anagrafe
{
    public class AnagrafeDyn2DataAccessFactory: Dyn2DataAccessFactoryBase
    {
        public class ClassLoader : IClasseContestoLoader
        {
            private readonly int _id;
            private readonly string _idComune;
            private readonly DataBase _db;

            internal ClassLoader(DataBase db, string idComune, int id)
            {
                _id = id;
                _idComune = idComune;
                _db = db;
            }

            public IClasseContestoModelloDinamico LoadClass()
            {
                return new AnagrafeMgr(this._db).GetById(this._idComune, this._id);
            }
        }

        private readonly DataBase _database;
        private readonly string _idComune;
        private readonly int _idAnagrafe;

        public AnagrafeDyn2DataAccessFactory(DataBase database, string idComune, int idAnagrafe)
            : base(database)
        {
            _database = database;
            _idComune = idComune;
            _idAnagrafe = idAnagrafe;
        }

        public override IClasseContestoLoader GetClassLoader()
        {
            return new ClassLoader(this._database, this._idComune, this._idAnagrafe);
        }

        public override IQueryLocalizzazioni GetQueryLocalizzazioni()
        {
            throw new NotImplementedException();
        }

        public override IDyn2DatiRepository GetRepository()
        {
            return new AnagrafeDyn2DatiRepository(this._database, this._idAnagrafe,this._idComune);
        }

        public override IDyn2DatiStoricoRepository GetStoricoRepository(int idVersioneStorico)
        {
            return new AnagrafeStoricoDyn2DatiRepository(this._database, this._idAnagrafe, this._idComune, idVersioneStorico);
        }
    }
}
