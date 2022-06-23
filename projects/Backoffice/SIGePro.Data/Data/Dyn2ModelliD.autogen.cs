
using System;
using System.Data;
using System.Reflection;
using System.Text;
using Init.SIGePro.Attributes;
using Init.SIGePro.Collection;
using PersonalLib2.Sql.Attributes;
using PersonalLib2.Sql;
using System.Runtime.Serialization;

namespace Init.SIGePro.Data
{
    ///
    /// File generato automaticamente dalla tabella DYN2_MODELLID il 26/01/2010 12.28.27
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
    [DataTable("DYN2_MODELLID")]
    [Serializable]
    [DataContract]
    public partial class Dyn2ModelliD : BaseDataClass
    {
        [KeyField("IDCOMUNE", Type = DbType.String, Size = 6)]
        [DataMember]
        public string Idcomune { get; set; }

        [KeyField("ID", Type = DbType.Decimal)]
        [useSequence]
        [DataMember]
        public int? Id { get; set; }

        [isRequired]
        [DataField("FK_D2MT_ID", Type = DbType.Decimal)]
        [DataMember]
        public int? FkD2mtId { get; set; }

        [DataField("FK_D2C_ID", Type = DbType.Decimal)]
        [DataMember]
        public int? FkD2cId { get; set; }

        [DataField("FK_D2MDT_ID", Type = DbType.Decimal)]
        [DataMember]
        public int? FkD2mdtId { get; set; }

        [isRequired]
        [DataField("POSVERTICALE", Type = DbType.Decimal)]
        [DataMember]
        public int? Posverticale { get; set; }

        [isRequired]
        [DataField("POSORIZZONTALE", Type = DbType.Decimal)]
        [DataMember]
        public int? Posorizzontale { get; set; }

        [DataField("FLG_MULTIPLO", Type = DbType.Decimal)]
        [DataMember]
        public int? FlgMultiplo { get; set; }

        [DataField("FLG_SPEZZA_TABELLA", Type = DbType.Decimal)]
        [DataMember]
        public int? FlgSpezzaTabella { get; set; }
    }
}
