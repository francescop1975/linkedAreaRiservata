
			
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
	/// File generato automaticamente dalla tabella TIPIBANDOOUTPUT per la classe TipiBandoOutput il 01/04/2009 9.45.24
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
	public partial class TipiBandoOutputMgr : BaseManager
	{
		public TipiBandoOutputMgr(DataBase dataBase) : base(dataBase) { }

		public TipiBandoOutput GetById(int id, string idcomune)
		{
			TipiBandoOutput c = new TipiBandoOutput();
			
			
			c.Id = id;
			c.Idcomune = idcomune;
			
			return (TipiBandoOutput)db.GetClass(c);
		}

		public List<TipiBandoOutput> GetList(TipiBandoOutput filtro)
		{
			return db.GetClassList( filtro ).ToList< TipiBandoOutput>();
		}

		public TipiBandoOutput Insert(TipiBandoOutput cls)
		{
			cls = DataIntegrations(cls);

			Validate(cls, AmbitoValidazione.Insert);
		
			db.Insert(cls);
			
			cls = (TipiBandoOutput)ChildDataIntegrations(cls);

			ChildInsert(cls);

			return cls;
		}
		
		public override DataClass ChildDataIntegrations(DataClass cls)
		{
			return cls;
		}

		private TipiBandoOutput ChildInsert(TipiBandoOutput cls)
		{
			return cls;
		}

		private TipiBandoOutput DataIntegrations(TipiBandoOutput cls)
		{
			return cls;
		}
		

		public TipiBandoOutput Update(TipiBandoOutput cls)
		{
			Validate( cls , AmbitoValidazione.Update );
		
			db.Update(cls);

			return cls;
		}

		public void Delete(TipiBandoOutput cls)
		{
			VerificaRecordCollegati( cls );
			
			EffettuaCancellazioneACascata( cls );
		
			db.Delete(cls);
		}
		
		private void VerificaRecordCollegati(TipiBandoOutput cls )
		{
			// Inserire la logica di verifica di integrit?? referenziale
			// Sollevare un'eccezione di tipo ReferentialIntegrityException nel caso in cui il record sia usato in foreign key in altre tabelle
		}
			
		private void EffettuaCancellazioneACascata(TipiBandoOutput cls )
		{
			// Inserire la logica di cancellazione a cascata di dati collegati
		}
		
		
		private void Validate(TipiBandoOutput cls, AmbitoValidazione ambitoValidazione)
		{
			RequiredFieldValidate( cls , ambitoValidazione );
		}	
	}
}
			
			
			