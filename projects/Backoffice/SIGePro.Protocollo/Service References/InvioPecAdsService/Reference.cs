//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.SIGePro.Protocollo.InvioPecAdsService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ParametriIngressoPG", Namespace="http://pec.ducd.affarigenerali.finmatica.it")]
    [System.SerializableAttribute()]
    public partial class ParametriIngressoPG : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int annoField;
        
        private string listaDestinatariField;
        
        private int numeroField;
        
        private string tipoRegistroField;
        
        private string utente_creazioneField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int anno {
            get {
                return this.annoField;
            }
            set {
                if ((this.annoField.Equals(value) != true)) {
                    this.annoField = value;
                    this.RaisePropertyChanged("anno");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string listaDestinatari {
            get {
                return this.listaDestinatariField;
            }
            set {
                if ((object.ReferenceEquals(this.listaDestinatariField, value) != true)) {
                    this.listaDestinatariField = value;
                    this.RaisePropertyChanged("listaDestinatari");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int numero {
            get {
                return this.numeroField;
            }
            set {
                if ((this.numeroField.Equals(value) != true)) {
                    this.numeroField = value;
                    this.RaisePropertyChanged("numero");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string tipoRegistro {
            get {
                return this.tipoRegistroField;
            }
            set {
                if ((object.ReferenceEquals(this.tipoRegistroField, value) != true)) {
                    this.tipoRegistroField = value;
                    this.RaisePropertyChanged("tipoRegistro");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string utente_creazione {
            get {
                return this.utente_creazioneField;
            }
            set {
                if ((object.ReferenceEquals(this.utente_creazioneField, value) != true)) {
                    this.utente_creazioneField = value;
                    this.RaisePropertyChanged("utente_creazione");
                }
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ParametriUscita", Namespace="http://pec.ducd.affarigenerali.finmatica.it")]
    [System.SerializableAttribute()]
    public partial class ParametriUscita : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int codiceField;
        
        private string descrizioneField;
        
        private string msgIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int codice {
            get {
                return this.codiceField;
            }
            set {
                if ((this.codiceField.Equals(value) != true)) {
                    this.codiceField = value;
                    this.RaisePropertyChanged("codice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string descrizione {
            get {
                return this.descrizioneField;
            }
            set {
                if ((object.ReferenceEquals(this.descrizioneField, value) != true)) {
                    this.descrizioneField = value;
                    this.RaisePropertyChanged("descrizione");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string msgId {
            get {
                return this.msgIdField;
            }
            set {
                if ((object.ReferenceEquals(this.msgIdField, value) != true)) {
                    this.msgIdField = value;
                    this.RaisePropertyChanged("msgId");
                }
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ParametriIngresso", Namespace="http://pec.ducd.affarigenerali.finmatica.it")]
    [System.SerializableAttribute()]
    public partial class ParametriIngresso : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int idDocumentoField;
        
        private string listaDestinatariField;
        
        private string utente_creazioneField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int idDocumento {
            get {
                return this.idDocumentoField;
            }
            set {
                if ((this.idDocumentoField.Equals(value) != true)) {
                    this.idDocumentoField = value;
                    this.RaisePropertyChanged("idDocumento");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string listaDestinatari {
            get {
                return this.listaDestinatariField;
            }
            set {
                if ((object.ReferenceEquals(this.listaDestinatariField, value) != true)) {
                    this.listaDestinatariField = value;
                    this.RaisePropertyChanged("listaDestinatari");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string utente_creazione {
            get {
                return this.utente_creazioneField;
            }
            set {
                if ((object.ReferenceEquals(this.utente_creazioneField, value) != true)) {
                    this.utente_creazioneField = value;
                    this.RaisePropertyChanged("utente_creazione");
                }
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://pec.ducd.affarigenerali.finmatica.it", ConfigurationName="InvioPecAdsService.PecSOAPImpl")]
    public interface PecSOAPImpl {
        
        // CODEGEN: Generating message contract since element name in from namespace http://pec.ducd.affarigenerali.finmatica.it is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGResponse invioPecPG(Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGRequest request);
        
        // CODEGEN: Generating message contract since element name in from namespace http://pec.ducd.affarigenerali.finmatica.it is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        Init.SIGePro.Protocollo.InvioPecAdsService.invioPecResponse invioPec(Init.SIGePro.Protocollo.InvioPecAdsService.invioPecRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class invioPecPGRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="invioPecPG", Namespace="http://pec.ducd.affarigenerali.finmatica.it", Order=0)]
        public Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGRequestBody Body;
        
        public invioPecPGRequest() {
        }
        
        public invioPecPGRequest(Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://pec.ducd.affarigenerali.finmatica.it")]
    public partial class invioPecPGRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Init.SIGePro.Protocollo.InvioPecAdsService.ParametriIngressoPG @in;
        
        public invioPecPGRequestBody() {
        }
        
        public invioPecPGRequestBody(Init.SIGePro.Protocollo.InvioPecAdsService.ParametriIngressoPG @in) {
            this.@in = @in;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class invioPecPGResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="invioPecPGResponse", Namespace="http://pec.ducd.affarigenerali.finmatica.it", Order=0)]
        public Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGResponseBody Body;
        
        public invioPecPGResponse() {
        }
        
        public invioPecPGResponse(Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://pec.ducd.affarigenerali.finmatica.it")]
    public partial class invioPecPGResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Init.SIGePro.Protocollo.InvioPecAdsService.ParametriUscita invioPecPGReturn;
        
        public invioPecPGResponseBody() {
        }
        
        public invioPecPGResponseBody(Init.SIGePro.Protocollo.InvioPecAdsService.ParametriUscita invioPecPGReturn) {
            this.invioPecPGReturn = invioPecPGReturn;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class invioPecRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="invioPec", Namespace="http://pec.ducd.affarigenerali.finmatica.it", Order=0)]
        public Init.SIGePro.Protocollo.InvioPecAdsService.invioPecRequestBody Body;
        
        public invioPecRequest() {
        }
        
        public invioPecRequest(Init.SIGePro.Protocollo.InvioPecAdsService.invioPecRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://pec.ducd.affarigenerali.finmatica.it")]
    public partial class invioPecRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Init.SIGePro.Protocollo.InvioPecAdsService.ParametriIngresso @in;
        
        public invioPecRequestBody() {
        }
        
        public invioPecRequestBody(Init.SIGePro.Protocollo.InvioPecAdsService.ParametriIngresso @in) {
            this.@in = @in;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class invioPecResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="invioPecResponse", Namespace="http://pec.ducd.affarigenerali.finmatica.it", Order=0)]
        public Init.SIGePro.Protocollo.InvioPecAdsService.invioPecResponseBody Body;
        
        public invioPecResponse() {
        }
        
        public invioPecResponse(Init.SIGePro.Protocollo.InvioPecAdsService.invioPecResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://pec.ducd.affarigenerali.finmatica.it")]
    public partial class invioPecResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Init.SIGePro.Protocollo.InvioPecAdsService.ParametriUscita invioPecReturn;
        
        public invioPecResponseBody() {
        }
        
        public invioPecResponseBody(Init.SIGePro.Protocollo.InvioPecAdsService.ParametriUscita invioPecReturn) {
            this.invioPecReturn = invioPecReturn;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PecSOAPImplChannel : Init.SIGePro.Protocollo.InvioPecAdsService.PecSOAPImpl, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PecSOAPImplClient : System.ServiceModel.ClientBase<Init.SIGePro.Protocollo.InvioPecAdsService.PecSOAPImpl>, Init.SIGePro.Protocollo.InvioPecAdsService.PecSOAPImpl {
        
        public PecSOAPImplClient() {
        }
        
        public PecSOAPImplClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PecSOAPImplClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PecSOAPImplClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PecSOAPImplClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGResponse Init.SIGePro.Protocollo.InvioPecAdsService.PecSOAPImpl.invioPecPG(Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGRequest request) {
            return base.Channel.invioPecPG(request);
        }
        
        public Init.SIGePro.Protocollo.InvioPecAdsService.ParametriUscita invioPecPG(Init.SIGePro.Protocollo.InvioPecAdsService.ParametriIngressoPG @in) {
            Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGRequest inValue = new Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGRequest();
            inValue.Body = new Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGRequestBody();
            inValue.Body.@in = @in;
            Init.SIGePro.Protocollo.InvioPecAdsService.invioPecPGResponse retVal = ((Init.SIGePro.Protocollo.InvioPecAdsService.PecSOAPImpl)(this)).invioPecPG(inValue);
            return retVal.Body.invioPecPGReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Protocollo.InvioPecAdsService.invioPecResponse Init.SIGePro.Protocollo.InvioPecAdsService.PecSOAPImpl.invioPec(Init.SIGePro.Protocollo.InvioPecAdsService.invioPecRequest request) {
            return base.Channel.invioPec(request);
        }
        
        public Init.SIGePro.Protocollo.InvioPecAdsService.ParametriUscita invioPec(Init.SIGePro.Protocollo.InvioPecAdsService.ParametriIngresso @in) {
            Init.SIGePro.Protocollo.InvioPecAdsService.invioPecRequest inValue = new Init.SIGePro.Protocollo.InvioPecAdsService.invioPecRequest();
            inValue.Body = new Init.SIGePro.Protocollo.InvioPecAdsService.invioPecRequestBody();
            inValue.Body.@in = @in;
            Init.SIGePro.Protocollo.InvioPecAdsService.invioPecResponse retVal = ((Init.SIGePro.Protocollo.InvioPecAdsService.PecSOAPImpl)(this)).invioPec(inValue);
            return retVal.Body.invioPecReturn;
        }
    }
}
