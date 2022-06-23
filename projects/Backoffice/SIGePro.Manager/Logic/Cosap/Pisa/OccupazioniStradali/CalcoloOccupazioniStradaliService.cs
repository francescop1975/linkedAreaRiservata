using Init.SIGePro.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.OccupazioniStradali
{
    public class CalcoloOccupazioniStradaliService : ICalcoloCosapService
    {
        public EsitoCalcolo CalcolaImportiPermanenti(CalcoloOccupazioniStradaliPermanentiRequest request, AuthenticationInfo authInfo) 
        {
            return this.CalcolaImportiPermanenti(request, new TariffeOccupazioniStradaliReader(authInfo, request));
        }

        public EsitoCalcolo CalcolaImportiPermanenti(CalcoloOccupazioniStradaliPermanentiRequest request, ITariffeOccupazioniStradaliReader reader)
        {
            var result = new FormulaOccupazioniStradaliPermanenti(request, reader.GetTariffe()).Calcola();

            return new EsitoCalcolo(result);
        }

        public EsitoCalcolo CalcolaImportiTemporanei(CalcoloOccupazioniStradaliTemporaneeRequest request, AuthenticationInfo authInfo)
        {
            return this.CalcolaImportiTemporanei(request, new TariffeOccupazioniStradaliReader(authInfo, request));
        }

        public EsitoCalcolo CalcolaImportiTemporanei(CalcoloOccupazioniStradaliTemporaneeRequest request, ITariffeOccupazioniStradaliReader reader)
        {
            var result = new FormulaOccupazioniStradaliTemporanee(request, reader.GetTariffe()).Calcola();

            return new EsitoCalcolo(result);
        }
    }
}
