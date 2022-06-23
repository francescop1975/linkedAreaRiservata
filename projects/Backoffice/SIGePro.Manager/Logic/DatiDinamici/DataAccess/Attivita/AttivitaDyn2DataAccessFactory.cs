using Init.SIGePro.Authentication;
using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.Manager.Logic.GestioneSchedeAttivita;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Attivita
{
    public class AttivitaDyn2DataAccessFactory : Dyn2DataAccessFactoryBase
    {
        public class ClassLoader : IClasseContestoLoader
        {
            private readonly AuthenticationInfo _authenticationInfo;
            private readonly int _id;

            internal ClassLoader(AuthenticationInfo authenticationInfo, int codiceAnagrafe)
            {
                _authenticationInfo = authenticationInfo;
                _id = codiceAnagrafe;
            }

            public IClasseContestoModelloDinamico LoadClass()
            {
                using (var db = _authenticationInfo.CreateDatabase())
                {
                    return new IAttivitaMgr(db).GetById(_authenticationInfo.IdComune, this._id);
                }
            }
        }

        private readonly AuthenticationInfo _authenticationInfo;
        private readonly ISchedeDinamicheAttivitaService _schedeDinamicheAttivitaService;
        private readonly int _idAttivita;

        public AttivitaDyn2DataAccessFactory(AuthenticationInfo authenticationInfo, ISchedeDinamicheAttivitaService schedeDinamicheAttivitaService, int idAttivita)
            : base(authenticationInfo.CreateDatabase())
        {
            _authenticationInfo = authenticationInfo;
            _schedeDinamicheAttivitaService = schedeDinamicheAttivitaService;
            _idAttivita = idAttivita;
        }

        public override IClasseContestoLoader GetClassLoader()
        {
            return new ClassLoader(this._authenticationInfo, this._idAttivita);
        }

        public override IQueryLocalizzazioni GetQueryLocalizzazioni()
        {
            throw new NotImplementedException();
        }

        public override IDyn2DatiRepository GetRepository()
        {
            return new AttivitaDyn2DatiRepository(this._schedeDinamicheAttivitaService, this._idAttivita);
        }

        public override IDyn2DatiStoricoRepository GetStoricoRepository(int idVersioneStorico)
        {
            return new AttivitaStoricoDyn2DatiRepository(this._schedeDinamicheAttivitaService, this._idAttivita, idVersioneStorico);
        }
    }
}
