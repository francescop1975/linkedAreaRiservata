//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// Codice sorgente generato automaticamente da wsdl, versione=1.1.4322.2032.
// 
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Web.Services2;

namespace Init.SIGePro.Protocollo.Sigedo.Proxies.Protocollazione
{
    /// <remarks/>
    //[DebuggerStepThrough()]
    //[DesignerCategory("code")]
    //[WebServiceBinding(Name="ServiceSoap", Namespace="http://tempuri.org/")]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "DOCAREAProtoSoap", Namespace = "http://tempuri.org/")]
    public class DOCAREAProto : WebServicesClientProtocol
    {
        public DOCAREAProto()
        {
            this.Timeout = 600000;
        }

        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Login", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public LoginRet Login(string strCodEnte, string strUserName, string strPassword)
        {
            object[] results = this.Invoke("Login", new object[] {
                    strCodEnte,
                    strUserName,
                    strPassword});
            return ((LoginRet)(results[0]));
        }

        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Inserimento", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public InserimentoRet Inserimento(string strUserName, string strDST)
        {
            object[] results = this.Invoke("Inserimento", new object[] {
                    strUserName,
                    strDST});
            return ((InserimentoRet)(results[0]));
        }
        
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Protocollazione", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ProtocollazioneRet Protocollazione(string strUserName, string strDST)
        {
            object[] results = this.Invoke("Protocollazione", new object[] {
                    strUserName,
                    strDST});
            return ((ProtocollazioneRet)(results[0]));
        }
        
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AggiungiAllegato", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public AggiungiAllegatoRet AggiungiAllegato(string strUserName, string strDST)
        {
            object[] results = this.Invoke("AggiungiAllegato", new object[] {
                    strUserName,
                    strDST});
            return ((AggiungiAllegatoRet)(results[0]));
        }

        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SostituisciDocumentoPrincipale", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SostituisciDocumentoPrincipaleRet SostituisciDocumentoPrincipale(string strUserName, string strDST)
        {
            object[] results = this.Invoke("SostituisciDocumentoPrincipale", new object[] {
                    strUserName,
                    strDST});
            return ((SostituisciDocumentoPrincipaleRet)(results[0]));
        }

        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SmistamentoAction", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SmistamentoActionRet SmistamentoAction(string strUserName, string strDST)
        {
            object[] results = this.Invoke("SmistamentoAction", new object[] {
                    strUserName,
                    strDST});
            return ((SmistamentoActionRet)(results[0]));
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public class LoginRet
    {

        /// <remarks/>
        public string strDST;

        /// <remarks/>
        public long lngErrNumber;

        /// <remarks/>
        public string strErrString;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public class ProtocollazioneRet
    {

        /// <remarks/>
        public long lngNumPG;

        /// <remarks/>
        public long lngAnnoPG;

        /// <remarks/>
        public string strDataPG;

        /// <remarks/>
        public long lngErrNumber;

        /// <remarks/>
        public string strErrString;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public class InserimentoRet
    {

        /// <remarks/>
        public long lngDocID;

        /// <remarks/>
        public long lngErrNumber;

        /// <remarks/>
        public string strErrString;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class AggiungiAllegatoRet
    {

        public long lngDocID;

        public long lngErrNumber;

        public string strErrString;
    }

    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class SostituisciDocumentoPrincipaleRet
    {
        public long lngDocID;

        public long lngErrNumber;

        public string strErrString;
    }

    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class SmistamentoActionRet
    {
        public long lngDocID;

        public long lngErrNumber;

        public string strErrString;
    }
}
