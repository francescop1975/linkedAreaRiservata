using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.ServiceModel;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Init.SIGePro.Protocollo.Constants;
using Microsoft.Web.Services2;

namespace Init.SIGePro.Protocollo.StudioK
{
    //------------------------------------------------------------------------------
    // <auto-generated>
    //     This code was generated by a tool.
    //     Runtime Version:4.0.30319.1022
    //
    //     Changes to this file may cause incorrect behavior and will be lost if
    //     the code is regenerated.
    // </auto-generated>
    //------------------------------------------------------------------------------

    // 
    // This source code was auto-generated by wsdl, Version=4.0.30319.1.
    // 

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ProWSApiSoapBinding", Namespace = "urn:ProWSApi")]
    public partial class ProtocolloStudioKStub : WebServicesClientProtocol 
    {
        /// <remarks/>
        public ProtocolloStudioKStub(string url, string proxyAddress)
        {
            this.Url = url;
            this.Timeout = 600000;
            if (!String.IsNullOrEmpty(proxyAddress))
            {
                this.Proxy = new WebProxy(proxyAddress, true);

                var endPointAddress = new EndpointAddress(url);

                if (endPointAddress.Uri.Scheme.ToLower() == ProtocolloConstants.HTTPS)
                {
                    //Trust all certificates
                    System.Net.ServicePointManager.ServerCertificateValidationCallback =
                        ((sender, certificate, chain, sslPolicyErrors) => true);

                    // trust sender
                    System.Net.ServicePointManager.ServerCertificateValidationCallback
                                    = ((sender, cert, chain, errors) => true);

                    // validate cert by calling a function
                    ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
                }
            }

        }

        private bool ValidateRemoteCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        [System.Web.Services.Protocols.SoapRpcMethodAttribute("GetDllVersion", RequestNamespace = "urn:ProWSApi", ResponseNamespace = "urn:ProWSApi")]
        [return: System.Xml.Serialization.XmlElementAttribute("GetDllVersionReturn")]
        public string GetDllVersion()
        {
            object[] results = this.Invoke("GetDllVersion", new object[0]);
            return ((string)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("GetSystemInfo", RequestNamespace = "urn:ProWSApi", ResponseNamespace = "urn:ProWSApi")]
        [return: System.Xml.Serialization.XmlElementAttribute("GetSystemInfoReturn")]
        public string GetSystemInfo()
        {
            object[] results = this.Invoke("GetSystemInfo", new object[0]);
            return ((string)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("registraProtocollo", RequestNamespace = "urn:ProWSApi", ResponseNamespace = "urn:ProWSApi")]
        [return: System.Xml.Serialization.XmlElementAttribute("registraProtocolloReturn")]
        public ProtocollazioneRet registraProtocollo(string connectionString)
        {
            object[] results = this.Invoke("registraProtocollo", new object[] {
                    connectionString});
            return ((ProtocollazioneRet)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("insertDocumento", RequestNamespace = "urn:ProWSApi", ResponseNamespace = "urn:ProWSApi")]
        [return: System.Xml.Serialization.XmlElementAttribute("insertDocumentoReturn")]
        public InserimentoRet insertDocumento(string connectionString, string documentName, string documentDescription)
        {
            object[] results = this.Invoke("insertDocumento", new object[] {
                    connectionString,
                    documentName,
                    documentDescription});
            return ((InserimentoRet)(results[0]));
        }


        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("infoProtocollo", RequestNamespace = "urn:ProWSApi", ResponseNamespace = "urn:ProWSApi")]
        [return: System.Xml.Serialization.XmlElementAttribute("infoProtocolloReturn")]
        public string infoProtocollo(string connectionString, long numProt, long annoProt, string aoo)
        {
            object[] results = this.Invoke("infoProtocollo", new object[] {
                    connectionString,
                    numProt,
                    annoProt,
                    aoo});
            return ((string)(results[0]));
        }


        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("queryProtocollo", RequestNamespace = "urn:ProWSApi", ResponseNamespace = "urn:ProWSApi")]
        [return: System.Xml.Serialization.XmlElementAttribute("queryProtocolloReturn")]
        public string queryProtocollo(string connectionString, string dataIni, string dataEnd, long annoProt, string Ufficio, string aoo)
        {
            object[] results = this.Invoke("queryProtocollo", new object[] {
                    connectionString,
                    dataIni,
                    dataEnd,
                    annoProt,
                    Ufficio,
                    aoo});
            return ((string)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("getDocumento", RequestNamespace = "urn:ProWSApi", ResponseNamespace = "urn:ProWSApi")]
        [return: System.Xml.Serialization.XmlElementAttribute("getDocumentoReturn")]
        public string getDocumento(string connectionString, long numProt, long annoProt, string documentName, string aoo)
        {
            object[] results = this.Invoke("getDocumento", new object[] {
                    connectionString,
                    numProt,
                    annoProt,
                    documentName,
                    aoo});
            return ((string)(results[0]));
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://impl.webservices.protocollo.pubblici.saga.it")]
    public partial class ProtocollazioneRet
    {

        private long lngNumPGField;

        private long lngAnnoPGField;

        private long lngErrNumberField;

        private string strErrStringField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long lngNumPG
        {
            get
            {
                return this.lngNumPGField;
            }
            set
            {
                this.lngNumPGField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long lngAnnoPG
        {
            get
            {
                return this.lngAnnoPGField;
            }
            set
            {
                this.lngAnnoPGField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long lngErrNumber
        {
            get
            {
                return this.lngErrNumberField;
            }
            set
            {
                this.lngErrNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string strErrString
        {
            get
            {
                return this.strErrStringField;
            }
            set
            {
                this.strErrStringField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://impl.webservices.protocollo.pubblici.saga.it")]
    public partial class InserimentoRet
    {

        private long lngDocIDField;

        private long lngErrNumberField;

        private string strErrStringField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long lngDocID
        {
            get
            {
                return this.lngDocIDField;
            }
            set
            {
                this.lngDocIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long lngErrNumber
        {
            get
            {
                return this.lngErrNumberField;
            }
            set
            {
                this.lngErrNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string strErrString
        {
            get
            {
                return this.strErrStringField;
            }
            set
            {
                this.strErrStringField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void GetDllVersionCompletedEventHandler(object sender, GetDllVersionCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDllVersionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetDllVersionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
    public delegate void GetSystemInfoCompletedEventHandler(object sender, GetSystemInfoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetSystemInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetSystemInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
    public delegate void registraProtocolloCompletedEventHandler(object sender, registraProtocolloCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class registraProtocolloCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal registraProtocolloCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ProtocollazioneRet Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ProtocollazioneRet)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void insertDocumentoCompletedEventHandler(object sender, insertDocumentoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insertDocumentoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal insertDocumentoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public InserimentoRet Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((InserimentoRet)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void infoProtocolloCompletedEventHandler(object sender, infoProtocolloCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class infoProtocolloCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal infoProtocolloCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
    public delegate void queryProtocolloCompletedEventHandler(object sender, queryProtocolloCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class queryProtocolloCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal queryProtocolloCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
    public delegate void getDocumentoCompletedEventHandler(object sender, getDocumentoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getDocumentoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getDocumentoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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

