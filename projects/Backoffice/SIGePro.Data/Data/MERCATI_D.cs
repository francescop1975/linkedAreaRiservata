using Init.SIGePro.Attributes;
using Init.SIGePro.DatiDinamici.Interfaces;
using PersonalLib2.Sql.Attributes;
using System.Collections.Generic;
using System.Data;

namespace Init.SIGePro.Data
{
    [DataTable("MERCATI_D")]
    public class Mercati_D : BaseDataClass, IClasseContestoModelloDinamico
    {
        [KeyField("IDCOMUNE", Size = 6, Type = DbType.String)]
        public string IdComune { get; set; } = null;

        [useSequence]
        [KeyField("IDPOSTEGGIO", Type = DbType.Decimal)]
        public int? IdPosteggio { get; set; } = null;

        [DataField("FKCODICEMERCATO", Type = DbType.Decimal)]
        public int? FkCodiceMercato { get; set; } = null;

        [DataField("CODICEPOSTEGGIO", Size = 50, Type = DbType.String, CaseSensitive = false)]
        public string CodicePosteggio { get; set; } = null;

        [DataField("LARGHEZZA", Type = DbType.Decimal)]
        public double? Larghezza { get; set; } = null;

        [DataField("LUNGHEZZA", Type = DbType.Decimal)]
        public double? Lunghezza { get; set; } = null;

        [DataField("SUPERFICIE", Type = DbType.Decimal)]
        public double? Superficie { get; set; } = null;

        [DataField("FKCODICETIPOSPAZIO", Type = DbType.Decimal)]
        public string FkCodiceTipoSpazio { get; set; } = null;

        [DataField("DISABILITATO", Type = DbType.Decimal)]
        public string Disabilitato { get; set; } = null;

        [DataField("FKCODICESTRADARIO", Type = DbType.Decimal)]
        public string FkCodiceStradario { get; set; } = null;

        [DataField("NOTE", Size = 1000, Type = DbType.String, CaseSensitive = false)]
        public string Note { get; set; } = null;

        #region Arraylist per gli inserimenti nelle tabelle collegate
        public List<Mercati_DAttivitaIstat> AttivitaIstat { get; set; } = new List<Mercati_DAttivitaIstat>();
        public List<Mercati_DConti> Conti { get; set; } = new List<Mercati_DConti>();
        #endregion
    }
}