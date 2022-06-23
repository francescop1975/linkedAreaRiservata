using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.ConversionePDF;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo.LetturaDatiDinamici;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.GenerazionePdfModelli
{
    public class GeneratoreRiepilogoModelloDinamico : IGeneratoreRiepilogoModelloDinamico
    {
        private readonly RiepilogoModelloInHtmlFactory _riepilogoModelloInHtmlFactory;
        private readonly IDatiDinamiciRiepilogoReader _reader;
        private readonly IHtmlToPdfFileConverter _fileConverter;

        public GeneratoreRiepilogoModelloDinamico(IDatiDinamiciRiepilogoReader reader,
                                                    RiepilogoModelloInHtmlFactory riepilogoModelloInHtmlFactory, IHtmlToPdfFileConverter fileConverter)
        {
            this._reader = reader;
            this._riepilogoModelloInHtmlFactory = riepilogoModelloInHtmlFactory;
            this._fileConverter = fileConverter;
        }

        public BinaryFile GeneraRiepilogo(int idModello, string nomeRiepilogo, int indiceMolteplicita)
        {
            var nomeFile = new NomeFileRiepilogoModello(nomeRiepilogo.Trim(), indiceMolteplicita).ToString();
            var riepilogo = this._riepilogoModelloInHtmlFactory.FromDomanda(this._reader, idModello, indiceMolteplicita);

            return riepilogo.ConvertiInPdf(this._fileConverter, nomeFile);
        }
    }
}
