﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.Auriga.UD.ExtractOne
{
    // 
    // Codice sorgente generato automaticamente da xsd, versione=4.6.1055.0.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("FileUDToExtract", Namespace = "", IsNullable = false)]
    public partial class EstremiXidentificazioneVerDocType
    {

        private EstremiXIdentificazioneUDType estremiXIdentificazioneUDField;

        private EstremiXIdentificazioneFileUDType estremixIdentificazioneFileUDField;

        /// <remarks/>
        public EstremiXIdentificazioneUDType EstremiXIdentificazioneUD
        {
            get
            {
                return this.estremiXIdentificazioneUDField;
            }
            set
            {
                this.estremiXIdentificazioneUDField = value;
            }
        }

        /// <remarks/>
        public EstremiXIdentificazioneFileUDType EstremixIdentificazioneFileUD
        {
            get
            {
                return this.estremixIdentificazioneFileUDField;
            }
            set
            {
                this.estremixIdentificazioneFileUDField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EstremiXIdentificazioneUDType
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EstremiRegNum", typeof(EstremiRegNumType))]
        [System.Xml.Serialization.XmlElementAttribute("IdUD", typeof(string), DataType = "integer")]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EstremiRegNumType
    {

        private EstremiRegNumTypeCategoriaReg categoriaRegField;

        private bool categoriaRegFieldSpecified;

        private string siglaRegField;

        private string annoRegField;

        private string numRegField;

        /// <remarks/>
        public EstremiRegNumTypeCategoriaReg CategoriaReg
        {
            get
            {
                return this.categoriaRegField;
            }
            set
            {
                this.categoriaRegField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CategoriaRegSpecified
        {
            get
            {
                return this.categoriaRegFieldSpecified;
            }
            set
            {
                this.categoriaRegFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string SiglaReg
        {
            get
            {
                return this.siglaRegField;
            }
            set
            {
                this.siglaRegField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string AnnoReg
        {
            get
            {
                return this.annoRegField;
            }
            set
            {
                this.annoRegField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string NumReg
        {
            get
            {
                return this.numRegField;
            }
            set
            {
                this.numRegField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum EstremiRegNumTypeCategoriaReg
    {

        /// <remarks/>
        PG,

        /// <remarks/>
        PP,

        /// <remarks/>
        R,

        /// <remarks/>
        E,

        /// <remarks/>
        A,

        /// <remarks/>
        I,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class OggDiTabDiSistemaType
    {

        private string itemField;

        private ItemChoiceType itemElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CodId", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Decodifica_Nome", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
    public enum ItemChoiceType
    {

        /// <remarks/>
        CodId,

        /// <remarks/>
        Decodifica_Nome,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EstremiXIdentificazioneFileUDType
    {

        private object itemField;

        private ItemChoiceType1 itemElementNameField;

        private string nroVersioneField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DesAllegato", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("FlagPrimario", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("NomeFile", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("NroAllegato", typeof(string), DataType = "positiveInteger")]
        [System.Xml.Serialization.XmlElementAttribute("TipoDocAllegato", typeof(OggDiTabDiSistemaType))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType1 ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger")]
        public string NroVersione
        {
            get
            {
                return this.nroVersioneField;
            }
            set
            {
                this.nroVersioneField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
    public enum ItemChoiceType1
    {

        /// <remarks/>
        DesAllegato,

        /// <remarks/>
        FlagPrimario,

        /// <remarks/>
        NomeFile,

        /// <remarks/>
        NroAllegato,

        /// <remarks/>
        TipoDocAllegato,
    }
}