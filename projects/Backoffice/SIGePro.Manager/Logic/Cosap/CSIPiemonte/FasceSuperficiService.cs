using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte
{
    public class FasceSuperficiService
    {
        public static class FasceSuperficiConstants
        {
            public const string TabellaFasceSuperfici = "FASCE";
        }

        private readonly AuthenticationInfo _authInfo;
        private readonly SoglieService _soglieService;
        private readonly string _chiaveTipoOccupazioneStradale;

        public FasceSuperficiService(AuthenticationInfo authInfo, string chiaveTipoOccupazioneStradale)
        {
            this._authInfo = authInfo;
            this._soglieService = new SoglieService(authInfo);
            this._chiaveTipoOccupazioneStradale = chiaveTipoOccupazioneStradale;
        }

        public IEnumerable<FasciaSuperficie> FasceAttive()
        {
            var soglie = this._soglieService.SoglieAttive(this._chiaveTipoOccupazioneStradale);
            var fasce = new List<FasciaSuperficie>();

            using (var db = this._authInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, this._authInfo.IdComune);


                foreach (var soglia in soglie)
                {
                    var raggruppamento = $"{this._chiaveTipoOccupazioneStradale}-{soglia.Chiave}";
                    var orderBy = "ordine asc";

                    var fasceDTO = decodificheService.GetDecodificheAttiveByRaggruppamento(FasceSuperficiConstants.TabellaFasceSuperfici, raggruppamento, orderBy);

                    if (fasceDTO == null)
                        return Enumerable.Empty<FasciaSuperficie>();

                    fasceDTO
                        .ToList()
                        .ForEach(f =>
                        {
                            fasce.Add(new FasciaSuperficie(f, Convert.ToDouble(soglia.Valore)));
                        });

                }
            }

            return fasce;
        }
    }
}
