/*//------------------------------------------------------------------------------
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
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Web.Services2;

namespace Init.SIGePro.Protocollo.GeProt.Proxy
{
    /// <remarks/>
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [WebServiceBinding(Name = "ServicesBinding", Namespace = "http://it.grupposistematica.vbg.geprot")]
    public class ProtocolloGeProtService : WebServicesClientProtocol
    {

        /// <remarks/>
        public ProtocolloGeProtService(string url)
        {
            this.Url = url;
            this.Timeout = 600000;
        }

        /// <remarks/>
        [SoapRpcMethod("", RequestNamespace = "http://it.grupposistematica.vbg.geprot", ResponseNamespace = "http://it.grupposistematica.vbg.geprot")]
        [return: SoapElement("result")]
        public string doLogin(string String_1, string String_2)
        {
            object[] results = this.Invoke("doLogin", new object[] {
																	   String_1,
																	   String_2});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public IAsyncResult BegindoLogin(string String_1, string String_2, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("doLogin", new object[] {
																String_1,
																String_2}, callback, asyncState);
        }

        /// <remarks/>
        public string EnddoLogin(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        [SoapRpcMethod("", RequestNamespace = "http://it.grupposistematica.vbg.geprot", ResponseNamespace = "http://it.grupposistematica.vbg.geprot")]
        public void doLogout(string String_1)
        {
            this.Invoke("doLogout", new object[] {
													 String_1});
        }

        /// <remarks/>
        public IAsyncResult BegindoLogout(string String_1, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("doLogout", new object[] {
																 String_1}, callback, asyncState);
        }

        /// <remarks/>
        public void EnddoLogout(IAsyncResult asyncResult)
        {
            this.EndInvoke(asyncResult);
        }

        /// <remarks/>
        [SoapRpcMethod("", RequestNamespace = "http://it.grupposistematica.vbg.geprot", ResponseNamespace = "http://it.grupposistematica.vbg.geprot")]
        [return: SoapElement("result")]
        public string getInfo(string String_1, string[] arrayOfString_2)
        {
            object[] results = this.Invoke("getInfo", new object[] {
																	   String_1,
																	   arrayOfString_2});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public IAsyncResult BegingetInfo(string String_1, string[] arrayOfString_2, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getInfo", new object[] {
																String_1,
																arrayOfString_2}, callback, asyncState);
        }

        /// <remarks/>
        public string EndgetInfo(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        [SoapRpcMethod("", RequestNamespace = "http://it.grupposistematica.vbg.geprot", ResponseNamespace = "http://it.grupposistematica.vbg.geprot")]
        [return: SoapElement("result")]
        public string getInfoFascicoli(string String_1, string[] arrayOfString_2)
        {
            object[] results = this.Invoke("getInfoFascicoli", new object[] {
																				String_1,
																				arrayOfString_2});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public IAsyncResult BegingetInfoFascicoli(string String_1, string[] arrayOfString_2, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getInfoFascicoli", new object[] {
																		 String_1,
																		 arrayOfString_2}, callback, asyncState);
        }

        /// <remarks/>
        public string EndgetInfoFascicoli(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        [SoapRpcMethod("", RequestNamespace = "http://it.grupposistematica.vbg.geprot", ResponseNamespace = "http://it.grupposistematica.vbg.geprot")]
        [return: SoapElement("result")]
        public string[] protocolla(string String_1, string String_2)
        {
            object[] results = this.Invoke("protocolla", new object[] {
																		  String_1,
																		  String_2});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public IAsyncResult Beginprotocolla(string String_1, string String_2, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("protocolla", new object[] {
																   String_1,
																   String_2}, callback, asyncState);
        }

        /// <remarks/>
        public string[] Endprotocolla(IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        [SoapRpcMethod("", RequestNamespace = "http://it.grupposistematica.vbg.geprot", ResponseNamespace = "http://it.grupposistematica.vbg.geprot")]
        public void updateNote(string String_1, string[] arrayOfString_2, string String_3)
        {
            this.Invoke("updateNote", new object[] {
													   String_1,
													   arrayOfString_2,
													   String_3});
        }

        /// <remarks/>
        public IAsyncResult BeginupdateNote(string String_1, string[] arrayOfString_2, string String_3, AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("updateNote", new object[] {
																   String_1,
																   arrayOfString_2,
																   String_3}, callback, asyncState);
        }

        /// <remarks/>
        public void EndupdateNote(IAsyncResult asyncResult)
        {
            this.EndInvoke(asyncResult);
        }
    }
}
*/