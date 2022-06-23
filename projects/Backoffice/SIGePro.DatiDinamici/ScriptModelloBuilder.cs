using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici
{
    public class ScriptModelloBuilder : IScriptModelloBuilder
    {
        private readonly IScriptModelloRepository _scriptModelloRepository;

        public ScriptModelloBuilder(IScriptModelloRepository scriptModelloRepository)
        {
            _scriptModelloRepository = scriptModelloRepository;
        }

        public ModelloDinamicoBase.ScriptsModello CostruisciScripts(ModelloDinamicoBase modello)
        {
            // Funzioni Condivise
            var script = this._scriptModelloRepository.GetById(modello.IdModello, TipoScriptEnum.Funzioni);
            var funzioniCondivise = script == null ? String.Empty : script.GetTestoScript();

            // Blocco using
            script = this._scriptModelloRepository.GetById(modello.IdModello, TipoScriptEnum.Using);
            var bloccoUsing = script == null ? String.Empty : script.GetTestoScript();

            var scriptCaricamento = CaricaScript(modello, TipoScriptEnum.Caricamento, funzioniCondivise, bloccoUsing);
            var scriptSalvataggio = CaricaScript(modello, TipoScriptEnum.Salvataggio, funzioniCondivise, bloccoUsing);
            var scriptModifica = CaricaScript(modello, TipoScriptEnum.Modifica, funzioniCondivise, bloccoUsing);
            var scriptElaborazioneMassiva = CaricaScript(modello, TipoScriptEnum.Massiva, funzioniCondivise, bloccoUsing);// new ScriptCampoDinamico(modello, script.GetTestoScript(), funzioniCondivise, bloccoUsing);

            return new ModelloDinamicoBase.ScriptsModello(scriptCaricamento, scriptSalvataggio, scriptModifica, scriptElaborazioneMassiva);
        }

        private ScriptCampoDinamico CaricaScript(ModelloDinamicoBase modello, TipoScriptEnum tipoScript, string funzioniCondivise, string bloccoUsing)
        {
            var script = this._scriptModelloRepository.GetById(modello.IdModello, tipoScript);

            if (script == null || String.IsNullOrEmpty(script.GetTestoScript()))
            {
                return null;
            }

            return new ScriptCampoDinamico(modello, script.GetTestoScript(), funzioniCondivise, bloccoUsing);
        }
    }
}
