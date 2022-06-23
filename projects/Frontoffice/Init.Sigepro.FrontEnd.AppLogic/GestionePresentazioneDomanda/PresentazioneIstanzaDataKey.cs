﻿using Init.Sigepro.FrontEnd.Pagamenti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda
{
	/// <summary>
	/// Incapsula i dati relativi alle chiavi necessarie per la serializzazione/deserializzazione
	/// di una presentazione istanza
	/// </summary>
	[Serializable]
	public class PresentazioneIstanzaDataKey : IRiferimentiDomandaPerPagamenti
	{
		public string IdComune { get; set; }
		
		public string Software { get; set; }
		
		public string CodiceUtente { get; set; }
		
		public int IdPresentazione { get; set; }

		[XmlIgnore]
		public string CodiceUnivocoDomanda => this.ToSerializationCode();

		public PresentazioneIstanzaDataKey()
        {

        }

		public static PresentazioneIstanzaDataKey New(string idComune, string software, string codiceUtente, int idPresentazione)
		{
			return new PresentazioneIstanzaDataKey(idComune, software, codiceUtente, idPresentazione);
		}

        public static PresentazioneIstanzaDataKey FromSerializationCode(string serializationCode)
        {
            var parts = serializationCode.Split('_');
            var idDomanda = 0;

            if (parts.Length != 4 || !Int32.TryParse(parts[3], out idDomanda)) 
            {
                throw new ArgumentException("Serialization code della domanda non valido: " + serializationCode);
            }

            return New(parts[0], parts[1], parts[2], idDomanda);

        }

		protected PresentazioneIstanzaDataKey(string idComune, string software, string codiceUtente, int idPresentazione)
		{
			IdComune = idComune;
			Software = software;
			CodiceUtente = codiceUtente;
			IdPresentazione = idPresentazione;
		}

		public static bool operator ==(PresentazioneIstanzaDataKey a, PresentazioneIstanzaDataKey b)
		{
			return (a.IdComune.ToUpper() == b.IdComune.ToUpper() &&
				a.Software.ToUpper() == b.Software.ToUpper() &&
				a.CodiceUtente.ToUpper() == b.CodiceUtente.ToUpper() &&
				a.IdPresentazione == b.IdPresentazione);
		}

		public static bool operator !=(PresentazioneIstanzaDataKey a, PresentazioneIstanzaDataKey b)
		{
			return (a.IdComune != b.IdComune || a.Software != b.Software || a.CodiceUtente != b.CodiceUtente || a.IdPresentazione != b.IdPresentazione);
		}

		public override string ToString()
		{
			return this.ToSerializationCode();
		}


		public string ToSerializationCode()
		{
			//string fmtString = "{0}_{1}_{2}_{3}_0000000";
			string fmtString = "{0}_{1}_{2}_{3}";

			string idComune = this.IdComune;
			string software = this.Software;
			string codiceUtente = this.CodiceUtente;
			string idPresentazione = this.IdPresentazione.ToString();

			if (codiceUtente != null)
			{
				if (codiceUtente.Length < 4)
					codiceUtente = codiceUtente.PadLeft(4, '0');
			}
			else
			{
				codiceUtente = "0000";
			}

			if (idPresentazione.Length < 4)
				idPresentazione = idPresentazione.PadLeft(4, '0');

			return String.Format(fmtString, idComune, software, codiceUtente, idPresentazione);
		}

		public override bool Equals(object obj)
		{
			return this == ((PresentazioneIstanzaDataKey)obj);
		}

		public override int GetHashCode()
		{
			return this.ToSerializationCode().GetHashCode();
		}


	}
}
