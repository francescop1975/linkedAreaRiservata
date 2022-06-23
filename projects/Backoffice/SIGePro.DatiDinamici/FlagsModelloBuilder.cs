using Init.SIGePro.DatiDinamici.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici
{
    public class FlagsModelloBuilder : IFlagsModelloBuilder
    {
        private readonly IDyn2ModelliManager _modelliManager;

        public FlagsModelloBuilder(IDyn2ModelliManager modelliManager)
        {
            this._modelliManager = modelliManager;
        }

        public ModelloDinamicoBase.FlagsModello CostruisciFlags(ModelloDinamicoBase modello)
        {
            var d2Modello = this._modelliManager.GetById(modello.IdComune, modello.IdModello);

            var nomeModello = d2Modello.Descrizione;
            var readOnlyWeb = d2Modello.FlgReadonlyWeb.GetValueOrDefault(0) == 1;
            var storicizza = d2Modello.FlgStoricizza.GetValueOrDefault(0) == 1;
            var modelloMultiplo = d2Modello.Modellomultiplo.GetValueOrDefault(0) == 1;

            return new ModelloDinamicoBase.FlagsModello(nomeModello, readOnlyWeb, storicizza, modelloMultiplo);
        }
    }
}
