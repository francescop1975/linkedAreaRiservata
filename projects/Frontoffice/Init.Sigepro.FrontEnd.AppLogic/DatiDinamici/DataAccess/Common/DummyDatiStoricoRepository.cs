using Init.SIGePro.DatiDinamici;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.DataAccess.Common
{
    public class DummyDatiStoricoRepository : IDyn2DatiStoricoRepository
    {
        public SerializableDictionary<int, IEnumerable<IValoreCampo>> GetValoriCampiDaIdModello(int idModello, int indiceModello)
        {
            return new SerializableDictionary<int, IEnumerable<IValoreCampo>>();
        }

        public void SalvaStoricoModello(ModelloDinamicoBase modelloDinamicoBase)
        {
        }
    }
}
