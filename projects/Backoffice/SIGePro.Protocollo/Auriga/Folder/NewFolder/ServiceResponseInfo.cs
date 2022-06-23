using System.Xml.Serialization;

namespace Init.SIGePro.Protocollo.Auriga.Folder.NewFolder
{
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "IdFolder")]
    public class ServiceResponseInfo
    {
        [XmlText]
        public string IdFolder { get; set; }
    }
}
