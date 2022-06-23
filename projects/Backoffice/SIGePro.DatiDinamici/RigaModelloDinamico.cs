using System.Collections.Generic;
using System.Linq;

namespace Init.SIGePro.DatiDinamici
{
    public class RigaModelloDinamico
    {
        // lista dei campi contenuti nella riga
        List<CampoDinamicoBase> _campi = new List<CampoDinamicoBase>();

        /// <summary>
        /// Restituisce o imposta il campo all'indice specificato
        /// </summary>
        /// <param name="idx">indice a cui impostare il campo</param>
        /// <returns>Campo che si trova all'indice specificato o null se il campo non viene trovato</returns>
        public CampoDinamicoBase this[int idx]
        {
            get
            {
                if (_campi.Count < (idx + 1))
                    return null;

                return _campi[idx];
            }

            set
            {
                while (_campi.Count < (idx + 1))
                    _campi.Add(null);

                _campi[idx] = value;
            }
        }

        /// <summary>
        /// restituisce il numero di colonne presenti nella riga
        /// </summary>
        public int NumeroColonne
        {
            get { return _campi.Count; }
        }

        /// <summary>
        /// Restituisce l'indice della riga all'interno del modello
        /// </summary>
        public int NumeroRiga { get; private set; }

        /// <summary>
        /// Restituisce o imposta se la riga è una riga multipla
        /// </summary>
        public TipoRigaEnum TipoRiga { get; set; }

        /// <summary>
        /// Restituisce l'id del gruppo a cui la riga appartiene 
        /// o -1 se la riga non appartiene a nessun gruppo
        /// </summary>
        public int IdGruppo { get; internal set; }

        internal IEnumerable<CampoDinamicoBase> Campi
        {
            get { return _campi; }
        }


        public RigaModelloDinamico(int numeroRiga)
        {
            NumeroRiga = numeroRiga;

            IdGruppo = -1;
        }

        public bool InterrompiTabellaDopo { get; set; }


        /// <summary>
        /// Restituisce la molteplicità della riga.
        /// La molteplicità dlla riga è l'indice di molteplicità più alto tra tutti i campi della riga
        /// </summary>
        /// <returns>Molteplicità della riga</returns>
        public int CalcolaMolteplicita()
        {
            if (_campi == null)
            {
                return 0;
            }

            return _campi.Where(x => x != null).Max(x => x.ListaValori.Count);
        }

        internal void IncrementaMolteplicitaSenzaNotificareModifica()
        {
            var molteplicitaAttuale = CalcolaMolteplicita();
            var molteplicitaModificata = molteplicitaAttuale + 1;

            _campi.Where(x => x != null).ToList().ForEach(campo =>
            {
                while (campo.ListaValori.Count < molteplicitaModificata)
                {
                    try
                    {
                        campo.ListaValori.SospendiNotificaModifiche();
                        campo.ListaValori.AggiungiValore("", "");
                    }
                    finally
                    {
                        campo.ListaValori.RipristinaNotificaModifiche();
                    }
                }
            });
        }

        internal void NotificaModificaBatch(int indiceMolteplicita)
        {
            _campi.Where(x => x != null).ToList().ForEach(campo =>
            {
                campo.ListaValori.NotificaModifica(indiceMolteplicita, true);
            });
        }


        ///// <summary>
        ///// Incrementa la molteplicità della riga verificando che essa sia consistente su tutti i campi.
        ///// </summary>
        //public void IncrementaMolteplicita()
        //{
        //    foreach (var campo in this._campi.Where(x => x != null))
        //    {
        //        try
        //        {
        //            campo.ListaValori.SospendiNotificaModifiche();
        //            campo.ListaValori.IncrementaMolteplicita();
        //        }
        //        finally
        //        {
        //            campo.ListaValori.RipristinaNotificaModifiche();
        //        }
        //    }

        //    VerificaConsistenzaMolteplicita();

        //    var molteplicitaAttuale = CalcolaMolteplicita();

        //    this.NotificaModificaBatch(molteplicitaAttuale - 1);
        //}


        /// <summary>
        /// Verifica che la molteplicità sia uguale per tutti i campi della riga
        /// </summary>
        internal void VerificaConsistenzaMolteplicita()
        {
            int molteplicita = CalcolaMolteplicita();

            foreach (var campo in this._campi.Where(x => x != null))
            {
                while (campo.ListaValori.Count < molteplicita)
                    campo.ListaValori.IncrementaMolteplicita();
            }
        }

        internal void EliminaValoriAllIndice(int indice)
        {
            for (int i = 0; i < _campi.Count; i++)
            {
                var campo = _campi[i];

                if (campo != null)
                {
                    campo.ListaValori.RemoveAt(indice);
                }
            }
        }
    }
}
