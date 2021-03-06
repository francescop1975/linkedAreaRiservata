//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
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

namespace Init.SIGePro.Protocollo.EGrammata.Proxy.NuovaUD
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "WSNuovaUDSoapBinding", Namespace = "http://10.1.1.189:8080/wsfe/services/WSNuovaUD")]
    public partial class WSNuovaUDService : WebServicesClientProtocol
    {

        private System.Threading.SendOrPostCallback serviceOperationCompleted;

        /// <remarks/>
        public WSNuovaUDService(string url)
        {
            this.Url = url;
        }

        /// <remarks/>
        public event serviceCompletedEventHandler serviceCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://protocollo.webservices.eng", ResponseNamespace = "http://protocollo.webservices.eng", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("serviceReturn")]
        public string service(string userid, string password, string IDUnita, string livelliUnita, string xml)
        {
            object[] results = this.Invoke("service", new object[] {
                    userid,
                    password,
                    IDUnita,
                    livelliUnita,
                    xml});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult Beginservice(string userid, string password, string IDUnita, string livelliUnita, string xml, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("service", new object[] {
                    userid,
                    password,
                    IDUnita,
                    livelliUnita,
                    xml}, callback, asyncState);
        }

        /// <remarks/>
        public string Endservice(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void serviceAsync(string userid, string password, string IDUnita, string livelliUnita, string xml)
        {
            this.serviceAsync(userid, password, IDUnita, livelliUnita, xml, null);
        }

        /// <remarks/>
        public void serviceAsync(string userid, string password, string IDUnita, string livelliUnita, string xml, object userState)
        {
            if ((this.serviceOperationCompleted == null))
            {
                this.serviceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnserviceOperationCompleted);
            }
            this.InvokeAsync("service", new object[] {
                    userid,
                    password,
                    IDUnita,
                    livelliUnita,
                    xml}, this.serviceOperationCompleted, userState);
        }

        private void OnserviceOperationCompleted(object arg)
        {
            if ((this.serviceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.serviceCompleted(this, new serviceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void serviceCompletedEventHandler(object sender, serviceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class serviceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal serviceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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