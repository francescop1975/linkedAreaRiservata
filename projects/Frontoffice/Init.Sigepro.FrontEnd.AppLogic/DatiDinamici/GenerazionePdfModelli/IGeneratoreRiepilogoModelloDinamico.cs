// -----------------------------------------------------------------------
// <copyright file="IGeneratoreRiepilogoModelloDinamico.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.GenerazionePdfModelli
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;


    public interface IGeneratoreRiepilogoModelloDinamico
	{
        //RisultatoGenerazioneRiepilogoDomanda GeneraRiepilogoEAllega( int idModello, string nomeRiepilogo, int indiceMolteplicita);
        BinaryFile GeneraRiepilogo(int idModello, string nomeRiepilogo, int indiceMolteplicita);

    }
}
