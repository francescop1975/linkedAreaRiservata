﻿
//using System.Xml.Serialization;
//namespace Init.SIGePro.Manager.Logic.RicercheAnagrafiche.Parix
//{
//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    [System.Xml.Serialization.XmlRootAttribute("RISPOSTA", Namespace = "", IsNullable = false)]
//    public partial class ListaImprese
//    {

//        private RISPOSTAHEADER hEADERField;

//        private RISPOSTADATI dATIField;

//        /// <remarks/>
//        public RISPOSTAHEADER HEADER
//        {
//            get
//            {
//                return this.hEADERField;
//            }
//            set
//            {
//                this.hEADERField = value;
//            }
//        }

//        /// <remarks/>
//        public RISPOSTADATI DATI
//        {
//            get
//            {
//                return this.dATIField;
//            }
//            set
//            {
//                this.dATIField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class RISPOSTAHEADER
//    {

//        private string eSECUTOREField = string.Empty;

//        private string sERVIZIOField = string.Empty;

//        private string eSITOField = string.Empty;

//        /// <remarks/>
//        public string ESECUTORE
//        {
//            get
//            {
//                return this.eSECUTOREField;
//            }
//            set
//            {
//                this.eSECUTOREField = value;
//            }
//        }

//        /// <remarks/>
//        public string SERVIZIO
//        {
//            get
//            {
//                return this.sERVIZIOField;
//            }
//            set
//            {
//                this.sERVIZIOField = value;
//            }
//        }

//        /// <remarks/>
//        public string ESITO
//        {
//            get
//            {
//                return this.eSITOField;
//            }
//            set
//            {
//                this.eSITOField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class RISPOSTADATI
//    {

//        private object itemField;

//        /// <remarks/>
//        [System.Xml.Serialization.XmlElementAttribute("ERRORE", typeof(RISPOSTADATIERRORE))]
//        [System.Xml.Serialization.XmlElementAttribute("LISTA_IMPRESE", typeof(RISPOSTADATILISTA_IMPRESE))]
//        public object Item
//        {
//            get
//            {
//                return this.itemField;
//            }
//            set
//            {
//                this.itemField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class RISPOSTADATIERRORE
//    {

//        private string tIPOField = string.Empty;

//        private string mSG_ERRField = string.Empty;

//        /// <remarks/>
//        public string TIPO
//        {
//            get
//            {
//                return this.tIPOField;
//            }
//            set
//            {
//                this.tIPOField = value;
//            }
//        }

//        /// <remarks/>
//        public string MSG_ERR
//        {
//            get
//            {
//                return this.mSG_ERRField;
//            }
//            set
//            {
//                this.mSG_ERRField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class RISPOSTADATILISTA_IMPRESE
//    {

//        private RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESA[] eSTREMI_IMPRESAField;

//        /// <remarks/>
//        [System.Xml.Serialization.XmlElementAttribute("ESTREMI_IMPRESA")]
//        public RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESA[] ESTREMI_IMPRESA
//        {
//            get
//            {
//                return this.eSTREMI_IMPRESAField;
//            }
//            set
//            {
//                this.eSTREMI_IMPRESAField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESA
//    {

//        private string dENOMINAZIONEField = string.Empty;

//        private string cODICE_FISCALEField = string.Empty;

//        private string pARTITA_IVAField = string.Empty;

//        private RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESAFORMA_GIURIDICA fORMA_GIURIDICAField;

//        private RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_RI dATI_ISCRIZIONE_RIField;

//        private RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_REA[] dATI_ISCRIZIONE_REAField;

//        /// <remarks/>
//        public string DENOMINAZIONE
//        {
//            get
//            {
//                return this.dENOMINAZIONEField;
//            }
//            set
//            {
//                this.dENOMINAZIONEField = value;
//            }
//        }

//        /// <remarks/>
//        public string CODICE_FISCALE
//        {
//            get
//            {
//                return this.cODICE_FISCALEField;
//            }
//            set
//            {
//                this.cODICE_FISCALEField = value;
//            }
//        }

