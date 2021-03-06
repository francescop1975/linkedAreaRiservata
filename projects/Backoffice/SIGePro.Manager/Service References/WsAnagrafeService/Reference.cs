//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.SIGePro.Manager.WsAnagrafeService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://gruppoinit.it/sigepro/definitions/anagrafe", ConfigurationName="WsAnagrafeService.Anagrafe")]
    public interface Anagrafe {
        
        // CODEGEN: Generating message contract since the operation InserimentoAnagrafe is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="inserimentoAnagrafe", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeResponse1 InserimentoAnagrafe(Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeRequest1 request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe")]
    public partial class InserimentoAnagrafeRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private AnagrafeType datiAnagraficiField;
        
        /// <remarks/>
        public string token {
            get {
                return this.tokenField;
            }
            set {
                this.tokenField = value;
                this.RaisePropertyChanged("token");
            }
        }
        
        /// <remarks/>
        public AnagrafeType datiAnagrafici {
            get {
                return this.datiAnagraficiField;
            }
            set {
                this.datiAnagraficiField = value;
                this.RaisePropertyChanged("datiAnagrafici");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe")]
    public partial class AnagrafeType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private RiferimentiAnagrafeType riferimentiAnagrafeField;
        
        private string nomeField;
        
        private string cognomeField;
        
        private string codiceFiscaleField;
        
        private string partitaIvaField;
        
        private bool tecnicoField;
        
        private string titoloField;
        
        private AnagrafeTypeSesso sessoField;
        
        private System.DateTime dataNascitaField;
        
        private bool dataNascitaFieldSpecified;
        
        private ComuneType comuneNascitaField;
        
        private LocalizzazioneType residenzaField;
        
        private LocalizzazioneType corrispondenzaField;
        
        private string telefonoField;
        
        private string faxField;
        
        private string emailField;
        
        private string pecField;
        
        private string strongAuthIdField;
        
        private string passwordField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public RiferimentiAnagrafeType riferimentiAnagrafe {
            get {
                return this.riferimentiAnagrafeField;
            }
            set {
                this.riferimentiAnagrafeField = value;
                this.RaisePropertyChanged("riferimentiAnagrafe");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string nome {
            get {
                return this.nomeField;
            }
            set {
                this.nomeField = value;
                this.RaisePropertyChanged("nome");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string cognome {
            get {
                return this.cognomeField;
            }
            set {
                this.cognomeField = value;
                this.RaisePropertyChanged("cognome");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string codiceFiscale {
            get {
                return this.codiceFiscaleField;
            }
            set {
                this.codiceFiscaleField = value;
                this.RaisePropertyChanged("codiceFiscale");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string partitaIva {
            get {
                return this.partitaIvaField;
            }
            set {
                this.partitaIvaField = value;
                this.RaisePropertyChanged("partitaIva");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public bool tecnico {
            get {
                return this.tecnicoField;
            }
            set {
                this.tecnicoField = value;
                this.RaisePropertyChanged("tecnico");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string titolo {
            get {
                return this.titoloField;
            }
            set {
                this.titoloField = value;
                this.RaisePropertyChanged("titolo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public AnagrafeTypeSesso sesso {
            get {
                return this.sessoField;
            }
            set {
                this.sessoField = value;
                this.RaisePropertyChanged("sesso");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=8)]
        public System.DateTime dataNascita {
            get {
                return this.dataNascitaField;
            }
            set {
                this.dataNascitaField = value;
                this.RaisePropertyChanged("dataNascita");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool dataNascitaSpecified {
            get {
                return this.dataNascitaFieldSpecified;
            }
            set {
                this.dataNascitaFieldSpecified = value;
                this.RaisePropertyChanged("dataNascitaSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public ComuneType comuneNascita {
            get {
                return this.comuneNascitaField;
            }
            set {
                this.comuneNascitaField = value;
                this.RaisePropertyChanged("comuneNascita");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public LocalizzazioneType residenza {
            get {
                return this.residenzaField;
            }
            set {
                this.residenzaField = value;
                this.RaisePropertyChanged("residenza");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public LocalizzazioneType corrispondenza {
            get {
                return this.corrispondenzaField;
            }
            set {
                this.corrispondenzaField = value;
                this.RaisePropertyChanged("corrispondenza");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string telefono {
            get {
                return this.telefonoField;
            }
            set {
                this.telefonoField = value;
                this.RaisePropertyChanged("telefono");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public string fax {
            get {
                return this.faxField;
            }
            set {
                this.faxField = value;
                this.RaisePropertyChanged("fax");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        public string email {
            get {
                return this.emailField;
            }
            set {
                this.emailField = value;
                this.RaisePropertyChanged("email");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=15)]
        public string pec {
            get {
                return this.pecField;
            }
            set {
                this.pecField = value;
                this.RaisePropertyChanged("pec");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=16)]
        public string strongAuthId {
            get {
                return this.strongAuthIdField;
            }
            set {
                this.strongAuthIdField = value;
                this.RaisePropertyChanged("strongAuthId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=17)]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
                this.RaisePropertyChanged("password");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe")]
    public partial class RiferimentiAnagrafeType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codiceanagrafeField;
        
        private string idcomuneField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=0)]
        public string codiceanagrafe {
            get {
                return this.codiceanagrafeField;
            }
            set {
                this.codiceanagrafeField = value;
                this.RaisePropertyChanged("codiceanagrafe");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string idcomune {
            get {
                return this.idcomuneField;
            }
            set {
                this.idcomuneField = value;
                this.RaisePropertyChanged("idcomune");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe")]
    public partial class ErroreType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string numeroErroreField;
        
        private string descrizioneField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string numeroErrore {
            get {
                return this.numeroErroreField;
            }
            set {
                this.numeroErroreField = value;
                this.RaisePropertyChanged("numeroErrore");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string descrizione {
            get {
                return this.descrizioneField;
            }
            set {
                this.descrizioneField = value;
                this.RaisePropertyChanged("descrizione");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe")]
    public partial class LocalizzazioneType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string indirizzoField;
        
        private string civicoField;
        
        private string localitaField;
        
        private string capField;
        
        private ComuneType comuneField;
        
        private string provinciaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string indirizzo {
            get {
                return this.indirizzoField;
            }
            set {
                this.indirizzoField = value;
                this.RaisePropertyChanged("indirizzo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string civico {
            get {
                return this.civicoField;
            }
            set {
                this.civicoField = value;
                this.RaisePropertyChanged("civico");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string localita {
            get {
                return this.localitaField;
            }
            set {
                this.localitaField = value;
                this.RaisePropertyChanged("localita");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string cap {
            get {
                return this.capField;
            }
            set {
                this.capField = value;
                this.RaisePropertyChanged("cap");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public ComuneType comune {
            get {
                return this.comuneField;
            }
            set {
                this.comuneField = value;
                this.RaisePropertyChanged("comune");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string provincia {
            get {
                return this.provinciaField;
            }
            set {
                this.provinciaField = value;
                this.RaisePropertyChanged("provincia");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe")]
    public partial class ComuneType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string itemField;
        
        private ItemChoiceType itemElementNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("codiceCatastale", typeof(string), Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("codiceIstat", typeof(string), Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("comune", typeof(string), Order=0)]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
                this.RaisePropertyChanged("Item");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName {
            get {
                return this.itemElementNameField;
            }
            set {
                this.itemElementNameField = value;
                this.RaisePropertyChanged("ItemElementName");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe", IncludeInSchema=false)]
    public enum ItemChoiceType {
        
        /// <remarks/>
        codiceCatastale,
        
        /// <remarks/>
        codiceIstat,
        
        /// <remarks/>
        comune,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe")]
    public enum AnagrafeTypeSesso {
        
        /// <remarks/>
        M,
        
        /// <remarks/>
        F,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe")]
    public partial class InserimentoAnagrafeResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private RiferimentiAnagrafeType riferimentiAnagrafeField;
        
        private ErroreType erroriField;
        
        /// <remarks/>
        public RiferimentiAnagrafeType riferimentiAnagrafe {
            get {
                return this.riferimentiAnagrafeField;
            }
            set {
                this.riferimentiAnagrafeField = value;
                this.RaisePropertyChanged("riferimentiAnagrafe");
            }
        }
        
        /// <remarks/>
        public ErroreType errori {
            get {
                return this.erroriField;
            }
            set {
                this.erroriField = value;
                this.RaisePropertyChanged("errori");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class InserimentoAnagrafeRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe", Order=0)]
        public Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeRequest InserimentoAnagrafeRequest;
        
        public InserimentoAnagrafeRequest1() {
        }
        
        public InserimentoAnagrafeRequest1(Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeRequest InserimentoAnagrafeRequest) {
            this.InserimentoAnagrafeRequest = InserimentoAnagrafeRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class InserimentoAnagrafeResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/anagrafe", Order=0)]
        public Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeResponse InserimentoAnagrafeResponse;
        
        public InserimentoAnagrafeResponse1() {
        }
        
        public InserimentoAnagrafeResponse1(Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeResponse InserimentoAnagrafeResponse) {
            this.InserimentoAnagrafeResponse = InserimentoAnagrafeResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface AnagrafeChannel : Init.SIGePro.Manager.WsAnagrafeService.Anagrafe, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AnagrafeClient : System.ServiceModel.ClientBase<Init.SIGePro.Manager.WsAnagrafeService.Anagrafe>, Init.SIGePro.Manager.WsAnagrafeService.Anagrafe {
        
        public AnagrafeClient() {
        }
        
        public AnagrafeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AnagrafeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AnagrafeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AnagrafeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeResponse1 Init.SIGePro.Manager.WsAnagrafeService.Anagrafe.InserimentoAnagrafe(Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeRequest1 request) {
            return base.Channel.InserimentoAnagrafe(request);
        }
        
        public Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeResponse InserimentoAnagrafe(Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeRequest InserimentoAnagrafeRequest) {
            Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeRequest1 inValue = new Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeRequest1();
            inValue.InserimentoAnagrafeRequest = InserimentoAnagrafeRequest;
            Init.SIGePro.Manager.WsAnagrafeService.InserimentoAnagrafeResponse1 retVal = ((Init.SIGePro.Manager.WsAnagrafeService.Anagrafe)(this)).InserimentoAnagrafe(inValue);
            return retVal.InserimentoAnagrafeResponse;
        }
    }
}
