﻿// -----------------------------------------------------------------------
// <copyright file="CittadinanzeService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneComuni
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using CuttingEdge.Conditions;
	using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;
	using Init.Sigepro.FrontEnd.AppLogic.Common;
    using Init.Sigepro.FrontEnd.AppLogic.WsComuniService;

    public interface ICittadinanzeService
	{
		bool IsCittadinanzaExtracomunitaria(int codiceCittadinanza);
		CittadinanzaCompatto GetCittadinanzaDaId(int codiceCittadinanza);
		IEnumerable<CittadinanzaCompatto> GetListaCittadinanze(bool italiaAlTop);
        IEnumerable<CittadinanzaCompatto> GetListaCittadinanze();
	}

	public class CittadinanzeService : ICittadinanzeService
	{
		IComuniRepository _comuniRepository;

		public CittadinanzeService(IComuniRepository comuniRepository)
		{
			Condition.Requires(comuniRepository, "comuniRepository").IsNotNull();

			this._comuniRepository = comuniRepository;
		}

		#region ICittadinanzeService Members

		public bool IsCittadinanzaExtracomunitaria(int idCittadinanza)
		{
			return this._comuniRepository.IsCittadinanzaExtracomunitaria(idCittadinanza);
		}


		public CittadinanzaCompatto GetCittadinanzaDaId(int codiceCittadinanza)
		{
			return this._comuniRepository.GetCittadinanzaDaId( codiceCittadinanza);
		}

		public IEnumerable<CittadinanzaCompatto> GetListaCittadinanze()
		{
			return this._comuniRepository.GetCittadinanze();
		}

        public IEnumerable<CittadinanzaCompatto> GetListaCittadinanze(bool italiaAlTop)
        {
            var cittadinanze = this._comuniRepository.GetCittadinanze().ToList();

            var cittadinanzaItaliana = cittadinanze.FirstOrDefault(x => x.Descrizione.ToUpperInvariant() == "ITALIA");

            if (cittadinanzaItaliana != null)
            {
                cittadinanze.Remove(cittadinanzaItaliana);
                cittadinanze.Insert(0, cittadinanzaItaliana);
            }

            return cittadinanze;
        }
		#endregion
	}
}
