//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1022
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 
namespace Init.SIGePro.Protocollo.DocEr.Fascicolazione
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("Segnatura", Namespace = "", IsNullable = false)]
    public partial class SegnaturaType
    {

        private IntestazioneType intestazioneField;

        /// <remarks/>
        public IntestazioneType Intestazione
        {
            get
            {
                return this.intestazioneField;
            }
            set
            {
                this.intestazioneField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IntestazioneType
    {

        private FascicoloType fascicoloPrimarioField;

        private FascicoloType[] fascicoliSecondariField;

        /// <remarks/>
        public FascicoloType FascicoloPrimario
        {
            get
            {
                return this.fascicoloPrimarioField;
            }
            set
            {
                this.fascicoloPrimarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FascicoloSecondario", IsNullable = false)]
        public FascicoloType[] FascicoliSecondari
        {
            get
            {
                return this.fascicoliSecondariField;
            }
            set
            {
                this.fascicoliSecondariField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FascicoloType
    {

        private string codiceAmministrazioneField;

        private string codiceAOOField;

        private string classificaField;

        private int annoField;

        private string progressivoField;

        /// <remarks/>
        public string CodiceAmministrazione
        {
            get
            {
                return this.codiceAmministrazioneField;
            }
            set
            {
                this.codiceAmministrazioneField = value;
            }
        }

        /// <remarks/>
        public string CodiceAOO
        {
            get
            {
                return this.codiceAOOField;
            }
            set
            {
                this.codiceAOOField = value;
            }
        }

        /// <remarks/>
        public string Classifica
        {
            get
            {
                return this.classificaField;
            }
            set
            {
                this.classificaField = value;
            }
        }

        /// <remarks/>
        public int Anno
        {
            get
            {
                return this.annoField;
            }
            set
            {
                this.annoField = value;
            }
        }

        /// <remarks/>
        public string Progressivo
        {
            get
            {
                return this.progressivoField;
            }
            set
            {
                this.progressivoField = value;
            }
        }
    }
}