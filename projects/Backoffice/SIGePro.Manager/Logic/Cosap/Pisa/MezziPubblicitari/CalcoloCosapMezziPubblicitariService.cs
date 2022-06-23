using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari.Permanenti;
using Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari.Preinsegne;
using Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari.Temporanei;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari
{
    public class CalcoloCosapMezziPubblicitariService : ICalcoloCosapService
    {

        public EsitoCalcolo CalcolaImportiPermanenti(CalcoloMezziPermanentiRequest request, AuthenticationInfo authInfo)
        {
            return this.CalcolaImportiPermanenti(request, new TariffeMezziPubblicitariPermamentiReader(authInfo, request));
        }

        public EsitoCalcolo CalcolaImportiPermanenti(CalcoloMezziPermanentiRequest request, ITariffeMezziPubblicitariReader reader)
        {
            var result = new FormulaMezziPermanenti(request, reader.GetTariffe()).Calcola();

            return new EsitoCalcolo(result);
        }

        public EsitoCalcolo CalcolaImportiTemporanei(CalcoloMezziTemporaneiRequest request, AuthenticationInfo authInfo)
        {
            return this.CalcolaImportiTemporanei(request, new TariffeMezziPubblicitariTemporaneiReader(authInfo, request));
        }

        public EsitoCalcolo CalcolaImportiTemporanei(CalcoloMezziTemporaneiRequest request, ITariffeMezziPubblicitariReader reader)
        {
            var result = new FormulaMezziTemporanei(request, reader.GetTariffe()).Calcola();

            return new EsitoCalcolo(result);
        }



        public EsitoCalcolo CalcolaImporti(CalcoloPreinsegneRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
