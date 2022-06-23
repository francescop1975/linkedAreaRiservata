namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    internal class FormulaEccedenza : FormulaCosapBase
    {
        private Calcolo calcoloCanone;
        private Calcolo calcoloPC;

        public FormulaEccedenza(Calcolo calcoloCanone, Calcolo calcoloPC)
        {
            this.calcoloCanone = calcoloCanone;
            this.calcoloPC = calcoloPC;
        }

        protected override double GetImporto()
        {
            return calcoloPC.Importo.Valore - calcoloCanone.Importo.Valore;
        }

        protected override string GetStringaCalcolo()
        {
            return $"{calcoloPC.Importo} - {calcoloCanone.Importo}";
        }
    }
}