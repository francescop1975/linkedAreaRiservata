﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.WsQuestionarioFo {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WsQuestionarioFo.IWsQuestionarioFoService")]
    public interface IWsQuestionarioFoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsQuestionarioFoService/QuestionarioCompilato", ReplyAction="http://tempuri.org/IWsQuestionarioFoService/QuestionarioCompilatoResponse")]
        bool QuestionarioCompilato(string token, string uuidIstanza);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsQuestionarioFoService/SalvaQuestionario", ReplyAction="http://tempuri.org/IWsQuestionarioFoService/SalvaQuestionarioResponse")]
        void SalvaQuestionario(string token, string uuidIstanza, int valutazione, string note);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWsQuestionarioFoServiceChannel : Init.Sigepro.FrontEnd.AppLogic.WsQuestionarioFo.IWsQuestionarioFoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsQuestionarioFoServiceClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.WsQuestionarioFo.IWsQuestionarioFoService>, Init.Sigepro.FrontEnd.AppLogic.WsQuestionarioFo.IWsQuestionarioFoService {
        
        public WsQuestionarioFoServiceClient() {
        }
        
        public WsQuestionarioFoServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsQuestionarioFoServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsQuestionarioFoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsQuestionarioFoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool QuestionarioCompilato(string token, string uuidIstanza) {
            return base.Channel.QuestionarioCompilato(token, uuidIstanza);
        }
        
        public void SalvaQuestionario(string token, string uuidIstanza, int valutazione, string note) {
            base.Channel.SalvaQuestionario(token, uuidIstanza, valutazione, note);
        }
    }
}
