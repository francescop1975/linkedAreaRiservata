﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://init.sigepro.it", ConfigurationName="SigeproSitWebService.WsSitSoap")]
    public interface WsSitSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://init.sigepro.it/GetListField", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.ListSit GetListField(string token, string field, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.Sit dataSit, string software);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://init.sigepro.it/ValidateField", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.ValidateSit ValidateField(string token, string field, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.Sit dataSit, string software);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://init.sigepro.it/GetDetailField", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.DetailSit GetDetailField(string token, string field, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.Sit dataSit, string software);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://init.sigepro.it/EffettuaValidazioneFormale", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool EffettuaValidazioneFormale(string token, string software, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.Sit sitClass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://init.sigepro.it/GetCampiGestiti", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string[] GetCampiGestiti(string token, string software);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://init.sigepro.it/GetFeatures", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.SitFeatures GetFeatures(string token, string software);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://init.sigepro.it/GetListaVie", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.DettagliVia[] GetListaVie(string token, string software, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.FiltroRicercaListaVie filtro, string[] codiciComuni);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://init.sigepro.it")]
    public partial class Sit : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idComuneField;
        
        private string codViaField;
        
        private string civicoField;
        
        private string kmField;
        
        private string esponenteField;
        
        private string coloreField;
        
        private string scalaField;
        
        private string internoField;
        
        private string esponenteInternoField;
        
        private string codCivicoField;
        
        private string tipoCatastoField;
        
        private string sezioneField;
        
        private string foglioField;
        
        private string particellaField;
        
        private string subField;
        
        private string uiField;
        
        private string fabbricatoField;
        
        private string oggettoTerritorialeField;
        
        private string descrizioneViaField;
        
        private string cAPField;
        
        private string circoscrizioneField;
        
        private string frazioneField;
        
        private string zonaField;
        
        private string pianoField;
        
        private string quartiereField;
        
        private string codiceComuneField;
        
        private string accessoTipoField;
        
        private string accessoNumeroField;
        
        private string accessoDescrizioneField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string CodVia {
            get {
                return this.codViaField;
            }
            set {
                this.codViaField = value;
                this.RaisePropertyChanged("CodVia");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Civico {
            get {
                return this.civicoField;
            }
            set {
                this.civicoField = value;
                this.RaisePropertyChanged("Civico");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Km {
            get {
                return this.kmField;
            }
            set {
                this.kmField = value;
                this.RaisePropertyChanged("Km");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string Esponente {
            get {
                return this.esponenteField;
            }
            set {
                this.esponenteField = value;
                this.RaisePropertyChanged("Esponente");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string Colore {
            get {
                return this.coloreField;
            }
            set {
                this.coloreField = value;
                this.RaisePropertyChanged("Colore");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string Scala {
            get {
                return this.scalaField;
            }
            set {
                this.scalaField = value;
                this.RaisePropertyChanged("Scala");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string Interno {
            get {
                return this.internoField;
            }
            set {
                this.internoField = value;
                this.RaisePropertyChanged("Interno");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string EsponenteInterno {
            get {
                return this.esponenteInternoField;
            }
            set {
                this.esponenteInternoField = value;
                this.RaisePropertyChanged("EsponenteInterno");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string CodCivico {
            get {
                return this.codCivicoField;
            }
            set {
                this.codCivicoField = value;
                this.RaisePropertyChanged("CodCivico");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string TipoCatasto {
            get {
                return this.tipoCatastoField;
            }
            set {
                this.tipoCatastoField = value;
                this.RaisePropertyChanged("TipoCatasto");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string Sezione {
            get {
                return this.sezioneField;
            }
            set {
                this.sezioneField = value;
                this.RaisePropertyChanged("Sezione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string Foglio {
            get {
                return this.foglioField;
            }
            set {
                this.foglioField = value;
                this.RaisePropertyChanged("Foglio");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public string Particella {
            get {
                return this.particellaField;
            }
            set {
                this.particellaField = value;
                this.RaisePropertyChanged("Particella");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        public string Sub {
            get {
                return this.subField;
            }
            set {
                this.subField = value;
                this.RaisePropertyChanged("Sub");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=15)]
        public string UI {
            get {
                return this.uiField;
            }
            set {
                this.uiField = value;
                this.RaisePropertyChanged("UI");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=16)]
        public string Fabbricato {
            get {
                return this.fabbricatoField;
            }
            set {
                this.fabbricatoField = value;
                this.RaisePropertyChanged("Fabbricato");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=17)]
        public string OggettoTerritoriale {
            get {
                return this.oggettoTerritorialeField;
            }
            set {
                this.oggettoTerritorialeField = value;
                this.RaisePropertyChanged("OggettoTerritoriale");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=18)]
        public string DescrizioneVia {
            get {
                return this.descrizioneViaField;
            }
            set {
                this.descrizioneViaField = value;
                this.RaisePropertyChanged("DescrizioneVia");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=19)]
        public string CAP {
            get {
                return this.cAPField;
            }
            set {
                this.cAPField = value;
                this.RaisePropertyChanged("CAP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=20)]
        public string Circoscrizione {
            get {
                return this.circoscrizioneField;
            }
            set {
                this.circoscrizioneField = value;
                this.RaisePropertyChanged("Circoscrizione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=21)]
        public string Frazione {
            get {
                return this.frazioneField;
            }
            set {
                this.frazioneField = value;
                this.RaisePropertyChanged("Frazione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=22)]
        public string Zona {
            get {
                return this.zonaField;
            }
            set {
                this.zonaField = value;
                this.RaisePropertyChanged("Zona");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=23)]
        public string Piano {
            get {
                return this.pianoField;
            }
            set {
                this.pianoField = value;
                this.RaisePropertyChanged("Piano");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=24)]
        public string Quartiere {
            get {
                return this.quartiereField;
            }
            set {
                this.quartiereField = value;
                this.RaisePropertyChanged("Quartiere");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=25)]
        public string CodiceComune {
            get {
                return this.codiceComuneField;
            }
            set {
                this.codiceComuneField = value;
                this.RaisePropertyChanged("CodiceComune");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=26)]
        public string AccessoTipo {
            get {
                return this.accessoTipoField;
            }
            set {
                this.accessoTipoField = value;
                this.RaisePropertyChanged("AccessoTipo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=27)]
        public string AccessoNumero {
            get {
                return this.accessoNumeroField;
            }
            set {
                this.accessoNumeroField = value;
                this.RaisePropertyChanged("AccessoNumero");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=28)]
        public string AccessoDescrizione {
            get {
                return this.accessoDescrizioneField;
            }
            set {
                this.accessoDescrizioneField = value;
                this.RaisePropertyChanged("AccessoDescrizione");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://init.sigepro.it")]
    public partial class DettagliVia : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codiceViarioField;
        
        private string toponimoField;
        
        private string denominazioneField;
        
        private string localitaField;
        
        private string codiceComuneField;
        
        private System.Nullable<System.DateTime> dataFineValiditaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string CodiceViario {
            get {
                return this.codiceViarioField;
            }
            set {
                this.codiceViarioField = value;
                this.RaisePropertyChanged("CodiceViario");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Toponimo {
            get {
                return this.toponimoField;
            }
            set {
                this.toponimoField = value;
                this.RaisePropertyChanged("Toponimo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Denominazione {
            get {
                return this.denominazioneField;
            }
            set {
                this.denominazioneField = value;
                this.RaisePropertyChanged("Denominazione");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string Localita {
            get {
                return this.localitaField;
            }
            set {
                this.localitaField = value;
                this.RaisePropertyChanged("Localita");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string CodiceComune {
            get {
                return this.codiceComuneField;
            }
            set {
                this.codiceComuneField = value;
                this.RaisePropertyChanged("CodiceComune");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public System.Nullable<System.DateTime> DataFineValidita {
            get {
                return this.dataFineValiditaField;
            }
            set {
                this.dataFineValiditaField = value;
                this.RaisePropertyChanged("DataFineValidita");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://init.sigepro.it")]
    public partial class BaseDtoOfTipoVisualizzazioneString : object, System.ComponentModel.INotifyPropertyChanged {
        
        private TipoVisualizzazione codiceField;
        
        private string descrizioneField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public TipoVisualizzazione Codice {
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://init.sigepro.it")]
    public enum TipoVisualizzazione {
        
        /// <remarks/>
        PuntoDaIndirizzo,
        
        /// <remarks/>
        PuntoDaMappale,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://init.sigepro.it")]
    public partial class SitFeatures : object, System.ComponentModel.INotifyPropertyChanged {
        
        private BaseDtoOfTipoVisualizzazioneString[] visualizzazioniFrontofficeField;
        
        private BaseDtoOfTipoVisualizzazioneString[] visualizzazioniBackofficeField;
        
        private string[] campiGestitiField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        public BaseDtoOfTipoVisualizzazioneString[] VisualizzazioniFrontoffice {
            get {
                return this.visualizzazioniFrontofficeField;
            }
            set {
                this.visualizzazioniFrontofficeField = value;
                this.RaisePropertyChanged("VisualizzazioniFrontoffice");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=1)]
        public BaseDtoOfTipoVisualizzazioneString[] VisualizzazioniBackoffice {
            get {
                return this.visualizzazioniBackofficeField;
            }
            set {
                this.visualizzazioniBackofficeField = value;
                this.RaisePropertyChanged("VisualizzazioniBackoffice");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=2)]
        public string[] CampiGestiti {
            get {
                return this.campiGestitiField;
            }
            set {
                this.campiGestitiField = value;
                this.RaisePropertyChanged("CampiGestiti");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://init.sigepro.it")]
    public partial class DetailField : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string campoField;
        
        private string valoreField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Campo {
            get {
                return this.campoField;
            }
            set {
                this.campoField = value;
                this.RaisePropertyChanged("Campo");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://init.sigepro.it")]
    public partial class DetailSit : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool returnValueField;
        
        private string messageCodeField;
        
        private string messageField;
        
        private DetailField[] fieldField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool ReturnValue {
            get {
                return this.returnValueField;
            }
            set {
                this.returnValueField = value;
                this.RaisePropertyChanged("ReturnValue");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string MessageCode {
            get {
                return this.messageCodeField;
            }
            set {
                this.messageCodeField = value;
                this.RaisePropertyChanged("MessageCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("Message");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=3)]
        public DetailField[] Field {
            get {
                return this.fieldField;
            }
            set {
                this.fieldField = value;
                this.RaisePropertyChanged("Field");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://init.sigepro.it")]
    public partial class ValidateSit : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool returnValueField;
        
        private string messageCodeField;
        
        private string messageField;
        
        private Sit dataSitField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool ReturnValue {
            get {
                return this.returnValueField;
            }
            set {
                this.returnValueField = value;
                this.RaisePropertyChanged("ReturnValue");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string MessageCode {
            get {
                return this.messageCodeField;
            }
            set {
                this.messageCodeField = value;
                this.RaisePropertyChanged("MessageCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("Message");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public Sit DataSit {
            get {
                return this.dataSitField;
            }
            set {
                this.dataSitField = value;
                this.RaisePropertyChanged("DataSit");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://init.sigepro.it")]
    public partial class ListSit : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool returnValueField;
        
        private string messageCodeField;
        
        private string messageField;
        
        private string[] fieldField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool ReturnValue {
            get {
                return this.returnValueField;
            }
            set {
                this.returnValueField = value;
                this.RaisePropertyChanged("ReturnValue");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string MessageCode {
            get {
                return this.messageCodeField;
            }
            set {
                this.messageCodeField = value;
                this.RaisePropertyChanged("MessageCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("Message");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=3)]
        public string[] Field {
            get {
                return this.fieldField;
            }
            set {
                this.fieldField = value;
                this.RaisePropertyChanged("Field");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://init.sigepro.it")]
    public enum FiltroRicercaListaVie {
        
        /// <remarks/>
        Tutte,
        
        /// <remarks/>
        Cessata,
        
        /// <remarks/>
        Attiva,
        
        /// <remarks/>
        Modificata,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WsSitSoapChannel : Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.WsSitSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsSitSoapClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.WsSitSoap>, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.WsSitSoap {
        
        public WsSitSoapClient() {
        }
        
        public WsSitSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsSitSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsSitSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsSitSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.ListSit GetListField(string token, string field, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.Sit dataSit, string software) {
            return base.Channel.GetListField(token, field, dataSit, software);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.ValidateSit ValidateField(string token, string field, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.Sit dataSit, string software) {
            return base.Channel.ValidateField(token, field, dataSit, software);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.DetailSit GetDetailField(string token, string field, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.Sit dataSit, string software) {
            return base.Channel.GetDetailField(token, field, dataSit, software);
        }
        
        public bool EffettuaValidazioneFormale(string token, string software, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.Sit sitClass) {
            return base.Channel.EffettuaValidazioneFormale(token, software, sitClass);
        }
        
        public string[] GetCampiGestiti(string token, string software) {
            return base.Channel.GetCampiGestiti(token, software);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.SitFeatures GetFeatures(string token, string software) {
            return base.Channel.GetFeatures(token, software);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.DettagliVia[] GetListaVie(string token, string software, Init.Sigepro.FrontEnd.AppLogic.SigeproSitWebService.FiltroRicercaListaVie filtro, string[] codiciComuni) {
            return base.Channel.GetListaVie(token, software, filtro, codiciComuni);
        }
    }
}