﻿using Init.Sigepro.FrontEnd.AppLogic.GestioneComuni;
using Init.Sigepro.FrontEnd.AppLogic.WsComuniService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogicTests.Adapters.IstanzaSigeproAdapterTests.Mocks
{
    public class ComuniServiceFake : IComuniService
    {
        public IEnumerable<DatiComuneCompatto> FindComuneDaMatchParziale(string matchComune)
        {
            throw new NotImplementedException();
        }

        public DatiComuneCompatto FindComuneDaNomeComune(string matchComune)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DatiProvinciaCompatto> FindProvinciaDaMatchParziale(string matchProvincia)
        {
            throw new NotImplementedException();
        }

        public DatiComuneCompatto GetDatiComune(string codiceComune)
        {
            return new DatiComuneCompatto
            {
                Cf = "E256",
                CodiceComune = "E256",
                Comune = "GUBBIO",
                Provincia = "Perugia",
                SiglaProvincia = "PG"
            };
        }

        public DatiProvinciaCompatto GetDatiProvincia(string siglaProvincia)
        {
            throw new NotImplementedException();
        }

        public string GetPecComuneAssociato(string software, string codiceComune)
        {
            throw new NotImplementedException();
        }

        public DatiProvinciaCompatto GetProvinciaDaCodiceComune(string codiceComune)
        {
            throw new NotImplementedException();
        }
    }
}
