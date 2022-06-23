
using System;
using System.Data;
using System.Reflection;
using System.Text;
using Init.SIGePro.Attributes;
using Init.SIGePro.Collection;
using PersonalLib2.Sql.Attributes;
using PersonalLib2.Sql;
using System.Security;
using Init.SIGePro.DatiDinamici.Utils;
using System.Runtime.Serialization;

namespace Init.SIGePro.Data
{
    ///
    /// File generato automaticamente dalla tabella DYN2_CAMPI il 05/08/2008 16.49.58
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
    [DataTable("DYN2_CAMPI")]
    [Serializable]
    [DataContract]
    public partial class Dyn2Campi : BaseDataClass
    { 
        [KeyField("IDCOMUNE", Type = DbType.String, Size = 6)]
        [DataMember]
        public string Idcomune { get; set; }

        [KeyField("ID", Type = DbType.Decimal)]
        [useSequence]
        [DataMember]
        public int? Id { get; set; }

        [isRequired]
        [DataField("SOFTWARE", Type = DbType.String, CaseSensitive = false, Size = 4000)]
        [DataMember]
        public string Software { get; set; }

        [isRequired]
        [DataField("NOMECAMPO", Type = DbType.String, CaseSensitive = false, Size = 4000)]
        [DataMember]
        public string Nomecampo { get; set; }

        [DataField("ETICHETTA", Type = DbType.String, CaseSensitive = false, Size = 2000)]
        [DataMember]
        public string Etichetta { get; set; }

        [DataField("DESCRIZIONE", Type = DbType.String, CaseSensitive = false, Size = 4000)]
        [DataMember]
        public string Descrizione { get; set; }

        [isRequired]
        [DataField("TIPODATO", Type = DbType.String, CaseSensitive = false, Size = 20)]
        [DataMember]
        public string Tipodato { get; set; }

        [DataField("OBBLIGATORIO", Type = DbType.Decimal)]
        [DataMember]
        public int? Obbligatorio { get; set; }

        [DataField("FK_D2BC_ID", Type = DbType.String, CaseSensitive = false, Size = 2)]
        [DataMember]
        public string FkD2bcId { get; set; }
    }
}
