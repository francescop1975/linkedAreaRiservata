using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.Urbi.Protocollazione
{
    //------------------------------------------------------------------------------
    // <auto-generated>
    //     This code was generated by a tool.
    //     Runtime Version:4.0.30319.42000
    //
    //     Changes to this file may cause incorrect behavior and will be lost if
    //     the code is regenerated.
    // </auto-generated>
    //------------------------------------------------------------------------------

    // 
    // This source code was auto-generated by xsd, Version=4.0.30319.33440.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("xapirest", Namespace = "", IsNullable = false)]
    public partial class xapirestTypeInsProtocollo
    {

        private insProtocollo_ResultType insProtocollo_ResultField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public insProtocollo_ResultType insProtocollo_Result
        {
            get
            {
                return this.insProtocollo_ResultField;
            }
            set
            {
                this.insProtocollo_ResultField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insProtocollo_ResultType
    {
        private string rESULTField;

        private string idProtoField;

        private string annoField;

        private string numeroField;

        private string sezioneField;

        private string dataProtocolloField;

        private string oraProtocolloField;

        private string idAOOField;

        private string denominazioneAOOField;

        private string MessageField;

        private string ErroreCodeField;


        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RESULT
        {
            get
            {
                return this.rESULTField;
            }
            set
            {
                this.rESULTField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IdProto
        {
            get
            {
                return this.idProtoField;
            }
            set
            {
                this.idProtoField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Anno
        {
            get
            {
                return this.annoField;
            }
            set
            {
                this.annoField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Numero
        {
            get
            {
                return this.numeroField;
            }
            set
            {
                this.numeroField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Sezione
        {
            get
            {
                return this.sezioneField;
            }
            set
            {
                this.sezioneField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DataProtocollo
        {
            get
            {
                return this.dataProtocolloField;
            }
            set
            {
                this.dataProtocolloField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OraProtocollo
        {
            get
            {
                return this.oraProtocolloField;
            }
            set
            {
                this.oraProtocolloField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string IdAOO
        {
            get
            {
                return this.idAOOField;
            }
            set
            {
                this.idAOOField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DenominazioneAOO
        {
            get
            {
                return this.denominazioneAOOField;
            }
            set
            {
                this.denominazioneAOOField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ERRORCODE
        {
            get
            {
                return this.ErroreCodeField;
            }
            set
            {
                this.ErroreCodeField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }
    }
}