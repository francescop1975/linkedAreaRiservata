using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.WebServices;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;

namespace Init.Sigepro.FrontEnd.GestioneMovimenti.DatiDinamici
{
	public class MovimentiIstanzeManager
	{
		IVisuraService _visuraService;

		public MovimentiIstanzeManager(IVisuraService visuraService)
		{
			this._visuraService = visuraService;
		}

        public SIGePro.DatiDinamici.Interfaces.IClasseContestoModelloDinamico LeggiIstanza(int codiceIstanza)
        {
            return this._visuraService.GetById(codiceIstanza, new VisuraIstanzaFlags { LeggiDatiConfigurazione = false });
        }
    }
}
