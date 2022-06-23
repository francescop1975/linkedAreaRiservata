using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.SchedeDomanda;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Interfaces.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Visura
{
    public class VisuraFoDataAccessFactory : ARDataAccessFactoryBase
    {
        public class DummyIstanza : IClasseContestoModelloDinamico
        {

        }

        private class DummyClassLoader : IClasseContestoLoader
        {
            public IClasseContestoModelloDinamico LoadClass()
            {
                return new DummyIstanza();
            }
        }

        private readonly Istanze _istanza;

        public VisuraFoDataAccessFactory(ModelloDinamicoCache cache, Istanze istanza, ITokenApplicazioneService tokenService)
            :base(cache, tokenService)
        {
            _istanza = istanza;
        }


        public override IClasseContestoLoader GetClassLoader()
        {
            return new DummyClassLoader();
        }

        public override IDyn2DatiRepository GetRepository()
        {
            return new VisuraDatiRepository(this._istanza);
        }

        public override IQueryLocalizzazioni GetQueryLocalizzazioni()
        {
            throw new NotImplementedException();
        }
    }
}
