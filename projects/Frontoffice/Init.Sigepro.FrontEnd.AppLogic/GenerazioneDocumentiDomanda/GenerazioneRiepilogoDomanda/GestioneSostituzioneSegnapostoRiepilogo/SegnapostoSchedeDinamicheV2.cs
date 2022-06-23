﻿using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo.LetturaDatiDinamici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo
{
    internal class SegnapostoSchedeDinamicheV2 : ISegnapostoRiepilogo
    {
        private readonly IGeneratoreHtmlSchedeDinamiche _generatoreHtml;
        private readonly IConfigurazione<ParametriGenerazioneRiepilogoDomanda> _parametriRiepilogo;

        internal SegnapostoSchedeDinamicheV2(IGeneratoreHtmlSchedeDinamiche generatoreHtml, IConfigurazione<ParametriGenerazioneRiepilogoDomanda> parametriRiepilogo)
        {
            this._generatoreHtml = generatoreHtml;
            this._parametriRiepilogo = parametriRiepilogo;
        }

        #region ISegnapostoRiepilogo Members

        public string NomeTag => "schedeDinamiche";

        public string NomeArgomento => "versione-renderer";

        public string Elabora(IDatiDinamiciRiepilogoReader reader, string argomento, string espressione)
        {
            if (argomento == "1")
            {
                return new SegnapostoSchedeDinamiche(this._generatoreHtml, this._parametriRiepilogo).Elabora(reader, argomento, espressione);
            }

            this._generatoreHtml.IgnoraCssDefault = true;

            return "<div id='datiDinamici'>" +
                    this._generatoreHtml.GeneraHtmlDelleSchedeDellaDomanda(reader, this._parametriRiepilogo.Parametri.FlagSchedeNelRiepilogo)
                    + "</div>";
        }

        #endregion
    }
}
