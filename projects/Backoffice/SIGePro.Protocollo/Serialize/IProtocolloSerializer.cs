using Init.SIGePro.Protocollo.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.Serialize
{
    public interface IProtocolloSerializer
    {
        T Deserialize<T>(string xml);
        T Deserialize<T>(byte[] buffer);
        object Deserialize(string xmlString, Type type);
        MemoryStream SerializeToStream<T>(T dataObject);
        string Serialize(string sFileName, object pProtocollo);
        string Serialize(string sFileName, object pProtocollo, string messaggio);
        string Serialize(string sFileName, object pProtocollo, ProtocolloValidation.TipiValidazione tipoValidazione = ProtocolloValidation.TipiValidazione.XSD);
        string Serialize(string sFileName, object pProtocollo, ProtocolloValidation.TipiValidazione eTipoValidazione, string sTipoValidazione, bool bValidazione);
        void SerializeAndValidateStream(object pProtocollo, string sFileName);
        XmlAttributeOverrides GetOverrides();
    }
}
