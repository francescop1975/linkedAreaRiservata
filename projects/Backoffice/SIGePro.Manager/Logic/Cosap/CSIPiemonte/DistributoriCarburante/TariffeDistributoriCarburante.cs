namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.DistributoriCarburante
{
    public class TariffeDistributoriCarburante
    {
        public readonly Importo Base;
        public readonly Importo Singoli;
        public readonly Importo Doppi;
        public readonly Importo Multiprodotto;
        public readonly double CoefficienteServiziAccessori;

        public TariffeDistributoriCarburante(double importoBase, double singoli, double doppi, double multiprodotto, double coefficienteServiziAccessori)
        {
            this.Base = new Importo(importoBase);
            this.Singoli = new Importo(singoli);
            this.Doppi = new Importo(doppi);
            this.Multiprodotto = new Importo(multiprodotto);
            this.CoefficienteServiziAccessori = coefficienteServiziAccessori;
        }
    }
}