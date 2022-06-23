using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.DatiDinamici.Interfaces;
using Init.Utils;
using Init.SIGePro.DatiDinamici.Contesti;

namespace Init.SIGePro.DatiDinamici
{
	public class ModelloDinamicoIstanza : ModelloDinamicoBase
	{
		public ModelloDinamicoIstanza(ModelloDinamicoLoader loader, int idModello, int indice, bool readOnly, int? idStorico= null)
			: base(idModello, indice, readOnly, idStorico, loader)
		{
		}

		protected override ContestoModelloDinamico InizializzaContesto()
		{
			var templateReader = new ResourceScriptTemplateReader();
			return new ContestoModelloDinamico(Token, ContestoModelloEnum.Istanza, LeggiIstanza(), templateReader);
		}
        
		private IClasseContestoModelloDinamico LeggiIstanza()
		{
            return this.Loader.DataAccessFactory.GetClassLoader().LoadClass();
		}
	}
}
