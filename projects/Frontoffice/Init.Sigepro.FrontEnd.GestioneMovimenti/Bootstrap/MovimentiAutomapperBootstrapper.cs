// -----------------------------------------------------------------------
// <copyright file="MovimentiAutomapperBootstrapper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.GestioneMovimenti.Bootstrap
{
	using AutoMapper;
	using Init.Sigepro.FrontEnd.GestioneMovimenti.Converters;
	using Init.SIGePro.DatiDinamici.GestioneLocalizzazioni.StringaFormattazioneIndirizzi;
	using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
    using Init.Sigepro.FrontEnd.GestioneMovimenti.GestioneMovimento.GestioneMovimentoDiOrigine;
    using Init.Sigepro.FrontEnd.GestioneMovimenti.GestioneMovimento.GestioneMovimentoDaEffettuare;
    using Init.SIGePro.Manager.DTO.Scadenzario;

    /// <summary>
    /// Inizializzatore del componente automapper relativo al componente di gesione movimenti del frontoffice
    /// </summary>
    public class MovimentiAutomapperBootstrapper
	{
		/// <summary>
		/// Inizializza l'automapper
		/// </summary>
		public static void Bootstrap()
		{
			Mapper.CreateMap<DatiMovimentoDaEffettuareDto, MovimentoDiOrigine>().ConvertUsing(new DatiMovimentoToDatiMovimentoDiOrigineConverter());
			Mapper.CreateMap<IstanzeStradario, LocalizzazioneIstanza>().ConvertUsing(new IstanzeStradarioToLocalizzazioneIstanzaConverter());

			Mapper.AssertConfigurationIsValid();
		}
	}
}
