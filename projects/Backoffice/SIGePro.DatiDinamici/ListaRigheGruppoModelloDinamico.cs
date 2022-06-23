using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici
{
	internal class ListaRigheGruppoModelloDinamico : IList<RigaModelloDinamico>
	{
		List<RigaModelloDinamico> _righeAppartenentiAlGruppo = new List<RigaModelloDinamico>();

        private readonly int _idGruppo;

        internal ListaRigheGruppoModelloDinamico(int idGruppo)
		{
			this._idGruppo = idGruppo;
		}

		/// <summary>
		/// Restituisce la molteplicità del gruppo.
		/// La molteplicità del gruppo è l'indice di molteplicità più alto tra tutte le righe del gruppo
		/// </summary>
		/// <returns>Molteplicità del gruppo</returns>
		internal int CalcolaMolteplicita()
		{
            if (this._righeAppartenentiAlGruppo == null)
            {
                return 0;
            }

            return this._righeAppartenentiAlGruppo.Max(x => x.CalcolaMolteplicita());
		}

		/// <summary>
		/// Incrementa la molteplicità del gruppo verificando che essa sia consistente su tutti i campi.
		/// </summary>
		internal void IncrementaMolteplicita()
		{
            foreach (var riga in this._righeAppartenentiAlGruppo)
            {
                riga.IncrementaMolteplicitaSenzaNotificareModifica();
            }

			VerificaConsistenzaMolteplicita();

            var molteplicitaAttuale = CalcolaMolteplicita();

            foreach (var riga in this._righeAppartenentiAlGruppo)
            {
                riga.NotificaModificaBatch(molteplicitaAttuale - 1);
            }
        }


		/// <summary>
		/// Verifica che la molteplicità sia uguale per tutti i campi del gruppo
		/// </summary>
		internal void VerificaConsistenzaMolteplicita()
		{
			int molteplicita = CalcolaMolteplicita();

			for (int i = 0; i < _righeAppartenentiAlGruppo.Count; i++)
			{
                while (_righeAppartenentiAlGruppo[i].CalcolaMolteplicita() < molteplicita)
                {
                    _righeAppartenentiAlGruppo[i].IncrementaMolteplicitaSenzaNotificareModifica();
                }
            }
		}

		internal void EliminaValoriAllIndice(int indice)
		{
			for (int i = 0; i < _righeAppartenentiAlGruppo.Count; i++)
				_righeAppartenentiAlGruppo[i].EliminaValoriAllIndice(indice);

			VerificaConsistenzaMolteplicita();
		}

		public bool IsVisibile()
		{
			foreach (var riga in _righeAppartenentiAlGruppo)
			{
				foreach (var campo in riga.Campi)
				{
					if (campo == null)
					{
						continue;
					}


					foreach (var valore in campo.ListaValori.Cast<ValoreDatiDinamici>())
					{
						if (valore.Visibile)
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		#region IList<RigaModelloDinamico> Members

		public int IndexOf(RigaModelloDinamico item)
		{
			return _righeAppartenentiAlGruppo.IndexOf(item);
		}

		public void Insert(int index, RigaModelloDinamico item)
		{
			item.IdGruppo = _idGruppo;

			_righeAppartenentiAlGruppo.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			var obj = _righeAppartenentiAlGruppo[index];

			if (obj != null)
				obj.IdGruppo = -1;

			_righeAppartenentiAlGruppo.RemoveAt(index);
		}

		public RigaModelloDinamico this[int index]
		{
			get
			{
				return _righeAppartenentiAlGruppo[index];
			}
			set
			{
				value.IdGruppo = _idGruppo;
				_righeAppartenentiAlGruppo[index] = value;
			}
		}

		#endregion

		#region ICollection<RigaModelloDinamico> Members

		public void Add(RigaModelloDinamico item)
		{
			item.IdGruppo = _idGruppo;

			_righeAppartenentiAlGruppo.Add(item);
		}

		public void Clear()
		{
			for (int i = 0; i < this.Count; i++)
				this[i].IdGruppo = -1;

			_righeAppartenentiAlGruppo.Clear();
		}

		public bool Contains(RigaModelloDinamico item)
		{
			return _righeAppartenentiAlGruppo.Contains(item);
		}

		public void CopyTo(RigaModelloDinamico[] array, int arrayIndex)
		{
			_righeAppartenentiAlGruppo.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return _righeAppartenentiAlGruppo.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(RigaModelloDinamico item)
		{
			if (this.Contains(item))
				item.IdGruppo = -1;

			return _righeAppartenentiAlGruppo.Remove(item);
		}

		#endregion

		#region IEnumerable<RigaModelloDinamico> Members

		public IEnumerator<RigaModelloDinamico> GetEnumerator()
		{
			return _righeAppartenentiAlGruppo.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _righeAppartenentiAlGruppo.GetEnumerator();
		}

		#endregion
	}
}
