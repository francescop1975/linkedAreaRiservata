using Init.SIGePro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneStradario.RicercaStradario
{
    public interface IStradarioRepository
    {
        IEnumerable<Stradario> FindByMatchParziale(CondizioniRicercaStradarioPerDescrizione condizioniRicerca);
    }
}
