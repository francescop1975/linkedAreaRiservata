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

namespace Init.SIGePro.Protocollo.Auriga.UD.UppUD
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
    public partial class Output_UD
    {

        private string idUDField;

        private EstremiRegNumType[] registrazioneDataUDField;

        private Output_UDVersioneElettronicaNonCaricata[] versioneElettronicaNonCaricataField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string IdUD
        {
            get
            {
                return this.idUDField;
            }
            set
            {
                this.idUDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RegistrazioneDataUD")]
        public EstremiRegNumType[] RegistrazioneDataUD
        {
            get
            {
                return this.registrazioneDataUDField;
            }
            set
            {
                this.registrazioneDataUDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("VersioneElettronicaNonCaricata")]
        public Output_UDVersioneElettronicaNonCaricata[] VersioneElettronicaNonCaricata
        {
            get
            {
                return this.versioneElettronicaNonCaricataField;
            }
            set
            {
                this.versioneElettronicaNonCaricataField = value;
            }
        }
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Output_UDVersioneElettronicaNonCaricata
    {

        private string nroAttachmentAssociatoField;

        private string nomeFileField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger")]
        public string NroAttachmentAssociato
        {
            get
            {
                return this.nroAttachmentAssociatoField;
            }
            set
            {
                this.nroAttachmentAssociatoField = value;
            }
        }

        /// <remarks/>
        public string NomeFile
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
    }
}