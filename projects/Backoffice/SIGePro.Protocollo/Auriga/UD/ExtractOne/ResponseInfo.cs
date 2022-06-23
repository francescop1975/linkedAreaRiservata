using Init.SIGePro.Protocollo.WsDataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Init.SIGePro.Protocollo.Auriga.UD.ExtractOne
{
    public class ResponseInfo : ProxyResponseInfo
    {
        public Output_FileUDToExtract ServiceResponse;

        public AllOut ToAllOut()
        {
            if (!String.IsNullOrEmpty(this.WsError))
            {
                throw new Exception(this.WsError);
            }

            if (this.ServiceResponse != null )
            {
                //return this.ServiceResponse.DatiFileEstratto.ToList().ConvertAll(new Converter<Output_FilesUDDatiFileEstratto, AllOut>(AllegatiToAllOut)).ToArray();
                return new AllOut
                {
                    IDBase = this.ServiceResponse.NroAllegato,
                    Image = this.ServiceResponse.Content,
                    Serial = (this.ServiceResponse.NomeFile != null) ? ((XmlNode[])this.ServiceResponse.NomeFile)[0].InnerText : "",
                    Commento = this.ServiceResponse.DesAllegato,
                    //TipoFile = (this.ServiceResponse.TipoDocAllegato != null ) ? this.ServiceResponse.TipoDocAllegato.Text[0] : "",
                    ContentType = ""
                };
            }
            return null;
        }
    }
}
