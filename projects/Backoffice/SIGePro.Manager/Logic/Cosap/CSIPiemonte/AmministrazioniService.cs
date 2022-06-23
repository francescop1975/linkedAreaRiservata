using Init.SIGePro.Authentication;
using Init.SIGePro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.Cosap.CSIPiemonte
{
    public class AmministrazioniService
    {
        private AuthenticationInfo _authInfo;

        public AmministrazioniService(AuthenticationInfo authInfo )
        {
            if (authInfo is null)
            {
                throw new ArgumentNullException(nameof(authInfo));
            }

            this._authInfo = authInfo;
        }

        public int GetCodiceAmministrazioneDaCodiceComune(string codiceComune) 
        {
            if (string.IsNullOrEmpty(codiceComune))
            {
                throw new ArgumentNullException(nameof(codiceComune));
            }

            var elenco = new AmministrazioniMgr(this._authInfo.CreateDatabase()).GetByCodiceComune(this._authInfo.IdComune, codiceComune);

            if (elenco == null || elenco.Count == 0) 
            {
                throw new Exception($"Non è configurata nessuna amministrazione con codice comune {codiceComune}");
            }

            if (elenco.Count > 1) 
            {
                throw new Exception($"Sono configurate {elenco.Count} amministrazioni con codice comune {codiceComune}");
            }

            return Convert.ToInt32( ((Amministrazioni)elenco[0]).CODICEAMMINISTRAZIONE);
        }
    }
}
