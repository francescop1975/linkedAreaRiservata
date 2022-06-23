﻿using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.Services.Domanda;
using System;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.CondizioniUscitaSteps
{
    public class ComuneDiPresentazioneSelezionato : CondizioneUscitaStepBase, ICondizioneUscitaStep
    {
        public ComuneDiPresentazioneSelezionato(IIdDomandaResolver idDomandaResolver, DomandeOnlineService domandeService)
            : base(idDomandaResolver, domandeService)
        {

        }

        #region ICondizioneUscitaStep Members

        public bool Verificata()
        {
            var domanda = base.Domanda;

            return !String.IsNullOrEmpty(domanda.ReadInterface.AltriDati.CodiceComune);
        }

        #endregion
    }
}