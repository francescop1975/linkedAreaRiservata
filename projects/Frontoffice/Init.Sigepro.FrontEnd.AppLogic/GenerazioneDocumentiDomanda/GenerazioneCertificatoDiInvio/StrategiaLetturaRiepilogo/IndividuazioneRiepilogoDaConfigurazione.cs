using CuttingEdge.Conditions;
using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.GestioneVisuraIstanza;
using System;

namespace Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.StrategiaLetturaRiepilogo
{
	public class IndividuazioneCertificatoInvioDaConfigurazione : IStrategiaIndividuazioneCertificatoInvio	
	{
		IConfigurazione<ParametriInvio> _configurazione;

		public IndividuazioneCertificatoInvioDaConfigurazione(IConfigurazione<ParametriInvio> configurazione)
		{
			Condition.Requires(configurazione, "configurazione").IsNotNull();

			this._configurazione = configurazione;
		}



		public bool IsCertificatoDefinito(int codiceIstanza)
		{
			return _configurazione.Parametri.CodiceOggettoInvioConFirmaDigitale.HasValue;
		}

		public int CodiceOggetto(int codiceIstanza)
        {
				if (!_configurazione.Parametri.CodiceOggettoInvioConFirmaDigitale.HasValue)
					throw new InvalidOperationException("Si sta cercando di leggere il riepilogo della domanda ma in configurazione non è presente nessun riepilogo");

				return _configurazione.Parametri.CodiceOggettoInvioConFirmaDigitale.Value;
		}
	}
}
