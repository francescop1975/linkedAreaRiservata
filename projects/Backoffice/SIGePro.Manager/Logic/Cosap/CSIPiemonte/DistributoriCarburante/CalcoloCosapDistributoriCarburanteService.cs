using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    public class CalcoloCosapDistributoriCarburanteService : CalcoloCosapBase
    {
        private static class Constants
        {
            public const string TabellaFronti = "CARBURANTI_FRONTE";
            public const string TabellaCB = "CARBURANTI_CB";
            public const string TabellaKC = "CARBURANTI_KC";
            public const string TabellaKS = "CARBURANTI_KS";
            public const string TabellaKI = "CARBURANTI_KI";
            public const string TabellaKR = "ADEGUAMENTO_ISTAT";
            public const string TabellaAT = "CARBURANTI_AT";
            public const string ChiaveKC1 = "CI01";
            public const string ChiaveKC2 = "CI02";
            public const string ChiaveKC3 = "CI03";
            public const string ChiaveKCH = "CI04";
            public const string ChiaveKSR = "CI05";
            public const string ChiaveKSS = "CI06";
        }

        public class EsitoCalcolo
        {
            public Calcolo Canone { get; }
            public Calcolo PassiCarrabili { get; }
            public Calcolo Eccedenza { get; }

            public EsitoCalcolo(Calcolo canone, Calcolo passiCarrabili, Calcolo eccedenza)
            {
                this.Canone = canone;
                this.PassiCarrabili = passiCarrabili;
                this.Eccedenza = eccedenza;
            }
        }

        public CalcoloCosapDistributoriCarburanteService()
        {
        }

        public EsitoCalcolo CalcolaImporti(CalcolaImportiRequest request, ITariffeDistributoriReader coefficientiProvider)
        {
            var tariffaCanone = coefficientiProvider.GetTariffeCanoneDistributori();
            var tariffaPC = coefficientiProvider.GetTariffaPassiCarrabili();

            var canone = new FormulaCanoneDistributore(request, tariffaCanone).Calcola();
            var passiCarrabili = new FormulaPassiCarrabili(request, tariffaPC).Calcola();
            var eccedenza = new FormulaEccedenza(canone, passiCarrabili).Calcola();

            return new EsitoCalcolo(canone, passiCarrabili, eccedenza);
        }

        public EsitoCalcolo CalcolaImporti(CalcolaImportiRequest request, AuthenticationInfo authenticationInfo)
        {
            var tariffeReader = new TariffeDistributoriReader(authenticationInfo, request);
            return this.CalcolaImporti(request, tariffeReader);
        }
        public Importo CalcolaCB(string codiceFronte, AuthenticationInfo authenticationInfo)
        {
            using (var db = authenticationInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, authenticationInfo.IdComune);
                var tariffa = decodificheService.GetDecodificheAttiveByRaggruppamento(Constants.TabellaCB, codiceFronte).FirstOrDefault();
                return (tariffa == null ? new Importo(0) : new Importo(Convert.ToDouble(tariffa.Valore)));
            }
        }

        public Importo CalcolaKC(ConfigurazioneDistributori configurazioneDistributori, AuthenticationInfo authenticationInfo)
        {
            var tariffaK1 = 0.0d;
            var tariffaK2 = 0.0d;
            var tariffaK3 = 0.0d;
            var tariffaKCH = 0.0d;
            var tariffaKSR = 0.0d;
            var tariffaKSS = 0.0d;

            using (var db = authenticationInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, authenticationInfo.IdComune);
                tariffaK1 = decodificheService
                                    .GetDecodificheAttiveByRaggruppamento(Constants.TabellaKC, Constants.ChiaveKC1)
                                    .Select(x => Convert.ToDouble(x.Valore))
                                    .DefaultIfEmpty(0.0d)
                                    .First();
                tariffaK2 = decodificheService
                                    .GetDecodificheAttiveByRaggruppamento(Constants.TabellaKC, Constants.ChiaveKC2)
                                    .Select(x => Convert.ToDouble(x.Valore))
                                    .DefaultIfEmpty(0.0d)
                                    .First();
                tariffaK3 = decodificheService
                                    .GetDecodificheAttiveByRaggruppamento(Constants.TabellaKC, Constants.ChiaveKC3)
                                    .Select(x => Convert.ToDouble(x.Valore))
                                    .DefaultIfEmpty(0.0d)
                                    .First();

                tariffaKCH = decodificheService
                                    .GetDecodificheAttiveByRaggruppamento(Constants.TabellaKC, Constants.ChiaveKCH)
                                    .Select(x => Convert.ToDouble(x.Valore))
                                    .DefaultIfEmpty(0.0d)
                                    .First();

                tariffaKSR = decodificheService
                                    .GetDecodificheAttiveByRaggruppamento(Constants.TabellaKC, Constants.ChiaveKSR)
                                    .Select(x => Convert.ToDouble(x.Valore))
                                    .DefaultIfEmpty(0.0d)
                                    .First();

                tariffaKSS = decodificheService
                                    .GetDecodificheAttiveByRaggruppamento(Constants.TabellaKC, Constants.ChiaveKSS)
                                    .Select(x => Convert.ToDouble(x.Valore))
                                    .DefaultIfEmpty(0.0d)
                                    .First();
            }



            var kc1 = configurazioneDistributori.Singoli * tariffaK1;
            var kc2 = configurazioneDistributori.Doppi * tariffaK2;
            var kc3 = configurazioneDistributori.Multiprodotto * tariffaK3;
            var kch = configurazioneDistributori.Chiosco ? tariffaKCH : 0.0d;
            var ksr = configurazioneDistributori.StazioneRifornimento ? tariffaKSR : 0.0d;
            var kss = configurazioneDistributori.StazioneServizio ? tariffaKSS : 0.0d;

            return new Importo(1.0d + kc1 + kc2 + kc3 + kch + ksr + kss);
        }

        public Importo CalcolaKS(IEnumerable<string> serviziAccessori, bool presenzaLocker, AuthenticationInfo authenticationInfo)
        {
            using (var db = authenticationInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, authenticationInfo.IdComune);

                var raggruppamento = String.Join(",", serviziAccessori);

                var decodificaKS = decodificheService
                                    .GetDecodificheAttiveByRaggruppamento(Constants.TabellaKS, raggruppamento)
                                    .FirstOrDefault();

                if (decodificaKS == null)
                {
                    return new Importo(0.0d);
                }

                if (presenzaLocker)
                {
                    if (!decodificaKS.Ordine.HasValue)
                    {
                        throw new Exception($"Impossibile applicare la maggiorazione del locker in quanto le decodifiche della tabella {decodificaKS.Tabella} non hanno un ordine valorizzato");
                    }

                    var decodificaKSSuccessiva = decodificheService
                                    .GetDecodificheSuccessiveAttive(Constants.TabellaKS, decodificaKS.Ordine.Value)
                                    .FirstOrDefault();

                    if (decodificaKSSuccessiva != null)
                    {
                        decodificaKS = decodificaKSSuccessiva;
                    }
                }

                return new Importo(Convert.ToDouble(decodificaKS.Valore));
            }
        }

        public Importo CalcolaKI(string categoriaStrada, AuthenticationInfo authenticationInfo)
        {
            using (var db = authenticationInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, authenticationInfo.IdComune);

                return new Importo(decodificheService
                                    .GetDecodificheAttiveByRaggruppamento(Constants.TabellaKI, categoriaStrada)
                                    .Select(x => Convert.ToDouble(x.Valore))
                                    .DefaultIfEmpty(0.0d)
                                    .First()
                                    );
            }
        }

        public Importo CalcolaKR(AuthenticationInfo authenticationInfo)
        {
            using (var db = authenticationInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, authenticationInfo.IdComune);

                return new Importo(decodificheService
                                    .GetDecodificheAttive(Constants.TabellaKR)
                                    .Select(x => Convert.ToDouble(x.Valore))
                                    .DefaultIfEmpty(0.0d)
                                    .First()
                                    );
            }
        }

        public Importo CalcolaAT(AuthenticationInfo authenticationInfo)
        {
            using (var db = authenticationInfo.CreateDatabase())
            {
                var decodificheService = new DecodificheService(db, authenticationInfo.IdComune);

                return new Importo(decodificheService
                                    .GetDecodificheAttive(Constants.TabellaAT)
                                    .Select(x => Convert.ToDouble(x.Valore))
                                    .DefaultIfEmpty(0.0d)
                                    .First()
                                    );
            }
        }

    }
}
