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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Output_FileUDToExtract
    {

        private object nomeFileField;

        private object nroVersioneField;

        private object nroUltimaVersioneField;

        private string nroAllegatoField;

        private Output_FileUDToExtractTipoDocAllegato tipoDocAllegatoField;

        private string desAllegatoField;

        private byte[] contentField;

        /// <remarks/>
        [XmlIgnore]
        public byte[] Content
        {
            get
            {
                return this.contentField;
            }
            set
            {
                this.contentField = value;
            }
        }

        /// <remarks/>
        public object NomeFile
        {
            get
            {
                return this.nomeFileField;
            }
            set
            {
                this.nomeFileField = value;
            }
        }

        /// <remarks/>
        public object NroVersione
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

        /// <remarks/>
        public object NroUltimaVersione
        {
            get
            {
                return this.nroUltimaVersioneField;
            }
            set
            {
                this.nroUltimaVersioneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger")]
        public string NroAllegato
        {
            get
            {
                return this.nroAllegatoField;
            }
            set
            {
                this.nroAllegatoField = value;
            }
        }

        /// <remarks/>
        public Output_FileUDToExtractTipoDocAllegato TipoDocAllegato
        {
            get
            {
                return this.tipoDocAllegatoField;
            }
            set
            {
                this.tipoDocAllegatoField = value;
            }
        }

        /// <remarks/>
        public string DesAllegato
        {
            get
            {
                return this.desAllegatoField;
            }
            set
            {
                this.desAllegatoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Output_FileUDToExtractTipoDocAllegato
    {

        private string codiceIdentificativoField;

        private string[] textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CodiceIdentificativo
        {
            get
            {
                return this.codiceIdentificativoField;
            }
            set
            {
                this.codiceIdentificativoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }
}