//        /// <remarks/>
//        public string PARTITA_IVA
//        {
//            get
//            {
//                return this.pARTITA_IVAField;
//            }
//            set
//            {
//                this.pARTITA_IVAField = value;
//            }
//        }

//        /// <remarks/>
//        public RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESAFORMA_GIURIDICA FORMA_GIURIDICA
//        {
//            get
//            {
//                return this.fORMA_GIURIDICAField;
//            }
//            set
//            {
//                this.fORMA_GIURIDICAField = value;
//            }
//        }

//        /// <remarks/>
//        public RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_RI DATI_ISCRIZIONE_RI
//        {
//            get
//            {
//                return this.dATI_ISCRIZIONE_RIField;
//            }
//            set
//            {
//                this.dATI_ISCRIZIONE_RIField = value;
//            }
//        }

//        /// <remarks/>
//        [System.Xml.Serialization.XmlElementAttribute("DATI_ISCRIZIONE_REA")]
//        public RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_REA[] DATI_ISCRIZIONE_REA
//        {
//            get
//            {
//                return this.dATI_ISCRIZIONE_REAField;
//            }
//            set
//            {
//                this.dATI_ISCRIZIONE_REAField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESAFORMA_GIURIDICA
//    {

//        private string c_FORMA_GIURIDICAField = string.Empty;

//        private string dESCRIZIONEField = string.Empty;

//        /// <remarks/>
//        public string C_FORMA_GIURIDICA
//        {
//            get
//            {
//                return this.c_FORMA_GIURIDICAField;
//            }
//            set
//            {
//                this.c_FORMA_GIURIDICAField = value;
//            }
//        }

//        /// <remarks/>
//        public string DESCRIZIONE
//        {
//            get
//            {
//                return this.dESCRIZIONEField;
//            }
//            set
//            {
//                this.dESCRIZIONEField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_RI
//    {

//        private string nUMERO_RIField = string.Empty;

//        private string dATAField = string.Empty;

//        private RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_RISEZIONE[] sEZIONIField;

//        /// <remarks/>
//        public string NUMERO_RI
//        {
//            get
//            {
//                return this.nUMERO_RIField;
//            }
//            set
//            {
//                this.nUMERO_RIField = value;
//            }
//        }

//        /// <remarks/>
//        public string DATA
//        {
//            get
//            {
//                return this.dATAField;
//            }
//            set
//            {
//                this.dATAField = value;
//            }
//        }

//        /// <remarks/>
//        [System.Xml.Serialization.XmlArrayItemAttribute("SEZIONE", IsNullable = false)]
//        public RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_RISEZIONE[] SEZIONI
//        {
//            get
//            {
//                return this.sEZIONIField;
//            }
//            set
//            {
//                this.sEZIONIField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_RISEZIONE
//    {

//        private string c_SEZIONEField = string.Empty;

//        private string dESCRIZIONEField = string.Empty;

//        private string dT_ISCRIZIONEField = string.Empty;

//        private string cOLT_DIRETTOField = string.Empty;

//        /// <remarks/>
//        public string C_SEZIONE
//        {
//            get
//            {
//                return this.c_SEZIONEField;
//            }
//            set
//            {
//                this.c_SEZIONEField = value;
//            }
//        }

//        /// <remarks/>
//        public string DESCRIZIONE
//        {
//            get
//            {
//                return this.dESCRIZIONEField;
//            }
//            set
//            {
//                this.dESCRIZIONEField = value;
//            }
//        }

//        /// <remarks/>
//        public string DT_ISCRIZIONE
//        {
//            get
//            {
//                return this.dT_ISCRIZIONEField;
//            }
//            set
//            {
//                this.dT_ISCRIZIONEField = value;
//            }
//        }

//        /// <remarks/>
//        public string COLT_DIRETTO
//        {
//            get
//            {
//                return this.cOLT_DIRETTOField;
//            }
//            set
//            {
//                this.cOLT_DIRETTOField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_REA
//    {

//        private string nREAField = string.Empty;

