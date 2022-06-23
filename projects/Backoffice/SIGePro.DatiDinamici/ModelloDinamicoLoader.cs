// -----------------------------------------------------------------------
// <copyright file="ModelloDinamicoLoader.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.SIGePro.DatiDinamici
{
    using Init.SIGePro.DatiDinamici.Interfaces;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ModelloDinamicoLoader : IModelloDinamicoLoader
    {
        public enum TipoModelloDinamicoEnum
        {
            Frontoffice,
            Backoffice
        }

        public IDyn2DataAccessFactory DataAccessFactory { get; }
        public string Idcomune { get; }
        public string Token { get; }
        public bool IsModelloFrontoffice { get; }

        public ModelloDinamicoLoader(IDyn2DataAccessFactory dataAccessFactory, string idComune, TipoModelloDinamicoEnum tipoModello)
        {
            this.Idcomune = idComune;
            this.DataAccessFactory = dataAccessFactory;
            this.Token = dataAccessFactory.GetToken();
            this.IsModelloFrontoffice = tipoModello == TipoModelloDinamicoEnum.Frontoffice;
        }
    }
}
