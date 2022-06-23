namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    public class TariffaPassiCarrabili
    {
        public readonly Importo Tariffa;

        public TariffaPassiCarrabili(double tariffa)
        {
            this.Tariffa = new Importo(tariffa);
        }
    }
}