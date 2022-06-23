using Init.Sigepro.FrontEnd.AppLogic.Configurazione.V2;
using Init.Sigepro.FrontEnd.AppLogic.ConversionePDF;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.Common;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.Configurazione;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.GestioneQrCode;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.LetturaXmlDomandaBackend;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio.StrategiaLetturaRiepilogo;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using log4net;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneCertificatoDiInvio
{
    public class GeneratoreCertificatoDiInvio
	{
		private static class Constants
		{
			public const string SegnapostoVisuraStc = "<!--VISURA_STC-->";
		}

		ILog _log = LogManager.GetLogger(typeof(GeneratoreCertificatoDiInvio));

        private readonly RiepilogoDomandaReader _riepilogoDomandaReader;
        private readonly IHtmlToPdfFileConverter _fileConverter;
        private readonly XmlDomandaBackendStrategy _visuraStrategy;
        private readonly ISostituzioneSegnapostoQrCode _sostituzioneQrCode;
        private readonly IConfigurazione<ParametriGenerazioneCertificatoInvio> _configurazione;
        private readonly IStrategiaIndividuazioneCertificatoInvio _strategiaIndividuazioneRiepilogo;

        public GeneratoreCertificatoDiInvio(RiepilogoDomandaReader riepilogoDomandaReader, IHtmlToPdfFileConverter fileConverter, XmlDomandaBackendStrategy visuraStrategy, 
                                            ISostituzioneSegnapostoQrCode sostituzioneQrCode, IConfigurazione<ParametriGenerazioneCertificatoInvio> configurazione,
                                            IStrategiaIndividuazioneCertificatoInvio strategiaIndividuazioneRiepilogo)
		{
			this._riepilogoDomandaReader = riepilogoDomandaReader;
            this._fileConverter = fileConverter;
            this._visuraStrategy = visuraStrategy;
            this._sostituzioneQrCode = sostituzioneQrCode;
            _configurazione = configurazione;
            _strategiaIndividuazioneRiepilogo = strategiaIndividuazioneRiepilogo;
        }

		public BinaryFile GeneraCertificatoDiInvio(int idDomandaBackoffice)
		{
			if (!this._strategiaIndividuazioneRiepilogo.IsCertificatoDefinito(idDomandaBackoffice))
			{
                _log.Error($"non è stato possibile generare un certificato di invio per l'istanza con codice istanza {idDomandaBackoffice}");
				return null;
			}

            var xslTemplate = _riepilogoDomandaReader.Read(idDomandaBackoffice, this._strategiaIndividuazioneRiepilogo);
            var xmlIstanza = LeggiXmlDomanda(idDomandaBackoffice, xslTemplate);

            DumpXmlIstanza(xmlIstanza);

            var htmlCertificato = new XslFile(xslTemplate).Trasforma(xmlIstanza);

            htmlCertificato = this._sostituzioneQrCode.ProcessaCertificato(Convert.ToInt32(idDomandaBackoffice), htmlCertificato);

            return this._fileConverter.Converti(_configurazione.Parametri.NomeFile, htmlCertificato);
		}

        private string LeggiXmlDomanda(int idDomandaBackoffice, string xslTemplate)
        {
            var tipoVisura = XmlDomandaBackendStrategy.TipoVisura.Vbg;

            if (xslTemplate.Contains(Constants.SegnapostoVisuraStc))
            {
                tipoVisura = XmlDomandaBackendStrategy.TipoVisura.Stc;
            }

            return this._visuraStrategy.GetXml(tipoVisura, idDomandaBackoffice);
        }
        
		private void DumpXmlIstanza(string xmlIstanza)
		{
            var dumpXmlIstanzaCaricata = ConfigurationManager.AppSettings["DumpXmlIstanzaDuranteGenerazioneCertificato"];

            if (String.IsNullOrEmpty(dumpXmlIstanzaCaricata))
                return;

			if(HttpContext.Current == null)
				return;

			var path = HttpContext.Current.Server.MapPath("~/Logs");
			path = Path.Combine(path, "dumpIstanzaCertificato_" + HttpContext.Current.Session.SessionID + ".xml");

			File.WriteAllText( path , xmlIstanza);
		}
	}
}
