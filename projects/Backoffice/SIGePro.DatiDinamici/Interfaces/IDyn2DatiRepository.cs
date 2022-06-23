using Init.SIGePro.DatiDinamici.Utils;
using Init.SIGePro.DatiDinamici.VisibilitaCampi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici.Interfaces
{
    public interface IDyn2DatiRepositoryBase
    {
        SerializableDictionary<int, IEnumerable<IValoreCampo>> GetValoriCampiDaIdModello(int idModello, int indiceModello);
    }

    public interface IDyn2DatiRepository: IDyn2DatiRepositoryBase
    {
        //void SalvaStoricoModello(ModelloDinamicoBase modelloDinamicoBase);
        void SalvaValoriCampi(DatiIdentificativiModello idModello, IEnumerable<CampoDaSalvare> campiDaSalvare);
        void EliminaValoriCampi(DatiIdentificativiModello idModello, IEnumerable<DatiIdentificativiCampo> campiDaEliminare);
        void SalvaCampiNonVisibili(DatiIdentificativiModello idModello, IEnumerable<IdValoreCampo> enumerable);
    }

    public interface IDyn2DatiStoricoRepository: IDyn2DatiRepositoryBase
    {
        void SalvaStoricoModello(ModelloDinamicoBase modelloDinamicoBase);
        //void SalvaValoriCampi(ModelloDinamicoBase modelloDinamicoBase, IEnumerable<CampoDinamico> campiDaSalvare);
    }
}
