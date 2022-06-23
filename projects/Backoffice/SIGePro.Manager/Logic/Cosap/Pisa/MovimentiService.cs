using Init.SIGePro.Authentication;
using Init.SIGePro.Manager.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.Pisa
{
    public class MovimentiService
    {
        private AuthenticationInfo _authInfo;

        private Authorization _auth;
        private int _codiceIstanza;

        public MovimentiService(AuthenticationInfo authInfo, int codiceIstanza)
        {

            if (authInfo is null)
            {
                throw new ArgumentNullException(nameof(authInfo));
            }

            this._authInfo = authInfo;
            var software = new IstanzeMgr(authInfo.CreateDatabase()).GetById(authInfo.IdComune, codiceIstanza).SOFTWARE;

            this._auth = new Authorization
            {
                token = authInfo.Token,
                software = software,
                alias = authInfo.Alias,

            };

            this._codiceIstanza = codiceIstanza;
        }

        public bool EsisteMovimentoEffettuato(string tipoMovimento)
        {
            return new MovimentiMgr(this._authInfo.CreateDatabase()).MovimentoEsistenteSuIstanza(this._authInfo.IdComune, this._codiceIstanza, tipoMovimento);
        }

        public int InsertMovimentoEffettuato(InsertMovimentoRequest clientRequest)
        {
            var url = $"{ ParametriConfigurazione.Get.RestIstanzeJsonUrl}/{this._codiceIstanza}/movimenti";

            var request = new MovimentoRestBean
            {
                codice_amministrazione = clientRequest.CodiceAmministrazione,
                codice_istanza = this._codiceIstanza,
                codice_responsabile = clientRequest.CodiceResponsabile,
                data = DateTime.Now,
                dataScadenza = clientRequest.DataScadenza,
                movimento = clientRequest.DescrizioneMovimento,
                pubblica = clientRequest.PubblicaMovimento,
                tipomovimento = clientRequest.TipoMovimento
            };

            return new JsonClient(this._auth, url).InserisciMovimento(request).codice_movimento;
        }
    }
}
