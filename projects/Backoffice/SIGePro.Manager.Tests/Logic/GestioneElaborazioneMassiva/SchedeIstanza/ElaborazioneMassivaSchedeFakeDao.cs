using Init.SIGePro.Manager.Logic.GestioneElaborazioneMassiva.SchedeIstanza;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIGePro.Manager.Tests.Logic.GestioneElaborazioneMassiva.SchedeIstanza
{

    public class ElaborazioneMassivaSchedeFakeDao : IElaborazioneMassivaSchedeIstanzaDao
    {
        public class Pratica
        {
            public int CodiceIstanza { get; set; }
            public string Stato { get; set; }
            public string Messaggio { get; set; }

            public Pratica(int codiceIstanza, string stato)
            {
                CodiceIstanza = codiceIstanza;
                Stato = stato;
            }
        }

        public DateTime? _dataAvvioElaborazione = null;
        public DateTime? _dataFineElaborazione = null;
        public List<Pratica> _pratiche = new List<Pratica>();

        public ElaborazioneMassivaSchedeFakeDao()
        {
            _pratiche.Add(new Pratica(1, EsitoElaborazioneMassivaSchedeEnum.ProntaPerElaborazione.ToString()));
            _pratiche.Add(new Pratica(2, EsitoElaborazioneMassivaSchedeEnum.ProntaPerElaborazione.ToString()));
        }

        public void AvviaElaborazione(int idElaborazione)
        {
            this._dataAvvioElaborazione = DateTime.Now;
        }

        public void AvviaElaborazioneIstanza(int idElaborazione, int codiceIstanza)
        {
            _pratiche.Where(x => x.CodiceIstanza == codiceIstanza)
                     .FirstOrDefault()
                     .Stato = EsitoElaborazioneMassivaSchedeEnum.ElaborazioneInCorso.ToString();
        }

        public IEnumerable<PraticaDaElaborare> GetPraticheDaElaborare(int idElaborazione)
        {
            return this._pratiche.Select(x => new PraticaDaElaborare(idElaborazione, x.CodiceIstanza));
        }

        public IEnumerable<SchedaDaElaborare> GetSchedeDaElaborare(int idElaborazione)
        {
            return Enumerable.Range(1, 3).Select(x => new SchedaDaElaborare(x));
        }

        public void ImpostaErroriElaborazione(int idElaborazione, int codiceIstanza, IEnumerable<string> messaggi)
        {
            var riga = _pratiche.Where(x => x.CodiceIstanza == codiceIstanza)
                     .FirstOrDefault();

            riga.Stato = EsitoElaborazioneMassivaSchedeEnum.ElaborazioneCompletataConErrori.ToString();
            riga.Messaggio = String.Join(Environment.NewLine, messaggi);
        }

        public bool IsElaborazioneInCorso(int idElaborazione)
        {
            return this._dataAvvioElaborazione != null && this._dataFineElaborazione == null;
        }

        public bool PraticaContieneScheda(int codiceIstanza, int idSchedaDinamica)
        {
            return true;
        }

        public void TerminaElaborazione(int idElaborazione)
        {
            this._dataFineElaborazione = DateTime.Now;
        }

        public void TerminaElaborazioneIstanza(int idElaborazione, int codiceIstanza)
        {
            var riga = _pratiche.Where(x => x.CodiceIstanza == codiceIstanza)
                     .FirstOrDefault();

            riga.Stato = EsitoElaborazioneMassivaSchedeEnum.ElaborazioneCompletataConSuccesso.ToString();
        }
    }

}
