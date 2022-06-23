﻿using CuttingEdge.Conditions;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo.LetturaDatiDinamici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo
{
    internal class SegnapostoSchedeDinamiche : ISegnapostoRiepilogo
    {
        private readonly IGeneratoreHtmlSchedeDinamiche _generatoreHtml;
        private readonly IConfigurazione<ParametriGenerazioneRiepilogoDomanda> _parametriRiepilogo;

        internal SegnapostoSchedeDinamiche(IGeneratoreHtmlSchedeDinamiche generatoreHtml, IConfigurazione<ParametriGenerazioneRiepilogoDomanda> parametriRiepilogo)
        {
            this._generatoreHtml = generatoreHtml;
            this._parametriRiepilogo = parametriRiepilogo;
        }

        #region ISegnapostoRiepilogo Members

        public string NomeTag => "schedeDinamiche";

        public string NomeArgomento => String.Empty;

        public  string Elabora(IDatiDinamiciRiepilogoReader reader, string argomento, string espressione)
        {
            Condition.Requires(reader, "reader").IsNotNull();

            return "<div id='datiDinamici'>" + 
                    this._generatoreHtml.GeneraHtmlDelleSchedeDellaDomanda(reader, this._parametriRiepilogo.Parametri.FlagSchedeNelRiepilogo)
                    + "</div>";
        }

        #endregion
    }
}
