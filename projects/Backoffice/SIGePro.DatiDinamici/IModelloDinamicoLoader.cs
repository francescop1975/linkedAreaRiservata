namespace Init.SIGePro.DatiDinamici
{
	using System;
	using Init.SIGePro.DatiDinamici.Interfaces;

	public interface IModelloDinamicoLoader
	{
        IDyn2DataAccessFactory DataAccessFactory { get; }
        //IDyn2DataAccessProvider DataAccessProvider { get; }
		string Idcomune { get; }
		string Token { get; }
        bool IsModelloFrontoffice { get; }
    }
}
