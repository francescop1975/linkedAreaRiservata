using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.Urbi.Classificazione
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
    public partial class xapirestTypeTitolario
    {

        private getElencoTitolario_ResultType getElencoTitolario_ResultField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public getElencoTitolario_ResultType getElencoTitolario_Result
        {
            get
            {
                return this.getElencoTitolario_ResultField;
            }
            set
            {
                this.getElencoTitolario_ResultField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getElencoTitolario_ResultType
    {

        private string rESULTField;

        private string eT_NumeroVociField;

        private TitolarioType[] sEQ_TitolarioField;

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
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string ET_NumeroVoci
        {
            get
            {
                return this.eT_NumeroVociField;
            }
            set
            {
                this.eT_NumeroVociField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Titolario", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public TitolarioType[] SEQ_Titolario
        {
            get
            {
                return this.sEQ_TitolarioField;
            }
            set
            {
                this.sEQ_TitolarioField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TitolarioType
    {

        private string codiceField;

        private string descrizioneField;

        private string codiceRicercaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Codice
        {
            get
            {
                return this.codiceField;
            }
            set
            {
                this.codiceField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Descrizione
        {
            get
            {
                return this.descrizioneField;
            }
            set
            {
                this.descrizioneField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CodiceRicerca
        {
            get
            {
                return this.codiceRicercaField;
            }
            set
            {
                this.codiceRicercaField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }
    }

}
