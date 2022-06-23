namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte.MezziPubblicitari
{
    public interface ITariffaMezziPubblicitari
    {
        Importo Tariffa { get; }
        Importo CoefficienteIlluminazione { get; } 
        Importo CoefficienteAreaServizio { get; }
    }
}