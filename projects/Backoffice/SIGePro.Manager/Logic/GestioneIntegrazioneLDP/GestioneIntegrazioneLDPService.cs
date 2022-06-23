using Init.SIGePro.Manager.Logic.GestioneDomandaOnLine;
using Init.SIGePro.Manager.Verticalizzazioni;
using Init.SIGePro.Verticalizzazioni;
using System;

namespace Init.SIGePro.Manager.Logic.GestioneIntegrazioneLDP
{
    public class GestioneIntegrazioneLDPService
    {
        private readonly ILdpProxyServiceWrapperFactory _factory;
        private readonly IDomandaOnlineService _foDomandeService;
        private readonly IVerticalizzazioneAttiva<VerticalizzazioneSitLdp> _checkVerticalizzazioneAttiva;

        public GestioneIntegrazioneLDPService(ILdpProxyServiceWrapperFactory factory, IDomandaOnlineService foDomandeService, IVerticalizzazioneAttiva<VerticalizzazioneSitLdp> checkVerticalizzazioneAttiva)
        {
            this._factory = factory;
            this._foDomandeService = foDomandeService;
            this._checkVerticalizzazioneAttiva = checkVerticalizzazioneAttiva;
        }

        public void AnnullaPrenotazione(int idDomanda)
        {
            try
            {
                var domanda = this._foDomandeService.GetById(idDomanda);

                if (_checkVerticalizzazioneAttiva.IsAttiva(domanda.Software))
                {
                    var annullamentoServiceWrapper = this._factory.GetAnnullamentoService(domanda.Software);

                    var occupazioneEsiste = annullamentoServiceWrapper.EsisteUnOccupazionePrenotata(domanda.Identificativodomanda);

                    if (occupazioneEsiste)
                    {
                        annullamentoServiceWrapper.EliminaOccupazione(domanda.Identificativodomanda);
                    }
                }
            }
            catch (Exception ex)
            {
                // throw new Exception("Errore durante l'annullamento della prenotazione dell'occupazione verso LDP:",ex);
                // ... 
                // Commentato il 22/04 per evitare un errore durrante l'eleiminazione di una pratica in sospeso a Siena
                // dove siamo integrati con LDP ma dove il loro ws non gestisce la cancellazione
                //
                // Stefano ha promesso di sistemarla :)
            }
        }
    }
}
