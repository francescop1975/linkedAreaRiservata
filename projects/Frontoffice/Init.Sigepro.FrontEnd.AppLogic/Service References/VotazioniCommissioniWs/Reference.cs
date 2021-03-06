//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Init.Sigepro.FrontEnd.AppLogic.VotazioniCommissioniWs {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="VotazioniCommissioniWs.IWsVotazioniCommissione")]
    public interface IWsVotazioniCommissione {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsVotazioniCommissione/GetListaPareri", ReplyAction="http://tempuri.org/IWsVotazioniCommissione/GetListaPareriResponse")]
        Init.SIGePro.Manager.DTO.Commissioni.Votazioni.CommissioniVotiBaseDto[] GetListaPareri(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsVotazioniCommissione/GetVotoUtente", ReplyAction="http://tempuri.org/IWsVotazioniCommissione/GetVotoUtenteResponse")]
        Init.SIGePro.Manager.DTO.Commissioni.Votazioni.VotazionePraticaCommissioneDto GetVotoUtente(string token, int idCommissione, int codiceAnagrafe, string uuidIstanza);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsVotazioniCommissione/UtentePuoEsprimereVoto", ReplyAction="http://tempuri.org/IWsVotazioniCommissione/UtentePuoEsprimereVotoResponse")]
        bool UtentePuoEsprimereVoto(string token, int idCommissione, int codiceAnagrafe, string uuidIstanza);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWsVotazioniCommissione/EsprimiVotoPerUtente", ReplyAction="http://tempuri.org/IWsVotazioniCommissione/EsprimiVotoPerUtenteResponse")]
        void EsprimiVotoPerUtente(string token, int idCommissione, int codiceAnagrafe, string uuidIstanza, Init.SIGePro.Manager.DTO.Commissioni.Votazioni.VotoPraticaCommissioneDto voto);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWsVotazioniCommissioneChannel : Init.Sigepro.FrontEnd.AppLogic.VotazioniCommissioniWs.IWsVotazioniCommissione, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WsVotazioniCommissioneClient : System.ServiceModel.ClientBase<Init.Sigepro.FrontEnd.AppLogic.VotazioniCommissioniWs.IWsVotazioniCommissione>, Init.Sigepro.FrontEnd.AppLogic.VotazioniCommissioniWs.IWsVotazioniCommissione {
        
        public WsVotazioniCommissioneClient() {
        }
        
        public WsVotazioniCommissioneClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WsVotazioniCommissioneClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsVotazioniCommissioneClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WsVotazioniCommissioneClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Init.SIGePro.Manager.DTO.Commissioni.Votazioni.CommissioniVotiBaseDto[] GetListaPareri(string token) {
            return base.Channel.GetListaPareri(token);
        }
        
        public Init.SIGePro.Manager.DTO.Commissioni.Votazioni.VotazionePraticaCommissioneDto GetVotoUtente(string token, int idCommissione, int codiceAnagrafe, string uuidIstanza) {
            return base.Channel.GetVotoUtente(token, idCommissione, codiceAnagrafe, uuidIstanza);
        }
        
        public bool UtentePuoEsprimereVoto(string token, int idCommissione, int codiceAnagrafe, string uuidIstanza) {
            return base.Channel.UtentePuoEsprimereVoto(token, idCommissione, codiceAnagrafe, uuidIstanza);
        }
        
        public void EsprimiVotoPerUtente(string token, int idCommissione, int codiceAnagrafe, string uuidIstanza, Init.SIGePro.Manager.DTO.Commissioni.Votazioni.VotoPraticaCommissioneDto voto) {
            base.Channel.EsprimiVotoPerUtente(token, idCommissione, codiceAnagrafe, uuidIstanza, voto);
        }
    }
}
