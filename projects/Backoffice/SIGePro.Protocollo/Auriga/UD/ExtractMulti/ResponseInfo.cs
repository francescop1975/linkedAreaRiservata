using Init.SIGePro.Protocollo.WsDataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Init.SIGePro.Protocollo.Auriga.UD.ExtractMulti
{

    public class ResponseInfo : ProxyResponseInfo
    {
 
        public Output_FilesUD ServiceResponse;

        public AllOut[] ToAllOut()
        {
            if (!String.IsNullOrEmpty(this.WsError))
            {
                throw new Exception(this.WsError);
            }

            if (this.ServiceResponse != null && this.ServiceResponse.DatiFileEstratto != null )
            {
                return this.ServiceResponse.DatiFileEstratto.ToList().ConvertAll( new Converter<Output_FilesUDDatiFileEstratto, AllOut>(AllegatiToAllOut)).ToArray();
            }
            return null;
        }

        private static AllOut AllegatiToAllOut(Output_FilesUDDatiFileEstratto allegato)
        {
            return new AllOut
            {
                IDBase = allegato.NroAllegato,
                Image = allegato.Content,
                Serial = (allegato.NomeFile != null) ? ((XmlNode[])allegato.NomeFile)[0].InnerText : "",
                Commento = allegato.DesOggetto,
                TipoFile = (allegato.TipoDoc != null ) ? allegato.TipoDoc.Decodifica_Nome : "",
                ContentType = ""
            };
        }
    }
}
