
			
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
	/// File generato automaticamente dalla tabella TIPIPROCEDURE_DYN2MODELLIT per la classe TipiProcedureDyn2ModelliT il 29/11/2011 11.16.55
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
	public partial class TipiProcedureDyn2ModelliTMgr : BaseManager
	{
		public TipiProcedureDyn2ModelliTMgr(DataBase dataBase) : base(dataBase) { }

		public TipiProcedureDyn2ModelliT GetById(string idcomune, int? fk_codiceprocedura, int? fk_d2mt_id)
		{
			TipiProcedureDyn2ModelliT c = new TipiProcedureDyn2ModelliT();
			
			
			c.Idcomune = idcomune;
			c.FkCodiceprocedura = fk_codiceprocedura;
			c.FkD2mtId = fk_d2mt_id;
			
			return (TipiProcedureDyn2ModelliT)db.GetClass(c);
		}

		public List<TipiProcedureDyn2ModelliT> GetList(TipiProcedureDyn2ModelliT filtro)
		{
			return db.GetClassList( filtro ).ToList< TipiProcedureDyn2ModelliT>();
		}

		public TipiProcedureDyn2ModelliT Insert(TipiProcedureDyn2ModelliT cls)
		{
			cls = DataIntegrations(cls);

			Validate(cls, AmbitoValidazione.Insert);
		
			db.Insert(cls);
			
			cls = (TipiProcedureDyn2ModelliT)ChildDataIntegrations(cls);

			ChildInsert(cls);

			return cls;
		}
		
		public override DataClass ChildDataIntegrations(DataClass cls)
		{
			return cls;
		}

		private TipiProcedureDyn2ModelliT ChildInsert(TipiProcedureDyn2ModelliT cls)
		{
			return cls;
		}

		private TipiProcedureDyn2ModelliT DataIntegrations(TipiProcedureDyn2ModelliT cls)
		{
			return cls;
		}
		

		public TipiProcedureDyn2ModelliT Update(TipiProcedureDyn2ModelliT cls)
		{
			Validate( cls , AmbitoValidazione.Update );
		
			db.Update(cls);

			return cls;
		}

		public void Delete(TipiProcedureDyn2ModelliT cls)
		{
			VerificaRecordCollegati( cls );
			
			EffettuaCancellazioneACascata( cls );
		
			db.Delete(cls);
		}
		
		private void VerificaRecordCollegati(TipiProcedureDyn2ModelliT cls )
		{
			// Inserire la logica di verifica di integrit?? referenziale
			// Sollevare un'eccezione di tipo ReferentialIntegrityException nel caso in cui il record sia usato in foreign key in altre tabelle
		}
			
		private void EffettuaCancellazioneACascata(TipiProcedureDyn2ModelliT cls )
		{
			// Inserire la logica di cancellazione a cascata di dati collegati
		}
		
		
		private void Validate(TipiProcedureDyn2ModelliT cls, AmbitoValidazione ambitoValidazione)
		{
			RequiredFieldValidate( cls , ambitoValidazione );
		}	
	}
}
			
			
			