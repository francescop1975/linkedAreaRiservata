using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2
{

    public class ParametriPresentazioneDomanda: IParametriConfigurazione
    {
        public readonly bool RichiedenteSoloPersoneFisiche;
        public readonly bool AbilitaTemplateDomanda;

        public ParametriPresentazioneDomanda( bool richiedenteSoloPersoneFisiche, bool abilitaTemplateDomanda)
        {
            this.RichiedenteSoloPersoneFisiche = richiedenteSoloPersoneFisiche;
            this.AbilitaTemplateDomanda = abilitaTemplateDomanda;
        }
    }
}
