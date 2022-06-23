using Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg.TraduzioneIdComune;
using Init.Sigepro.FrontEnd.AppLogic.GestioneComuni;
using Init.Sigepro.FrontEnd.AppLogic.GestioneInterventi;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.AmbitoRicercaIntervento;
using Init.Sigepro.FrontEnd.AppLogic.Repositories.Interfaces;
using Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Adapters.SigeproPartialAdapters
{
    public class DatiGeneraliSigeproAdapter : IIstanzaSigeproPartialAdapter
    {
        private readonly IInterventiRepository _alberoprocRepository;
        private readonly IComuniService _comuniService;
        private readonly IConfigurazioneAreaRiservataRepository _configurazioneAreaRiservataRepository;
        private readonly IStatiIstanzaRepository _statiIstanzaRepository;
        private readonly IAliasToIdComuneTranslator _aliasToIdComuneTranslator;

        public DatiGeneraliSigeproAdapter(IInterventiRepository alberoprocRepository, IComuniService comuniService, IConfigurazioneAreaRiservataRepository configurazioneAreaRiservataRepository, 
            IStatiIstanzaRepository statiIstanzaRepository, IAliasToIdComuneTranslator aliasToIdComuneTranslator)
        {
            _alberoprocRepository = alberoprocRepository;
            _comuniService = comuniService;
            _configurazioneAreaRiservataRepository = configurazioneAreaRiservataRepository;
            _statiIstanzaRepository = statiIstanzaRepository;
            _aliasToIdComuneTranslator = aliasToIdComuneTranslator;
        }

        public void Adatta(IDomandaOnlineReadInterface _readInterface, Istanze istanzaSigepro, IstanzaSigeproAdapterFlags flags)
        {
            var aliasComune = _readInterface.AltriDati.AliasComune;
            var software = _readInterface.AltriDati.Software;

            istanzaSigepro.IDCOMUNE = _aliasToIdComuneTranslator.Translate(_readInterface.AltriDati.AliasComune);
            istanzaSigepro.SOFTWARE = software;

            if (_readInterface.AltriDati.Intervento != null)
            {
                var codiceIntervento = _readInterface.AltriDati.Intervento.Codice;

                istanzaSigepro.CODICEINTERVENTOPROC = codiceIntervento.ToString();

                var datiIntervento = _alberoprocRepository.GetDettagliIntervento(aliasComune, codiceIntervento, new AmbitoRicercaAreaRiservata(true), false);

                // Risolvo la descrizione dell'intervento
                istanzaSigepro.Intervento = new Init.Sigepro.FrontEnd.AppLogic.WebServiceReferences.IstanzeService.AlberoProc
                {
                    SC_DESCRIZIONE = _readInterface.AltriDati.Intervento.Descrizione,
                    SC_NOTE = datiIntervento.Note
                };
            }

            if (!String.IsNullOrEmpty(_readInterface.AltriDati.CodiceComune))
            {
                istanzaSigepro.CODICECOMUNE = _readInterface.AltriDati.CodiceComune;

                var com = _comuniService.GetDatiComune(istanzaSigepro.CODICECOMUNE);

                if (com != null)
                {
                    istanzaSigepro.ComuneIstanza = new Comuni
                    {
                        CODICECOMUNE = com.CodiceComune,
                        COMUNE = com.Comune,
                        PROVINCIA = com.Provincia,
                        SIGLAPROVINCIA = com.SiglaProvincia
                    };
                }
            }

            istanzaSigepro.LAVORIESTESA = _readInterface.AltriDati.Note;
            istanzaSigepro.LAVORI = _readInterface.AltriDati.DescrizioneLavori;
            istanzaSigepro.DATA = DateTime.Now.Date;
            istanzaSigepro.CHIUSURA = _configurazioneAreaRiservataRepository.DatiConfigurazione(aliasComune, software).StatoInizialeIstanza;

            if (!String.IsNullOrEmpty(istanzaSigepro.CHIUSURA))
            {
                istanzaSigepro.Stato = new StatiIstanza
                {
                    Codicestato = istanzaSigepro.CHIUSURA,
                    Stato = _statiIstanzaRepository.GetById(aliasComune, software, istanzaSigepro.CHIUSURA).Stato
                };
            };
        }
    }
}
