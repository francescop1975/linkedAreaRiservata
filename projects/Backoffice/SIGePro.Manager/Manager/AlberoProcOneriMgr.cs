using System;
using System.Collections;
using System.Collections.Generic;
using Init.SIGePro.Data;
using PersonalLib2.Data;

namespace Init.SIGePro.Manager
{   ///<summary>
    /// Descrizione di riepilogo per AlberoProcOneriMgr.\n	/// </summary>
    public class AlberoProcOneriMgr : BaseManager
    {


        public AlberoProcOneriMgr(DataBase dataBase) : base(dataBase) { }

        #region Metodi per l'accesso di base al DB


        public AlberoProcOneri GetById(String pIDCOMUNE, String pAO_ID)
        {
            AlberoProcOneri retVal = new AlberoProcOneri();
            retVal.IDCOMUNE = pIDCOMUNE;
            retVal.AO_ID = pAO_ID;

            var mydc = db.GetClassList(retVal, true, false);
            if (mydc.Count != 0)
                return (mydc[0]) as AlberoProcOneri;

            return null;



        }



        /// <summary>
        /// Ritorna una lista di classi corrispondenti ai criteri di ricerca passati
        /// </summary>
        /// <param name="p_class">Criteri di ricerca</param>
        /// <returns>ArrayList di oggetti corrispondenti ai criteri di ricerca passati</returns>
        public List<AlberoProcOneri> GetList(AlberoProcOneri p_class)
        {
            return this.GetList(p_class, null);
        }

        public List<AlberoProcOneri> GetList(AlberoProcOneri p_class, AlberoProcOneri p_cmpClass)
        {
            return db.GetClassList(p_class, p_cmpClass, false, false).ToList<AlberoProcOneri>();
        }



        #endregion
    }
}