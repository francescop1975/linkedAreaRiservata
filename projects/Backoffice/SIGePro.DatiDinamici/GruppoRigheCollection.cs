using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.DatiDinamici
{
	internal class GruppoRigheCollection
	{
		private Dictionary<int, ListaRigheGruppoModelloDinamico> _gruppi;

		internal GruppoRigheCollection()
		{
			this._gruppi = new Dictionary<int, ListaRigheGruppoModelloDinamico>();
		}

		internal void AggiungiRigaAGruppo(int idGruppo, RigaModelloDinamico riga)
		{
			var raggruppamento = GetRaggruppamentoById(idGruppo);

			if (raggruppamento == null)
			{
				raggruppamento = new ListaRigheGruppoModelloDinamico(idGruppo);
                this._gruppi.Add(idGruppo, raggruppamento);
			}

			raggruppamento.Add(riga);
		}

		internal IEnumerable<ListaRigheGruppoModelloDinamico> GetRaggruppamenti()
		{
			return this._gruppi.Values;
		}

		internal ListaRigheGruppoModelloDinamico GetRaggruppamentoById(int id)
		{
			ListaRigheGruppoModelloDinamico val = null;

			if (this._gruppi.TryGetValue(id, out val))
				return val;

			return null;
		}
	}
}
