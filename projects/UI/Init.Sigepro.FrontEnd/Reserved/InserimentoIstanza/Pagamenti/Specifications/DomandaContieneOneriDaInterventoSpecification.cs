﻿using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneOneri;
using Init.Sigepro.FrontEnd.Infrastructure;
using System.Linq;

namespace Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.Pagamenti.Specifications
{
    public class DomandaContieneOneriDaInterventoSpecification : ISpecification<IDomandaOnlineReadInterface>
    {
        #region ISpecification<double> Members

        public bool IsSatisfiedBy(IDomandaOnlineReadInterface item)
        {
            return item.Oneri.Oneri.Where(x => x.Provenienza == OnereFrontoffice.ProvenienzaOnereEnum.Intervento).Count() > 0;
        }

        #endregion
    }
}