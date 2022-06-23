using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.Sigepro.FrontEnd.AppLogic.GestioneLocalizzazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogicTests.Adapters.IstanzaSigeproAdapterTests.Mocks
{
    public class StradarioRepositoryFake : IStradarioRepository
    {
        public Stradario GetByCodiceStradario(string aliasComune, int codiceStradario)
        {
            return new Stradario
            {
                CODICESTRADARIO = codiceStradario.ToString(),
                CODICECOMUNE = aliasComune,
                PREFISSO = "Via",
                DESCRIZIONE = "Le mani dal naso"
            };
        }

        public StradarioDto GetByCodViario(string alias, string codViario)
        {
            throw new NotImplementedException();
        }

        public Stradario GetByIndirizzo(string aliasComune, string codiceComune, string indirizzo)
        {
            throw new NotImplementedException();
        }

        public List<StradarioDto> GetByMatchParziale(string aliasComune, string codiceComune, string comuneLocalizzazione, string indirizzo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DatiComuneCompatto> GetComuniStradario(string codiceComune)
        {
            throw new NotImplementedException();
        }

        public List<StradarioColore> GetListaColori(string aliasComune)
        {
            throw new NotImplementedException();
        }
    }
}
