using System.Collections;
using System.Collections.Generic;
using Init.SIGePro.Data;
using PersonalLib2.Data;
using PersonalLib2.Sql.Collections;

namespace Init.SIGePro.Manager
{   ///<summary>
    /// Descrizione di riepilogo per AmministrazioneResponsabileMgr.\n	/// </summary>
    public class AmministrazioniResponsabiliMgr : BaseManager
    {


        public AmministrazioniResponsabiliMgr(DataBase dataBase) : base(dataBase) { }

        #region Metodi per l'accesso di base al DB

        public AmministrazioniResponsabili GetById(string pIDCOMUNE, string pID)
        {
            AmministrazioniResponsabili retVal = new AmministrazioniResponsabili();
            retVal.IDCOMUNE = pIDCOMUNE;
            retVal.ID = pID;

            var mydc = db.GetClassList(retVal, true, false);
            if (mydc.Count != 0)
                return (mydc[0]) as AmministrazioniResponsabili;

            return null;
        }



        /// <summary>
        /// Ritorna una lista di classi corrispondenti ai criteri di ricerca passati
        /// </summary>
        /// <param name="p_class">Criteri di ricerca</param>
        /// <returns>ArrayList di oggetti corrispondenti ai criteri di ricerca passati</returns>
        public List<AmministrazioniResponsabili> GetList(AmministrazioniResponsabili p_class)
        {
            return this.GetList(p_class, null);
        }

        public List<AmministrazioniResponsabili> GetList(AmministrazioniResponsabili p_class, AmministrazioniResponsabili p_cmpClass)
        {
            return db.GetClassList(p_class, p_cmpClass, false, false).ToList<AmministrazioniResponsabili>();
        }

        #endregion
    }
}