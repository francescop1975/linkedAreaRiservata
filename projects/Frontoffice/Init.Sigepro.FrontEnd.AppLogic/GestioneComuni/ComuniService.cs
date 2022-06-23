// -----------------------------------------------------------------------
// <copyright file="ComuniService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneComuni
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Init.Sigepro.FrontEnd.AppLogic.Common;
    using Init.Sigepro.FrontEnd.AppLogic.WsComuniService;

    public interface IComuniService
	{
		DatiProvinciaCompatto GetProvinciaDaCodiceComune(string codiceComune);
		string GetPecComuneAssociato(string software, string codiceComune);
		DatiComuneCompatto GetDatiComune(string codiceComune);
		IEnumerable<DatiComuneCompatto> FindComuneDaMatchParziale(string matchComune);
		IEnumerable<DatiProvinciaCompatto> FindProvinciaDaMatchParziale(string matchProvincia);
		DatiProvinciaCompatto GetDatiProvincia( string siglaProvincia);
		DatiComuneCompatto FindComuneDaNomeComune(string matchComune);
	}

	public class ComuniService : IComuniService
	{
        private readonly ISoftwareResolver _softwareResolver;
		private readonly IComuniRepository _comuniRepository;

		public ComuniService(ISoftwareResolver softwareResolver, IComuniRepository comuniRepository)
		{
            _softwareResolver = softwareResolver;
            this._comuniRepository = comuniRepository;
		}



		#region IComuniService Members

		public DatiProvinciaCompatto GetProvinciaDaCodiceComune(string codiceComune)
		{
			return this._comuniRepository.GetProvinciaDaCodiceComune(codiceComune);
		}

		public string GetPecComuneAssociato(string software, string codiceComune)
		{
			return this._comuniRepository.GetPecComuneAssociato( codiceComune, software);
		}

		public DatiComuneCompatto GetDatiComune(string codiceComune)
		{
			return this._comuniRepository.GetDatiComune(codiceComune);
		}

		public IEnumerable<DatiComuneCompatto> FindComuneDaMatchParziale(string matchComune)
		{
			return this._comuniRepository.FindComuneDaMatchParziale(matchComune);
		}

		public IEnumerable<DatiProvinciaCompatto> FindProvinciaDaMatchParziale(string matchProvincia)
		{
			return this._comuniRepository.FindProvinciaDaMatchParziale( matchProvincia);
		}

		public DatiProvinciaCompatto GetDatiProvincia( string siglaProvincia)
		{
			return this._comuniRepository.GetDatiProvincia(siglaProvincia);
		}



		#endregion


		public DatiComuneCompatto FindComuneDaNomeComune(string matchComune)
		{
			var results = this.FindComuneDaMatchParziale(matchComune);

			if (results == null || results.Count() == 0)
				return null;

			return results.Where(x => x.Comune.ToUpperInvariant() == matchComune.ToUpperInvariant()).FirstOrDefault();
		}
	}
}
