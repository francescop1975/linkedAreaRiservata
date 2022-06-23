using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TokenApplicazione;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Init.SIGePro.DatiDinamici.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.SchedeDomanda
{
    public class DomandaOnlineDataAccessFactoryMolt: DomandaOnlineDataAccessFactory
    {
        private readonly int _indiceMolteplicita;

        //public DomandaOnlineDataAccessFactoryMolt(ModelloDinamicoCache cache, DomandaOnline domanda, ITokenApplicazioneService tokenApplicazioneService, int indiceMolteplicita, IIstanzaSigeproAdapterService istanzaSigeproAdapterService)
        //    :base(cache, domanda, tokenApplicazioneService, istanzaSigeproAdapterService)
        //{
        //    _indiceMolteplicita = indiceMolteplicita;
        //}

        public DomandaOnlineDataAccessFactoryMolt(ModelloDinamicoCache cache, DomandaOnline domanda, ITokenApplicazioneService tokenApplicazioneService, int indiceMolteplicita, Istanze istanza)
    : base(cache, domanda, tokenApplicazioneService, istanza)
        {
            _indiceMolteplicita = indiceMolteplicita;
        }

        public override IDyn2DatiRepository GetRepository()
        {
            return new StampaMolteplicitaRepository(base.GetRepository(),this._indiceMolteplicita, base._cache);
        }
    }
}
