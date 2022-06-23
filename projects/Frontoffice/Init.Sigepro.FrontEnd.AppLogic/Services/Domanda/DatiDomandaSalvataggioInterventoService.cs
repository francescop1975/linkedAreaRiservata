using CuttingEdge.Conditions;
using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.Sigepro.FrontEnd.AppLogic.GestioneInterventi;
using Init.Sigepro.FrontEnd.AppLogic.PresentazioneIstanze.Workflow;
using Init.Sigepro.FrontEnd.AppLogic.SalvataggioDomanda;
using Init.Sigepro.FrontEnd.AppLogic.ServiceCreators;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.Services.Domanda
{
    public class DatiDomandaSalvataggioInterventoService: DatiDomandaService
    {
        AreaRiservataServiceCreator _serviceCreator;
        ILog _logger = LogManager.GetLogger(typeof(DatiDomandaSalvataggioInterventoService));

        internal DatiDomandaSalvataggioInterventoService(ISalvataggioDomandaStrategy salvataggioStrategy, IResolveDescrizioneIntervento resolveDescrizioneIntervento,
                                    IAliasSoftwareResolver aliasSoftwareResolver, IWorkflowService workflowService, AreaRiservataServiceCreator serviceCreator) : base(salvataggioStrategy, resolveDescrizioneIntervento, aliasSoftwareResolver, workflowService)
        {
            this._serviceCreator = serviceCreator;
        }

        public override void ImpostaIdIntervento(int idDomanda, int idIntervento, int? idAttivitaAtecoSelezionata, bool popolaDescrizioneLavoriDaIntervento)
        {
            base.ImpostaIdIntervento(idDomanda, idIntervento, idAttivitaAtecoSelezionata, popolaDescrizioneLavoriDaIntervento);

            using (var ws = this._serviceCreator.CreateClient())
            {
                try
                {
                    ws.Service.SalvaCodiceInterventoPerStatistica(ws.Token, idDomanda, idIntervento);
                }
                catch (Exception ex)
                {
                    ws.Service.Abort();
                    this._logger.Error("Errore durante la chiamata al WS AreaRiservata.SalvaCodiceInterventoPerStatistica: " + ex.ToString());
                }
            }
        }
    }
}
