//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.BookmarksWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BookmarksWebService.BookmarksServiceSoap")]
    public interface BookmarksServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBookmark", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.BookmarksWebService.BookmarkInterventoDto GetBookmark(string token, string nomeLink);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class BookmarkInterventoDto : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int idField;
        
        private string idComuneField;
        
        private string urlField;
        
        private bool anonimoField;
        
        private int idInterventoField;
        
        private NodoDestinazioneDto nodoDestinatarioField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("Id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string IdComune {
            get {
                return this.idComuneField;
            }
            set {
                this.idComuneField = value;
                this.RaisePropertyChanged("IdComune");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Url {
            get {
                return this.urlField;
            }
            set {
                this.urlField = value;
                this.RaisePropertyChanged("Url");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public bool Anonimo {
            get {
                return this.anonimoField;
            }
            set {
                this.anonimoField = value;
                this.RaisePropertyChanged("Anonimo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public int IdIntervento {
            get {
                return this.idInterventoField;
            }
            set {
                this.idInterventoField = value;
                this.RaisePropertyChanged("IdIntervento");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public NodoDestinazioneDto NodoDestinatario {
            get {
                return this.nodoDestinatarioField;
            }
            set {
                this.nodoDestinatarioField = value;
                this.RaisePropertyChanged("NodoDestinatario");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class NodoDestinazioneDto : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int idField;
        
        private string idComuneField;
        
        private string descrizioneField;
        
        private string idNodoField;
        
        private string idEnteField;
        
        private string idSportelloField;
        
        private string pecField;
        
        private NodoDestinazioneParameteriDto[] parametriField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("Id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string IdComune {
            get {
                return this.idComuneField;
            }
            set {
                this.idComuneField = value;
                this.RaisePropertyChanged("IdComune");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string IdNodo {
            get {
                return this.idNodoField;
            }
            set {
                this.idNodoField = value;
                this.RaisePropertyChanged("IdNodo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string IdEnte {
            get {
                return this.idEnteField;
            }
            set {
                this.idEnteField = value;
                this.RaisePropertyChanged("IdEnte");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string IdSportello {
            get {
                return this.idSportelloField;
            }
            set {
                this.idSportelloField = value;
                this.RaisePropertyChanged("IdSportello");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Pec {
            get {
                return this.pecField;
            }
            set {
                this.pecField = value;
                this.RaisePropertyChanged("Pec");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Parametri", Order=7)]
        public NodoDestinazioneParameteriDto[] Parametri {
            get {
                return this.parametriField;
            }
            set {
                this.parametriField = value;
                this.RaisePropertyChanged("Parametri");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class NodoDestinazioneParameteriDto : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string nomeField;
        
        private string valoreField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Nome {
            get {
                return this.nomeField;
            }
            set {
                this.nomeField = value;
                this.RaisePropertyChanged("Nome");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Valore {
            get {
                return this.valoreField;
            }
            set {
                this.valoreField = value;
                this.RaisePropertyChanged("Valore");
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
    public interface BookmarksServiceSoapChannel : Init.Sigepro.FrontEnd.AppLogic.BookmarksWebService.BookmarksServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BookmarksServiceSoapClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.BookmarksWebService.BookmarksServiceSoap>, Init.Sigepro.FrontEnd.AppLogic.BookmarksWebService.BookmarksServiceSoap {
        
        public BookmarksServiceSoapClient() {
        }
        
        public BookmarksServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BookmarksServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BookmarksServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BookmarksServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.BookmarksWebService.BookmarkInterventoDto GetBookmark(string token, string nomeLink) {
            return base.Channel.GetBookmark(token, nomeLink);
        }
    }
}
