//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://gruppoinit.it/stcmobile/", ConfigurationName="VisuraMobileServices.StcMobileWS")]
    public interface StcMobileWS {
        
        // CODEGEN: Generating message contract since element name shortid from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://gruppoinit.it/stcmobile/registraProfilo", ReplyAction="*")]
        Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloResponse registraProfilo(Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class registraProfiloRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="registraProfilo", Namespace="http://gruppoinit.it/stcmobile/", Order=0)]
        public Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloRequestBody Body;
        
        public registraProfiloRequest() {
        }
        
        public registraProfiloRequest(Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class registraProfiloRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string shortid;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string nome;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string cognome;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string codiceFiscale;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string aliasSportello;
        
        public registraProfiloRequestBody() {
        }
        
        public registraProfiloRequestBody(string shortid, string nome, string cognome, string codiceFiscale, string aliasSportello) {
            this.shortid = shortid;
            this.nome = nome;
            this.cognome = cognome;
            this.codiceFiscale = codiceFiscale;
            this.aliasSportello = aliasSportello;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class registraProfiloResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="registraProfiloResponse", Namespace="http://gruppoinit.it/stcmobile/", Order=0)]
        public Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloResponseBody Body;
        
        public registraProfiloResponse() {
        }
        
        public registraProfiloResponse(Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class registraProfiloResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int code;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string message;
        
        public registraProfiloResponseBody() {
        }
        
        public registraProfiloResponseBody(int code, string message) {
            this.code = code;
            this.message = message;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface StcMobileWSChannel : Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.StcMobileWS, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StcMobileWSClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.StcMobileWS>, Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.StcMobileWS {
        
        public StcMobileWSClient() {
        }
        
        public StcMobileWSClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StcMobileWSClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StcMobileWSClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StcMobileWSClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloResponse Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.StcMobileWS.registraProfilo(Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloRequest request) {
            return base.Channel.registraProfilo(request);
        }
        
        public int registraProfilo(string shortid, string nome, string cognome, string codiceFiscale, string aliasSportello, out string message) {
            Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloRequest inValue = new Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloRequest();
            inValue.Body = new Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloRequestBody();
            inValue.Body.shortid = shortid;
            inValue.Body.nome = nome;
            inValue.Body.cognome = cognome;
            inValue.Body.codiceFiscale = codiceFiscale;
            inValue.Body.aliasSportello = aliasSportello;
            Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.registraProfiloResponse retVal = ((Init.Sigepro.FrontEnd.AppLogic.VisuraMobileServices.StcMobileWS)(this)).registraProfilo(inValue);
            message = retVal.Body.message;
            return retVal.Body.code;
        }
    }
}
