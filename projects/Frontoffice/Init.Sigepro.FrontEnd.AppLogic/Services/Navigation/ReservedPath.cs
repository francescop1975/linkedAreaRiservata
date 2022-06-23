namespace Init.Sigepro.FrontEnd.AppLogic.Services.Navigation
{
    using Init.Sigepro.FrontEnd.AppLogic.Common;
    using Init.Sigepro.FrontEnd.Infrastructure.UrlsAndPaths;
    using System.Text;

	public class ReservedPath
	{
		public InserimentoIstanzaPath InserimentoIstanza { get; private set; }

        IAliasSoftwareResolver _aliasSoftwareResolver;


        public ReservedPath(IAliasSoftwareResolver aliasSoftwareResolver)
		{
			this.InserimentoIstanza = new InserimentoIstanzaPath(aliasSoftwareResolver);
            this._aliasSoftwareResolver = aliasSoftwareResolver;
		}

	}
}
