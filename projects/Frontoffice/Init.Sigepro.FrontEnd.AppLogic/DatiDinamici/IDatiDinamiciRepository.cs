using System.Collections.Generic;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Entities;
using Init.Sigepro.FrontEnd.Infrastructure.Caching;
using Init.SIGePro.Manager.DTO.DatiDinamici;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici
{

    public interface IDatiDinamiciRepository
	{
		ModelloDinamicoCache GetCacheModelloDinamico(int idModello);
		ListaModelliDinamiciDomandaDto GetSchedeDaInterventoEEndo(int intervento, IEnumerable<int> endo, IEnumerable<string> tipiLocalizzazioni, UsaTipiLocalizzazioniPerSelezionareSchedeDinamiche usaTipiLocalizzazioni);
		RisultatoRicercaDatiDinamiciDto InitializeControl(int idCampo, string value);
		RisultatoRicercaDatiDinamiciDto[] GetCompletionList(int idCampo, string partial, ValoreFiltroRicercaDto[] filtri);
	}
}
