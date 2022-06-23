using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    public class TariffeDistributoriReader : ITariffeDistributoriReader
    {
        private static class Constants
        {
            public const string ChiaveTabellaTariffePassiCarrabili = "TARDPC";
            public const string ChiaveTabellaTariffeBase = "TARDIS";
            public const string ChiaveTabellaTariffeImpianti = "TARIMP";
            public const string ChiaveTabellaTariffeServiziAccessori = "COEFFI";

            public const string CodiceDistributoreSingolo = "CI01";
            public const string CodiceDistributoreDoppio = "CI02";
            public const string CodiceDistributoreMultiprodotto = "CI03";
        }

        private readonly AuthenticationInfo _authenticationInfo;
        private readonly CalcolaImportiRequest _calcolaImportiRequest;

        public TariffeDistributoriReader(AuthenticationInfo authenticationInfo, CalcolaImportiRequest calcolaImportiRequest)
        {
            _authenticationInfo = authenticationInfo;
            _calcolaImportiRequest = calcolaImportiRequest;
        }

        public TariffaPassiCarrabili GetTariffaPassiCarrabili()
        {
            using (var db = this._authenticationInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, this._authenticationInfo.IdComune);
                var tabella = Constants.ChiaveTabellaTariffePassiCarrabili;
                var raggruppamento = this._calcolaImportiRequest.Categorie.Cosap;

                var importo = GetValoreDaDecodifica(decodificheService, tabella, raggruppamento);

                return new TariffaPassiCarrabili(importo);
            }
                
        }

        public TariffeDistributoriCarburante GetTariffeCanoneDistributori()
        {
            using (var db = this._authenticationInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, this._authenticationInfo.IdComune);
                var tabella = Constants.ChiaveTabellaTariffeBase;
                var raggruppamento = $"{this._calcolaImportiRequest.TipologiaDistributore}-{this._calcolaImportiRequest.Categorie.Distributori}";

                var importoBase = GetValoreDaDecodifica(decodificheService, tabella, raggruppamento);

                raggruppamento = $"{Constants.CodiceDistributoreSingolo}-{this._calcolaImportiRequest.Categorie.Distributori}";
                var importoSingoli = GetValoreDaDecodifica(decodificheService, Constants.ChiaveTabellaTariffeImpianti, raggruppamento);

                raggruppamento = $"{Constants.CodiceDistributoreDoppio}-{this._calcolaImportiRequest.Categorie.Distributori}";
                var importoDoppio = GetValoreDaDecodifica(decodificheService, Constants.ChiaveTabellaTariffeImpianti, raggruppamento);

                raggruppamento = $"{Constants.CodiceDistributoreMultiprodotto}-{this._calcolaImportiRequest.Categorie.Distributori}";
                var importoMulti = GetValoreDaDecodifica(decodificheService, Constants.ChiaveTabellaTariffeImpianti, raggruppamento);

                var sommaCoeffi = this._calcolaImportiRequest.ServiziAccessoriAttivi
                                                             .Sum(x => GetValoreDaDecodifica(decodificheService, Constants.ChiaveTabellaTariffeServiziAccessori, x));
                sommaCoeffi = 1.0d + sommaCoeffi / 10.0d;

                return new TariffeDistributoriCarburante(importoBase, importoSingoli, importoDoppio, importoMulti, sommaCoeffi);
            }
        }

        private double GetValoreDaDecodifica(DecodificheService decodificheService, string tabella, string raggruppamento)
        {
            var tariffa = decodificheService.GetDecodificheAttiveByRaggruppamento(tabella, raggruppamento).FirstOrDefault();
            return Double.Parse(tariffa?.Valore ?? "0");
        }
    }
}
