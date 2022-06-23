using System;
using System.Collections;
using System.Collections.Generic;
using Init.SIGePro.Data;
using PersonalLib2.Data;
using PersonalLib2.Sql.Collections;

namespace Init.SIGePro.Manager
{   ///<summary>
    /// Descrizione di riepilogo per TitoliMgr.\n	/// </summary>
    public class ComuniSecurity_ConnectionMgr : BaseManager
    {

        public ComuniSecurity_ConnectionMgr(DataBase dataBase) : base(dataBase) { }

        #region Metodi per l'accesso di base al DB


        public ComuniSecurity_Connection GetById(string pCS_CODICEISTAT, string pAMBIENTE)
        {
            ComuniSecurity_Connection retVal = new ComuniSecurity_Connection();
            retVal.CS_CONNECTION_CODICEISTAT = pCS_CODICEISTAT;
            retVal.CS_CONNECTION_AMBIENTE = pAMBIENTE;

            var mydc = db.GetClassList(retVal, true, false);
            if (mydc.Count != 0)
                return (mydc[0]) as ComuniSecurity_Connection;

            return null;
        }

        //public ComuniSecurity_Connection GetByClass(ComuniSecurity pClass)
        //{
        //    var comunisecurity_connection = db.GetClassList(pClass, true, false);
        //    if (comunisecurity_connection.Count != 0)
        //    {
        //        return new ComuniSecurity_Connection{
        //            comunisecurity_connection[0];
        //    }

        //    return null;
        //}

        /// <summary>
        /// Ritorna una lista di classi corrispondenti ai criteri di ricerca passati
        /// </summary>
        /// <param name="p_class">Criteri di ricerca</param>
        /// <returns>ArrayList di oggetti corrispondenti ai criteri di ricerca passati</returns>
        public List<ComuniSecurity_Connection> GetList(ComuniSecurity_Connection p_class)
        {
            return this.GetList(p_class, null);
        }

        public List<ComuniSecurity_Connection> GetList(ComuniSecurity_Connection p_class, ComuniSecurity_Connection p_cmpClass)
        {
            return db.GetClassList(p_class, p_cmpClass, false, false).ToList<ComuniSecurity_Connection>();
        }

        #endregion
    }
}

