using System.Collections.Generic;
using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.EntraNext;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneOneri;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneFixDatiModena
{
    public class DatiDomandaModena
    {
        public int Id => Domanda.Id.Value;

        public FoDomande Domanda { get;  set; }
        public IEnumerable<OnereFrontoffice> OneriInSospeso { get; set; }
        public PagamentiEntraNextService.PagamentiDatiExtra DatiExtra { get; set; }
        public string Intervento { get; internal set; }
        public IEnumerable<OnereFrontoffice> OneriTotali { get; set; }
    }
}