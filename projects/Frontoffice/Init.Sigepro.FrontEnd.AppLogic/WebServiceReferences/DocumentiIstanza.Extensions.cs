// -----------------------------------------------------------------------
// <copyright file="DocumentiIstanzaExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class DocumentiIstanzaOggetti
	{
		private static class Constants
		{
			public const string Md5Key = "MD5_SUM";
		}

		[SoapIgnore]
		[XmlIgnore]
		public bool HasMd5
		{
			get
			{
				return !String.IsNullOrEmpty(this.Md5);
			}
		}

		[SoapIgnore]
		[XmlIgnore]
		public string Md5
		{
			get { return this.GetMetadatoMd5(); }
			set { AggiungiMetadatoMd5(value); }
		}

		private string GetMetadatoMd5()
		{
			if (this.Metadati == null)
			{
				return string.Empty;
			}

			var metadato = this.Metadati.Where(x => x.Chiave == Constants.Md5Key).FirstOrDefault();

			return metadato == null ? String.Empty : metadato.Valore;
		}

		public void AggiungiMetadatoMd5(string md5)
        {
			var metadati = new List<OggettiMetadati>();

			if (this.Metadati != null)
            {
				metadati.AddRange(this.Metadati.Where(x => x.Chiave != Constants.Md5Key));
            }

			metadati.Add(new OggettiMetadati
			{
				Chiave = Constants.Md5Key,
				Valore = md5
			});

			this.Metadati = metadati.ToArray();
		}
	}
}
