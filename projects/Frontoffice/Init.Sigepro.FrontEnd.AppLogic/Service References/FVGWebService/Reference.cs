﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.FVGWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://forms.insiel.it/FormService/servizi/FormService", ConfigurationName="FVGWebService.FormService")]
    public interface FormService {
        
        // CODEGEN: Generating message contract since the wrapper namespace (http://forms.insiel.it/FormService/schemixsd/FormService) of message getManagedDataFromIdIstanza does not match the default value (http://forms.insiel.it/FormService/servizi/FormService)
        [System.ServiceModel.OperationContractAttribute(Action="http://forms.diaa.insiel.it/FormService/servizi/FormService/getManagedDataFromIdI" +
            "stanza", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getManagedDataFromIdIstanzaResponse getManagedDataFromIdIstanza(Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getManagedDataFromIdIstanza request);
        
        // CODEGEN: Generating message contract since the wrapper namespace (http://forms.insiel.it/FormService/schemixsd/FormService) of message getXMLModulo does not match the default value (http://forms.insiel.it/FormService/servizi/FormService)
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getXMLModuloResponse getXMLModulo(Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getXMLModulo request);
        
        // CODEGEN: Generating message contract since the wrapper namespace (http://forms.insiel.it/FormService/schemixsd/FormService) of message salvaBinaryContent does not match the default value (http://forms.insiel.it/FormService/servizi/FormService)
        [System.ServiceModel.OperationContractAttribute(Action="http://forms.diaa.insiel.it/FormService/servizi/FormService/salvaBinaryContent", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.FVGWebService.salvaBinaryContentResponse salvaBinaryContent(Init.Sigepro.FrontEnd.AppLogic.FVGWebService.salvaBinaryContent request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://forms.insiel.it/FormService/common/ws")]
    public partial class Avvertimento : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codiceField;
        
        private string descrizioneField;
        
        private InfoAvanzataSuProblema infoAvanzataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Codice {
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
        public InfoAvanzataSuProblema infoAvanzata {
            get {
                return this.infoAvanzataField;
            }
            set {
                this.infoAvanzataField = value;
                this.RaisePropertyChanged("infoAvanzata");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://forms.insiel.it/FormService/common/ws")]
    public partial class InfoAvanzataSuProblema : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string[] risorseField;
        
        private string[] suggerimentiField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Risorse", Order=0)]
        public string[] Risorse {
            get {
                return this.risorseField;
            }
            set {
                this.risorseField = value;
                this.RaisePropertyChanged("Risorse");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Suggerimenti", Order=1)]
        public string[] Suggerimenti {
            get {
                return this.suggerimentiField;
            }
            set {
                this.suggerimentiField = value;
                this.RaisePropertyChanged("Suggerimenti");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://forms.insiel.it/FormService/common/ws")]
    public partial class Errore : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codiceField;
        
        private string descrizioneField;
        
        private InfoAvanzataSuProblema infoAvanzataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Codice {
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
        public InfoAvanzataSuProblema infoAvanzata {
            get {
                return this.infoAvanzataField;
            }
            set {
                this.infoAvanzataField = value;
                this.RaisePropertyChanged("infoAvanzata");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://forms.insiel.it/FormService/common/ws")]
    public partial class otherType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Xml.XmlElement[] anyField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute(Order=0)]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
                this.RaisePropertyChanged("Any");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://forms.insiel.it/FormService/common/ws")]
    public partial class BinaryContent : object, System.ComponentModel.INotifyPropertyChanged {
        
        private BinaryContentContent contentField;
        
        private string contentTypeField;
        
        private string fileExtensionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public BinaryContentContent content {
            get {
                return this.contentField;
            }
            set {
                this.contentField = value;
                this.RaisePropertyChanged("content");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string contentType {
            get {
                return this.contentTypeField;
            }
            set {
                this.contentTypeField = value;
                this.RaisePropertyChanged("contentType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string fileExtension {
            get {
                return this.fileExtensionField;
            }
            set {
                this.fileExtensionField = value;
                this.RaisePropertyChanged("fileExtension");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://forms.insiel.it/FormService/common/ws")]
    public partial class BinaryContentContent : object, System.ComponentModel.INotifyPropertyChanged {
        
        private byte[] binaryDataField;
        
        private string uRIField;
        
        private otherType otherField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=0)]
        public byte[] binaryData {
            get {
                return this.binaryDataField;
            }
            set {
                this.binaryDataField = value;
                this.RaisePropertyChanged("binaryData");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string URI {
            get {
                return this.uRIField;
            }
            set {
                this.uRIField = value;
                this.RaisePropertyChanged("URI");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public otherType other {
            get {
                return this.otherField;
            }
            set {
                this.otherField = value;
                this.RaisePropertyChanged("other");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName="Richiesta_getManagedDataFromIdIstanza", WrapperNamespace="http://forms.insiel.it/FormService/schemixsd/FormService", IsWrapped=true)]
    public partial class getManagedDataFromIdIstanza {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=0)]
        public long idIstanza;
        
        public getManagedDataFromIdIstanza() {
        }
        
        public getManagedDataFromIdIstanza(long idIstanza) {
            this.idIstanza = idIstanza;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Risposta_getManagedDataFromIdIstanza", WrapperNamespace="http://forms.insiel.it/FormService/schemixsd/FormService", IsWrapped=true)]
    public partial class getManagedDataFromIdIstanzaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=0)]
        public bool esito;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute("Avvertimento")]
        public Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Avvertimento[] Avvertimento;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=2)]
        public Init.Sigepro.FrontEnd.AppLogic.FVGWebService.BinaryContent managedData;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute("Errore")]
        public Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Errore[] Errore;
        
        public getManagedDataFromIdIstanzaResponse() {
        }
        
        public getManagedDataFromIdIstanzaResponse(bool esito, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Avvertimento[] Avvertimento, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.BinaryContent managedData, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Errore[] Errore) {
            this.esito = esito;
            this.Avvertimento = Avvertimento;
            this.managedData = managedData;
            this.Errore = Errore;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Richiesta_getXMLModulo", WrapperNamespace="http://forms.insiel.it/FormService/schemixsd/FormService", IsWrapped=true)]
    public partial class getXMLModulo {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=0)]
        public long idIstanza;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=1)]
        public string codiceModulo;
        
        public getXMLModulo() {
        }
        
        public getXMLModulo(long idIstanza, string codiceModulo) {
            this.idIstanza = idIstanza;
            this.codiceModulo = codiceModulo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Risposta_getXMLModulo", WrapperNamespace="http://forms.insiel.it/FormService/schemixsd/FormService", IsWrapped=true)]
    public partial class getXMLModuloResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=0)]
        public bool esito;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute("Avvertimento")]
        public Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Avvertimento[] Avvertimento;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=2)]
        public Init.Sigepro.FrontEnd.AppLogic.FVGWebService.BinaryContent XMLModulo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute("Errore")]
        public Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Errore[] Errore;
        
        public getXMLModuloResponse() {
        }
        
        public getXMLModuloResponse(bool esito, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Avvertimento[] Avvertimento, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.BinaryContent XMLModulo, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Errore[] Errore) {
            this.esito = esito;
            this.Avvertimento = Avvertimento;
            this.XMLModulo = XMLModulo;
            this.Errore = Errore;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Richiesta_salvaBinaryContent", WrapperNamespace="http://forms.insiel.it/FormService/schemixsd/FormService", IsWrapped=true)]
    public partial class salvaBinaryContent {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=0)]
        public long idIstanza;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=1)]
        public string codiceModulo;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=2)]
        public Init.Sigepro.FrontEnd.AppLogic.FVGWebService.BinaryContent xml;
        
        public salvaBinaryContent() {
        }
        
        public salvaBinaryContent(long idIstanza, string codiceModulo, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.BinaryContent xml) {
            this.idIstanza = idIstanza;
            this.codiceModulo = codiceModulo;
            this.xml = xml;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Risposta_salvaBinaryContent", WrapperNamespace="http://forms.insiel.it/FormService/schemixsd/FormService", IsWrapped=true)]
    public partial class salvaBinaryContentResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=0)]
        public bool esito;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute("Avvertimento")]
        public Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Avvertimento[] Avvertimento;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://forms.insiel.it/FormService/schemixsd/FormService", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute("Errore")]
        public Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Errore[] Errore;
        
        public salvaBinaryContentResponse() {
        }
        
        public salvaBinaryContentResponse(bool esito, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Avvertimento[] Avvertimento, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Errore[] Errore) {
            this.esito = esito;
            this.Avvertimento = Avvertimento;
            this.Errore = Errore;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface FormServiceChannel : Init.Sigepro.FrontEnd.AppLogic.FVGWebService.FormService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FormServiceClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.FVGWebService.FormService>, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.FormService {
        
        public FormServiceClient() {
        }
        
        public FormServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FormServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FormServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FormServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getManagedDataFromIdIstanzaResponse Init.Sigepro.FrontEnd.AppLogic.FVGWebService.FormService.getManagedDataFromIdIstanza(Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getManagedDataFromIdIstanza request) {
            return base.Channel.getManagedDataFromIdIstanza(request);
        }
        
        public bool getManagedDataFromIdIstanza(long idIstanza, out Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Avvertimento[] Avvertimento, out Init.Sigepro.FrontEnd.AppLogic.FVGWebService.BinaryContent managedData, out Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Errore[] Errore) {
            Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getManagedDataFromIdIstanza inValue = new Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getManagedDataFromIdIstanza();
            inValue.idIstanza = idIstanza;
            Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getManagedDataFromIdIstanzaResponse retVal = ((Init.Sigepro.FrontEnd.AppLogic.FVGWebService.FormService)(this)).getManagedDataFromIdIstanza(inValue);
            Avvertimento = retVal.Avvertimento;
            managedData = retVal.managedData;
            Errore = retVal.Errore;
            return retVal.esito;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getXMLModuloResponse Init.Sigepro.FrontEnd.AppLogic.FVGWebService.FormService.getXMLModulo(Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getXMLModulo request) {
            return base.Channel.getXMLModulo(request);
        }
        
        public bool getXMLModulo(long idIstanza, string codiceModulo, out Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Avvertimento[] Avvertimento, out Init.Sigepro.FrontEnd.AppLogic.FVGWebService.BinaryContent XMLModulo, out Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Errore[] Errore) {
            Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getXMLModulo inValue = new Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getXMLModulo();
            inValue.idIstanza = idIstanza;
            inValue.codiceModulo = codiceModulo;
            Init.Sigepro.FrontEnd.AppLogic.FVGWebService.getXMLModuloResponse retVal = ((Init.Sigepro.FrontEnd.AppLogic.FVGWebService.FormService)(this)).getXMLModulo(inValue);
            Avvertimento = retVal.Avvertimento;
            XMLModulo = retVal.XMLModulo;
            Errore = retVal.Errore;
            return retVal.esito;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.Sigepro.FrontEnd.AppLogic.FVGWebService.salvaBinaryContentResponse Init.Sigepro.FrontEnd.AppLogic.FVGWebService.FormService.salvaBinaryContent(Init.Sigepro.FrontEnd.AppLogic.FVGWebService.salvaBinaryContent request) {
            return base.Channel.salvaBinaryContent(request);
        }
        
        public bool salvaBinaryContent(long idIstanza, string codiceModulo, Init.Sigepro.FrontEnd.AppLogic.FVGWebService.BinaryContent xml, out Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Avvertimento[] Avvertimento, out Init.Sigepro.FrontEnd.AppLogic.FVGWebService.Errore[] Errore) {
            Init.Sigepro.FrontEnd.AppLogic.FVGWebService.salvaBinaryContent inValue = new Init.Sigepro.FrontEnd.AppLogic.FVGWebService.salvaBinaryContent();
            inValue.idIstanza = idIstanza;
            inValue.codiceModulo = codiceModulo;
            inValue.xml = xml;
            Init.Sigepro.FrontEnd.AppLogic.FVGWebService.salvaBinaryContentResponse retVal = ((Init.Sigepro.FrontEnd.AppLogic.FVGWebService.FormService)(this)).salvaBinaryContent(inValue);
            Avvertimento = retVal.Avvertimento;
            Errore = retVal.Errore;
            return retVal.esito;
        }
    }
}
