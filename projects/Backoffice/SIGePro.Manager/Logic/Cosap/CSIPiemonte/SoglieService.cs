using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali.TariffeConcessioniStradaliReader;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte
{
    public class SoglieService
    {
        public static class SoglieConstants
        {
            public const string TabellaSoglie = "SOGLIE";
        }

        private readonly AuthenticationInfo _authInfo;

        public SoglieService(AuthenticationInfo authInfo)
        {
            this._authInfo = authInfo;
        }

        public IEnumerable<Soglia> SoglieAttive(string raggruppamento) {
            using (var db = this._authInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, this._authInfo.IdComune);

                var soglie = decodificheService.GetDecodificheAttiveByRaggruppamento(SoglieConstants.TabellaSoglie, raggruppamento, "ordine asc");

                if (soglie == null)
                    return Enumerable.Empty<Soglia>();

                return soglie.ToList().ConvertAll( new Converter<DecodificaDTO,Soglia>(DecodificaDTOToSoglia));
            }
        }

        private Soglia DecodificaDTOToSoglia(DecodificaDTO decodifica ) 
        {
            return new Soglia(decodifica);
        }
    }
}
