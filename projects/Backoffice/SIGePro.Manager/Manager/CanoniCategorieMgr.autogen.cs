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
	/// File generato automaticamente dalla tabella CANONI_CATEGORIE per la classe CanoniCategorie il 11/11/2008 9.18.14
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
	public partial class CanoniCategorieMgr : BaseManager
	{
		public CanoniCategorieMgr(DataBase dataBase) : base(dataBase) { }

		public CanoniCategorie GetById(string idcomune, int id)
		{
			CanoniCategorie c = new CanoniCategorie();
			
			
			c.Idcomune = idcomune;
			c.Id = id;
			
			return (CanoniCategorie)db.GetClass(c);
		}

		public List<CanoniCategorie> GetList(CanoniCategorie filtro)
		{
			return db.GetClassList( filtro ).ToList< CanoniCategorie>();
		}

		public CanoniCategorie Insert(CanoniCategorie cls)
		{
			cls = DataIntegrations(cls);

			Validate(cls, AmbitoValidazione.Insert);
		
			db.Insert(cls);
			
			cls = (CanoniCategorie)ChildDataIntegrations(cls);

			ChildInsert(cls);

			return cls;
		}
		
		public override DataClass ChildDataIntegrations(DataClass cls)
		{
			return cls;
		}

		private CanoniCategorie ChildInsert(CanoniCategorie cls)
		{
			return cls;
		}

		private CanoniCategorie DataIntegrations(CanoniCategorie cls)
		{
			return cls;
		}
		
		public CanoniCategorie Update(CanoniCategorie cls)
		{
			Validate( cls , AmbitoValidazione.Update );
		
			db.Update(cls);

			return cls;
		}

		public void Delete(CanoniCategorie cls)
		{
			VerificaRecordCollegati( cls );
			
			EffettuaCancellazioneACascata( cls );
		
			db.Delete(cls);
		}
				
		private void EffettuaCancellazioneACascata(CanoniCategorie cls )
		{
			// Inserire la logica di cancellazione a cascata di dati collegati
		}
		
		private void Validate(CanoniCategorie cls, AmbitoValidazione ambitoValidazione)
		{
			RequiredFieldValidate( cls , ambitoValidazione );
		}	
	}
}
			
			
			