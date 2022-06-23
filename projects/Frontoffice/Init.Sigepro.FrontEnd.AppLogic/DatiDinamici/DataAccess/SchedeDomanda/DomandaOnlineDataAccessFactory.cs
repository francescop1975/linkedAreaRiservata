using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Interfaces.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.SchedeDomanda
{
    public class DomandaOnlineDataAccessFactory : ARDataAccessFactoryBase
    {
        private readonly DolClasseContestoLoader _classeContestoLoader;
        private readonly DatiRepository _datiRepository;

        public DomandaOnlineDataAccessFactory(ModelloDinamicoCache cache, DomandaOnline domanda, ITokenApplicazioneService tokenApplicazioneService, Istanze istanza)
            : base(cache, tokenApplicazioneService)
        {
            this._classeContestoLoader = new DolClasseContestoLoader(new Lazy<Istanze>(() => istanza));
            this._datiRepository = new DatiRepository(domanda);
        }

        public override IClasseContestoLoader GetClassLoader()
        {
            return this._classeContestoLoader;
        }

        public override IQueryLocalizzazioni GetQueryLocalizzazioni()
        {
            return this._classeContestoLoader.GetQueryLocalizzazioni();
        }

        public override IDyn2DatiRepository GetRepository() => this._datiRepository;

    }
}
