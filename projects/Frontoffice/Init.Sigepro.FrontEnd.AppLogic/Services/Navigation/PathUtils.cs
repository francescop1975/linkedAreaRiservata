namespace Init.Sigepro.FrontEnd.AppLogic.Services.Navigation
{
    using Init.Sigepro.FrontEnd.AppLogic.Common;
    using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
    using System.Text;

	public class PathUtils
	{
		public static class UrlParameters
		{
			public const string Alias = "alias";
			public const string IdComune = "idComune";
			public const string Id = "id";
			public const string Token = "token";
			public const string Software = "software";
			public const string IdAllegato = "IdAllegato";
			public const string TipoAllegato = "TipoAllegato";
			public const string ReturnTo = "ReturnTo";
			public const string IdPresentazione = "IdPresentazione";
			public const string Convert = "convert";
			public const string CodiceOggetto = "codiceOggetto";
			public const string Timestamp = "_ts";
			public const string Fmt = "Fmt";
			public const string NomeFile = "nome";
			public const string PdfCompilabile = "pdfCompilabile";
		}

		public static class UrlParametersValues
		{
			public const string TipoAllegatoEndo = "E";
			public const string TipoAllegatoIntervento = "I";
			public const string True = "true";
			public const string False = "false";
		}

		public ReservedPath Reserved { get; private set; }

        IAliasSoftwareResolver _aliasSoftwareResolver;


        public PathUtils(IAliasSoftwareResolver aliasSoftwareResolver)
		{
			this.Reserved = new ReservedPath(aliasSoftwareResolver);
            this._aliasSoftwareResolver = aliasSoftwareResolver;
		}
	}
}
