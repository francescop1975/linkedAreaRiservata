using Init.SIGePro.Manager.DTO.DatiDinamici;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Ricerche
{
    public class RicercheDatiDinamiciService : IRicercheDatiDinamiciService
	{
		IDatiDinamiciRepository _repository;

		public RicercheDatiDinamiciService(IDatiDinamiciRepository repository)
		{
			this._repository = repository;
		}

		#region IRicercheDatiDinamiciService Members

		public RisultatoRicercaDatiDinamiciDto InitializeControl(int idCampo, string value)
		{
			return this._repository.InitializeControl(idCampo, value);
		}

		public RisultatoRicercaDatiDinamiciDto[] GetCompletionList(int idCampo, string partial, ValoreFiltroRicercaDto[] filtri)
		{
			return this._repository.GetCompletionList(idCampo, partial, filtri);
		}

		#endregion
	}
}
