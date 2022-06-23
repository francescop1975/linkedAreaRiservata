﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.CommissioniWs {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CommissioniWs.IWsCommissioni")]
    public interface IWsCommissioni {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsCommissioni/GetCommissioniAperteByCodiceAnagrafe", ReplyAction="http://tempuri.org/IWsCommissioni/GetCommissioniAperteByCodiceAnagrafeResponse")]
        Init.SIGePro.Manager.DTO.Commissioni.CommissioneTestataDto[] GetCommissioniAperteByCodiceAnagrafe(string token, int codiceAnagrafe);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsCommissioni/GetDettaglioCommissione", ReplyAction="http://tempuri.org/IWsCommissioni/GetDettaglioCommissioneResponse")]
        Init.SIGePro.Manager.DTO.Commissioni.DettaglioCommissioneDto GetDettaglioCommissione(string token, int idCommissione, int codiceAnagrafe);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsCommissioni/GetDettaglioPratica", ReplyAction="http://tempuri.org/IWsCommissioni/GetDettaglioPraticaResponse")]
        Init.SIGePro.Manager.DTO.Commissioni.PraticaCommissioneEstesaDto GetDettaglioPratica(string token, int idCommissione, int codiceAnagrafe, string uuidIstanza);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsCommissioni/VerificaAccessoFile", ReplyAction="http://tempuri.org/IWsCommissioni/VerificaAccessoFileResponse")]
        bool VerificaAccessoFile(string token, int idCommissione, int codiceAnagrafe, string uuidIstanza, int codiceOggetto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsCommissioni/ApprovaAllegato", ReplyAction="http://tempuri.org/IWsCommissioni/ApprovaAllegatoResponse")]
        bool ApprovaAllegato(string token, int idCommissione, int idAllegato, int codiceAnagrafe);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWsCommissioniChannel : Init.Sigepro.FrontEnd.AppLogic.CommissioniWs.IWsCommissioni, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsCommissioniClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.CommissioniWs.IWsCommissioni>, Init.Sigepro.FrontEnd.AppLogic.CommissioniWs.IWsCommissioni {
        
        public WsCommissioniClient() {
        }
        
        public WsCommissioniClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsCommissioniClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsCommissioniClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsCommissioniClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Init.SIGePro.Manager.DTO.Commissioni.CommissioneTestataDto[] GetCommissioniAperteByCodiceAnagrafe(string token, int codiceAnagrafe) {
            return base.Channel.GetCommissioniAperteByCodiceAnagrafe(token, codiceAnagrafe);
        }
        
        public Init.SIGePro.Manager.DTO.Commissioni.DettaglioCommissioneDto GetDettaglioCommissione(string token, int idCommissione, int codiceAnagrafe) {
            return base.Channel.GetDettaglioCommissione(token, idCommissione, codiceAnagrafe);
        }
        
        public Init.SIGePro.Manager.DTO.Commissioni.PraticaCommissioneEstesaDto GetDettaglioPratica(string token, int idCommissione, int codiceAnagrafe, string uuidIstanza) {
            return base.Channel.GetDettaglioPratica(token, idCommissione, codiceAnagrafe, uuidIstanza);
        }
        
        public bool VerificaAccessoFile(string token, int idCommissione, int codiceAnagrafe, string uuidIstanza, int codiceOggetto) {
            return base.Channel.VerificaAccessoFile(token, idCommissione, codiceAnagrafe, uuidIstanza, codiceOggetto);
        }
        
        public bool ApprovaAllegato(string token, int idCommissione, int idAllegato, int codiceAnagrafe) {
            return base.Channel.ApprovaAllegato(token, idCommissione, idAllegato, codiceAnagrafe);
        }
    }
}
