using Init.SIGePro.Protocollo.AurigaProxyService;
using System.Text;
using System.Web;
using System.Xml;


namespace Init.SIGePro.Protocollo.Auriga.UD.UppUD
{
    public class ResponseInfoAdapter: ProxyResponseInfoAdapter
    {
        public ResponseInfoAdapter(AurigaProxyResponseType response) : base(response)
        {
        }

        public ResponseInfo Adatta()
        {
            return new ResponseInfo {
                WarningMessage = base.GetWarningMessage(),
                WsError = base.GetWsError(),
                WsResult = base.GetResult(),
                ServiceResponse = GetServiceResponse()
            };
        }

        protected Output_UD GetServiceResponse()
        {
            if (this._response.allegati != null)
            {
                var resXml = StringSerializationExtensions.GetXmlDaStringaConEscapeHtmlEncoded(Encoding.Default.GetString(this._response.allegati[0].binaryData));
                resXml = resXml.Replace('\u00A0', ' ');

                return resXml.DeserializeXML<Output_UD>();
            }
            return null;
        }
    }
}
