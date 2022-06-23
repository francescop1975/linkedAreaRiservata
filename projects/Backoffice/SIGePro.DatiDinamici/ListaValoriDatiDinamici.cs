using System;
using System.Collections.Generic;

namespace Init.SIGePro.DatiDinamici
{
    /// <summary>
    /// Rappresenta una lista di valori associati ad un campo dinamico
    /// </summary>
    public partial class ListaValoriDatiDinamici : IEnumerable<ValoreDatiDinamici>
    {
        // Delegate e eventi per la notifica di una modifica
        internal delegate void ValoreModificatoDelegate(ListaValoriDatiDinamici sender, int indiceCampoModificato, bool perIncrementoMolteplicita);
        internal event ValoreModificatoDelegate ValoreModificato;

        /// <summary>
        /// Utilizzato internamente
        /// </summary>
        protected List<ValoreDatiDinamici> ListaValori { get; set; }

        /// <summary>
        /// Restituisce un riferimento al campo dinamico a cui la lista di valori appartiene
        /// </summary>
        public CampoDinamicoBase Campo { get; protected set; }

        /// <summary>
        /// REstituisce il numero di elementi presenti nella lista di valori
        /// </summary>
        public int Count { get { return this.ListaValori.Count; } }

        /// <summary>
        /// Restituisce il valore del campo all'indice specificato, se il valore è null restituisce il valore specificato nel parametro valoreDefault
        /// </summary>
        /// <typeparam name="T">Tipo in cui deve essere convertito il valore</typeparam>
        /// <param name="indice">Indice del valore che si intende leggere</param>
        /// <param name="valoreDefault">Valore da utilizzare se il valore del campo è null</param>
        /// <returns>Valore del campo all'indice specificato o valoreDefault se il campo è null</returns>
        /// <exception cref="System.IndexOutOfRangeException">Se l'indice specificato è maggiore del numero di valori del campo</exception>
        public T GetValoreODefault<T>(int indice, T valoreDefault)
        {
            if (indice >= this.Count)
                throw new IndexOutOfRangeException("Il campo dinamico non contiene l'indice " + indice);

            if (String.IsNullOrEmpty(this.ListaValori[indice].Valore))
                return valoreDefault;
            else
                return (T)Convert.ChangeType(this.ListaValori[indice].Valore, typeof(T));
        }

        private bool _notificaModificheSospesa = false;

        public void SospendiNotificaModifiche()
        {
            this._notificaModificheSospesa = true;
        }

        public void RipristinaNotificaModifiche()
        {
            this._notificaModificheSospesa = false;
        }

        /// <summary>
        /// Incrementa la molteplicità del campo
        /// </summary>
        public void IncrementaMolteplicita() { this.AggiungiValore("", ""); }

        /// <summary>
        /// Aggiunge un valore alla lista dei valori del campo
        /// </summary>
        /// <param name="valore"></param>
        /// <param name="valoreDecodificato"></param>
        public void AggiungiValore(string valore, string valoreDecodificato)
        {
            ValoreDatiDinamici val = new ValoreDatiDinamici(valore, valoreDecodificato);

            val.OnValoreCampoDinamicoModificato += new ValoreDatiDinamici.ValoreCampoDinamicoModificato(this.OnValoreCampoDinamicoModificato);

            this.ListaValori.Add(val);

            if (!this._notificaModificheSospesa)
                this.NotificaModifica(this.ListaValori.Count - 1, true);
        }

        /// <summary>
        /// Utilizzato internamente
        /// </summary>
        public void PplSvuotaValori(bool notificaModifica = true)
        {
            this.ListaValori.Clear();

            if (notificaModifica)
            {
                this.NotificaModifica(0);
            }
        }

        public void SvuotaValori(bool notificaModifica = true)
        {
            this.PplSvuotaValori(notificaModifica);
        }

        private void OnValoreCampoDinamicoModificato(ValoreDatiDinamici sender, string vecchioValore, string nuovoValore)
        {
            var index = this.ListaValori.IndexOf(sender);

            this.NotificaModifica(index);
        }

        internal void NotificaModifica(int indiceValoreModificato, bool perIncrementoMolteplicita = false)
        {
            ValoreModificato?.Invoke(this, indiceValoreModificato, perIncrementoMolteplicita);
        }

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="campo">Campo a cui appartiene la lista di valori</param>
        public ListaValoriDatiDinamici(CampoDinamicoBase campo)
        {
            this.Campo = campo;
            this.ListaValori = new List<ValoreDatiDinamici>();
        }

        /// <summary>
        /// Restituisce il valore all'indice specificato
        /// </summary>
        /// <param name="idx">Indice</param>
        /// <returns>valore all'indice specificato</returns>
        /// <remarks>Se l'indice specificato è maggiore del numero di valori presenti non viene sollevata un'eccezione. La molteplicità del campo viene incrementatà finchè non raggiunge il valore di idx</remarks>
        public ValoreDatiDinamici this[int idx]
        {
            get
            {
                while (idx >= this.ListaValori.Count)
                    this.AggiungiValore(String.Empty, String.Empty);

                return this.ListaValori[idx];
            }
        }

        /// <summary>
        /// Elimina il valore all'indice specificato
        /// </summary>
        /// <param name="index">Indice a cui rimuovere il valore</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        internal void RemoveAt(int index)
        {
            if (this.ListaValori.Count > index)
            {
                var oldVal = this.ListaValori[index];
                this.ListaValori.RemoveAt(index);
                oldVal.Valore = String.Empty;
            }
        }

        #region IEnumerable<ValoreDatiDinamici> Members

        public IEnumerator<ValoreDatiDinamici> GetEnumerator()
        {
            return this.ListaValori.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.ListaValori.GetEnumerator();
        }

        #endregion

        //public IEnumerable<X> Select<X>(Func<ValoreDatiDinamici, int, X> callback)
        //{
        //    for (int i = 0; i < this.ListaValori.Count; i++)
        //    {
        //        yield return callback(this.ListaValori[i], i);
        //    }
        //}
    }
}
