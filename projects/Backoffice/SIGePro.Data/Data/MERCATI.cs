using System.Data;
using Init.SIGePro.Attributes;
using Init.SIGePro.Collection;
using Init.SIGePro.Data;
using PersonalLib2.Sql.Attributes;
using System;
using Init.SIGePro.DatiDinamici.Interfaces;

namespace Init.SIGePro.Data
{
	[DataTable("MERCATI")]
    [Serializable]
	public class Mercati : BaseDataClass, IClasseContestoModelloDinamico
	{
        [KeyField("IDCOMUNE", Size = 6, Type = DbType.String)]
        public string IdComune { get; set; } = null;

        [useSequence]
        [KeyField("CODICEMERCATO", Type = DbType.Decimal)]
        public int? CodiceMercato { get; set; } = null;

        [DataField("DESCRIZIONE", Size = 100, Type = DbType.String, Compare = "like", CaseSensitive = false)]
        public string Descrizione { get; set; } = null;

        [DataField("ATTIVO", Type = DbType.Decimal)]
        public string Attivo { get; set; } = null;

        [DataField("SOFTWARE", Size = 2, Type = DbType.String)]
        public string Software { get; set; } = null;

        [DataField("NOTE", Size = 1000, Type = DbType.String, Compare = "like", CaseSensitive = false)]
        public string Note { get; set; } = null;

        [DataField("TIPO_MERCATO", Size = 2, Type = DbType.String)]
        public string Tipo_mercato { get; set; } = null;

        [DataField("FLAG_REGCONTASSENZA", Type = DbType.Int32)]
        public string FlagRegContaAssenza { get; set; } = null;

        [DataField("CODICEOGGETTO", Type = DbType.Int32)]
        public int? Codiceoggetto { get; set; } = null;

        [isRequired]
        [DataField("TIPO_MANIFEST", Type = DbType.Int32)]
        public int? TipoManifest { get; set; } = null;

        [DataField("FLAG_CONTABILITA", Type = DbType.Int32)]
        public string FlagContabilita { get; set; } = null;

        [DataField("ORA_INGRESSO_INIZIO", Size = 6, Type = DbType.String, CaseSensitive = false)]
        public string OraIngressoInizio { get; set; } = null;

        [DataField("ORA_INGRESSO_FINE", Size = 6, Type = DbType.String, CaseSensitive = false)]
        public string OraIngressoFine { get; set; } = null;

        [DataField("ORA_VENDITA_INIZIO", Size = 6, Type = DbType.String, CaseSensitive = false)]
        public string OraVenditaInizio { get; set; } = null;

        [DataField("ORA_VENDITA_FINE", Size = 6, Type = DbType.String, CaseSensitive = false)]
        public string OraVenditaFine { get; set; } = null;

        [DataField("ORA_SGOMBERO_INIZIO", Size = 6, Type = DbType.String, CaseSensitive = false)]
        public string OraSgomberoInizio { get; set; } = null;

        [DataField("ORA_SGOMBERO_FINE", Size = 6, Type = DbType.String, CaseSensitive = false)]
        public string OraSgomberoFine { get; set; } = null;

        [DataField("DET_DIRIGENZIALE_NUMERO", Size = 10, Type = DbType.String, Compare = "like", CaseSensitive = false)]
        public string DetDirigenzialeNumero { get; set; } = null;

        [DataField("DET_DIRIGENZIALE_DATA", Type = DbType.DateTime)]
        public DateTime? DetDirigenzialeData { get; set; } = null;

        #region Arraylist per gli inserimenti nelle tabelle collegate
        public Mercati_DCollection PosteggiMercato { get; set; } = new Mercati_DCollection();
        public MercatiStradarioCollection Stradario { get; set; } = new MercatiStradarioCollection();
        #endregion

        public override string ToString()
        {
            return Descrizione;
        }
	}
}