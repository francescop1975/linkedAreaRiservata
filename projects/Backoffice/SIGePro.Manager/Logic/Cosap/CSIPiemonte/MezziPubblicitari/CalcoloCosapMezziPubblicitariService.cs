using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Permanenti;
using Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Temporanei;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari
{
    public class CalcoloCosapMezziPubblicitariService : CalcoloCosapBase
    {
        public class EsitoCalcolo
        {
            public Calcolo Canone { get; }

            public EsitoCalcolo(Calcolo canone)
            {
                Canone = canone;
            }
        }

        public EsitoCalcolo CalcolaImporti(CalcoloMezziPermanentiRequest request, ITariffeMezziPubblicitariReader reader)
        {
            var result = new FormulaMezziPermanenti(request, reader.GetTariffe()).Calcola();

            return new EsitoCalcolo(result);
        }

        public EsitoCalcolo CalcolaImporti(CalcoloMezziPermanentiRequest request, AuthenticationInfo authInfo)
        {
            return this.CalcolaImporti(request, new TariffeMezziPubblicitariPermamentiReader(authInfo, request));
        }

        public EsitoCalcolo CalcolaImporti(CalcoloMezziTemporaneiRequest request, ITariffeMezziPubblicitariReader reader)
        {
            var result = new FormulaMezziTemporanei(request, reader.GetTariffe()).Calcola();

            return new EsitoCalcolo(result);
        }

        public EsitoCalcolo CalcolaImporti(CalcoloMezziTemporaneiRequest request, AuthenticationInfo authInfo)
        {
            return this.CalcolaImporti(request, new TariffeMezziPubblicitariTemporaneiReader(authInfo, request));
        }

        public EsitoCalcolo CalcolaImporti(CalcoloPreinsegneRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
