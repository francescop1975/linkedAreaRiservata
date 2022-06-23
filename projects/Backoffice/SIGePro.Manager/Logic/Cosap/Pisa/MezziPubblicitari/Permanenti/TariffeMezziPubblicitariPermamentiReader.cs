using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari.Permanenti
{
    public class TariffeMezziPubblicitariPermamentiReader : ITariffeMezziPubblicitariReader
    {
        private static class TariffeMezziPubblicitariPermamentiReaderConstants
        {
            public const string TabellaCoefficientiMoltiplicatori = "COEMOL";
            public const string ChiaveCoefficientiMoltiplicatoreBifacciale = "CMBIFACCIALE";
            public const string ChiaveCoefficientiMoltiplicatoreLuminoso = "CMLUMINOSO";
            public const string ChiaveTabellaTariffeBase = "TARBASE";
            public const string ValoreTabellaTariffeBase = "DUP";
            public const string ChiaveTabellaCoefficienteCategoriaStrada = "COESTR";
            public const string ChiaveTabellaCoefficienteTipoOccupazione = "COETOC";
            public const string ChiaveTabellaCoefficientiMezzi = "COEMEZ";
            public const string ChiaveTabellaSoglie = "SOGLIE";
            public const int CifreDecimaliCoefficienti = 2;

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

                //1. Tariffa base
                var tariffaBase = GetValoreDaDecodifica(decodificheService, TariffeMezziPubblicitariPermamentiReaderConstants.ChiaveTabellaTariffeBase, TariffeMezziPubblicitariPermamentiReaderConstants.ValoreTabellaTariffeBase);

                //2. Verifico se sono previste soglie di scaglionamento e relativi coefficienti per i mezzi
                var coefficienti = decodificheService.GetDecodificheAttive(TariffeMezziPubblicitariPermamentiReaderConstants.ChiaveTabellaCoefficientiMezzi).ToArray();

                var soglie = decodificheService.GetDecodificheAttive(TariffeMezziPubblicitariPermamentiReaderConstants.ChiaveTabellaSoglie)
                     .Where(x => x.Raggruppamento.Contains(this._request.TipoOccupazione))
                     .Select(x =>
                     {
                         var raggruppamento = $"{this._request.TipoOccupazione}-{TariffeMezziPubblicitariPermamentiReaderConstants.ValoreTabellaTariffeBase}-{this._request.OccupazioneStrada}-{x.Chiave}";
                         return new Soglia(this._authInfo, x.Chiave, x.Valore, coefficienti.Where(y => y.Raggruppamento == raggruppamento).Select(y => Convert.ToDouble(y.Valore)).FirstOrDefault());
                     });

                //3. Coefficiente categoria strada
                var coefficienteCategoriaStrada = GetValoreDaDecodifica(decodificheService, TariffeMezziPubblicitariPermamentiReaderConstants.ChiaveTabellaCoefficienteCategoriaStrada, this._request.CategoriaStrada);

                //4. Coefficiente tipologia occupazione
                var coefficienteTipoOccupazione = GetValoreDaDecodifica(decodificheService, TariffeMezziPubblicitariPermamentiReaderConstants.ChiaveTabellaCoefficienteTipoOccupazione, this._request.TipoOccupazione);

                double? coefficienteBifacciale = null;
                //5.Verifico se previsto coefficiente bifacciale
                if (this._request.Bifacciale)
                {
                    coefficienteBifacciale = decodificheService.GetDecodificheAttive(TariffeMezziPubblicitariPermamentiReaderConstants.TabellaCoefficientiMoltiplicatori)
                                                    .Where(x => x.Chiave == TariffeMezziPubblicitariPermamentiReaderConstants.ChiaveCoefficientiMoltiplicatoreBifacciale)
                                                    .Select(x => Convert.ToDouble(x.Valore))
                                                    .First();
                }

                double? coefficienteLuminoso = null;
                //5.Verifico se previsto coefficiente luminoso
                if (this._request.Luminoso)
                {
                    coefficienteLuminoso = decodificheService.GetDecodificheAttive(TariffeMezziPubblicitariPermamentiReaderConstants.TabellaCoefficientiMoltiplicatori)
                                                    .Where(x => x.Chiave == TariffeMezziPubblicitariPermamentiReaderConstants.ChiaveCoefficientiMoltiplicatoreLuminoso)
                                                    .Select(x => Convert.ToDouble(x.Valore))
                                                    .First();
                }

                return new TariffaMezziPubblicitariPermamenti(tariffaBase, soglie, coefficienteCategoriaStrada, coefficienteTipoOccupazione, coefficienteBifacciale, coefficienteLuminoso, TariffeMezziPubblicitariPermamentiReaderConstants.CifreDecimaliCoefficienti);
            }
        }

        private double GetValoreDaDecodifica(DecodificheService decodificheService, string tabella, string raggruppamento)
        {
            var tariffa = decodificheService.GetDecodificheAttiveByRaggruppamento(tabella, raggruppamento).FirstOrDefault();
            return Double.Parse(tariffa?.Valore ?? "1");
        }
    }
}
