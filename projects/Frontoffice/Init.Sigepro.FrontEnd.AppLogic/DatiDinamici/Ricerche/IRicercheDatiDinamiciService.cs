using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
using Init.SIGePro.Manager.DTO.DatiDinamici;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Ricerche
{
    public interface IRicercheDatiDinamiciService
	{
		RisultatoRicercaDatiDinamiciDto InitializeControl(int idCampo, string value);
		RisultatoRicercaDatiDinamiciDto[] GetCompletionList(int idCampo, string partial, ValoreFiltroRicercaDto[] filtri);
	}
}
