namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari.Permanenti
{
    public class TariffaMezziPubblicitariPermamenti : ITariffaMezziPubblicitari
    {
        public Importo Tariffa { get; }

        public Importo CoefficienteIlluminazione { get; }

        public Importo CoefficienteAreaServizio { get; }


        public TariffaMezziPubblicitariPermamenti(double tariffa, double coeffIlluminazione, double coeffCoeffAreaServizio)
        {
            this.Tariffa = new Importo(tariffa);
            this.CoefficienteIlluminazione = new Importo(coeffIlluminazione);
            this.CoefficienteAreaServizio = new Importo(coeffCoeffAreaServizio);
        }


    }
}