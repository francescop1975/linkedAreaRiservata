﻿using Init.SIGePro.Data;
using Init.SIGePro.Manager;
using Init.SIGePro.Manager.Logic.GestioneCittadinanze;
using Init.SIGePro.Manager.Logic.GestioneComuni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace Sigepro.net.WebServices.WsAreaRiservata.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IComuniService" in both code and config file together.
    [ServiceContract]
	public interface IWsComuniService
    {
		[OperationContract]
		 List<DatiComuneCompatto> FindComuniDaMatchParziale(string token, string matchComune);

		[OperationContract]
		 List<DatiComuneCompatto> GetListaComuni(string token, string siglaProvincia);

		[OperationContract]
		DatiComuneCompatto GetDatiComune(string token, string codiceComune);

		[OperationContract]
		DatiProvinciaCompatto GetDatiProvincia(string token, string siglaProvincia);

		[OperationContract]
		DatiProvinciaCompatto GetDatiProvinciaDaCodiceComune(string token, string codiceComune);

		[OperationContract]
		List<DatiProvinciaCompatto> GetListaProvincie(string token);

		[OperationContract]
		List<DatiComuneCompatto> GetComuniAssociati(string token, string software);

		[OperationContract]
		string GetPecComuneAssociato(string token, string codiceComuneAssociato, string software);

		[OperationContract]
		DatiComuneCompatto GetComuneDaCodiceIstat(string token, string codiceIstat);


		// TODO: Spostare su servizio a parte
		[OperationContract]
		List<CittadinanzaCompatto> GetCittadinanze(string token);
		
		[OperationContract]
		CittadinanzaCompatto GetCittadinanzaById(string token, int id);
	}
}
