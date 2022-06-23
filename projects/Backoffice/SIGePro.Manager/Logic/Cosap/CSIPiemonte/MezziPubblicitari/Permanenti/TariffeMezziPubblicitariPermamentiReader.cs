using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Linq;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Permanenti
{
    internal class TariffeMezziPubblicitariPermamentiReader : ITariffeMezziPubblicitariReader
    {
        private static class Constants
        {
            public const string ChiaveTabellaTariffeMezziPubblicitariPermanenti = "TARIFFE_MPP";
            public const string ChiaveTabellaCoeffMezziPubblicitari = "COEFFICENTI_MP";

            public const string ChiaveCoeffConIlluminazione = "ILLUMINAZIONE";
            public const string ChiaveCoeffSenzaIlluminazione = "SENZA_ILLUMINAZIONE";

            public const string ChiaveCoeffAreaServizio = "IN_AREA_SERVIZIO";
            public const string ChiaveCoeffNoAreaServizio = "NO_AREA_SERVIZIO";
        }


        private readonly AuthenticationInfo _authInfo;
        private readonly CalcoloMezziPermanentiRequest _request;

        public TariffeMezziPubblicitariPermamentiReader(AuthenticationInfo authInfo, CalcoloMezziPermanentiRequest request)
        {
            this._authInfo = authInfo;
            this._request = request;
        }

        public ITariffaMezziPubblicitari GetTariffe()
        {
            using (var db = this._authInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, this._authInfo.IdComune);
                var chiave = $"{this._request.TipoImpianto}-{this._request.ClasseDimensione}-{this._request.CategoriaStrada}-{this._request.OccupazioneStrada}";

                var tariffa = GetValoreDaDecodifica(decodificheService, Constants.ChiaveTabellaTariffeMezziPubblicitariPermanenti, chiave);
                
                var raggrCoeffIll = this._request.Illuminazione ? Constants.ChiaveCoeffConIlluminazione : Constants.ChiaveCoeffSenzaIlluminazione;
                var coeffIlluminazione = GetValoreDaDecodifica(decodificheService, Constants.ChiaveTabellaCoeffMezziPubblicitari, raggrCoeffIll);

                var raggrCoeffAreaServizio = this._request.AreaServizio ? Constants.ChiaveCoeffAreaServizio: Constants.ChiaveCoeffNoAreaServizio;
                var coeffCoeffAreaServizio = GetValoreDaDecodifica(decodificheService, Constants.ChiaveTabellaCoeffMezziPubblicitari, raggrCoeffAreaServizio);

                return new TariffaMezziPubblicitariPermamenti(tariffa, coeffIlluminazione, coeffCoeffAreaServizio);
            }
        }

        private double GetValoreDaDecodifica(DecodificheService decodificheService, string tabella, string raggruppamento)
        {
            var tariffa = decodificheService.GetDecodificheAttiveByRaggruppamento(tabella, raggruppamento).FirstOrDefault();
            return Double.Parse(tariffa?.Valore ?? "0");
        }
    }
}