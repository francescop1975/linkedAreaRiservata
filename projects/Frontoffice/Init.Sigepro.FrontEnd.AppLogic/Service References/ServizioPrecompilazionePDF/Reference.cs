//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://gruppoinit.it/messages/pdfutils", ConfigurationName="ServizioPrecompilazionePDF.PdfUtils")]
    public interface PdfUtils {
        
        // CODEGEN: Generating message contract since the operation RecuperaDatiDaPDF is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="recuperaDatiDaPDF", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFResponse RecuperaDatiDaPDF(Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFRequest request);
        
        // CODEGEN: Generating message contract since the operation PrecompilaPDF is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="precompilaPDF", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFResponse PrecompilaPDF(Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/schemas/messages/pdfutils")]
    public partial class RecuperaDatiDaPDFRequestType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private PDFFileType pdfFileField;
        
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
        public PDFFileType pdfFile {
            get {
                return this.pdfFileField;
            }
            set {
                this.pdfFileField = value;
                this.RaisePropertyChanged("pdfFile");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/schemas/messages/pdfutils")]
    public partial class PDFFileType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idField;
        
        private string fileNameField;
        
        private byte[] binaryDataField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/schemas/messages/pdfutils")]
    public partial class PrecompilaPDFResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private object[] itemsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("errori", typeof(string), Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("pdfList", typeof(PDFFileType), Order=0)]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/schemas/messages/pdfutils")]
    public partial class XmlFileType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private byte[] binaryDataField;
        
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/schemas/messages/pdfutils")]
    public partial class PrecompilaPDFRequestType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private XmlFileType xmlFileInField;
        
        private PDFFileType[] pdfListField;
        
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
        public XmlFileType xmlFileIn {
            get {
                return this.xmlFileInField;
            }
            set {
                this.xmlFileInField = value;
                this.RaisePropertyChanged("xmlFileIn");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("pdfList", Order=2)]
        public PDFFileType[] pdfList {
            get {
                return this.pdfListField;
            }
            set {
                this.pdfListField = value;
                this.RaisePropertyChanged("pdfList");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/schemas/messages/pdfutils")]
    public partial class DatiPDFType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codiceField;
        
        private string[] valoreField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string codice {
            get {
                return this.codiceField;
            }
            set {
                this.codiceField = value;
                this.RaisePropertyChanged("codice");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("valore", Order=1)]
        public string[] valore {
            get {
                return this.valoreField;
            }
            set {
                this.valoreField = value;
                this.RaisePropertyChanged("valore");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/schemas/messages/pdfutils")]
    public partial class RecuperaDatiDaPDFResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private object[] itemsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("dati", typeof(DatiPDFType), Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("errori", typeof(string), Order=0)]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
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
    public partial class RecuperaDatiDaPDFRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RecuperaDatiDaPDFRequest", Namespace="http://gruppoinit.it/schemas/messages/pdfutils", Order=0)]
        public Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFRequestType RecuperaDatiDaPDFRequest1;
        
        public RecuperaDatiDaPDFRequest() {
        }
        
        public RecuperaDatiDaPDFRequest(Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFRequestType RecuperaDatiDaPDFRequest1) {
            this.RecuperaDatiDaPDFRequest1 = RecuperaDatiDaPDFRequest1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RecuperaDatiDaPDFResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RecuperaDatiDaPDFResponse", Namespace="http://gruppoinit.it/schemas/messages/pdfutils", Order=0)]
        public Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFResponseType RecuperaDatiDaPDFResponse1;
        
        public RecuperaDatiDaPDFResponse() {
        }
        
        public RecuperaDatiDaPDFResponse(Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFResponseType RecuperaDatiDaPDFResponse1) {
            this.RecuperaDatiDaPDFResponse1 = RecuperaDatiDaPDFResponse1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PrecompilaPDFRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="PrecompilaPDFRequest", Namespace="http://gruppoinit.it/schemas/messages/pdfutils", Order=0)]
        public Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFRequestType PrecompilaPDFRequest1;
        
        public PrecompilaPDFRequest() {
        }
        
        public PrecompilaPDFRequest(Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFRequestType PrecompilaPDFRequest1) {
            this.PrecompilaPDFRequest1 = PrecompilaPDFRequest1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PrecompilaPDFResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="PrecompilaPDFResponse", Namespace="http://gruppoinit.it/schemas/messages/pdfutils", Order=0)]
        public Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFResponseType PrecompilaPDFResponse1;
        
        public PrecompilaPDFResponse() {
        }
        
        public PrecompilaPDFResponse(Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFResponseType PrecompilaPDFResponse1) {
            this.PrecompilaPDFResponse1 = PrecompilaPDFResponse1;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PdfUtilsChannel : Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PdfUtils, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PdfUtilsClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PdfUtils>, Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PdfUtils {
        
        public PdfUtilsClient() {
        }
        
        public PdfUtilsClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PdfUtilsClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PdfUtilsClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PdfUtilsClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFResponse Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PdfUtils.RecuperaDatiDaPDF(Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFRequest request) {
            return base.Channel.RecuperaDatiDaPDF(request);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFResponseType RecuperaDatiDaPDF(Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFRequestType RecuperaDatiDaPDFRequest1) {
            Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFRequest inValue = new Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFRequest();
            inValue.RecuperaDatiDaPDFRequest1 = RecuperaDatiDaPDFRequest1;
            Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.RecuperaDatiDaPDFResponse retVal = ((Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PdfUtils)(this)).RecuperaDatiDaPDF(inValue);
            return retVal.RecuperaDatiDaPDFResponse1;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFResponse Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PdfUtils.PrecompilaPDF(Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFRequest request) {
            return base.Channel.PrecompilaPDF(request);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFResponseType PrecompilaPDF(Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFRequestType PrecompilaPDFRequest1) {
            Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFRequest inValue = new Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFRequest();
            inValue.PrecompilaPDFRequest1 = PrecompilaPDFRequest1;
            Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PrecompilaPDFResponse retVal = ((Init.Sigepro.FrontEnd.AppLogic.ServizioPrecompilazionePDF.PdfUtils)(this)).PrecompilaPDF(inValue);
            return retVal.PrecompilaPDFResponse1;
        }
    }
}
