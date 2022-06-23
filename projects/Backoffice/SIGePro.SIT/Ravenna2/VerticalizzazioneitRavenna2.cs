﻿using Init.SIGePro.Verticalizzazioni;
using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Sit.Ravenna2
{
	public class VerticalizzazioneSitRavenna2 : Verticalizzazione
	{
		public static class Constants
		{
			public const string NomeVerticalizzazione = "SIT_RAVENNA2";
			public const string ConnectionString = "CONNECTION_STRING";
			public const string PrefissoTabelle = "PREFISSO_TABELLE";
			public const string UrlCartografiaDaCivico = "URL_CARTOGRAFIA_DA_CIVICO";
			public const string UrlCartografiaDaMappale = "URL_CARTOGRAFIA_DA_MAPPALE";
			public const string DatabaseClientType = "DATABASE_CLIENT_TYPE";
		}

		public VerticalizzazioneSitRavenna2()
			: base()
		{
		}

		public VerticalizzazioneSitRavenna2(bool attiva)
			: base()
		{
			base.Attiva = attiva;
		}

		public VerticalizzazioneSitRavenna2(string idComuneAlias, string software)
			: base(idComuneAlias, Constants.NomeVerticalizzazione, software)
		{
		}

		public string ConnectionString
		{
			get { return GetString(Constants.ConnectionString); }
			set { SetString(Constants.ConnectionString, value); }
		}

		public string PrefissoTabelle
		{
			get { return GetString(Constants.PrefissoTabelle); }
			set { SetString(Constants.PrefissoTabelle, value); }
		}

		public string UrlCartografiaDaCivico
		{
			get { return GetString(Constants.UrlCartografiaDaCivico); }
			set { SetString(Constants.UrlCartografiaDaCivico, value); }
		}

		public string UrlCartografiaDaMappale
		{
			get { return GetString(Constants.UrlCartografiaDaMappale); }
			set { SetString(Constants.UrlCartografiaDaMappale, value); }
		}

		public ProviderType DatabaseClientType
		{
			get
			{
				var client = GetString(Constants.DatabaseClientType);

				if (String.IsNullOrEmpty(client))
				{
					return ProviderType.OracleClient;
				}

				return (ProviderType)Enum.Parse(typeof(ProviderType), client);
			}
		}
	}
}
