using Init.SIGePro.DatiDinamici.Interfaces;
using Init.SIGePro.DatiDinamici.Scripts;
using Init.SIGePro.DatiDinamici.ValidazioneValoriCampi;
using Init.SIGePro.DatiDinamici.WebControls;
using System;
using System.Collections.Generic;

namespace Init.SIGePro.DatiDinamici
{
    public partial class CampoDinamico : CampoDinamicoBase
    {
        public CampoDinamico(ModelloDinamicoBase modello, IDyn2Campo campoDinamico,
                             IDyn2DataAccessFactory dataAccessFactory,
                            int posizioneOrizzontale, int posizioneVerticale)
            : base(modello, dataAccessFactory)
        {
            base.PosizioneOrizzontale = posizioneOrizzontale;
            base.PosizioneVericale = posizioneVerticale;
            
            Inizializza(campoDinamico);
        }

        private void Inizializza(IDyn2Campo campo)
        {
            Id = campo.Id.GetValueOrDefault(int.MinValue);
            NomeCampo = campo.Nomecampo;
            Etichetta = campo.Etichetta;
            Descrizione = campo.Descrizione;
            TipoCampo = (TipoControlloEnum)Enum.Parse(typeof(TipoControlloEnum), campo.Tipodato);

            // Carica lo script del modello
            var idComune = campo.Idcomune;
            var idCampo = campo.Id.GetValueOrDefault(int.MinValue);

            var scripts = this.DataAccessFactory.GetScriptCampiManager().GetScriptsCampo(idComune, idCampo);

            // Script caricamento
            if (scripts.ContainsKey(TipoScriptEnum.Caricamento))
            {
                var script = scripts[TipoScriptEnum.Caricamento];

                if (!String.IsNullOrEmpty(script.GetTestoScript()))
                    ScriptCaricamento = new ScriptCampoDinamico(ModelloCorrente, script.GetTestoScript(), "", "");
            }

            // Script modifica
            if (scripts.ContainsKey(TipoScriptEnum.Modifica))
            {
                var script = scripts[TipoScriptEnum.Modifica];

                if (!String.IsNullOrEmpty(script.GetTestoScript()))
                    ScriptModifica = new ScriptCampoDinamico(ModelloCorrente, script.GetTestoScript(), "", "");
            }

            // Script Salvataggio
            if (scripts.ContainsKey(TipoScriptEnum.Salvataggio))
            {
                var script = scripts[TipoScriptEnum.Salvataggio];

                if (!String.IsNullOrEmpty(script.GetTestoScript()))
                    ScriptSalvataggio = new ScriptCampoDinamico(ModelloCorrente, script.GetTestoScript(), "", "");
            }

            //Proprieta del controllo web (se esiste)
            //var mgrProprieta = ModelloCorrente.DataAccessProvider.GetProprietaCampiManager();

            var propList = this.DataAccessFactory.GetProprietaCampiManager().GetProprietaCampo(idComune, idCampo);

            try
            {
                propList.ForEach(p =>
                {

                    string key = p.Proprieta;
                    string val = p.Valore;

                    base.ImpostaProprietaRiservate(key, val);

                    ProprietaControlloWeb.Add(new KeyValuePair<string, string>(key, val));
                });
            }
            catch (Exception ex)
            {
                string fmtMsg = "Errore durante l'assegnazione delle proprietà del campo dinamico {0} ({1}): {2}";

                throw new Exception(String.Format(fmtMsg, campo.Id, campo.Nomecampo, ex.ToString()), ex);
            }
        }

        public override IEnumerable<ErroreValidazione> Valida()
        {
            var errori = new List<ErroreValidazione>();

            var validatori = new CampiDinamiciValidationService().GetValidatoriPer(this);

            foreach (var validatore in validatori)
            {
                errori.AddRange(validatore.GetErroriDiValidazione());
            }

            return errori;
        }
    }
}
