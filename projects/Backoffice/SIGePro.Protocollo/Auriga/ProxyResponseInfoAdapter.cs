using Init.SIGePro.Protocollo.AurigaProxyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Init.SIGePro.Protocollo.Auriga
{
    public class ProxyResponseInfoAdapter
    {
        protected AurigaProxyResponseType _response;

        public ProxyResponseInfoAdapter(AurigaProxyResponseType response)
        {
            this._response = response;
        }

        protected string GetWsError()
        {
            var xml = Encoding.UTF8.GetString(Convert.FromBase64String(this._response.xml));

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            string xpath = "BaseOutput_WS/WSError";
            var node = xmlDoc.SelectSingleNode(xpath);

            if (node != null)
                return node.InnerText;

            return null;
        }

        protected string GetResult()
        {
            var xml = Encoding.UTF8.GetString(Convert.FromBase64String(this._response.xml));

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            string xpath = "BaseOutput_WS/WSResult";
            var node = xmlDoc.SelectSingleNode(xpath);

            if (node != null)
                return node.InnerText;

            return null;
        }

        protected string GetWarningMessage()
        {
            var xml = Encoding.UTF8.GetString(Convert.FromBase64String(this._response.xml));

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            string xpath = "BaseOutput_WS/WarningMessage";
            var node = xmlDoc.SelectSingleNode(xpath);

            if (node != null)
                return node.InnerText;

            return null;
        }

        protected string RemoveXMLDeclaration(string xml)
        {
            if( !string.IsNullOrEmpty(xml)  && xml.IndexOf("?>") > -1) { 
                return xml.Substring( xml.IndexOf("?>")+2 );
            }
            return xml;
        }
    }
}
