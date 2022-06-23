using Init.Sigepro.FrontEnd.AppLogic.Common;
using Init.SIGePro.Manager.DTO.Commissioni.Votazioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneCommissioni.Votazioni
{
    public class VotazioniCommissioneService : IVotazioniCommissioniService
    {
        private readonly IAuthenticationDataResolver _authDataResolver;
        private readonly IVotazioniCommissioneDao _dao;

        public VotazioniCommissioneService(IAuthenticationDataResolver authDataResolver, IVotazioniCommissioneDao dao)
        {
            _authDataResolver = authDataResolver ?? throw new ArgumentNullException(nameof(authDataResolver));
            _dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public void EsprimiVotoPerUtenteLoggato(int idCommissione, string uuidIstanza, VotoPraticaCommissioneDto voto)
        {
            int codiceAnagrafe = this._authDataResolver.DatiAutenticazione.DatiUtente.Codiceanagrafe.Value;

            this._dao.EsprimiVotoPerUtente(idCommissione, codiceAnagrafe, uuidIstanza, voto);
        }

        public IEnumerable<CommissioniVotiBaseDto> GetListaPareri()
        {
            return this._dao.GetListaPareri();
        }

        public VotazionePraticaCommissioneDto GetVotoUtenteLoggato(int idCommissione, string uuidIstanza)
        {
            int codiceAnagrafe = this._authDataResolver.DatiAutenticazione.DatiUtente.Codiceanagrafe.Value;

            return this._dao.GetVotoUtente(idCommissione, codiceAnagrafe, uuidIstanza);
        }

        public bool UtenteLoggatoPuoEsprimereVoto(int idCommissione, string uuidIstanza)
        {
            int codiceAnagrafe = this._authDataResolver.DatiAutenticazione.DatiUtente.Codiceanagrafe.Value;

            return this._dao.UtentePuoEsprimereVoto(idCommissione, codiceAnagrafe, uuidIstanza);
        }
    }
}
