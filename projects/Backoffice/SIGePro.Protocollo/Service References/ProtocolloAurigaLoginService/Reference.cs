﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.SIGePro.Protocollo.ProtocolloAurigaLoginService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://login.webservices.repository2.auriga.eng.it", ConfigurationName="ProtocolloAurigaLoginService.WSILogin")]
    public interface WSILogin {
        
        // CODEGEN: Generazione di un contratto di messaggio perché l'operazione serviceOperation non è RPC né incapsulata da documenti.
        [System.ServiceModel.OperationContractAttribute(Action="http://login.webservices.repository2.auriga.eng.it/WSILogin/serviceOperationReque" +
            "st", ReplyAction="http://login.webservices.repository2.auriga.eng.it/WSILogin/serviceOperationRespo" +
            "nse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.serviceOperationResponse serviceOperation(Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.serviceOperationRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://login.webservices.repository2.auriga.eng.it")]
    public partial class service : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codApplicazioneField;
        
        private string istanzaApplicazioneField;
        
        private string userNameField;
        
        private string passwordField;
        
        private string xmlField;
        
        private string hashField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string codApplicazione {
            get {
                return this.codApplicazioneField;
            }
            set {
                this.codApplicazioneField = value;
                this.RaisePropertyChanged("codApplicazione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string istanzaApplicazione {
            get {
                return this.istanzaApplicazioneField;
            }
            set {
                this.istanzaApplicazioneField = value;
                this.RaisePropertyChanged("istanzaApplicazione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string userName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
                this.RaisePropertyChanged("userName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
                this.RaisePropertyChanged("password");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string xml {
            get {
                return this.xmlField;
            }
            set {
                this.xmlField = value;
                this.RaisePropertyChanged("xml");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string hash {
            get {
                return this.hashField;
            }
            set {
                this.hashField = value;
                this.RaisePropertyChanged("hash");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://login.webservices.repository2.auriga.eng.it")]
    public partial class serviceResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string serviceReturnField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string serviceReturn {
            get {
                return this.serviceReturnField;
            }
            set {
                this.serviceReturnField = value;
                this.RaisePropertyChanged("serviceReturn");
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
    public partial class serviceOperationRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://login.webservices.repository2.auriga.eng.it", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.service service;
        
        public serviceOperationRequest() {
        }
        
        public serviceOperationRequest(Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.service service) {
            this.service = service;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class serviceOperationResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://login.webservices.repository2.auriga.eng.it", Order=0)]
        public Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.serviceResponse serviceResponse;
        
        public serviceOperationResponse() {
        }
        
        public serviceOperationResponse(Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.serviceResponse serviceResponse) {
            this.serviceResponse = serviceResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WSILoginChannel : Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.WSILogin, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSILoginClient : System.ServiceModel.ClientBase<Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.WSILogin>, Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.WSILogin {
        
        public WSILoginClient() {
        }
        
        public WSILoginClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WSILoginClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSILoginClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WSILoginClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.serviceOperationResponse Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.WSILogin.serviceOperation(Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.serviceOperationRequest request) {
            return base.Channel.serviceOperation(request);
        }
        
        public Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.serviceResponse serviceOperation(Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.service service) {
            Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.serviceOperationRequest inValue = new Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.serviceOperationRequest();
            inValue.service = service;
            Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.serviceOperationResponse retVal = ((Init.SIGePro.Protocollo.ProtocolloAurigaLoginService.WSILogin)(this)).serviceOperation(inValue);
            return retVal.serviceResponse;
        }
    }
}