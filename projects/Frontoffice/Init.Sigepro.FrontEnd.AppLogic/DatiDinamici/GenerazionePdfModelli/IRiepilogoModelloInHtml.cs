// -----------------------------------------------------------------------
// <copyright file="IriepilogoModelloInHtml.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.GenerazionePdfModelli
{
    using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
    using Init.Sigepro.FrontEnd.AppLogic.ConversionePDF;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IRiepilogoModelloInHtml
	{
		string GetHtml();
        BinaryFile ConvertiInPdf(IHtmlToPdfFileConverter fileConverter, string nomeFile, bool wrapinHtml = true);
	}
}
