using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using Init.SIGePro.DatiDinamici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.DatiDinamici.Visura
{
    public interface IVisuraDatiDinamiciService
    {
        ModelloDinamicoIstanza GetModello(Istanze istanza, int idModello);
        IEnumerable<VisuraTitoloModelloDinamicoIstanza> GetTitoliModelli(Istanze istanza);
    }
}
