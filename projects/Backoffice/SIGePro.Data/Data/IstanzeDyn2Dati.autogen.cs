
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
    /// File generato automaticamente dalla tabella ISTANZEDYN2DATI il 28/01/2010 15.49.37
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
    [DataTable("ISTANZEDYN2DATI")]
    [Serializable]
    [DataContract]
    public partial class IstanzeDyn2Dati : BaseDataClass
    {

        [DataMember]
        [KeyField("IDCOMUNE", Type = DbType.String, Size = 6)]
        public string Idcomune { get; set; }

        [KeyField("CODICEISTANZA", Type = DbType.Decimal)]
        [DataMember] 
        public int? Codiceistanza { get; set; }

        [KeyField("FK_D2C_ID", Type = DbType.Decimal)]
        [DataMember] 
        public int? FkD2cId { get; set; }

        [KeyField("INDICE", Type = DbType.Decimal)]
        [DataMember] 
        public int? Indice { get; set; }

        [KeyField("INDICE_MOLTEPLICITA", Type = DbType.Decimal)]
        [DataMember] 
        public int? IndiceMolteplicita { get; set; }

        [DataField("VALORE", Type = DbType.String, CaseSensitive = false, Size = 2147483647)]
        [DataMember] 
        public string Valore { get; set; }

        [DataField("VALOREDECODIFICATO", Type = DbType.String, CaseSensitive = false, Size = 2147483647)]
        [DataMember] 
        public string Valoredecodificato { get; set; }
    }
}
