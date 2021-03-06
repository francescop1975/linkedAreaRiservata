
			
using System;
using System.Collections.Generic;
using System.Text;
using Init.SIGePro.Manager;
using PersonalLib2.Data;
using Init.SIGePro.Data;
using Init.SIGePro.Validator;
using PersonalLib2.Sql;

namespace Init.SIGePro.Manager
{

	///
	/// File generato automaticamente dalla tabella FO_ARCONFIGURAZIONE per la classe FoArConfigurazione il 13/11/2009 15.50.56
	///
	///						ELENCARE DI SEGUITO EVENTUALI MODIFICHE APPORTATE MANUALMENTE ALLA CLASSE
	///				(per tenere traccia dei cambiamenti nel caso in cui la classe debba essere generata di nuovo)
	/// -
	/// -
	/// -
	/// - 
	///
	///	Prima di effettuare modifiche al template di MyGeneration in caso di dubbi contattare Nicola Gargagli ;)
	///
	public partial class FoArConfigurazioneMgr : BaseManager
	{
		public FoArConfigurazioneMgr(DataBase dataBase) : base(dataBase) { }

		public FoArConfigurazione GetById(string idcomune, string software)
		{
			FoArConfigurazione c = new FoArConfigurazione();
			
			
			c.Idcomune = idcomune;
			c.Software = software;
			
			return (FoArConfigurazione)db.GetClass(c);
		}

		public List<FoArConfigurazione> GetList(FoArConfigurazione filtro)
		{
			return db.GetClassList( filtro ).ToList< FoArConfigurazione>();
		}

		public FoArConfigurazione Insert(FoArConfigurazione cls)
		{
			cls = DataIntegrations(cls);

			Validate(cls, AmbitoValidazione.Insert);
		
			db.Insert(cls);
			
			cls = (FoArConfigurazione)ChildDataIntegrations(cls);

			ChildInsert(cls);

			return cls;
		}
		
		public override DataClass ChildDataIntegrations(DataClass cls)
		{
			return cls;
		}

		private FoArConfigurazione ChildInsert(FoArConfigurazione cls)
		{
			return cls;
		}

		private FoArConfigurazione DataIntegrations(FoArConfigurazione cls)
		{
			return cls;
		}
		

		public FoArConfigurazione Update(FoArConfigurazione cls)
		{
			Validate( cls , AmbitoValidazione.Update );
		
			db.Update(cls);

			return cls;
		}

		public void Delete(FoArConfigurazione cls)
		{
			VerificaRecordCollegati( cls );
			
			EffettuaCancellazioneACascata( cls );
		
			db.Delete(cls);
		}
		
		private void VerificaRecordCollegati(FoArConfigurazione cls )
		{
			// Inserire la logica di verifica di integrit?? referenziale
			// Sollevare un'eccezione di tipo ReferentialIntegrityException nel caso in cui il record sia usato in foreign key in altre tabelle
		}
			
		private void EffettuaCancellazioneACascata(FoArConfigurazione cls )
		{
			// Inserire la logica di cancellazione a cascata di dati collegati
		}
		
		
		private void Validate(FoArConfigurazione cls, AmbitoValidazione ambitoValidazione)
		{
			RequiredFieldValidate( cls , ambitoValidazione );
		}	
	}
}
			
			
			