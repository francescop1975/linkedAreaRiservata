//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://sigepro.it/scrivania-enti-terzi", ConfigurationName="WsEntiTerzi.ws_enti_terziSoap")]
    public interface ws_enti_terziSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sigepro.it/scrivania-enti-terzi/GetDatiAmministrazione", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ETDatiAmministrazione GetDatiAmministrazione(string token, int codiceAnagrafe);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sigepro.it/scrivania-enti-terzi/PuoEffettuareMovimenti", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool PuoEffettuareMovimenti(string token, int codiceAnagrafe);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sigepro.it/scrivania-enti-terzi/GetListaPratiche", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ETPraticaEnteTerzo[] GetListaPratiche(string token, Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ETFiltriPraticheEntiTerzi filtri);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sigepro.it/scrivania-enti-terzi/GetListaSoftwareConPratiche", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ETSoftware[] GetListaSoftwareConPratiche(string token, int codiceAnagrafe);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sigepro.it/scrivania-enti-terzi/MarcaPraticaComeElaborata", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void MarcaPraticaComeElaborata(string token, int codiceIstanza, int codiceAnagrafe);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sigepro.it/scrivania-enti-terzi/MarcaPraticaComeNonElaborata", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void MarcaPraticaComeNonElaborata(string token, int codiceIstanza, int codiceAnagrafe);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sigepro.it/scrivania-enti-terzi/PraticaElaborata", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool PraticaElaborata(string token, int codiceIstanza, int codiceAnagrafe);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://sigepro.it/scrivania-enti-terzi")]
    public partial class ETDatiAmministrazione : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<int> codiceField;
        
        private string descrizioneField;
        
        private string partitaIvaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<int> Codice {
            get {
                return this.codiceField;
            }
            set {
                this.codiceField = value;
                this.RaisePropertyChanged("Codice");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Descrizione {
            get {
                return this.descrizioneField;
            }
            set {
                this.descrizioneField = value;
                this.RaisePropertyChanged("Descrizione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string PartitaIva {
            get {
                return this.partitaIvaField;
            }
            set {
                this.partitaIvaField = value;
                this.RaisePropertyChanged("PartitaIva");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://sigepro.it/scrivania-enti-terzi")]
    public partial class ETSoftware : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string descrizioneField;
        
        private string codiceField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Descrizione {
            get {
                return this.descrizioneField;
            }
            set {
                this.descrizioneField = value;
                this.RaisePropertyChanged("Descrizione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Codice {
            get {
                return this.codiceField;
            }
            set {
                this.codiceField = value;
                this.RaisePropertyChanged("Codice");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://sigepro.it/scrivania-enti-terzi")]
    public partial class ETPraticaEnteTerzo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string numeroProtocolloField;
        
        private System.Nullable<System.DateTime> dataProtocolloField;
        
        private int codiceIstanzaField;
        
        private string localizzazioneField;
        
        private string richiedenteField;
        
        private string tipoInterventoField;
        
        private string oggettoField;
        
        private System.DateTime dataPresentazioneField;
        
        private string statoLavorazioneField;
        
        private string uUIDField;
        
        private string numeroIstanzaField;
        
        private string softwareCodiceField;
        
        private string softwareDescrizioneField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string NumeroProtocollo {
            get {
                return this.numeroProtocolloField;
            }
            set {
                this.numeroProtocolloField = value;
                this.RaisePropertyChanged("NumeroProtocollo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public System.Nullable<System.DateTime> DataProtocollo {
            get {
                return this.dataProtocolloField;
            }
            set {
                this.dataProtocolloField = value;
                this.RaisePropertyChanged("DataProtocollo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int CodiceIstanza {
            get {
                return this.codiceIstanzaField;
            }
            set {
                this.codiceIstanzaField = value;
                this.RaisePropertyChanged("CodiceIstanza");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Localizzazione {
            get {
                return this.localizzazioneField;
            }
            set {
                this.localizzazioneField = value;
                this.RaisePropertyChanged("Localizzazione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string Richiedente {
            get {
                return this.richiedenteField;
            }
            set {
                this.richiedenteField = value;
                this.RaisePropertyChanged("Richiedente");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string TipoIntervento {
            get {
                return this.tipoInterventoField;
            }
            set {
                this.tipoInterventoField = value;
                this.RaisePropertyChanged("TipoIntervento");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Oggetto {
            get {
                return this.oggettoField;
            }
            set {
                this.oggettoField = value;
                this.RaisePropertyChanged("Oggetto");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public System.DateTime DataPresentazione {
            get {
                return this.dataPresentazioneField;
            }
            set {
                this.dataPresentazioneField = value;
                this.RaisePropertyChanged("DataPresentazione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string StatoLavorazione {
            get {
                return this.statoLavorazioneField;
            }
            set {
                this.statoLavorazioneField = value;
                this.RaisePropertyChanged("StatoLavorazione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string UUID {
            get {
                return this.uUIDField;
            }
            set {
                this.uUIDField = value;
                this.RaisePropertyChanged("UUID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string NumeroIstanza {
            get {
                return this.numeroIstanzaField;
            }
            set {
                this.numeroIstanzaField = value;
                this.RaisePropertyChanged("NumeroIstanza");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string SoftwareCodice {
            get {
                return this.softwareCodiceField;
            }
            set {
                this.softwareCodiceField = value;
                this.RaisePropertyChanged("SoftwareCodice");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string SoftwareDescrizione {
            get {
                return this.softwareDescrizioneField;
            }
            set {
                this.softwareDescrizioneField = value;
                this.RaisePropertyChanged("SoftwareDescrizione");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://sigepro.it/scrivania-enti-terzi")]
    public partial class ETFiltriPraticheEntiTerzi : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int codiceAnagrafeField;
        
        private System.DateTime dallaDataField;
        
        private System.DateTime allaDataField;
        
        private string numeroProtocolloField;
        
        private string numeroIstanzaField;
        
        private string moduloField;
        
        private System.Nullable<bool> elaborataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int CodiceAnagrafe {
            get {
                return this.codiceAnagrafeField;
            }
            set {
                this.codiceAnagrafeField = value;
                this.RaisePropertyChanged("CodiceAnagrafe");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public System.DateTime DallaData {
            get {
                return this.dallaDataField;
            }
            set {
                this.dallaDataField = value;
                this.RaisePropertyChanged("DallaData");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public System.DateTime AllaData {
            get {
                return this.allaDataField;
            }
            set {
                this.allaDataField = value;
                this.RaisePropertyChanged("AllaData");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string NumeroProtocollo {
            get {
                return this.numeroProtocolloField;
            }
            set {
                this.numeroProtocolloField = value;
                this.RaisePropertyChanged("NumeroProtocollo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string NumeroIstanza {
            get {
                return this.numeroIstanzaField;
            }
            set {
                this.numeroIstanzaField = value;
                this.RaisePropertyChanged("NumeroIstanza");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string Modulo {
            get {
                return this.moduloField;
            }
            set {
                this.moduloField = value;
                this.RaisePropertyChanged("Modulo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public System.Nullable<bool> Elaborata {
            get {
                return this.elaborataField;
            }
            set {
                this.elaborataField = value;
                this.RaisePropertyChanged("Elaborata");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ws_enti_terziSoapChannel : Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ws_enti_terziSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ws_enti_terziSoapClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ws_enti_terziSoap>, Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ws_enti_terziSoap {
        
        public ws_enti_terziSoapClient() {
        }
        
        public ws_enti_terziSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ws_enti_terziSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ws_enti_terziSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ws_enti_terziSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ETDatiAmministrazione GetDatiAmministrazione(string token, int codiceAnagrafe) {
            return base.Channel.GetDatiAmministrazione(token, codiceAnagrafe);
        }
        
        public bool PuoEffettuareMovimenti(string token, int codiceAnagrafe) {
            return base.Channel.PuoEffettuareMovimenti(token, codiceAnagrafe);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ETPraticaEnteTerzo[] GetListaPratiche(string token, Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ETFiltriPraticheEntiTerzi filtri) {
            return base.Channel.GetListaPratiche(token, filtri);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsEntiTerzi.ETSoftware[] GetListaSoftwareConPratiche(string token, int codiceAnagrafe) {
            return base.Channel.GetListaSoftwareConPratiche(token, codiceAnagrafe);
        }
        
        public void MarcaPraticaComeElaborata(string token, int codiceIstanza, int codiceAnagrafe) {
            base.Channel.MarcaPraticaComeElaborata(token, codiceIstanza, codiceAnagrafe);
        }
        
        public void MarcaPraticaComeNonElaborata(string token, int codiceIstanza, int codiceAnagrafe) {
            base.Channel.MarcaPraticaComeNonElaborata(token, codiceIstanza, codiceAnagrafe);
        }
        
        public bool PraticaElaborata(string token, int codiceIstanza, int codiceAnagrafe) {
            return base.Channel.PraticaElaborata(token, codiceIstanza, codiceAnagrafe);
        }
    }
}
