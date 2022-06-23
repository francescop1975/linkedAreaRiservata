using Init.SIGePro.DatiDinamici;
using Init.SIGePro.Manager.Logic.DatiDinamici.DataAccess.Istanze;
using log4net;
using PersonalLib2.Data;
using System;
using System.Linq;

namespace Init.SIGePro.Manager.Logic.GestioneElaborazioneMassiva.SchedeIstanza
{
    public class ElaborazioneModelloScriptRunner : IElaborazioneModelloScriptRunner
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(ElaborazioneModelloScriptRunner));
        private readonly DataBase _db;
        private readonly string _idComune;

        public ElaborazioneModelloScriptRunner(DataBase db, string idComune)
        {
            _db = db;
            _idComune = idComune;
        }

        public void EseguiScriptElaborazioneMassiva(int codiceIstanza, int idScheda)
        {
            try
            {
                this._log.Debug($"Inizio elaborazione della scheda {idScheda} per la pratica {codiceIstanza}");

                var daf = new IstanzeDyn2DataAccessFactory(this._db, this._idComune, codiceIstanza);
                var loader = new ModelloDinamicoLoader(daf, this._idComune, ModelloDinamicoLoader.TipoModelloDinamicoEnum.Backoffice);
                var modello = new ModelloDinamicoIstanza(loader, idScheda, 0, false);

                if (modello.SupportaScriptElaborazioneMassiva)
                {
                    modello.EseguiScriptElaborazioneMassiva();
                }

                if (modello.HaErroriScript)
                {
                    var errori = string.Join(", ", modello.ErroriScript.Select(x => x.Messaggio));
                    throw new EsecuzioneScriptSchedaDinamicaException(errori);
                }
            }
            catch (Exception ex)
            {
                this._log.Error($"Errore durante l'elaborazione della scheda {idScheda} per la pratica {codiceIstanza}: {ex}");

                throw;
            }
            finally
            {
                this._log.Debug($"Elaborazione della scheda {idScheda} per la pratica {codiceIstanza}");
            }
        }
    }
}
