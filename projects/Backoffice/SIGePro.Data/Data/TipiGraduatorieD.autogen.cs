
					using System;
					using System.Data;
					using System.Reflection;
					using System.Text;
					using Init.SIGePro.Attributes;
					using Init.SIGePro.Collection;
					using PersonalLib2.Sql.Attributes;
					using PersonalLib2.Sql;

					namespace Init.SIGePro.Data
					{
						///
						/// File generato automaticamente dalla tabella TIPIGRADUATORIED il 01/04/2009 9.28.02
						///
						///												ATTENZIONE!!!
						///	- Specificare manualmente in quali colonne vanno applicate eventuali sequenze		
						/// - Verificare l'applicazione di eventuali attributi di tipo "[isRequired]". In caso contrario applicarli manualmente
						///	- Verificare che il tipo di dati assegnato alle proprietà sia corretto
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
						[DataTable("TIPIGRADUATORIED")]
						[Serializable]
						public partial class TipiGraduatorieD : BaseDataClass
						{
							#region Membri privati

                            private int? m_id = null;

                            private int? m_fk_tgt_id = null;

                            private int? m_fk_d2c_id = null;

							private string m_valore = null;

							private string m_idcomune = null;
			
							#endregion
							
							#region properties
							
							#region Key Fields
							
							
							[KeyField("ID" , Type=DbType.Decimal)]
                            [useSequence]
							public int? Id
							{
								get{ return m_id; }
								set{ m_id = value; }
							}
							
							[KeyField("IDCOMUNE" , Type=DbType.String, Size=6)]
							public string Idcomune
							{
								get{ return m_idcomune; }
								set{ m_idcomune = value; }
							}
							
							
							#endregion
							
							#region Data fields
							
							[isRequired]
                            [DataField("FK_TGT_ID" , Type=DbType.Decimal)]
							public int? FkTgtId
							{
								get{ return m_fk_tgt_id; }
								set{ m_fk_tgt_id = value; }
							}
							
							[isRequired]
                            [DataField("FK_D2C_ID" , Type=DbType.Decimal)]
							public int? FkD2cId
							{
								get{ return m_fk_d2c_id; }
								set{ m_fk_d2c_id = value; }
							}
							
							[isRequired]
                            [DataField("VALORE" , Type=DbType.String, CaseSensitive=false, Size=250)]
							public string Valore
							{
								get{ return m_valore; }
								set{ m_valore = value; }
							}
							
							#endregion

							#endregion
						}
					}
				