using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.ConcessioniStradali
{
    public class TariffeConcessioniStradaliReader : ITariffeConcessioniStradaliReader
    {
        public static class ConcessioniStradaliReadersConstants
        {
            public const string ChiaveTabellaTariffeConcessioniStradaliCOSAP = "TARIFFE_CS";
            public const string ChiaveTabellaRiduzioniTariffeConcessioniStradaliCOSAP = "TARIFFE_RID_CS";
            public const int CifreDecimaliCoefficienti = 6;
        }

        private readonly AuthenticationInfo _authInfo;
        private readonly CalcoloConcessioniStradaliRequest _request;


        public TariffeConcessioniStradaliReader(AuthenticationInfo authInfo, CalcoloConcessioniStradaliRequest request)
        {
            this._authInfo = authInfo;
            this._request = request;
        }

        public ITariffeConcessioniStradali GetTariffe()
        {
            using (var db = this._authInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, this._authInfo.IdComune);
                var chiave = $"{this._request.TipoOccupazione}-{this._request.CategoriaStrada}";

                var tariffaBase = GetValoreDaDecodifica(decodificheService, ConcessioniStradaliReadersConstants.ChiaveTabellaTariffeConcessioniStradaliCOSAP, chiave);

                var percentualeRiduzione = GetValoreDaDecodifica(decodificheService, ConcessioniStradaliReadersConstants.ChiaveTabellaRiduzioniTariffeConcessioniStradaliCOSAP, chiave);

                if (percentualeRiduzione > 0.0d)
                    return new Tariffario(tariffaBase, percentualeRiduzione, ConcessioniStradaliReadersConstants.CifreDecimaliCoefficienti);

                //in alternativa alla percentuale di riduzione potrebbero essere presenti delle fasce di calcolo
                var fasce = new FasceSuperficiService(this._authInfo, this._request.TipoOccupazione).FasceAttive();
                if(fasce.Count() > 0)
                    return new Tariffario(tariffaBase, ConcessioniStradaliReadersConstants.CifreDecimaliCoefficienti, fasce);

                return new Tariffario(tariffaBase, ConcessioniStradaliReadersConstants.CifreDecimaliCoefficienti); 

            }
        }

        private double GetValoreDaDecodifica(DecodificheService decodificheService, string tabella, string raggruppamento)
        {
            var tariffa = decodificheService.GetDecodificheAttiveByRaggruppamento(tabella, raggruppamento).FirstOrDefault();
            return Double.Parse(tariffa?.Valore ?? "0");
        }
    }
}
