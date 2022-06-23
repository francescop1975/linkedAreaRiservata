using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni;
using Init.SIGePro.DatiDinamici.Interfaces;
using PersonalLib2.Data;
using PersonalLib2.Sql;

namespace Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Istanze
{
    public class IstanzeDyn2DataAccessFactory: Dyn2DataAccessFactoryBase
    {
        public class ClassLoader : IClasseContestoLoader
        {
            private readonly int _codiceIstanza;
            private readonly string _idComune;
            private readonly DataBase _db;

            internal ClassLoader(DataBase db, string idComune, int codiceIstanza)
            {
                _codiceIstanza = codiceIstanza;
                _idComune = idComune;
                _db = db;
            }

            public IClasseContestoModelloDinamico LoadClass()
            {
                return new IstanzeMgr(this._db).GetById(this._idComune, this._codiceIstanza, useForeignEnum.Recoursive);
            }
        }


        private readonly DataBase _database;
        private readonly string _idComune;
        private readonly int _codiceIstanza;

        public IstanzeDyn2DataAccessFactory(DataBase database, string idComune, int codiceIstanza)
            :base(database)
        {
            _database = database;
            _idComune = idComune;
            _codiceIstanza = codiceIstanza;
        }

        public override IClasseContestoLoader GetClassLoader()
        {
            return new ClassLoader(this._database, this._idComune, this._codiceIstanza);
        }

        public override IQueryLocalizzazioni GetQueryLocalizzazioni()
        {
            return new QueryLocalizzazioni(this._database, this._codiceIstanza, this._idComune);
        }

        public override IDyn2DatiRepository GetRepository()
        {
            return new IstanzeDyn2DatiRepository(this._database, this._codiceIstanza, this._idComune);
        }

        public override IDyn2DatiStoricoRepository GetStoricoRepository(int idVersioneStorico)
        {
            return new IstanzeStoricoDyn2DatiRepository(this._database, this._idComune, this._codiceIstanza, idVersioneStorico);
        }
    }
}
