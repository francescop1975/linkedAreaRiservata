﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.Data;
using Init.SIGePro.Protocollo.FascicolazioneJIrideService;

namespace Init.SIGePro.Protocollo.JIride.Fascicolazione
{
    public interface IFascicolazione
    {
        EsitoOperazione FascicolaDocumento(int IDFascicolo, int IDDocumento, string AggiornaClassifica, string Utente, string Ruolo, string idProtocollo);
        FascicoloOutXml CreaFascicolo(FascicolazioneInfo info);
    }
}
