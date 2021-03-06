//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.SIGePro.Manager.WsOggettiService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://gruppoinit.it/sigepro/definitions/oggetti", ConfigurationName="WsOggettiService.Oggetti")]
    public interface Oggetti {
        
        // CODEGEN: Generating message contract since the operation OggettiUpdate is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="oggettiUpdate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.WsOggettiService.OggettiUpdateResponse1 OggettiUpdate(Init.SIGePro.Manager.WsOggettiService.OggettiUpdateRequest1 request);
        
        // CODEGEN: Generating message contract since the operation OggettiFind is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="oggettiFind", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.WsOggettiService.OggettiFindResponse1 OggettiFind(Init.SIGePro.Manager.WsOggettiService.OggettiFindRequest1 request);
        
        // CODEGEN: Generating message contract since the operation OggettiFindNome is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="oggettiFindNome", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeResponse1 OggettiFindNome(Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeRequest1 request);
        
        // CODEGEN: Generating message contract since the operation OggettiDelete is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="oggettiDelete", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.WsOggettiService.OggettiDeleteResponse1 OggettiDelete(Init.SIGePro.Manager.WsOggettiService.OggettiDeleteRequest1 request);
        
        // CODEGEN: Generating message contract since the operation OggettiInsert is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="oggettiInsert", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.WsOggettiService.OggettiInsertResponse1 OggettiInsert(Init.SIGePro.Manager.WsOggettiService.OggettiInsertRequest1 request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti")]
    public partial class OggettiUpdateRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private string idField;
        
        private string fileNameField;
        
        private string mimeTypeField;
        
        private byte[] binaryDataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=1)]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string fileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
                this.RaisePropertyChanged("fileName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string mimeType {
            get {
                return this.mimeTypeField;
            }
            set {
                this.mimeTypeField = value;
                this.RaisePropertyChanged("mimeType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=4)]
        public byte[] binaryData {
            get {
                return this.binaryDataField;
            }
            set {
                this.binaryDataField = value;
                this.RaisePropertyChanged("binaryData");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti")]
    public partial class OggettiUpdateResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool resultField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool result {
            get {
                return this.resultField;
            }
            set {
                this.resultField = value;
                this.RaisePropertyChanged("result");
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
    public partial class OggettiUpdateRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti", Order=0)]
        public Init.SIGePro.Manager.WsOggettiService.OggettiUpdateRequest OggettiUpdateRequest;
        
        public OggettiUpdateRequest1() {
        }
        
        public OggettiUpdateRequest1(Init.SIGePro.Manager.WsOggettiService.OggettiUpdateRequest OggettiUpdateRequest) {
            this.OggettiUpdateRequest = OggettiUpdateRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class OggettiUpdateResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti", Order=0)]
        public Init.SIGePro.Manager.WsOggettiService.OggettiUpdateResponse OggettiUpdateResponse;
        
        public OggettiUpdateResponse1() {
        }
        
        public OggettiUpdateResponse1(Init.SIGePro.Manager.WsOggettiService.OggettiUpdateResponse OggettiUpdateResponse) {
            this.OggettiUpdateResponse = OggettiUpdateResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti")]
    public partial class OggettiFindRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private string idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=1)]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti")]
    public partial class OggettiFindResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string fileNameField;
        
        private string mimeTypeField;
        
        private byte[] binaryDataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string fileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
                this.RaisePropertyChanged("fileName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string mimeType {
            get {
                return this.mimeTypeField;
            }
            set {
                this.mimeTypeField = value;
                this.RaisePropertyChanged("mimeType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=2)]
        public byte[] binaryData {
            get {
                return this.binaryDataField;
            }
            set {
                this.binaryDataField = value;
                this.RaisePropertyChanged("binaryData");
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
    public partial class OggettiFindRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti", Order=0)]
        public Init.SIGePro.Manager.WsOggettiService.OggettiFindRequest OggettiFindRequest;
        
        public OggettiFindRequest1() {
        }
        
        public OggettiFindRequest1(Init.SIGePro.Manager.WsOggettiService.OggettiFindRequest OggettiFindRequest) {
            this.OggettiFindRequest = OggettiFindRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class OggettiFindResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti", Order=0)]
        public Init.SIGePro.Manager.WsOggettiService.OggettiFindResponse OggettiFindResponse;
        
        public OggettiFindResponse1() {
        }
        
        public OggettiFindResponse1(Init.SIGePro.Manager.WsOggettiService.OggettiFindResponse OggettiFindResponse) {
            this.OggettiFindResponse = OggettiFindResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti")]
    public partial class OggettiFindNomeRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private string idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=1)]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti")]
    public partial class OggettiFindNomeResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string fileNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string fileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
                this.RaisePropertyChanged("fileName");
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
    public partial class OggettiFindNomeRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti", Order=0)]
        public Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeRequest OggettiFindNomeRequest;
        
        public OggettiFindNomeRequest1() {
        }
        
        public OggettiFindNomeRequest1(Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeRequest OggettiFindNomeRequest) {
            this.OggettiFindNomeRequest = OggettiFindNomeRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class OggettiFindNomeResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti", Order=0)]
        public Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeResponse OggettiFindNomeResponse;
        
        public OggettiFindNomeResponse1() {
        }
        
        public OggettiFindNomeResponse1(Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeResponse OggettiFindNomeResponse) {
            this.OggettiFindNomeResponse = OggettiFindNomeResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti")]
    public partial class OggettiDeleteRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private string idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=1)]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti")]
    public partial class OggettiDeleteResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool resultField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool result {
            get {
                return this.resultField;
            }
            set {
                this.resultField = value;
                this.RaisePropertyChanged("result");
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
    public partial class OggettiDeleteRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti", Order=0)]
        public Init.SIGePro.Manager.WsOggettiService.OggettiDeleteRequest OggettiDeleteRequest;
        
        public OggettiDeleteRequest1() {
        }
        
        public OggettiDeleteRequest1(Init.SIGePro.Manager.WsOggettiService.OggettiDeleteRequest OggettiDeleteRequest) {
            this.OggettiDeleteRequest = OggettiDeleteRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class OggettiDeleteResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti", Order=0)]
        public Init.SIGePro.Manager.WsOggettiService.OggettiDeleteResponse OggettiDeleteResponse;
        
        public OggettiDeleteResponse1() {
        }
        
        public OggettiDeleteResponse1(Init.SIGePro.Manager.WsOggettiService.OggettiDeleteResponse OggettiDeleteResponse) {
            this.OggettiDeleteResponse = OggettiDeleteResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti")]
    public partial class OggettiInsertRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private string fileNameField;
        
        private string mimeTypeField;
        
        private byte[] binaryDataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string fileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
                this.RaisePropertyChanged("fileName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string mimeType {
            get {
                return this.mimeTypeField;
            }
            set {
                this.mimeTypeField = value;
                this.RaisePropertyChanged("mimeType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=3)]
        public byte[] binaryData {
            get {
                return this.binaryDataField;
            }
            set {
                this.binaryDataField = value;
                this.RaisePropertyChanged("binaryData");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti")]
    public partial class OggettiInsertResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=0)]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
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
    public partial class OggettiInsertRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti", Order=0)]
        public Init.SIGePro.Manager.WsOggettiService.OggettiInsertRequest OggettiInsertRequest;
        
        public OggettiInsertRequest1() {
        }
        
        public OggettiInsertRequest1(Init.SIGePro.Manager.WsOggettiService.OggettiInsertRequest OggettiInsertRequest) {
            this.OggettiInsertRequest = OggettiInsertRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class OggettiInsertResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/oggetti", Order=0)]
        public Init.SIGePro.Manager.WsOggettiService.OggettiInsertResponse OggettiInsertResponse;
        
        public OggettiInsertResponse1() {
        }
        
        public OggettiInsertResponse1(Init.SIGePro.Manager.WsOggettiService.OggettiInsertResponse OggettiInsertResponse) {
            this.OggettiInsertResponse = OggettiInsertResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface OggettiChannel : Init.SIGePro.Manager.WsOggettiService.Oggetti, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OggettiClient : System.ServiceModel.ClientBase<Init.SIGePro.Manager.WsOggettiService.Oggetti>, Init.SIGePro.Manager.WsOggettiService.Oggetti {
        
        public OggettiClient() {
        }
        
        public OggettiClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OggettiClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OggettiClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OggettiClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.WsOggettiService.OggettiUpdateResponse1 Init.SIGePro.Manager.WsOggettiService.Oggetti.OggettiUpdate(Init.SIGePro.Manager.WsOggettiService.OggettiUpdateRequest1 request) {
            return base.Channel.OggettiUpdate(request);
        }
        
        public Init.SIGePro.Manager.WsOggettiService.OggettiUpdateResponse OggettiUpdate(Init.SIGePro.Manager.WsOggettiService.OggettiUpdateRequest OggettiUpdateRequest) {
            Init.SIGePro.Manager.WsOggettiService.OggettiUpdateRequest1 inValue = new Init.SIGePro.Manager.WsOggettiService.OggettiUpdateRequest1();
            inValue.OggettiUpdateRequest = OggettiUpdateRequest;
            Init.SIGePro.Manager.WsOggettiService.OggettiUpdateResponse1 retVal = ((Init.SIGePro.Manager.WsOggettiService.Oggetti)(this)).OggettiUpdate(inValue);
            return retVal.OggettiUpdateResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.WsOggettiService.OggettiFindResponse1 Init.SIGePro.Manager.WsOggettiService.Oggetti.OggettiFind(Init.SIGePro.Manager.WsOggettiService.OggettiFindRequest1 request) {
            return base.Channel.OggettiFind(request);
        }
        
        public Init.SIGePro.Manager.WsOggettiService.OggettiFindResponse OggettiFind(Init.SIGePro.Manager.WsOggettiService.OggettiFindRequest OggettiFindRequest) {
            Init.SIGePro.Manager.WsOggettiService.OggettiFindRequest1 inValue = new Init.SIGePro.Manager.WsOggettiService.OggettiFindRequest1();
            inValue.OggettiFindRequest = OggettiFindRequest;
            Init.SIGePro.Manager.WsOggettiService.OggettiFindResponse1 retVal = ((Init.SIGePro.Manager.WsOggettiService.Oggetti)(this)).OggettiFind(inValue);
            return retVal.OggettiFindResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeResponse1 Init.SIGePro.Manager.WsOggettiService.Oggetti.OggettiFindNome(Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeRequest1 request) {
            return base.Channel.OggettiFindNome(request);
        }
        
        public Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeResponse OggettiFindNome(Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeRequest OggettiFindNomeRequest) {
            Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeRequest1 inValue = new Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeRequest1();
            inValue.OggettiFindNomeRequest = OggettiFindNomeRequest;
            Init.SIGePro.Manager.WsOggettiService.OggettiFindNomeResponse1 retVal = ((Init.SIGePro.Manager.WsOggettiService.Oggetti)(this)).OggettiFindNome(inValue);
            return retVal.OggettiFindNomeResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.WsOggettiService.OggettiDeleteResponse1 Init.SIGePro.Manager.WsOggettiService.Oggetti.OggettiDelete(Init.SIGePro.Manager.WsOggettiService.OggettiDeleteRequest1 request) {
            return base.Channel.OggettiDelete(request);
        }
        
        public Init.SIGePro.Manager.WsOggettiService.OggettiDeleteResponse OggettiDelete(Init.SIGePro.Manager.WsOggettiService.OggettiDeleteRequest OggettiDeleteRequest) {
            Init.SIGePro.Manager.WsOggettiService.OggettiDeleteRequest1 inValue = new Init.SIGePro.Manager.WsOggettiService.OggettiDeleteRequest1();
            inValue.OggettiDeleteRequest = OggettiDeleteRequest;
            Init.SIGePro.Manager.WsOggettiService.OggettiDeleteResponse1 retVal = ((Init.SIGePro.Manager.WsOggettiService.Oggetti)(this)).OggettiDelete(inValue);
            return retVal.OggettiDeleteResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.WsOggettiService.OggettiInsertResponse1 Init.SIGePro.Manager.WsOggettiService.Oggetti.OggettiInsert(Init.SIGePro.Manager.WsOggettiService.OggettiInsertRequest1 request) {
            return base.Channel.OggettiInsert(request);
        }
        
        public Init.SIGePro.Manager.WsOggettiService.OggettiInsertResponse OggettiInsert(Init.SIGePro.Manager.WsOggettiService.OggettiInsertRequest OggettiInsertRequest) {
            Init.SIGePro.Manager.WsOggettiService.OggettiInsertRequest1 inValue = new Init.SIGePro.Manager.WsOggettiService.OggettiInsertRequest1();
            inValue.OggettiInsertRequest = OggettiInsertRequest;
            Init.SIGePro.Manager.WsOggettiService.OggettiInsertResponse1 retVal = ((Init.SIGePro.Manager.WsOggettiService.Oggetti)(this)).OggettiInsert(inValue);
            return retVal.OggettiInsertResponse;
        }
    }
}
