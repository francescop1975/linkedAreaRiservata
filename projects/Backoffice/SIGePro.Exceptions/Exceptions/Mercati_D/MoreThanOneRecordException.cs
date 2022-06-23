namespace Init.SIGePro.Exceptions.Mercati_D
{
	/// <summary>
	/// Descrizione di riepilogo per RecordNotfoundException.
	/// </summary>
	public class MoreThanOneRecordException : Init.SIGePro.Exceptions.MoreThanOneRecordException
	{
		public MoreThanOneRecordException( Init.SIGePro.Data.Mercati_D mercati_D ):base( "Sono stati trovati pi� posteggi per il mercato " + mercati_D.FkCodiceMercato + " con il codice " + mercati_D.CodicePosteggio ){}
	}
}
