using Init.SIGePro.Data;
using Init.SIGePro.Manager.Logic.GestioneStradario.RicercaStradario;
using System.Collections.Generic;
using System.Linq;

namespace SIGePro.Manager.Tests.Logic.GestioneStradario.RicercaStradario
{

    public class FakeStradarioRepository : IStradarioRepository
    {

        public IEnumerable<Stradario> RisultatoFindByMatchParziale = Enumerable.Empty<Stradario>();
        public IEnumerable<Stradario> FindByMatchParziale(CondizioniRicercaStradarioPerDescrizione condizioniRicerca)
        {
            return RisultatoFindByMatchParziale;
        }
    }
}
