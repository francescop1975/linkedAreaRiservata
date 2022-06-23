﻿using CuttingEdge.Conditions;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo.LetturaDatiDinamici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo
{
    internal class SegnapostoSchedaDinamica : ISegnapostoRiepilogo
    {
        IGeneratoreHtmlSchedeDinamiche _generatoreHtml;

        internal SegnapostoSchedaDinamica(IGeneratoreHtmlSchedeDinamiche generatoreHtml)
        {
            this._generatoreHtml = generatoreHtml;
        }

        #region ISegnapostoRiepilogo Members

        public string NomeTag
        {
            get { return "schedaDinamica"; }
        }

        public string NomeArgomento
        {
            get { return "id"; }
        }

        public string Elabora(IDatiDinamiciRiepilogoReader reader, string argomento, string espressione)
        {
            Condition.Requires(reader, "reader").IsNotNull();

            int idScheda = -1;

            if (!int.TryParse(argomento, out idScheda))
                throw new ArgomentoSegnapostoNonValidoException(ArgomentoSegnapostoNonValidoException.TipoSegnaposto.Scheda, espressione);

            return this._generatoreHtml.GeneraHtml(reader, idScheda);
        }

        #endregion
    }
}
