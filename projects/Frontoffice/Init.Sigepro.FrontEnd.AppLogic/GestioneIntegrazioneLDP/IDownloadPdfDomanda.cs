﻿using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneIntegrazioneLDP
{
    public interface IDownloadPdfDomanda
    {
        BinaryFile FromIdentificativoTemporaneo(string identificativoTemporaneo);
    }
}
