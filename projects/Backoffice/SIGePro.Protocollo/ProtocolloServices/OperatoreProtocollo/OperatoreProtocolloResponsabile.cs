using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.ProtocolloInterfaces;
using PersonalLib2.Data;

namespace Init.SIGePro.Protocollo.ProtocolloServices.OperatoreProtocollo
{
    public class OperatoreProtocolloResponsabile : BaseOperatoreProtocollo, IOperatoreProtocollo
    {
        public OperatoreProtocolloResponsabile(int codiceOperatore, string idComune, DataBase db) : base(db, codiceOperatore, idComune)
        {

        }

        #region IOperatoreProtocollo Members

        public string CodiceOperatore
        {
            get { return Responsabile.RESPONSABILE; }
        }

        public bool IsOperatoreDefault
        {
            get { return false; }
        }

        #endregion
    }
}
