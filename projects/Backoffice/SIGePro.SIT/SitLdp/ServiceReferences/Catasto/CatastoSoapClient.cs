﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Sit.SitLdp.ServiceReferences.Catasto
{
    //------------------------------------------------------------------------------
    // <auto-generated>
    //     Il codice è stato generato da uno strumento.
    //     Versione runtime:4.0.30319.42000
    //
    //     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
    //     il codice viene rigenerato.
    // </auto-generated>
    //------------------------------------------------------------------------------



    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "https://ws.ldpgis.it/", ConfigurationName = "CatastoSoap")]
    public interface CatastoSoap
    {

        // CODEGEN: Generazione di un contratto di messaggio perché l'operazione getSezioni non è RPC né incapsulata da documenti.
        [System.ServiceModel.OperationContractAttribute(Action = "https://ws.ldpgis.it/getSezioni", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        getSezioniResponse getSezioni(getSezioniRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "https://ws.ldpgis.it/getSezioni", ReplyAction = "*")]
        System.Threading.Tasks.Task<getSezioniResponse> getSezioniAsync(getSezioniRequest request);

        // CODEGEN: Generazione di un contratto di messaggio perché l'operazione getFogliBySezione non è RPC né incapsulata da documenti.
        [System.ServiceModel.OperationContractAttribute(Action = "https://ws.ldpgis.it/getFogliBySezione", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        getFogliBySezioneResponse getFogliBySezione(getFogliBySezioneRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "https://ws.ldpgis.it/getFogliBySezione", ReplyAction = "*")]
        System.Threading.Tasks.Task<getFogliBySezioneResponse> getFogliBySezioneAsync(getFogliBySezioneRequest request);

        // CODEGEN: Generazione di un contratto di messaggio perché l'operazione getParticelleBySezioneFoglio non è RPC né incapsulata da documenti.
        [System.ServiceModel.OperationContractAttribute(Action = "https://ws.ldpgis.it/getParticelleBySezioneFoglio", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        getParticelleBySezioneFoglioResponse getParticelleBySezioneFoglio(getParticelleBySezioneFoglioRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "https://ws.ldpgis.it/getParticelleBySezioneFoglio", ReplyAction = "*")]
        System.Threading.Tasks.Task<getParticelleBySezioneFoglioResponse> getParticelleBySezioneFoglioAsync(getParticelleBySezioneFoglioRequest request);

        // CODEGEN: Generazione di un contratto di messaggio perché l'operazione getSubalterniBySezioneFoglioParticella non è RPC né incapsulata da documenti.
        [System.ServiceModel.OperationContractAttribute(Action = "https://ws.ldpgis.it/getSubalterniBySezioneFoglioParticella", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        getSubalterniBySezioneFoglioParticellaResponse getSubalterniBySezioneFoglioParticella(getSubalterniBySezioneFoglioParticellaRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "https://ws.ldpgis.it/getSubalterniBySezioneFoglioParticella", ReplyAction = "*")]
        System.Threading.Tasks.Task<getSubalterniBySezioneFoglioParticellaResponse> getSubalterniBySezioneFoglioParticellaAsync(getSubalterniBySezioneFoglioParticellaRequest request);

        // CODEGEN: Generazione di un contratto di messaggio perché l'operazione isValidSubalterno non è RPC né incapsulata da documenti.
        [System.ServiceModel.OperationContractAttribute(Action = "https://ws.ldpgis.it/isValidSubalterno", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        isValidSubalternoResponse isValidSubalterno(isValidSubalternoRequest1 request);

        [System.ServiceModel.OperationContractAttribute(Action = "https://ws.ldpgis.it/isValidSubalterno", ReplyAction = "*")]
        System.Threading.Tasks.Task<isValidSubalternoResponse> isValidSubalternoAsync(isValidSubalternoRequest1 request);
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://ws.ldpgis.it/")]
    public partial class ComplexTypeStringa
    {

        private string testoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 0)]
        public string testo
        {
            get
            {
                return this.testoField;
            }
            set
            {
                this.testoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://ws.ldpgis.it/")]
    public partial class isValidSubalternoRequest
    {

        private string sezioneField;

        private string foglioField;

        private string particellaField;

        private string subalternoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 0)]
        public string sezione
        {
            get
            {
                return this.sezioneField;
            }
            set
            {
                this.sezioneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public string foglio
        {
            get
            {
                return this.foglioField;
            }
            set
            {
                this.foglioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 2)]
        public string particella
        {
            get
            {
                return this.particellaField;
            }
            set
            {
                this.particellaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 3)]
        public string subalterno
        {
            get
            {
                return this.subalternoField;
            }
            set
            {
                this.subalternoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://ws.ldpgis.it/")]
    public partial class ComplexTypeSubalterno
    {

        private string sezioneField;

        private string foglioField;

        private string particellaField;

        private string subalternoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 0)]
        public string sezione
        {
            get
            {
                return this.sezioneField;
            }
            set
            {
                this.sezioneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public string foglio
        {
            get
            {
                return this.foglioField;
            }
            set
            {
                this.foglioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 2)]
        public string particella
        {
            get
            {
                return this.particellaField;
            }
            set
            {
                this.particellaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 3)]
        public string subalterno
        {
            get
            {
                return this.subalternoField;
            }
            set
            {
                this.subalternoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://ws.ldpgis.it/")]
    public partial class ComplexTypeParticella
    {

        private string sezioneField;

        private string foglioField;

        private string particellaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 0)]
        public string sezione
        {
            get
            {
                return this.sezioneField;
            }
            set
            {
                this.sezioneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public string foglio
        {
            get
            {
                return this.foglioField;
            }
            set
            {
                this.foglioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 2)]
        public string particella
        {
            get
            {
                return this.particellaField;
            }
            set
            {
                this.particellaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://ws.ldpgis.it/")]
    public partial class ComplexTypeFoglio
    {

        private string sezioneField;

        private string foglioField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 0)]
        public string sezione
        {
            get
            {
                return this.sezioneField;
            }
            set
            {
                this.sezioneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true, Order = 1)]
        public string foglio
        {
            get
            {
                return this.foglioField;
            }
            set
            {
                this.foglioField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getSezioniRequest
    {

        public getSezioniRequest()
        {
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getSezioniResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "https://ws.ldpgis.it/", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public ComplexTypeStringa[] ComplexTypeStringaArray;

        public getSezioniResponse()
        {
        }

        public getSezioniResponse(ComplexTypeStringa[] ComplexTypeStringaArray)
        {
            this.ComplexTypeStringaArray = ComplexTypeStringaArray;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getFogliBySezioneRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "https://ws.ldpgis.it/", Order = 0)]
        public ComplexTypeStringa ComplexTypeStringa;

        public getFogliBySezioneRequest()
        {
        }

        public getFogliBySezioneRequest(ComplexTypeStringa ComplexTypeStringa)
        {
            this.ComplexTypeStringa = ComplexTypeStringa;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getFogliBySezioneResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "https://ws.ldpgis.it/", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public ComplexTypeFoglio[] ComplexTypeFoglioArray;

        public getFogliBySezioneResponse()
        {
        }

        public getFogliBySezioneResponse(ComplexTypeFoglio[] ComplexTypeFoglioArray)
        {
            this.ComplexTypeFoglioArray = ComplexTypeFoglioArray;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getParticelleBySezioneFoglioRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "https://ws.ldpgis.it/", Order = 0)]
        public ComplexTypeFoglio ComplexTypeFoglio;

        public getParticelleBySezioneFoglioRequest()
        {
        }

        public getParticelleBySezioneFoglioRequest(ComplexTypeFoglio ComplexTypeFoglio)
        {
            this.ComplexTypeFoglio = ComplexTypeFoglio;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getParticelleBySezioneFoglioResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "https://ws.ldpgis.it/", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public ComplexTypeParticella[] ComplexTypeParticellaArray;

        public getParticelleBySezioneFoglioResponse()
        {
        }

        public getParticelleBySezioneFoglioResponse(ComplexTypeParticella[] ComplexTypeParticellaArray)
        {
            this.ComplexTypeParticellaArray = ComplexTypeParticellaArray;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getSubalterniBySezioneFoglioParticellaRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "https://ws.ldpgis.it/", Order = 0)]
        public ComplexTypeParticella ComplexTypeParticella;

        public getSubalterniBySezioneFoglioParticellaRequest()
        {
        }

        public getSubalterniBySezioneFoglioParticellaRequest(ComplexTypeParticella ComplexTypeParticella)
        {
            this.ComplexTypeParticella = ComplexTypeParticella;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class getSubalterniBySezioneFoglioParticellaResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "https://ws.ldpgis.it/", Order = 0)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public ComplexTypeSubalterno[] ComplexTypeSubalternoArray;

        public getSubalterniBySezioneFoglioParticellaResponse()
        {
        }

        public getSubalterniBySezioneFoglioParticellaResponse(ComplexTypeSubalterno[] ComplexTypeSubalternoArray)
        {
            this.ComplexTypeSubalternoArray = ComplexTypeSubalternoArray;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class isValidSubalternoRequest1
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "https://ws.ldpgis.it/", Order = 0)]
        public isValidSubalternoRequest isValidSubalternoRequest;

        public isValidSubalternoRequest1()
        {
        }

        public isValidSubalternoRequest1(isValidSubalternoRequest isValidSubalternoRequest)
        {
            this.isValidSubalternoRequest = isValidSubalternoRequest;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class isValidSubalternoResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "https://ws.ldpgis.it/", Order = 0)]
        public ComplexTypeStringa ComplexTypeStringa;

        public isValidSubalternoResponse()
        {
        }

        public isValidSubalternoResponse(ComplexTypeStringa ComplexTypeStringa)
        {
            this.ComplexTypeStringa = ComplexTypeStringa;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CatastoSoapChannel : CatastoSoap, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CatastoSoapClient : System.ServiceModel.ClientBase<CatastoSoap>, CatastoSoap, ILDPSoapClient
    {

        public CatastoSoapClient()
        {
        }

        public CatastoSoapClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public CatastoSoapClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public CatastoSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public CatastoSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        getSezioniResponse CatastoSoap.getSezioni(getSezioniRequest request)
        {
            return base.Channel.getSezioni(request);
        }

        public ComplexTypeStringa[] getSezioni()
        {
            getSezioniRequest inValue = new getSezioniRequest();
            getSezioniResponse retVal = ((CatastoSoap)(this)).getSezioni(inValue);
            return retVal.ComplexTypeStringaArray;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<getSezioniResponse> CatastoSoap.getSezioniAsync(getSezioniRequest request)
        {
            return base.Channel.getSezioniAsync(request);
        }

        public System.Threading.Tasks.Task<getSezioniResponse> getSezioniAsync()
        {
            getSezioniRequest inValue = new getSezioniRequest();
            return ((CatastoSoap)(this)).getSezioniAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        getFogliBySezioneResponse CatastoSoap.getFogliBySezione(getFogliBySezioneRequest request)
        {
            return base.Channel.getFogliBySezione(request);
        }

        public ComplexTypeFoglio[] getFogliBySezione(ComplexTypeStringa ComplexTypeStringa)
        {
            getFogliBySezioneRequest inValue = new getFogliBySezioneRequest();
            inValue.ComplexTypeStringa = ComplexTypeStringa;
            getFogliBySezioneResponse retVal = ((CatastoSoap)(this)).getFogliBySezione(inValue);
            return retVal.ComplexTypeFoglioArray;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<getFogliBySezioneResponse> CatastoSoap.getFogliBySezioneAsync(getFogliBySezioneRequest request)
        {
            return base.Channel.getFogliBySezioneAsync(request);
        }

        public System.Threading.Tasks.Task<getFogliBySezioneResponse> getFogliBySezioneAsync(ComplexTypeStringa ComplexTypeStringa)
        {
            getFogliBySezioneRequest inValue = new getFogliBySezioneRequest();
            inValue.ComplexTypeStringa = ComplexTypeStringa;
            return ((CatastoSoap)(this)).getFogliBySezioneAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        getParticelleBySezioneFoglioResponse CatastoSoap.getParticelleBySezioneFoglio(getParticelleBySezioneFoglioRequest request)
        {
            return base.Channel.getParticelleBySezioneFoglio(request);
        }

        public ComplexTypeParticella[] getParticelleBySezioneFoglio(ComplexTypeFoglio ComplexTypeFoglio)
        {
            getParticelleBySezioneFoglioRequest inValue = new getParticelleBySezioneFoglioRequest();
            inValue.ComplexTypeFoglio = ComplexTypeFoglio;
            getParticelleBySezioneFoglioResponse retVal = ((CatastoSoap)(this)).getParticelleBySezioneFoglio(inValue);
            return retVal.ComplexTypeParticellaArray;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<getParticelleBySezioneFoglioResponse> CatastoSoap.getParticelleBySezioneFoglioAsync(getParticelleBySezioneFoglioRequest request)
        {
            return base.Channel.getParticelleBySezioneFoglioAsync(request);
        }

        public System.Threading.Tasks.Task<getParticelleBySezioneFoglioResponse> getParticelleBySezioneFoglioAsync(ComplexTypeFoglio ComplexTypeFoglio)
        {
            getParticelleBySezioneFoglioRequest inValue = new getParticelleBySezioneFoglioRequest();
            inValue.ComplexTypeFoglio = ComplexTypeFoglio;
            return ((CatastoSoap)(this)).getParticelleBySezioneFoglioAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        getSubalterniBySezioneFoglioParticellaResponse CatastoSoap.getSubalterniBySezioneFoglioParticella(getSubalterniBySezioneFoglioParticellaRequest request)
        {
            return base.Channel.getSubalterniBySezioneFoglioParticella(request);
        }

        public ComplexTypeSubalterno[] getSubalterniBySezioneFoglioParticella(ComplexTypeParticella ComplexTypeParticella)
        {
            getSubalterniBySezioneFoglioParticellaRequest inValue = new getSubalterniBySezioneFoglioParticellaRequest();
            inValue.ComplexTypeParticella = ComplexTypeParticella;
            getSubalterniBySezioneFoglioParticellaResponse retVal = ((CatastoSoap)(this)).getSubalterniBySezioneFoglioParticella(inValue);
            return retVal.ComplexTypeSubalternoArray;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<getSubalterniBySezioneFoglioParticellaResponse> CatastoSoap.getSubalterniBySezioneFoglioParticellaAsync(getSubalterniBySezioneFoglioParticellaRequest request)
        {
            return base.Channel.getSubalterniBySezioneFoglioParticellaAsync(request);
        }

        public System.Threading.Tasks.Task<getSubalterniBySezioneFoglioParticellaResponse> getSubalterniBySezioneFoglioParticellaAsync(ComplexTypeParticella ComplexTypeParticella)
        {
            getSubalterniBySezioneFoglioParticellaRequest inValue = new getSubalterniBySezioneFoglioParticellaRequest();
            inValue.ComplexTypeParticella = ComplexTypeParticella;
            return ((CatastoSoap)(this)).getSubalterniBySezioneFoglioParticellaAsync(inValue);
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        isValidSubalternoResponse CatastoSoap.isValidSubalterno(isValidSubalternoRequest1 request)
        {
            return base.Channel.isValidSubalterno(request);
        }

        public ComplexTypeStringa isValidSubalterno(isValidSubalternoRequest isValidSubalternoRequest)
        {
            isValidSubalternoRequest1 inValue = new isValidSubalternoRequest1();
            inValue.isValidSubalternoRequest = isValidSubalternoRequest;
            isValidSubalternoResponse retVal = ((CatastoSoap)(this)).isValidSubalterno(inValue);
            return retVal.ComplexTypeStringa;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<isValidSubalternoResponse> CatastoSoap.isValidSubalternoAsync(isValidSubalternoRequest1 request)
        {
            return base.Channel.isValidSubalternoAsync(request);
        }

        public System.Threading.Tasks.Task<isValidSubalternoResponse> isValidSubalternoAsync(isValidSubalternoRequest isValidSubalternoRequest)
        {
            isValidSubalternoRequest1 inValue = new isValidSubalternoRequest1();
            inValue.isValidSubalternoRequest = isValidSubalternoRequest;
            return ((CatastoSoap)(this)).isValidSubalternoAsync(inValue);
        }
    }

}