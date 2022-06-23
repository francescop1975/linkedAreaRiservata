﻿using Init.SIGePro.Protocollo.AurigaProxyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Auriga.Folder.NewFolder
{
    public class ResponseInfoAdapter : ProxyResponseInfoAdapter
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
                IdFolder = GetIdFolder()
            };
        }

        protected string GetIdFolder()
        {
            if (this._response.allegati != null)
            {

                var resXml = StringSerializationExtensions.GetXmlDaStringaConEscape(Encoding.Default.GetString(this._response.allegati[0].binaryData));
                resXml = resXml.Replace('\u00A0', ' ');

                var r = resXml.DeserializeXML<ServiceResponseInfo>();
                if (r != null)
                    return r.IdFolder;
            }

            return null;
        }
    }
}
