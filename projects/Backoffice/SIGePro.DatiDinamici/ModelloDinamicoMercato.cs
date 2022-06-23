using Init.SIGePro.DatiDinamici.Contesti;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici
{
    public class ModelloDinamicoMercato : ModelloDinamicoBase
    {
        public ModelloDinamicoMercato(ModelloDinamicoLoader loader, int idModello, int indice, bool readOnly, int? idStorico = null)
            : base(idModello, indice, readOnly, idStorico, loader)
        {
        }

        protected override ContestoModelloDinamico InizializzaContesto()
        {
            var templateReader = new ResourceScriptTemplateReader();
            return new ContestoModelloDinamico(Token, ContestoModelloEnum.Istanza, LeggiMercato(), templateReader);
        }

        private IClasseContestoModelloDinamico LeggiMercato()
        {
            return null;
            //throw new NotImplementedException();
            //return this.Loader.DataAccessFactory.GetClassLoader().LoadClass();
        }
    }
}
