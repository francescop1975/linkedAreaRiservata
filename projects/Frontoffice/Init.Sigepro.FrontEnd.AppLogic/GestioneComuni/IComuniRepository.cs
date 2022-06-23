// -----------------------------------------------------------------------
// <copyright file="IComuniRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Init.Sigepro.FrontEnd.AppLogic.WsComuniService;
using System.Collections.Generic;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneComuni
{
    public interface IComuniRepository
    {
        IEnumerable<DatiComuneCompatto> FindComuneDaMatchParziale(string matchComune);
        IEnumerable<DatiProvinciaCompatto> FindProvinciaDaMatchParziale(string matchProvincia);
        CittadinanzaCompatto GetCittadinanzaDaId(int idCittadinanza);
        IEnumerable<CittadinanzaCompatto> GetCittadinanze();
        DatiComuneCompatto GetComuneDaCodiceIstat(string codiceIstat);
        IEnumerable<DatiComuneCompatto> GetComuniAssociati(string software);
        DatiComuneCompatto GetDatiComune(string codiceComune);
        DatiProvinciaCompatto GetDatiProvincia(string siglaProvincia);
        IEnumerable<DatiComuneCompatto> GetListaComuni();
        IEnumerable<DatiComuneCompatto> GetListaComuni(string siglaProvincia);
        IEnumerable<DatiProvinciaCompatto> GetListaProvincie();
        string GetPecComuneAssociato(string codiceComune, string software);
        DatiProvinciaCompatto GetProvinciaDaCodiceComune(string codiceComune);
        bool IsCittadinanzaExtracomunitaria(int idCittadinanza);
    }
}