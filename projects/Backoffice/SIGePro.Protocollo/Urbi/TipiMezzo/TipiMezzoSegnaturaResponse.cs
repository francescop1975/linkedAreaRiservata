using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Urbi.TipiMezzo
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

    using System.Xml.Serialization;

    // 
    // This source code was auto-generated by xsd, Version=4.0.30319.33440.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("xapirest", Namespace = "", IsNullable = false)]
    public partial class xapirestTypeTipiMezzo
    {

        private getElencoTipiMezzo_ResultType getElencoTipiMezzo_ResultField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public getElencoTipiMezzo_ResultType getElencoTipiMezzo_Result
        {
            get
            {
                return this.getElencoTipiMezzo_ResultField;
            }
            set
            {
                this.getElencoTipiMezzo_ResultField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getElencoTipiMezzo_ResultType
    {

        private string rESULTField;

        private string numTipiMezzoField;

        private MezzoType[] sEQ_MezzoField;

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
        public string NumTipiMezzo
        {
            get
            {
                return this.numTipiMezzoField;
            }
            set
            {
                this.numTipiMezzoField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Mezzo", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public MezzoType[] SEQ_Mezzo
        {
            get
            {
                return this.sEQ_MezzoField;
            }
            set
            {
                this.sEQ_MezzoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MezzoType
    {

        private string tipoMezzoField;

        private string codiceField;

        private string validoPECField;

        private string validoECARTField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TipoMezzo
        {
            get
            {
                return this.tipoMezzoField;
            }
            set
            {
                this.tipoMezzoField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

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
        public string ValidoPEC
        {
            get
            {
                return this.validoPECField;
            }
            set
            {
                this.validoPECField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ValidoECART
        {
            get
            {
                return this.validoECARTField;
            }
            set
            {
                this.validoECARTField = Utility.FormattaValoriDaDeserializzare(value);
            }
        }

        
    }


}
