﻿using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG.Database;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Interfaces.WebControls;
using Init.SIGePro.DatiDinamici.Scripts;
using Init.SIGePro.DatiDinamici.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneServiziFVG
{
    public class FvgDyn2DataAccessFactory : ARDataAccessFactoryBase//IDyn2DataAccessProvider
    {
        private class DummyClassLoader : IClasseContestoLoader
        {
            public IClasseContestoModelloDinamico LoadClass()
            {
                return new Istanze();
            }
        }

        private class DummyQueryLocalizzazioni : IQueryLocalizzazioni
        {
            public IEnumerable<RiferimentiLocalizzazione> Execute(string tipoLocalizzazione, string espressioneFormattazioneDati)
            {
                return Enumerable.Empty<RiferimentiLocalizzazione>();
            }
        }

        private readonly FvgDatabase _database;

        public FvgDyn2DataAccessFactory(ModelloDinamicoCache cache, FvgDatabase database, /* int idModello, IDatiDinamiciRepository datiDinamiciRepository, IIstanzeDyn2DatiManager istanzeDyn2DatiManager, */
                                        ITokenApplicazioneService tokenService )
            :base(cache, tokenService)
        {
            _database = database;
        }

        public override IDyn2DatiRepository GetRepository()
        {
            return new FvgDyn2DatiRepository(this._database);
        }

        public override IClasseContestoLoader GetClassLoader()
        {
            return new DummyClassLoader();
        }

        public override IQueryLocalizzazioni GetQueryLocalizzazioni()
        {
            return new DummyQueryLocalizzazioni();
        }
    }
}
