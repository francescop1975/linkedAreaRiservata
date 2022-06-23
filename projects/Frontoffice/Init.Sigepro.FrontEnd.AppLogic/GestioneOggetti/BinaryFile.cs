﻿// -----------------------------------------------------------------------
// <copyright file="BinaryFile.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti
{
	using System;
	using System.IO;
	using System.Web;
	using System.Web.UI.WebControls;
    using Init.Sigepro.FrontEnd.Infrastructure;
    using Init.Utils;

	public class BinaryFile
	{
        public static BinaryFile FromFileData(string nomeFile, string mimeType, byte[] bytes)
        {
            return new BinaryFile(nomeFile, mimeType, bytes);
        }

		public string FileName { get; set; }

		public string MimeType { get; set; }

		public byte[] FileContent { get; set; }

		public string Estensione
		{
			get
			{
				return Path.GetExtension(FileName);
			}
		}

		public virtual int Size { get { return this.FileContent.Length; } }

		internal BinaryFile()
		{
		}

		internal BinaryFile(string nomeFile, string mimeType, byte[] bytes)
		{
			Initialize(nomeFile, mimeType, bytes);
		}

		private void Initialize(string nomeFile, string mimeType, byte[] bytes)
		{
			if (String.IsNullOrEmpty(Path.GetExtension(nomeFile)))
			{
				throw new ArgumentException("Il file caricato è privo di estensione. Verificare che il nome file contenga un'estensione valida");
			}

			FileName = Path.GetFileName(nomeFile);
			MimeType = mimeType;
			FileContent = bytes;
		}

		public BinaryFile(FileUpload fileUpload, IValidPostedFileSpecification specification)
			:this(fileUpload.PostedFile, specification)
		{
		}

		public BinaryFile(HttpPostedFile postedFile, IValidPostedFileSpecification specification)
		{
			if (postedFile.ContentLength == 0)
			{
				throw new InvalidOperationException("Il file è vuoto o non è valido");
			}

			if(!specification.IsSatisfiedBy(postedFile))
			{
				throw new InvalidOperationException(specification.ErrorMessage);
			}

			Initialize(postedFile.FileName, postedFile.ContentType, StreamUtils.StreamToBytes(postedFile.InputStream));
		}

		public void WriteTo(HttpResponse response)
        {
			response.Clear();
			response.AddHeader("content-disposition", "attachment; filename=\"" + this.FileName.Replace("\"", "_") + "\"");
			response.ContentType = this.MimeType;
			response.BinaryWrite(this.FileContent);
		}
	}
}
