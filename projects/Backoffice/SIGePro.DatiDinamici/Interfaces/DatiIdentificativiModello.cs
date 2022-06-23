namespace Init.SIGePro.DatiDinamici.Interfaces
{
    public class DatiIdentificativiModello
    {
        public readonly int IdModello;
        public readonly int IndiceModello;

        public DatiIdentificativiModello(int idModello, int indiceModello)
        {
            this.IdModello = idModello;
            this.IndiceModello = indiceModello;
        }
    }
}