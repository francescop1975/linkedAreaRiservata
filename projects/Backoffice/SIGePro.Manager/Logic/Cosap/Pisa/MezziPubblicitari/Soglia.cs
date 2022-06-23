using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Logic.GestioneDecodifiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa.MezziPubblicitari
{
    public class Soglia
    {
        private static class SogliaConstants
        {
            public const string ChiaveTabellaValoriSoglie = "VALORISOGLIE";
            public const string ChiaveTabellaCoefficientiMezzi = "COEMEZ";
            public const int CifreDecimaliCoefficienti = 2;
        }

        public string Codice { get; }
        public string Descrizione { get; }
        public bool Ciascuna { get; }
        public double Da { get; }
        public double A { get; }

        public Coefficiente Coefficiente;



        public Soglia(AuthenticationInfo authInfo, string codice, string descrizione, double coefficiente )
        {
            this.Codice = codice;
            this.Descrizione = descrizione;
            this.Coefficiente = new Coefficiente(coefficiente);

            using (var db = authInfo.CreateDatabase())
            {
                DecodificheService service = new DecodificheService(db, authInfo.IdComune);

                DecodificaDTO decodifica = service.GetDecodificheAttive(SogliaConstants.ChiaveTabellaValoriSoglie)
                    .Where(x => x.Raggruppamento.Contains(this.Codice))
                    .FirstOrDefault();

                this.Ciascuna = decodifica.Valore.IndexOf("-") == 0;

                if (!this.Ciascuna)
                {
                    string first = decodifica.Valore.Split(Convert.ToChar("-")).First();
                    string last = decodifica.Valore.Split(Convert.ToChar("-")).Last();

                    this.Da = Convert.ToDouble(first);
                    this.A = String.IsNullOrEmpty(last) ? double.MaxValue : Convert.ToDouble(last);
                }
            }

        }

    }
}
