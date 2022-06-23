using System.Collections.Generic;
using System.Linq;
using Init.SIGePro.DatiDinamici.WebControls;

namespace Init.SIGePro.DatiDinamici.Interfaces
{
    public class CampoDaSalvare
    {
        public interface IValoreDatoDinamicoReadonly
        {
            string Valore { get;  }
            string ValoreDecodificato { get; }
        }

        protected class ValoreDatoDinamicoReadonly : IValoreDatoDinamicoReadonly
        {
            public string Valore { get; }
            public string ValoreDecodificato { get; }

            public ValoreDatoDinamicoReadonly(string valore, string valoreDecodificato)
            {
                this.Valore = valore;
                this.ValoreDecodificato = valoreDecodificato;
            }
        }

        public readonly int Id;
        public readonly string Etichetta;
        public readonly string NomeCampo;
        public readonly bool IsCampoUpload;
        public readonly bool IsCampoData;
        public List<IValoreDatoDinamicoReadonly> ListaValori;

        internal CampoDaSalvare(CampoDinamico campo)
        {
            this.Id = campo.Id;
            this.Etichetta = campo.Etichetta;
            this.NomeCampo = campo.NomeCampo;
            this.IsCampoUpload = campo.TipoCampo == TipoControlloEnum.Upload;
            this.IsCampoData = campo.TipoCampo == TipoControlloEnum.Data;
            this.ListaValori = campo.ListaValori.Select(x => (IValoreDatoDinamicoReadonly)new ValoreDatoDinamicoReadonly(x.Valore, x.ValoreDecodificato)).ToList();
        }

        public CampoDaSalvare(int id, string etichetta, bool isCampoUpload)
        {
            this.Id = id;
            this.Etichetta = etichetta;
            this.IsCampoUpload = isCampoUpload;
            this.ListaValori = new List<IValoreDatoDinamicoReadonly>();
        }

        public void AggiungiValore(string valore, string valoreDecodificato)
        {
            this.ListaValori.Add(new ValoreDatoDinamicoReadonly(valore, valoreDecodificato));
        }
    }
}