//        private string cCIAAField = string.Empty;

//        private string fLAG_SEDEField = string.Empty;

//        private string i_TRASFERIMENTO_SEDEField = string.Empty;

//        private string f_AGGIORNAMENTOField = string.Empty;

//        private string dATAField = string.Empty;

//        private string dT_ULT_AGGIORNAMENTOField = string.Empty;

//        private string c_FONTEField = string.Empty;

//        private RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_REACESSAZIONE cESSAZIONEField;

//        /// <remarks/>
//        public string NREA
//        {
//            get
//            {
//                return this.nREAField;
//            }
//            set
//            {
//                this.nREAField = value;
//            }
//        }

//        /// <remarks/>
//        public string CCIAA
//        {
//            get
//            {
//                return this.cCIAAField;
//            }
//            set
//            {
//                this.cCIAAField = value;
//            }
//        }

//        /// <remarks/>
//        public string FLAG_SEDE
//        {
//            get
//            {
//                return this.fLAG_SEDEField;
//            }
//            set
//            {
//                this.fLAG_SEDEField = value;
//            }
//        }

//        /// <remarks/>
//        public string I_TRASFERIMENTO_SEDE
//        {
//            get
//            {
//                return this.i_TRASFERIMENTO_SEDEField;
//            }
//            set
//            {
//                this.i_TRASFERIMENTO_SEDEField = value;
//            }
//        }

//        /// <remarks/>
//        public string F_AGGIORNAMENTO
//        {
//            get
//            {
//                return this.f_AGGIORNAMENTOField;
//            }
//            set
//            {
//                this.f_AGGIORNAMENTOField = value;
//            }
//        }

//        /// <remarks/>
//        public string DATA
//        {
//            get
//            {
//                return this.dATAField;
//            }
//            set
//            {
//                this.dATAField = value;
//            }
//        }

//        /// <remarks/>
//        public string DT_ULT_AGGIORNAMENTO
//        {
//            get
//            {
//                return this.dT_ULT_AGGIORNAMENTOField;
//            }
//            set
//            {
//                this.dT_ULT_AGGIORNAMENTOField = value;
//            }
//        }

//        /// <remarks/>
//        public string C_FONTE
//        {
//            get
//            {
//                return this.c_FONTEField;
//            }
//            set
//            {
//                this.c_FONTEField = value;
//            }
//        }

//        /// <remarks/>
//        public RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_REACESSAZIONE CESSAZIONE
//        {
//            get
//            {
//                return this.cESSAZIONEField;
//            }
//            set
//            {
//                this.cESSAZIONEField = value;
//            }
//        }
//    }

//    /// <remarks/>
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//    public partial class RISPOSTADATILISTA_IMPRESEESTREMI_IMPRESADATI_ISCRIZIONE_REACESSAZIONE
//    {

//        private string dT_CANCELLAZIONEField = string.Empty;

//        private string dT_CESSAZIONEField = string.Empty;

//        private string dT_DENUNCIA_CESSField = string.Empty;

//        private string cAUSALEField = string.Empty;

//        /// <remarks/>
//        public string DT_CANCELLAZIONE
//        {
//            get
//            {
//                return this.dT_CANCELLAZIONEField;
//            }
//            set
//            {
//                this.dT_CANCELLAZIONEField = value;
//            }
//        }

//        /// <remarks/>
//        public string DT_CESSAZIONE
//        {
//            get
//            {
//                return this.dT_CESSAZIONEField;
//            }
//            set
//            {
//                this.dT_CESSAZIONEField = value;
//            }
//        }

//        /// <remarks/>
//        public string DT_DENUNCIA_CESS
//        {
//            get
//            {
//                return this.dT_DENUNCIA_CESSField;
//            }
//            set
//            {
//                this.dT_DENUNCIA_CESSField = value;
//            }
//        }

//        /// <remarks/>
//        public string CAUSALE
//        {
//            get
//            {
//                return this.cAUSALEField;
//            }
//            set
//            {
//                this.cAUSALEField = value;
//            }
//        }
//    }
//}
