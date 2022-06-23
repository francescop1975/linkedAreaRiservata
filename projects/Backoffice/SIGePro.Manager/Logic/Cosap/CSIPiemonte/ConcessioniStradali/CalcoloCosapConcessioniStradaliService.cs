using Init.SIGePro.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali
{
    public class CalcoloCosapConcessioniStradaliService: CalcoloCosapBase
    {
        public class EsitoCalcolo
        {

            public Calcolo Canone { get; }

            public EsitoCalcolo(Calcolo canone)
            {
                Canone = canone;
            }
        }

        public EsitoCalcolo CalcolaImportiPermanenti(CalcoloConcessioniStradaliPermanentiRequest request, AuthenticationInfo authInfo)
        {
            return this.CalcolaImportiPermanenti(request, new TariffeConcessioniStradaliReader(authInfo, request));
        }

        public EsitoCalcolo CalcolaImportiPermanenti(CalcoloConcessioniStradaliPermanentiRequest request, ITariffeConcessioniStradaliReader reader)
        {
            var result = new FormulaConcessioniStradaliPermanenti(request, reader.GetTariffe()).Calcola();

            return new EsitoCalcolo(result);
        }

        public EsitoCalcolo CalcolaImportiTemporanei(CalcoloConcessioniStradaliTemporaneeRequest request, AuthenticationInfo authInfo)
        {
            return this.CalcolaImportiTemporanei(request, new TariffeConcessioniStradaliReader(authInfo, request));
        }

        public EsitoCalcolo CalcolaImportiTemporanei(CalcoloConcessioniStradaliTemporaneeRequest request, ITariffeConcessioniStradaliReader reader)
        {
            var result = new FormulaConcessioniStradaliTemporanee(request, reader.GetTariffe()).Calcola();

            return new EsitoCalcolo(result);
        }
    }
}
