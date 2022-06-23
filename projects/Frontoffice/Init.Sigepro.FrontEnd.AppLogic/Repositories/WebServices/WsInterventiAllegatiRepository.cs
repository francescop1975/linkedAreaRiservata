﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using CuttingEdge.Conditions;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.WsInterventi;
using Init.SIGePro.Manager.DTO.AllegatiDomandaOnline;
using Init.Sigepro.FrontEnd.AppLogic.GestioneInterventi;
using Init.SIGePro.Manager.DTO.Common;

namespace Init.Sigepro.FrontEnd.AppLogic.Repositories.WebServices
{
	internal class WsInterventiAllegatiRepository : IInterventiAllegatiRepository
	{
		private readonly InterventiServiceCreator _serviceCreator;
		private readonly ISoftwareResolver _softwareResolver;

		public WsInterventiAllegatiRepository(ISoftwareResolver aliasSoftwareResolver, InterventiServiceCreator serviceCreator)
		{
			Condition.Requires(serviceCreator, "serviceCreator").IsNotNull();
			Condition.Requires(aliasSoftwareResolver, "aliasSoftwareResolver").IsNotNull();

			_serviceCreator = serviceCreator;
			_softwareResolver = aliasSoftwareResolver;
		}


		public IEnumerable<AllegatoInterventoDomandaOnlineDto> GetAllegatiDaIdintervento( int codiceIntervento, AmbitoRicerca ambitoRicerca)
		{
			using (var ws = _serviceCreator.CreateClient())
			{
				return ws.Service.GetDocumentiDaCodiceIntervento(ws.Token, codiceIntervento, ambitoRicerca);
			}
		}

        public string GetIdDrupalDaIdIntervento(int codiceIntervento)
        {
            using (var ws = _serviceCreator.CreateClient())
            {
                return ws.Service.GetIdDrupalDaCodiceIntervento(ws.Token, codiceIntervento);
            }
        }

		public IEnumerable<AlberoProcDocumentiCat> GetListaCategorieAllegati()
		{
			var software = this._softwareResolver.Software;

			using (var ws = _serviceCreator.CreateClient())
			{
				return new List<AlberoProcDocumentiCat>(ws.Service.GetCategorieAllegatiInterventoChePermettonoUpload(ws.Token, software));
			}
		}
	}
}
