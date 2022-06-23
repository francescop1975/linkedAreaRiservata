using System;
using System.Collections.Generic;
using System.Text;
using Init.SIGePro.Data;
using Init.SIGePro.Validator;

namespace Init.SIGePro.Manager
{
    public partial class CCConfigurazioneMgr
    {
        private CCConfigurazione Insert( CCConfigurazione cls )
        {
            throw new NotImplementedException("Il metodo da utilizzare per il salvataggio della configurazione � CCConfigurazioneMgr.Save"); 
        }

        private CCConfigurazione Update(CCConfigurazione cls)
        {
            throw new NotImplementedException("Il metodo da utilizzare per il salvataggio della configurazione � CCConfigurazioneMgr.Save");
        }

        private void Delete(CCConfigurazione cls)
        {
            throw new NotImplementedException("Nella configurazione � presente solamente un record e non pu� essere cancellato");
        }

        public CCConfigurazione Save(CCConfigurazione cls)
        {
            cls = DataIntegrations(cls);

            Validate(cls, AmbitoValidazione.Insert);

            if (db.Update(cls) == 0)
                db.Insert(cls);

            return cls;
        }
    }
}
