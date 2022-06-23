using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici
{
	/// <summary>
	/// Rappresenta uno dei valori di un campo dinamico
	/// </summary>
	public partial class ValoreDatiDinamici
	{
		internal delegate void ValoreCampoDinamicoModificato(ValoreDatiDinamici sender, string vecchioValore, string nuovoValore);
		internal event ValoreCampoDinamicoModificato OnValoreCampoDinamicoModificato;

		private string _vecchioValore;
		private string _valore;
		private string _valoreDecodificato = String.Empty;
		private bool _visibile = true;

		/// <summary>
		/// Valore del campo
		/// </summary>
		public string Valore
		{
			get { return _valore; }
			set
			{
				if (value == null)
					value = String.Empty;

				string vecchioValore = _vecchioValore;

				this._vecchioValore = _valore = value;
				this.ValoreDecodificato = value;

				if (vecchioValore != _valore)
				{
					if (OnValoreCampoDinamicoModificato != null)
						OnValoreCampoDinamicoModificato(this, vecchioValore, _valore);
				}
			}
		}

		/// <summary>
		/// Valore decodificato (descrittivo) del campo
		/// </summary>
		public string ValoreDecodificato
		{
			get { return this._valoreDecodificato; }
			set { this._valoreDecodificato = value; }
		}

		/// <summary>
		/// Imposta Contemporaneamente valore e valore decodificato per evitare che si verifichino problemi
		/// come nelle date per cui l'evento campoModificato viene generato prima di impostare correttamente
		/// il valore decodificato
		/// </summary>
		/// <param name="valore"></param>
		/// <param name="valoreDecodificato"></param>
		public void SetValore(string valore, string valoreDecodificato)
        {
			if (valore == null) {
				valore = String.Empty;
				valoreDecodificato = String.Empty;
			}

			string vecchioValore = _vecchioValore;

			this._vecchioValore = valore;
			this._valore = valore;
			this._valoreDecodificato = valoreDecodificato;

			if (vecchioValore != _valore)
			{
				if (OnValoreCampoDinamicoModificato != null)
					OnValoreCampoDinamicoModificato(this, vecchioValore, _valore);
			}
		}

		/// <summary>
		/// Gestisce la visibilità del valore
		/// </summary>
		internal bool Visibile 
		{
			get { return this._visibile; }
			set 
			{
				this._visibile = value;

				if (!this._visibile)
				{
					this._valore = String.Empty;
					this._valoreDecodificato = String.Empty;
					this._vecchioValore = String.Empty;
				}
			} 
		}

		/// <summary>
		/// Imposta direttamente il flag di visibilità del valore senza settare a "" il valore o il valore decodificato.
		/// Serve ad esempio quando viene nascosto un bottone che non deve triggerare l'evento di modifica
		/// </summary>
		/// <param name="visibile"></param>
		internal void SetVisibileInternal(bool visibile)
        {
			this._visibile = visibile;
		}

		/// <summary>
		/// Restituisce il valore del campo o valoreDefault se il valore del campo è null o String.Empty
		/// </summary>
		/// <typeparam name="T">Tipo in cui il campo deve essere convertito</typeparam>
		/// <param name="valoreDefault">Valore che deve essere restituito se il valore del campo è null o String.Empty</param>
		/// <returns>valore del campo o valoreDefault se il valore del campo è null o String.Empty</returns>
		/// <remarks>Utilizza internamente <see cref="System.Convert.ChangeType"/> per effettuare la conversione. Prima di utilizzare il metodo verificare che esista una conversione valida</remarks>
		/// <exception cref="System.InvalidCastException"></exception>
		/// <exception cref="System.ArgumentNullException"></exception>
		public T GetValoreODefault<T>(T valoreDefault)
		{
			if (String.IsNullOrEmpty(this.Valore))
				return valoreDefault;
			else
				return (T)Convert.ChangeType(this.Valore, typeof(T));
		}


		/// <summary>
		/// Costruttore, inizializza il valore del campo dinamico con i valori passati
		/// </summary>
		/// <param name="valore">Valore del campo dinamico</param>
		/// <param name="valoreDecodificato">Valore decodificato del campo dinamico</param>
		internal ValoreDatiDinamici(string valore, string valoreDecodificato, bool visibile = true)
		{
			this._vecchioValore = _valore = valore;
			this.ValoreDecodificato = valoreDecodificato;
			this.Visibile = visibile;
		}
	}
}
