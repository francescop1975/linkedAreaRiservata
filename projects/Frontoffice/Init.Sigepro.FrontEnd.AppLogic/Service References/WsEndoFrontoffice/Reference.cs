﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FamigliaEndoFrontoffice", Namespace="http://schemas.datacontract.org/2004/07/Init.SIGePro.Manager.Logic.GestioneEndopr" +
        "ocedimenti")]
    [System.SerializableAttribute()]
    public partial class FamigliaEndoFrontoffice : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodiceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescrizioneField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Codice {
            get {
                return this.CodiceField;
            }
            set {
                if ((this.CodiceField.Equals(value) != true)) {
                    this.CodiceField = value;
                    this.RaisePropertyChanged("Codice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Descrizione {
            get {
                return this.DescrizioneField;
            }
            set {
                if ((object.ReferenceEquals(this.DescrizioneField, value) != true)) {
                    this.DescrizioneField = value;
                    this.RaisePropertyChanged("Descrizione");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="CategoriaEndoFrontoffice", Namespace="http://schemas.datacontract.org/2004/07/Init.SIGePro.Manager.Logic.GestioneEndopr" +
        "ocedimenti")]
    [System.SerializableAttribute()]
    public partial class CategoriaEndoFrontoffice : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodiceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescrizioneField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Codice {
            get {
                return this.CodiceField;
            }
            set {
                if ((this.CodiceField.Equals(value) != true)) {
                    this.CodiceField = value;
                    this.RaisePropertyChanged("Codice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Descrizione {
            get {
                return this.DescrizioneField;
            }
            set {
                if ((object.ReferenceEquals(this.DescrizioneField, value) != true)) {
                    this.DescrizioneField = value;
                    this.RaisePropertyChanged("Descrizione");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="EndoBreveFrontoffice", Namespace="http://schemas.datacontract.org/2004/07/Init.SIGePro.Manager.Logic.GestioneEndopr" +
        "ocedimenti")]
    [System.SerializableAttribute()]
    public partial class EndoBreveFrontoffice : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodiceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescrizioneField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Codice {
            get {
                return this.CodiceField;
            }
            set {
                if ((this.CodiceField.Equals(value) != true)) {
                    this.CodiceField = value;
                    this.RaisePropertyChanged("Codice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Descrizione {
            get {
                return this.DescrizioneField;
            }
            set {
                if ((object.ReferenceEquals(this.DescrizioneField, value) != true)) {
                    this.DescrizioneField = value;
                    this.RaisePropertyChanged("Descrizione");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LivelloCaricamentoGerarchia", Namespace="http://schemas.datacontract.org/2004/07/Sigepro.net.WebServices.WsAreaRiservata.W" +
        "cfServices.EndoFrontoffice")]
    public enum LivelloCaricamentoGerarchia : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Famiglia = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Categoria = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Endo = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RisultatoCaricamentoGerarchiaEndo", Namespace="http://schemas.datacontract.org/2004/07/Sigepro.net.WebServices.WsAreaRiservata.W" +
        "cfServices.EndoFrontoffice")]
    [System.SerializableAttribute()]
    public partial class RisultatoCaricamentoGerarchiaEndo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CategoriaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int EndoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int FamigliaField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Categoria {
            get {
                return this.CategoriaField;
            }
            set {
                if ((this.CategoriaField.Equals(value) != true)) {
                    this.CategoriaField = value;
                    this.RaisePropertyChanged("Categoria");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Endo {
            get {
                return this.EndoField;
            }
            set {
                if ((this.EndoField.Equals(value) != true)) {
                    this.EndoField = value;
                    this.RaisePropertyChanged("Endo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Famiglia {
            get {
                return this.FamigliaField;
            }
            set {
                if ((this.FamigliaField.Equals(value) != true)) {
                    this.FamigliaField = value;
                    this.RaisePropertyChanged("Famiglia");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TipoRicercaEnum", Namespace="http://schemas.datacontract.org/2004/07/Init.SIGePro.Manager.Logic.GestioneEndopr" +
        "ocedimenti")]
    public enum TipoRicercaEnum : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FraseCompleta = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AlmenoUnaParola = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TutteLeParole = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RisultatoRicercaTestualeEndo", Namespace="http://schemas.datacontract.org/2004/07/Init.SIGePro.Manager.Logic.GestioneEndopr" +
        "ocedimenti")]
    [System.SerializableAttribute()]
    public partial class RisultatoRicercaTestualeEndo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.CategoriaEndoFrontoffice> CategorieField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.EndoBreveFrontoffice> EndoprocedimentiField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.FamigliaEndoFrontoffice> FamiglieField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.CategoriaEndoFrontoffice> Categorie {
            get {
                return this.CategorieField;
            }
            set {
                if ((object.ReferenceEquals(this.CategorieField, value) != true)) {
                    this.CategorieField = value;
                    this.RaisePropertyChanged("Categorie");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.EndoBreveFrontoffice> Endoprocedimenti {
            get {
                return this.EndoprocedimentiField;
            }
            set {
                if ((object.ReferenceEquals(this.EndoprocedimentiField, value) != true)) {
                    this.EndoprocedimentiField = value;
                    this.RaisePropertyChanged("Endoprocedimenti");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.FamigliaEndoFrontoffice> Famiglie {
            get {
                return this.FamiglieField;
            }
            set {
                if ((object.ReferenceEquals(this.FamiglieField, value) != true)) {
                    this.FamiglieField = value;
                    this.RaisePropertyChanged("Famiglie");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsEndoFrontoffice.IWsEndoFrontofficeService")]
    public interface IWsEndoFrontofficeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsEndoFrontofficeService/GetFamiglieEndoFrontoffice", ReplyAction="http://tempuri.org/IWsEndoFrontofficeService/GetFamiglieEndoFrontofficeResponse")]
        System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.FamigliaEndoFrontoffice> GetFamiglieEndoFrontoffice(string token, string software);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsEndoFrontofficeService/GetCategorieEndoFrontoffice", ReplyAction="http://tempuri.org/IWsEndoFrontofficeService/GetCategorieEndoFrontofficeResponse")]
        System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.CategoriaEndoFrontoffice> GetCategorieEndoFrontoffice(string token, string software, int codiceFamiglia);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsEndoFrontofficeService/GetListaEndoFrontoffice", ReplyAction="http://tempuri.org/IWsEndoFrontofficeService/GetListaEndoFrontofficeResponse")]
        System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.EndoBreveFrontoffice> GetListaEndoFrontoffice(string token, string software, int codiceCategoria);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsEndoFrontofficeService/GetGerarchiaEndo", ReplyAction="http://tempuri.org/IWsEndoFrontofficeService/GetGerarchiaEndoResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.RisultatoCaricamentoGerarchiaEndo GetGerarchiaEndo(string token, int valore, Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.LivelloCaricamentoGerarchia livelloRicerca);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsEndoFrontofficeService/RicercaTestualeEndo", ReplyAction="http://tempuri.org/IWsEndoFrontofficeService/RicercaTestualeEndoResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.RisultatoRicercaTestualeEndo RicercaTestualeEndo(string token, string software, string partial, Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.TipoRicercaEnum tipoRicerca);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWsEndoFrontofficeServiceChannel : Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.IWsEndoFrontofficeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsEndoFrontofficeServiceClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.IWsEndoFrontofficeService>, Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.IWsEndoFrontofficeService {
        
        public WsEndoFrontofficeServiceClient() {
        }
        
        public WsEndoFrontofficeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsEndoFrontofficeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsEndoFrontofficeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsEndoFrontofficeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.FamigliaEndoFrontoffice> GetFamiglieEndoFrontoffice(string token, string software) {
            return base.Channel.GetFamiglieEndoFrontoffice(token, software);
        }
        
        public System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.CategoriaEndoFrontoffice> GetCategorieEndoFrontoffice(string token, string software, int codiceFamiglia) {
            return base.Channel.GetCategorieEndoFrontoffice(token, software, codiceFamiglia);
        }
        
        public System.Collections.Generic.List<Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.EndoBreveFrontoffice> GetListaEndoFrontoffice(string token, string software, int codiceCategoria) {
            return base.Channel.GetListaEndoFrontoffice(token, software, codiceCategoria);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.RisultatoCaricamentoGerarchiaEndo GetGerarchiaEndo(string token, int valore, Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.LivelloCaricamentoGerarchia livelloRicerca) {
            return base.Channel.GetGerarchiaEndo(token, valore, livelloRicerca);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.RisultatoRicercaTestualeEndo RicercaTestualeEndo(string token, string software, string partial, Init.Sigepro.FrontEnd.AppLogic.WsEndoFrontoffice.TipoRicercaEnum tipoRicerca) {
            return base.Channel.RicercaTestualeEndo(token, software, partial, tipoRicerca);
        }
    }
}
