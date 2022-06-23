using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.Sigepro.FrontEnd.AppLogic.GestioneComuni;
using Init.Sigepro.FrontEnd.AppLogic.GestionePresentazioneDomanda.GestioneAnagrafiche;
using Init.Sigepro.FrontEnd.Pagamenti.NODOPAGAMENTI;

namespace Init.Sigepro.FrontEnd.AppLogic.GestionePagamenti.NODOPAGAMENTI
{
    public class EstremiDomandaNodoPagamenti
    {
        public readonly int IdDomanda;
        public readonly int StepId;

        public EstremiDomandaNodoPagamenti(int idDomanda, int stepId, AnagraficaDomanda anagrafica, IComuniService comuniService, string email)
        {
            var datiComune = comuniService.GetDatiComune(anagrafica.IndirizzoResidenza.CodiceComune);

            this.IdDomanda = idDomanda;
            this.StepId = stepId;

            var nome = anagrafica.Nome;
            var cognome = anagrafica.Nominativo;
            var codiceFiscale = anagrafica.Codicefiscale;
            var indirizzo = anagrafica.IndirizzoResidenza.Via;
            var cap = anagrafica.IndirizzoResidenza.Cap;
            var provincia = anagrafica.IndirizzoResidenza.SiglaProvincia;
            var comune = datiComune.Comune;

            this.RiferimentiUtenteNodoPagamenti = new RiferimentiUtenteNodoPagamenti(nome, cognome, codiceFiscale, comune, indirizzo, provincia, cap, email);
        }

        public RiferimentiUtenteNodoPagamenti RiferimentiUtenteNodoPagamenti { get; }
    }
}
