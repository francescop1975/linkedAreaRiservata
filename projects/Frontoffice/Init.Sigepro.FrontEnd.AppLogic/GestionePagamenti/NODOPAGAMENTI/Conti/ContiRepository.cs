using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using Init.Sigepro.FrontEnd.Pagamenti.NODOPAGAMENTI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.NODOPAGAMENTI.Conti
{
    public class ContiRepository : IContiRepository
    {
        private readonly IAliasSoftwareResolver _aliasSoftwareResolver;
        private readonly AreaRiservataServiceCreator _areaRiservataServiceCreator;

        internal ContiRepository(IAliasSoftwareResolver aliasSoftwareResolver, AreaRiservataServiceCreator areaRiservataServiceCreator)
        {
            _aliasSoftwareResolver = aliasSoftwareResolver;
            _areaRiservataServiceCreator = areaRiservataServiceCreator;
        }

        public IContoDTO GetDatiContoDaCausaleOnere(string codiceComune, int causaleOnereId)
        {
            using (var ws = this._areaRiservataServiceCreator.CreateClient())
            {
                return ws.Service.GetContoDaIdCausaleOnere(ws.Token, causaleOnereId);
            }
        }
    }
}
