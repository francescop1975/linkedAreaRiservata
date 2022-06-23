﻿// -----------------------------------------------------------------------
// <copyright file="IOneriRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneOneri
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Init.Sigepro.FrontEnd.AppLogic.AreaRiservataService;

	public interface IOneriRepository
	{
        IEnumerable<Onere> GetByIdInterventoIdEndo(int codiceIntervento, List<int> listaIdEndo);
		
		IEnumerable<TipoPagamento> GetModalitaPagamento();

        string GetCodiceCausaleOnereTraslazione(int idCausale);

		TipoPagamento GetModalitaPagamentoById(string modalitaPagamentoId);
	}
}
