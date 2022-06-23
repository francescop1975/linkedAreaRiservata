using Init.SIGePro.Protocollo.AurigaProxyService;
using System.IO;
using System.Xml;

namespace Init.SIGePro.Protocollo.Auriga.Login
{
    public class ResponseInfoAdapter: ProxyResponseInfoAdapter
    {

        public ResponseInfoAdapter(AurigaProxyResponseType response) : base(response)
        {

        }

        public ResponseInfo Adatta( )
        {
            return new ResponseInfo
            {
  
                WsResult = GetResult(),
                WarningMessage = GetWarningMessage(),
                WsError = GetWsError(),
                TokenConnessione = GetToken()
            };
        }

        

        protected TokenConnessione GetToken()
        {
            TokenConnessione retVal = new TokenConnessione();
            retVal.Token = new TokenConnessioneElement();

            if ( this._response.allegati != null )
            {
                XmlDocument myXML = new XmlDocument();
                MemoryStream ms = new MemoryStream(this._response.allegati[0].binaryData);
                myXML.Load(ms);

                var list = myXML.GetElementsByTagName("TokenConnessione");
                if( list != null)
                {
                    var el = list[0];
                    foreach (XmlAttribute attr in el.Attributes)
                    {
                        switch (attr.Name)
                        {
                            case "DesUser":
                                {
                                    retVal.Token.DesUser = attr.Value;
                                    break;
                                }
                            case "IdDominio":
                                {
                                    retVal.Token.IdDominio = attr.Value;
                                    break;
                                }
                            default:
                                break;
                        }
                    }

                    retVal.Token.Valore = el.InnerText;
                }
            }

            return retVal;
        }
    }
}
