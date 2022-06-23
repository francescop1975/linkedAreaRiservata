using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari.Temporanei
{
    public class TariffeMezziPubblicitariTemporaneiReader : ITariffeMezziPubblicitariReader
    {
        private static class TariffeMezziPubblicitariTemporaneiReaderConstants
        {
            public const string TabellaCoefficientiMoltiplicatori = "COEMOL";
            public const string ChiaveCoefficientiMoltiplicatoreBifacciale = "CMBIFACCIALE";
            public const string ChiaveCoefficientiMoltiplicatoreLuminoso = "CMLUMINOSO";
            public const string ChiaveTabellaTariffeBase = "TARBASE";
            public const string ValoreTabellaTariffeBase = "DUT";
            public const string ChiaveTabellaCoefficienteCategoriaStrada = "COESTR";
            public const string ChiaveTabellaCoefficienteTipoOccupazione = "COETOC";
            public const string ChiaveTabellaCoefficientiMezzi = "COEMEZ";
            public const string ChiaveTabellaSoglie = "SOGLIE";
            public const string ChiaveTabellaMisura = "MISURA";
            public const int CifreDecimaliCoefficienti = 2;
        }

        private readonly AuthenticationInfo _authInfo;
        private readonly CalcoloMezziTemporaneiRequest _request;

        public TariffeMezziPubblicitariTemporaneiReader(AuthenticationInfo authInfo, CalcoloMezziTemporaneiRequest request)
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
                var tariffaBase = GetValoreDaDecodifica(decodificheService, TariffeMezziPubblicitariTemporaneiReaderConstants.ChiaveTabellaTariffeBase, TariffeMezziPubblicitariTemporaneiReaderConstants.ValoreTabellaTariffeBase);

                //2. Verifico se sono previste soglie di scaglionamento e relativi coefficienti per i mezzi
                var coefficienti = decodificheService.GetDecodificheAttive(TariffeMezziPubblicitariTemporaneiReaderConstants.ChiaveTabellaCoefficientiMezzi).ToArray();

                var soglie = decodificheService.GetDecodificheAttive(TariffeMezziPubblicitariTemporaneiReaderConstants.ChiaveTabellaSoglie)
                     .Where(x => x.Raggruppamento.Contains(this._request.TipoOccupazione))
                     .Select( x => 
                     {
                         var raggruppamento = $"{this._request.TipoOccupazione}-{TariffeMezziPubblicitariTemporaneiReaderConstants.ValoreTabellaTariffeBase}-{this._request.OccupazioneStrada}-{x.Chiave}";
                         return new Soglia(this._authInfo, x.Chiave, x.Valore, coefficienti.Where(y => y.Raggruppamento == raggruppamento).Select(y => Convert.ToDouble(y.Valore)).FirstOrDefault());
                     });

                
                //2. Coefficiente categoria strada
                var coefficienteCategoriaStrada = GetValoreDaDecodifica(decodificheService, TariffeMezziPubblicitariTemporaneiReaderConstants.ChiaveTabellaCoefficienteCategoriaStrada, this._request.CategoriaStrada);

                //3. Coefficiente tipologia occupazione
                var coefficienteTipoOccupazione = GetValoreDaDecodifica(decodificheService, TariffeMezziPubblicitariTemporaneiReaderConstants.ChiaveTabellaCoefficienteTipoOccupazione, this._request.TipoOccupazione);

                double? coefficienteBifacciale = null;
                //4.Verifico se previsto coefficiente bifacciale
                if (this._request.Bifacciale)
                {
                    coefficienteBifacciale = decodificheService.GetDecodificheAttive(TariffeMezziPubblicitariTemporaneiReaderConstants.TabellaCoefficientiMoltiplicatori)
                                                    .Where(x => x.Chiave == TariffeMezziPubblicitariTemporaneiReaderConstants.ChiaveCoefficientiMoltiplicatoreBifacciale)
                                                    .Select(x => Convert.ToDouble(x.Valore))
                                                    .First();
                }

                double? coefficienteLuminoso = null;
                //5.Verifico se previsto coefficiente bifacciale
                if (this._request.Luminoso)
                {
                    coefficienteLuminoso = decodificheService.GetDecodificheAttive(TariffeMezziPubblicitariTemporaneiReaderConstants.TabellaCoefficientiMoltiplicatori)
                                                    .Where(x => x.Chiave == TariffeMezziPubblicitariTemporaneiReaderConstants.ChiaveCoefficientiMoltiplicatoreLuminoso)
                                                    .Select(x => Convert.ToDouble(x.Valore))
                                                    .First();
                }

                return new TariffaMezziPubblicitariTemporanei(tariffaBase, soglie, coefficienteCategoriaStrada, coefficienteTipoOccupazione, coefficienteBifacciale, coefficienteLuminoso, TariffeMezziPubblicitariTemporaneiReaderConstants.CifreDecimaliCoefficienti);
  
            }
        }

        private double GetValoreDaDecodifica(DecodificheService decodificheService, string tabella, string raggruppamento)
        {
            var tariffa = decodificheService.GetDecodificheAttiveByRaggruppamento(tabella, raggruppamento).FirstOrDefault();
            return Double.Parse(tariffa?.Valore ?? "1");
        }

    }
}
