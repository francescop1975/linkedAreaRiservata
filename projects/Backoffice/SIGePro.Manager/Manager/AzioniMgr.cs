using System;
using System.Collections;
using System.Collections.Generic;
using Init.SIGePro.Data;
using PersonalLib2.Data;

namespace Init.SIGePro.Manager 
 { 	///<summary>
	/// Descrizione di riepilogo per AzioniMgr.\n	/// </summary>
	public class AzioniMgr: BaseManager
{

		public AzioniMgr( DataBase dataBase ) : base( dataBase ) {}

		#region Metodi per l'accesso di base al DB


		public Azioni GetById(String pAZ_ID)
		{
			Azioni retVal = new Azioni();
						retVal.AZ_ID = pAZ_ID;
		
			var mydc = db.GetClassList(retVal,true,false);
			if (mydc.Count!=0)
				return (mydc[0]) as Azioni;
			
			return null;
		}

 

		/// <summary>
		/// Ritorna una lista di classi corrispondenti ai criteri di ricerca passati
		/// </summary>
		/// <param name="p_class">Criteri di ricerca</param>
		/// <returns>ArrayList di oggetti corrispondenti ai criteri di ricerca passati</returns>
		public List<Azioni > GetList(Azioni p_class)
		{
			return this.GetList(p_class,null);
		}
	
		public List<Azioni> GetList(Azioni p_class, Azioni p_cmpClass )
		{
			return db.GetClassList(p_class,p_cmpClass,false,false).ToList<Azioni>();
		}

		#endregion
	}
}