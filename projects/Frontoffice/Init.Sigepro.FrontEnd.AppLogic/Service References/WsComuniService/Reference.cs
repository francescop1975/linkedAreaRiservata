﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.WsComuniService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DatiComuneCompatto", Namespace="http://schemas.datacontract.org/2004/07/Init.SIGePro.Manager.Logic.GestioneComuni" +
        "")]
    [System.SerializableAttribute()]
    public partial class DatiComuneCompatto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CfField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodiceComuneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ComuneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProvinciaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SiglaProvinciaField;
        
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
        public string Cf {
            get {
                return this.CfField;
            }
            set {
                if ((object.ReferenceEquals(this.CfField, value) != true)) {
                    this.CfField = value;
                    this.RaisePropertyChanged("Cf");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodiceComune {
            get {
                return this.CodiceComuneField;
            }
            set {
                if ((object.ReferenceEquals(this.CodiceComuneField, value) != true)) {
                    this.CodiceComuneField = value;
                    this.RaisePropertyChanged("CodiceComune");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Comune {
            get {
                return this.ComuneField;
            }
            set {
                if ((object.ReferenceEquals(this.ComuneField, value) != true)) {
                    this.ComuneField = value;
                    this.RaisePropertyChanged("Comune");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Provincia {
            get {
                return this.ProvinciaField;
            }
            set {
                if ((object.ReferenceEquals(this.ProvinciaField, value) != true)) {
                    this.ProvinciaField = value;
                    this.RaisePropertyChanged("Provincia");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SiglaProvincia {
            get {
                return this.SiglaProvinciaField;
            }
            set {
                if ((object.ReferenceEquals(this.SiglaProvinciaField, value) != true)) {
                    this.SiglaProvinciaField = value;
                    this.RaisePropertyChanged("SiglaProvincia");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DatiProvinciaCompatto", Namespace="http://schemas.datacontract.org/2004/07/Init.SIGePro.Manager.Logic.GestioneComuni" +
        "")]
    [System.SerializableAttribute()]
    public partial class DatiProvinciaCompatto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProvinciaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SiglaProvinciaField;
        
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
        public string Provincia {
            get {
                return this.ProvinciaField;
            }
            set {
                if ((object.ReferenceEquals(this.ProvinciaField, value) != true)) {
                    this.ProvinciaField = value;
                    this.RaisePropertyChanged("Provincia");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SiglaProvincia {
            get {
                return this.SiglaProvinciaField;
            }
            set {
                if ((object.ReferenceEquals(this.SiglaProvinciaField, value) != true)) {
                    this.SiglaProvinciaField = value;
                    this.RaisePropertyChanged("SiglaProvincia");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="CittadinanzaCompatto", Namespace="http://schemas.datacontract.org/2004/07/Init.SIGePro.Manager.Logic.GestioneCittad" +
        "inanze")]
    [System.SerializableAttribute()]
    public partial class CittadinanzaCompatto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CfField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodiceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescrizioneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool DisabilitatoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool FlgPaeseComunitarioField;
        
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
        public string Cf {
            get {
                return this.CfField;
            }
            set {
                if ((object.ReferenceEquals(this.CfField, value) != true)) {
                    this.CfField = value;
                    this.RaisePropertyChanged("Cf");
                }
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Disabilitato {
            get {
                return this.DisabilitatoField;
            }
            set {
                if ((this.DisabilitatoField.Equals(value) != true)) {
                    this.DisabilitatoField = value;
                    this.RaisePropertyChanged("Disabilitato");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool FlgPaeseComunitario {
            get {
                return this.FlgPaeseComunitarioField;
            }
            set {
                if ((this.FlgPaeseComunitarioField.Equals(value) != true)) {
                    this.FlgPaeseComunitarioField = value;
                    this.RaisePropertyChanged("FlgPaeseComunitario");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsComuniService.IWsComuniService")]
    public interface IWsComuniService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/FindComuniDaMatchParziale", ReplyAction="http://tempuri.org/IWsComuniService/FindComuniDaMatchParzialeResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiComuneCompatto[] FindComuniDaMatchParziale(string token, string matchComune);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/GetListaComuni", ReplyAction="http://tempuri.org/IWsComuniService/GetListaComuniResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiComuneCompatto[] GetListaComuni(string token, string siglaProvincia);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/GetDatiComune", ReplyAction="http://tempuri.org/IWsComuniService/GetDatiComuneResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiComuneCompatto GetDatiComune(string token, string codiceComune);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/GetDatiProvincia", ReplyAction="http://tempuri.org/IWsComuniService/GetDatiProvinciaResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiProvinciaCompatto GetDatiProvincia(string token, string siglaProvincia);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/GetDatiProvinciaDaCodiceComune", ReplyAction="http://tempuri.org/IWsComuniService/GetDatiProvinciaDaCodiceComuneResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiProvinciaCompatto GetDatiProvinciaDaCodiceComune(string token, string codiceComune);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/GetListaProvincie", ReplyAction="http://tempuri.org/IWsComuniService/GetListaProvincieResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiProvinciaCompatto[] GetListaProvincie(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/GetComuniAssociati", ReplyAction="http://tempuri.org/IWsComuniService/GetComuniAssociatiResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiComuneCompatto[] GetComuniAssociati(string token, string software);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/GetPecComuneAssociato", ReplyAction="http://tempuri.org/IWsComuniService/GetPecComuneAssociatoResponse")]
        string GetPecComuneAssociato(string token, string codiceComuneAssociato, string software);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/GetComuneDaCodiceIstat", ReplyAction="http://tempuri.org/IWsComuniService/GetComuneDaCodiceIstatResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiComuneCompatto GetComuneDaCodiceIstat(string token, string codiceIstat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/GetCittadinanze", ReplyAction="http://tempuri.org/IWsComuniService/GetCittadinanzeResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsComuniService.CittadinanzaCompatto[] GetCittadinanze(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsComuniService/GetCittadinanzaById", ReplyAction="http://tempuri.org/IWsComuniService/GetCittadinanzaByIdResponse")]
        Init.Sigepro.FrontEnd.AppLogic.WsComuniService.CittadinanzaCompatto GetCittadinanzaById(string token, int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWsComuniServiceChannel : Init.Sigepro.FrontEnd.AppLogic.WsComuniService.IWsComuniService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsComuniServiceClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.WsComuniService.IWsComuniService>, Init.Sigepro.FrontEnd.AppLogic.WsComuniService.IWsComuniService {
        
        public WsComuniServiceClient() {
        }
        
        public WsComuniServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsComuniServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsComuniServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsComuniServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiComuneCompatto[] FindComuniDaMatchParziale(string token, string matchComune) {
            return base.Channel.FindComuniDaMatchParziale(token, matchComune);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiComuneCompatto[] GetListaComuni(string token, string siglaProvincia) {
            return base.Channel.GetListaComuni(token, siglaProvincia);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiComuneCompatto GetDatiComune(string token, string codiceComune) {
            return base.Channel.GetDatiComune(token, codiceComune);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiProvinciaCompatto GetDatiProvincia(string token, string siglaProvincia) {
            return base.Channel.GetDatiProvincia(token, siglaProvincia);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiProvinciaCompatto GetDatiProvinciaDaCodiceComune(string token, string codiceComune) {
            return base.Channel.GetDatiProvinciaDaCodiceComune(token, codiceComune);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiProvinciaCompatto[] GetListaProvincie(string token) {
            return base.Channel.GetListaProvincie(token);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiComuneCompatto[] GetComuniAssociati(string token, string software) {
            return base.Channel.GetComuniAssociati(token, software);
        }
        
        public string GetPecComuneAssociato(string token, string codiceComuneAssociato, string software) {
            return base.Channel.GetPecComuneAssociato(token, codiceComuneAssociato, software);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsComuniService.DatiComuneCompatto GetComuneDaCodiceIstat(string token, string codiceIstat) {
            return base.Channel.GetComuneDaCodiceIstat(token, codiceIstat);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsComuniService.CittadinanzaCompatto[] GetCittadinanze(string token) {
            return base.Channel.GetCittadinanze(token);
        }
        
        public Init.Sigepro.FrontEnd.AppLogic.WsComuniService.CittadinanzaCompatto GetCittadinanzaById(string token, int id) {
            return base.Channel.GetCittadinanzaById(token, id);
        }
    }
}
