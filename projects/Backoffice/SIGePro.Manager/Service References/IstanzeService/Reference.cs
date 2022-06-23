﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.SIGePro.Manager.IstanzeService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://gruppoinit.it/sigepro/definitions/istanze", ConfigurationName="IstanzeService.Istanze")]
    public interface Istanze {
        
        // CODEGEN: Generazione di un contratto di messaggio perché l'operazione IstanzeRuoli non è RPC né incapsulata da documenti.
        [System.ServiceModel.OperationContractAttribute(Action="IstanzeRuoli", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.IstanzeService.IstanzeRuoliResponse1 IstanzeRuoli(Init.SIGePro.Manager.IstanzeService.IstanzeRuoli request);
        
        // CODEGEN: Generazione di un contratto di messaggio perché l'operazione SoggettiCollegatiInsert non è RPC né incapsulata da documenti.
        [System.ServiceModel.OperationContractAttribute(Action="SoggettiCollegatiInsert", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsertResponse SoggettiCollegatiInsert(Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsert request);
        
        // CODEGEN: Generazione di un contratto di messaggio perché l'operazione IstanzeResponsabili non è RPC né incapsulata da documenti.
        [System.ServiceModel.OperationContractAttribute(Action="IstanzeResponsabili", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.SIGePro.Manager.IstanzeService.IstanzeResponsabiliResponse1 IstanzeResponsabili(Init.SIGePro.Manager.IstanzeService.IstanzeResponsabili request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze")]
    public partial class IstanzeRuoliRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private int codiceRuoloField;
        
        private int codiceIstanzaField;
        
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
        public int codiceRuolo {
            get {
                return this.codiceRuoloField;
            }
            set {
                this.codiceRuoloField = value;
                this.RaisePropertyChanged("codiceRuolo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int codiceIstanza {
            get {
                return this.codiceIstanzaField;
            }
            set {
                this.codiceIstanzaField = value;
                this.RaisePropertyChanged("codiceIstanza");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze")]
    public partial class IdentificativiAnagrafeType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string partitaIvaField;
        
        private string codiceFiscaleField;
        
        private int codiceAnagraficaField;
        
        private bool codiceAnagraficaFieldSpecified;
        
        private TipoanagrafeEnum tipoanagrafeEnumField;
        
        public IdentificativiAnagrafeType() {
            this.tipoanagrafeEnumField = TipoanagrafeEnum.NON_SPECIFICATO;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int codiceAnagrafica {
            get {
                return this.codiceAnagraficaField;
            }
            set {
                this.codiceAnagraficaField = value;
                this.RaisePropertyChanged("codiceAnagrafica");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool codiceAnagraficaSpecified {
            get {
                return this.codiceAnagraficaFieldSpecified;
            }
            set {
                this.codiceAnagraficaFieldSpecified = value;
                this.RaisePropertyChanged("codiceAnagraficaSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public TipoanagrafeEnum tipoanagrafeEnum {
            get {
                return this.tipoanagrafeEnumField;
            }
            set {
                this.tipoanagrafeEnumField = value;
                this.RaisePropertyChanged("tipoanagrafeEnum");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze")]
    public enum TipoanagrafeEnum {
        
        /// <remarks/>
        G,
        
        /// <remarks/>
        F,
        
        /// <remarks/>
        NON_SPECIFICATO,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/base")]
    public partial class ErroreBackofficeType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codiceField;
        
        private string descrizioneField;
        
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/base")]
    public partial class EsitoOperazioneType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int esitoField;
        
        private ErroreBackofficeType[] listaErroriField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int esito {
            get {
                return this.esitoField;
            }
            set {
                this.esitoField = value;
                this.RaisePropertyChanged("esito");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("listaErrori", IsNullable=true, Order=1)]
        public ErroreBackofficeType[] listaErrori {
            get {
                return this.listaErroriField;
            }
            set {
                this.listaErroriField = value;
                this.RaisePropertyChanged("listaErrori");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze")]
    public partial class IstanzeRuoliResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private EsitoOperazioneType esitoOperazioneTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public EsitoOperazioneType EsitoOperazioneType {
            get {
                return this.esitoOperazioneTypeField;
            }
            set {
                this.esitoOperazioneTypeField = value;
                this.RaisePropertyChanged("EsitoOperazioneType");
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
    public partial class IstanzeRuoli {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze", Order=0)]
        public Init.SIGePro.Manager.IstanzeService.IstanzeRuoliRequest IstanzeRuoliRequest;
        
        public IstanzeRuoli() {
        }
        
        public IstanzeRuoli(Init.SIGePro.Manager.IstanzeService.IstanzeRuoliRequest IstanzeRuoliRequest) {
            this.IstanzeRuoliRequest = IstanzeRuoliRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class IstanzeRuoliResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze", Order=0)]
        public Init.SIGePro.Manager.IstanzeService.IstanzeRuoliResponse IstanzeRuoliResponse;
        
        public IstanzeRuoliResponse1() {
        }
        
        public IstanzeRuoliResponse1(Init.SIGePro.Manager.IstanzeService.IstanzeRuoliResponse IstanzeRuoliResponse) {
            this.IstanzeRuoliResponse = IstanzeRuoliResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze")]
    public partial class SoggettiCollegatiInsertByIdentificativoRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private IdentificativiAnagrafeType identificativiAnagrafeTypeField;
        
        private int codiceTipologiaSoggettoField;
        
        private int codiceIstanzaField;
        
        private bool isNonInserireSeTipologiaPresenteField;
        
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
        public IdentificativiAnagrafeType identificativiAnagrafeType {
            get {
                return this.identificativiAnagrafeTypeField;
            }
            set {
                this.identificativiAnagrafeTypeField = value;
                this.RaisePropertyChanged("identificativiAnagrafeType");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int codiceTipologiaSoggetto {
            get {
                return this.codiceTipologiaSoggettoField;
            }
            set {
                this.codiceTipologiaSoggettoField = value;
                this.RaisePropertyChanged("codiceTipologiaSoggetto");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public bool isNonInserireSeTipologiaPresente {
            get {
                return this.isNonInserireSeTipologiaPresenteField;
            }
            set {
                this.isNonInserireSeTipologiaPresenteField = value;
                this.RaisePropertyChanged("isNonInserireSeTipologiaPresente");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze")]
    public partial class SoggettiCollegatiInsertByIdentificativoResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codiceSoggettoField;
        
        private string nominativoField;
        
        private EsitoOperazioneType esitoOperazioneTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=0)]
        public string codiceSoggetto {
            get {
                return this.codiceSoggettoField;
            }
            set {
                this.codiceSoggettoField = value;
                this.RaisePropertyChanged("codiceSoggetto");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string nominativo {
            get {
                return this.nominativoField;
            }
            set {
                this.nominativoField = value;
                this.RaisePropertyChanged("nominativo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public EsitoOperazioneType EsitoOperazioneType {
            get {
                return this.esitoOperazioneTypeField;
            }
            set {
                this.esitoOperazioneTypeField = value;
                this.RaisePropertyChanged("EsitoOperazioneType");
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
    public partial class SoggettiCollegatiInsert {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze", Order=0)]
        public Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsertByIdentificativoRequest SoggettiCollegatiInsertByIdentificativoRequest;
        
        public SoggettiCollegatiInsert() {
        }
        
        public SoggettiCollegatiInsert(Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsertByIdentificativoRequest SoggettiCollegatiInsertByIdentificativoRequest) {
            this.SoggettiCollegatiInsertByIdentificativoRequest = SoggettiCollegatiInsertByIdentificativoRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SoggettiCollegatiInsertResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze", Order=0)]
        public Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsertByIdentificativoResponse SoggettiCollegatiInsertByIdentificativoResponse;
        
        public SoggettiCollegatiInsertResponse() {
        }
        
        public SoggettiCollegatiInsertResponse(Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsertByIdentificativoResponse SoggettiCollegatiInsertByIdentificativoResponse) {
            this.SoggettiCollegatiInsertByIdentificativoResponse = SoggettiCollegatiInsertByIdentificativoResponse;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze")]
    public partial class IstanzeResponsabiliRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string tokenField;
        
        private int codiceResponsabileField;
        
        private int codiceIstanzaField;
        
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
        public int codiceResponsabile {
            get {
                return this.codiceResponsabileField;
            }
            set {
                this.codiceResponsabileField = value;
                this.RaisePropertyChanged("codiceResponsabile");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public int codiceIstanza {
            get {
                return this.codiceIstanzaField;
            }
            set {
                this.codiceIstanzaField = value;
                this.RaisePropertyChanged("codiceIstanza");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze")]
    public partial class IstanzeResponsabiliResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private EsitoOperazioneType esitoOperazioneTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public EsitoOperazioneType EsitoOperazioneType {
            get {
                return this.esitoOperazioneTypeField;
            }
            set {
                this.esitoOperazioneTypeField = value;
                this.RaisePropertyChanged("EsitoOperazioneType");
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
    public partial class IstanzeResponsabili {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze", Order=0)]
        public Init.SIGePro.Manager.IstanzeService.IstanzeResponsabiliRequest IstanzeResponsabiliRequest;
        
        public IstanzeResponsabili() {
        }
        
        public IstanzeResponsabili(Init.SIGePro.Manager.IstanzeService.IstanzeResponsabiliRequest IstanzeResponsabiliRequest) {
            this.IstanzeResponsabiliRequest = IstanzeResponsabiliRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class IstanzeResponsabiliResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://gruppoinit.it/sigepro/schemas/messages/istanze", Order=0)]
        public Init.SIGePro.Manager.IstanzeService.IstanzeResponsabiliResponse IstanzeResponsabiliResponse;
        
        public IstanzeResponsabiliResponse1() {
        }
        
        public IstanzeResponsabiliResponse1(Init.SIGePro.Manager.IstanzeService.IstanzeResponsabiliResponse IstanzeResponsabiliResponse) {
            this.IstanzeResponsabiliResponse = IstanzeResponsabiliResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IstanzeChannel : Init.SIGePro.Manager.IstanzeService.Istanze, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IstanzeClient : System.ServiceModel.ClientBase<Init.SIGePro.Manager.IstanzeService.Istanze>, Init.SIGePro.Manager.IstanzeService.Istanze {
        
        public IstanzeClient() {
        }
        
        public IstanzeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IstanzeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IstanzeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IstanzeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.IstanzeService.IstanzeRuoliResponse1 Init.SIGePro.Manager.IstanzeService.Istanze.IstanzeRuoli(Init.SIGePro.Manager.IstanzeService.IstanzeRuoli request) {
            return base.Channel.IstanzeRuoli(request);
        }
        
        public Init.SIGePro.Manager.IstanzeService.IstanzeRuoliResponse IstanzeRuoli(Init.SIGePro.Manager.IstanzeService.IstanzeRuoliRequest IstanzeRuoliRequest) {
            Init.SIGePro.Manager.IstanzeService.IstanzeRuoli inValue = new Init.SIGePro.Manager.IstanzeService.IstanzeRuoli();
            inValue.IstanzeRuoliRequest = IstanzeRuoliRequest;
            Init.SIGePro.Manager.IstanzeService.IstanzeRuoliResponse1 retVal = ((Init.SIGePro.Manager.IstanzeService.Istanze)(this)).IstanzeRuoli(inValue);
            return retVal.IstanzeRuoliResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsertResponse Init.SIGePro.Manager.IstanzeService.Istanze.SoggettiCollegatiInsert(Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsert request) {
            return base.Channel.SoggettiCollegatiInsert(request);
        }
        
        public Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsertByIdentificativoResponse SoggettiCollegatiInsert(Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsertByIdentificativoRequest SoggettiCollegatiInsertByIdentificativoRequest) {
            Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsert inValue = new Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsert();
            inValue.SoggettiCollegatiInsertByIdentificativoRequest = SoggettiCollegatiInsertByIdentificativoRequest;
            Init.SIGePro.Manager.IstanzeService.SoggettiCollegatiInsertResponse retVal = ((Init.SIGePro.Manager.IstanzeService.Istanze)(this)).SoggettiCollegatiInsert(inValue);
            return retVal.SoggettiCollegatiInsertByIdentificativoResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.SIGePro.Manager.IstanzeService.IstanzeResponsabiliResponse1 Init.SIGePro.Manager.IstanzeService.Istanze.IstanzeResponsabili(Init.SIGePro.Manager.IstanzeService.IstanzeResponsabili request) {
            return base.Channel.IstanzeResponsabili(request);
        }
        
        public Init.SIGePro.Manager.IstanzeService.IstanzeResponsabiliResponse IstanzeResponsabili(Init.SIGePro.Manager.IstanzeService.IstanzeResponsabiliRequest IstanzeResponsabiliRequest) {
            Init.SIGePro.Manager.IstanzeService.IstanzeResponsabili inValue = new Init.SIGePro.Manager.IstanzeService.IstanzeResponsabili();
            inValue.IstanzeResponsabiliRequest = IstanzeResponsabiliRequest;
            Init.SIGePro.Manager.IstanzeService.IstanzeResponsabiliResponse1 retVal = ((Init.SIGePro.Manager.IstanzeService.Istanze)(this)).IstanzeResponsabili(inValue);
            return retVal.IstanzeResponsabiliResponse;
        }
    }
}