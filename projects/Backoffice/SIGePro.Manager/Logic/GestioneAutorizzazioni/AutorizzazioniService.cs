using Init.SIGePro.Authentication;
using Init.SIGePro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Logic.GestioneAutorizzazioni
{
    public class AutorizzazioniService
    {
        AuthenticationInfo _ai = null;

        public AutorizzazioniService(AuthenticationInfo ai)
        {
            this._ai = ai;
        }

        public List<Autorizzazioni> GetAutorizzazioniAttiveDellIstanza(int codiceIstanza) 
        {
            using (var db = _ai.CreateDatabase())
            {
                try
                {
                    db.Connection.Open();

                    var filtro = new Autorizzazioni
                    {
                        IDCOMUNE = _ai.IdComune,
                        FKIDISTANZA = codiceIstanza.ToString(),
                        FlagAttiva = 1,
                        UseForeign = PersonalLib2.Sql.useForeignEnum.Yes                        
                    };

                    var autMgr = new AutorizzazioniMgr(db);

                    return autMgr.GetList(filtro);
                }
                finally
                {
                    db.Connection.Close();
                }
            }
        }
    }
}
