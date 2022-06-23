using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.OccupazioniStradali
{
    public class TariffeOccupazioniStradaliReader : ITariffeOccupazioniStradaliReader
    {
        public static class TariffeOccupazioniStradaliReaderConstants
        {
            public const string ChiaveTabellaTariffeBase = "TARBASE";
            public const string ChiaveTabellaCoefficienteCategoriaStrada = "COESTR";
            public const string ChiaveTabellaCoefficienteTipoOccupazione = "COETOC";
            public const int CifreDecimaliCoefficienti = 2;
        }

        private readonly AuthenticationInfo _authInfo;
        private readonly CalcoloOccupazioniStradaliRequest _request;

        public TariffeOccupazioniStradaliReader(AuthenticationInfo authInfo, CalcoloOccupazioniStradaliRequest request)
        {
            this._authInfo = authInfo;
            this._request = request;
        }

        public ITariffeOccupazioniStradali GetTariffe()
        {

            using (var db = this._authInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, this._authInfo.IdComune);

                //1. Tariffa base
                var tariffaBase = GetValoreDaDecodifica(decodificheService, TariffeOccupazioniStradaliReaderConstants.ChiaveTabellaTariffeBase, this._request.DurataOccupazione);

                //2. Coefficiente categoria strada
                var coefficienteCategoriaStrada = GetValoreDaDecodifica(decodificheService, TariffeOccupazioniStradaliReaderConstants.ChiaveTabellaCoefficienteCategoriaStrada, this._request.CategoriaStrada);

                //3. Coefficiente tipologia occupazione
                var coefficienteTipoOccupazione = decodificheService.GetDecodificheAttive(TariffeOccupazioniStradaliReaderConstants.ChiaveTabellaCoefficienteTipoOccupazione)
                                                    .Where(x => x.Raggruppamento.Contains(this._request.TipoOccupazione))
                                                    .Where(x => x.Raggruppamento.Contains(this._request.DurataOccupazione))
                                                    .Where(x => x.Raggruppamento.Contains(this._request.CategoriaStrada))
                                                    .Select(x => x.Valore)
                                                    .FirstOrDefault();

                return new Tariffario(tariffaBase, coefficienteCategoriaStrada, Double.Parse(coefficienteTipoOccupazione ?? "1"), TariffeOccupazioniStradaliReaderConstants.CifreDecimaliCoefficienti);

            }
        }

        private double GetValoreDaDecodifica(DecodificheService decodificheService, string tabella, string raggruppamento)
        {
            var tariffa = decodificheService.GetDecodificheAttiveByRaggruppamento(tabella, raggruppamento).FirstOrDefault();
            return Double.Parse(tariffa?.Valore ?? "1");
        }
    }
}
