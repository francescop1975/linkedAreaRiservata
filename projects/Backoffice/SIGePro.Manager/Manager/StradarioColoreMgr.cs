using System;
using System.Collections;
using Init.SIGePro.Data;
using PersonalLib2.Data;
using System.Collections.Generic;

namespace Init.SIGePro.Manager 
 { 	///<summary>
	/// Descrizione di riepilogo per StradarioColoreMgr.\n	/// </summary>
	public class StradarioColoreMgr: BaseManager
{

		public StradarioColoreMgr( DataBase dataBase ) : base( dataBase ) {}

		#region Metodi per l'accesso di base al DB

		public StradarioColore GetById(String pCODICECOLORE, String pIDCOMUNE)
		{
			StradarioColore retVal = new StradarioColore();
			retVal.CODICECOLORE = pCODICECOLORE;
			retVal.IDCOMUNE = pIDCOMUNE;
		
			var mydc = db.GetClassList(retVal,true,false);
			if (mydc.Count!=0)
				return (mydc[0]) as StradarioColore;
			
			return null;
		}

 

		/// <summary>
		/// Ritorna una lista di classi corrispondenti ai criteri di ricerca passati
		/// </summary>
		/// <param name="p_class">Criteri di ricerca</param>
		/// <returns>ArrayList di oggetti corrispondenti ai criteri di ricerca passati</returns>
		public List<StradarioColore> GetList(StradarioColore p_class)
		{
			return this.GetList(p_class,null);
		}
	
		public List<StradarioColore> GetList(StradarioColore p_class, StradarioColore p_cmpClass )
		{
			return db.GetClassList(p_class,p_cmpClass,false,false).ToList<StradarioColore>();
		}
#endregion
	}
}