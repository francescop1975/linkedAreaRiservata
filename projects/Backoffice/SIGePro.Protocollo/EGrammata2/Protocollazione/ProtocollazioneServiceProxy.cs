//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1022
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Web.Services2;

// 
// This source code was auto-generated by wsdl, Version=4.0.30319.1.
// 

namespace Init.SIGePro.Protocollo.EGrammata2.Protocollazione
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "WSProtocollazioneAllegatiSoapBinding", Namespace = "http://10.1.1.31:8080/egrammata/services/WSProtocollazioneAllegati")]
    public partial class ProtocollazioneServiceProxy : WebServicesClientProtocol
    {

        private System.Threading.SendOrPostCallback protocollaOperationCompleted;

        private System.Threading.SendOrPostCallback protocollaNSOperationCompleted;

        /// <remarks/>
        public ProtocollazioneServiceProxy(string url)
        {
            this.Url = url;
            this.Timeout = 1200000;
        }

        /// <remarks/>
        public event protocollaCompletedEventHandler protocollaCompleted;

        /// <remarks/>
        public event protocollaNSCompletedEventHandler protocollaNSCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://protocollo.webservices.eng", ResponseNamespace = "http://protocollo.webservices.eng", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("protocollaReturn")]
        public string protocolla(string codEnte, string username, string password, string indirizzoIP, string xml, string hash, string userApp, string postazione)
        {
            object[] results = this.Invoke("protocolla", new object[] {
                    codEnte,
                    username,
                    password,
                    indirizzoIP,
                    xml,
                    hash,
                    userApp,
                    postazione});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult Beginprotocolla(string codEnte, string username, string password, string indirizzoIP, string xml, string hash, string userApp, string postazione, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("protocolla", new object[] {
                    codEnte,
                    username,
                    password,
                    indirizzoIP,
                    xml,
                    hash,
                    userApp,
                    postazione}, callback, asyncState);
        }

        /// <remarks/>
        public string Endprotocolla(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void protocollaAsync(string codEnte, string username, string password, string indirizzoIP, string xml, string hash, string userApp, string postazione)
        {
            this.protocollaAsync(codEnte, username, password, indirizzoIP, xml, hash, userApp, postazione, null);
        }

        /// <remarks/>
        public void protocollaAsync(string codEnte, string username, string password, string indirizzoIP, string xml, string hash, string userApp, string postazione, object userState)
        {
            if ((this.protocollaOperationCompleted == null))
            {
                this.protocollaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnprotocollaOperationCompleted);
            }
            this.InvokeAsync("protocolla", new object[] {
                    codEnte,
                    username,
                    password,
                    indirizzoIP,
                    xml,
                    hash,
                    userApp,
                    postazione}, this.protocollaOperationCompleted, userState);
        }

        private void OnprotocollaOperationCompleted(object arg)
        {
            if ((this.protocollaCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.protocollaCompleted(this, new protocollaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://protocollo.webservices.eng", ResponseNamespace = "http://10.1.1.31:8080/egrammata/services/WSProtocollazioneAllegati", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("protocollaNSReturn")]
        public string protocollaNS(string codEnte, string username, string password, string indirizzoIP, string xml, string hash, string userApp, string postazione)
        {
            object[] results = this.Invoke("protocollaNS", new object[] {
                    codEnte,
                    username,
                    password,
                    indirizzoIP,
                    xml,
                    hash,
                    userApp,
                    postazione});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginprotocollaNS(string codEnte, string username, string password, string indirizzoIP, string xml, string hash, string userApp, string postazione, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("protocollaNS", new object[] {
                    codEnte,
                    username,
                    password,
                    indirizzoIP,
                    xml,
                    hash,
                    userApp,
                    postazione}, callback, asyncState);
        }

        /// <remarks/>
        public string EndprotocollaNS(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void protocollaNSAsync(string codEnte, string username, string password, string indirizzoIP, string xml, string hash, string userApp, string postazione)
        {
            this.protocollaNSAsync(codEnte, username, password, indirizzoIP, xml, hash, userApp, postazione, null);
        }

        /// <remarks/>
        public void protocollaNSAsync(string codEnte, string username, string password, string indirizzoIP, string xml, string hash, string userApp, string postazione, object userState)
        {
            if ((this.protocollaNSOperationCompleted == null))
            {
                this.protocollaNSOperationCompleted = new System.Threading.SendOrPostCallback(this.OnprotocollaNSOperationCompleted);
            }
            this.InvokeAsync("protocollaNS", new object[] {
                    codEnte,
                    username,
                    password,
                    indirizzoIP,
                    xml,
                    hash,
                    userApp,
                    postazione}, this.protocollaNSOperationCompleted, userState);
        }

        private void OnprotocollaNSOperationCompleted(object arg)
        {
            if ((this.protocollaNSCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.protocollaNSCompleted(this, new protocollaNSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void protocollaCompletedEventHandler(object sender, protocollaCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class protocollaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal protocollaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void protocollaNSCompletedEventHandler(object sender, protocollaNSCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class protocollaNSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal protocollaNSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}