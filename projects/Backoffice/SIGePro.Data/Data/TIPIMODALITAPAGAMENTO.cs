using System;
using System.Data;
using Init.SIGePro.Attributes;
using PersonalLib2.Sql.Attributes;

namespace Init.SIGePro.Data
{
	[DataTable("TIPIMODALITAPAGAMENTO")]
	[Serializable]
	public class TipiModalitaPagamento : BaseDataClass
	{

        #region Key Fields
        [useSequence]
        [KeyField("MP_ID", Type = DbType.Decimal)]
        public string MP_ID { get; set; } = null;
        [KeyField("IDCOMUNE", Size = 6, Type = DbType.String)]
        public string IDCOMUNE { get; set; } = null;

        #endregion
        [DataField("MP_DESCRESTESA", Size = 100, Type = DbType.String, CaseSensitive = false)]
        public string MP_DESCRESTESA { get; set; } = null;

        [DataField("FLAG_DISABILITATO", Type = DbType.Decimal)]
        public int? FLAG_DISABILITATO { get; set; } = null;
    }
}