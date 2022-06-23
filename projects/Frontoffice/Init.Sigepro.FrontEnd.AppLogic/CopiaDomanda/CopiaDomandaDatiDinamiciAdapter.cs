﻿using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.CopiaDomanda
{
    public class CopiaDomandaDatiDinamiciAdapter : ICopiaDomandaDatiAdapter
    {

        public CopiaDomandaDatiDinamiciAdapter()
        {
        }

        public void Adatta(Istanze istanzaTemplate, DomandaOnline domanda)
        {
            foreach (var idd in istanzaTemplate.IstanzeDyn2Dati)
            {
                var indiceScheda = 0;
                var indiceMolteplicita = idd.IndiceMolteplicita.HasValue ? idd.IndiceMolteplicita.Value : 0;

                domanda.WriteInterface.DatiDinamici.AggiornaOCrea(idd.FkD2cId.Value, indiceScheda, indiceMolteplicita, idd.Valore, idd.Valoredecodificato, idd.CampoDinamico?.Nomecampo);
            }
        }
    }
}
