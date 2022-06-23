using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.GestioneOggetti;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using System.IO;
using System.Web;
using Init.Sigepro.FrontEnd.AppLogic.Adapters;
using System.Configuration;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.Common;
using Init.Sigepro.FrontEnd.AppLogic.ConversionePDF;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo;
using Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda.GestioneSostituzioneSegnapostoRiepilogo.LetturaDatiDinamici.LetturaDaDomandaOnline;
using Init.Sigepro.FrontEnd.AppLogic.DatiDinamici;
using Init.Sigepro.FrontEnd.AppLogic.Adapters.SigeproPartialAdapters;

namespace Init.Sigepro.FrontEnd.AppLogic.GenerazioneDocumentiDomanda.GenerazioneRiepilogoDomanda
{
    public class RiepilogoDomanda
    {
        private readonly IOggettiService _oggettiService;
        private readonly IHtmlToPdfFileConverter _fileConverter;
        private readonly SostituzioneSegnapostoRiepilogoService _sostituzioneSegnapostoRiepilogoService;
        private readonly IDatiDinamiciRepository _datiDinamiciRepository;
        private readonly IIstanzaSigeproAdapterService _istanzaSigeproAdapterService;

        public RiepilogoDomanda(IOggettiService oggettiService, SostituzioneSegnapostoRiepilogoService sostituzioneSegnapostoRiepilogoService, 
                                IHtmlToPdfFileConverter fileConverter, IDatiDinamiciRepository datiDinamiciRepository, IIstanzaSigeproAdapterService istanzaSigeproAdapterService)
        {
            this._oggettiService = oggettiService;
            this._fileConverter = fileConverter;
            this._sostituzioneSegnapostoRiepilogoService = sostituzioneSegnapostoRiepilogoService;
            this._datiDinamiciRepository = datiDinamiciRepository;
            _istanzaSigeproAdapterService = istanzaSigeproAdapterService;
        }

        public BinaryFile GeneraRiepilogoDomandaOnline(DomandaOnline domanda, int idFileModello, bool aggiungiPdfSchedeAListaAllegati, bool dumpXml = false)
        {
            var oggetto = _oggettiService.GetById(idFileModello);
            var idDomanda = domanda.DataKey.ToString();

            if (oggetto == null)
                throw new ArgumentException("L'oggetto " + idFileModello + " non è stato trovato");

            var istanzaXml = this._istanzaSigeproAdapterService.ToIstanzaBackoffice(
                    domanda.ReadInterface, 
                    new IstanzaSigeproAdapterFlags
                    { 
                        AggiungiPdfSchedeAListaAllegati = aggiungiPdfSchedeAListaAllegati
                    }).ToXmlModelloRiepilogo(idDomanda);

            if (dumpXml)
            {
                DumpDatiDomanda(idDomanda, istanzaXml);
            }

            var risultatoTrasformazione = new XslFile(oggetto.FileContent).Trasforma(istanzaXml);

            // Nel caso in cui il modello contenga il segnaposto delle schede dinamiche utilizzo il servizio
            // per leggerle in formato html
            var reader = new DomandaOnlineDatiDinamiciReader(domanda, this._datiDinamiciRepository, this._istanzaSigeproAdapterService);
            var risultatoTrasformazioneConSchede = _sostituzioneSegnapostoRiepilogoService.ProcessaRiepilogo(reader, risultatoTrasformazione);

            var nomeFile = String.Format("modello-domanda.{0}.pdf", idDomanda);
            var pdf = this._fileConverter.Converti(nomeFile, risultatoTrasformazioneConSchede);

            return pdf;
        }
        
        private static void DumpDatiDomanda(string idDomanda, string istanzaXml)
        {
            if (HttpContext.Current != null)
            {
                var path = HttpContext.Current.Server.MapPath("~/Logs/");
                path = Path.Combine(path, $"riepilogo_{idDomanda}.xml");
                using (var fs = File.Open(path, FileMode.CreateNew))
                {
                    fs.Write(Encoding.UTF8.GetBytes(istanzaXml), 0, Encoding.UTF8.GetByteCount(istanzaXml));
                }
            }
        }
    }
}
