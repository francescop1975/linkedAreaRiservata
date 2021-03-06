//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.SIGePro.Manager.WsMercatiService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://gruppoinit.it/sigepro/definitions/mercati", ConfigurationName="WsMercatiService.Mercati")]
    public interface Mercati {
        
        // CODEGEN: Generating message contract since the operation PresenzePosteggio is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="getPresenzePosteggio", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioResponse1 PresenzePosteggio(Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioRequest1 request);
        
        // CODEGEN: Generating message contract since the operation PresenzeProprietario is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="getPresenzeProprietario", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioResponse1 PresenzeProprietario(Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioRequest1 request);
        
        // CODEGEN: Generating message contract since the operation Presenze is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="getPresenze", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.WsMercatiService.PresenzeResponse1 Presenze(Init.SIGePro.Manager.WsMercatiService.PresenzeRequest1 request);
        
        // CODEGEN: Generating message contract since the operation PresenzeManifestazione is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="getPresenzeManifestazione", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneResponse1 PresenzeManifestazione(Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneRequest1 request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati")]
    public partial class PresenzePosteggioRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private int codIstanzaField;
        
        private string codPosteggioField;
        
        private EstremiAut estremiAutField;
        
        private string catMercField;
        
        private bool inserisciAutSeNonTrovataField;
        
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
        public int codIstanza {
            get {
                return this.codIstanzaField;
            }
            set {
                this.codIstanzaField = value;
                this.RaisePropertyChanged("codIstanza");
            }
        }
        
        /// <remarks/>
        public string codPosteggio {
            get {
                return this.codPosteggioField;
            }
            set {
                this.codPosteggioField = value;
                this.RaisePropertyChanged("codPosteggio");
            }
        }
        
        /// <remarks/>
        public EstremiAut estremiAut {
            get {
                return this.estremiAutField;
            }
            set {
                this.estremiAutField = value;
                this.RaisePropertyChanged("estremiAut");
            }
        }
        
        /// <remarks/>
        public string catMerc {
            get {
                return this.catMercField;
            }
            set {
                this.catMercField = value;
                this.RaisePropertyChanged("catMerc");
            }
        }
        
        /// <remarks/>
        public bool inserisciAutSeNonTrovata {
            get {
                return this.inserisciAutSeNonTrovataField;
            }
            set {
                this.inserisciAutSeNonTrovataField = value;
                this.RaisePropertyChanged("inserisciAutSeNonTrovata");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati")]
    public partial class EstremiAut : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string autoriznumeroField;
        
        private string autorizdataField;
        
        private string codiceAutorizcomuneField;
        
        private int codiceAutorizregistroField;
        
        /// <remarks/>
        public string autoriznumero {
            get {
                return this.autoriznumeroField;
            }
            set {
                this.autoriznumeroField = value;
                this.RaisePropertyChanged("autoriznumero");
            }
        }
        
        /// <remarks/>
        public string autorizdata {
            get {
                return this.autorizdataField;
            }
            set {
                this.autorizdataField = value;
                this.RaisePropertyChanged("autorizdata");
            }
        }
        
        /// <remarks/>
        public string codiceAutorizcomune {
            get {
                return this.codiceAutorizcomuneField;
            }
            set {
                this.codiceAutorizcomuneField = value;
                this.RaisePropertyChanged("codiceAutorizcomune");
            }
        }
        
        /// <remarks/>
        public int codiceAutorizregistro {
            get {
                return this.codiceAutorizregistroField;
            }
            set {
                this.codiceAutorizregistroField = value;
                this.RaisePropertyChanged("codiceAutorizregistro");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati")]
    public partial class PresenzePosteggioResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int numeroPresenzeField;
        
        private int numeroPresenzePropField;
        
        /// <remarks/>
        public int numeroPresenze {
            get {
                return this.numeroPresenzeField;
            }
            set {
                this.numeroPresenzeField = value;
                this.RaisePropertyChanged("numeroPresenze");
            }
        }
        
        /// <remarks/>
        public int numeroPresenzeProp {
            get {
                return this.numeroPresenzePropField;
            }
            set {
                this.numeroPresenzePropField = value;
                this.RaisePropertyChanged("numeroPresenzeProp");
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
    public partial class PresenzePosteggioRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati", Order=0)]
        public Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioRequest PresenzePosteggioRequest;
        
        public PresenzePosteggioRequest1() {
        }
        
        public PresenzePosteggioRequest1(Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioRequest PresenzePosteggioRequest) {
            this.PresenzePosteggioRequest = PresenzePosteggioRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PresenzePosteggioResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati", Order=0)]
        public Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioResponse PresenzePosteggioResponse;
        
        public PresenzePosteggioResponse1() {
        }
        
        public PresenzePosteggioResponse1(Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioResponse PresenzePosteggioResponse) {
            this.PresenzePosteggioResponse = PresenzePosteggioResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati")]
    public partial class PresenzeProprietarioRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private int codIstanzaField;
        
        private EstremiAut estremiAutField;
        
        private string catMercField;
        
        private bool inserisciAutSeNonTrovataField;
        
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
        public int codIstanza {
            get {
                return this.codIstanzaField;
            }
            set {
                this.codIstanzaField = value;
                this.RaisePropertyChanged("codIstanza");
            }
        }
        
        /// <remarks/>
        public EstremiAut estremiAut {
            get {
                return this.estremiAutField;
            }
            set {
                this.estremiAutField = value;
                this.RaisePropertyChanged("estremiAut");
            }
        }
        
        /// <remarks/>
        public string catMerc {
            get {
                return this.catMercField;
            }
            set {
                this.catMercField = value;
                this.RaisePropertyChanged("catMerc");
            }
        }
        
        /// <remarks/>
        public bool inserisciAutSeNonTrovata {
            get {
                return this.inserisciAutSeNonTrovataField;
            }
            set {
                this.inserisciAutSeNonTrovataField = value;
                this.RaisePropertyChanged("inserisciAutSeNonTrovata");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati")]
    public partial class PresenzeProprietarioResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int numeroPresenzePropField;
        
        /// <remarks/>
        public int numeroPresenzeProp {
            get {
                return this.numeroPresenzePropField;
            }
            set {
                this.numeroPresenzePropField = value;
                this.RaisePropertyChanged("numeroPresenzeProp");
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
    public partial class PresenzeProprietarioRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati", Order=0)]
        public Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioRequest PresenzeProprietarioRequest;
        
        public PresenzeProprietarioRequest1() {
        }
        
        public PresenzeProprietarioRequest1(Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioRequest PresenzeProprietarioRequest) {
            this.PresenzeProprietarioRequest = PresenzeProprietarioRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PresenzeProprietarioResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati", Order=0)]
        public Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioResponse PresenzeProprietarioResponse;
        
        public PresenzeProprietarioResponse1() {
        }
        
        public PresenzeProprietarioResponse1(Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioResponse PresenzeProprietarioResponse) {
            this.PresenzeProprietarioResponse = PresenzeProprietarioResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati")]
    public partial class PresenzeRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private int codiceIstanzaField;
        
        private EstremiAut estremiAutField;
        
        private string catMercField;
        
        private bool inserisciAutSeNonTrovataField;
        
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
        public int codiceIstanza {
            get {
                return this.codiceIstanzaField;
            }
            set {
                this.codiceIstanzaField = value;
                this.RaisePropertyChanged("codiceIstanza");
            }
        }
        
        /// <remarks/>
        public EstremiAut estremiAut {
            get {
                return this.estremiAutField;
            }
            set {
                this.estremiAutField = value;
                this.RaisePropertyChanged("estremiAut");
            }
        }
        
        /// <remarks/>
        public string catMerc {
            get {
                return this.catMercField;
            }
            set {
                this.catMercField = value;
                this.RaisePropertyChanged("catMerc");
            }
        }
        
        /// <remarks/>
        public bool inserisciAutSeNonTrovata {
            get {
                return this.inserisciAutSeNonTrovataField;
            }
            set {
                this.inserisciAutSeNonTrovataField = value;
                this.RaisePropertyChanged("inserisciAutSeNonTrovata");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati")]
    public partial class PresenzeResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int numeroPresenzeField;
        
        /// <remarks/>
        public int numeroPresenze {
            get {
                return this.numeroPresenzeField;
            }
            set {
                this.numeroPresenzeField = value;
                this.RaisePropertyChanged("numeroPresenze");
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
    public partial class PresenzeRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati", Order=0)]
        public Init.SIGePro.Manager.WsMercatiService.PresenzeRequest PresenzeRequest;
        
        public PresenzeRequest1() {
        }
        
        public PresenzeRequest1(Init.SIGePro.Manager.WsMercatiService.PresenzeRequest PresenzeRequest) {
            this.PresenzeRequest = PresenzeRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PresenzeResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati", Order=0)]
        public Init.SIGePro.Manager.WsMercatiService.PresenzeResponse PresenzeResponse;
        
        public PresenzeResponse1() {
        }
        
        public PresenzeResponse1(Init.SIGePro.Manager.WsMercatiService.PresenzeResponse PresenzeResponse) {
            this.PresenzeResponse = PresenzeResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati")]
    public partial class PresenzeManifestazioneRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private int codiceIstanzaField;
        
        private int codiceManifestazioneField;
        
        private int codiceUsoField;
        
        private bool codiceUsoFieldSpecified;
        
        private EstremiAut estremiAutField;
        
        private string catMercField;
        
        private bool inserisciAutSeNonTrovataField;
        
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
        public int codiceIstanza {
            get {
                return this.codiceIstanzaField;
            }
            set {
                this.codiceIstanzaField = value;
                this.RaisePropertyChanged("codiceIstanza");
            }
        }
        
        /// <remarks/>
        public int codiceManifestazione {
            get {
                return this.codiceManifestazioneField;
            }
            set {
                this.codiceManifestazioneField = value;
                this.RaisePropertyChanged("codiceManifestazione");
            }
        }
        
        /// <remarks/>
        public int codiceUso {
            get {
                return this.codiceUsoField;
            }
            set {
                this.codiceUsoField = value;
                this.RaisePropertyChanged("codiceUso");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool codiceUsoSpecified {
            get {
                return this.codiceUsoFieldSpecified;
            }
            set {
                this.codiceUsoFieldSpecified = value;
                this.RaisePropertyChanged("codiceUsoSpecified");
            }
        }
        
        /// <remarks/>
        public EstremiAut estremiAut {
            get {
                return this.estremiAutField;
            }
            set {
                this.estremiAutField = value;
                this.RaisePropertyChanged("estremiAut");
            }
        }
        
        /// <remarks/>
        public string catMerc {
            get {
                return this.catMercField;
            }
            set {
                this.catMercField = value;
                this.RaisePropertyChanged("catMerc");
            }
        }
        
        /// <remarks/>
        public bool inserisciAutSeNonTrovata {
            get {
                return this.inserisciAutSeNonTrovataField;
            }
            set {
                this.inserisciAutSeNonTrovataField = value;
                this.RaisePropertyChanged("inserisciAutSeNonTrovata");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1009")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati")]
    public partial class PresenzeManifestazioneResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int numeroPresenzeField;
        
        private int numeroPresenzePropField;
        
        /// <remarks/>
        public int numeroPresenze {
            get {
                return this.numeroPresenzeField;
            }
            set {
                this.numeroPresenzeField = value;
                this.RaisePropertyChanged("numeroPresenze");
            }
        }
        
        /// <remarks/>
        public int numeroPresenzeProp {
            get {
                return this.numeroPresenzePropField;
            }
            set {
                this.numeroPresenzePropField = value;
                this.RaisePropertyChanged("numeroPresenzeProp");
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
    public partial class PresenzeManifestazioneRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati", Order=0)]
        public Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneRequest PresenzeManifestazioneRequest;
        
        public PresenzeManifestazioneRequest1() {
        }
        
        public PresenzeManifestazioneRequest1(Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneRequest PresenzeManifestazioneRequest) {
            this.PresenzeManifestazioneRequest = PresenzeManifestazioneRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class PresenzeManifestazioneResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/mercati", Order=0)]
        public Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneResponse PresenzeManifestazioneResponse;
        
        public PresenzeManifestazioneResponse1() {
        }
        
        public PresenzeManifestazioneResponse1(Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneResponse PresenzeManifestazioneResponse) {
            this.PresenzeManifestazioneResponse = PresenzeManifestazioneResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface MercatiChannel : Init.SIGePro.Manager.WsMercatiService.Mercati, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MercatiClient : System.ServiceModel.ClientBase<Init.SIGePro.Manager.WsMercatiService.Mercati>, Init.SIGePro.Manager.WsMercatiService.Mercati {
        
        public MercatiClient() {
        }
        
        public MercatiClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MercatiClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MercatiClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MercatiClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioResponse1 Init.SIGePro.Manager.WsMercatiService.Mercati.PresenzePosteggio(Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioRequest1 request) {
            return base.Channel.PresenzePosteggio(request);
        }
        
        public Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioResponse PresenzePosteggio(Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioRequest PresenzePosteggioRequest) {
            Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioRequest1 inValue = new Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioRequest1();
            inValue.PresenzePosteggioRequest = PresenzePosteggioRequest;
            Init.SIGePro.Manager.WsMercatiService.PresenzePosteggioResponse1 retVal = ((Init.SIGePro.Manager.WsMercatiService.Mercati)(this)).PresenzePosteggio(inValue);
            return retVal.PresenzePosteggioResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioResponse1 Init.SIGePro.Manager.WsMercatiService.Mercati.PresenzeProprietario(Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioRequest1 request) {
            return base.Channel.PresenzeProprietario(request);
        }
        
        public Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioResponse PresenzeProprietario(Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioRequest PresenzeProprietarioRequest) {
            Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioRequest1 inValue = new Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioRequest1();
            inValue.PresenzeProprietarioRequest = PresenzeProprietarioRequest;
            Init.SIGePro.Manager.WsMercatiService.PresenzeProprietarioResponse1 retVal = ((Init.SIGePro.Manager.WsMercatiService.Mercati)(this)).PresenzeProprietario(inValue);
            return retVal.PresenzeProprietarioResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.WsMercatiService.PresenzeResponse1 Init.SIGePro.Manager.WsMercatiService.Mercati.Presenze(Init.SIGePro.Manager.WsMercatiService.PresenzeRequest1 request) {
            return base.Channel.Presenze(request);
        }
        
        public Init.SIGePro.Manager.WsMercatiService.PresenzeResponse Presenze(Init.SIGePro.Manager.WsMercatiService.PresenzeRequest PresenzeRequest) {
            Init.SIGePro.Manager.WsMercatiService.PresenzeRequest1 inValue = new Init.SIGePro.Manager.WsMercatiService.PresenzeRequest1();
            inValue.PresenzeRequest = PresenzeRequest;
            Init.SIGePro.Manager.WsMercatiService.PresenzeResponse1 retVal = ((Init.SIGePro.Manager.WsMercatiService.Mercati)(this)).Presenze(inValue);
            return retVal.PresenzeResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneResponse1 Init.SIGePro.Manager.WsMercatiService.Mercati.PresenzeManifestazione(Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneRequest1 request) {
            return base.Channel.PresenzeManifestazione(request);
        }
        
        public Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneResponse PresenzeManifestazione(Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneRequest PresenzeManifestazioneRequest) {
            Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneRequest1 inValue = new Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneRequest1();
            inValue.PresenzeManifestazioneRequest = PresenzeManifestazioneRequest;
            Init.SIGePro.Manager.WsMercatiService.PresenzeManifestazioneResponse1 retVal = ((Init.SIGePro.Manager.WsMercatiService.Mercati)(this)).PresenzeManifestazione(inValue);
            return retVal.PresenzeManifestazioneResponse;
        }
    }
}
