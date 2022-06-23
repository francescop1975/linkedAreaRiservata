﻿using Init.SIGePro.Protocollo.AurigaProxyService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Xml;

namespace Init.SIGePro.Protocollo.Auriga.UD.GetMetadataUd
{
    public class ResponseInfoAdapter: ProxyResponseInfoAdapter
    {
        public ResponseInfoAdapter(AurigaProxyResponseType response) : base(response)
        {

        }

        public ResponseInfo Adatta()
        {
            return new ResponseInfo
            {
                WarningMessage = base.GetWarningMessage(),
                WsError = base.GetWsError(),
                WsResult = base.GetResult(),
                DatiUD = GetDatiUD()
            };
        }

        protected DatiUD GetDatiUD()
        {
            if (this._response.allegati != null)
            {

                var resXml = StringSerializationExtensions.GetXmlDaStringaConEscapeHtmlEncoded(Encoding.Default.GetString(this._response.allegati[0].binaryData));
                resXml = resXml.Replace('\u00A0', ' ');

                return resXml.DeserializeXML<DatiUD>();
            }

            return null;
        }
    }
}
