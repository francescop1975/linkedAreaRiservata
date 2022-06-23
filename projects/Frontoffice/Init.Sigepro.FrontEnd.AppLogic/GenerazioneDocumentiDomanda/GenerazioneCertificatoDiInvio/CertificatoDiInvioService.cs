// -----------------------------------------------------------------------
// <copyright file="CertificatoDiInvioService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio
{
    using CuttingEdge.Conditions;
    using Init.Sigepro.FrontEnd.AppLogic.Common;
    using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.AllegaCertificatoDiInvio;
    using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.StrategiaLetturaRiepilogo;
    using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
    using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
    using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
    using log4net;
    using System;

	public class CertificatoDiInvioService
	{
		ILog _log = LogManager.GetLogger(typeof(CertificatoDiInvioService));

        private readonly GeneratoreCertificatoDiInvio _generatoreCertificatoDiInvio;
        private readonly IAllegaCertificatoDiInvioService _allegaCertificatoDiInvioService;
        private readonly ICertificatoDiInvioFinder _certificatoDiInvioFinder;

        public CertificatoDiInvioService(GeneratoreCertificatoDiInvio generatoreCertificatoDiInvio, 
                                        IAllegaCertificatoDiInvioService allegaCertificatoDiInvioService, ICertificatoDiInvioFinder certificatoDiInvioFinder)
		{
			this._generatoreCertificatoDiInvio = generatoreCertificatoDiInvio;
            _allegaCertificatoDiInvioService = allegaCertificatoDiInvioService;
            _certificatoDiInvioFinder = certificatoDiInvioFinder;
        }


		public BinaryFile GeneraCertificatoDiInvio(int idDomandaBackoffice, bool allegaCertificatoAPratica = true)
		{
			try
			{
				var fileCertificato = this._generatoreCertificatoDiInvio.GeneraCertificatoDiInvio(idDomandaBackoffice);

                if (allegaCertificatoAPratica)
                {
                    this._allegaCertificatoDiInvioService.AllegaSeNonEsiste(idDomandaBackoffice, fileCertificato);
                }

                return fileCertificato;
			}
			catch (Exception ex)
			{
				_log.Error($"Generazione del certificato di invio fallita per la domanda con id domanda backoffice {idDomandaBackoffice}): {ex.ToString()}");

				throw;
			}
			finally
			{
				_log.Debug("Fine generazione del certificato di invio");
			}
		}

		public int? GetCodiceOggettoCertificatoDiInvioDaIdDomandaBackoffice(int idDomandaBackoffice)
		{
			try
			{
				_log.DebugFormat("Inizio lettura del certificato di invio allegato alla domanda backoffice con id {0}", idDomandaBackoffice);

				return this._certificatoDiInvioFinder.GetCodiceOggettoCertificatoDiInvio(idDomandaBackoffice);
			}
			catch (Exception ex)
			{
				_log.ErrorFormat("Errore durante la lettura del certificato di invio allegato alla domanda backoffice con id {0}: {1}", idDomandaBackoffice, ex.ToString());

				throw;
			}
			finally
			{
				_log.Debug("Fine della lettura del certificato di invio");
			}
		}
	}
}
