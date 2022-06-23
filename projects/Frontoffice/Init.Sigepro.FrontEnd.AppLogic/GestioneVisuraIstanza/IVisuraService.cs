﻿using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza
{
    public interface IVisuraService
    {
        Istanze GetById(int idPratica, VisuraIstanzaFlags flags);
        Istanze GetByUuid(string uuid, bool effettuaSubVisuraMovimenti);
        IEnumerable<VisuraListItem> GetListaPratiche(RichiestaListaPraticheV3 richiesta);

        string GetUUIDDaCodiceIstanza(int codiceIstanza);
    }
}
