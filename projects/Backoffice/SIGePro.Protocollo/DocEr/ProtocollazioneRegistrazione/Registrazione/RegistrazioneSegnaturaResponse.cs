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

namespace Init.SIGePro.Protocollo.DocEr.ProtocollazioneRegistrazione.Registrazione
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class esito
    {

        private string codiceField;

        private string descrizioneField;

        private esitoDati_registro[] dati_registroField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string codice
        {
            get
            {
                return this.codiceField;
            }
            set
            {
                this.codiceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string descrizione
        {
            get
            {
                return this.descrizioneField;
            }
            set
            {
                this.descrizioneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("dati_registro", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public esitoDati_registro[] dati_registro
        {
            get
            {
                return this.dati_registroField;
            }
            set
            {
                this.dati_registroField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class esitoDati_registro
    {

        private string numeroRegistrazioneField;

        private string dataRegistrazioneField;

        private string oggettoRegistrazioneField;

        private string iDRegistroField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NumeroRegistrazione
        {
            get
            {
                return this.numeroRegistrazioneField;
            }
            set
            {
                this.numeroRegistrazioneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DataRegistrazione
        {
            get
            {
                return this.dataRegistrazioneField;
            }
            set
            {
                this.dataRegistrazioneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OggettoRegistrazione
        {
            get
            {
                return this.oggettoRegistrazioneField;
            }
            set
            {
                this.oggettoRegistrazioneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IDRegistro
        {
            get
            {
                return this.iDRegistroField;
            }
            set
            {
                this.iDRegistroField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class NewDataSet
    {

        private esito[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("esito")]
        public esito[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}