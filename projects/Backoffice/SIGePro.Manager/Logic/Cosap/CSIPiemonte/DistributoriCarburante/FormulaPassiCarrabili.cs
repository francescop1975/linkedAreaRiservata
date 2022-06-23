namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    internal class FormulaPassiCarrabili : FormulaCosapBase
    {
        private CalcolaImportiRequest _request;
        private TariffaPassiCarrabili _tariffaPC;

        public FormulaPassiCarrabili(CalcolaImportiRequest request, TariffaPassiCarrabili tariffaPC)
        {
            this._request = request;
            this._tariffaPC = tariffaPC;
        }

        protected override double GetImporto()
        {
            return _request.SommaMqPassiCarrabili * _tariffaPC.Tariffa.Valore;
        }

        protected override string GetStringaCalcolo()
        {
            return $"{ImportoToString(_request.SommaMqPassiCarrabili)} * {_tariffaPC.Tariffa}";
        }
    }
}